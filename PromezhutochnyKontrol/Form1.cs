using Npgsql;
using System.Data;

namespace PromezhutochnyKontrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Product> products;
        DataBaseHelper helper = new DataBaseHelper();
        private void Form1_Load(object sender, EventArgs e)
        {

            UpdateItems();
        }

        public void UpdateItems()
        {
            products = helper.GetProducts();

            flowLayoutPanel1.Controls.Clear();
            foreach (Product product in products)
            {
                CustomItem item = new CustomItem { Product = product };
                flowLayoutPanel1.Controls.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2(new Product()).ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string findstring = textBox1.Text;
            if (string.IsNullOrEmpty(findstring))
            {
                UpdateItems();
            }
            else
            {
                products = helper.FindProduct(findstring);
                flowLayoutPanel1.Controls.Clear();
                foreach (Product product in products)
                {
                    CustomItem item = new CustomItem { Product = product };
                    flowLayoutPanel1.Controls.Add(item);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}