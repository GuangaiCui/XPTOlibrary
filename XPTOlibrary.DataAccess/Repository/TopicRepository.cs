using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;

namespace XPTOlibrary.DataAccess.Repository
{
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        private ApplicationDbContext _db;

        public TopicRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Topic obj)
        {
            _db.Topic.Update(obj);
        }
    }
}
