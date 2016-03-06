using KeePassLib.Keys;
using LibLogicalAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace KeePassRFID
{
    public sealed class RFIDKeyProvider : KeyProvider
    {
        public override string Name
        {
            get { return "ISLOG RFID/NFC Key Provider"; }
        }

        public override byte[] GetKey(KeyProviderQueryContext ctx)
        {
            byte[] key = null;

            try
            {
                KeePassRFIDConfig rfidConfig = KeePassRFIDConfig.GetFromCurrentSession();
                ChipAction(new Action<IChip>(delegate (IChip chip)
                {
                    if (rfidConfig.KeyType == KeyType.NFC)
                    {
                        // Only tag type 4 supported for now.
                        IDESFireEV1NFCTag4CardService nfcsvc = chip.GetService(CardServiceType.CST_NFC_TAG) as IDESFireEV1NFCTag4CardService;
                        if (nfcsvc == null)
                            throw new KeePassRFIDException(Properties.Resources.UnsupportedNFCTag);

                        NdefMessage msg = nfcsvc.ReadNDEF();
                        if (msg.GetRecordCount() > 0)
                        {
                            object[] records = msg.Records as object[];
                            if (records != null)
                            {
                                // Always use first record only
                                INdefRecord record = records[0] as INdefRecord;
                                // Don't care about payload type, use whole payload as the key
                                key = record.Payload as byte[];
                            }
                        }
                    }
                    else
                    {
                        string csn = chip.ChipIdentifier;
                        if (!String.IsNullOrEmpty(csn))
                        {
                            key = new byte[csn.Length * sizeof(char)];
                            System.Buffer.BlockCopy(csn.ToCharArray(), 0, key, 0, key.Length);
                        }
                    }
                }), rfidConfig);
            }
            catch (KeePassRFIDException ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.PluginError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return key;
        }

        public static void ChipAction(Action<IChip> action, KeePassRFIDConfig rfidConfig)
        {
            if (rfidConfig == null)
                rfidConfig = KeePassRFIDConfig.GetFromCurrentSession();

            IReaderProvider readerProvider;
            try
            {
                ILibraryManager libmgt = new LibraryManager();

                readerProvider = libmgt.GetReaderProvider(!String.IsNullOrEmpty(rfidConfig.ReaderProvider) ? rfidConfig.ReaderProvider : "PCSC");
                if (readerProvider == null)
                    throw new KeePassRFIDException(Properties.Resources.RFIDConfigurationError);
            }
            catch (COMException ex)
            {
                throw new KeePassRFIDException(Properties.Resources.RFIDInitError, ex);
            }

            IReaderUnit readerUnit = null;
            if (String.IsNullOrEmpty(rfidConfig.ReaderUnit))
            {
                readerUnit = readerProvider.CreateReaderUnit();
                if (readerUnit == null)
                    throw new KeePassRFIDException(Properties.Resources.RFIDReaderInitError);
            }
            else
            {
                object[] readers = readerProvider.GetReaderList() as object[];
                foreach (IReaderUnit reader in readers)
                {
                    if (reader.name == rfidConfig.ReaderUnit)
                    {
                        readerUnit = reader;
                        break;
                    }
                }
            }

            if (readerUnit == null)
                throw new KeePassRFIDException(Properties.Resources.RFIDReaderNotFound);

            try
            {
                if (readerUnit.ConnectToReader())
                {
                    // Don't wait for chip insertion, it must already be on the reader
                    if (readerUnit.WaitInsertion(1))
                    {
                        if (readerUnit.Connect())
                        {
                            IChip chip = readerUnit.GetSingleChip();
                            if (chip != null)
                            {
                                action(chip);
                            }
                            else
                            {
                                ShowError(Properties.Resources.NoChipError);
                            }

                            readerUnit.Disconnect();
                        }
                        else
                        {
                            ShowError(Properties.Resources.CardConnectionFailed);
                        }
                        readerUnit.WaitRemoval(1);
                    }
                    else
                    {
                        ShowError(Properties.Resources.NoCardInserted);
                    }

                    readerUnit.DisconnectFromReader();
                }
                else
                {
                    ShowError(Properties.Resources.ReaderConnectionFailed);
                }
            }
            catch (COMException ex)
            {
                ShowError(ex.Message);
            }

            GC.KeepAlive(readerUnit);
            GC.KeepAlive(readerProvider);
        }

        private static void ShowError(string message)
        {
            MessageBox.Show(message, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
