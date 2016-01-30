using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkitoRentalsSample.Models.ViewModels
{
    public class CarMakeViewModel
    {
        /// <summary>
        /// Represents the Primary Key for the CarMake table.
        /// </summary>
        public int MakeId { get; set; }

		/// <summary>
		/// Represents the Foreign Key to the CarMakeOrigin table.
		/// </summary>
		[UIHint ( "CarMakeOriginEditor" ), Required]
		public int OriginId { get; set; }

		/// <summary>
		/// Represents the Origin Title string value
		/// </summary>
		public string OriginTitle { get; set; }

        /// <summary>
        /// Represents the title of the Make.
        /// </summary>
        [Required]
        [DisplayName ( "Make" )]
        public string MakeTitle { get; set; }

		[UIHint("BrandLogoUrlEditor")]
		/// <summary>
		/// Represents the Brand Logo Url value.
		/// </summary>
        public string BrandLogoUrl { get; set; }
        
		
        public CarMakeOrigin CarMakeOrigin { get; set; }

        public ICollection<CarModel> CarModels { get; set; }
    }
}
