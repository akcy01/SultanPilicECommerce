using Sultan.DataAccess.Repository.IRepository;
using Sultan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultan.DataAccess.Repository
{
    public class ChickTypeRepository : Repository<ChickType>,IChickTypeRepository
    {
        private ApplicationDbContext _dbContext;
        public ChickTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(ChickType chickType)
        {
            _dbContext.ChickTypes.Update(chickType);
        }
    }
}
