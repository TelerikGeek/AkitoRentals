using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
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

			ViewData [ "CarMakeOrigins" ] = from origin in db.CarMakeOrigins.Distinct ( )
											select new
											{
												origin.OriginId,
												origin.OriginTitle
											};
			ViewData [ "DefaultOrigin" ] = db.CarMakeOrigins.First ( );

			if ( make != null && ModelState.IsValid )
            {
				carMake.MakeTitle = make.MakeTitle;
				carMake.OriginId = make.OriginId;

				carMake.CarMakeOrigin = db.CarMakeOrigins.Where ( o => o.OriginId == make.OriginId ).FirstOrDefault ( );
				make.CarMakeOrigin = carMake.CarMakeOrigin;

				try
				{
					db.CarMakes.Attach ( carMake );
					db.Entry ( carMake ).State = EntityState.Modified;
					db.SaveChanges ( );
				}
				catch ( DbEntityValidationException ex)
				{
					Debug.WriteLine ( "[Car Make Module] DbEntityValidationException Caught" );
					Debug.WriteLine ( "----------------------------------------------------" );
					var errorMessages = ex.EntityValidationErrors.SelectMany ( x => x.ValidationErrors ).Select ( x => x.ErrorMessage );

					var fullErrorMessage = string.Join ( ";\n", errorMessages );

					var exceptionMessage = string.Concat ( ex.Message, "The validation errors are: ", fullErrorMessage );

					Debug.WriteLine ( exceptionMessage );
				}
            }

			Debug.WriteLine ( "[Car Make Module] Record values:" );
			Debug.WriteLine ( "{" );
			Debug.WriteLine ( "\tMakeId : " + make.MakeId.ToString ( ) );
			Debug.WriteLine ( "\tOriginId : " + make.OriginId.ToString ( ) );
			Debug.WriteLine ( "\tMakeTitle : " + make.MakeTitle );
			Debug.WriteLine ( "\tOriginTitle : " + make.CarMakeOrigin.OriginTitle );
			Debug.WriteLine ( "\tBrandLogoUrl : " + make.BrandLogoUrl );
			Debug.WriteLine ( "}" );

			return Json ( new [ ] { make }.ToDataSourceResult ( request, ModelState ) );
        }
    }
}