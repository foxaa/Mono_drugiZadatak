using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Service.DAL;
using Project.Service.Models;
using Project.Service.ViewModels;

namespace Project.MVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        IVehicleService v1 = new VehicleService();
        private VehicleService vehicleService = new VehicleService();
        private const int PageSize = 5;
        //private VehicleContext db = new VehicleContext();
        /*public ActionResult Index()
        {
            return View(vehicleService.GetVehicleModels());
        }*/
        public ActionResult Index(string search,string sortOrder,string searchString, int? page)
        {
            //ViewBag.SortNameMake = String.IsNullOrEmpty(sortOrder) ? "MakeName" : "";
            //ViewBag.SortNameMake = sortOrder== "MakeName"?"MakeName_desc":"Abrv";
            ViewBag.SortNameModel = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.SortNameMake = sortOrder == "MakeName" ? "MakeName_desc":"MakeName";
            ViewBag.SortAbrvModel = sortOrder == "Abrv" ? "Abrv_desc" : "Abrv";
            if (searchString == null)
            {
                return View(vehicleService.SortVehicleModel("", sortOrder, "",page,PageSize));
            }
            else
            {
                return View(vehicleService.SortVehicleModel(search, sortOrder, searchString,page,PageSize));
            }
        }
        //// GET: VehicleModels
        //public ActionResult Index()
        //{
        //    var vehicleModels = db.VehicleModels.Include(v => v.VehicleMake);
        //    return View(vehicleModels.ToList());
        //}

        //// GET: VehicleModels/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModelViewModel vehicleModel = vehicleService.FindVehicleModel((Guid)id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        //// GET: VehicleModels/Create
        //public ActionResult Create()
        //{
        //    ViewBag.VehicleMakeId = new SelectList(db.VehicleMakers, "Id", "Name");
        //    return View();
        //}
        public ActionResult Create()
        {
            ViewBag.VehicleMakeId = new SelectList(vehicleService.GetVehicleMakes(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleModelId,VehicleMakeId,Name,Abrv")] VehicleModelViewModel vehicleModel)
        {
            vehicleModel.VehicleModelId = Guid.NewGuid();
            
            if (ModelState.IsValid)
            {
                //vehicleModel.VehicleModelId = Guid.NewGuid();
                vehicleService.CreateVehicleModel(vehicleModel);
                return RedirectToAction("Index");
            }
            ViewBag.VehicleMakeId = new SelectList(vehicleService.GetVehicleMakes(), "Id", "Name", vehicleModel.VehicleMakeId);
            return View(vehicleModel);
        }
        //// POST: VehicleModels/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "VehicleModelId,VehicleMakeId,Name,Abrv")] VehicleModel vehicleModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        vehicleModel.VehicleModelId = Guid.NewGuid();
        //        db.VehicleModels.Add(vehicleModel);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.VehicleMakeId = new SelectList(db.VehicleMakers, "Id", "Name", vehicleModel.VehicleMakeId);
        //    return View(vehicleModel);
        //}
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModelViewModel vehicleModel = vehicleService.FindVehicleModel((Guid)id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleMakeId = new SelectList(vehicleService.GetVehicleMakes(), "Id", "Name", vehicleModel.VehicleMakeId);
            return View(vehicleModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleModelId,VehicleMakeId,Name,Abrv")] VehicleModelViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                vehicleService.EditVehicleModel(vehicleModel);
                return RedirectToAction("Index");
            }
            ViewBag.VehicleMakeId = new SelectList(vehicleService.GetVehicleMakes(), "Id", "Name", vehicleModel.VehicleMakeId);
            return View(vehicleModel);
        }

        //// GET: VehicleModels/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VehicleModel vehicleModel = db.VehicleModels.Find(id);
        //    if (vehicleModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.VehicleMakeId = new SelectList(db.VehicleMakers, "Id", "Name", vehicleModel.VehicleMakeId);
        //    return View(vehicleModel);
        //}

        //// POST: VehicleModels/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "VehicleModelId,VehicleMakeId,Name,Abrv")] VehicleModel vehicleModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(vehicleModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.VehicleMakeId = new SelectList(db.VehicleMakers, "Id", "Name", vehicleModel.VehicleMakeId);
        //    return View(vehicleModel);
        //}

        //// GET: VehicleModels/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VehicleModel vehicleModel = db.VehicleModels.Find(id);
        //    if (vehicleModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(vehicleModel);
        //}
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModelViewModel vehicleModel = vehicleService.FindVehicleModel((Guid)id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            VehicleModelViewModel vehicleModel = vehicleService.FindVehicleModel((Guid)id);
            vehicleService.DeleteVehicleModel(id);
            return RedirectToAction("Index");
        }
        //// POST: VehicleModels/Delete/5

        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    VehicleModel vehicleModel = db.VehicleModels.Find(id);
        //    db.VehicleModels.Remove(vehicleModel);
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
