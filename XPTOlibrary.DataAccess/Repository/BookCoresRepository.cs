using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;

namespace XPTOlibrary.DataAccess.Repository
{
    public class BookCoresRepository : Repository<BookCores>, IBookCoresRepository
    {
        private ApplicationDbContext _db;

        public BookCoresRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(BookCores obj)
        {
            _db.BookCores.Update(obj);
        }
    }
}
