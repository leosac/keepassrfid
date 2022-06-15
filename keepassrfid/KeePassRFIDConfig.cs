using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace KeePassRFID
{
    public enum KeyType
    {
        CSN,
        NFC,
        SecureID,
        OTP
    }

    [Serializable]
    public class KeePassRFIDConfig
    {
        public KeePassRFIDConfig()
        {
            ReaderProvider = "PCSC";
            ReaderUnit = String.Empty;
            CardType = String.Empty;
            KeyType = KeyType.CSN;
            Challenge = null;
        }

        public string ReaderProvider { get; set; }

        public string ReaderUnit { get; set; }

        public string CardType { get; set; }

        public KeyType KeyType { get; set; }

        public byte[] Challenge { get; set; }

        private static string GetCurrentSessionConfigPath(bool createDir = false)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Leosac");
            if (createDir && !Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, "KeePassRFID");
            if (createDir && !Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Combine(path, "configuration.xml");
        }

        public static KeePassRFIDConfig GetFromCurrentSession()
        {
            KeePassRFIDConfig config = new KeePassRFIDConfig();
            string xmlfile = GetCurrentSessionConfigPath();
            if (File.Exists(xmlfile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(KeePassRFIDConfig));
                using (TextReader reader = new StringReader(File.ReadAllText(xmlfile)))
                {
                    config = serializer.Deserialize(reader) as KeePassRFIDConfig;
                }
            }
            return config;
        }

        public static void SaveToCurrentSession(KeePassRFIDConfig config)
        {
            XmlSerializer xser = new XmlSerializer(typeof(KeePassRFIDConfig));
            using (StringWriter sww = new StringWriter())
            using (XmlTextWriter writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented })
            {
                xser.Serialize(writer, config);
                string xml = sww.ToString();
                File.WriteAllText(GetCurrentSessionConfigPath(true), xml);
            }
        }
    }
}
