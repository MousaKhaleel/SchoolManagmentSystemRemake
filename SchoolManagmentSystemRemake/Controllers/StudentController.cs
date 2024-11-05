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
		public async Task<IActionResult> Index()
		{
			var Students = await _context.Students.Where(c => !c.IsDeleted).ToListAsync();
			//var Students = await _context.Students.Include(x => x.City).ToListAsync();
			//Students = await _context.Students.Include(x => x.EducationalLevel).ToListAsync();
			return View("Index", Students);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			ViewBag.Action = "Create";
			ViewBag.Cities = _context.Cities.ToList();
			ViewBag.EducationalLevel = _context.educationalLevels.ToList();
			ViewBag.Courses = _context.Courses.Where(c => !c.IsDeleted).ToList();

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
				CourseId = viewModel.CourseId,
				IsDeleted = false
			};

			await _context.Students.AddAsync(Student);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			//get the std by id ,Student
			var studentFind = await _context.Students.FindAsync(id);
			//var studentFind = _context.Courses.Where(x => x.Id == id).FirstOrDefault();
			vmStudent student = new vmStudent
			{
				StudentName = studentFind.StudentName,
				DOB = studentFind.DOB,
				EducationalLevelId = studentFind.EducationalLevelId,
				CityId = studentFind.CityId,
				CourseId = studentFind.CourseId,
				IsDeleted = false
			};
			await _context.SaveChangesAsync();
			//get the std by id ,Student
			ViewBag.Action = "Edit";
			ViewBag.Cities = _context.Cities.ToList();
			ViewBag.EducationalLevel = _context.educationalLevels.ToList();
			ViewBag.Courses = _context.Courses.Where(c => !c.IsDeleted).ToList();
			return View("StudentForm", student);
		}
		[HttpPost]
		public IActionResult Edit(vmStudent viewModel)
		{
			var studentFind = _context.Students.Where(x => x.Id == viewModel.Id).FirstOrDefault();
			studentFind.StudentName = viewModel.StudentName;
			studentFind.DOB = viewModel.DOB;
			studentFind.EducationalLevelId = viewModel.EducationalLevelId;
			studentFind.CityId = viewModel.CityId;
			studentFind.CourseId = viewModel.CourseId;
			studentFind.IsDeleted = false;
			_context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(int id)
		{
			//delete std
			var student = await _context.Students.FindAsync(id);
			student.IsDeleted = true;
			await _context.SaveChangesAsync();
			//delete std
			return RedirectToAction("Index");
		}
	}
}
