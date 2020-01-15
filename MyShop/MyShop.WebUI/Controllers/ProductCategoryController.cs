using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.DataAccess.InMemory.ProductRpository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryController : Controller
    {
        IRepositroy<ProductCategory> context;
        public ProductCategoryController(IRepositroy<ProductCategory> productRepository)
        {
            context = productRepository;
        }

        public ActionResult AddProductCategory()
        {
            ProductCategory product = new ProductCategory();
            return View(product);
        }
        [HttpPost]
        [ActionName("AddProductCategory")]
        public ActionResult AddProduct(ProductCategory ProductCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(ProductCategory);
            }
            else
            {
                context.Insert(ProductCategory);
                context.Commit();
                return RedirectToAction("List");
            }
        }
        public ActionResult Delete(string Id)
        {
            ProductCategory item = context.Find(Id);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                throw new Exception("item not found");
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string Id)
        {
            var productToRemove = context.Find(Id);
            if (productToRemove != null)
            {
                context.Delete(productToRemove);
                context.Commit();
                return RedirectToAction("List");
            }
            else
            {
                return new HttpNotFoundResult("Not Found");
            }
        }

        public ActionResult Update(string Id)
        {
            ProductCategory item = context.Find(Id);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                return new HttpNotFoundResult("item not found");
            }
        }
        [HttpPost]
        [ActionName("Update")]
        public ActionResult UpdatePost(ProductCategory ProductCategory, string Id)
        {
            if (!ModelState.IsValid)
            {
                var item = context.Find(Id);
                return View(item);
            }
            var productToUpdate = context.Find(Id);
            if (productToUpdate != null)
            {
                productToUpdate.Category = ProductCategory.Category;
                context.Edit(productToUpdate);
                context.Commit();
                return RedirectToAction("List");
            }
            else
            {
                return new HttpNotFoundResult("Not Found");
            }
        }
        public ActionResult List()
        {
            var collection = context.Collection();
            return View(collection);
        }

    }
}
 