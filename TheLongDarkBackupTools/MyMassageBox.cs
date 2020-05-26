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

        /// <summary>
        /// 标签对象
        /// </summary>
        public Control labelObj;

        public MyMassageBox(string title,string value)
        {
            InitializeComponent();
            Text = title;
            label1.Text = title;
            label2.Text = value;
        }

        public MyMassageBox(string title,string value,Control label):this(title,value)
        {
            labelObj = label;
        }

        private void MassageBox_Load(object sender, EventArgs e)
        {
            returnValue = false;
            Main.IsElseForm = true;
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

        private void MassageBox_UnLoad(object sender,EventArgs e)
        {
            try
            {
                if (labelObj!=null)
                {
                    labelObj.Text = returnValue.ToString();
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
