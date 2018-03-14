/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Akelote_e_Shop.Models;

using Akelote_e_Shop.Dtos;

namespace Akelote_e_Shop.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Item, ItemDto>();
            });



            var builder = config.CreateExpressionBuilder();
            var items = _context.Item.ProjectTo<ItemDto>(builder);
        }
    }
}*/