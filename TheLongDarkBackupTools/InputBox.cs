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
    public partial class InputBox : Form
    {
        /// <summary>
        /// 返回值
        /// </summary>
        public static string returnValue;

        public InputBox(string title,string tishi)
        {
            InitializeComponent();
            Text = title;
            label2.Text = tishi;
            returnValue = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            returnValue = textBox1.Text;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            returnValue = "";
            textBox1.Text = "";
            Close();
        }
    }
}
