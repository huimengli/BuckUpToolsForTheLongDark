using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheLongDarkBackupTools
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }

    /// <summary>
    /// 工具类
    /// </summary>
    public static class Item
    {
        /// <summary>
        /// 选择的文件夹路径
        /// </summary>
        private static string Path;

        /// <summary>
        /// 存档文件路径
        /// </summary>
        private static string FileName;

        /// <summary>
        /// 打开网站
        /// </summary>
        /// <param name="web"></param>
        public static void OpenWeb(string web)
        {
            System.Diagnostics.Process.Start(web);
        }

        /// <summary>
        /// 产生一个弹出信息框
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static void NewMassageBox(string title,string str)
        {
            new MyMassageBox(title, str).Show();
        }

        /// <summary>
        /// 产生一个弹出信息框
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static void NewMassageBox(string title,string str,Control label)
        {
            new MyMassageBox(title, str,label).Show();
        }

        public static void NewInputBox(string title,string tishi)
        {
            new InputBox(title, tishi).Show();
        }

        public static void NewInputBox(string title,string tishi,Control label)
        {
            new InputBox(title, tishi,label).Show();
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        public static void ChoiceFolder(Control label)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择存档所在文件夹";
            dialog.ShowNewFolderButton = false;
            dialog.RootFolder = Environment.SpecialFolder.LocalApplicationData;
            if (Path!=String.Empty&&Path!=null)
            {
                dialog.SelectedPath = Path;
            }
            //else
            //{
            //    dialog.SelectedPath = dialog.SelectedPath + "\\Hinterland\\TheLongDark";
            //}
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    return;
                }
                //this.LoadingText = "处理中...";
                //this.LoadingDisplay = true;
                //Action<string> a = DaoRuData;
                //a.BeginInvoke(dialog.SelectedPath, asyncCallback, a);
                Path = dialog.SelectedPath;
            }
            label.Text = Path;
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="label"></param>
        public static void ChoiceFile(string path,Control label)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = path;
            if (dialog.ShowDialog()==DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.FileName))
                {
                    return;
                }
                FileName = dialog.FileName;
            }
            FileName = FileName ?? "";
            label.Text = FileName.Length > path.Length ? FileName.Remove(0, path.Length+1) : "";
        }
    }

    /// <summary>
    /// 添加内容
    /// </summary>
    public static class AddValue
    {

    }
}
