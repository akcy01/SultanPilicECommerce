using Sultan.DataAccess.Repository.IRepository;
using Sultan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultan.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(Product product)
        {
            var objFromDb = _dbContext.Products.FirstOrDefault(u => u.Id == product.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = product.Title;
                objFromDb.Description = product.Description;
                objFromDb.Price = product.Price;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.ChickTypeId = product.ChickTypeId;
                if(objFromDb.ImageUrl != null)
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
