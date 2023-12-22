using System;
using System.IO;

namespace Utility
{
    public class FileUtility
    {
        public static FileStream ReadFileStream(string filePath, long currentOffset = 0)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open
                , FileAccess.Read, FileShare.ReadWrite);
            fs.Seek(currentOffset, SeekOrigin.Begin);
            return fs;
        }

        private static string CreateFolderByDirectoryPath(string directoryPath)
        {
            string mFolderYear = "yyyy";
            string mFolderMonth = "yyyy-MM";
            string mFolderDay = "yyyy-MM-dd";

            var folderYear = directoryPath + "\\" + DateTime.Now.ToString(mFolderYear);
            var folderMonth = folderYear + "\\" + DateTime.Now.ToString(mFolderMonth);
            var folderDay = folderMonth + "\\" + DateTime.Now.ToString(mFolderDay);

            if (!Directory.Exists(folderDay))
            {
                if (!Directory.Exists(folderMonth))
                {
                    if (!Directory.Exists(folderYear))
                    {
                        Directory.CreateDirectory(folderYear);
                        Directory.CreateDirectory(folderMonth);
                        Directory.CreateDirectory(folderDay);
                    }
                    else
                    {
                        Directory.CreateDirectory(folderMonth);
                        Directory.CreateDirectory(folderDay);
                    }
                }
                else
                {
                    Directory.CreateDirectory(folderDay);
                }
            }
            return folderDay;
        }

        public static string BuildingFileLogPath(string folderPath, string fileName)
        {
            folderPath = CreateFolderByDirectoryPath(folderPath);
            return folderPath += "\\" + fileName;
        }
    }
}
