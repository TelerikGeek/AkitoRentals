using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkitoRentalsSample.Models.ViewModels
{
    public class CarMakeListingViewModel
    {
        /// <summary>
        /// Represents the Primary Key for the CarMake table.
        /// </summary>
        public int MakeId { get; set; }

        /// <summary>
        /// Represents the Country of Origin for the Make.
        /// </summary>
        public string OriginTitle { get; set; }

        /// <summary>
        /// Represents the title of the Make.
        /// </summary>
        [Required]
        [DisplayName ( "Make" )]
        public string MakeTitle { get; set; }
    }
}
