using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;

namespace XPTOlibrary.DataAccess.Repository
{
    public class CoresRepository : Repository<Cores>, ICoresRepository
    {
        private ApplicationDbContext _db;

        public CoresRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Cores obj)
        {
            _db.Cores.Update(obj);
        }
    }
}
