using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Needs.Core.Models;
using Needs.API.ORM;
using Needs.API.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Needs.API.Controllers
{
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly NeedsDbContext needsDbContext;

        public ProductController(NeedsDbContext needsDbContext)
        {
            this.needsDbContext = needsDbContext;
        }

        [HttpGet("")]
        public IEnumerable<Product> GetAll()
        {
            return needsDbContext.Set<Product>().AsQueryable().ToList();
        }

        [HttpGet("{id}")]
        public async Task<Product> GetById([FromRoute] string id)
        {
            return needsDbContext.Set<Product>().AsQueryable().Where(x => x.Id == id).FirstOrDefault() ?? throw new Exception("Item não encontrado");
        }

        [HttpPost("")]
        public async Task<Product> Upsert([FromBody] Product product)
        {
            var product1 = needsDbContext.Set<Product>().AsQueryable().Where(x => x.Id == product.Id).FirstOrDefault();

            if (product1 == null)
            {
                product1 = new Product();
                product1.Id = Guid.NewGuid().ToString();

                needsDbContext.Set<Product>().Add(product1);
            }

            product1.Name = product.Name;
            product1.ImageUri = product.ImageUri;
            product1.Price = product.Price;

            needsDbContext.SaveChanges();

            return await GetById(product1.Id);
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] string id)
        {
            var product1 = needsDbContext.Set<Product>().AsQueryable().Where(x => x.Id == id).FirstOrDefault() ?? throw new Exception("Item não encontrado");
            needsDbContext.Set<Product>().Remove(product1);
            needsDbContext.SaveChanges();
        }
    }
}
