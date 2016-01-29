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
using AkitoRentalsSample.Models.Services;

namespace AkitoRentalsSample.Controllers
{
    public class CarMakesController : Controller
    {
        private readonly AkitoRentalsContext db = new AkitoRentalsContext ( );
		//private CarMakeServices services;
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
        public ActionResult UpdateCarMake ( [DataSourceRequest] DataSourceRequest request, CarMakeViewModel make )
        {
			CarMake carMake = db.CarMakes.Where ( m => m.MakeId == make.MakeId ).FirstOrDefault ( );

			if (make.CarMakeOrigin != null )
			{
				carMake.OriginId = make.CarMakeOrigin.OriginId;
			}
			carMake.MakeTitle = make.MakeTitle;

			if ( make != null && ModelState.IsValid )
            {
                db.CarMakes.Attach ( carMake );
                db.Entry ( carMake ).State = EntityState.Modified;
                db.SaveChanges ( );
            }

            return Json ( new [ ] { carMake }.ToDataSourceResult ( request, ModelState ) );
        }
    }
}