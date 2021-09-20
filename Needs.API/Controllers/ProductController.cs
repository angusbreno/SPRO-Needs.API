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
    //urlbase/product/3/byid/554433?cordoproduto=verde
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("")]
        public IEnumerable<ProductBase> GetAll()
        {
            return new List<ProductBase>();
        }

        [HttpGet("{id}")]
        public async Task<object> GetById(
            [FromServices] IMemoryCache memoryCache,
            [FromRoute] string id)
        {
            return await memoryCache.GetOrCreateAsync("test", async (cacheEntry) => {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                return await PegaProduto();
            });
            
            //return new ProductBase();
        }

        private async Task<Test> PegaProduto()
        {
            await Task.Delay(10000);
            return new Test();
        }

        [HttpPost("")]
        public ProductBase Upsert(
            [FromServices] NeedsDbContext dbContext,
            [FromBody] ProductBase product,
            [FromServices] NeedsDbContext needsDbContext
            )
        {
            return product;
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] string id)
        {
        }
    }
}
