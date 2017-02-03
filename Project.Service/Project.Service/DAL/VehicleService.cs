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
        public VehicleModelViewModel FindVehicleModel(Guid id)
        {
            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            return Mapper.Map<VehicleModelViewModel>(vehicleModel);
        }
        public IEnumerable<VehicleMakeViewModel> SortVehicleMake(string sortOrder,string searchString)
        {
            //var vmks = Mapper.Map < IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.ToList());
            switch (searchString)
            {
                case "Name":
                    switch (sortOrder)
                    {
                        case "Name_desc":
                            //vmks = vmks.OrderByDescending(vmk => vmk.Name);
                            //return Mapper.Map<VehicleMakeViewModel>(db.VehicleMakers.OrderByDescending(v => v.Name));
                            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.Where(vm=>vm.Name.Contains(searchString)).OrderByDescending(v => v.Name).ToList());
                        //break;
                        case "Abrv":
                            //return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.OrderBy(v => v.Abrv).ToList());
                            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.Where(vm => vm.Name.Contains(searchString)).OrderBy(v => v.Abrv).ToList());
                        case "Abrv_desc":
                            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.Where(vm => vm.Name.Contains(searchString)).OrderByDescending(v => v.Abrv).ToList());

                        default:
                            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.Where(vm => vm.Name.Contains(searchString)).OrderBy(v => v.Name).ToList());
                    }
                case "Abrv":
                    switch(sortOrder)
                    {
                        case "Name_desc":
                            //vmks = vmks.OrderByDescending(vmk => vmk.Name);
                            //return Mapper.Map<VehicleMakeViewModel>(db.VehicleMakers.OrderByDescending(v => v.Name));
                            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.OrderByDescending(v => v.Name).ToList());
                        //break;
                        case "Abrv":
                            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.OrderBy(v => v.Abrv).ToList());
                        case "Abrv_desc":
                            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.OrderByDescending(v => v.Abrv).ToList());

                        default:
                            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.OrderBy(v => v.Name).ToList());
                    }
                default:
                    switch(sortOrder)
                    {
                        case "Name_desc":
                            //vmks = vmks.OrderByDescending(vmk => vmk.Name);
                            //return Mapper.Map<VehicleMakeViewModel>(db.VehicleMakers.OrderByDescending(v => v.Name));
                            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.OrderByDescending(v => v.Name).ToList());
                        //break;
                        case "Abrv":
                            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.OrderBy(v => v.Abrv).ToList());
                        case "Abrv_desc":
                            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.OrderByDescending(v => v.Abrv).ToList());

                        default:
                            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.OrderBy(v => v.Name).ToList());
                    }
            }   
        }
        public IEnumerable<VehicleModelViewModel> SortVehicleModel(string sortOrder)//ide ovo IEnumerable zato sto se mora vratit cijela lista tj tablica
        {
            switch(sortOrder)
            {
                case "Name_desc":
                    return Mapper.Map<IEnumerable<VehicleModelViewModel>>(db.VehicleModels.OrderByDescending(v => v.Name).ToList());
                case "Abrv":
                    return Mapper.Map<IEnumerable<VehicleModelViewModel>>(db.VehicleModels.OrderBy(v => v.Abrv).ToList());
                case "Abrv_desc":
                    return Mapper.Map<IEnumerable<VehicleModelViewModel>>(db.VehicleModels.OrderByDescending(v => v.Abrv).ToList());
                default:
                    return Mapper.Map<IEnumerable<VehicleModelViewModel>>(db.VehicleModels.OrderBy(v => v.Name).ToList());
            }
        }
        public void CreateVehicleMake(VehicleMakeViewModel vm)
        {
            db.VehicleMakers.Add(Mapper.Map<VehicleMake>(vm));
            db.SaveChanges();
        }
        public void CreateVehicleModel(VehicleModelViewModel vm)
        {
            db.VehicleModels.Add(Mapper.Map<VehicleModel>(vm));
            db.SaveChanges();
        }
        
        public void DeleteVehicleMake(Guid id)
        {
            VehicleMake vM = db.VehicleMakers.Find(id);
            db.VehicleMakers.Remove(vM);
            db.SaveChanges();
        }
        public void DeleteVehicleModel(Guid id)
        {
            VehicleModel vm = db.VehicleModels.Find(id);
            db.VehicleModels.Remove(vm);
            db.SaveChanges();
        }
        public void EditVehicleMake(VehicleMakeViewModel vmk)
        {
            //db.Entry<VehicleMake>(vmk).State = EntityState.Modified;
            
            db.Entry(Mapper.Map<VehicleMake>(vmk)).State = EntityState.Modified;
            //db.VehicleMakers.Add(Mapper.Map<VehicleMake>(vm));
            db.SaveChanges();   
        }
        public void EditVehicleModel(VehicleModelViewModel vvm)
        {
            db.Entry(Mapper.Map<VehicleModel>(vvm)).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<VehicleMakeViewModel> GetVehicleMakes()
        {
            
            return Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakers.ToList());
        }
        public IEnumerable<VehicleModelViewModel> GetVehicleModels()
        {
            return Mapper.Map<IEnumerable<VehicleModelViewModel>>(db.VehicleModels.ToList());
        }
        //public VehicleMake Retrieve 
    }
}
