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
    public partial class Add_edit_position_product : Form
    {
        SqlDataAdapter sda;
        DataTable dt = new DataTable();
        int id_product, quantity_product;
        string name_product;
        public Add_edit_position_product()
        {
            InitializeComponent();
        }

        public Add_edit_position_product(int id_product, string name_product, int quantity_product)
        {
            InitializeComponent();
            this.id_product = id_product;
            this.name_product = name_product;
            this.quantity_product = quantity_product;
        }

        private void Add_edit_position_product_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            sda = new SqlDataAdapter("SELECT id_product, name_product, price_product FROM product", sqlconnect.sql_con);
            sda.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "name_product";
            comboBox1.ValueMember = "id_product";
            if (id_product > 0)
            {
                comboBox1.Text = name_product;
                numericUpDown1.Value = quantity_product;
            }
            else
            {
                comboBox1.SelectedIndex = -1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1 && numericUpDown1.Value > 0)
            {
                Cart.addToCart((int)comboBox1.SelectedValue, comboBox1.Text, (int)numericUpDown1.Value, (int)dt.Rows[(int)comboBox1.SelectedIndex][2]);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Не выбран товар и/или количество товара указано не верно", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
