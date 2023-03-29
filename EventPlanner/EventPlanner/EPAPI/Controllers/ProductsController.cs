using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventPlannerModels;
using EPAPI.Models;

namespace EPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly EventPlanningContext _context;

        public ProductsController()
        {
            _context = new EventPlanningContext();
        }
        // GET: api/Products/Sellers
        [HttpGet]
        [Route("Sellers")]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.Seller>>> GetSellers()
        {
            if (_context.ProductCategories == null)
            {
                return NotFound();
            }
            var sellers = await (from s in _context.Sellers
                                           join u in _context.Users on s.UserId equals u.Id
                                           select new EventPlannerModels.Seller
                                           {
                                               Id = s.Id,
                                               CompanyName = s.CompanyName,
                                               User = new EventPlannerModels.User
                                               {
                                                   Id = s.UserId,
                                                   FirstName = u.FirstName,
                                                   LastName = u.LastName,
                                               }
                                           }).ToListAsync();
            return sellers;
        }

        // GET: api/Products/ProductCategories
        [HttpGet]
        [Route("ProductCategories")]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.ProductCategory>>> GetProductCategories()
    {
            if (_context.ProductCategories == null)
            {
                return NotFound();
            }
            var productCategories = await (from pc in _context.ProductCategories
                                   select new EventPlannerModels.ProductCategory
                                   {
                                       Id = pc.Id,
                                       Category = pc.Category,
                                   }).ToListAsync();
            return productCategories;
        }
    // GET: api/Products
    [HttpGet]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var products = await (from p in _context.Products
                               join pc in _context.ProductCategories on p.ProductCategoryId equals pc.Id
                               join s in _context.Sellers on p.SellerId equals s.Id
                               join u in _context.Users on s.UserId equals u.Id
                               select new EventPlannerModels.Product
                               {
                                   Id = p.Id,
                                   SellerId = p.SellerId,
                                   ProductName = p.ProductName,
                                   ProductDescription = p.ProductDescription,
                                   ProductPrice = p.ProductPrice,
                                   ProductCategoryId = p.ProductCategoryId,
                                   ProductCategory = new EventPlannerModels.ProductCategory
                                   {
                                       Id = pc.Id,
                                       Category = pc.Category
                                   },
                                   Seller = new EventPlannerModels.Seller 
                                   { 
                                       Id = s.Id, 
                                       CompanyName = s.CompanyName,
                                       User = new EventPlannerModels.User
                                       {
                                           Id = s.UserId,
                                           FirstName = u.FirstName,
                                           LastName = u.LastName,
                                       }
                                   },
                                   
                               }).ToListAsync();
            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventPlannerModels.Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await (from p in _context.Products
                                 join pc in _context.ProductCategories on p.ProductCategoryId equals pc.Id
                                 join s in _context.Sellers on p.SellerId equals s.Id
                                 join u in _context.Users on s.UserId equals u.Id
                                 where p.Id == id
                                 select new EventPlannerModels.Product
                                 {
                                     Id = p.Id,
                                     SellerId = p.SellerId,
                                     ProductName = p.ProductName,
                                     ProductDescription = p.ProductDescription,
                                     ProductPrice = p.ProductPrice,
                                     ProductCategoryId = p.ProductCategoryId,
                                     ProductCategory = new EventPlannerModels.ProductCategory
                                     {
                                         Id = pc.Id,
                                         Category = pc.Category
                                     },
                                     Seller = new EventPlannerModels.Seller
                                     {
                                         Id = s.Id,
                                         CompanyName = s.CompanyName,
                                         User = new EventPlannerModels.User
                                         {
                                             Id = s.UserId,
                                             FirstName = u.FirstName,
                                             LastName = u.LastName,
                                         }
                                     },
                                     
                                 }).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResult>> PutProduct(int id, EventPlannerModels.Product product)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                Models.Product context_product = new Models.Product()
                {
                    Id = id,
                    SellerId= product.SellerId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                    ProductCategoryId = product.ProductCategoryId,
                };
                _context.Entry(context_product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
                throw;
            }


            return generalResult;
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GeneralResult>> PostProduct(EventPlannerModels.Product product)
        {
            GeneralResult generalResult =
                      new GeneralResult() { Result = false };
            try
            {
                if (_context.Users == null)
                {
                    return Problem("Entity set 'MoviesContext.Categories'  is null.");
                }
                Models.Product context_product = new Models.Product()
                {
                    Id = product.Id,
                    SellerId = product.SellerId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                    ProductCategoryId = product.ProductCategoryId,
                };
                _context.Products.Add(context_product);
                await _context.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
                throw;
            }

            return generalResult;
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResult>> DeleteProduct(int id)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                if (_context.Users == null)
                {
                    return NotFound();
                }
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
                throw;
            }
            return generalResult;
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
