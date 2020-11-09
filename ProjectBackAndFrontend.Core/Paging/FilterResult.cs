using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBackAndFrontend.Core.Paging
{
    public class FilterResult<TModel>
    {
        public List<TModel> DataItems { get; set; }

        public int TotalPageCount { get; set; }

        public int TotalItemsCount { get; set; }

        public int PageIndex { get; set; }

        public string TotalString { get; set; }
    }
}
