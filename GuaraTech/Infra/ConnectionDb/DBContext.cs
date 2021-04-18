using System;
using System.Data;
using System.Data.SqlClient;


namespace GuaraTech.Infra
{
    public class DBContext : IDisposable
    {
        public SqlConnection Connection { get; set; }


        public DBContext()
        {
            Connection = new SqlConnection("Server = den1.mssql8.gear.host; Database = guaratechdb; User ID = guaratechdb; Password = De32DsV - F4!O; Encrypt = True; TrustServerCertificate = True");
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
