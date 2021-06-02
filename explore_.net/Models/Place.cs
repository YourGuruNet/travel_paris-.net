﻿using System;
namespace explore_.net.Models
{
    public class Place
    {
        public int PlaceId {get; set;}
        public string Title { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CreatorId { get; set; }
        public string Picture { get; set; }
    }
}
