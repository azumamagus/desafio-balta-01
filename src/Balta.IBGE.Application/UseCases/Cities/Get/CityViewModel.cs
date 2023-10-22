using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balta.IBGE.Application.UseCases.Cities.Get
{
    public class CityViewModel
    {
        public int Id { get; set; }
        public string State { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
