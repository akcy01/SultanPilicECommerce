using Sultan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultan.DataAccess.Repository.IRepository
{
    public interface IChickTypeRepository : IRepository<ChickType>
    {
        void Update(ChickType chickType);
    }
}
