using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeLavant.Domain.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace DeLavant.Domain.Courses
{
    public class Course : ContentItem
    {
       
        public List<string>? Elements { get; set; }
        public string? AccessType { get; set; }
        public List<string>? Access {  get; set; }
    }
}
