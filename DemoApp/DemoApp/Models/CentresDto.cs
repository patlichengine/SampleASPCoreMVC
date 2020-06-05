using DemoApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Models
{
    public class CentresDto
    {
        public Guid Id { get; set; }
        public string CentreNumber { get; set; }
        public string CentreName { get; set; }
        public byte[] CentreImage { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }

        public ApplicationUsers CreatedByNavigation { get; set; }
        public  SchoolCategories SchoolCategory { get; set; }
        public IEnumerable<CentreSanctions> CentreSanctions { get; set; }
    }

    public class CentreCreateDto : CentreManipulationDto
    {
        [Required]
        [MaxLength(150)]
        [EmailAddress(ErrorMessage = "Enter a valid Email Address")]

        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
    }


    public class CentreUpdateDto : CentreManipulationDto //: IValidatableObject
    {
        [Required(ErrorMessage = "Please select a centre to update")]
        public Guid Id { get; set; }
    }


    public abstract class CentreManipulationDto
    {

        [Required(ErrorMessage = "Centre Number must exist")]
        [MaxLength(100)]
        public string CentreNumber { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        public string CentreName { get; set; }

        [Required(ErrorMessage = "The Surname must be specified and should not be more that 20 characters")]
        public Guid? SchoolCategoryId { get; set; }

        public byte[] CentrePhoto { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public bool? IsActive { get; set; } = true;
    }

}
