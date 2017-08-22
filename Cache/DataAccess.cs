using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace InteractingWithDatabases
{
    public class DataAccess
    {
        public string ConnectionString { get; set; }
        SqlConnection m_sqlConn = null;


        public DataAccess()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
        }

        public bool Connect()
        {
            m_sqlConn = new SqlConnection(ConnectionString);
            m_sqlConn.Open();

            return m_sqlConn.State == System.Data.ConnectionState.Open;
        }

        public bool Disconnect()
        {
            m_sqlConn = new SqlConnection(ConnectionString);
            m_sqlConn.Close();

            return m_sqlConn.State == System.Data.ConnectionState.Closed;
        }

        
        }
    }

