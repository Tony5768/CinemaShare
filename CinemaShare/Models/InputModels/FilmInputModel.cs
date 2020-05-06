﻿using Castle.Components.DictionaryAdapter;
using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaShare.Models
{
    public class FilmInputModel
    {

        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = 20)]
        public string Poster { get; set; }

        [Required]
        [StringLength(maximumLength: 300, 
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        [MinLength(1,
            ErrorMessage ="The {0} must conatains at least {1} genre")]
        public List<GenreType> Genre { get; set; }

        [Required]
        [StringLength(maximumLength: 40,
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = 5)]
        public string Director { get; set; }

        [Required]
        [StringLength(maximumLength: 300,
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = 5)]
        public string Cast { get; set; }
        
        [Required]
        [Range(0, 300, ErrorMessage = "Can only be between 0 .. 300")]
        public int Runtime { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public TargetAudience TargetAudience { get; set; }

    }
}
