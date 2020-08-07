using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using TheLongDarkBuckupTools.GameData;
using TheLongDarkBuckupTools.Helpers;

namespace TheLongDarkBuckupTools
{
    public partial class ShowFile : Form
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
        /// zip文件保存路径
        /// </summary>
        public static string ZipPath;

        /// <summary>
        /// 是否是读取备份
        /// </summary>
        public bool isRead;

        /// <summary>
        /// 文件夹中文件数量
        /// </summary>
        public int Count;

        /// <summary>
        /// 当前选择的文件
        /// </summary>
        public FileInfo NowSelect;

        /// <summary>
        /// 临时文件夹
        /// </summary>
        public string TempFolder;

        /// <summary>
        /// 文件夹内文件信息文件
        /// </summary>
        public string FolderInfoIni;

        /// <summary>
        /// 所有的文件的简单信息
        /// </summary>
        public string AllFileInfo = "";

        /// <summary>
        /// 读取文件线程
        /// </summary>
        public Thread readFile;

        /// <summary>
        /// 显示并选取文件页面构造函数
        /// </summary>
        /// <param name="isRead">是否是读取备份</param>
        /// <param name="gameSavePath">存档所在位置</param>
        /// <param name="buckUpPath">备份文件位置</param>
        public ShowFile(bool isRead,Value gameSavePath,Value buckUpPath)
        {
            this.isRead = isRead;
            this.gameSavePath = gameSavePath;
            this.buckUpPath = buckUpPath;
            ZipPath = buckUpPath.val + @"\zippath\";
            TempFolder = AppDomain.CurrentDomain.BaseDirectory + @"\temp\";
            FolderInfoIni = AppDomain.CurrentDomain.BaseDirectory + @"\FolderInfo.ini";
            Directory.CreateDirectory(TempFolder);
            if (Directory.Exists(ZipPath) == false)
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
            readFile = new Thread(new ThreadStart(FolderInfoIniInit));
            //readFile.Start();
            InitializeComponent();
        }

        private void ShowFile_Load(object sender, EventArgs e)
        {
            ChackIsRead();
            Main.IsElseForm = true;
            label4.Visible = false;
        }

