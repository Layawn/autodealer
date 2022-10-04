using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace autodealer_project
{
    class sqlconnect
    {
        //public static SqlConnection sql_con = new SqlConnection(@"Data Source=DESKTOP-56R4KBL\SQLEXPRESS;Initial Catalog=autodealer;User ID=sa;Password=12345678");
        public static SqlConnection sql_con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\autodealer.mdf;Integrated Security = True");
        public static bool connect_open()
        {
            bool temp = false;
            try
            {
                sql_con.Open();
                temp = true;
            }
            catch
            {
                temp = false;
            }
            return temp;
        }
        public static void sqlconnect_close()
        {
            try
            {
                sql_con.Close();
            }
            catch { }
        }
    }
}
