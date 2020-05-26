using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
        /// 保存文件的文件路径
        /// </summary>
        //private static string SavePath;

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
        /// <param name="tishi">选择时候提示内容</param>
        /// <param name="folder">系统文件夹枚举项</param>
        public static void ChoiceFolder(Control label,string tishi,Environment.SpecialFolder folder)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = tishi;
            dialog.ShowNewFolderButton = false;
            dialog.RootFolder = folder;
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

        /// <summary>
        /// 保存文件
        /// </summary>
        public static void Save(string path,string name,string savePath,int times)
        {
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
        public static void SaveIni(string iniPath,string path,string name,string savePath,int saveTimes)
        {
            path = Base64Encrypt(path);
            name = Base64Encrypt(name);
            savePath = Base64Encrypt(savePath);
            var saveTime = Base64Encrypt(saveTimes.ToString());
            var saveIni = "path:" + path + ";name:" + name + ";savePath:" + savePath + ";saveTimes:" + saveTime;
            SaveFile(iniPath, saveIni);
        }

        public static void ReadNewBfFile(string bepath,string path)
        {
            var varCode = "set n=0";
            var getNumCode = "for /f \"delims=\" %a in ('dir /a-d/b \""+bepath+"\"') do set/an+=1";
            var retCode = "echo %n%";
            UseCmd(varCode, getNumCode, retCode);
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
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="savePath"></param>
        public static void ReadSave(string path,string name,string savePath,int saveTimes)
        {
            if (File.Exists(path + "\\" + name ))
            {
                File.Delete(path + "\\" + name);
            }
            var copyCmd = "copy /b \"" + savePath + "\\" + name + "_bf" + saveTimes + "\" \"" + path + "\\" + name + "\" ";
            //Main.saveTimes++;
            UseCmd(copyCmd);
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
    }

    /// <summary>
    /// 添加内容
    /// </summary>
    public static class AddValue
    {

    }
}
