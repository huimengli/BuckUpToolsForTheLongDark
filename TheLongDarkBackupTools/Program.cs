﻿using System;
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
        public static bool NewMassageBox(object str)
        {
            var ret = false;

            return ret;
        }

        /// <summary>
        /// 选择文件夹(未完成)
        /// </summary>
        public static void ChoiceFolder()
        {

        }
    }
}
