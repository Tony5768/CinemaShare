﻿using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaShare.Models
{
    public class ExtendedFilmCardViewModel: FilmCardViewModel
    {
        public string Cast { get; set; }
        public string Director { get; set; }
        public int Runtime { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TargetAudience TargetAudience { get; set; }
    }
}
