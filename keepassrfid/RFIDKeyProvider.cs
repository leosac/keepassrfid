using KeePassLib.Keys;
using LibLogicalAccess;
using LibLogicalAccess.Card;
using LibLogicalAccess.Reader;
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
                ChipAction(new Action<Chip>(delegate (Chip chip)
                {
                    if (rfidConfig.KeyType == KeyType.NFC)
                    {
                        // Only tag type 4 supported for now.
                        NFCTagCardService nfcsvc = chip.getService(CardServiceType.CST_NFC_TAG) as NFCTagCardService;
                        if (nfcsvc == null)
                            throw new KeePassRFIDException(Properties.Resources.UnsupportedNFCTag);

                        NdefMessage msg = nfcsvc.readNDEF();
                        if (msg.getRecordCount() > 0)
                        {
                            NdefRecordCollection records = msg.getRecords();
                            if (records.Count > 0)
                            {
                                // Always use first record only
                                NdefRecord record = records[0];
                                // Don't care about payload type, use whole payload as the key
                                var payload = record.getPayload();
                                if (payload.Count > 0)
                                {
                                    key = payload.ToArray();
                                }
                            }
                        }
                    }
                    else
                    {
                        var csn = chip.getChipIdentifier();
                        if (csn.Count > 0)
                        {
                            key = csn.ToArray();
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

        public static void ChipAction(Action<Chip> action, KeePassRFIDConfig rfidConfig)
        {
            if (rfidConfig == null)
                rfidConfig = KeePassRFIDConfig.GetFromCurrentSession();

            ReaderProvider readerProvider;
            try
            {
                LibraryManager libmgt = LibraryManager.getInstance();

                readerProvider = libmgt.getReaderProvider(!String.IsNullOrEmpty(rfidConfig.ReaderProvider) ? rfidConfig.ReaderProvider : "PCSC");
                if (readerProvider == null)
                    throw new KeePassRFIDException(Properties.Resources.RFIDConfigurationError);
            }
            catch (COMException ex)
            {
                throw new KeePassRFIDException(Properties.Resources.RFIDInitError, ex);
            }

            ReaderUnit readerUnit = null;
            if (String.IsNullOrEmpty(rfidConfig.ReaderUnit))
            {
                readerUnit = readerProvider.createReaderUnit();
                if (readerUnit == null)
                    throw new KeePassRFIDException(Properties.Resources.RFIDReaderInitError);
            }
            else
            {
                var readers = readerProvider.getReaderList();
                foreach (ReaderUnit reader in readers)
                {
                    if (reader.getName() == rfidConfig.ReaderUnit)
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
                if (readerUnit.connectToReader())
                {
                    // Don't wait for chip insertion, it must already be on the reader
                    if (readerUnit.waitInsertion(1))
                    {
                        if (readerUnit.connect())
                        {
                            Chip chip = readerUnit.getSingleChip();
                            if (chip != null)
                            {
                                action(chip);
                            }
                            else
                            {
                                ShowError(Properties.Resources.NoChipError);
                            }

                            readerUnit.disconnect();
                        }
                        else
                        {
                            ShowError(Properties.Resources.CardConnectionFailed);
                        }
                        readerUnit.waitRemoval(1);
                    }
                    else
                    {
                        ShowError(Properties.Resources.NoCardInserted);
                    }

                    readerUnit.disconnectFromReader();
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
