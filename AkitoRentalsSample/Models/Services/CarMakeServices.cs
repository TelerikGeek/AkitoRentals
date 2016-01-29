using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AkitoRentalsSample.Models;
using AkitoRentalsSample.Models.ViewModels;

namespace AkitoRentalsSample.Models.Services
{
	public class CarMakeServices : IDisposable
	{
		readonly private AkitoRentalsContext _context;

		public CarMakeServices ( AkitoRentalsContext context )
		{
			this._context = context;
		}

		public IEnumerable<CarMakeViewModel> Read()
		{
			return _context.CarMakes.Include(m => m.CarMakeOrigin).Select( make => new CarMakeViewModel
			{
				MakeId = make.MakeId,
				OriginId = make.OriginId,
				OriginTitle = make.CarMakeOrigin.OriginTitle,
				MakeTitle = make.MakeTitle,
				BrandLogoUrl = make.BrandLogoUrl,
				CarMakeOrigin = make.CarMakeOrigin
			});
		}

		public void Create (CarMakeViewModel carMake)
		{
			var make = new CarMake ( );

			make.MakeTitle = carMake.MakeTitle;
			make.BrandLogoUrl = carMake.BrandLogoUrl;
			make.OriginId = carMake.OriginId;

			_context.CarMakes.Add ( make );
			_context.SaveChanges ( );

			carMake.MakeId = make.MakeId;
		}

		public void Update (CarMakeViewModel carMake)
		{
			var make = new CarMake ( );

			make.MakeId = carMake.MakeId;
			make.OriginId = carMake.OriginId;
			make.MakeTitle = carMake.MakeTitle;

			if (carMake.BrandLogoUrl != null)
			{
				make.BrandLogoUrl = carMake.BrandLogoUrl;
			}

			if (carMake.CarMakeOrigin != null)
			{
				make.OriginId = carMake.CarMakeOrigin.OriginId;
			}

			_context.CarMakes.Attach ( make );
			_context.Entry ( make ).State = EntityState.Modified;
			_context.SaveChanges ( );
		}

		public void Destroy(CarMakeViewModel carMake)
		{
			var make = new CarMake ( );

			make.MakeId = carMake.MakeId;

			_context.CarMakes.Attach ( make );

			_context.CarMakes.Remove ( make );

			_context.SaveChanges ( );
		}

		public void Dispose()
		{
			_context.Dispose ( );
		}
	}
}