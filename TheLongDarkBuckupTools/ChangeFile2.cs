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
using TheLongDarkBuckupTools.GameData;
using TheLongDarkBuckupTools.Helpers;

namespace TheLongDarkBuckupTools
{
    public partial class ChangeFile2 : Form
    {
        /// <summary>
        /// 存档文件路径
        /// </summary>
        public string filePath;

        /// <summary>
        /// 存档文件信息
        /// </summary>
        private FileInfo fileInfo;

        /// <summary>
        /// 基础数据
        /// </summary>
        private string baseDataValue = "";

        /// <summary>
        /// 数据
        /// </summary>
        private SlotData data;

        public ChangeFile2(string filePath)
        {
            InitializeComponent();

            this.filePath = filePath;
            this.Name = "存档修改工具";
        }

        private void ChangeFile2_Load(object sender, EventArgs e)
        {
            this.fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists == false)
            {
                if (MessageBox.Show("文件不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    Close();
                }
            }
            byte[] bytes = File.ReadAllBytes(filePath);
            baseDataValue = EncryptString.DecompressBytesToString(bytes);
            this.data = (SlotData)Item.DeserializeObject<SlotData>(baseDataValue);
            // 显示数据
            ShowData(data);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(baseDataValue))
            {
                MessageBox.Show("存档数据为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.data = (SlotData)Item.DeserializeObject<SlotData>(baseDataValue);
            ShowData(data);
        }

        /// <summary>
        /// 显示存档数据
        /// </summary>
        /// <param name="data"></param>
        private void ShowData(SlotData data)
        {
            textBox1.Text = fileInfo.Name;
            textBox2.Text = data.m_Name ?? data.m_InternalName;
            textBox3.Text = data.m_DisplayName;
            textBox4.Text = data.GetGameMode();
            textBox5.Text = data.m_VersionChangelistNumber == 0 ? data.m_Changelist.ToString(): data.m_VersionChangelistNumber.ToString();
            textBox6.Text = data.m_Timestamp.ToString();
        }
    }
}
