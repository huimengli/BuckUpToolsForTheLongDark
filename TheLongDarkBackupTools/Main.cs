using System;
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

        /// <summary>
        /// 当前程序所在的路径
        /// </summary>
        private string CurrentPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// ini文件保存的位置
        /// </summary>
        private string IniPath;

        /// <summary>
        /// ini中的所有内容
        /// </summary>
        private string IniAllValues;

        /// <summary>
        /// 保存的次数
        /// </summary>
        private int saveTimes;

        public Main()
        {
            InitializeComponent();
            Text = "漫漫长夜存档备份工具";
            IsElseForm = false;
            NeedHelp = false;
            IniPath = CurrentPath + "BuckUpTools.ini";
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
            if (!IsElseForm && NeedHelp)
            {
                Item.NewMassageBox("提示", "请寻找Hinterland文件夹中的TheLongDark\n文件夹,如果没有请至少保存一下存档");
            }
            Item.ChoiceFolder(textBox1);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            IniAllValues = Item.ReadAllIni(IniPath)[0];
            var allValues = Item.GetValues(IniAllValues, "path", "name", "savePath", "saveTimes");
            textBox1.Text = allValues[0];
            textBox2.Text = allValues[1];
            textBox3.Text = allValues[2] == "" ? CurrentPath + "bfFolder" : allValues[2];
            saveTimes = allValues[3] == "" ? 0 : int.Parse(allValues[3]);
            label6.Text = "当前备份的存档指针:" + saveTimes.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!IsElseForm && NeedHelp)
            {
                Item.NewMassageBox("提示", "生存模式的存档大多为sandbox+数字");
            }
            if ((textBox1.Text == "" || textBox1.Text == null) && !IsElseForm)
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (!IsElseForm && NeedHelp)
            {
                Item.NewMassageBox("提示", "不设置默认保存在工具同一目录");
            }
            Item.ChoiceFolder(textBox3, "请选择备份保存的文件夹", CurrentPath, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ChackTexts())
            {
                SaveTimeAdd();
                Item.Save(textBox1.Text, textBox2.Text, textBox3.Text, saveTimes);
            }
        }

        /// <summary>
        /// 保存次数增加
        /// </summary>
        private void SaveTimeAdd()
        {
            saveTimes++;
            label6.Text = "当前备份的存档指针:" + saveTimes.ToString();
        }

        /// <summary>
        /// 检查所有的输入框有没有内容
        /// </summary>
        /// <returns></returns>
        private bool ChackTexts()
        {
            if (string.IsNullOrEmpty(textBox1.Text) && !IsElseForm)
            {
                Item.NewMassageBox("警告", "没有选择备份所在的文件夹");
                return false;
            }
            if (string.IsNullOrEmpty(textBox2.Text) && !IsElseForm)
            {
                Item.NewMassageBox("警告", "没有选择备份的文件名");
                return false;
            }
            if (string.IsNullOrEmpty(textBox3.Text) && !IsElseForm)
            {
                Item.NewMassageBox("警告", "没有选择备份保存的位置");
                return false;
            }
            return true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Item.NewFolder(textBox3.Text);
        }

        private void Main_Unload(object sender, EventArgs e)
        {
            Item.SaveIni(IniPath, textBox1.Text, textBox2.Text, textBox3.Text, saveTimes);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ChackTexts())
            {
                Item.Save(textBox1.Text, textBox2.Text, textBox3.Text, saveTimes);
            }
            //if (!IsElseForm)
            //{
            //    Item.NewMassageBox("抱歉", "这个函数没写好,会导致程序卡死");
            //}
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ChackTexts())
            {
                Item.ReadSave(textBox1.Text, textBox2.Text, textBox3.Text, saveTimes);
            }
        }
    }
}
