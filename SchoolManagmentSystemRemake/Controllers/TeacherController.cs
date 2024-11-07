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
			ViewBag.Courses = _context.Courses.Where(x => !x.IsDeleted).ToList();

			return View("TeacherForm");
		}
		[HttpPost]
		public async Task<IActionResult> Create(vmTeacher viewModel)
		{

			var Teacher = new Teacher
			{
				TeacherName = viewModel.TeacherName,
				MajorId = viewModel.MajorId,
				PricePerHour = viewModel.PricePerHour,
				IsDeleted = false,
			};
			await _context.Teachers.AddAsync(Teacher);
			await _context.SaveChangesAsync();
			int generatedId = Teacher.Id;

			List<int> coursesIds = new List<int>();
			for (int i = 0; i < viewModel.SelectedCourseIds.Count; i++)
			{
				coursesIds.Add(viewModel.SelectedCourseIds[i]);
			}
			for (int i = 0; i < coursesIds.Count; i++)
			{
				var CourseTeacher = new CourseTeacher
				{
					CourseId = coursesIds[i],
					TeacherId = generatedId,
				};
				await _context.CourseTeachers.AddAsync(CourseTeacher);
			}

			await _context.SaveChangesAsync();


			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			//get the std by id ,Teacher
			var teacherFind = await _context.Teachers.FindAsync(id);
			vmTeacher teacher = new vmTeacher
			{
				TeacherName = teacherFind.TeacherName,
				MajorId = teacherFind.MajorId,
				PricePerHour = teacherFind.PricePerHour,
				SelectedCourseIds = _context.CourseTeachers
									.Where(x => x.TeacherId == id)
									.Select(ct => ct.CourseId)
									.ToList(),
				IsDeleted = false,
			};
			await _context.SaveChangesAsync();
			//get the std by id ,Teacher
			ViewBag.Action = "Edit";
			ViewBag.Majors = _context.Majors.ToList();
			ViewBag.Courses = _context.Courses.Where(x => !x.IsDeleted).ToList();
			//ViewBag.selectedCourseIds = _context.CourseTeachers.Where(x => x.TeacherId == id).Select(ct => ct.CourseId).ToList();
			return View("TeacherForm", teacher);
		}
		[HttpPost]
		public IActionResult Edit(vmTeacher viewModel)
		{
			var teacherFind = _context.Teachers.Where(x => x.Id == viewModel.Id).FirstOrDefault();
			teacherFind.TeacherName = viewModel.TeacherName;
			teacherFind.MajorId = viewModel.MajorId;
			teacherFind.PricePerHour = viewModel.PricePerHour;
			teacherFind.IsDeleted = false;

			var teacherRecords = _context.CourseTeachers.Where(x => x.TeacherId == teacherFind.Id);
			_context.CourseTeachers.RemoveRange(teacherRecords);
			_context.SaveChanges();

			List<int> coursesIds = new List<int>();
			for (int i = 0; i < viewModel.SelectedCourseIds.Count; i++)
			{
				coursesIds.Add(viewModel.SelectedCourseIds[i]);
			}

			for (int i = 0; i < coursesIds.Count; i++)
			{
				var CourseTeacher = new CourseTeacher
				{
					CourseId = coursesIds[i],
					TeacherId = teacherFind.Id,
				};
				_context.CourseTeachers.AddAsync(CourseTeacher);
			}

			_context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Delete(int id)
		{
			//delete std
			var teacher = await _context.Teachers.FindAsync(id);
			teacher.IsDeleted = true;
			await _context.SaveChangesAsync();
			//delete std
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> RetrieveDeleted()
		{
			var Teachers = await _context.Teachers.Where(c => c.IsDeleted).ToListAsync();
			ViewBag.Action = "Deleted";
			return View("Index", Teachers);
		}
		public IActionResult DeletePermanent(int id)
		{
			var teacher = _context.Teachers.Find(id);
			if (teacher.IsDeleted == true)
			{
				_context.Teachers.Remove(teacher);
			}
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
