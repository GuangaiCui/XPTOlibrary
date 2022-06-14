using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;

namespace XPTOlibrary.DataAccess.Repository
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        private ApplicationDbContext _db;

        public PublisherRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Publisher obj)
        {
            _db.Publisher.Update(obj);
        }
    }
}
