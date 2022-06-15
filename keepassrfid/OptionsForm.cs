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
                RefreshCardTypes();
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

        private void RefreshCardTypes()
        {
            cbCardType.Items.Clear();

            LibraryManager libmgt = LibraryManager.getInstance();
            StringCollection cardtypes = libmgt.getAvailableCards();
            foreach (string cardtype in cardtypes)
            {
                cbCardType.Items.Add(cardtype);
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
                        NFCTagCardService nfcsvc = chip.getService(CardServiceType.CST_NFC_TAG) as NFCTagCardService;
                        if (nfcsvc == null)
                            throw new KeePassRFIDException(Properties.Resources.UnsupportedNFCTag);
                        
                        NdefMessage msg = new NdefMessage();
                        msg.addTextRecord(pwdForm.Password, "en");

                        // Special case until new LLA SWIG version get released
                        if (nfcsvc is DESFireEV1NFCTag4CardService)
                        {
                            var storagesvc = chip.getService(CardServiceType.CST_STORAGE) as DESFireStorageCardService;
                            if (storagesvc == null)
                                throw new KeePassRFIDException(Properties.Resources.UnsupportedNFCTag);
                            storagesvc.erase();
                        }
                        else
                        {
                            nfcsvc.eraseNDEF();
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
            config.CardType = (cbCardType.SelectedIndex > -1) ? cbCardType.SelectedItem.ToString() : String.Empty;
            if (rbtnNFC.Checked)
            {
                config.KeyType = KeyType.NFC;
            }
            else if (rbtnSecureID.Checked)
            {
                config.KeyType = KeyType.SecureID;
            }
            else if (rbtnOTP.Checked)
            {
                config.KeyType = KeyType.OTP;
            }
            else
            {
                config.KeyType = KeyType.CSN;
            }
            if (rbtnChallengeBlank.Checked || String.IsNullOrEmpty(tbxChallenge.Text))
            {
                config.Challenge = null;
            }
            else
            {
                byte[] challenge = new byte[tbxChallenge.Text.Length / 2];
                for (int i = 0; i < tbxChallenge.Text.Length; i += 2)
                    challenge[i / 2] = Convert.ToByte(tbxChallenge.Text.Substring(i, 2), 16);
                config.Challenge = challenge;
            }
            return config;
        }

        private void SetConfiguration(KeePassRFIDConfig config)
        {
            cbReaderProvider.SelectedItem = config.ReaderProvider;
            cbReaderUnit.SelectedItem = config.ReaderUnit;
            cbCardType.SelectedItem = config.CardType;
            switch (config.KeyType)
            {
                case KeyType.NFC:
                    rbtnNFC.Checked = true;
                    break;
                case KeyType.SecureID:
                    rbtnSecureID.Checked = true;
                    break;
                case KeyType.OTP:
                    rbtnOTP.Checked = true;
                    break;
                default:
                    rbtnCSN.Checked = true;
                    break;
            }
            rbtnChallengeBlank.Checked = (config.Challenge == null);
            tbxChallenge.Text = config.Challenge == null ? String.Empty : BitConverter.ToString(config.Challenge).Replace("-", String.Empty);
        }

        private void lblSecureID_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/leosac/keepassrfid/issues/5");
        }

        private void rbtnOTP_CheckedChanged(object sender, EventArgs e)
        {
            gpChallengeStrategy.Enabled = rbtnOTP.Checked;
        }

        private void linkQueryChallenge_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RFIDKeyProvider.ChipAction(new Action<Chip>(delegate (Chip chip)
            {
                var otpsvc = chip.getService(CardServiceType.CST_CHALLENGE_RESPONSE) as ChallengeCardService;
                if (otpsvc == null)
                    throw new KeePassRFIDException(Properties.Resources.OTPUnsupported);

                var challenge = otpsvc.getChallenge();
                if (challenge != null && challenge.Count > 0)
                {
                    tbxChallenge.Text = BitConverter.ToString(challenge.ToArray()).Replace("-", String.Empty);
                }
            }), GetConfiguration());
        }

        private void rbtnChallengeFixed_CheckedChanged(object sender, EventArgs e)
        {
            linkQueryChallenge.Enabled = tbxChallenge.Enabled = rbtnChallengeFixed.Checked;
        }
    }
}
