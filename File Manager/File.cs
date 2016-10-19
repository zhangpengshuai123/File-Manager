using System;
using System.IO;

namespace File_Manager
{
    class File
    {
        string strName;
        bool isDir;
        DateTime dateTime;
        string strSize;
        string strPath;
        string fileIcon;

        public File(FileInfo info)
        {
            isDir = false;
            strSize = (info.Length / 1024).ToString("N0") + " KB";
            dateTime = info.LastWriteTime;
            strName = info.Name;
            strPath = info.DirectoryName;
            fileIcon = SetIcon(info.Extension);
            
        }

        public File(DirectoryInfo info)
        {
            isDir = true;
            strSize = " 1 KB";
            dateTime = info.LastWriteTime;
            strName = info.Name;
            strPath = info.FullName;
            fileIcon = SetIcon(info.Extension);
            
        }

        public string Name
        {
            get { return strName; }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
        }

        public string Size
        {
            get { return strSize; }
        }

        public string Path
        {
            get { return strPath; }
        }

        public string Icon
        {
            get { return fileIcon; }
        }

        public string FullPath
        {
            get {
                if (isDir == true)
                {
                    return strPath;
                }
                return System.IO.Path.Combine(Path, Name);
            }
        }

        public override string ToString()
        {
            return FullPath;
        }

        private string SetIcon(string strExten)
        {
            string strUri;

            if (isDir == true)
            {
                return @"/images/FileDirectory.ico";
            }

            switch (strExten)
            {
                default:
                    strUri = @"/images/FileNormal.ico";
                    break;
            }
            return strUri;
        }

    }
}
