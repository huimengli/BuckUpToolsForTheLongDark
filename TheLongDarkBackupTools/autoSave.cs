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
using System.Threading;
using System.Text.RegularExpressions;

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
        /// 备份所在的位置
        /// </summary>
        public static Value BuckUpPath;

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
        //public static string TemporaryPath;//= AppDomain.CurrentDomain.BaseDirectory + @"temporary\";

        /// <summary>
        /// zip文件保存路径
        /// </summary>
        public static string ZipPath;

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
            BuckUpPath = buckUpPath;
            ZipPath = buckUpPath.val + @"\zippath\";
            InitializeComponent();
            if (Directory.Exists(ZipPath)==false)
            {
                Directory.CreateDirectory(ZipPath);
            }
            label4.Text = "";//自动保存还没有做好
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
            //结束文件夹监视
            watcher.EnableRaisingEvents = false;
        }

        private void autoSave_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            textBox1.Text = path;
            var file = new FileInfo(path);
            try
            {
                var main = (Main)form;
                main.SaveTimeAdd();
                var times = DateTime.Now.ToFileTimeUtc();
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

        private void autoSave_Load(object sender, EventArgs e)
        {
            var watch = new Watcher();
            if (!Directory.Exists(gameSavePath.val))
            {
                Console.WriteLine("存档文件夹不存在！");
                return;
            }
            watcher.Path = gameSavePath.val;
            /*监视LastAcceSS和LastWrite时间的更改以及文件或目录的重命名*/
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite |
                  NotifyFilters.FileName | NotifyFilters.DirectoryName;
            //添加事件句柄
            //当由FileSystemWatcher所指定的路径中的文件或目录的
            //大小、系统属性、最后写时间、最后访问时间或安全权限
            //发生更改时，更改事件就会发生
            watcher.Changed += new FileSystemEventHandler(watch.OnChange);
            //watcher.Changed += new FileSystemEventHandler(watch.ChackChange);
            //由FileSystemWatcher所指定的路径中文件或目录被创建时，创建事件就会发生
            watcher.Created += new FileSystemEventHandler(watch.OnChange);
            //当由FileSystemWatcher所指定的路径中文件或目录被删除时，删除事件就会发生
            watcher.Deleted += new FileSystemEventHandler(watch.OnChange);
            //当由FileSystemWatcher所指定的路径中文件或目录被重命名时，重命名事件就会发生
            watcher.Renamed += new RenamedEventHandler(watch.OnRenamed);
            //开始监视
            watcher.EnableRaisingEvents = true;
        }
    }

    public class Watcher
    {
        /// <summary>
        /// 被修改的文件列表
        /// </summary>
        private static List<string> changeFiles = new List<string>();

        /// <summary>
        /// 上一次文件列表的数量是多少
        /// </summary>
        private int lastChangeFilesCount = 0;

        /// <summary>
        /// 等待时间(默认一秒)
        /// </summary>
        public static int waitTime = 1000;

        /// <summary>
        /// 检查是否完成变更的线程
        /// </summary>
        private Thread changeOver = new Thread(Change);

        /// <summary>
        /// 测试是否是用户数据
        /// </summary>
        private static Regex testUser = new Regex("user");

        /// <summary>
        /// 信号器
        /// </summary>
        //private AutoResetEvent resetEvent = new AutoResetEvent(true);

        /// <summary>
        /// 变动已经完成
        /// </summary>
        //private bool changeOver = true;

        /// <summary>
        /// 向修改文件列表中添加新的文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool AddFile(string fileName)
        {
            lastChangeFilesCount = changeFiles.Count;
            var ret = false;

            if (changeFiles.IndexOf(fileName)==-1)
            {
                ret = true;
                changeFiles.Add(fileName);
            }

            return ret;
        }

        /// <summary>
        /// 在修改文件列表中删去文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool DeleteFile(string fileName)
        {
            lastChangeFilesCount = changeFiles.Count;
            var ret = false;
            var index = changeFiles.IndexOf(fileName);

            if (index>=0)
            {
                changeFiles.RemoveAt(index);
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 检查更改
        /// </summary>
        public void ChackChange(object sender,FileSystemEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(waitTime);
                if (lastChangeFilesCount==changeFiles.Count)
                {
                    Item.Log("更改已经结束");
                    break;
                }
            }
        }

        /// <summary>
        /// 监测修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnChange(object sender,FileSystemEventArgs e)
        {
            if (File.Exists(e.FullPath)&&e.ChangeType!= WatcherChangeTypes.Deleted)
            {
                var file = new FileInfo(e.FullPath);
                AddFile(e.FullPath);
                Console.WriteLine("文件变动");
                if (changeOver.ThreadState== ThreadState.Unstarted||changeOver.ThreadState== ThreadState.WaitSleepJoin)
                {
                    if (changeOver.IsAlive)
                    {
                        changeOver.Abort();
                        changeOver = new Thread(Change);
                        changeOver.Start();
                    }
                    else
                    {
                        changeOver.Start();
                    }
                }
                else if (changeOver.ThreadState==ThreadState.Stopped)
                {
                    changeOver = new Thread(Change);
                    changeOver.Start();
                }
            }
            else
            {
                if (Directory.Exists(e.FullPath))
                {
                    Console.WriteLine("文件夹变动");
                }
                else
                {
                    DeleteFile(e.FullPath);
                    Console.WriteLine("删除变动");
                }
            }
            Console.WriteLine("file:" + e.FullPath + " " + e.ChangeType);
        }

        /// <summary>
        /// 监测修改文件名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnRenamed(object sender,RenamedEventArgs e)
        {
            Console.WriteLine("file: " + e.OldFullPath + " change name to " + e.FullPath);
        }

        public void Save(object sender,FileSystemEventArgs e)
        {
            var time = DateTime.Now.ToFileTimeUtc();
            var file = new FileInfo(e.FullPath);
            //var imgSrc = autoSave.TemporaryPath +file.Name+ "_bf"+time.ToString()+".png";
            //Item.Screenshot(imgSrc);
        }

        /// <summary>
        /// 内容修改
        /// 最开始的时候写成异步函数了
        /// 异步调用 task.delay 会导致
        /// 运行过程中 thread.isalive 参数在调用时候
        /// 除了最开始一次其他都是 false
        /// </summary>
        public static void Change()
        {
            var time = DateTime.Now;
            Item.Log(time.ToString()+"线程开始");
            //等待五秒,如果线程未结束,就开始进行备份
            Thread.Sleep(5 * 1000);
            var time2 = DateTime.Now;
            Item.Log(time2.ToString()+"线程完成任务");
            foreach (var item in changeFiles)
            {
                Item.Log(item);
                if (testUser.IsMatch(item)==false)
                {
                    //需要自动备份的文件对象
                    var file = new FileInfo(item);
                    //截图并保存到临时文件夹
                    Item.Screenshot(autoSave.BuckUpPath.val + "\\" + file.Name + "_bf" + time2.ToFileTimeUtc().ToString() + ".png");
                    //备份文件为在文件夹为zip文件
                    Item.ZipFile(file.FullName, autoSave.ZipPath + file.Name + "_bf" + time2.ToFileTimeUtc().ToString() + ".zip", Item.ZipEnum.GZIP);
                }
            }
        }
    }

    /// <summary>
    /// 是或者否?
    /// </summary>
    public class TrueFalse
    {
        /// <summary>
        /// 保护起来的值
        /// </summary>
        private bool value;

        /// <summary>
        /// 只读的值
        /// </summary>
        public bool Value
        {
            get
            {
                return value;
            }
        }

        public TrueFalse()
        {
            value = false;
        }

        public TrueFalse(bool torf)
        {
            value = torf;
        }

        public void ChangeValue(bool torf)
        {
            value = torf;
        }
    }
}
