using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    [MetadataType(typeof(NewsMetadata))]
    public partial class News
    {
        private class NewsMetadata
        {
            [Required(ErrorMessage = "Title must fill.")]
            public string Title { get; set; }

            [Required(ErrorMessage = "Subtitle must fill.")]
            public string Subtitle { get; set; }
        }
    }

    [MetadataType(typeof(CarMetadata))]
    public partial class CarAnnouncement
    {
        private class CarMetadata
        {
            [Required(ErrorMessage = "Production date must fill.")]
            public Nullable<System.DateTime> CarProductionDate { get; set; }

            [Required(ErrorMessage = "Motor must fill.")]
            public Nullable<decimal> CarMotor { get; set; }

            [Required(ErrorMessage = "About must fill.")]
            public string About { get; set; }

            [Required(ErrorMessage = "Model must fill.")]
            public Nullable<int> CarModelID { get; set; }

            [Required(ErrorMessage = "Gear must fill.")]
            public Nullable<int> CarGearID { get; set; }

            [Required(ErrorMessage = "Fuel must fill.")]
            public Nullable<int> CarFuelID { get; set; }

            [Required(ErrorMessage = "City must fill.")]
            public Nullable<int> CityID { get; set; }

            [Required(ErrorMessage = "Color must fill.")]
            public Nullable<int> ColorID { get; set; }

            [Required(ErrorMessage = "Ride must fill.")]
            public Nullable<int> Ride { get; set; }

            [Required(ErrorMessage = "Price must fill.")]
            public Nullable<int> Price { get; set; }

            [Required(ErrorMessage = "Title must fill.")]
            public string Title { get; set; }
        }
    }
}