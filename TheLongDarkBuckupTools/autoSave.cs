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
using System.Runtime.InteropServices;
using TheLongDarkBuckupTools.Helpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using System.Diagnostics;

namespace TheLongDarkBuckupTools
{
    public partial class autoSave : Form
    {
        /// <summary>
        /// 剧情模式存档位置
        /// </summary>
        private Value gameStorySavePath;

        /// <summary>
        /// 生存模式存档位置
        /// </summary>
        private Value gameSurvivalSavePath;

        /// <summary>
        /// 剧情模式备份所在位置
        /// </summary>
        private Value storyBuckUpPath;

        /// <summary>
        /// 剧情模式备份所在的位置
        /// </summary>
        public static Value StoryBuckUpPath;

        /// <summary>
        /// 生存模式备份所在位置
        /// </summary>
        private Value survivalBuckUpPath;

        /// <summary>
        /// 生存模式备份所在的位置
        /// </summary>
        public static Value SurvivalBuckUpPath;

        /// <summary>
        /// 游戏模式
        /// </summary>
        public PlayType playType;

        /// <summary>
        /// 主页面
        /// </summary>
        private Form form;

        /// <summary>
        /// 文件夹监控对象
        /// 老版本检测(生存模式检测)
        /// </summary>
        public static FileSystemWatcher watcherStory;

        /// <summary>
        /// 文件夹监控对象
        /// 新版生存模式检测
        /// </summary>
        public static FileSystemWatcher watcherSurvival;

        /// <summary>
        /// 临时文件夹路径
        /// </summary>
        //public static string TemporaryPath;//= AppDomain.CurrentDomain.BaseDirectory + @"temporary\";

        /// <summary>
        /// zip文件保存路径
        /// </summary>
        public static string ZipPath;

        /// <summary>
        /// 建立检查
        /// </summary>
        public Watcher watch;

        /// <summary>
        /// 快速保存对象
        /// </summary>
        public Value quickSave;

