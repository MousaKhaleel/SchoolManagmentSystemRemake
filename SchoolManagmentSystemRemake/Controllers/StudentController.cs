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

		public async Task<IActionResult> Create(string operation)
		{
			ViewBag.operation = operation;
			ViewBag.Cities = _context.Cities.ToList();
			ViewBag.EducationalLevel = _context.educationalLevels.ToList();
			ViewBag.Courses = _context.Courses.ToList();
           
			return View("StudentForm");
		}
		[HttpPost]
		public async Task<IActionResult> CreateEdit(vmStudent viewModel)
		{
			var Student = new Student
			{
				StudentName = viewModel.StudentName,
				DOB = viewModel.DOB,
				EducationalLevel = viewModel.EducationalLevel,
				City = viewModel.City,
				//Courses = viewModel.Courses,
				IsDeleted = false
			};


			await _context.Students.AddAsync(Student);
			await _context.SaveChangesAsync();
			return View("Index");
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int Id, string operation)
		{
            //get the std by id ,Student
            var student = await _context.Students.FindAsync(Id);
            //get the std by id ,Student
            ViewBag.operation = operation;
			return View("StudentForm",student);
		}

		public async Task<IActionResult> ViewDelete()
		{
            var Students = await _context.Students.ToListAsync();
            //var Students = await _context.Students.Include(x => x.City).ToListAsync();
            //Students = await _context.Students.Include(x => x.EducationalLevel).ToListAsync();
            return View("Index",Students);
		}
		public async Task<IActionResult> Delete(int Id)
		{
			//delete std
			return View("Index");
		}
	}
}
