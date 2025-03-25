using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Concurrent;

/// <summary>
/// 漫漫长夜备份工具
/// </summary>
namespace TheLongDarkBuckupTools
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //防止多个备份工具同时运行
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("程序已经在运行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
#if DEBUG
            //MessageBox.Show("DEBUG");
            //程序启动
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());

#else
            //MessageBox.Show("else");
            //捕获全局异常
            try
            {
                //处理未捕获的异常模式
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                //程序启动
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            catch (Exception err)
            {
                string str = "";
                string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";

                if (err != null)
                {
                    str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                    err.GetType().Name, err.Message, err.StackTrace);
                }
                else
                {
                    str = string.Format("应用程序线程错误:{0}", err);
                }

                MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("发生致命错误，请及时联系作者！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

#endif
        }

        /// <summary>
        /// 多窗体之间共享数据
        /// </summary>
        /// <example> 这里记录着一些公共数据
        /// starWay : Program.PublicData["starWay"] = "steam://rungameid/305620"; // 游戏启动路径
        /// </example>
        public static ConcurrentDictionary<string,object> PublicData = new ConcurrentDictionary<string, object>();

        /// <summary>
        ///这就是我们要在发生未处理异常时处理的方法，我这是写出错详细信息到文本，如出错后弹出一个漂亮的出错提示窗体，给大家做个参考
        ///做法很多，可以是把出错详细信息记录到文本、数据库，发送出错邮件到作者信箱或出错后重新初始化等等
        ///这就是仁者见仁智者见智，大家自己做了。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {

            string str = "";
            string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            Exception error = e.Exception as Exception;
            if (error != null)
            {
                str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("应用程序线程错误:{0}", e);
            }

            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //MessageBox.Show("发生致命错误，请及时联系作者！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = "";
            Exception error = e.ExceptionObject as Exception;
            string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            if (error != null)
            {
                str = string.Format(strDateInfo + "Application UnhandledException:{0};\n\r堆栈信息:{1}", error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("Application UnhandledError:{0}", e);
            }

            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //MessageBox.Show("发生致命错误，请停止当前操作并及时联系作者！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}
