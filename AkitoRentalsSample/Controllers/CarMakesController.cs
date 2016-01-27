using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using AkitoRentalsSample.Models;
using AkitoRentalsSample.Models.ViewModels;

namespace AkitoRentalsSample.Controllers
{
    public class CarMakesController : Controller
    {
        private readonly AkitoRentalsContext db = new AkitoRentalsContext ( );
        // GET: CarMakes
        public ActionResult Index()
        {
            ViewData [ "CarMakeOrigins" ] = from origin in db.CarMakeOrigins.Distinct()
                                            select new
                                            {
                                                origin.OriginId,
                                                origin.OriginTitle
                                            };

            ViewData [ "CarMakes" ] = db.CarMakes.Include ( m => m.CarMakeOrigin ).Include ( m => m.CarModels ).ToList ( );

            ViewData [ "DefaultOrigin" ] = db.CarMakeOrigins.First ( );

            return View();
        }
		
        public ActionResult GetCarMakesViewModel ( [DataSourceRequest] DataSourceRequest request )
        {
            var carmakes = from carMake in db.CarMakes
						   join carMakeOrigin in db.CarMakeOrigins on carMake.OriginId equals carMakeOrigin.OriginId
                           select new
                           {
								carMake.MakeId,
								carMake.OriginId,
								carMake.CarMakeOrigin.OriginTitle,
								carMake.MakeTitle,
								carMake.BrandLogoUrl
							};
              
            //var carMakes = db.CarMakes.Include ( m => m.CarMakeOrigins ).ToList ( );

            return Json ( carmakes.ToDataSourceResult ( request ) );
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateCarMake([DataSourceRequest] DataSourceRequest request, CarMake carMake)
        {
            if (carMake != null && ModelState.IsValid)
            {
                db.CarMakes.Add ( carMake );
                db.SaveChanges ( );
            }

            return Json ( new [ ] { carMake }.ToDataSourceResult ( request, ModelState ) );
        }

        [AcceptVerbs ( HttpVerbs.Post )]
        public ActionResult UpdateCarMake ( [DataSourceRequest] DataSourceRequest request, [Bind ( Prefix = "models" )]IEnumerable<CarMake> makes )
        {
            if ( makes != null && ModelState.IsValid )
            {
                foreach ( var make in makes )
                {
                    db.CarMakes.Attach ( make );
                    db.Entry ( make ).State = EntityState.Modified;
                    db.SaveChanges ( );
                }
            }

            return Json ( makes.ToDataSourceResult ( request, ModelState ) );
        }
    }
}