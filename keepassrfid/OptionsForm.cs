using LibLogicalAccess;
using LibLogicalAccess.Card;
using LibLogicalAccess.Reader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace KeePassRFID
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void imgLogo_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.islog.com");
        }

        private void linkRefreshRU_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RefreshReaderUnits();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshReaderProviders();
                SetConfiguration(KeePassRFIDConfig.GetFromCurrentSession());
            }
            catch (COMException)
            {
                MessageBox.Show(Properties.Resources.RFIDInitError, Properties.Resources.PluginError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void cbReaderProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshReaderUnits();
        }

        private void RefreshReaderProviders()
        {
            cbReaderProvider.Items.Clear();

            LibraryManager libmgt = LibraryManager.getInstance();
            StringCollection providers = libmgt.getAvailableReaders();
            foreach (string provider in providers)
            {
                cbReaderProvider.Items.Add(provider);
            }
        }

        private void RefreshReaderUnits()
        {
            cbReaderUnit.Items.Clear();

            if (cbReaderProvider.SelectedIndex > -1)
            {
                string provider = cbReaderProvider.SelectedItem.ToString();
                LibraryManager libmgt = LibraryManager.getInstance();
                ReaderProvider readerProvider = libmgt.getReaderProvider(provider);
                if (readerProvider != null)
                {
                    var readers = readerProvider.getReaderList();
                    foreach (ReaderUnit reader in readers)
                    {
                        cbReaderUnit.Items.Add(reader.getName());
                    }
                }
            }
        }

        private void rbtnNFC_CheckedChanged(object sender, EventArgs e)
        {
            linkWriteNFC.Enabled = rbtnNFC.Checked;
        }

        private void linkWriteNFC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PasswordForm pwdForm = new PasswordForm();
            if (pwdForm.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    RFIDKeyProvider.ChipAction(new Action<Chip>(delegate (Chip chip)
                    {
                        // Only tag type 4 supported for now.
                        NFCTagCardService nfcsvc = chip.getService(CardServiceType.CST_NFC_TAG) as NFCTagCardService;
                        if (nfcsvc == null)
                            throw new KeePassRFIDException(Properties.Resources.UnsupportedNFCTag);
                        
                        NdefMessage msg = new NdefMessage();
                        msg.addTextRecord(pwdForm.Password, "en");

                        StorageCardService storage = chip.getService(CardServiceType.CST_STORAGE) as StorageCardService;
                        if (storage != null)
                        {
                            storage.erase(null, null);
                        }
                        nfcsvc.writeNDEF(msg);
                    }), GetConfiguration());

                    MessageBox.Show(Properties.Resources.NFCTagWritten, Properties.Resources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (KeePassRFIDException ex)
                {
                    MessageBox.Show(ex.Message, Properties.Resources.PluginError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public KeePassRFIDConfig GetConfiguration()
        {
            KeePassRFIDConfig config = new KeePassRFIDConfig();
            config.ReaderProvider = (cbReaderProvider.SelectedIndex > -1) ? cbReaderProvider.SelectedItem.ToString() : String.Empty;
            config.ReaderUnit = (cbReaderUnit.SelectedIndex > -1) ? cbReaderUnit.SelectedItem.ToString() : String.Empty;
            config.KeyType = rbtnNFC.Checked ? KeyType.NFC : KeyType.CSN;
            return config;
        }

        private void SetConfiguration(KeePassRFIDConfig config)
        {
            cbReaderProvider.SelectedItem = config.ReaderProvider;
            cbReaderUnit.SelectedItem = config.ReaderUnit;
            switch (config.KeyType)
            {
                case KeyType.NFC:
                    rbtnNFC.Checked = true;
                    break;
                default:
                    rbtnCSN.Checked = true;
                    break;
            }
        }

        private void lblSecureID_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:dev@islog.com?Subject=KeePass%20RFID%20-%20Secure%20ID");
        }
    }
}
