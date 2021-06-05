using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscapeRoomWebAppCore.Areas.Admin.DTOs
{
    public class EscapeRoomDTO
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "String length must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 10, ErrorMessage = "String length must be greater than 10 characters")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Time Limit")]
        [Range(1, 600)]
        public int TimeLimitMinutes { get; set; }

        [Required]
        [Display(Name = "Min Players")]
        [Range(1, 10)]
        public int MinimumPlayers { get; set; }

        [Required]
        [Display(Name = "Max Players")]
        [Range(1, 30)]
        public int MaximumPlayers { get; set; }

        [Required]
        [Display(Name = "Minimum Age")]
        [Range(1, 50)]
        public int MinimumPlayerAge { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Phone number")]
        //[RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Incorrect Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [Range(1, 100)]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "Fear Level")]
        [Range(1, 5)]
        public byte FearLevel { get; set; }

        [Required]
        [Display(Name = "Difficulty Level")]
        [Range(1, 5)]
        public byte DifficultyLevel { get; set; }

        public byte[] LogoBytes { get; set; }
        public IFormFile Logotype { get; set; }
        //public virtual IEnumerable<Photo> Photos { get; set; }
    }
}
