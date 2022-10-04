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
    public partial class cashier_form : Form
    {
        int indexRow = 0;
        SqlDataAdapter sda;
        public cashier_form()
        {
            InitializeComponent();
        }

        private void reload()
        {
            sda = new SqlDataAdapter("SELECT id_cashier, surname Фамилия, name Имя, patronymic Отчество FROM cashier", sqlconnect.sql_con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                indexRow = e.RowIndex;
            }
        }

        private void cashier_form_Load(object sender, EventArgs e)
        {
            reload();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_edit_cashier aec = new Add_edit_cashier();
            aec.ShowDialog();
            reload();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_edit_cashier aec = new Add_edit_cashier((int)dataGridView1.Rows[indexRow].Cells[0].Value, dataGridView1.Rows[indexRow].Cells[1].Value.ToString(),
                dataGridView1.Rows[indexRow].Cells[2].Value.ToString(), dataGridView1.Rows[indexRow].Cells[3].Value.ToString());
            aec.ShowDialog();
            reload();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                sda.DeleteCommand = new SqlCommand("DELETE cashier WHERE id_cashier = @id_cashier", sqlconnect.sql_con);
                sda.DeleteCommand.Parameters.Add(new SqlParameter("@id_cashier", SqlDbType.Int));
                sda.DeleteCommand.Parameters[0].Value = (int)dataGridView1.Rows[indexRow].Cells[0].Value;
                sda.DeleteCommand.ExecuteNonQuery();
                reload();
            }
            catch
            {
                MessageBox.Show("Данные используются");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Add_edit_cashier aec = new Add_edit_cashier((int)dataGridView1.Rows[indexRow].Cells[0].Value, dataGridView1.Rows[indexRow].Cells[1].Value.ToString(),
                dataGridView1.Rows[indexRow].Cells[2].Value.ToString(), dataGridView1.Rows[indexRow].Cells[3].Value.ToString());
            aec.ShowDialog();
            reload();
        }
    }
}
