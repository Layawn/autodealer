using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autodealer_project
{
    public partial class main_form : Form
    {
        public main_form()
        {
            InitializeComponent();
        }

        private void main_form_Load(object sender, EventArgs e)
        {
            bool info = sqlconnect.connect_open();
            if (!info)
            {
                MessageBox.Show("BD connect error");
            }
        }

        private void main_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            sqlconnect.sqlconnect_close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            auto_form cars = new auto_form();
            this.Hide();
            cars.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            service_form sf = new service_form();
            this.Hide();
            sf.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            products_form pf = new products_form();
            this.Hide();
            pf.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            auto_sale_form asf = new auto_sale_form();
            this.Hide();
            asf.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            service_sale_form ssf = new service_sale_form();
            this.Hide();
            ssf.ShowDialog();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            product_sale_form psf = new product_sale_form();
            this.Hide();
            psf.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cashier_form cf = new cashier_form();
            this.Hide();
            cf.ShowDialog();
            this.Show();
        }
    }
}
