using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Car
    {
        public int CarID {get; set;}
        public string CarMakeModel {get; set;}
        public int Mileage {get; set;}
        public string Date {get; set;}
        public bool Hold {get; set;}
        public bool Sold {get; set;}
    }
}