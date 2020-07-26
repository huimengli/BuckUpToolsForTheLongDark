using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TheLongDarkBuckupTools
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

        /// <summary>
        /// 快捷备份按键
        /// </summary>
        public static Keys QuickSave;

        public Main()
        {
            InitializeComponent();
            Text = "漫漫长夜存档备份工具";
            IsElseForm = false;
            NeedHelp = false;
            IniPath = CurrentPath + "BuckUpTools.ini";
            if (File.Exists(IniPath)==false) {
                var tishi = CurrentPath + "注意事项.txt";
                File.WriteAllText(tishi, Massage.tishi);
                Item.OpenOnWindows(tishi);
            }
            QuickSave = (Keys)Enum.Parse(typeof(Keys), textBox1.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!IsElseForm)
            {
                Item.OpenOnWindows("https://github.com/huimengli/BuckUpToolsForTheLongDark");
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //Console.WriteLine(System.Diagnostics.Process.GetProcesses("TheLongDarkBuckUpTools").ToString());
            IniAllValues = Item.ReadAllIni(IniPath)[0];
            var allValues = Item.GetValues(IniAllValues, "savePath");
            gameSavePath.val = "";
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
            textBox3.Text = allValues[0] == "" ? CurrentPath + "bfFolder\\" : allValues[0];
            if (Item.CheckFolder(textBox3.Text) == false)
            {
                Directory.CreateDirectory(textBox3.Text);
            }
            saveTimes = 0; /*allValues[2] == "" ? 0 : int.Parse(allValues[2]);*/
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
                    var time = DateTime.Now.ToFileTimeUtc();
                    Item.Save(gameSavePath.val, fileName, textBox3.Text, time);
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
                new autoSave(this, gameSavePath, new Value(textBox3.Text),new Value(textBox4.Text)).Show();
                Opacity = 0;
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //获取TabControl主控件的工作区域
            Rectangle rec = tabControl1.ClientRectangle;

            //获取背景图片，我的背景图片在项目资源文件中。
            //Image backImage = (Image)new System.ComponentModel.ComponentResourceManager().GetObject("background_min_black");
            Image backImage = Resources.Resource.tab_background;

            //每个小页面的背景图片
            Image EachBackImage = Resources.Resource.tab_each_background;

            //新建一个StringFormat对象，用于对标签文字的布局设置
            StringFormat StrFormat = new StringFormat();
            StrFormat.LineAlignment = StringAlignment.Center;// 设置文字垂直方向居中

            StrFormat.Alignment = StringAlignment.Center;// 设置文字水平方向居中           

            // 标签背景填充颜色，也可以是图片

            //SolidBrush bru = new SolidBrush(Color.FromArgb(72, 181, 250));
            SolidBrush bru = new SolidBrush(Color.FromArgb(0,255, 255, 255));
            
            //SolidBrush bruFont = new SolidBrush(Color.FromArgb(217, 54, 26));// 标签字体颜色
            SolidBrush bruFont = new SolidBrush(Color.FromArgb(0, 0, 0));// 标签字体颜色

            Font font = new System.Drawing.Font("微软雅黑", 12F);//设置标签字体样式



            //绘制主控件的背景

            e.Graphics.DrawImage(backImage, 0, 0, tabControl1.Width, tabControl1.Height);

            //绘制标签样式

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                //获取标签头的工作区域
                Rectangle recChild = tabControl1.GetTabRect(i);
                
                //绘制标签头背景颜色
                e.Graphics.FillRectangle(bru, recChild);

                //绘制标签头的文字
                e.Graphics.DrawString(tabControl1.TabPages[i].Text, font, bruFont, recChild, StrFormat);

                //每页的背景颜色
                tabControl1.TabPages[i].BackgroundImage = EachBackImage;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("抱歉,暂时不能修改按键", "抱歉", MessageBoxButtons.OK);
        }
    }
}
