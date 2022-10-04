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
    public partial class Add_edit_product : Form
    {
        SqlDataAdapter sda = new SqlDataAdapter();
        int id_product, price_product;
        string name_product;
        public Add_edit_product()
        {
            InitializeComponent();
        }

        public Add_edit_product(int id_product, string name_product, int price_product)
        {
            InitializeComponent();
            this.id_product = id_product;
            this.name_product = name_product;
            this.price_product = price_product;
        }

        private void Add_edit_product_Load(object sender, EventArgs e)
        {
            if (id_product > 0)
            {
                textBox1.Text = name_product;
                textBox2.Text = price_product.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && int.TryParse(textBox2.Text, out price_product))
            {
                if (id_product == 0)
                {
                    sda.InsertCommand = new SqlCommand("Insert into product(name_product,price_product) values (@name_product,@price_product)", sqlconnect.sql_con);
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@name_product", SqlDbType.VarChar));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@price_product", SqlDbType.Int));
                    sda.InsertCommand.Parameters[0].Value = textBox1.Text;
                    sda.InsertCommand.Parameters[1].Value = int.Parse(textBox2.Text);
                    sda.InsertCommand.ExecuteNonQuery();
                }
                else
                {
                    sda.UpdateCommand = new SqlCommand("UPDATE product SET name_product = @name_product,price_product = @price_product WHERE id_product = " + id_product, sqlconnect.sql_con);
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@name_product", SqlDbType.Int));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@price_product", SqlDbType.VarChar));
                    sda.UpdateCommand.Parameters[0].Value = textBox1.Text;
                    sda.UpdateCommand.Parameters[1].Value = int.Parse(textBox2.Text);
                    sda.UpdateCommand.ExecuteNonQuery();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Введите данные полностью");
            }
        }
    }
}
