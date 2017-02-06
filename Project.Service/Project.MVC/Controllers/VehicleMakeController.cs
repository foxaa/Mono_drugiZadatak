using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using Project.Service;
using Project.Service.DAL;
using Project.Service.Models;
using Project.Service.ViewModels;


namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private VehicleService vehicleService = new VehicleService();
        private const int PageSize = 5;
        // GET: VehicleMake

        /*private VehicleMakeController()
         {
             this.vehicleService = VehicleService.Instance;
         }*/
        /*public ActionResult Index()
        {
            return View(vehicleService.GetVehicleMakes());
           // return View();
        }*/
        public ActionResult Index(string search,string sortOrder,string searchString,int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.AbrvSort = sortOrder == "Abrv" ? "Abrv_desc" : "Abrv";
            //VehicleMakeViewModel vehicleMake = vehicleService.SortVehicleMake(sortOrder);
            /*var vehicles = from v in vehicleService.GetVehicleMakes() select v;
            switch(sortOrder)
            {
                case "Name":
                    vehicles = vehicles.OrderByDescending(v => v.Name);
                    break;
                default:
                    vehicles = vehicles.OrderBy(v => v.Name);
                    break;
            }*/
            if (searchString == null)
            {
                return View(vehicleService.SortVehicleMake("",sortOrder,"",page,PageSize));
            }
            else
            {
                return View(vehicleService.SortVehicleMake(search,sortOrder, searchString,page,PageSize));
            }
        }
        public ActionResult Details(Guid? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMakeViewModel vehicleMake = vehicleService.FindVehicleMake((Guid)id);
            if(vehicleMake==null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        //// GET: VehicleMake/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VehicleMake vehicleMake = db.VehicleMakers.Find(id);
        //    if (vehicleMake == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(vehicleMake);
        //}

        //// GET: VehicleMake/Create
        /*public ActionResult Create()
        {
            return View();
        }*/

        //// POST: VehicleMake/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Abrv")] VehicleMake vehicleMake)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        vehicleMake.Id = Guid.NewGuid();
        //        db.VehicleMakers.Add(vehicleMake);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(vehicleMake);
        //}

        // GET: VehicleMake/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Id,Name,Abrv")] VehicleMakeViewModel vehicleMakeView)
        {
            vehicleMakeView.Id = System.Guid.NewGuid();
            if (ModelState.IsValid)
            {
                vehicleService.CreateVehicleMake(vehicleMakeView);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Krivo unesen maker");
            }
            return View(vehicleMakeView);

        }
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMakeViewModel vehicleMake = vehicleService.FindVehicleMake((Guid)id);
            //VehicleMake vehicleMake = db.VehicleMakers.Find(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(Guid id)
        {
            VehicleMakeViewModel vehicleMakeView = vehicleService.FindVehicleMake((Guid)id);
            //VehicleMake vehicleMake = db.VehicleMakers.Find(id);
            vehicleService.DeleteVehicleMake(id);
            //db.VehicleMakers.Remove(vehicleMake);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }



        //// GET: VehicleMake/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    /*if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }*/
        //    VehicleMake vehicleMake = db.VehicleMakers.Find(id);
        //    if (vehicleMake == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(vehicleMake);
        //}
        public ActionResult Edit(Guid? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMakeViewModel vehicleMake = vehicleService.FindVehicleMake((Guid)id);
            if(vehicleMake==null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VehicleMakeViewModel vm)
        {

                vehicleService.EditVehicleMake(vm);
                //db.Entry(vehicleMake).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");

        }
        //// POST: VehicleMake/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Abrv")] VehicleMake vehicleMake)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(vehicleMake).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(vehicleMake);
        //}

        //// GET: VehicleMake/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VehicleMake vehicleMake = db.VehicleMakers.Find(id);
        //    if (vehicleMake == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(vehicleMake);
        //}

        //// POST: VehicleMake/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    VehicleMake vehicleMake = db.VehicleMakers.Find(id);
        //    db.VehicleMakers.Remove(vehicleMake);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
