using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CICS_PP
{
    public class FileAccess
    {
        /// <summary>  
        /// 返回指定目录中与指定的搜索模式匹配的文件的名称（包含它们的路径），并使用一个值以确定是否搜索子目录  
        /// </summary>  
        /// <param name="folderPath">将从其检索文件的目录</param>  
        /// <param name="filter">搜索模式匹配</param>  
        /// <param name="searchOption">指定搜索操作应包括所有子目录还是仅包括当前目录</param>  
        /// <returns>包含指定目录中与指定搜索模式匹配的文件的名称列表</returns>  
        public static String[] GetFileNames(String folderPath, String filter, SearchOption searchOption)
        {   // 获取文件列表  
            String[] FileEntries = Directory.GetFiles(folderPath, "*", searchOption);
            if (FileEntries.Length == 0)
                return null;
            else if (String.IsNullOrEmpty(filter))
                return FileEntries;

            // 建立正则表达式  
            Regex rx = new Regex(filter, RegexOptions.IgnoreCase);

            // 过滤文件  
            List<String> FilterFileEntries = new List<String>(FileEntries.Length);
            foreach (String FileName in FileEntries)
            {
                if (rx.IsMatch(FileName))
                {
                    FilterFileEntries.Add(FileName);
                }
            }

            return (FilterFileEntries.Count == 0) ? null : FilterFileEntries.ToArray();
        }

        /// <summary>  
        /// 返回指定目录中图像文件列表，并使用一个值以确定是否搜索子目录  
        /// </summary>  
        /// <param name="folderPath">将从其检索文件的目录</param>  
        /// <param name="searchOption">指定搜索操作应包括所有子目录还是仅包括当前目录</param>  
        /// <returns>图像文件列表</returns>  
        public static String[] GetImages(String folderPath, SearchOption searchOption)
        {
            String filter = "\\.(bmp|gif|jpg|jpe|png|tiff|tif)$";
            return GetFileNames(folderPath, filter, searchOption);
        }
    }
}
