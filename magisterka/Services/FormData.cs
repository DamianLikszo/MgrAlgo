using System;
using magisterka.Interfaces;
using magisterka.Models;

namespace magisterka.Services
{
    public class FormData : IFormData
    {
        public GranuleSet GranuleSet { get; set; }
        public string PathSource { get; set; }
    }
}
