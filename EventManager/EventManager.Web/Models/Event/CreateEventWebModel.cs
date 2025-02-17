﻿using EventManager.Common.Constants;
using EventManager.Data.Models.Picture;
using EventManager.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Web.Models.Event
{
    public class CreateEventWebModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public HashSet<EventTypes> Types { get; set; }
        public string? Webpage { get; set; }
        public required string Address { get; set; }
        public bool IsActivity { get; set; }
        public bool IsThirdParty { get; set; }

    }
}
