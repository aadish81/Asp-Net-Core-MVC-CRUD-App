using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using LabAssignment.Data;
using LabAssignment.Models;
using Microsoft.AspNetCore.Mvc;


namespace LabAssignment.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StudentsController(ApplicationDbContext db) 
        {
            _db = db;
        }
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NewStudent()
        {
           return View();
            
        }
        [HttpPost]
        public IActionResult NewStudent(Student std)
        {
            _db.Students.Add(std);
            _db.SaveChanges();
            TempData["student"] = JsonSerializer.Serialize(std);
            return RedirectToAction("CurrentRecord");
        }

        public IActionResult CurrentRecord()
        {
            var jsonstd = TempData["student"] as string;
            var student = jsonstd != null ? JsonSerializer.Deserialize<Student>(jsonstd) : null;
            if (student != null)
            {
                return View(student);
            }
            return NotFound();
        }
        [Route("/students")]
        public IActionResult StudentRecord()
        {
            List<Student> studentList = _db.Students.ToList();

            //Student? student = _db.Students.Find(id);
             if (studentList == null)
            {
                return NotFound("User Not Found");
            }
            return View(studentList);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Student? std = _db.Students.Find(id);
            if(std == null)
            {
                return NotFound();
            }
            return View(std);
        }
        [HttpPost]
        public IActionResult Edit(Student? obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(obj);
                _db.SaveChanges();

                return RedirectToAction("StudentRecord");
            }
            return RedirectToAction("StudentRecord");
        }
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Student? std = _db.Students.Find(id);
            if(std == null)
            {
                return NotFound();
            }
            return View(std);
        }
        [HttpPost]
        public IActionResult Delete(Student? std)
        { 
            if(std == null)
            {
                return NotFound();
            }
            
            _db.Students.Remove(std);
            _db.SaveChanges();
               
            
            return RedirectToAction("StudentRecord");

        }
    }
}
