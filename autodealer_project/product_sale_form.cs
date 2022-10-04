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
    public partial class product_sale_form : Form
    {
        int indexRow = 0, id_trade;
        SqlDataAdapter sda = new SqlDataAdapter();
        public product_sale_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_edit_position_product aepp = new Add_edit_position_product();
            aepp.ShowDialog();
            if (aepp.DialogResult == DialogResult.OK)
            {
                dataGridView1.Rows.Add(Cart.getId(), Cart.getName(), Cart.getQuantity(), Cart.getPrice());
            }
        }

        private void product_sale_form_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            dataGridView1.Columns[0].HeaderText = "id_product";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Товар";
            dataGridView1.Columns[2].HeaderText = "Количество";
            dataGridView1.Columns[3].HeaderText = "Цена за 1 ед.";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                indexRow = e.RowIndex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Add_edit_position_product aepp = new Add_edit_position_product((int)dataGridView1.Rows[indexRow].Cells[0].Value, dataGridView1.Rows[indexRow].Cells[1].Value.ToString(),
                (int)dataGridView1.Rows[indexRow].Cells[2].Value);
                aepp.ShowDialog();
                if (aepp.DialogResult == DialogResult.OK)
                {
                    dataGridView1.Rows[indexRow].SetValues(Cart.getId(), Cart.getName(), Cart.getQuantity(), Cart.getPrice());
                }
            }
            catch
            {
                MessageBox.Show("Не выбрана позиция", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.RemoveAt(indexRow);
            }
            catch
            {
                MessageBox.Show("Не выбрана позиция", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                //вставка в таблицу trade
                sda.InsertCommand = new SqlCommand("INSERT INTO trade (trade_date, id_cashier) VALUES (@trade_date,@id_cashier)", sqlconnect.sql_con);
                sda.InsertCommand.Parameters.Add(new SqlParameter("@trade_date", SqlDbType.DateTime));
                sda.InsertCommand.Parameters.Add(new SqlParameter("@id_cashier", SqlDbType.Int));
                sda.InsertCommand.Parameters[0].Value = DateTime.Now;
                sda.InsertCommand.Parameters[1].Value = 1;
                sda.InsertCommand.ExecuteNonQuery();
                //получение айди продажи
                sda = new SqlDataAdapter("SELECT top 1 id_trade FROM trade ORDER BY id_trade DESC", sqlconnect.sql_con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                id_trade = (int)dt.Rows[0][0];
                //вставка в таблицу trade_product
                //цикл для каждой строки
                SqlDataAdapter sda2 = new SqlDataAdapter();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    sda2.InsertCommand = new SqlCommand("INSERT INTO trade_product (id_trade, id_product, quantity) VALUES (@id_trade, @id_product, @quantity)", sqlconnect.sql_con);
                    sda2.InsertCommand.Parameters.Add(new SqlParameter("@id_trade", SqlDbType.Int));
                    sda2.InsertCommand.Parameters.Add(new SqlParameter("@id_product", SqlDbType.Int));
                    sda2.InsertCommand.Parameters.Add(new SqlParameter("@quantity", SqlDbType.Int));
                    sda2.InsertCommand.Parameters["@id_trade"].Value = id_trade;
                    sda2.InsertCommand.Parameters["@id_product"].Value = (int)dataGridView1.Rows[i].Cells[0].Value;
                    sda2.InsertCommand.Parameters["@quantity"].Value = (int)dataGridView1.Rows[i].Cells[2].Value;
                    sda2.InsertCommand.ExecuteNonQuery();
                }

                MessageBox.Show("Покупка успешно совершена");
            }
            else
            {
                MessageBox.Show("Не выбраны позиции", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
