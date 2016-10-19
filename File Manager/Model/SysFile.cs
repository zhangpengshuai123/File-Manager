using System;
using System.IO;
using System.Collections.Generic;

namespace File_Manager.Model
{
    class SysFile: SysFileIf
    {
        string strName;
        long lngSize;
        string strType;
        string strPath;
        string fileIcon;
        DateTime dateCreateTime;
        DateTime dateWriteTime;
        DateTime dateAccessTime;

        public SysFile(FileInfo info)
        {
            lngSize = info.Length;
            strName = info.Name;
            strPath = info.DirectoryName;
            fileIcon = SetIcon(info.Extension);
            dateCreateTime = info.CreationTime;
            dateWriteTime = info.LastWriteTime;
            dateAccessTime = info.LastAccessTime;
        }

        public string Name
        {
            get { return strName; }
        }

        public string Size
        {
            get
            {
                if (lngSize < 1024)
                {
                    return lngSize.ToString("N0") + " B";
                }
                return (lngSize / 1024).ToString("N0") + " KB"; 
            }
        }

        public string Path
        {
            get { return strPath; }
        }

        public string Icon
        {
            get { return fileIcon; }
        }

        public string Type
        {
            get
            {
                return strType;
            }
        }

        public string DisplayName
        {
            get
            {
                return Name;
                //return ParaString(Name, 6);
            }
        }

        public string FullPath
        {
            get {
                return System.IO.Path.Combine(Path, Name);
            }
        }

        public string CreateDateTime
        {
            get { return dateCreateTime.ToShortDateString() + " " + dateCreateTime.ToLongTimeString(); }
        }

        public string WriteDateTime
        {
            get { return dateWriteTime.ToShortDateString() + " " + dateWriteTime.ToLongTimeString(); }
        }

        public string AccessDateTime
        {
            get { return dateAccessTime.ToShortDateString() + " " + dateAccessTime.ToLongTimeString(); }
        }

        public override string ToString()
        {
            return FullPath;
        }

        private string SetIcon(string strExten)
        {
            string strUri;
            
            switch (strExten.ToLower())
            {
                case ".exe":
                    strType = "Executable File";
                    strUri = @"/File Manager;component/images/FileExe.ico";
                    break;
                case ".bat":
                    strType = "Batch File";
                    strUri = @"/File Manager;component/images/FileExe.ico";
                    break;
                case ".com":
                    strType = "Command File";
                    strUri = @"/File Manager;component/images/FileExe.ico";
                    break;
                case ".rar":
                case ".7z":
                case ".zip":
                    strType = "Compressed File";
                    strUri = @"/File Manager;component/images/FileCompress.ico";
                    break;
                case ".mp3":
                case ".wma":
                case ".ogg":
                case ".wav":
                    strType = "Music File";
                    strUri = @"/File Manager;component/images/FileMusic.ico";
                    break;
                case ".jpg":
                case ".png":
                case ".jpeg":
                case ".gif":
                case ".bmp":
                case ".ico":
                    strType = "Picture File";
                    strUri = @"/File Manager;component/images/FilePicture.ico";
                    break;
                case ".dll":
                case ".sys":
                    strType = "System File";
                    strUri = @"/File Manager;component/images/FileSys.ico";
                    break;
                case ".txt":
                case ".xml":
                case ".ini":
                case ".html":
                    strType = "Text File";
                    strUri = @"/File Manager;component/images/FileText.ico";
                    break;
                case ".mp4":
                case ".avi":
                case ".wmv":
                case ".flv":
                case ".mkv":
                case ".rmvb":
                    strType = "Vedio File";
                    strUri = @"/File Manager;component/images/FileVedio.ico";
                    break;
                default:
                    strType = "File";
                    strUri = @"/File Manager;component/images/FileNormal.ico";
                    break;
            }
            return strUri;
        }

        private string ParaString(string sStr, int len)
        {
            //string strTarget;
            string strResult;
            //List<string> strSeg;

            if (len < 1)
            {
                return sStr;
            }
            if (sStr.Length <= len)
            {
                return sStr;
            }
            strResult = sStr.Substring(0, len) + "...";
            //strTarget = sStr;
            //strSeg = new List<string>();
            //while (strTarget.Length > len)
            //{
            //    strSeg.Add(strTarget.Substring(0, len));
            //    strTarget = strTarget.Substring(len);
            //}
            //strResult = "";
            //foreach (string sSeg in strSeg)
            //{
            //    strResult += sSeg + "\n";
            //}
            //strResult += strTarget;
            return strResult;
        }
    }
}
