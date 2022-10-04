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
    public partial class auto_form : Form
    {
        SqlDataAdapter sda;
        int id_auto;
        int indexRow = 0;
        public auto_form()
        {
            InitializeComponent();
        }

        private void reload()
        {
            sda = new SqlDataAdapter("SELECT id_auto, name_brand Марка, model.name_model Модель, name_color Цвет, name_body_type Тип_кузова, name_engine Двигатель, engine_capacity Объем_двигателя, " +
            "engine_power Мощность, name_gearbox Коробка, name_wheel_drive Привод, year_of_production Год_выпуска, price Стоимость, picture_name FROM [auto] " +
                                    "INNER JOIN model ON [auto].model = model.id_model " +
                                    "INNER JOIN brand ON model.brand = brand.id_brand " +
                                    "INNER JOIN color ON [auto].color = color.id_color " +
                                    "INNER JOIN body_type ON [auto].body_type = body_type.id_body_type " +
                                    "INNER JOIN engine ON [auto].engine = engine.id_engine " +
                                    "INNER JOIN gearbox ON [auto].gearbox = gearbox.id_gearbox " +
                                    "INNER JOIN wheel_drive ON [auto].wheel_drive = wheel_drive.id_wheel_drive", sqlconnect.sql_con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[12].Visible = false;
        }

        private void auto_form_Load(object sender, EventArgs e)
        {
            reload();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_edit_auto aea = new Add_edit_auto();
            aea.ShowDialog();
            reload();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_edit_auto aea = new Add_edit_auto((int)dataGridView1.Rows[indexRow].Cells[0].Value, dataGridView1.Rows[indexRow].Cells[1].Value.ToString(), 
                dataGridView1.Rows[indexRow].Cells[2].Value.ToString(), dataGridView1.Rows[indexRow].Cells[3].Value.ToString(), dataGridView1.Rows[indexRow].Cells[4].Value.ToString(),
                dataGridView1.Rows[indexRow].Cells[5].Value.ToString(), (double)dataGridView1.Rows[indexRow].Cells[6].Value, dataGridView1.Rows[indexRow].Cells[7].Value.ToString(), 
                dataGridView1.Rows[indexRow].Cells[8].Value.ToString(), dataGridView1.Rows[indexRow].Cells[9].Value.ToString(), (int)dataGridView1.Rows[indexRow].Cells[10].Value,
                (int)dataGridView1.Rows[indexRow].Cells[11].Value, dataGridView1.Rows[indexRow].Cells[12].Value.ToString());
            aea.ShowDialog();
            reload();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                indexRow = e.RowIndex;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Add_edit_auto aea = new Add_edit_auto((int)dataGridView1.Rows[indexRow].Cells[0].Value, dataGridView1.Rows[indexRow].Cells[1].Value.ToString(),
                dataGridView1.Rows[indexRow].Cells[2].Value.ToString(), dataGridView1.Rows[indexRow].Cells[3].Value.ToString(), dataGridView1.Rows[indexRow].Cells[4].Value.ToString(),
                dataGridView1.Rows[indexRow].Cells[5].Value.ToString(), (double)dataGridView1.Rows[indexRow].Cells[6].Value, dataGridView1.Rows[indexRow].Cells[7].Value.ToString(),
                dataGridView1.Rows[indexRow].Cells[8].Value.ToString(), dataGridView1.Rows[indexRow].Cells[9].Value.ToString(), (int)dataGridView1.Rows[indexRow].Cells[10].Value,
                (int)dataGridView1.Rows[indexRow].Cells[11].Value, dataGridView1.Rows[indexRow].Cells[12].Value.ToString());
            aea.ShowDialog();
            reload();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                sda.DeleteCommand = new SqlCommand("DELETE [auto] WHERE id_auto = @id_auto", sqlconnect.sql_con);
                sda.DeleteCommand.Parameters.Add(new SqlParameter("@id_auto", SqlDbType.Int));
                sda.DeleteCommand.Parameters[0].Value = (int)dataGridView1.Rows[indexRow].Cells[0].Value;
                sda.DeleteCommand.ExecuteNonQuery();
                reload();
            }
            catch
            {
                MessageBox.Show("Данные используются");
            }
        }
    }
}
