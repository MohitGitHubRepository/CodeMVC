using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductController : Controller
    {

        IRepositroy<Product> context;
        IRepositroy<ProductCategory> contextCategory;
        public ProductController(IRepositroy<Product> productRepository,IRepositroy<ProductCategory> prductCategoryRepository)
        {
            context = productRepository;
            contextCategory = prductCategoryRepository;

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
        public ActionResult AddProduct(Product product ,HttpPostedFileBase file)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if(file!=null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//Image//") + product.Image);
                }
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
        public ActionResult UpdatePost(Product product, string Id,HttpPostedFileBase file)
        {
            if(!ModelState.IsValid)
            {
                var item = context.Find(Id);
                return View(item);
            }
            var productToUpdate = context.Find(Id);
            if (productToUpdate != null)
            {
                if (file != null)
                {
                    productToUpdate.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//Image//") + productToUpdate.Image);
                }
                productToUpdate.Name = product.Name;
                productToUpdate.Description = product.Description;
                productToUpdate.Price = product.Price;
                productToUpdate.Category = product.Category;
                //productToUpdate.Image = product.Image;

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