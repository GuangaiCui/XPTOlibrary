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
        IPublisherRepository Publisher { get; }
        IAuthorRepository Author { get; }
        ITopicRepository Topic { get; }
        ICoresRepository Cores { get; }
        IBorrowRecordRepository BorrowRecord { get; }
        IApplicationUserRepository ApplicationUser { get; }


        void Save();
    }
}