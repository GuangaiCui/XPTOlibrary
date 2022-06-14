﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPTOlibrary.Models;

namespace XPTOlibrary.DataAccess.Repository.IRepository
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        void Update(Publisher obj);
    }
}
