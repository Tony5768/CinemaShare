﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class FilmProjection
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(Film))]
        public string FilmId { get; set; }

        public DateTime Date { get; set; }

        public ushort TotalTickets { get; set; }

        public virtual Cinema Cinema { get; set; }
        public virtual Film Film { get; set; }
        public virtual IEnumerable<ProjectionTicket> ProjectionTickets { get; set; }

    }
}
