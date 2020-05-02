﻿using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class FilmData
    {
        public FilmData()
        {
            this.Genre = new HashSet<GenreType>();
           // this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string FilmId { get; set; }

       // [ForeignKey("fk_film")]
        public virtual Film Film { get; set; }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        public string Poster { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IEnumerable<GenreType> Genre { get; set; }

        [MaxLength(50)]
        public string Director { get; set; }

        public string Cast { get; set; }

        public int Runtime { get; set; }

        public DateTime ReleaseDate { get; set; }

        public TargetAudience TargetAudience { get; set; }

        
    }
}
