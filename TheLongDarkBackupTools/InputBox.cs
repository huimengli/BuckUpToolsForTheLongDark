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

        /// <summary>
        /// 修改对象
        /// </summary>
        public Control labelObj;

        public InputBox(string title,string tishi)
        {
            InitializeComponent();
            Text = title;
            label2.Text = tishi;
            returnValue = "";
        }

        public InputBox(string title,string tishi,Control control):this(title,tishi)
        {
            labelObj = control;
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

        private void InputBox_Load(object sender, EventArgs e)
        {
            Main.IsElseForm = true;
        }
        
        private void InputBox_UnLoad(object sender, EventArgs e)
        {
            try
            {
                if (labelObj!=null)
                {
                    labelObj.Text = returnValue;
                }
            }
            catch (Exception)
            {
                throw;
            }
            Main.IsElseForm = false;
        }
    }
}
