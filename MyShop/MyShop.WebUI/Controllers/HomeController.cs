using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {

        IRepositroy<Product> context;
        IRepositroy<ProductCategory> contextCategory;
        public HomeController(IRepositroy<Product> productRepository, IRepositroy<ProductCategory> prductCategoryRepository)
        {
            context = productRepository;
            contextCategory = prductCategoryRepository;

        }
        public ActionResult Index(string CollectionId)
        {
            ProductCaegoryViewModel productCaegoryViewModel = new ProductCaegoryViewModel();
            var productsList = context.Collection();
            var productCategory = contextCategory.Collection();
            if (CollectionId==null)
            {
                productCaegoryViewModel.setViewModel(productsList, productCategory);
            }
            else
            {
                var category= contextCategory.Collection().Where(a => a.Id == CollectionId).FirstOrDefault().Category;
                productsList = context.Collection().Where(a => a.Category == category);
                productCaegoryViewModel.setViewModel(productsList, productCategory);
            }
            
            return View(productCaegoryViewModel);
        }

       
        public ActionResult About(string productId)
       {
            Product product = context.Find(productId);

            return View(product);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}