using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Model
{
    enum SortDirectrion
    {
        Ascending = 0,
        Descending
    }

    enum SortField
    {
        Name,
        Type,
        Size,
        CreateTime,
        WriteTime
    }

    class SysSorter
    {
        public SortField field;
        public SortDirectrion direction;

        public SysSorter(SortField sfield, SortDirectrion sDirect = SortDirectrion.Ascending)
        {
            this.field = sfield;
            this.direction = sDirect;
        }


    }
}
