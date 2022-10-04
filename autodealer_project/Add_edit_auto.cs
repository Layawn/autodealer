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
    public partial class Add_edit_auto : Form
    {
        SqlDataAdapter sda;
        int id_auto, year_of_production, price;
        string engine_power;
        int engine_power_2;
        double engine_capacity;
        string brand, model, color, body_type, engine, gearbox, wheel_drive, picture_name = "", path_to_picture, startupPath = Application.StartupPath.ToString() + @"\pictures";
        Bitmap btm = new Bitmap(308, 143);
        TextBox textBox5 = new TextBox();
        TextBox textBox6 = new TextBox();
        TextBox textBox7 = new TextBox();
        public Add_edit_auto()
        {
            InitializeComponent();
        }

        public Add_edit_auto(int id_auto, string brand, string model, string color, string body_type, string engine, 
            double engine_capacity, string engine_power, string gearbox, string wheel_drive, int year_of_production, int price,
            string picture_name)
        {
            InitializeComponent();
            this.id_auto = id_auto;
            this.brand = brand;
            this.model = model;
            this.color = color;
            this.body_type = body_type;
            this.engine = engine;
            this.engine_capacity = engine_capacity;
            this.engine_power = engine_power;
            this.gearbox = gearbox;
            this.wheel_drive = wheel_drive;
            this.year_of_production = year_of_production;
            this.price = price;
            this.picture_name = picture_name;
        }

        private void Add_edit_auto_Load(object sender, EventArgs e)
        {
            button4.Visible = false;
            button6.Visible = false;
            button8.Visible = false;
            openFileDialog1.InitialDirectory = startupPath;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            sda = new SqlDataAdapter("SELECT id_brand, name_brand FROM brand", sqlconnect.sql_con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "name_brand";
            comboBox1.ValueMember = "id_brand";
            comboBox1.SelectedIndex = -1;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            sda = new SqlDataAdapter("SELECT id_color, name_color FROM color", sqlconnect.sql_con);
            DataTable dt2 = new DataTable();
            sda.Fill(dt2);
            comboBox3.DataSource = dt2;
            comboBox3.DisplayMember = "name_color";
            comboBox3.ValueMember = "id_color";
            comboBox3.SelectedIndex = -1;
            sda = new SqlDataAdapter("SELECT id_body_type, name_body_type FROM body_type", sqlconnect.sql_con);
            DataTable dt3 = new DataTable();
            sda.Fill(dt3);
            comboBox4.DataSource = dt3;
            comboBox4.DisplayMember = "name_body_type";
            comboBox4.ValueMember = "id_body_type";
            comboBox4.SelectedIndex = -1;
            sda = new SqlDataAdapter("SELECT id_engine, name_engine FROM engine", sqlconnect.sql_con);
            DataTable dt4 = new DataTable();
            sda.Fill(dt4);
            comboBox5.DataSource = dt4;
            comboBox5.DisplayMember = "name_engine";
            comboBox5.ValueMember = "id_engine";
            comboBox5.SelectedIndex = -1;
            sda = new SqlDataAdapter("SELECT id_gearbox, name_gearbox FROM gearbox", sqlconnect.sql_con);
            DataTable dt5 = new DataTable();
            sda.Fill(dt5);
            comboBox6.DataSource = dt5;
            comboBox6.DisplayMember = "name_gearbox";
            comboBox6.ValueMember = "id_gearbox";
            comboBox6.SelectedIndex = -1;
            sda = new SqlDataAdapter("SELECT id_wheel_drive, name_wheel_drive FROM wheel_drive", sqlconnect.sql_con);
            DataTable dt6 = new DataTable();
            sda.Fill(dt6);
            comboBox7.DataSource = dt6;
            comboBox7.DisplayMember = "name_wheel_drive";
            comboBox7.ValueMember = "id_wheel_drive";
            comboBox7.SelectedIndex = -1;
            if (id_auto > 0)
            {
                comboBox1.Text = brand;
                comboBox2.Text = model;
                comboBox3.Text = color;
                comboBox4.Text = body_type;
                comboBox5.Text = engine;
                textBox1.Text = engine_capacity.ToString();
                textBox2.Text = engine_power.ToString();
                comboBox6.Text = gearbox;
                comboBox7.Text = wheel_drive;
                textBox3.Text = year_of_production.ToString();
                textBox4.Text = price.ToString();
                try
                {
                    pictureBox1.Image = new Bitmap(startupPath + @"\" + picture_name);
                }
                catch
                {
                    pictureBox1.Hide();
                }
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((int)comboBox1.SelectedIndex > -1)
            {
                sda = new SqlDataAdapter("SELECT id_model, name_model FROM model WHERE brand = " + (int)comboBox1.SelectedValue, sqlconnect.sql_con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "name_model";
                comboBox2.ValueMember = "id_model";
                comboBox2.SelectedIndex = -1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1 && comboBox2.SelectedIndex > -1 && comboBox3.SelectedIndex > -1 &&
                comboBox4.SelectedIndex > -1 && comboBox5.SelectedIndex > -1 && comboBox6.SelectedIndex > -1 && 
                comboBox7.SelectedIndex > -1 && double.TryParse(textBox1.Text, out engine_capacity) && int.TryParse(textBox2.Text, out engine_power_2)
                && int.TryParse(textBox3.Text, out year_of_production) && int.TryParse(textBox4.Text, out price) && picture_name != "")
            {
                if (id_auto == 0)
                {
                    sda.InsertCommand = new SqlCommand("INSERT INTO [auto] (model, color, body_type, engine, engine_capacity, engine_power, gearbox, wheel_drive, year_of_production, price, picture_name) " +
                    "VALUES (@model, @color, @body_type, @engine, @engine_capacity, @engine_power, @gearbox, @wheel_drive, @year_of_production, @price, @picture_name)", sqlconnect.sql_con);
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@model", SqlDbType.Int));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@color", SqlDbType.Int));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@body_type", SqlDbType.Int));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@engine", SqlDbType.Int));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@engine_capacity", SqlDbType.Float));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@engine_power", SqlDbType.Int));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@gearbox", SqlDbType.Int));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@wheel_drive", SqlDbType.Int));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@year_of_production", SqlDbType.Int));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@price", SqlDbType.Int));
                    sda.InsertCommand.Parameters.Add(new SqlParameter("@picture_name", SqlDbType.VarChar));
                    sda.InsertCommand.Parameters["@model"].Value = comboBox2.SelectedValue;
                    sda.InsertCommand.Parameters["@color"].Value = comboBox3.SelectedValue;
                    sda.InsertCommand.Parameters["@body_type"].Value = comboBox4.SelectedValue;
                    sda.InsertCommand.Parameters["@engine"].Value = comboBox5.SelectedValue;
                    sda.InsertCommand.Parameters["@engine_capacity"].Value = Convert.ToDouble(textBox1.Text.Replace(".", ","));
                    sda.InsertCommand.Parameters["@engine_power"].Value = Convert.ToInt32(textBox2.Text);
                    sda.InsertCommand.Parameters["@gearbox"].Value = comboBox6.SelectedValue;
                    sda.InsertCommand.Parameters["@wheel_drive"].Value = comboBox7.SelectedValue;
                    sda.InsertCommand.Parameters["@year_of_production"].Value = Convert.ToInt32(textBox3.Text);
                    sda.InsertCommand.Parameters["@price"].Value = Convert.ToInt32(textBox4.Text);
                    sda.InsertCommand.Parameters["@picture_name"].Value = picture_name;
                    sda.InsertCommand.ExecuteNonQuery();
                }
                else
                {
                    sda.UpdateCommand = new SqlCommand("UPDATE [auto] SET model = @model,color = @color,body_type = @body_type, " + 
                    "engine = @engine, engine_capacity = @engine_capacity, engine_power = @engine_power, gearbox = @gearbox, " + 
                    "wheel_drive = @wheel_drive, year_of_production = @year_of_production, price = @price, picture_name = @picture_name WHERE id_auto = " + id_auto, sqlconnect.sql_con);
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@model", SqlDbType.Int));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@color", SqlDbType.Int));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@body_type", SqlDbType.Int));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@engine", SqlDbType.Int));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@engine_capacity", SqlDbType.Float));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@engine_power", SqlDbType.Int));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@gearbox", SqlDbType.Int));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@wheel_drive", SqlDbType.Int));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@year_of_production", SqlDbType.Int));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@price", SqlDbType.Int));
                    sda.UpdateCommand.Parameters.Add(new SqlParameter("@picture_name", SqlDbType.VarChar));
                    sda.UpdateCommand.Parameters["@model"].Value = comboBox2.SelectedValue;
                    sda.UpdateCommand.Parameters["@color"].Value = comboBox3.SelectedValue;
                    sda.UpdateCommand.Parameters["@body_type"].Value = comboBox4.SelectedValue;
                    sda.UpdateCommand.Parameters["@engine"].Value = comboBox5.SelectedValue;
                    sda.UpdateCommand.Parameters["@engine_capacity"].Value = Convert.ToDouble(textBox1.Text);
                    sda.UpdateCommand.Parameters["@engine_power"].Value = Convert.ToInt32(textBox2.Text);
                    sda.UpdateCommand.Parameters["@gearbox"].Value = comboBox6.SelectedValue;
                    sda.UpdateCommand.Parameters["@wheel_drive"].Value = comboBox7.SelectedValue;
                    sda.UpdateCommand.Parameters["@year_of_production"].Value = Convert.ToInt32(textBox3.Text);
                    sda.UpdateCommand.Parameters["@price"].Value = Convert.ToInt32(textBox4.Text);
                    sda.UpdateCommand.Parameters["@picture_name"].Value = picture_name;
                    sda.UpdateCommand.ExecuteNonQuery();
                }
                try
                {
                    pictureBox1.Image.Save(startupPath + @"\" + picture_name);
                }
                catch
                {
                    MessageBox.Show("Изображение не сохранено", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Не все поля введены или введены некорректно", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picture_name = openFileDialog1.SafeFileName;
                path_to_picture = openFileDialog1.FileName;
                pictureBox1.Show();
                pictureBox1.Image = new Bitmap(path_to_picture);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Size = new Size(121, 21);
            textBox5.Location = new Point(100, 79);
            textBox5.MaxLength = 30;
            this.Controls.Add(textBox5);
            textBox5.Visible = false;
            if (button3.Text == "+")
            {
                comboBox1.SelectedIndex = -1;
                comboBox1.Visible = false;
                textBox5.Visible = true;
                button4.Visible = true;
                button3.Text = "-";
            }
            else
            {
                textBox5.Text = "";
                comboBox1.Visible = true;
                textBox5.Visible = false;
                button4.Visible = false;
                button3.Text = "+";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox6.Size = new Size(121, 21);
            textBox6.Location = new Point(227, 79);
            textBox6.MaxLength = 50;
            this.Controls.Add(textBox6);
            if (button5.Text == "+")
            {
                comboBox2.SelectedIndex = -1;
                comboBox2.Visible = false;
                textBox6.Visible = true;
                button6.Visible = true;
                button5.Text = "-";
            }
            else
            {
                textBox6.Text = "";
                comboBox2.Visible = true;
                textBox6.Visible = false;
                button6.Visible = false;
                button5.Text = "+";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox7.Size = new Size(121, 21);
            textBox7.Location = new Point(356, 79);
            textBox7.MaxLength = 20;
            this.Controls.Add(textBox7);
            if (button7.Text == "+")
            {
                comboBox3.SelectedIndex = -1;
                comboBox3.Visible = false;
                textBox7.Visible = true;
                button8.Visible = true;
                button7.Text = "-";
            }
            else
            {
                textBox7.Text = "";
                comboBox3.Visible = true;
                textBox7.Visible = false;
                button8.Visible = false;
                button7.Text = "+";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                //добавление новой марки
                sda.InsertCommand = new SqlCommand("INSERT INTO brand (name_brand) VALUES (@name_brand)", sqlconnect.sql_con);
                sda.InsertCommand.Parameters.Add(new SqlParameter("@name_brand", SqlDbType.VarChar));
                sda.InsertCommand.Parameters["@name_brand"].Value = textBox5.Text;
                sda.InsertCommand.ExecuteNonQuery();
                //получение кода последнего добавления
                SqlDataAdapter sqlDAForBrand = new SqlDataAdapter("SELECT top 1 id_brand FROM brand ORDER BY id_brand DESC", sqlconnect.sql_con);
                DataTable dt = new DataTable();
                sqlDAForBrand.Fill(dt);
                int id_brand = (int)dt.Rows[0][0];
                //обнова
                sda = new SqlDataAdapter("SELECT id_brand, name_brand FROM brand", sqlconnect.sql_con);
                DataTable dt2 = new DataTable();
                sda.Fill(dt2);
                comboBox1.DataSource = dt2;
                comboBox1.DisplayMember = "name_brand";
                comboBox1.ValueMember = "id_brand";
                comboBox1.SelectedValue = id_brand;
                comboBox1.Visible = true;
                textBox5.Visible = false;
                button4.Visible = false;
                button3.Text = "+";
            }
            else
            {
                MessageBox.Show("Не введено обязательное поле", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "" && (int)comboBox1.SelectedIndex > -1)
            {
                //добавление новой модели
                sda.InsertCommand = new SqlCommand("INSERT INTO model (name_model,brand) VALUES (@name_model,@brand)", sqlconnect.sql_con);
                sda.InsertCommand.Parameters.Add(new SqlParameter("@name_model", SqlDbType.VarChar));
                sda.InsertCommand.Parameters.Add(new SqlParameter("@brand", SqlDbType.Int));
                sda.InsertCommand.Parameters["@name_model"].Value = textBox6.Text;
                sda.InsertCommand.Parameters["@brand"].Value = comboBox1.SelectedValue;
                sda.InsertCommand.ExecuteNonQuery();
                //получение кода последнего добавления
                SqlDataAdapter sqlDAForModel = new SqlDataAdapter("SELECT top 1 id_model FROM model ORDER BY id_model DESC", sqlconnect.sql_con);
                DataTable dt = new DataTable();
                sqlDAForModel.Fill(dt);
                int id_model = (int)dt.Rows[0][0];
                //обнова
                sda = new SqlDataAdapter("SELECT id_model, name_model FROM model WHERE brand = " + (int)comboBox1.SelectedValue, sqlconnect.sql_con);
                DataTable dt2 = new DataTable();
                sda.Fill(dt2);
                comboBox2.DataSource = dt2;
                comboBox2.DisplayMember = "name_model";
                comboBox2.ValueMember = "id_model";
                comboBox2.SelectedValue = id_model;
                comboBox2.Visible = true;
                textBox6.Visible = false;
                button6.Visible = false;
                button5.Text = "+";
            }
            else
            {
                MessageBox.Show("Не введено обязательное поле и/или не выбрана марка", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                //добавление нового цвета
                sda.InsertCommand = new SqlCommand("INSERT INTO color (name_color) VALUES (@name_color)", sqlconnect.sql_con);
                sda.InsertCommand.Parameters.Add(new SqlParameter("@name_color", SqlDbType.VarChar));
                sda.InsertCommand.Parameters["@name_color"].Value = textBox7.Text;
                sda.InsertCommand.ExecuteNonQuery();
                //получение кода последнего добавления
                SqlDataAdapter sqlDAForColor = new SqlDataAdapter("SELECT top 1 id_color FROM color ORDER BY id_color DESC", sqlconnect.sql_con);
                DataTable dt = new DataTable();
                sqlDAForColor.Fill(dt);
                int id_color = (int)dt.Rows[0][0];
                //обнова
                sda = new SqlDataAdapter("SELECT id_color, name_color FROM color", sqlconnect.sql_con);
                DataTable dt2 = new DataTable();
                sda.Fill(dt2);
                comboBox3.DataSource = dt2;
                comboBox3.DisplayMember = "name_color";
                comboBox3.ValueMember = "id_color";
                comboBox3.SelectedValue = id_color;
                comboBox3.Visible = true;
                textBox6.Visible = false;
                button8.Visible = false;
                button7.Text = "+";
            }
            else
            {
                MessageBox.Show("Не введено обязательное поле", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
