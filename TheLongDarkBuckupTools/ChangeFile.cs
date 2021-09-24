using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheLongDarkBuckupTools.GameData;
using TheLongDarkBuckupTools.Helpers;

namespace TheLongDarkBuckupTools
{
    public partial class ChangeFile : Form
    {
        /// <summary>
        /// 存档文件所在位置
        /// </summary>
        public Value gameSavePath;

        /// <summary>
        /// 备份文件所在位置
        /// </summary>
        public Value buckUpPath;

        /// <summary>
        /// 启动此页面的页面
        /// </summary>
        public Form form;

        /// <summary>
        /// 是否在备份界面
        /// </summary>
        public bool isBuck;

        /// <summary>
        /// 当前选择的文件
        /// </summary>
        public FileInfo nowSelect;

        /// <summary>
        /// 临时文件夹
        /// </summary>
        public string TempFolder;

        /// <summary>
        /// zip文件保存路径
        /// </summary>
        public string ZipPath;

        #region 功能模块

        /// <summary>
        /// 选中文件
        /// </summary>
        /// <param name="file"></param>
        private void SelectFile(FileInfo file)
        {
            nowSelect = file;
            var data = new SlotData();
            if (file.Extension == "")
            {
                pictureBox1.Visible = false;
                data = (SlotData)Item.DeserializeObject<SlotData>(EncryptString.DecompressBytesToString(File.ReadAllBytes(file.FullName)));
                Console.WriteLine(data.m_Dict["screenshot"].Length);
                var img = ((Screenshot)Item.DeserializeObject<Screenshot>(EncryptString.DecompressBytesToString(data.m_Dict["screenshot"]))).ToImage();
                pictureBox1.Visible = true;
                pictureBox1.Image = img;
            }
            else if (file.Extension == ".png")
            {
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
            var bootData = Item.DeserializeObject<BootData>(EncryptString.DecompressBytesToString(data.m_Dict["boot"]));
            var proxy = Item.DeserializeObject<GlobalDataFormat>(EncryptString.DecompressBytesToString(data.m_Dict["global"]));
            //Console.WriteLine(EncryptString.DecompressBytesToString(data.m_Dict["global"]));
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
            var getTime = new Regex(@"_bf(\d+)");
            var match = getTime.Match(file.Name);
        }

        /// <summary>
        /// 检查isBuck参数
        /// </summary>
        private void ChackIsBuck()
        {
            listBox1.Items.Clear();
            var dir = isBuck ? new DirectoryInfo(buckUpPath.val) : new DirectoryInfo(gameSavePath.val);
            var files = dir.GetFiles();
            if (files.Length <= 0)
            {
                MessageBox.Show(isBuck ? "备份文件夹没有任何文件" : "存档文件夹没有任何文件", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Close();
            }
            else
            {
                listBox1.Items.AddRange(RemoveUsers(files));
            }
            if (isBuck)
            {
                Text = "修改文件页面-备份文件夹内文件";
                label3.Text = "页面状态:修改备份";
            }
            else
            {
                Text = "修改文件页面-存档文件夹内文件";
                label3.Text = "页面状态:修改存档";
            }
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



        #endregion

        public ChangeFile(Form form,Value gameSavePath,Value buckUpPath)
        {
            InitializeComponent();
            this.form = form;
            this.gameSavePath = gameSavePath;
            this.buckUpPath = buckUpPath;
        }

        public ChangeFile(Form form,Value gameSavePath,Value buckUpPath,bool isBuck)
        {
            InitializeComponent();
            this.form = form;
            this.gameSavePath = gameSavePath;
            this.buckUpPath = buckUpPath;
            this.isBuck = isBuck;
        }

        private void ChangeFile_Load(object sender, EventArgs e)
        {
            ChackIsBuck();
            Main.IsElseForm = true;
            ZipPath = buckUpPath.val + @"\zippath\";
            TempFolder = AppDomain.CurrentDomain.BaseDirectory + @"\temp\";
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
        }

        private void ChangeFile_UnLoad(object sender, EventArgs e)
        {
            form.Opacity = 1;
            Main.IsElseForm = false;
            //删除临时文件夹
            if (Directory.Exists(TempFolder))
            {
                Directory.Delete(TempFolder, true);
            }
            //进行一次强制垃圾回收,减小占用内存.
            GC.Collect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isBuck = !isBuck;
            ChackIsBuck();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectFile((FileInfo)listBox1.SelectedItem);
        }



    }
}
