using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPTOlibrary.Models.ViewModels
{
    public class BookwithCoresVM
    {
        public BookInformation BookInformation { get; set; }
        public IEnumerable<BookCores> BookCores { get; set; }
        //public BookCores BookCores { get; set; }
    }
}
