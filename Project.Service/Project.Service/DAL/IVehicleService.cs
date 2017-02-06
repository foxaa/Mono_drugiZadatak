using PagedList;
using Project.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.DAL
{
    public interface IVehicleService
    {
        VehicleMakeViewModel FindVehicleMake(Guid id);
        VehicleModelViewModel FindVehicleModel(Guid id);
        IPagedList<VehicleMakeViewModel> SortVehicleMake(string search, string sortOrder, string searchString, int? page, int PageSize);
        IEnumerable<VehicleModelViewModel> SortVehicleModel(string search, string sortOrder, string searchString, int? page, int PageSize);
        void CreateVehicleMake(VehicleMakeViewModel vm);
        void CreateVehicleModel(VehicleModelViewModel vm);
        void DeleteVehicleMake(Guid id);
        void DeleteVehicleModel(Guid id);
        void EditVehicleMake(VehicleMakeViewModel vmk);
        void EditVehicleModel(VehicleModelViewModel vvm);
        IEnumerable<VehicleMakeViewModel> GetVehicleMakes();
        IEnumerable<VehicleModelViewModel> GetVehicleModels();
    }
}
