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
		public IActionResult Index()
		{
			var Course = _context.Courses.Where(c => !c.IsDeleted).Include(x=>x.Category).ToList();
			//var Course = await _context.Course.Include(x => x.City).ToListAsync();
			//Course = await _context.Course.Include(x => x.EducationalLevel).ToListAsync();
			return View("Index", Course);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			ViewBag.Action = "Create";
			ViewBag.Categories = _context.Categories.ToList();

			return View("CourseForm");
		}
		[HttpPost]
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
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{
			//get the std by id ,Course
			var courseFind = _context.Courses.Where(x => x.Id == id).FirstOrDefault();
			vmCourse course = new vmCourse
			{
				CourseName = courseFind.CourseName,
				StartDate = courseFind.StartDate,
				EndDate = courseFind.EndDate,
				CategoryId = courseFind.CategoryId,
				Price = courseFind.Price,
				IsDeleted = false
			};
			//get the std by id ,Course
			ViewBag.Action = "Edit";
			ViewBag.Categories = _context.Categories.ToList();

			return View("CourseForm", course);
		}
		[HttpPost]
		public IActionResult Edit(vmCourse viewModel)
		{
			var courseFind = _context.Courses.Where(x => x.Id == viewModel.Id).FirstOrDefault();
				courseFind.CourseName = viewModel.CourseName;
				courseFind.StartDate = viewModel.StartDate;
				courseFind.EndDate = viewModel.EndDate;
				courseFind.CategoryId = viewModel.CategoryId;
				courseFind.Price = viewModel.Price;
				courseFind.IsDeleted = false;
			_context.SaveChangesAsync();
			//get id and fill new info??
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Delete(int id)
		{
			//delete Course
			var Course = await _context.Courses.FindAsync(id);
			Course.IsDeleted = true;
			await _context.SaveChangesAsync();
			//delete Course
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> RetrieveDeleted()
		{
			var Courses = await _context.Courses.Where(c => c.IsDeleted).Include(x => x.Category).ToListAsync();
			ViewBag.Action = "Deleted";
			return View("Index", Courses);
		}
		public IActionResult DeletePermanent(int id)
		{
			var course = _context.Courses.Find(id);
			if (course.IsDeleted == true)
			{
				_context.Courses.Remove(course);
			}
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> RestoreDeleted(int id)
		{
			var Course = await _context.Courses.FindAsync(id);
			Course.IsDeleted = false;
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}
