using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Project.Service.DAL;
using Project.Service.Models;
using Project.Service.ViewModels;
using AutoMapper;
using PagedList;



namespace Project.Service.DAL
{
    public class VehicleService
    {
        private VehicleModelViewModel VehicleModel;
        private VehicleMakeViewModel VehicleMake;
        private VehicleContext db;
        private static VehicleService instance;
        IPagedList VehiclesList;

        public VehicleService()
        {
            db = new VehicleContext();
        }
        public static VehicleService Instance
        {
            get
            {
                if(instance==null)
                {
                    instance = new VehicleService();
                }
                return instance;
            }
        }
        public VehicleMakeViewModel FindVehicleMake(Guid id)
        {
            VehicleMake vehicleMake=db.VehicleMakers.Find(id);
            return Mapper.Map<VehicleMakeViewModel>(vehicleMake);//mapping iz source(vehicleMake model) u VehicleMakeViewModel

        }
        public void CreateVehicleMake(VehicleMakeViewModel vm)
        {
            db.VehicleMakers.Add(Mapper.Map<VehicleMake>(vm));
            db.SaveChanges();
        }
        public void DeleteVehicleMake(Guid id)
        {
            VehicleMake vM = db.VehicleMakers.Find(id);
            db.VehicleMakers.Remove(vM);
            db.SaveChanges();
        }
        public void EditVehicleMake(VehicleMakeViewModel vmk)
        {
            //db.Entry<VehicleMake>(vmk).State = EntityState.Modified;
            
            db.Entry(Mapper.Map<VehicleMake>(vmk)).State = EntityState.Modified;
            //db.VehicleMakers.Add(Mapper.Map<VehicleMake>(vm));
            db.SaveChanges();   
        }

        public IEnumerable<VehicleMakeViewModel> GetVehicleMakes()
        {
            
            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.ToList());
        }
        //public VehicleMake Retrieve 
    }
}
