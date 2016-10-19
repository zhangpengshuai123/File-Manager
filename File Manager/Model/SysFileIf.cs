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
        string FullPath { get; }
        string CreateDateTime { get; }
        string WriteDateTime { get; }
        string AccessDateTime { get; }
    }
}
