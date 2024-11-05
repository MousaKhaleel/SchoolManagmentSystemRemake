using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystemRemake.Data;
using SchoolManagmentSystemRemake.Models;
using SchoolManagmentSystemRemake.ViewModels;

namespace SchoolManagmentSystemRemake.Controllers
{
	public class TeacherController : Controller
	{
		private readonly AppDbContext _context;
		public TeacherController(AppDbContext dbContext)
		{
			_context = dbContext;
		}
		public async Task<IActionResult> Index()
		{
			var Teachers = await _context.Teachers.Where(c => !c.IsDeleted).ToListAsync();
			//var Teachers = await _context.Teachers.Include(x => x.City).ToListAsync();
			//Teachers = await _context.Teachers.Include(x => x.EducationalLevel).ToListAsync();
			return View("Index", Teachers);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			ViewBag.Action = "Create";
			ViewBag.Majors = _context.Majors.ToList();

			return View("TeacherForm");
		}
		[HttpPost]
		public async Task<IActionResult> Create(vmTeacher viewModel)
		{
			var Teacher = new Teacher
			{
				TeacherName = viewModel.TeacherName,
				Major = viewModel.Major,
				PricePerHour = viewModel.PricePerHour,
				IsDeleted = false
			};


			await _context.Teachers.AddAsync(Teacher);
			await _context.SaveChangesAsync();
			return View("Index");
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			//get the std by id ,Teacher
			var teacher = await _context.Teachers.FindAsync(id);

			await _context.SaveChangesAsync();
			//get the std by id ,Teacher
			ViewBag.Action = "Edit";
			return View("TeacherForm", teacher);
		}
		public async Task<IActionResult> Delete(int id)
		{
			//delete std
			var teacher = await _context.Teachers.FindAsync(id);
			teacher.IsDeleted = true;
			await _context.SaveChangesAsync();
			//delete std
			return View("Index");
		}
	}
}
