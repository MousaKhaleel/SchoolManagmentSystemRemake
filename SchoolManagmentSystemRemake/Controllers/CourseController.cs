using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystemRemake.Data;
using SchoolManagmentSystemRemake.Models;
using SchoolManagmentSystemRemake.ViewModels;

namespace SchoolManagmentSystemRemake.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext dbContext)
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
            ViewBag.Categories = _context.Categories.ToList();

            return View("CourseForm");
        }
        public async Task<IActionResult> Create(vmCourse viewModel)
        {
            var Course = new Course
            {
                CourseName = viewModel.CourseName,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                CategoryId = viewModel.CategoryId,
                Price = viewModel.Price,
                IsDeleted = false
            };


            await _context.Courses.AddAsync(Course);
            await _context.SaveChangesAsync();
            return View("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //get the std by id ,Course
            var course = _context.Courses.Where(x => x.Id == id).FirstOrDefault();
            //get the std by id ,Course
            ViewBag.Action = "Edit";
            return View("CourseForm", course);
        }
        public IActionResult Index()
        {
            var Course = _context.Courses.Where(c => !c.IsDeleted).ToList();
            //var Course = await _context.Course.Include(x => x.City).ToListAsync();
            //Course = await _context.Course.Include(x => x.EducationalLevel).ToListAsync();
            return View("Index", Course);
        }
        public async Task<IActionResult> Delete(int id)
        {
            //delete Course
            var Course = await _context.Courses.FindAsync(id);
            Course.IsDeleted = true;
            await _context.SaveChangesAsync();
            //delete Course
            return View("Index");
        }
    }
}
