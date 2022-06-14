using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPTOlibrary.DataAccess.Repository.IRepository;

namespace XPTOlibrary.DataAccess.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            BookInformation = new BookInformationRepository(_db);
        }
        public IBookInformationRepository BookInformation { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
