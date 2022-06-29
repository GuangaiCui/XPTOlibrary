using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace XPTOlibrary.Models.ViewModels
{
    public class BookCoresVM
    {
        public BookCores BookCores { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> BookList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CoreList { get; set; }
    }
}
