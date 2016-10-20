using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Model
{
    interface SysFileIf
    {
        string Name { get; }
        string Type { get; }
        string Size { get; }
        long SizeValue { get; }
        string FullPath { get; }
        string CreateTime { get; }
        DateTime CreateDateTime { get; }
        string WriteTime { get; }
        DateTime WriteDateTime { get; }
        string AccessTime { get; }
        DateTime AccessDateTime { get; }
    }

    class FileComparer : IComparer<SysFileIf>
    {
        private List<SysSorter> sList;

        public FileComparer(List<SysSorter> list)
        {
            sList = list;
        }

        public int Compare(SysFileIf x, SysFileIf y)
        {
            int result = 0;

            foreach (SysSorter cond in sList)
            {
                result = Compare(x, y, cond.field, cond.direction);
                if (result != 0)
                {
                    break;
                }
            }

            return result;
        }

        public int Compare(SysFileIf x, SysFileIf y, SortField field, SortDirectrion direction)
        {
            int result = 0;

            switch (field)
            {

                case SortField.Name:
                    if (direction == SortDirectrion.Ascending)
                    {
                        result = x.Name.CompareTo(y.Name);
                    }
                    else
                    {
                        result = y.Name.CompareTo(x.Name);
                    }
                    break;
                case SortField.Type:
                    if (direction == SortDirectrion.Ascending)
                    {
                        result = x.Type.CompareTo(y.Type);
                    }
                    else
                    {
                        result = y.Type.CompareTo(x.Type);
                    }
                    break;
                case SortField.Size:
                    if (direction == SortDirectrion.Ascending)
                    {
                        result = x.SizeValue.CompareTo(y.SizeValue);
                    }
                    else
                    {
                        result = y.SizeValue.CompareTo(x.SizeValue);
                    }
                    break;
                case SortField.CreateTime:
                    if (direction == SortDirectrion.Ascending)
                    {
                        result = x.CreateDateTime.CompareTo(y.CreateDateTime);
                    }
                    else
                    {
                        result = y.CreateDateTime.CompareTo(x.CreateDateTime);
                    }
                    break;
                case SortField.WriteTime:
                    if (direction == SortDirectrion.Ascending)
                    {
                        result = x.AccessDateTime.CompareTo(y.AccessDateTime);
                    }
                    else
                    {
                        result = y.AccessDateTime.CompareTo(x.AccessDateTime);
                    }
                    break;
            }

            return result;
        }
    }

}
