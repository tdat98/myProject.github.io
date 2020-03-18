using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Des { get; set; }
        public IList<Stock> Stocks { get; set; } = new List<Stock>();

    }
}
