using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheLongDarkBuckupTools.PhotoData;
using TheLongDarkBuckupTools.Helpers;
using TheLongDarkBuckupTools.Class;

namespace TheLongDarkBuckupTools
{
    public partial class PhotoViewer : Form
    {
        /// <summary>
        /// 图片数据
        /// </summary>
        public List<ImageData> imageDatas = new List<ImageData>();

        /// <summary>
        /// 图片缓存
        /// </summary>
        public DictionaryEX<int, Image> imageCache = new DictionaryEX<int, Image>();

        /// <summary>
        /// 图片指针
        /// </summary>
        private int photoIndex = 0;

        /// <summary>
        /// 图片指针
        /// </summary>
        public int PhotoIndex
        {
            get
            {
                return photoIndex;
            }
            set
            {
                if (value<0)
                {
                    photoIndex = 0;
                }
                else if (value>=imageDatas.Count)
                {
                    photoIndex = imageDatas.Count - 1;
                }
                else
                {
                    photoIndex = value;
                }
            }
        }

        /// <summary>
        /// 当前图片
        /// </summary>
        public Image CurrentImage
        {
            get
            {
                if (imageDatas.Count ==0)
                {
                    return null;
                }
                else
                {
                    var img = imageCache[PhotoIndex];
                    if (img is null)
                    {
                        var imgData = imageDatas[PhotoIndex];
                        var imgBase64 = imgData.m_JpegData;
                        img = (Image)Item.GetImageFromBase64(imgBase64);
                        imageCache[PhotoIndex] = img;
                    }
                    return img;
                }
            }
        }

        public PhotoViewer(List<ImageData> imageDatas)
        {
            InitializeComponent();
            
            this.imageDatas = imageDatas;
        }

        private void PhotoViewer_Load(object sender, EventArgs e)
        {
            update_image();
        }

        /// <summary>
        /// 更新显示图片
        /// </summary>
        private void update_image()
        {
            // 修改指针
            this.label1.Text = $"{this.PhotoIndex+1}/{this.imageDatas.Count}";
            // 修改图片
            if (this.imageDatas.Count == 0)
            {
                if (MessageBox.Show("错误", "图片不存在!", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    Close();
                }
            }
            else
            {
                this.pictureBox1.Image = CurrentImage;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.PhotoIndex = 0;
            this.update_image();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.PhotoIndex -= 1;
            this.update_image();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.PhotoIndex += 1;
            this.update_image();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.PhotoIndex = this.imageDatas.Count - 1;
            this.update_image();
        }
    }
}
