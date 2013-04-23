using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace athena.AR.Common
{
    internal static class ExceptionLogger
    {

        public static void AppendAsync(Exception ex)
        {
            Task.Factory.StartNew(()=>{
            using (var file = new System.IO.StreamWriter(@"C:\Athena\Error.txt",true))
            {
                file.Write(file.NewLine);
                file.WriteLine(DateTime.Now.ToString());
                file.WriteLine(ex.ToString());
                file.Flush();

            }
            });
        }
    }
}