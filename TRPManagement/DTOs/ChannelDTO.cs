using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TRPManagement.DTOs
{
    public class ChannelDTO
    {
        public int ChannelId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Channel Name cannot exceed 100 characters.")]
        public string ChannelName { get; set; }

        [Required]
        [Range(1900, int.MaxValue, ErrorMessage = "Established Year must be between 1900 and the current year.")]
        public int EstablishedYear { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        public string Country { get; set; }
    }
}