using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeePassRFID
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (String.IsNullOrEmpty(tbxPassword.Text))
                errorProvider.SetError(tbxPassword, Properties.Resources.EmptyPasswordError);
            else
            {
                errorProvider.SetError(tbxPassword, String.Empty);

                if (tbxPassword.Text != tbxVerif.Text)
                    errorProvider.SetError(tbxVerif, Properties.Resources.PasswordMismatchError);
                else
                {
                    errorProvider.SetError(tbxVerif, String.Empty);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        public string Password
        {
            get { return tbxPassword.Text; }
        }
    }
}
