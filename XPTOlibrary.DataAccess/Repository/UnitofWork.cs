using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models.ViewModels;

namespace XPTOlibrary.DataAccess.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            BookInformation = new BookInformationRepository(_db);
            Publisher = new PublisherRepository(_db);
            Author = new AuthorRepository(_db);
            Cores = new CoresRepository(_db);
            Topic = new TopicRepository(_db);


        }
        public IBookInformationRepository BookInformation { get; private set; }
        public IPublisherRepository Publisher { get; private set; }
        public IAuthorRepository Author { get; private set; }
        public ITopicRepository Topic { get; private set; }
        public ICoresRepository Cores { get; private set; }



        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
