using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TRPManagement.DTOs
{
    public class ProgramDTO
    {
        public int ProgramId { get; set; }
        [Required, StringLength(150)]
        public string ProgramName { get; set; }
        [Required, Range(0.0, 10.0)]
        public decimal TRPScore { get; set; }
        [Required]
        public int ChannelId { get; set; }
        [Required]
        public TimeSpan AirTime { get; set; }
    }
}