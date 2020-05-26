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
    public partial class Main : Form
    {
        /// <summary>
        /// 是否存在其他窗体
        /// </summary>
        public static bool IsElseForm;

        /// <summary>
        /// 是否需要提示
        /// </summary>
        public bool NeedHelp;

        public Main()
        {
            InitializeComponent();
            Text = "漫漫长夜存档备份工具";
            IsElseForm = false;
            NeedHelp = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!IsElseForm)
            {
                Item.OpenWeb("https://github.com/huimengli/BuckUpToolsForTheLongDark");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsElseForm&&NeedHelp)
            {
                Item.NewMassageBox("提示", "请寻找Hinterland文件夹中的TheLongDark\n文件夹,如果没有请至少保存一下存档");
            }
            Item.ChoiceFolder(textBox1);
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!IsElseForm&&NeedHelp)
            {
                Item.NewMassageBox("提示", "生存模式的存档大多为sandbox+数字");
            }
            if ((textBox1.Text==""||textBox1.Text ==null)&&!IsElseForm)
            {
                Item.NewMassageBox("警告", "请先选择文件夹");
            }
            else
            {
                Item.ChoiceFile(textBox1.Text, textBox2);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            NeedHelp = !NeedHelp;
        }
    }
}
