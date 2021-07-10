using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaAmericana
{
    class Conexion
    {
        public static SqlConnection getConnection()
        {
            SqlConnection conn = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=F_Americana;Integrated Security=True");
            conn.Open();
            return conn;
        }
    }
}
