using System.IO;
using System.Text.RegularExpressions;
using TheLongDarkBuckupTools.Helpers;
using TheLongDarkBuckupTools.Serialization;

namespace TheLongDarkBuckupTools.MoreData
{
    /// <summary>
    /// 档案类(移植自TLD-Save-Editor的Profile)
    /// 管理user001档案文件(徽章进度、4DON徽章等)
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// 档案文件路径
        /// </summary>
        public string path;

        private DynamicSerializable<ProfileState> dynamicState;
        /// <summary>
        /// 档案状态数据
        /// </summary>
        public ProfileState State { get { return dynamicState.Obj; } }

        public Profile(string path)
        {
            this.path = path;

            var json = EncryptString.DecompressBytesToString(File.ReadAllBytes(path));

            // m_StatsDictionary是无效json(键未加引号),需要修复
            json = Regex.Replace(json, @"(\\*\""m_StatsDictionary\\*\"":\{)((?:[-0-9\.]+:\\*\""[-+0-9eE\.]+\\*\""\,?)+)(\})", delegate (Match match)
            {
                string jsonSubStr = Regex.Replace(match.Groups[2].ToString(), @"([-0-9]+):(\\*\"")", delegate (Match matchSub)
                {
                    var escapeStr = matchSub.Groups[2].ToString();
                    return escapeStr + matchSub.Groups[1].ToString() + escapeStr + @":" + escapeStr;
                });
                return match.Groups[1].ToString() + jsonSubStr + match.Groups[3].ToString();
            });

            dynamicState = new DynamicSerializable<ProfileState>(json);
        }

        /// <summary>
        /// 保存档案
        /// </summary>
        public void Save()
        {
            string json = dynamicState.Serialize();

            // 游戏无法读取m_StatsDictionary的标准json,需要去掉键的引号
            json = Regex.Replace(json, @"(\\*\""m_StatsDictionary\\*\"":\{)((?:\\*\""[-0-9\.]+\\*\"":\\*\""[-+0-9eE\.]+\\*\""\,?)+)(\})", delegate (Match match)
            {
                string jsonSubStr = Regex.Replace(match.Groups[2].ToString(), @"\\*\""([-0-9]+)\\*\"":", delegate (Match matchSub)
                {
                    return matchSub.Groups[1].ToString() + @":";
                });
                return match.Groups[1].ToString() + jsonSubStr + match.Groups[3].ToString();
            });

            File.WriteAllBytes(path, EncryptString.CompressStringToBytes(json));
        }
    }
}
