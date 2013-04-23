using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace athena.AR.Library.Common
{
    internal sealed class ExceptionManager
    {

        internal static void AppendAsync(Exception ex)
        {
            Task.Factory.StartNew(()=>
            {

            if(!System.IO.Directory.Exists(@"C:\Athena"))
                System.IO.Directory.CreateDirectory(@"C:\Athena");

            using (var file = new System.IO.StreamWriter(@"C:\Athena\Error.txt",true))
            {
                file.Write(file.NewLine);
                file.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                file.WriteLine(ex.ToString());
                file.Flush();

            }
            });
        }
    }
}