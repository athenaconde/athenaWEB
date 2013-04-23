using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.EntityClient;
using System.Text;

namespace athena.AR.Data
{
    public sealed class ConnectionHelper
    {
        internal static EntityConnection GetConnection()
        {
            var connstring = new System.Data.SqlClient.SqlConnectionStringBuilder();
           // connstring.ApplicationName = "Athena AR";
            connstring.PersistSecurityInfo = true;
            connstring.UserID = "db2admin";
            connstring.Password = "cool_2012";
            connstring["Server"] = "localhost";
            connstring["Database"] = "Athena";
           
            var connStringBuilder = new EntityConnectionStringBuilder();
            connStringBuilder.Provider = "IBM.Data.DB2";
            connStringBuilder.ProviderConnectionString = connstring.ConnectionString.Replace("Data Source=","Server=").Replace("Initial Catalog=","Database=") ;
            connStringBuilder.Metadata = @"res://*/Data.ARModel.csdl|res://*/Data.ARModel.ssdl|res://*/Data.ARModel.msl";
            

            return new EntityConnection(connStringBuilder.ToString());
          
        }
    }
}