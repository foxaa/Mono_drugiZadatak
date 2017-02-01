using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Project.Service.Models;
using Project.Service.ViewModels;

namespace Project.MVC.App_Start
{
    public static class MappingConfig 
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
                config.CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();
            });
        }
    }
}