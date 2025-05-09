using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TheLongDarkBuckupTools.GameData;
using TheLongDarkBuckupTools.Helpers;

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

        /// <summary>
        /// 启动方法
        /// </summary>
        private string starWay = "";

        /// <summary>
        /// 启动方法
        /// </summary>
        private string StarWay
        {
            get
            {
                return starWay;
            }
            set
            {
                starWay = value;
                Program.PublicData["starWay"] = starWay;
            }
        }

        public Main()
        {
            InitializeComponent();
            Text = "漫漫长夜存档备份工具";
            IsElseForm = false;
            NeedHelp = false;
            var tishi = CurrentPath + "注意事项.txt";
            IniPath = CurrentPath + "BuckUpTools.ini";
            if (File.Exists(tishi)==false) {
                File.WriteAllText(tishi, Massage.tishi);
                Item.OpenOnWindows(tishi);
            }
            //QuickSave = (Keys)Enum.Parse(typeof(Keys), textBox1.Text);
            //radioButton1.Checked = true;
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
            var allValues = BigData.Parses(IniAllValues);
            gameSavePath.val = allValues.SearchData("savePath").Value;
            if (string.IsNullOrWhiteSpace(gameSavePath.val)||!Directory.Exists(gameSavePath.val))
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
            textBox3.Text = allValues.SearchData("buckPath").Value;
            textBox3.Text = string.IsNullOrEmpty(textBox3.Text) ||!Directory.Exists(textBox3.Text) ?CurrentPath+"bfFolder\\":allValues.SearchData("buckPath").Value;
            if (!Item.CheckFolder(textBox3.Text))
            {
                Directory.CreateDirectory(textBox3.Text);
            }
            saveTimes = string.IsNullOrEmpty(allValues.SearchData("saveTimes").Value)?0:int.Parse(allValues.SearchData("saveTimes").Value);
            textBox1.Text = string.IsNullOrEmpty(allValues.SearchData("quickSave").Value)?"F6": allValues.SearchData("quickSave").Value;
            QuickSave = (Keys)Enum.Parse(typeof(Keys), textBox1.Text);
            var RIDIO = allValues.SearchData("starType").Value;
            if (string.IsNullOrEmpty(RIDIO))
            {
                RIDIO = "0";
            }
            switch (RIDIO)
            {
                case "1":
                    radioButton1.Checked = true;
                    break;
                case "2":
                    radioButton2.Checked = true;
                    textBox5.Text = allValues.SearchData("starWay").Value;
                    StarWay = textBox5.Text;
                    break;
                default:
                    radioButton1.Checked = true;
                    break;
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
            var Saves = new List<BigData>();
            Saves.Add(new BigData("savePath", gameSavePath));
            Saves.Add(new BigData("buckPath", textBox3.Text));
            Saves.Add(new BigData("saveTimes",saveTimes));
            Saves.Add(new BigData("saveTimes",saveTimes));
            Saves.Add(new BigData("quickSave",textBox1.Text));
            Saves.Add(new BigData("starType",radioButton1.Checked?1:radioButton2.Checked?2:0));
            Saves.Add(new BigData("starWay",StarWay));

            Item.SaveFile(IniPath, Saves.ToArray().ToString(true));
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
            //if (ChackTexts()&&IsElseForm==false)
            //{
            //    Item.ReadSave( textBox3.Text, gameSavePath.val);
            //}
            new ShowFile(true, gameSavePath, new Value(textBox3.Text),PlayType.Story).Show();
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
            //if (ChackTexts() && IsElseForm == false)
            //{
            //    var fileName = "";
            //    try
            //    {
            //        fileName = Item.GetTheNewFile(gameSavePath.val);
            //    }
            //    catch (Exception err)
            //    {
            //        Console.WriteLine(err);
            //        new InputChouseBox("提示", "没有找到存档文件夹\n请手动输入或者右边选择:", gameSavePath).Show();
            //    }
            //    if (string.IsNullOrEmpty(fileName)==false)
            //    {
            //        SaveTimeAdd();
            //        var time = DateTime.Now.ToFileTimeUtc();
            //        Item.Save(gameSavePath.val, fileName, textBox3.Text, time);
            //    }
            //}
            if (ChackTexts() && IsElseForm == false)
            {
                SaveTimeAdd();
                var saveFile = new Value(textBox3.Text);
                //new InputChouseBox("选择", "请选择要备份的文件", saveFile, new Value(saveTimes.ToString())).Show();
                new ShowFile(false, gameSavePath, saveFile,PlayType.Story).Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ChackTexts()&&IsElseForm==false)
            {
                SaveTimeAdd();
                var saveFile = new Value(textBox3.Text);
                //new InputChouseBox("选择", "请选择要备份的文件", saveFile, new Value(saveTimes.ToString())).Show();
                new ShowFile(false, gameSavePath, saveFile,PlayType.Survival).Show();
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!IsElseForm)
            {
                Item.OpenOnWindows("https://tieba.baidu.com/p/6891350202");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new ShowFile(true, gameSavePath, new Value(textBox3.Text),PlayType.Survival).Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text)==false)
            {
                Item.OpenOnWindows(textBox5.Text);
            }
            else
            {
                MessageBox.Show("请在关于界面设定游戏启动位置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            StarWay = textBox5.Text;
            textBox5.Text = "steam://rungameid/305620";
            textBox5.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Text = StarWay;
            textBox5.Enabled = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                MessageBox.Show("不允许修改steam启动项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (radioButton2.Checked)
            {
                Item.ChoiceFileWithoutCut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), textBox5);
                StarWay = textBox5.Text;
                return;
            }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.Text = "启动游戏";
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Text = "漫漫长夜";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //打开一个全新的界面
            new ChangeFile(this,gameSavePath,new Value(textBox3.Text)).Show();
            Opacity = 0;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Item.Screenshot(@"C:\Users\29133\Desktop\测试的截图\test.png", true);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            StarWay = textBox5.Text;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
