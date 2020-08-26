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
            Connection = new SqlConnection("");
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
