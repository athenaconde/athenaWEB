using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace athena.AR.Security
{
    public sealed class EncryptionManager
    {

        internal static string EncryptId(int id)
        {

            return string.Format("fdfj43ej4ifg3kjidj4k4jkmgdkfdfe{0}35kdfh3idudfmdh3=dhQ3;", id);
           //return Encoding.ASCII.GetString( Cryptography.Encrypt(id.ToString(),
           //                "riCGJRKYcccccccc",
           //                "con45FDErrrrrrrr",
           //                5,
           //                "ARSOLONricky2012",
           //                256
           //     ));
        }

        internal static int DecryptStringId(string id)
        {
            try
            {

                return Convert.ToInt32( id.Replace("fdfj43ej4ifg3kjidj4k4jkmgdkfdfe", string.Empty).Replace("35kdfh3idudfmdh3=dhQ3;",string.Empty));
                //return Convert.ToInt32(Cryptography.Decrypt(Encoding.ASCII.GetBytes(id),
                //            "riCGJRKYcccccccc",
                //           "con45FDErrrrrrrr",
                //           5,
                //           "ARSOLONricky2012",
                //           256
                //));
            }
            catch (Exception) { return 0; }
        }
    }
}