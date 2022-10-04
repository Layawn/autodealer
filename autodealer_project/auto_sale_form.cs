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
    public partial class auto_sale_form : Form
    {
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        int id_auto, id_trade;
        public auto_sale_form()
        {
            InitializeComponent();
        }

        private void auto_sale_form_Load(object sender, EventArgs e)
        {
            sda = new SqlDataAdapter("SELECT [auto].id_auto, name_brand Марка, model.name_model Модель, name_color Цвет, name_body_type Тип_кузова, name_engine Двигатель, engine_capacity Объем_двигателя, " +
            "engine_power Мощность, name_gearbox Коробка, name_wheel_drive Привод, year_of_production Год_выпуска, price Стоимость, picture_name FROM [auto] " +
                                    "INNER JOIN model ON [auto].model = model.id_model " +
                                    "INNER JOIN brand ON model.brand = brand.id_brand " +
                                    "INNER JOIN color ON [auto].color = color.id_color " +
                                    "INNER JOIN body_type ON [auto].body_type = body_type.id_body_type " +
                                    "INNER JOIN engine ON [auto].engine = engine.id_engine " +
                                    "INNER JOIN gearbox ON [auto].gearbox = gearbox.id_gearbox " +
                                    "INNER JOIN wheel_drive ON [auto].wheel_drive = wheel_drive.id_wheel_drive " + 
                                    "LEFT JOIN trade_auto ON [auto].id_auto = trade_auto.id_auto " + 
                                    "WHERE id_trade_auto IS NULL", sqlconnect.sql_con);
            sda.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Нет доступных автомобилей", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                int pozY_label1 = 28;
                int pozY_label2 = 56;
                int pozY_picture = 28;
                int pozY_label3 = 28;
                int pozY_button1 = 56;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Марка, модель, год выпуска
                    Label tempLabel1 = new Label();
                    tempLabel1.Name = "label" + (i + 1).ToString();
                    tempLabel1.AutoSize = false;
                    tempLabel1.Size = new System.Drawing.Size(200, 20);
                    tempLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    tempLabel1.Text = dt.Rows[i][1].ToString() + " " + dt.Rows[i][2].ToString() + " " + dt.Rows[i][10].ToString();
                    tempLabel1.Location = new Point(178, pozY_label1);
                    //Объем двигателя, цвет и пр.
                    Label tempLabel2 = new Label();
                    tempLabel2.Name = "label" + (i + 2).ToString();
                    tempLabel2.AutoSize = false;
                    tempLabel2.Size = new System.Drawing.Size(200, 40);
                    tempLabel2.Text = dt.Rows[i][3].ToString() + " / " + dt.Rows[i][4].ToString() + " / " + dt.Rows[i][5].ToString() +
                         " / " + dt.Rows[i][6].ToString() + " / " + dt.Rows[i][7].ToString() + " / " + dt.Rows[i][8].ToString() +
                         " / " + dt.Rows[i][9].ToString();
                    tempLabel2.Location = new Point(178, pozY_label2);
                    //Цена
                    Label tempLabel3 = new Label();
                    tempLabel3.Name = "label" + (i + 3).ToString();
                    tempLabel3.Text = dt.Rows[i][11].ToString() + " руб.";
                    tempLabel3.Location = new Point(550, pozY_label3);
                    //button купить
                    Button tempButton1 = new Button();
                    tempButton1.Name = "button" + (i + 1).ToString();
                    tempButton1.Text = "Купить";
                    tempButton1.Tag = dt.Rows[i][0];
                    tempButton1.Location = new Point(550, pozY_button1);
                    //Картинка
                    string path_to_picture = Application.StartupPath.ToString() + @"\pictures\" + dt.Rows[i][12].ToString();
                    PictureBox tempPictureBox1 = new PictureBox();
                    tempPictureBox1.Name = "pictureBox" + (i + 1).ToString();
                    tempPictureBox1.Location = new Point(35, pozY_picture);
                    tempPictureBox1.Size = new System.Drawing.Size(126, 89);
                    try
                    {
                        tempPictureBox1.Image = new Bitmap(path_to_picture);
                    }
                    catch
                    {
                        tempPictureBox1.Image = new Bitmap(Application.StartupPath.ToString() + @"\pictures\error_picture.PNG");
                    }
                    tempPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                    pozY_label1 = pozY_label1 + 117;
                    pozY_label2 = pozY_label2 + 117;
                    pozY_label3 = pozY_label3 + 117;
                    pozY_picture = pozY_picture + 117;
                    pozY_button1 = pozY_button1 + 117;
                    this.Controls.Add(tempLabel1);
                    this.Controls.Add(tempLabel2);
                    this.Controls.Add(tempLabel3);
                    this.Controls.Add(tempButton1);
                    this.Controls.Add(tempPictureBox1);
                }
                //привязка события для каждой кнопки
                foreach (Control tempControl in this.Controls)
                {
                    if (tempControl is Button)
                    {
                        ((Button)tempControl).Click += button_Click;
                    }
                }
            }
            
        }

        private void button_Click(object sender, EventArgs e)
        {
            id_auto = (int)((sender as Button).Tag);
            //вставка в таблицу trade
            sda.InsertCommand = new SqlCommand("INSERT INTO trade (trade_date, id_cashier) VALUES (@trade_date,@id_cashier)", sqlconnect.sql_con);
            sda.InsertCommand.Parameters.Add(new SqlParameter("@trade_date", SqlDbType.DateTime));
            sda.InsertCommand.Parameters.Add(new SqlParameter("@id_cashier", SqlDbType.Int));
            sda.InsertCommand.Parameters[0].Value = DateTime.Now;
            sda.InsertCommand.Parameters[1].Value = 2;
            sda.InsertCommand.ExecuteNonQuery();
            //получение кода продажи
            sda = new SqlDataAdapter("SELECT top 1 id_trade FROM trade ORDER BY id_trade DESC", sqlconnect.sql_con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            id_trade = (int)dt.Rows[0][0];
            //вставка в таблицу trade_auto
            sda.InsertCommand = new SqlCommand("INSERT INTO trade_auto (id_trade, id_auto) VALUES (@id_trade,@id_auto)", sqlconnect.sql_con);
            sda.InsertCommand.Parameters.Add(new SqlParameter("@id_trade", SqlDbType.Int));
            sda.InsertCommand.Parameters.Add(new SqlParameter("@id_auto", SqlDbType.Int));
            sda.InsertCommand.Parameters[0].Value = id_trade;
            sda.InsertCommand.Parameters[1].Value = id_auto;
            sda.InsertCommand.ExecuteNonQuery();
            this.Close();
        }
    }
}