        private void ShowFile_UnLoad(object sender, EventArgs e)
        {
            Main.IsElseForm = false;
            //删除临时文件夹
            if (Directory.Exists(TempFolder))
            {
                Directory.Delete(TempFolder, true);
            }
            //进行一次强制垃圾回收,减小占用内存.
            GC.Collect();
            //保存所有的文件信息
            var theFile = new FileInfo(FolderInfoIni).OpenWrite();
            var sw = new StreamWriter(theFile);
            sw.Write(AllFileInfo);
            sw.Close();
            theFile.Close();
            Console.WriteLine(AllFileInfo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isRead = !isRead;
            ChackIsRead();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(listBox1.SelectedItem.ToString());
            SelectFile((FileInfo)listBox1.SelectedItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("未选择文件!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var file = (FileInfo)listBox1.SelectedItem;
            if (isRead)
            {
                Item.ReadSave(file, gameSavePath.val);
            }
            else
            {
                Console.WriteLine(DateTime.Now);
                Item.Save(file, buckUpPath.val, DateTime.Now.ToFileTimeUtc());
            }
        }

        /// <summary>
        /// 检查isread参数
        /// </summary>
        private void ChackIsRead()
        {
            listBox1.Items.Clear();
            Text = isRead ? "文件查看器-读取备份状态" : "文件查看器-备份存档状态";
            label3.Text = isRead ? "页面状态:读取备份" : "页面状态:备份存档";
            var dir = isRead ? new DirectoryInfo(buckUpPath.val) : new DirectoryInfo(gameSavePath.val);
            var files = dir.GetFiles();
            if (files.Length <= 0)
            {
                MessageBox.Show(isRead ? "备份文件夹没有任何文件" : "存档文件夹没有任何文件", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Close();
            }
            else
            {
                listBox1.Items.AddRange(RemoveUsers(files));
            }
            button3.Text = isRead ? "读取该备份" : "备份该存档";
        }

        /// <summary>
        /// 选中文件
        /// </summary>
        /// <param name="file"></param>
        private void SelectFile(FileInfo file)
        {
            NowSelect = file;
            var data = new SlotData();
            if (file.Extension == "")
            {
                label4.Visible = true;
                pictureBox1.Visible = false;
                data = Item.DeserializeObject<SlotData>(EncryptString.DecompressBytesToString(File.ReadAllBytes(file.FullName)));
            }
            else if (file.Extension == ".png")
            {
                label4.Visible = false;
                pictureBox1.Visible = true;
                pictureBox1.ImageLocation = file.FullName;
                var zipFile = ZipPath + file.NameWithoutExtension() + ".gz";
                Console.WriteLine("file:" + zipFile);
                if (File.Exists(zipFile) == false)
                {
                    MessageBox.Show("程序未找到文件:" + zipFile + "!\r\n请检查图片同名压缩包是否删除\r\n或者是否删除zippath文件夹", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    Item.UnZipFile(zipFile, TempFolder + file.NameWithoutExtension());
                    data = Item.DeserializeObject<SlotData>(EncryptString.DecompressBytesToString(File.ReadAllBytes(TempFolder + file.NameWithoutExtension())));
                }
            }
            ShowData(data);
        }

        /// <summary>
        /// 移除用户信息文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private FileInfo[] RemoveUsers(FileInfo[] files)
        {
            var rets = new List<FileInfo>();
            var isuser = new Regex(@"user");
            foreach (var item in files)
            {
                if (isuser.IsMatch(item.Name))
                {
                    continue;
                }
                else
                {
                    rets.Add(item);
                }
            }
            return rets.ToArray();
        }

        /// <summary>
        /// 显示存档信息
        /// </summary>
        /// <param name="data"></param>
        private void ShowData(SlotData data)
        {
            var file = (FileInfo)listBox1.SelectedItem;
            textBox1.Text = file.Name;
            textBox2.Text = data.m_Name;
            textBox3.Text = data.m_DisplayName;
            textBox4.Text = data.GetGameMode();
            textBox5.Text = data.m_VersionChangelistNumber.ToString();
            textBox6.Text = data.m_Timestamp.ToString();
            if (isRead)
            {
                var getTime = new Regex(@"_bf(\d+)");
                var match = getTime.Match(file.Name);
                //Console.WriteLine(time.Groups[1]);
                textBox7.Text = DateTime.FromFileTime(long.Parse(match.Groups[1].ToString())).ToString();
            }
            else
            {
                textBox7.Text = "无";
            }
        }

        /// <summary>
        /// 文件信息文件初始化
        /// </summary>
        private void FolderInfoIniInit()
        {
            if (File.Exists(FolderInfoIni) == false)
            {
                File.Create(FolderInfoIni);
                var saveDatas = "";
                var buckDatas = "";
                var saveFiles = RemoveUsers(new DirectoryInfo(gameSavePath.val).GetFiles());
                var buckFiles = RemoveUsers(new DirectoryInfo(buckUpPath.val).GetFiles());

                foreach (var file in saveFiles)
                {
                    SlotData data = new SlotData();
                    if (file.Extension == "")
                    {
                        data = Item.DeserializeObject<SlotData>(EncryptString.DecompressBytesToString(File.ReadAllBytes(file.FullName)));
                    }
                    else if (file.Extension == ".png")
                    {
                        var zipFile = ZipPath + file.NameWithoutExtension() + ".gz";
                        Console.WriteLine("file:" + zipFile);
                        if (File.Exists(zipFile) == false)
                        {
                            //MessageBox.Show("程序未找到文件:" + zipFile + "!\r\n请检查图片同名压缩包是否删除\r\n或者是否删除zippath文件夹", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //return;
                            //data = new SlotData();
                        }
                        else
                        {
                            Item.UnZipFile(zipFile, TempFolder + file.NameWithoutExtension());
                            data = Item.DeserializeObject<SlotData>(EncryptString.DecompressBytesToString(File.ReadAllBytes(TempFolder + file.NameWithoutExtension())));
                        }
                    }
                    saveDatas += data.ToBigDatas(file, false).ToString(true);
                }
                foreach (var file in buckFiles)
                {
                    SlotData data = new SlotData();
                    if (file.Extension == "")
                    {
                        data = Item.DeserializeObject<SlotData>(EncryptString.DecompressBytesToString(File.ReadAllBytes(file.FullName)));
                    }
                    else if (file.Extension == ".png")
                    {
                        var zipFile = ZipPath + file.NameWithoutExtension() + ".gz";
                        Console.WriteLine("file:" + zipFile);
                        if (File.Exists(zipFile) == false)
                        {
                            //MessageBox.Show("程序未找到文件:" + zipFile + "!\r\n请检查图片同名压缩包是否删除\r\n或者是否删除zippath文件夹", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //return;
                            //data = new SlotData();
                        }
                        else
                        {
                            Item.UnZipFile(zipFile, TempFolder + file.NameWithoutExtension());
                            data = Item.DeserializeObject<SlotData>(EncryptString.DecompressBytesToString(File.ReadAllBytes(TempFolder + file.NameWithoutExtension())));
                        }
                    }
                    buckDatas += data.ToBigDatas(file, true).ToString(true);
                }
                AllFileInfo = new BigData("saveDatas", saveDatas).ToString() +
                new BigData("buckDatas", buckDatas).ToString();

                var theFile = new FileInfo(FolderInfoIni).OpenWrite();
                var sw = new StreamWriter(theFile);
                sw.Write(AllFileInfo);
                sw.Close();
                theFile.Close();
            }
            else
            {
                var theFile = new FileInfo(FolderInfoIni).OpenRead();
                var sr = new StreamReader(theFile);
                AllFileInfo = sr.ReadToEnd();
                sr.Close();
                theFile.Close();
            }
        }
    }
}
