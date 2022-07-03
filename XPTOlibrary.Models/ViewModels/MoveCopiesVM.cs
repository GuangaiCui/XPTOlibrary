using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace XPTOlibrary.Models.ViewModels
{
    public class MoveCopiesVM
    {
        public BookCores BookCores { get; set; }
        public int MoveCopies { get; set; }
        public int OriginCoreId { get; set; }
        public int DestinationCoreId { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> OriginCoreList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DestinationCoreList { get; set; }
    }
}
