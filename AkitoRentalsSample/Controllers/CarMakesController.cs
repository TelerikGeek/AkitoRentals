using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using AkitoRentalsSample.Models;

namespace AkitoRentalsSample.Controllers
{
    public class CarMakesController : Controller
    {
        private readonly AkitoRentalsContext db = new AkitoRentalsContext ( );
        // GET: CarMakes
        public ActionResult Index()
        {
            ViewData [ "CarMakeOrigins" ] = from origin in db.CarMakeOrigins
                                            join make in db.CarMakes on origin.OriginId equals make.OriginId
                                            select new
                                            {
                                                origin.OriginId,
                                                origin.OriginTitle,
                                                TotalMakeCount = origin.CarMakes.Count
                                            };

            return View();
        }

        public ActionResult GetCarMakesViewModel ( [DataSourceRequest] DataSourceRequest request )
        {
            var carmakes = ( from make in db.CarMakes.ToList ( )
                              join origin in db.CarMakeOrigins.ToList ( ) on make.OriginId equals origin.OriginId
                              join model in db.CarModels.ToList() on make.MakeId equals model.MakeId
                              select new
                              {
                                  make.MakeId,
                                  origin.OriginTitle,
                                  make.MakeTitle

                              } ).ToList ( );
              

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
    }
}