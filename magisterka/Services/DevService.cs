using magisterka.Interfaces;
using magisterka.Models;
using System.Collections.Generic;

namespace magisterka.Services
{
    public class DevService : IDevService
    {
        public GranuleSet pushGran()
        {
            return new GranuleSet
            {
                Granules = new List<Granule>
                {
                    new Granule(new List<int> {0, 0, 0, 1}),
                    new Granule(new List<int> {0, 0, 1, 1}),
                    new Granule(new List<int> {0, 1, 1, 1}),
                    new Granule(new List<int> {1, 0, 1, 1}),
                    new Granule(new List<int> {1, 1, 0, 1})
                }
            };
        }
    }
}
