using AutoMapper;
using QLKHO3.Models;
using QLKHO3.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Stock, StockViewModel>();
            CreateMap<StockViewModel, Stock>();
        }
    }
}
