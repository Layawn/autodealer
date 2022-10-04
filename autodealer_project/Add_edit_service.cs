using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace autodealer_project
{
    public partial class Add_edit_service : Form
    {
        SqlDataAdapter sda = new SqlDataAdapter();
        int id_service, price_service;
        string name_service;
        public Add_edit_service()
        {
            InitializeComponent();
        }
        public Add_edit_service(int id_service, string name_service, int price_service)
        {
            InitializeComponent();
            this.id_service = id_service;
            this.name_service = name_service;
            this.price_service = price_service;
        }

        private void Add_edit_service_Load(object sender, EventArgs e)
        {
            if (id_service > 0)
            {
                textBox1.Text = name_service;
                textBox2.Text = price_service.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && int.TryParse(textBox2.Text, out price_service))
            {
                if (id_service == 0)
                {
                    sda.InsertCommand = new SqlCommand("Insert into [service](name_service,price_service) values (@name_service,@price_service)", sqlconnect.sql_con);
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@name_service", SqlDbType.VarChar));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@price_service", SqlDbType.Int));
                    sda.InsertCommand.Parameters[0].Value = textBox1.Text;
                    sda.InsertCommand.Parameters[1].Value = Convert.ToInt32(textBox2.Text);
                    sda.InsertCommand.ExecuteNonQuery();
                }
                else
                {
                    sda.UpdateCommand = new SqlCommand("UPDATE [service] SET name_service = @name_service,price_service = @price_service WHERE id_service = " + id_service, sqlconnect.sql_con);
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@name_service", SqlDbType.VarChar));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@price_service", SqlDbType.Int));
                    sda.UpdateCommand.Parameters[0].Value = textBox1.Text;
                    sda.UpdateCommand.Parameters[1].Value = Convert.ToInt32(textBox2.Text);
                    sda.UpdateCommand.ExecuteNonQuery();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены и/или заполнены не верно", "Ошибка");
            }
        }


    }
}
