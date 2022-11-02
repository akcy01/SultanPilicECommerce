using Sultan.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultan.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Category = new CategoryRepository(_dbContext);
        }
        public ICategoryRepository Category { get; private set; }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
