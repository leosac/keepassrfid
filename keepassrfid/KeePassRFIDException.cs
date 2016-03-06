using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeePassRFID
{
    public class KeePassRFIDException : Exception
    {
        public KeePassRFIDException()
            : base()
        {

        }

        public KeePassRFIDException(string message)
            : base (message)
        {

        }

        public KeePassRFIDException(string message, Exception innerException)
            : base (message, innerException)
        {

        }
    }
}
