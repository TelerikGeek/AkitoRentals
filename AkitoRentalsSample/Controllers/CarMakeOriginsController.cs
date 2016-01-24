using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using AkitoRentalsSample.Models;

namespace AkitoRentalsSample.Controllers
{
    public class CarMakeOriginsController : Controller
    {
        readonly private AkitoRentalsContext db = new AkitoRentalsContext ( );

        // GET: CarMakeOrigins
        public ActionResult Index ( )
        {
            return View ( );
        }

        public ActionResult GetCarMakeOrigins ( [DataSourceRequest] DataSourceRequest request )
        {
            var CarMakeOriginsViewModel = from origin in db.CarMakeOrigins
                                          join make in db.CarMakes on origin.OriginId equals make.OriginId
                                          select new
                                          {
                                              origin.OriginId,
                                              origin.OriginTitle,
                                              TotalMakeCount = origin.CarMakes.Count
                                          };

            return Json ( CarMakeOriginsViewModel.Distinct().ToDataSourceResult ( request ) );
        }

        public ActionResult GetCarMakes(int originId, [DataSourceRequest] DataSourceRequest request)
        {
            var CarMakesViewModel = from make in db.CarMakes
                                    select new
                                    {
                                        make.MakeId,
                                        make.OriginId,
                                        make.MakeTitle
                                    };

            return Json ( CarMakesViewModel.Where(m => m.OriginId == originId).ToDataSourceResult ( request ) );
        }
    }
}