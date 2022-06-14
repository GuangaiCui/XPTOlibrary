using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;

namespace XPTOlibrary.DataAccess.Repository
{
    public class BookInformationRepository : Repository<BookInformation>, IBookInformationRepository
    {
        private ApplicationDbContext _db;

        public BookInformationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(BookInformation obj)
        {
            _db.BookInformation.Update(obj);
        }
    }
}
