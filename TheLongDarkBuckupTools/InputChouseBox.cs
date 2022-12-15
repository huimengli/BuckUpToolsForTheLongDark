using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheLongDarkBuckupTools.Helpers;

namespace TheLongDarkBuckupTools
{
    public partial class InputChouseBox : Form
    {
        /// <summary>
        /// 返回值
        /// </summary>
        public static string returnValue;

        /// <summary>
        /// 修改对象
        /// </summary>
        public Control labelObj;

        /// <summary>
        /// 修改值对象
        /// </summary>
        public Value value;

        /// <summary>
        /// 保存次数
        /// </summary>
        public Value times;

        /// <summary>
        /// 普通构建
        /// </summary>
        /// <param name="title"></param>
        /// <param name="tishi"></param>
        public InputChouseBox(string title, string tishi)
        {
            InitializeComponent();
            Text = title;
            label2.Text = tishi;
            returnValue = "";
        }

        /// <summary>
        /// 获取选择文件的文件夹
        /// </summary>
        /// <param name="title"></param>
        /// <param name="tishi"></param>
        /// <param name="value"></param>
        public InputChouseBox(string title, string tishi, Value value) : this(title, tishi)
        {
            this.value = value;
        }

        /// <summary>
        /// 获取文件并自动备份
        /// </summary>
        /// <param name="title"></param>
        /// <param name="tishi"></param>
        /// <param name="value"></param>
        /// <param name="times"></param>
        public InputChouseBox(string title, string tishi, Value value,Value times) : this(title, tishi,value)
        {
            this.times = times;
        }

        /// <summary>
        /// 获取的内容显示在被修改项目上
        /// </summary>
        /// <param name="title"></param>
        /// <param name="tishi"></param>
        /// <param name="control"></param>
        public InputChouseBox(string title, string tishi, Control control) : this(title, tishi)
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

        private void InputChouseBox_Load(object sender, EventArgs e)
        {
            Main.IsElseForm = true;
        }

        private void InputChouseBox_UnLoad(object sender, EventArgs e)
        {
            try
            {
                if (labelObj != null)
                {
                    labelObj.Text = returnValue;
                }
                if (value != null&&times!=null)
                {
                    var time = DateTime.Now.ToFileTimeUtc();
                    Item.Save(Item.FileName, value.val, time);
                }
                if (value != null)
                {
                    try
                    {
                        var file = new FileInfo(returnValue);
                        value.val = file.DirectoryName+"\\";
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err);
                        value.val = returnValue;
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
            Main.IsElseForm = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)+ @"\Hinterland\TheLongDark";
            Item.ChoiceFile(path, textBox1);
        }
    }
}
