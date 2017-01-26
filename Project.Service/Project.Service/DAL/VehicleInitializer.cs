using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Project.Service.Models;


namespace Project.Service.DAL
{
    public class VehicleInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges<VehicleContext>
    {
        protected override void Seed(VehicleContext context)
        {
            var makers = new List<VehicleMake>
            {
                new VehicleMake {Name="Mercedes",Abrv="Mecka" },
                new VehicleMake {Name="Bayerische Motoren Werke AG",Abrv="BMW" }
            };
            makers.ForEach(s => context.VehicleMakers.Add(s));//dodaje iz listu u tablicu VehicleMaker
            context.SaveChanges();


            var models = new List<VehicleModel>
            {
                new VehicleModel {VehicleMakeId=Guid.NewGuid(),Name="Golf",Abrv="VW" },
                new VehicleModel {VehicleMakeId=Guid.NewGuid(),Name="X5",Abrv="BMW" }
            };
            models.ForEach(a => context.VehicleModels.Add(a));
            context.SaveChanges();//nije potrebno poslije svakog entitya 
        }
    }
}