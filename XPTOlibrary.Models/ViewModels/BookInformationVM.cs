using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace XPTOlibrary.Models.ViewModels
{
    public class BookInformationVM
    {
        public BookInformation BookInformation { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> PublisherList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> AuthorList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> TopicList { get; set; }

    }
}