        /// <summary>
        /// 是否是Survival模式
        /// </summary>
        private Regex isSurvival = new Regex("Survival");

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="form"></param>
        /// <param name="gameSavePath"></param>
        /// <param name="buckUpPath"></param>
        public autoSave(Form form,Value gameSavePath,Value buckUpPath)
        {
            this.form = form;
            this.gameStorySavePath = gameSavePath;
            this.gameSurvivalSavePath = gameSavePath + @"\Survival\";
            this.storyBuckUpPath = buckUpPath;
            this.survivalBuckUpPath = buckUpPath + @"\Survival\";
            StoryBuckUpPath = storyBuckUpPath;
            SurvivalBuckUpPath = survivalBuckUpPath;
            ZipPath = buckUpPath.val + @"\zippath\";
            InitializeComponent();
            if (Directory.Exists(ZipPath)==false)
            {
                Directory.CreateDirectory(ZipPath);
                var dir = new DirectoryInfo(ZipPath);
                dir.Attributes = FileAttributes.Hidden;
            }
            else
            {
                var dir = new DirectoryInfo(ZipPath);
                dir.Attributes = FileAttributes.Hidden;
            }
            if (Directory.Exists(survivalBuckUpPath.val)==false)
            {
                Directory.CreateDirectory(survivalBuckUpPath.val);
            }
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="form"></param>
        /// <param name="gameSavePath"></param>
        /// <param name="buckUpPath"></param>
        public autoSave(Form form,Value gameSavePath,Value buckUpPath,Value quickSave):this(form,gameSavePath,buckUpPath)
        {
            this.quickSave = quickSave;
            //开始键盘监视
            Start();
            //OnKeyUpEvent += new KeyEventHandler(AllKeyUp);
            label4.Text = "快捷键保存有错误,暂时屏蔽!";//自动保存还没有做好//做好了
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Item.OpenFolder(gameStorySavePath.val);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Item.OpenFolder(storyBuckUpPath.val);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void autoSave_UnLoad(object sender,EventArgs e)
        {
            form.Opacity = 1;
            //结束文件夹监视
            watcherStory.EnableRaisingEvents = false;
            watcherSurvival.EnableRaisingEvents = false;
            watch = null;
            //卸载键盘监视
            Stop();
            //进行强制回收内存
            GC.Collect();
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
                if (isSurvival.IsMatch(path)==false)
                {
                    Item.Save(path, storyBuckUpPath.val, times);
                }
                else
                {
                    Item.Save(path, survivalBuckUpPath.val, times);
                }
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
            //MessageBox.Show(System.Diagnostics.Process.GetProcessesByName("tld").Length.ToString(), "测试", MessageBoxButtons.OK);
            
            if (watch == null)
            {
                watch = new Watcher();
            }
            if (watcherStory==null)
            {
                if (!Directory.Exists(gameStorySavePath.val))
                {
                    throw new Exception("存档文件夹不存在！");
                }
                watcherStory = new FileSystemWatcher();
                watcherStory.Path = gameStorySavePath.val;
                /*监视LastAcceSS和LastWrite时间的更改以及文件或目录的重命名*/
                watcherStory.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite |
                      NotifyFilters.FileName | NotifyFilters.DirectoryName;
                //添加事件句柄
                //当由FileSystemWatcher所指定的路径中的文件或目录的
                //大小、系统属性、最后写时间、最后访问时间或安全权限
                //发生更改时，更改事件就会发生
                watcherStory.Changed += new FileSystemEventHandler(watch.StoryOnChange);
                //watcher.Changed += new FileSystemEventHandler(watch.ChackChange);
                //由FileSystemWatcher所指定的路径中文件或目录被创建时，创建事件就会发生
                watcherStory.Created += new FileSystemEventHandler(watch.StoryOnChange);
                //当由FileSystemWatcher所指定的路径中文件或目录被删除时，删除事件就会发生
                watcherStory.Deleted += new FileSystemEventHandler(watch.StoryOnChange);
                //当由FileSystemWatcher所指定的路径中文件或目录被重命名时，重命名事件就会发生
                watcherStory.Renamed += new RenamedEventHandler(watch.OnRenamed);
            }
            if (watcherSurvival==null)
            {
                if (!Directory.Exists(gameSurvivalSavePath.val))
                {
                    throw new Exception("存档文件夹不存在！");
                }
                watcherSurvival = new FileSystemWatcher();
                watcherSurvival.Path = gameSurvivalSavePath.val;
                /*监视LastAcceSS和LastWrite时间的更改以及文件或目录的重命名*/
                watcherSurvival.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite |
                      NotifyFilters.FileName | NotifyFilters.DirectoryName;
                //添加事件句柄
                //当由FileSystemWatcher所指定的路径中的文件或目录的
                //大小、系统属性、最后写时间、最后访问时间或安全权限
                //发生更改时，更改事件就会发生
                watcherSurvival.Changed += new FileSystemEventHandler(watch.SurvivalOnChange);
                //watcher.Changed += new FileSystemEventHandler(watch.ChackChange);
                //由FileSystemWatcher所指定的路径中文件或目录被创建时，创建事件就会发生
                watcherSurvival.Created += new FileSystemEventHandler(watch.SurvivalOnChange);
                //当由FileSystemWatcher所指定的路径中文件或目录被删除时，删除事件就会发生
                watcherSurvival.Deleted += new FileSystemEventHandler(watch.SurvivalOnChange);
                //当由FileSystemWatcher所指定的路径中文件或目录被重命名时，重命名事件就会发生
                watcherSurvival.Renamed += new RenamedEventHandler(watch.OnRenamed);
            }
            //开始监视
            watcherStory.EnableRaisingEvents = true;
            watcherSurvival.EnableRaisingEvents = true;
        }

        #region 键盘监测

        #region 全局常量声明
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;
        #endregion

        #region 全局的事件
        public event KeyEventHandler OnKeyDownEvent;
        public event KeyEventHandler OnKeyUpEvent;
        public event KeyPressEventHandler OnKeyPressEvent;
        #endregion

        #region 鼠标常量
        /// <summary>
        /// 键盘钩子句柄
        /// </summary>
        static int hKeyboardHook = 0;
        /// <summary>
        /// 类型  定义在winuser.h
        /// </summary>
        public const int WH_KEYBOARD_LL = 13;
        #endregion

        #region 有关键盘
        /// <summary>
        /// 声明键盘钩子事件类型.
        /// </summary>
        HookProc KeyboardHookProcedure;
        /// <summary>
        /// 声明键盘钩子的封送结构类型
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            /// <summary>
            /// 表示一个在1到254间的虚似键盘码
            /// </summary>
            public int vkCode;
            /// <summary>
            /// 表示硬件扫描码
            /// </summary>
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        #endregion

        #region api
        /// <summary>
        /// 装置钩子的函数
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="lpfn"></param>
        /// <param name="hInstance"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        /// <summary>
        /// 卸下钩子的函数
        /// </summary>
        /// <param name="idHook"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        /// <summary>
        /// 下一个钩挂的函数
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);
        /// <summary>
        /// 转化成ASCII码
        /// </summary>
        /// <param name="uVirtKey"></param>
        /// <param name="uScanCode"></param>
        /// <param name="lpbKeyState"></param>
        /// <param name="lpwTransKey"></param>
        /// <param name="fuState"></param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);
        /// <summary>
        /// 获取键盘状态
        /// </summary>
        /// <param name="pbKeyState"></param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);
        /// <summary>
        /// 获取模块句柄
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        /// <summary>
        /// 钩子定义声明器
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        /// <summary>
        /// 先前按下的键
        /// </summary>
        public List<Keys> preKeys = new List<Keys>();

        #endregion

        #region 键盘钩子安装与卸载处理

        /// <summary>
        /// 安装键盘钩子
        /// </summary>
        public void Start()
        {
            Console.WriteLine("开始安装钩子");
            //安装键盘钩子
            if (hKeyboardHook == 0)
            {
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                //hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                System.Diagnostics.Process curProcess = System.Diagnostics.Process.GetCurrentProcess();
                System.Diagnostics.ProcessModule curModule = curProcess.MainModule;
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, GetModuleHandle(curModule.ModuleName), 0);
                if (hKeyboardHook == 0)
                {
                    Stop();
                    throw new Exception("安装键盘钩子");
                }
            }
        }

