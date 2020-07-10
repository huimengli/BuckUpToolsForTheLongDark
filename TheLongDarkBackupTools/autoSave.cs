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

namespace TheLongDarkBackupTools
{
    public partial class autoSave : Form
    {

        /// <summary>
        /// 游戏存档位置
        /// </summary>
        private Value gameSavePath;

        /// <summary>
        /// 备份所在位置
        /// </summary>
        private Value buckUpPath;

        /// <summary>
        /// 主页面
        /// </summary>
        private Form form;

        /// <summary>
        /// 文件夹监控对象
        /// </summary>
        public static FileSystemWatcher watcher = new FileSystemWatcher();

        /// <summary>
        /// 临时文件夹路径
        /// </summary>
        private string TemporaryPath = AppDomain.CurrentDomain.BaseDirectory + @"temporary\";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="form"></param>
        /// <param name="gameSavePath"></param>
        /// <param name="buckUpPath"></param>
        public autoSave(Form form,Value gameSavePath,Value buckUpPath)
        {
            this.form = form;
            this.gameSavePath = gameSavePath;
            this.buckUpPath = buckUpPath;
            InitializeComponent();
            this.label4.Text = "";//自动保存还没有做好
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Item.OpenFolder(gameSavePath.val);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void autoSave_UnLoad(object sender,EventArgs e)
        {
            form.Opacity = 1;
        }

        private void autoSave_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            textBox1.Text = path;
            var file = new FileInfo(path);
            try
            {
                var main = (Main)form;
                var times = main.SaveTimeAdd();
                Item.Save(path, buckUpPath.val, times);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }

        private void autoSave_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

    }
}
