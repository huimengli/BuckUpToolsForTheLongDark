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
        public Main()
        {
            InitializeComponent();
            Text = "漫漫长夜存档备份工具";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Item.OpenWeb("https://github.com/huimengli/BuckUpToolsForTheLongDark");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            //dialog.Description = "请选择存档所在文件夹";
            //if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    if (string.IsNullOrEmpty(dialog.SelectedPath))
            //    {
            //        return;
            //    }
            //    //this.LoadingText = "处理中...";
            //    //this.LoadingDisplay = true;
            //    //Action<string> a = DaoRuData;
            //    //a.BeginInvoke(dialog.SelectedPath, asyncCallback, a);
            //}
            Item.NewMassageBox("null");
        }
    }
}
