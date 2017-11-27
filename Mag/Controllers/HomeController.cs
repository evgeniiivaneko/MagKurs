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
            CatalogContext catalogContext = new CatalogContext();
            var brands = catalogContext.Brand.ToList<Brand>();
            foreach(var brand in brands)
            {
                brand.Image = catalogContext.Image.FirstOrDefault(x => x.PK_ImageId == brand.FK_Image);
            }
            catalogContext.Dispose();
            ChangeImage();
            
            return View(brands);
        }

        public static System.Drawing.Image GetImageFromByteArray(byte[] byteArray)
        {
            MemoryStream memoryStream = new MemoryStream(byteArray);
            return System.Drawing.Image.FromStream(memoryStream);
        }

        private void ChangeImage()
        {
            using(CatalogContext catalogContext = new CatalogContext())
            {
                var images = catalogContext.Image.ToList<Image>().OrderBy(x => x.PK_ImageId).ToList();
                images[0].Picture = @"c:\users\evgen\documents\visual studio 2017\Projects\Mag\Mag\Images\Brand\bosch.png";
                images[1].Picture = @"c:\users\evgen\documents\visual studio 2017\Projects\Mag\Mag\Images\Brand\Electrolux_2015.png";
                images[2].Picture = @"c:\users\evgen\documents\visual studio 2017\Projects\Mag\Mag\Images\Brand\Gree_logo.png";
                images[3].Picture = @"c:\users\evgen\documents\visual studio 2017\Projects\Mag\Mag\Images\Brand\7ecd1ff17f9ac162feb51a84f04224a3.png";
                images[4].Picture = @"c:\users\evgen\documents\visual studio 2017\Projects\Mag\Mag\Images\Brand\C&H_logo.jpg";
                images[5].Picture = @"c:\users\evgen\documents\visual studio 2017\Projects\Mag\Mag\Images\Brand\Logo-KITANO.png";
                images[6].Picture = @"c:\users\evgen\documents\visual studio 2017\Projects\Mag\Mag\Images\Brand\1476097431_30415405_w640_h640_logo4.jpg";
                images[7].Picture = @"c:\users\evgen\documents\visual studio 2017\Projects\Mag\Mag\Images\Brand\logo-lessar.png";
                images[8].Picture = @"c:\users\evgen\documents\visual studio 2017\Projects\Mag\Mag\Images\Brand\1494579302_2e28236f2b9ebd93382cf860917e6018.jpeg";

                foreach(var image in images)
                {
                    catalogContext.Entry<Image>(image).State = System.Data.Entity.EntityState.Modified;
                }
                catalogContext.SaveChanges();
            }
        }

    }
}