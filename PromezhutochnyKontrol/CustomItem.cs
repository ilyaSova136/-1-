using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PromezhutochnyKontrol
{
    public partial class CustomItem : UserControl
    {
        public CustomItem()
        {
            InitializeComponent();
        }

        public Product Product { get; set; }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray;
        }

        private void CustomItem_Click(object sender, EventArgs e)
        {
            new Form2(Product).ShowDialog();
        }

        private void CustomItem_Load(object sender, EventArgs e)
        {
            label1.Text = Product.ProductType.Title;
            label2.Text = Product.Title;
            label3.Text = Product.Article;
            label4.Text = string.Join(", ", Product.Materials.Select(m => m.Title));
            string Puth = Product.Image ?? "C:\\Users\\karat\\Desktop\\Промежуточный контроль\\Сессия 1\\products\\paper_0.jpeg";
            pictureBox1.Image = Image.FromFile(Puth);
        }

    }
}
