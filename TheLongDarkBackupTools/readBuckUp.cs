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
    public partial class readBuckUp : Form
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
        /// 其他窗体页面
        /// </summary>
        private Form form;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="gameSavePath">存档保存的位置</param>
        /// <param name="buckUpPath">备份保存的位置</param>
        public readBuckUp(Form form,Value gameSavePath,Value buckUpPath)
        {
            this.form = form;
            this.gameSavePath = gameSavePath;
            this.buckUpPath = buckUpPath;
            InitializeComponent();
        }

        private void readBuckUp_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            textBox1.Text = path;
            var file = new FileInfo(path);
            try
            {
                Item.ReadSave(file, gameSavePath.val);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private void readBuckUp_DragEnter(object sender, DragEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(buckUpPath.val))
            {
                Item.OpenFolder(buckUpPath.val);
            }
            else
            {
                Directory.CreateDirectory(buckUpPath.val);
                Item.OpenFolder(buckUpPath.val);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void readBuckUp_UnLoad(object sender,EventArgs e)
        {
            form.Opacity = 1;
        }

        private void readBuckUp_Load(object sender, EventArgs e)
        {

        }
    }
}
