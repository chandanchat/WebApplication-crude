using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_crude.Models;
using Newtonsoft.Json;

namespace WebApplication_crude.Controllers
{
    public class StudentController1 : Controller
    {
        DatabaseContext db;
        public StudentController1(DatabaseContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Index(int id)
        {
            Employee obj = new Employee();
            if (id>0)
            {
                var dd = db.Employees.Where(m => m.id == id).FirstOrDefault();
                obj.name= dd.name;
                obj.age = dd.age;
              
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Index(Employee empobj)
        {
            if(empobj.id>0)
            {
                db.Entry(empobj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                db.Employees.Add(empobj);
            }
            
            db.SaveChanges();
            return RedirectToAction("GetData");
        }
        public ActionResult GetData()
        {
           var data= db.Employees.ToList();
           // var d= JsonConvert.SerializeObject(data);
            return View(data);
            
        }
        public ActionResult EmpDelete(int id)
        {
          var data=  db.Employees.Where(m=>m.id==id).FirstOrDefault();
            db.Employees.Remove(data);
            db.SaveChanges();
            return RedirectToAction("GetData");
        }
    }
}
