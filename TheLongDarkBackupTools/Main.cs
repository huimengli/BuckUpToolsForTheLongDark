using System;
using System.IO;
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

        /// <summary>
        /// 游戏存档位置
        /// </summary>
        private Value gameSavePath = new Value("");

        public Main()
        {
            InitializeComponent();
            Text = "漫漫长夜存档备份工具";
            IsElseForm = false;
            NeedHelp = false;
            IniPath = CurrentPath + "BuckUpTools.ini";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!IsElseForm)
            {
                Item.OpenOnWindwos("https://github.com/huimengli/BuckUpToolsForTheLongDark");
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            IniAllValues = Item.ReadAllIni(IniPath)[0];
            var allValues = Item.GetValues(IniAllValues, "path", "savePath", "saveTimes");
            gameSavePath.val = allValues[0];
            if (string.IsNullOrEmpty(gameSavePath.val))
            {
                //自动读取存档路径
                gameSavePath.val = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                gameSavePath.val += "\\Hinterland\\TheLongDark\\";
            }
            if (Item.CheckFolder(gameSavePath.val) == false)
            {
                if (IsElseForm==false)
                {
                    new InputChouseBox("提示", "没有找到存档文件夹\n请手动输入或者右边选择:", gameSavePath);
                }
            }
            textBox3.Text = allValues[1] == "" ? CurrentPath + "bfFolder\\" : allValues[1];
            saveTimes = allValues[2] == "" ? 0 : int.Parse(allValues[2]);
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

        /// <summary>
        /// 保存次数增加
        /// </summary>
        public int SaveTimeAdd()
        {
            saveTimes++;
            return saveTimes;
        }

        /// <summary>
        /// 检查所有的输入框有没有内容
        /// </summary>
        /// <returns></returns>
        private bool ChackTexts()
        {
            if (string.IsNullOrEmpty(gameSavePath.val) && !IsElseForm)
            {
                Item.NewMassageBox("警告", "没有选择备份所在的文件夹\n请退出程序重开试试");
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
            try
            {
                Item.NewFolder(textBox3.Text);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }

        private void Main_Unload(object sender, EventArgs e)
        {
            Item.SaveIni(IniPath,gameSavePath.val, textBox3.Text, saveTimes);
        }

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    if (ChackTexts())
        //    {
        //        Item.Save(textBox1.Text, textBox2.Text, textBox3.Text, saveTimes);
        //    }
        //    //if (!IsElseForm)
        //    //{
        //    //    Item.NewMassageBox("抱歉", "这个函数没写好,会导致程序卡死");
        //    //}
        //}

        private void button6_Click(object sender, EventArgs e)
        {
            if (ChackTexts())
            {
                Item.ReadSave( textBox3.Text, gameSavePath.val);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ChackTexts())
            {
                if (Directory.Exists(textBox3.Text))
                {
                    Item.OpenFolder(textBox3.Text);
                }
                else
                {
                    Directory.CreateDirectory(textBox3.Text);
                    Item.OpenFolder(textBox3.Text);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ChackTexts())
            {
                var fileName = "";
                try
                {
                    fileName = Item.GetTheNewFile(gameSavePath.val);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                    new InputChouseBox("提示", "没有找到存档文件夹\n请手动输入或者右边选择:", gameSavePath).Show();
                }
                if (string.IsNullOrEmpty(fileName)==false)
                {
                    SaveTimeAdd();
                    Item.Save(gameSavePath.val, fileName, textBox3.Text, saveTimes);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ChackTexts()&&IsElseForm==false)
            {
                SaveTimeAdd();
                var saveFile = new Value(textBox3.Text);
                new InputChouseBox("选择", "请选择要备份的文件", saveFile, new Value(saveTimes.ToString())).Show();
            }
        }

        //private void label3_DragEnter(object sender, DragEventArgs e)
        //{
        //    Console.WriteLine(e);
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        e.Effect = DragDropEffects.All;
        //    }
        //    else
        //    {
        //        e.Effect = DragDropEffects.None;
        //    }
        //}

        //private void label3_DragDrop(object sender, DragEventArgs e)
        //{
        //    string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
        //    Console.WriteLine(path);
        //}

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Item.OpenFolder(gameSavePath.val);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                Item.ChoiceFolder(gameSavePath);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (IsElseForm==false)
            {
                new readBuckUp(this,gameSavePath,new Value(textBox3.Text)).Show();
                Opacity = 0;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (IsElseForm==false)
            {
                new autoSave(this, gameSavePath, new Value(textBox3.Text)).Show();
                Opacity = 0;
            }
        }
    }
}
