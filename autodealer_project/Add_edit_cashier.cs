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
    public partial class Add_edit_cashier : Form
    {
        SqlDataAdapter sda = new SqlDataAdapter();
        int id_cashier;
        string surname_cashier, name_cashier, patronymic_cashier;
        public Add_edit_cashier()
        {
            InitializeComponent();
        }

        public Add_edit_cashier(int id_cashier, string surname_cashier, string name_cashier, string patronymic_cashier)
        {
            InitializeComponent();
            this.id_cashier = id_cashier;
            this.surname_cashier = surname_cashier;
            this.name_cashier = name_cashier;
            this.patronymic_cashier = patronymic_cashier;
        }

        private void Add_edit_cashier_Load(object sender, EventArgs e)
        {
            if (id_cashier > 0)
            {
                textBox1.Text = surname_cashier;
                textBox2.Text = name_cashier;
                textBox3.Text = patronymic_cashier;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (id_cashier == 0 && textBox3.Text != "")
                {
                    sda.InsertCommand = new SqlCommand("Insert into cashier(surname,name,patronymic) values (@surname,@name,@patronymic)", sqlconnect.sql_con);
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@surname", SqlDbType.VarChar));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@patronymic", SqlDbType.VarChar));
                    sda.InsertCommand.Parameters[0].Value = textBox1.Text;
                    sda.InsertCommand.Parameters[1].Value = textBox2.Text;
                    sda.InsertCommand.Parameters[2].Value = textBox3.Text;
                    sda.InsertCommand.ExecuteNonQuery();
                }
                else if (id_cashier == 0 && textBox3.Text == "")
                {
                    sda.InsertCommand = new SqlCommand("Insert into cashier(surname,name) values (@surname,@name)", sqlconnect.sql_con);
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@surname", SqlDbType.VarChar));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
                    sda.InsertCommand.Parameters[0].Value = textBox1.Text;
                    sda.InsertCommand.Parameters[1].Value = textBox2.Text;
                    sda.InsertCommand.ExecuteNonQuery();
                }
                else
                {
                    sda.UpdateCommand = new SqlCommand("UPDATE cashier SET surname = @surname,name = @name,patronymic = @patronymic WHERE id_cashier = " + id_cashier, sqlconnect.sql_con);
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@surname", SqlDbType.VarChar));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@patronymic", SqlDbType.VarChar));
                    sda.UpdateCommand.Parameters[0].Value = textBox1.Text;
                    sda.UpdateCommand.Parameters[1].Value = textBox2.Text;
                    sda.UpdateCommand.Parameters[2].Value = textBox3.Text;
                    sda.UpdateCommand.ExecuteNonQuery();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка");
            }
        }
    }
}
