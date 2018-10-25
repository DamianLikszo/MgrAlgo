using magisterka.Interfaces;
using magisterka.Models;
using System.Collections.Generic;

namespace magisterka.Services
{
    public class DevService : IDevService
    {
        public ZbGran pushGran()
        {
            return new ZbGran
            {
                Granules = new List<Granula> {
                new Granula { Inside = new List<int> { 0, 0, 0, 1 } },
                new Granula { Inside = new List<int> { 0, 0, 1, 1 } },
                new Granula { Inside = new List<int> { 0, 1, 1, 1 } },
                new Granula { Inside = new List<int> { 1, 0, 1, 1 } },
                new Granula { Inside = new List<int> { 1, 1, 0, 1 } }
            }
            };
        }
    }
}
