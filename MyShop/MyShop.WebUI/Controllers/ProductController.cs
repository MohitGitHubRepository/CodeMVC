using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory.ProductRpository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductController : Controller
    {

        ProductDetailsRepository context;
        ProductCategoryRepository contextCategory;
        public ProductController()
        {
            context = new ProductDetailsRepository();
            contextCategory = new ProductCategoryRepository();

        }
     
        public ActionResult AddProduct()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Product = new Product();
            productViewModel.productCategories = contextCategory.Collection().ToList();
            return View(productViewModel);
        }
        [HttpPost]
        [ActionName("AddProduct")]
        public ActionResult AddProduct(Product product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("List");
            }

        }
        public ActionResult Delete(string Id)
        {
            Product item = context.Find(Id);
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
            Product item = context.Find(Id);
            if(item!=null)
            {
                ProductViewModel productViewModel = new ProductViewModel();
                productViewModel.Product = item;
                productViewModel.productCategories = contextCategory.Collection().ToList();

                return View(productViewModel);
            }
            else
            {
                throw new Exception("item not found");
            }
           
        }
        [HttpPost]
        [ActionName("Update")]
        public ActionResult UpdatePost(Product product, string Id)
        {
            if(!ModelState.IsValid)
            {
                var item = context.Find(Id);
                return View(item);
            }
            var productToUpdate = context.Find(Id);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Description = product.Description;
                productToUpdate.Price = product.Price;
                productToUpdate.Category = product.Category;
                productToUpdate.Image = product.Image;

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