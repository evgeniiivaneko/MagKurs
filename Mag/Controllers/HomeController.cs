using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mag.Models;
using System.IO;

namespace Mag.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using (CatalogContext catalogContext = new CatalogContext())
            {
                //ChangeImage(catalogContext);
                //System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter();
                var products = catalogContext.Database.SqlQuery<Product>("SELECT * FROM GetLastProduct()").ToList();
                foreach (var product in products)
                {
                    product.Brand = catalogContext.Brand.FirstOrDefault(x => x.PK_BrandId == product.FK_Brand);
                    product.Image = catalogContext.Image.FirstOrDefault(x => x.PK_ImageId == product.FK_Image);
                    product.Type = catalogContext.Type.FirstOrDefault(x => x.PK_TypeId == product.FK_Type);
                }
                return View(products);
            }
        }

        public ActionResult Catalog()
        {
            using (CatalogContext catalogContext = new CatalogContext())
            {
                System.Data.SqlClient.SqlParameter page = new System.Data.SqlClient.SqlParameter("@page", 1);
                System.Data.SqlClient.SqlParameter count = new System.Data.SqlClient.SqlParameter("@count", 10);
                var products = catalogContext.Database.SqlQuery<Product>("SELECT * FROM GetProductPage(@page,@count)",page,count).ToList();
                foreach (var product in products)
                {
                    product.Brand = catalogContext.Brand.FirstOrDefault(x => x.PK_BrandId == product.FK_Brand);
                    product.Image = catalogContext.Image.FirstOrDefault(x => x.PK_ImageId == product.FK_Image);
                    product.Type = catalogContext.Type.FirstOrDefault(x => x.PK_TypeId == product.FK_Type);
                }
                return View(products);
            }

        }

        public ActionResult Create()
        {
            using (CatalogContext catalogContext = new CatalogContext())
            { 

                var brandList = catalogContext.Database.SqlQuery<Brand>("SELECT * FROM GetBrandList()").ToList();
                ViewBag.BrandNameList = brandList.Select(x => new SelectListItem() {Text = x.Name });
                var typeList = catalogContext.Database.SqlQuery<Mag.Models.Type>("SELECT * FROM GetTypeList()").ToList();
                ViewBag.TypeNameList = typeList.Select(x => new SelectListItem() { Text = x.Name });

                return View();
            }
        }
    }
}