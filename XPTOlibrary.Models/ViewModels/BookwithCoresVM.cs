using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPTOlibrary.Models.ViewModels
{
    public class BookwithCoresVM
    {
        public BookCores BookCores { get; set; }
        public IEnumerable<BookInformation> BookInformation { get; set; }
        public IEnumerable<Cores> Cores { get; set; }
    }
}
