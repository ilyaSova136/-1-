using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PromezhutochnyKontrol
{
    public partial class Form2 : Form
    {
        Product product;
        DataBaseHelper helper;

        //List<Material> types 

        public Form2(Product product)
        {
            InitializeComponent();
            helper = new DataBaseHelper();
            this.product = product ?? new Product();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox2.Text = product.Title;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            helper.AddOrUpdateProduct(new Product
            {
                Title = textBox2.Text,
                Article = textBox1.Text,
                Description = textBox6.Text,
                WorkshopNumber = int.Parse(textBox4.Text),
                PersonCount = int.Parse(textBox3.Text),
                MinCost = decimal.Parse(textBox5.Text),
                Image = textBox7.Text,
                ProductTypeId = int.Parse(comboBox1.Text)
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(openFileDialog.FileName))
            {
                return;
            }
            else
            {
                textBox7.Text = openFileDialog.FileName;
                pictureBox1.Image = Bitmap.FromFile(openFileDialog.FileName);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
