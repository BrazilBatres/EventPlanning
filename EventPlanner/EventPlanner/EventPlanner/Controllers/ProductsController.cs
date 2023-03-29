using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventPlannerModels;

namespace EventPlanner.Controllers
{
    public class ProductsController : Controller
    {

        public ProductsController()
        {
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await Functions.APIServices.ProductsGetList();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Product product = await Functions.APIServices.ProductsDetails(id);
            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<ProductCategory> productCategories = await Functions.APIServices.ProductCategoriesGetList();
            ViewData["ProductCategoryId"] = new SelectList(productCategories, "Id", "Category");
            IEnumerable<Seller> sellers = await Functions.APIServices.SellersGetList();
            ViewData["SellerId"] = new SelectList(sellers, "Id", "User.FirstName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SellerId,ProductName,ProductDescription,ProductPrice,ProductCategoryId")] Product product)
        {
            GeneralResult generalResult = await Functions.APIServices.ProductsCreate(product);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Product product = await Functions.APIServices.ProductsDetails(id);
            IEnumerable<ProductCategory> productCategories = await Functions.APIServices.ProductCategoriesGetList();
            ViewData["ProductCategoryId"] = productCategories.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Category,
                Selected = (s.Id == product.ProductCategoryId)
            }).ToList();
            IEnumerable<Seller> sellers = await Functions.APIServices.SellersGetList();
            ViewData["SellerId"] = sellers.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.User.FirstName,
                Selected = (s.Id == product.SellerId)
            }).ToList();
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SellerId,ProductName,ProductDescription,ProductPrice,ProductCategoryId")] Product product)
        {
            GeneralResult generalResult = await Functions.APIServices.ProductsEdit(id, product);
            if (generalResult.Result)
            {

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Product product = await Functions.APIServices.ProductsDetails(id);

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            GeneralResult generalResult = await Functions.APIServices.ProductsDelete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
