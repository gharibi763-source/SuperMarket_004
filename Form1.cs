using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket_004
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void افزودنکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FUser fu = new FUser();
            fu.ShowDialog();
        }

        private void افزودنمشتریجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FMoshtari moshtaro = new FMoshtari();
            moshtaro.ShowDialog();

        }

        private void ویرایشمشتریToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FMoshtariEdit ME = new FMoshtariEdit();
            ME.ShowDialog();
        }

        private void افزودنکالاهایجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FProducts product = new FProducts();
            product.ShowDialog();
        }

        private void ویرایشکالاهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FProductEdit edit = new FProductEdit();
            edit.ShowDialog();
        }

        private void فاکتورToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FFactor factor = new FFactor();
            factor.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Memory.username == " ") 
            {
                FLogin log = new FLogin();
                log.ShowDialog();
            }
        }
    } 
}
