using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkitoRentalsSample.Models.ViewModels
{
    public class CarMakeOriginListingViewModel
    {
        /// <summary>
        /// Represents the Primary Key of the CarMakeOrigin table.
        /// </summary>
        public int OriginId { get; set; }

        /// <summary>
        /// Represents the Title of the County of Origin for the Make.
        /// </summary>
        public string OriginTitle { get; set; }

        /// <summary>
        /// Represents the total number of Makes that belongs to each Make.
        /// </summary>
        public int TotalMakeCount { get; set; }
    }
}
