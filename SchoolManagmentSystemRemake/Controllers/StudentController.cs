using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystemRemake.Data;
using SchoolManagmentSystemRemake.Models;
using SchoolManagmentSystemRemake.ViewModels;

namespace SchoolManagmentSystemRemake.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
        public StudentController(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        //public IActionResult Index()
        //{
        //	return View();
        //}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Action = "Create";
            ViewBag.Cities = _context.Cities.ToList();
            ViewBag.EducationalLevel = _context.educationalLevels.ToList();
            ViewBag.Courses = _context.Courses.ToList();

            return View("StudentForm");
        }
        [HttpPost]
        public async Task<IActionResult> Create(vmStudent viewModel)
        {
            var Student = new Student
            {
                StudentName = viewModel.StudentName,
                DOB = viewModel.DOB,
                EducationalLevelId = viewModel.EducationalLevelId,
                CityId = viewModel.CityId,
                //Courses = viewModel.Courses,
                IsDeleted = false
            };


            await _context.Students.AddAsync(Student);
            await _context.SaveChangesAsync();
            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //get the std by id ,Student
            var student = await _context.Students.FindAsync(id);

            await _context.SaveChangesAsync();
            //get the std by id ,Student
            ViewBag.operation = "Edit";
            return View("StudentForm", student);
        }

        public async Task<IActionResult> Index()
        {
            var Students = await _context.Students.Where(c => !c.IsDeleted).ToListAsync();
            //var Students = await _context.Students.Include(x => x.City).ToListAsync();
            //Students = await _context.Students.Include(x => x.EducationalLevel).ToListAsync();
            return View("Index", Students);
        }
        public async Task<IActionResult> Delete(int id)
        {
            //delete std
            var student = await _context.Students.FindAsync(id);
            student.IsDeleted = true;
            await _context.SaveChangesAsync();
            //delete std
            return View("Index");
        }
    }
}
