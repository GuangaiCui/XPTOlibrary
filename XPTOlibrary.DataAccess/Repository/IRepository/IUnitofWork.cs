using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPTOlibrary.DataAccess.Repository.IRepository
{
    public interface IUnitofWork
    {
        IBookInformationRepository BookInformation { get; }


        void Save();
    }
}