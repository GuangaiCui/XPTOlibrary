using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;

namespace XPTOlibrary.DataAccess.Repository
{
    public class BorrowRecordRepository : Repository<BorrowRecord>, IBorrowRecordRepository
    {
        private ApplicationDbContext _db;

        public BorrowRecordRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(BorrowRecord obj)
        {
            _db.BorrowRecord.Update(obj);
        }
    }
}
