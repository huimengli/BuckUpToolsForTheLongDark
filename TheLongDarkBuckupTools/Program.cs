﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO.Compression;
using System.Diagnostics;

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
            //获取程序进程名称
            //MessageBox.Show(Process.GetCurrentProcess().ProcessName);

            //防止多个备份工具同时运行
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("程序已经在运行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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


                MessageBox.Show(str,"系统错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //MessageBox.Show("发生致命错误，请及时联系作者！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

    /// <summary>
    /// 工具类
    /// </summary>
    public static class Item
    {
        /// <summary>
        /// 选择的文件夹路径
        /// </summary>
        private static string ChousePath;

        /// <summary>
        /// 存档文件路径
        /// </summary>
        public static string FileName;

        /// <summary>
        /// 保存文件的文件路径
        /// </summary>
        //private static string SavePath;

        /// <summary>
        /// 打开网站|其他东西
        /// </summary>
        /// <param name="web">网址|地址</param>
        public static void OpenOnWindows(string web)
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

        /// <summary>
        /// 创建一个输入框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="tishi"></param>
        public static void NewInputBox(string title,string tishi)
        {
            new InputBox(title, tishi).Show();
        }

        /// <summary>
        /// 创建一个输入框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="tishi"></param>
        /// <param name="label"></param>
        public static void NewInputBox(string title,string tishi,Control label)
        {
            new InputBox(title, tishi,label).Show();
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        public static void ChoiceFolder(Control label)
        {
            ChoiceFolder(label, "请选择存档所在文件夹");
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="tishi">选择时候提示内容</param>
        public static void ChoiceFolder(Control label,string tishi)
        {
            ChoiceFolder(label, tishi, Environment.SpecialFolder.LocalApplicationData);
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="tishi">选择时候提示内容</param>
        /// <param name="path">已经存在的文件路径</param>
        public static void ChoiceFolder(Control label,string tishi,string path)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = tishi;
            dialog.ShowNewFolderButton = false;
            if (path!=String.Empty&&path!=null)
            {
                dialog.SelectedPath = path;
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
                path = dialog.SelectedPath;
            }
            label.Text = path;
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="label">提示标签</param>
        /// <param name="tishi">选择时候提示内容</param>
        /// <param name="path">已经存在的文件路径</param>
        /// <param name="needNewFolder">是否允许创建新文件夹</param>
        public static void ChoiceFolder(Control label, string tishi, string path, bool needNewFolder)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = tishi;
            dialog.ShowNewFolderButton = needNewFolder;
            if (path != String.Empty && path != null)
            {
                dialog.SelectedPath = path;
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
                path = dialog.SelectedPath;
            }
            label.Text = path;
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="tishi">选择时候提示内容</param>
        /// <param name="folder">系统文件夹枚举项</param>
        public static void ChoiceFolder(Control label,string tishi,Environment.SpecialFolder folder)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = tishi;
            dialog.ShowNewFolderButton = false;
            dialog.RootFolder = folder;
            if (ChousePath!=String.Empty&&ChousePath!=null)
            {
                dialog.SelectedPath = ChousePath;
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
                ChousePath = dialog.SelectedPath;
            }
            label.Text = ChousePath;
        }

        public static void ChoiceFolder(Value value)
        {
            var tishi = "选择存档所在的文件夹\n大概是\\Hinterland\\TheLongDark下";
            Console.WriteLine(tishi);
            ChoiceFolder(value, "选择存档所在的文件夹\n大概是\\Hinterland\\TheLongDark下", Environment.SpecialFolder.LocalApplicationData);
        }

        /// <summary>
        /// 没写
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tishi"></param>
        /// <param name="path"></param>
        /// <param name="needNewFolder"></param>
        public static void ChoiceFolder(Value value,string tishi,string path,bool needNewFolder)
        {

        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="tishi">选择时候提示内容</param>
        /// <param name="folder">系统文件夹枚举项</param>
        public static void ChoiceFolder(Value value, string tishi, Environment.SpecialFolder folder)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = tishi;
            dialog.ShowNewFolderButton = false;
            dialog.RootFolder = folder;
            if (ChousePath != String.Empty && ChousePath != null)
            {
                dialog.SelectedPath = ChousePath;
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
                ChousePath = dialog.SelectedPath;
            }
            value.val = ChousePath;
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

        /// <summary>
        /// 选择文件（包括路径）
        /// </summary>
        /// <param name="label"></param>
        public static void ChoiceFileAll(string path,Control label)
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
            label.Text = FileName;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        public static void Save(string path,string name,string savePath,long times)
        {
            if (File.Exists(savePath+"\\"+name+"_bf"+times.ToString()))
            {
                File.Delete(savePath + "\\" + name + "_bf" + times.ToString());
            }
            var copyCmd = "copy /b \"" + path + "\\" + name + "\" \"" + savePath + "\\" + name + "_bf"+times+"\"";
            //Main.saveTimes++;
            UseCmd(copyCmd);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        public static void Save(string pathName,string savePath,long times)
        {
            var file = new FileInfo(pathName);
            var path = file.DirectoryName;
            var name = file.Name;
            
            if (File.Exists(savePath+"\\"+name+"_bf"+times.ToString()))
            {
                File.Delete(savePath + "\\" + name + "_bf" + times.ToString());
            }
            var copyCmd = "copy /b \"" + path + "\\" + name + "\" \"" + savePath + "\\" + name + "_bf"+times+"\"";
            //Main.saveTimes++;
            UseCmd(copyCmd);
        }

        ///// <summary>
        ///// 保存文件
        ///// </summary>
        //public static void Save(string path,string name,string savePath,int times)
        //{
        //    var copyCmd = "copy /b \"" + path + "\\" + name + "\" \"" + savePath + "\\" + name + "_bf"+times+"\"";
        //    UseCmd(copyCmd);
        //}

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="savePath"></param>
        /// <param name="saveTimes">保存存档次数</param>
        public static void SaveIni(string iniPath,string path,string savePath,int saveTimes)
        {
            path = Base64Encrypt(path);
            savePath = Base64Encrypt(savePath);
            var saveTime = Base64Encrypt(saveTimes.ToString());
            var saveIni = "path:" + path + ";savePath:" + savePath + ";saveTimes:" + saveTime;
            SaveFile(iniPath, saveIni);
        }

        /// <summary>
        /// 检查是否存在这个文件夹
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static bool CheckFolder(string folderName)
        {
            return Directory.Exists(folderName);
        }

        /// <summary>
        /// 读取最新文件夹中有多少文件?
        /// </summary>
        /// <param name="bepath"></param>
        /// <param name="path"></param>
        public static void ReadNewBfFile(string bepath,string path)
        {
            var varCode = "set n=0";
            var getNumCode = "for /f \"delims=\" %a in ('dir /a-d/b \""+bepath+"\"') do set/an+=1";
            var retCode = "echo %n%";
            UseCmd(varCode, getNumCode, retCode);
        }

        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="savePath">保存位置</param>
        public static void Screenshot(string savePath)
        {
            Image img = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
            //Clipboard.SetImage(img);
            img.Save(savePath);
            //Console.WriteLine(g);
        }

        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="savePath">保存位置</param>
        public static Image Screenshot(string savePath,bool nowSave)
        {
            Image img = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
            //Clipboard.SetImage(img);
            //img.Save(savePath);
            //Console.WriteLine(g);
            if (nowSave)
            {
                img.Save(savePath);
            }
            return img;
        }

        /// <summary>
        /// 文件合并
        /// </summary>
        /// <param name="jpgPath"></param>
        /// <param name="zipPath"></param>
        /// <param name="savePath"></param>
        public static void JpgAddZip(string jpgPath,string zipPath,string savePath)
        {
            var code = "copy /b '"+jpgPath+"'+'"+zipPath+"' '"+savePath+"'";
            UseCmd(code);
        }

        /// <summary>
        /// 使用cmd命令
        /// </summary>
        /// <param name="cmdCode"></param>
        public static void UseCmd(string cmdCode)
        {
            System.Diagnostics.Process proIP = new System.Diagnostics.Process();
            proIP.StartInfo.FileName = "cmd.exe";
            proIP.StartInfo.UseShellExecute = false;
            proIP.StartInfo.RedirectStandardInput = true;
            proIP.StartInfo.RedirectStandardOutput = true;
            proIP.StartInfo.RedirectStandardError = true;
            proIP.StartInfo.CreateNoWindow = true;
            proIP.Start();
            proIP.StandardInput.WriteLine(cmdCode);
            proIP.StandardInput.WriteLine("exit");
            string strResult = proIP.StandardOutput.ReadToEnd();
            Console.WriteLine(strResult);
            proIP.Close();
        }

        /// <summary>
        /// 使用cmd命令
        /// </summary>
        /// <param name="cmdCode"></param>
        public static void UseCmd(params string[] cmdCodes)
        {
            System.Diagnostics.Process proIP = new System.Diagnostics.Process();
            proIP.StartInfo.FileName = "cmd.exe";
            proIP.StartInfo.UseShellExecute = false;
            proIP.StartInfo.RedirectStandardInput = true;
            proIP.StartInfo.RedirectStandardOutput = true;
            proIP.StartInfo.RedirectStandardError = true;
            proIP.StartInfo.CreateNoWindow = true;
            proIP.Start();
            foreach (var eachCmd in cmdCodes)
            {
                proIP.StandardInput.WriteLine(eachCmd);
            }
            proIP.StandardInput.WriteLine("exit");
            string strResult = proIP.StandardOutput.ReadToEnd();
            Console.WriteLine(strResult);
            proIP.Close();
        }

        /// <summary>
        /// 保存信息到信息文件中
        /// </summary>
        /// <param name="ini"></param>
        public static void SaveFile(string path,params string[] ini)
        {
            File.WriteAllLines(path, ini,System.Text.Encoding.Unicode);
        }

        /// <summary>
        /// 读取ini配置文件中所有内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] ReadAllIni(string path)
        {
            if (File.Exists(path))
            {
                var ret = File.ReadAllLines(path, System.Text.Encoding.Unicode);
                return ret.Length == 0 ? new string[] { "" } : ret;
            }
            else
            {
                SaveFile(path, "");
                return new string[] {""};
            }
        }

        /// <summary>
        /// 创建新文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void NewFolder(string path)
        {
            //path = path + "\\bfFolder";
            Log(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 向控制台输出内容
        /// </summary>
        /// <param name="str"></param>
        public static void Log(object str) => Console.WriteLine(str);

        /// <summary>
        /// 从字符串中获取信息
        /// </summary>
        /// <param name="beStr"></param>
        /// <param name="valueNames"></param>
        /// <returns></returns>
        public static List<string> GetValues(string beStr,params string[] valueNames)
        {
            var ret = new List<string>();
            for (int i = 0; i < valueNames.Length; i++)
            {
                var ValueGet = new Regex(valueNames[i] + @":([^:;]+)").Match(beStr).Groups[1];
                ret.Add(
                    Base64Decrypt(ValueGet.ToString())
                    );
            }
            return ret;
        }

        /// <summary>
        /// 读取存档
        /// </summary>
        /// <param name="path">备份所在位置</param>
        /// <param name="name"></param>
        /// <param name="savePath">存档所在位置</param>
        public static void ReadSave(string path,string name,string savePath,int saveTimes)
        {
            if (File.Exists(savePath + "\\" + name ))
            {
                File.Delete(savePath + "\\" + name);
            }
            var copyCmd = "copy /b \"" + path + "\\" + name + "_bf" + saveTimes + "\" \"" + savePath + "\\" + name + "\" ";
            //Main.saveTimes++;
            UseCmd(copyCmd);
        }

        /// <summary>
        /// 读取存档
        /// </summary>
        /// <param name="path">备份文件所在位置</param>
        /// <param name="name"></param>
        /// <param name="savePath">存档所在位置</param>
        public static void ReadSave(string path, string savePath)
        {
            var name = GetTheNewFile(path);
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            var file = new FileInfo(name);
            var lastName = file.Extension;
            var saveTimes = new Regex(@"_bf[\d]*").Match(name).Groups[0].ToString();
            var trueName = "";
            try
            {
                if (string.IsNullOrEmpty(lastName))
                {
                    trueName = name.Remove(name.Length - saveTimes.Length);
                }
                else
                {
                    trueName = name.Remove(name.Length - saveTimes.Length - lastName.Length);
                }
            }
            catch (Exception)
            {
                trueName = name;
            }

            if (File.Exists(savePath + "\\" + trueName))
            {
                File.Delete(savePath + "\\" + trueName);
            }

            if (string.IsNullOrEmpty(lastName))
            {
                var copyCmd = "copy /b \"" + path + "\\" + name /*+ "_" + saveTimes */+ "\" \"" + savePath + "\\" + trueName + "\" ";
                //Main.saveTimes++;
                UseCmd(copyCmd);
            }
            else if (lastName == ".gz")
            {
                UnZipFile(file.FullName, savePath + "\\" + trueName);
            }
            else if (lastName == ".png")
            {
                var zipFilePath = file.DirectoryName + @"\zippath\" + trueName + saveTimes + ".gz";
                if (File.Exists(zipFilePath)==false)
                {
                    MessageBox.Show("程序未找到.gz文件!\r\n请检查图片同名压缩包是否删除\r\n或者是否删除zippath文件夹", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UnZipFile(zipFilePath, savePath + "\\" + trueName);
            }
        }

        /// <summary>
        /// 读取存档
        /// </summary>
        /// <param name="path"></param>
        /// <param name="haveName">路径中是否含有文件名</param>
        /// <param name="savePath"></param>
        public static void ReadSave(string path, string savePath,bool haveName)
        {
            if (haveName==false)
            {
                ReadSave(path, savePath);
                return;
            }
            else {
                var name = PathGetName(path);
                path = PathGetFolder(path);
                var saveTimes = new Regex(@"_bf[\d]*").Match(name).Groups[0].ToString();
                var trueName = "";
                try
                {
                    trueName = name.Remove(name.Length - saveTimes.Length);
                }
                catch (Exception)
                {
                    trueName = name;
                }

                if (File.Exists(savePath + "\\" + trueName))
                {
                    File.Delete(savePath + "\\" + trueName);
                }
                var copyCmd = "copy /b \"" + path + "\\" + name /*+ "_" + saveTimes*/ + "\" \"" + savePath + "\\" + trueName + "\" ";
                //Main.saveTimes++;
                UseCmd(copyCmd);
            }                       
        }

        /// <summary>
        /// 读取存档
        /// 可以读取zip类型的存档
        /// 
        /// </summary>
        /// <param name="file">文件对象</param>
        /// <param name="savePath">存档所在位置</param>
        public static void ReadSave(FileInfo file, string savePath)
        {
            var name = file.Name;
            var lastName = file.Extension;
            var path = file.DirectoryName;
            var saveTimes = new Regex(@"_bf[\d]*").Match(name).Groups[0].ToString();
            var trueName = "";
            try
            {
                if (string.IsNullOrEmpty(lastName))
                {
                    trueName = name.Remove(name.Length - saveTimes.Length);
                }
                else
                {
                    trueName = name.Remove(name.Length - saveTimes.Length-lastName.Length);
                }
            }
            catch (Exception)
            {
                trueName = name;
            }
            Console.WriteLine(trueName);

            if (File.Exists(savePath + "\\" + trueName))
            {
                File.Delete(savePath + "\\" + trueName);
            }

            if (string.IsNullOrEmpty(lastName))
            {
                var copyCmd = "copy /b \"" + path + "\\" + name /*+ "_" + saveTimes */+ "\" \"" + savePath + "\\" + trueName + "\" ";
                //Main.saveTimes++;
                UseCmd(copyCmd);
            }
            else if (lastName==".gz")
            {
                UnZipFile(file.FullName, savePath + "\\" + trueName);
            }
            else if (lastName==".png")
            {
                var zipFilePath = file.DirectoryName + @"\zippath\"+trueName+saveTimes+".gz";
                if (File.Exists(zipFilePath)==false)
                {
                    MessageBox.Show("程序未找到.gz文件!\r\n请检查图片同名压缩包是否删除\r\n或者是否删除zippath文件夹", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UnZipFile(zipFilePath, savePath + "\\" + trueName);
            }
        }

        /// <summary>
        /// 在文件管理器中打开文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void OpenFolder(string path)
        {
            var cmd = "explorer.exe ";
            cmd += path;
            UseCmd(cmd);
        }

        #region Base64加密解密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string Base64Encrypt(string input)
        {
            return Base64Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符编码</param>
        /// <returns></returns>
        public static string Base64Encrypt(string input, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(input));
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns></returns>
        public static string Base64Decrypt(string input)
        {
            return Base64Decrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string Base64Decrypt(string input, Encoding encode)
        {
            return encode.GetString(Convert.FromBase64String(input));
        }
        #endregion

        #region 压缩工具

        /// <summary>  
        /// 压缩枚举  
        /// </summary>  
        public enum ZipEnum
        {
            /// <summary>
            /// 压缩时间长，压缩率高
            /// </summary>            
            BZIP2,

            /// <summary>
            /// 压缩效率高，压缩率低
            /// </summary>
            GZIP
        }

        /// <summary>
        /// 单文件压缩（生成的压缩包和第三方的解压软件兼容）
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <returns></returns>
        public static void ZipFile(string sourceFilePath, string zipFileName)
        {
            //string zipFileName = sourceFilePath + ".gz";
            using (FileStream sourceFileStream = new FileInfo(sourceFilePath).OpenRead())
            {
                using (FileStream zipFileStream = File.Create(zipFileName))
                {
                    using (GZipStream zipStream = new GZipStream(zipFileStream, CompressionMode.Compress))
                    {
                        sourceFileStream.CopyTo(zipStream);
                    }
                }
            }
            //return zipFileName;
        }

        /// <summary>
        /// 自定义多文件压缩（生成的压缩包和第三方的压缩文件解压不兼容）
        /// </summary>
        /// <param name="sourceFileList">文件列表</param>
        /// <param name="saveFullPath">压缩包全路径</param>
        public static void ZipFiles(string[] sourceFileList, string saveFullPath)
        {
            MemoryStream ms = new MemoryStream();
            foreach (string filePath in sourceFileList)
            {
                Console.WriteLine(filePath);
                if (File.Exists(filePath))
                {
                    string fileName = Path.GetFileName(filePath);
                    byte[] fileNameBytes = System.Text.Encoding.UTF8.GetBytes(fileName);
                    byte[] sizeBytes = BitConverter.GetBytes(fileNameBytes.Length);
                    ms.Write(sizeBytes, 0, sizeBytes.Length);
                    ms.Write(fileNameBytes, 0, fileNameBytes.Length);
                    byte[] fileContentBytes = System.IO.File.ReadAllBytes(filePath);
                    ms.Write(BitConverter.GetBytes(fileContentBytes.Length), 0, 4);
                    ms.Write(fileContentBytes, 0, fileContentBytes.Length);
                }
            }
            ms.Flush();
            ms.Position = 0;
            using (FileStream zipFileStream = File.Create(saveFullPath))
            {
                using (GZipStream zipStream = new GZipStream(zipFileStream, CompressionMode.Compress))
                {
                    ms.Position = 0;
                    ms.CopyTo(zipStream);
                }
            }
            ms.Close();
        }

        /// <summary>
        /// 多文件压缩解压
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="targetPath">解压目录</param>
        public static void UnZipFiles(string zipPath, string targetPath)
        {
            byte[] fileSize = new byte[4];
            if (File.Exists(zipPath))
            {
                using (FileStream fStream = File.Open(zipPath, FileMode.Open))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (GZipStream zipStream = new GZipStream(fStream, CompressionMode.Decompress))
                        {
                            zipStream.CopyTo(ms);
                        }
                        ms.Position = 0;
                        while (ms.Position != ms.Length)
                        {
                            ms.Read(fileSize, 0, fileSize.Length);
                            int fileNameLength = BitConverter.ToInt32(fileSize, 0);
                            byte[] fileNameBytes = new byte[fileNameLength];
                            ms.Read(fileNameBytes, 0, fileNameBytes.Length);
                            string fileName = System.Text.Encoding.UTF8.GetString(fileNameBytes);
                            string fileFulleName = targetPath + fileName;
                            ms.Read(fileSize, 0, 4);
                            int fileContentLength = BitConverter.ToInt32(fileSize, 0);
                            byte[] fileContentBytes = new byte[fileContentLength];
                            ms.Read(fileContentBytes, 0, fileContentBytes.Length);
                            using (FileStream childFileStream = File.Create(fileFulleName))
                            {
                                childFileStream.Write(fileContentBytes, 0, fileContentBytes.Length);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 单文件压缩（生成的压缩包和第三方的解压软件兼容）
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <returns></returns>
        public static void UnZipFile(string zipFileName, string sourceFilePath)
        {
            using (GZipStream zipStream = new GZipStream(new FileInfo(zipFileName).OpenRead(), CompressionMode.Decompress))
            {
                using (FileStream srcStream = File.Create(sourceFilePath))
                {
                    zipStream.CopyTo(srcStream);
                }
            }
        }
        #region 弃用的压缩方式（无法使程序为单文件）
        ///// <summary>  
        ///// 制作压缩包（单个文件压缩）  
        ///// </summary>  
        ///// <param name="srcFileName">原文件</param>  
        ///// <param name="zipFileName">压缩文件</param>  
        ///// <param name="zipEnum">压缩算法枚举</param>  
        ///// <returns>压缩成功标志</returns>  
        //public static bool ZipFile(string srcFileName, string zipFileName, ZipEnum zipEnum)
        //{
        //    bool flag = true;
        //    try
        //    {
        //        switch (zipEnum)
        //        {
        //            case ZipEnum.BZIP2:

        //                FileStream inStream = File.OpenRead(srcFileName);
        //                FileStream outStream = File.Open(zipFileName, FileMode.Create);

        //                //参数true表示压缩完成后，inStream和outStream连接都释放  
        //                BZip2.Compress(inStream, outStream, true, 4096);

        //                inStream.Close();
        //                outStream.Close();


        //                break;
        //            case ZipEnum.GZIP:

        //                FileStream srcFile = File.OpenRead(srcFileName);

        //                GZipOutputStream zipFile = new GZipOutputStream(File.Open(zipFileName, FileMode.Create));

        //                byte[] fileData = new byte[srcFile.Length];
        //                srcFile.Read(fileData, 0, (int)srcFile.Length);
        //                zipFile.Write(fileData, 0, fileData.Length);

        //                srcFile.Close();
        //                zipFile.Close();

        //                break;
        //            default: break;
        //        }
        //    }
        //    catch
        //    {
        //        flag = false;
        //    }
        //    return flag;
        //}

        ///// <summary>
        ///// 解压压缩包(单个文件的压缩包)
        ///// </summary>
        ///// <param name="zipFileName">压缩包路径</param>
        ///// <param name="srcFileName">解压文件到</param>
        ///// <param name="zipEnum">压缩类型</param>
        ///// <returns></returns>
        //public static bool UnZipFile(string zipFileName, string srcFileName, ZipEnum zipEnum)
        //{
        //    var ret = true;

        //    try
        //    {
        //        switch (zipEnum)
        //        {
        //            case ZipEnum.BZIP2:

        //                var inFile = File.OpenRead(zipFileName);
        //                var outFile = File.Open(srcFileName, FileMode.Create);

        //                BZip2.Decompress(inFile, outFile, true);

        //                inFile.Close();
        //                outFile.Close();

        //                break;
        //            case ZipEnum.GZIP:

        //                var srcFile = File.Open(srcFileName, FileMode.Create);
        //                var zipFile = new GZipInputStream(File.OpenRead(zipFileName));

        //                int buffersize = 2048;//设置缓冲区大小
        //                byte[] fileData = new byte[buffersize];//创建缓冲数据

        //                while (buffersize > 0)//一直读取到文件末尾
        //                {
        //                    buffersize = zipFile.Read(fileData, 0, buffersize);
        //                    srcFile.Write(fileData, 0, buffersize);//写入目标文件
        //                }


        //                srcFile.Close();
        //                zipFile.Close();

        //                break;
        //            default: break;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ret = false;
        //        throw;
        //    }

        //    return ret;
        //} 
        #endregion

        #endregion

        /// <summary>
        /// 监测文件夹变动
        /// </summary>
        [PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
        public static void CheckFolder()
        {

        }

        /// <summary>
        /// 切割文件名称
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string CutFileName(string fileName)
        {
            var all = fileName.Split('.');
            var ret = all.Length==1?all[0]:"";
            for (int i = 0; i < all.Length-1; i++)
            {
                ret += all[i];
                if (i<all.Length-2)
                {
                    ret += ".";
                }
            }
            return ret;
        }

        /// <summary>
        /// 获取文件后缀名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string CutFileLastName(string fileName)
        {
            var all = fileName.Split('.');
            var ret = "";
            if (all.Length>1)
            {
                ret = all[all.Length - 1];
            }
            return ret;
        }

        /// <summary>
        /// 获取文件夹中最近修改的文件
        /// </summary>
        /// <param name="floderPath">文件夹路径</param>
        /// <returns></returns>
        public static string GetTheNewFile(string floderPath)
        {
            var ret = "";

            var folder = new DirectoryInfo(floderPath);
            var files = folder.GetFiles();

            var allTimes = new List<double>();

            foreach (var item in files)
            {
                allTimes.Add(item.LastAccessTime.ToFileTimeUtc());
            }

            var index = MaxIndex(allTimes);

            ret = files[index].Name;

            return ret;
        }

        /// <summary>
        /// 从路径+文件名中切割出文件名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string PathGetName(string path)
        {
            var ret = "";

            var all = path.Split('\\', '/');

            ret = all[all.Length - 1];

            return ret;
        }

        /// <summary>
        /// 从路径+文件名中切割出路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string PathGetFolder(string path)
        {
            var ret = "";

            var all = path.Split('\\', '/');

            for (int i = 0; i < all.Length-1; i++)
            {
                ret += all[i] += "\\";
            }

            return ret;
        }

        #region 获取最大最小值

        /// <summary>
        /// 获取所有值中最小的
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static double Min(params double[] numbers)
        {
            var mix = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (mix > numbers[i])
                {
                    mix = numbers[i];
                }
            }

            return mix;
        }

        /// <summary>
        /// 获取所有值中最小的
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static double Min(List<double> numbers)
        {
            var mix = numbers[0];

            for (int i = 1; i < numbers.Count; i++)
            {
                if (mix > numbers[i])
                {
                    mix = numbers[i];
                }
            }

            return mix;
        }

        /// <summary>
        /// 获取所有值中最小的项数
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int MinIndex(params double[] numbers)
        {
            var index = 0;
            var mix = numbers[0];

            for (int i = 0; i < numbers.Length; i++)
            {
                if (mix > numbers[i])
                {
                    mix = numbers[i];
                    index = i;
                }
            }

            return index;
        }

        /// <summary>
        /// 获取所有值中最小的项数
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int MinIndex(List<double> numbers)
        {
            var index = 0;
            var mix = numbers[0];

            for (int i = 0; i < numbers.Count; i++)
            {
                if (mix > numbers[i])
                {
                    mix = numbers[i];
                    index = i;
                }
            }

            return index;
        }

        /// <summary>
        /// 获取所有值中最大的
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static double Max(params double[] numbers)
        {
            var max = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (max < numbers[i])
                {
                    max = numbers[i];
                }
            }

            return max;
        }

        /// <summary>
        /// 获取所有值中最大的
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static double Max(List<double> numbers)
        {
            var max = numbers[0];

            for (int i = 1; i < numbers.Count; i++)
            {
                if (max < numbers[i])
                {
                    max = numbers[i];
                }
            }

            return max;
        }

        /// <summary>
        /// 获取所有值中最小的项数
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int MaxIndex(params double[] numbers)
        {
            var index = 0;
            var max = numbers[0];

            for (int i = 0; i < numbers.Length; i++)
            {
                if (max < numbers[i])
                {
                    max = numbers[i];
                    index = i;
                }
            }

            return index;
        }

        /// <summary>
        /// 获取所有值中最小的项数
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int MaxIndex(List<double> numbers)
        {
            var index = 0;
            var max = numbers[0];

            for (int i = 0; i < numbers.Count; i++)
            {
                if (max < numbers[i])
                {
                    max = numbers[i];
                    index = i;
                }
            }

            return index;
        }

        #endregion

        /// <summary>
        /// 打印所有进程名称
        /// </summary>
        public static void PrintAllProcess()
        {
            foreach (var p in Process.GetProcesses())
            {
                Console.WriteLine(p.ProcessName);
            }
        }
    }

    /// <summary>
        /// 添加内容
        /// </summary>
    public static class AddValue
    {


        ///// <summary>
        ///// 注册热键
        ///// </summary>
        ///// <param name="c">按键</param>
        ///// <param name="bCtrl">是否需要ctrl</param>
        ///// <param name="bShift">是否需要shift</param>
        ///// <param name="bAlt">是否需要alt</param>
        ///// <param name="bWindows">是否需要win</param>
        //public void SetHotKey(Keys c, bool bCtrl, bool bShift, bool bAlt, bool bWindows)
        //{
        //    //先赋到变量
        //    Keys m_key = c;
        //    bool m_ctrlhotkey = bCtrl;
        //    bool m_shifthotkey = bShift;
        //    bool m_althotkey = bAlt;
        //    bool m_winhotkey = bWindows;

        //    //注册系统热键
        //    NativeWIN32.KeyModifiers modifiers = NativeWIN32.KeyModifiers.None;
        //    if (bCtrl)
        //        modifiers |= NativeWIN32.KeyModifiers.Control;
        //    if (bShift)
        //        modifiers |= NativeWIN32.KeyModifiers.Shift;
        //    if (bAlt)
        //        modifiers |= NativeWIN32.KeyModifiers.Alt;
        //    if (bWindows)
        //        modifiers |= NativeWIN32.KeyModifiers.Windows;
        //    NativeWIN32.RegisterHotKey(Handle, 100, modifiers, c);
        //}

        ///// <summary>
        ///// 重写windows消息响应
        ///// </summary>
        ///// <param name="m"></param>
        //protected override void WndProc(ref Message m)
        //{
        //    const int wmHotkey = 0x0312;
        //    switch (m.Msg)
        //    {
        //        case wmHotkey:
        //            WindowMax();
        //            break;
        //    }
        //    base.WndProc(ref m);
        //}
    }

    /// <summary>
    /// string指向性问题
    /// 通过类来进行指向性修改内容
    /// </summary>
    public class Value
    {
        /// <summary>
        /// 数据类数据值
        /// </summary>
        public string val;

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="value"></param>
        public Value(string value)
        {
            val = value;
        }
    }

    public class NativeWIN32
    {
        public NativeWIN32(){ }
        /* ------- using WIN32 Windows API in a C# application ------- */

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern IntPtr GetForegroundWindow(); //

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct STRINGBUFFER
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szText;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, out STRINGBUFFER ClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        public delegate bool EnumThreadProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumThreadWindows(int threadId, EnumThreadProc pfnEnum, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr next, string sClassName, IntPtr sWindowTitle);


        /* ------- using HOTKEYs in a C# application -------
 
        in form load :
         bool success = RegisterHotKey(Handle, 100,     KeyModifiers.Control | KeyModifiers.Shift, Keys.J);
 
        in form closing :
         UnregisterHotKey(Handle, 100);
  
 
        protected override void WndProc( ref Message m )
        { 
         const int WM_HOTKEY = 0x0312;  
   
         switch(m.Msg) 
         { 
          case WM_HOTKEY:  
           MessageBox.Show("Hotkey pressed");  
           break; 
         }  
         base.WndProc(ref m );
        }
 
        ------- using HOTKEYs in a C# application ------- */

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, // handle to window   
         int id,            // hot key identifier   
         KeyModifiers fsModifiers,  // key-modifier options   
         Keys vk            // virtual-key code   
         );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd,  // handle to window   
         int id      // hot key identifier   
         );

        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }
    }

    #region 自定义的tabControl(弃用)
    //public class TabControlEx : TabControl
    //{
    //    private Color _BackColor; //背景颜色

    //    public TabControlEx()
    //    {
    //        this.SetStyle(ControlStyles.UserPaint, true);//用户自己绘制
    //        this.SetStyle(ControlStyles.ResizeRedraw, true);
    //        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);   //
    //        this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    //        //让控件支持透明色
    //        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    //        this.UpdateStyles();
    //    }

    //    public override Color BackColor
    //    {//重写backcolor属性 
    //        get
    //        {
    //            return this._BackColor;
    //        }
    //        set
    //        {
    //            this._BackColor = value;
    //        }
    //    }

    //    protected override void OnPaint(PaintEventArgs e)
    //    {
    //        this.DrawTitle(e.Graphics);
    //        base.OnPaint(e);
    //    }

    //    protected virtual void DrawTitle(Graphics g)
    //    {
    //        Image imgButton = OcvMana.Properties.Resources.button;

    //        StringFormat sf = new StringFormat();
    //        sf.Alignment = StringAlignment.Center;
    //        sf.LineAlignment = StringAlignment.Center;
    //        Font font = new System.Drawing.Font("微软雅黑", 10F, FontStyle.Bold);//设置标签字体样式
    //        using (SolidBrush sb = new SolidBrush(Color.FromArgb(127, 0, 0, 0)))
    //        {
    //            for (int i = 0; i < this.TabPages.Count; i++)
    //            {
    //                Rectangle rect = this.GetTabRect(i);
    //                Rectangle newRect = new Rectangle(rect.Left + 7, rect.Top, rect.Width - 7, rect.Height);

    //                g.DrawImage(imgButton, newRect);
    //                g.DrawString(this.TabPages[i].Text, font, Brushes.White, rect, sf);
    //            }
    //        }
    //    }
    //} 
    #endregion
}
