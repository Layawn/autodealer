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
    public partial class Add_edit_position_service : Form
    {
        SqlDataAdapter sda;
        DataTable dt = new DataTable();
        int id_service, quantity_service;
        string name_service;
        public Add_edit_position_service()
        {
            InitializeComponent();
        }

        public Add_edit_position_service(int id_service, string name_service, int quantity_service)
        {
            InitializeComponent();
            this.id_service = id_service;
            this.name_service = name_service;
            this.quantity_service = quantity_service;
        }

        private void Add_edit_position_service_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            sda = new SqlDataAdapter("SELECT id_service, name_service, price_service FROM [service]", sqlconnect.sql_con);
            sda.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "name_service";
            comboBox1.ValueMember = "id_service";
            if (id_service > 0)
            {
                comboBox1.Text = name_service;
                numericUpDown1.Value = quantity_service;
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
