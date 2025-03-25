using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLongDarkBuckupTools.PhotoData
{
    /// <summary>
    /// 拍立得数据
    /// </summary>
    public class ImageData
    {
        #region 数据部分
        /// <summary>
        /// 所属存档名称
        /// </summary>
        public string m_AssociatedSave { get; set; }

        /// <summary>
        /// 图片数据
        /// </summary>
        public string m_JpegData { get; set; }
    
        /// <summary>
        /// 照片宽度
        /// </summary>
        public string m_Width { get; set; }

        /// <summary>
        /// 照片高度
        /// </summary>
        public string m_Height { get; set; }

        /// <summary>
        /// 照片类型
        /// </summary>
        public PhotoType m_TextureFormat { get; set; }
        #endregion
    }
}
