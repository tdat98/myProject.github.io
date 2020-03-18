using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Resources
{
    public class StockViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Soluong { get; set; }
        public string Noisx { get; set; }
        public int InventoryId { get; set; }
        public string InventoryName { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
    }
}
