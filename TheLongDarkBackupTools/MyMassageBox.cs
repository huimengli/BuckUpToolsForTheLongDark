using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheLongDarkBackupTools
{
    public partial class MyMassageBox : Form
    {
        /// <summary>
        /// Massage窗体返回值
        /// </summary>
        public bool returnValue;

        public MyMassageBox(string title,string value)
        {
            InitializeComponent();
            Text = title;
            label1.Text = title;
            label2.Text = value;
        }

        private void MassageBox_Load(object sender, EventArgs e)
        {
            returnValue = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            returnValue = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            returnValue = false;
            Close();
        }
    }
}
