using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopTanuki
{
    public partial class Fukidashi : Form
    {
        public Fukidashi()
        {
            InitializeComponent();
        }

        public void setText(String str)
        {
            textBox1.Text = str;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
