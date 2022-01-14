using KeePass.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeePassRFID
{
    public sealed class KeePassRFIDExt : Plugin
    {
        private IPluginHost host = null;
        private RFIDKeyProvider keyProv = new RFIDKeyProvider();
        ToolStripMenuItem optionsMenu;

        public override bool Initialize(IPluginHost host)
        {
            this.host = host;

            // Add menu items
            this.optionsMenu = new ToolStripMenuItem(Properties.Resources.KeePassRFIDOptions);
            this.optionsMenu.Click += OnOptions_Click;
            this.optionsMenu.Image = Properties.Resources.liblogicalaccess_logo_x32;
            this.host.MainWindow.ToolsMenu.DropDownItems.Add(this.optionsMenu);

            // Register key provider
            this.host.KeyProviderPool.Add(keyProv);

            return true;
        }

        public override void Terminate()
        {
            // Remove menu items
            this.optionsMenu.Click -= OnOptions_Click;
            this.host.MainWindow.ToolsMenu.DropDownItems.Remove(this.optionsMenu);

            // Unregister key provider
            this.host.KeyProviderPool.Remove(keyProv);
        }

        public override string UpdateUrl
        {
            get
            {
                return "https://raw.githubusercontent.com/islog/keepassrfid/master/latest-version.txt";
            }
        }

        public override Image SmallIcon
        {
            get
            {
                return Properties.Resources.liblogicalaccess_logo_x32;
            }
        }

        private void OnOptions_Click(object sender, EventArgs e)
        {
            OptionsForm options = new OptionsForm();
            if (options.ShowDialog() == DialogResult.OK)
            {
                KeePassRFIDConfig config = options.GetConfiguration();
                KeePassRFIDConfig.SaveToCurrentSession(config);
            }
        }
    }
}
