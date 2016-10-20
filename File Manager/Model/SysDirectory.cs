using System;
using System.IO;
using System.Collections.Generic;

namespace File_Manager.Model
{
    class SysDirectory: SysFileIf
    {
        string strName;
        long lngSize;
        string strType;
        string strPath;
        string fileIcon;
        DateTime dateCreateTime;
        DateTime dateWriteTime;
        DateTime dateAccessTime;

        public SysDirectory(DirectoryInfo info)
        {
            lngSize = 1024;
            strName = info.Name;
            strPath = info.FullName;
            strType = "Folder";
            fileIcon = SetIcon(info.Extension);
            dateCreateTime = info.CreationTime;
            dateWriteTime = info.LastWriteTime;
            dateAccessTime = info.LastAccessTime;
        }

        public string Name
        {
            get { return strName; }
        }

        public string CreateTime
        {
            get { return dateCreateTime.ToShortDateString() + " " + dateCreateTime.ToLongTimeString(); }
        }

        public DateTime CreateDateTime
        {
            get { return dateCreateTime; }
        }

        public string WriteTime
        {
            get { return dateWriteTime.ToShortDateString() + " " + dateWriteTime.ToLongTimeString(); }
        }

        public DateTime WriteDateTime
        {
            get { return dateWriteTime; }
        }

        public string AccessTime
        {
            get { return dateAccessTime.ToShortDateString() + " " + dateAccessTime.ToLongTimeString(); }
        }

        public DateTime AccessDateTime
        {
            get { return dateAccessTime; }
        }

        public string Size
        {
            get { return (lngSize / 1024).ToString("N0") +" KB"; }
        }

        public long SizeValue
        {
            get
            {
                return lngSize;
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
            get
            {
                return strPath;
            }
        }

        public override string ToString()
        {
            return FullPath;
        }

        private string SetIcon(string strExten)
        {
            return @"/File Manager;component/images/FileDirectory.ico";
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
            strResult = sStr.Substring(0, len)+"...";
            //strTarget = sStr;
            //strSeg = new List<string>();
            //while (strTarget.Length > len)
            //{
            //    strSeg.Add(strTarget.Substring(0,len));
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
