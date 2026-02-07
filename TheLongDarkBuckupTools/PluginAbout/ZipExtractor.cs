using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheLongDarkBuckupTools.PluginAbout
{
    /// <summary>
    /// 提取嵌入的ZIP文件到指定目录
    /// </summary>
    public class Extractor
    {
        /// <summary>
        /// 提取嵌入文件到指定目录
        /// </summary>
        /// <param name="resourceName">资源名称</param>
        /// <param name="outputPath">输出路径</param>
        /// <param name="overwrite">是否询问覆盖</param>
        /// <returns></returns>
        public static bool ExtractFile(string resourceName, string outputPath, bool overwrite = false)
        {
            // 获取当前程序集
            var assembly = Assembly.GetExecutingAssembly();

            // 构建资源名称：默认命名空间.文件名
            // 如果文件在文件夹中：默认命名空间.文件夹名.文件名
            string fullResourceName = $"{assembly.GetName().Name}.{resourceName}";

            using (Stream stream = assembly.GetManifestResourceStream(fullResourceName))
            {
                if (stream == null)
                    throw new FileNotFoundException($"资源 '{fullResourceName}' 未找到");

                // 创建临时文件
                string tempPath = Path.GetTempFileName();

                try
                {
                    // 将资源流写入临时文件
                    using (FileStream fs = new FileStream(tempPath, FileMode.Create))
                    {
                        stream.CopyTo(fs);
                    }

                    // 如果文件已存在，询问是否覆盖
                    if (File.Exists(outputPath))
                    {
                        if (
                            overwrite ||
                            MessageBox.Show(
                                "文件已存在，是否覆盖？", 
                                "询问", 
                                MessageBoxButtons.YesNo
                            ) == DialogResult.Yes
                        )
                        {
                            File.Delete(outputPath);
                        }
                    }

                    // 将临时文件复制到输出路径
                    File.Copy(tempPath, outputPath);
                    File.Delete(tempPath);

                    // 返回
                    return true;
                }
                catch (Exception)
                {
#if DEBUG
                    throw;
#else
                    return false;
#endif
                }
            }
        }

        /// <summary>
        /// 提取嵌入的ZIP文件并解压到指定目录
        /// <br />
        /// 构建资源名称：默认命名空间.文件名
        /// <br />
        /// 如果文件在文件夹中：默认命名空间.文件夹名.文件名
        /// </summary
        /// <param name="resourceName">资源名称</param>
        /// <param name="outputPath">输出路径</param>
        /// <param name="overwrite">是否询问覆盖</param>
        public static bool ExtractEmbeddedZip(string resourceName, string outputPath, bool overwrite = false)
        {
            // 获取当前程序集
            var assembly = Assembly.GetExecutingAssembly();

            // 构建资源名称：默认命名空间.文件名
            // 如果文件在文件夹中：默认命名空间.文件夹名.文件名
            string fullResourceName = $"{assembly.GetName().Name}.{resourceName}";

            using (Stream stream = assembly.GetManifestResourceStream(fullResourceName))
            {
                if (stream == null)
                    throw new FileNotFoundException($"资源 '{fullResourceName}' 未找到");

                // 创建临时文件
                string tempZipPath = Path.GetTempFileName();

                try
                {
                    // 将资源流写入临时文件
                    using (FileStream fs = new FileStream(tempZipPath, FileMode.Create))
                    {
                        stream.CopyTo(fs);
                    }

                    try
                    {
                        // 解压ZIP文件
                        ZipFile.ExtractToDirectory(tempZipPath, outputPath);

                        return true;
                    } catch (IOException e)
                    {
                        // 弹窗询问是否强制覆盖
                        if (
                            overwrite ||
                            MessageBox.Show("解压文件时发生错误" +
#if DEBUG
                            $"：\n{e.Message}" +
#endif
                            "\n\n是否强制覆盖已存在的文件？", 
                            "错误", 
                            MessageBoxButtons.YesNo, 
                            MessageBoxIcon.Error) == DialogResult.Yes
                        )
                        {
                            // 1. 确保目标目录存在
                            if (!Directory.Exists(outputPath))
                            {
                                Directory.CreateDirectory(outputPath);
                            }

                            // 2. 使用 ZipArchive 进行精细控制
                            using (var archive = ZipFile.OpenRead(tempZipPath))
                            {
                                foreach (ZipArchiveEntry entry in archive.Entries)
                                {
                                    // 获取解压后的完整路径
                                    string destinationPath = Path.GetFullPath(Path.Combine(outputPath, entry.FullName));

                                    // 确保路径在目标目录内（防止Zip滑移攻击）
                                    if (!destinationPath.StartsWith(outputPath, StringComparison.OrdinalIgnoreCase))
                                    {
                                        continue; // 跳过不安全的路径
                                    }

                                    // 如果是目录，创建它
                                    if (entry.FullName.EndsWith("/"))
                                    {
                                        Directory.CreateDirectory(destinationPath);
                                        continue;
                                    }

                                    // 确保目标文件的父目录存在
                                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                                    // 【关键点】解压并覆盖：如果文件存在，直接覆盖
                                    entry.ExtractToFile(destinationPath, overwrite: true);
                                }
                            }

                            // 3. 返回
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                finally
                {
                    // 清理临时文件
                    if (File.Exists(tempZipPath))
                        File.Delete(tempZipPath);
                }
            }
        }
    }
}