        /// <summary>
        /// 卸载键盘钩子
        /// </summary>
        public void Stop()
        {
            bool retKeyboard = true;
            if (hKeyboardHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
            }
            //如果卸下钩子失败
            if (!(retKeyboard)) throw new Exception("卸下钩子失败");
            Console.WriteLine("键盘钩子已经卸下");
        }
        #endregion

        #region 处理方法
        /// <summary>
        /// 键盘钩子程序
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            //Console.WriteLine("事件激发");
            //Console.WriteLine(wParam);
            //Console.WriteLine(preKeys.ToArray().Length);
            if ((nCode >= 0) && (OnKeyDownEvent != null || OnKeyUpEvent != null || OnKeyPressEvent != null))
            {
                //Console.WriteLine(1);
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                //当有OnKeyDownEvent 或 OnKeyPressEvent不为null时,ctrl alt shift keyup时 preKeys
                //中的对应的键增加                  
                if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    //Console.WriteLine(keyData);
                    if (IsCtrlAltShiftKeys(keyData) && preKeys.IndexOf(keyData) == -1)
                    {
                        preKeys.Add(keyData);
                    }
                }
                //引发OnKeyDownEvent
                if (OnKeyDownEvent != null && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));
                    OnKeyDownEvent(this, e);
                }
                //引发OnKeyPressEvent
                if (OnKeyPressEvent != null && wParam == WM_KEYDOWN)
                {
                    byte[] keyState = new byte[256];
                    GetKeyboardState(keyState);
                    byte[] inBuffer = new byte[2];
                    if (ToAscii(MyKeyboardHookStruct.vkCode,
                    MyKeyboardHookStruct.scanCode,
                    keyState,
                    inBuffer,
                    MyKeyboardHookStruct.flags) == 1)
                    {
                        KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                        OnKeyPressEvent(this, e);
                    }
                }
                //当有OnKeyDownEvent 或 OnKeyPressEvent不为null时,ctrl alt shift keyup时 preKeys
                //中的对应的键删除
                if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    if (IsCtrlAltShiftKeys(keyData))
                    {
                        for (int i = preKeys.Count - 1; i >= 0; i--)
                        {
                            if (preKeys[i] == keyData)
                            {
                                preKeys.RemoveAt(i);
                            }
                        }
                    }
                }
                //引发OnKeyUpEvent
                if (OnKeyUpEvent != null && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));
                    OnKeyUpEvent(this, e);
                }
            }
            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }
        /// <summary>
        /// 获取落下的按键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private Keys GetDownKeys(Keys key)
        {
            Keys rtnKey = Keys.None;
            foreach (Keys keyTemp in preKeys)
            {
                switch (keyTemp)
                {
                    case Keys.LControlKey:
                    case Keys.RControlKey:
                        rtnKey = rtnKey | Keys.Control;
                        break;
                    case Keys.LMenu:
                    case Keys.RMenu:
                        rtnKey = rtnKey | Keys.Alt;
                        break;
                    case Keys.LShiftKey:
                    case Keys.RShiftKey:
                        rtnKey = rtnKey | Keys.Shift;
                        break;
                    default:
                        break;
                }
            }
            rtnKey = rtnKey | key;
            return rtnKey;
        }
        /// <summary>
        /// 是否是其他（ctrl，shift，alt）按键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private Boolean IsCtrlAltShiftKeys(Keys key)
        {
            Console.WriteLine(key);
            switch (key)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                case Keys.LMenu:
                case Keys.RMenu:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    return true;
                default:
                    return false;
            }
        }
        #endregion

        #region 键盘事件

        /// <summary>
        /// 全部按键弹起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AllKeyUp(object sender, KeyEventArgs e)
        {
            var time = DateTime.Now;
            Console.WriteLine(Item.GetTheNewFile(gameStorySavePath.val));
            //文件名
            var fileName = string.IsNullOrEmpty(quickSave.val)?gameStorySavePath.val+"\\"+Item.GetTheNewFile(gameStorySavePath.val):quickSave.val;
            //Console.WriteLine(e.KeyCode);
            if (e.KeyCode==Main.QuickSave&&System.Diagnostics.Process.GetProcessesByName("tld").Length>0)
            {
                //需要自动备份的文件对象
                var file = new FileInfo(fileName);
                //截图并保存到临时文件夹
                //Item.Screenshot(BuckUpPath.val + "\\" + file.Name + "_bf" + time.ToFileTimeUtc().ToString() + ".png");
                Item.Screenshot(StoryBuckUpPath.val + "\\" + file.Name + "_bf" + time.ToFileTimeUtc().ToString() + ".png",true);
                //程序运行到这里可以保存之前的截图文件了
                //img.Save(autoSave.BuckUpPath.val + "\\" + file.Name + "_bf" + time2.ToFileTimeUtc().ToString() + ".png");
                //备份文件为在文件夹为zip文件
                Item.ZipFile(file.FullName, ZipPath + file.Name + "_bf" + time.ToFileTimeUtc().ToString() + ".gz");
            }
            else if (e.KeyCode==Main.QuickSave&&System.Diagnostics.Process.GetProcessesByName("tld").Length==0)
            {
                Item.Save(fileName, StoryBuckUpPath.val, time.ToFileTimeUtc());
            }
        }

        #endregion

        #endregion

        #region 添加启动功能
        private void label2_Click(object sender, EventArgs e)
        {
            var starWay = Program.PublicData["starWay"].ToString();
            if (string.IsNullOrEmpty(starWay) == false)
            {
                Item.OpenOnWindows(starWay);
            }
            else
            {
                MessageBox.Show("请在程序主页 关于 界面设定游戏启动位置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.Text = "启动游戏";
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "漫漫长夜";
        }

        #endregion
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
        private Thread changeStoryOver = new Thread(StoryChange);

        /// <summary>
        /// 检查是否完成变更的线程
        /// </summary>
        private Thread changeSurvivalOver = new Thread(SurvivalChange);

        /// <summary>
        /// 需要排除的文件
        /// </summary>
        private static List<Regex> Exclude = new List<Regex>
        {
            /// <summary>
            /// 测试是否是用户数据
            /// </summary>
            new Regex("user"),
            /// <summary>
            /// 测试是否是steam文件
            /// </summary>
            new Regex(@"steam"),
            /// <summary>
            /// 测试是否是profile文件
            /// </summary>
            new Regex(@"profile"),
            /// <summary>
            /// 测试是否是拍立得照片数据
            /// </summary>
            new Regex(@"photo"),
        };

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
        public void StoryOnChange(object sender,FileSystemEventArgs e)
        {
            if (File.Exists(e.FullPath)&&e.ChangeType!= WatcherChangeTypes.Deleted)
            {
                var file = new FileInfo(e.FullPath);
                AddFile(e.FullPath);
                Console.WriteLine("文件变动");
                if (changeStoryOver.ThreadState== ThreadState.Unstarted||changeStoryOver.ThreadState== ThreadState.WaitSleepJoin)
                {
                    if (changeStoryOver.IsAlive)
                    {
                        changeStoryOver.Abort();
                        changeStoryOver = new Thread(StoryChange);
                        changeStoryOver.Start();
                    }
                    else
                    {
                        changeStoryOver.Start();
                    }
                }
                else if (changeStoryOver.ThreadState==ThreadState.Stopped)
                {
                    changeStoryOver = new Thread(StoryChange);
                    changeStoryOver.Start();
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
        /// 监测修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SurvivalOnChange(object sender,FileSystemEventArgs e)
        {
            if (File.Exists(e.FullPath)&&e.ChangeType!= WatcherChangeTypes.Deleted)
            {
                var file = new FileInfo(e.FullPath);
                AddFile(e.FullPath);
                Console.WriteLine("文件变动");
                if (changeSurvivalOver.ThreadState== ThreadState.Unstarted||changeSurvivalOver.ThreadState== ThreadState.WaitSleepJoin)
                {
                    if (changeSurvivalOver.IsAlive)
                    {
                        changeSurvivalOver.Abort();
                        changeSurvivalOver = new Thread(SurvivalChange);
                        changeSurvivalOver.Start();
                    }
                    else
                    {
                        changeSurvivalOver.Start();
                    }
                }
                else if (changeSurvivalOver.ThreadState==ThreadState.Stopped)
                {
                    changeSurvivalOver = new Thread(SurvivalChange);
                    changeSurvivalOver.Start();
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
        public static void StoryChange()
        {
            var time = DateTime.Now;
            Item.Log(time.ToString()+"线程开始");
            //获取截图但是暂不保存
            Image img = Item.Screenshot("", false);
            //等待五秒,如果线程未结束,就开始进行备份
            Thread.Sleep(5 * 1000);
            var time2 = DateTime.Now;
            Item.Log(time2.ToString()+"线程完成任务");
            try
            {
                foreach (var item in changeFiles)
                {
                    Item.Log(item);
                    if (System.Diagnostics.Process.GetProcessesByName("tld").Length > 0)
                    {
                        if (Exclude.TestFile(item))
                        {
                            //需要自动备份的文件对象
                            var file = new FileInfo(item);
                            //截图并保存到临时文件夹
                            //Item.Screenshot(autoSave.BuckUpPath.val + "\\" + file.Name + "_bf" + time2.ToFileTimeUtc().ToString() + ".png");
                            //程序运行到这里可以保存之前的截图文件了
                            img.Save(autoSave.StoryBuckUpPath.val + "\\" + file.Name + "_bf" + time2.ToFileTimeUtc().ToString() + ".png");
                            //备份文件为在文件夹为zip文件
                            Item.ZipFile(file.FullName, autoSave.ZipPath + file.Name + "_bf" + time2.ToFileTimeUtc().ToString() + ".gz");
                        }
                    }
                    else
                    {
                        if (Exclude.TestFile(item))
                        {
                            //游戏进程没运行则直接备份不截图
                            Item.Save(item, autoSave.StoryBuckUpPath.val, time2.ToFileTimeUtc());
                        }
                    }
                }
                //之前没有重置导致出备份出错
                changeFiles = new List<string>();
            }
            catch (Exception err)
            {
                Item.Log(err);
            }
        }
        
        /// <summary>
        /// 内容修改
        /// 最开始的时候写成异步函数了
        /// 异步调用 task.delay 会导致
        /// 运行过程中 thread.isalive 参数在调用时候
        /// 除了最开始一次其他都是 false
        /// </summary>
        public static void SurvivalChange()
        {
            var time = DateTime.Now;
            Item.Log(time.ToString()+"线程开始");
            //获取截图但是暂不保存
            Image img = Item.Screenshot("", false);
            //等待五秒,如果线程未结束,就开始进行备份
            Thread.Sleep(5 * 1000);
            var time2 = DateTime.Now;
            Item.Log(time2.ToString()+"线程完成任务");
            try
            {
                foreach (var item in changeFiles)
                {
                    Item.Log(item);
                    if (System.Diagnostics.Process.GetProcessesByName("tld").Length > 0)
                    {
                        if (Exclude.TestFile(item))
                        {
                            //需要自动备份的文件对象
                            var file = new FileInfo(item);
                            //截图并保存到临时文件夹
                            //Item.Screenshot(autoSave.BuckUpPath.val + "\\" + file.Name + "_bf" + time2.ToFileTimeUtc().ToString() + ".png");
                            //程序运行到这里可以保存之前的截图文件了
                            img.Save(autoSave.SurvivalBuckUpPath.val + "\\" + file.Name + "_bf" + time2.ToFileTimeUtc().ToString() + ".png");
                            //备份文件为在文件夹为zip文件
                            Item.ZipFile(file.FullName, autoSave.ZipPath + file.Name + "_bf" + time2.ToFileTimeUtc().ToString() + ".gz");
                        }
                    }
                    else
                    {
                        if (Exclude.TestFile(item))
                        {
                            //游戏进程没运行则直接备份不截图
                            Item.Save(item, autoSave.SurvivalBuckUpPath.val, time2.ToFileTimeUtc());
                        }
                    }
                }
                //之前没有重置导致出备份出错
                changeFiles = new List<string>();
            }
            catch (Exception err)
            {
                Item.Log(err);
            }
        }
        
        /// <summary>
        /// 回收机制
        /// </summary>
        //~Watcher()
        //{
        //    try
        //    {
        //        changeOver.Start();
        //        changeOver.Abort();
        //    }
        //    catch (Exception err)
        //    {
        //        Item.Log(err);
        //    }
        //    changeOver.DisableComObjectEagerCleanup();
        //}
    }

    public static class WatcherAdd
    {
        /// <summary>
        /// 测试文件
        /// true:需要的文件
        /// false:不需要的文件
        /// </summary>
        /// <param name="regices"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool TestFile(this List<Regex> regices, string fileName)
        {
            var ret = true;

            for (int i = 0; i < regices.Count; i++)
            {
                if (regices[i].IsMatch(fileName))
                {
                    return false;
                }
            }

            return ret;
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
