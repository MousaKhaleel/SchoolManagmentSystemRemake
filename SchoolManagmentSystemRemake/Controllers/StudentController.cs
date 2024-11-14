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
			var Students = await _context.Students.Where(c => !c.IsDeleted).Include(x=>x.EducationalLevel).Include(y=>y.City).Include(z=>z.Course).Include(t=>t.Teacher).ToListAsync();
			ViewBag.Action = "All";
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
				TeacherId = viewModel.TeacherId,
				IsDeleted = false
			};

            await _context.Students.AddAsync(Student);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult GetTeachersByCourse(int courseId)
		{
			var teacherIds = _context.CourseTeachers
									 .Where(x => x.CourseId == courseId)
									 .Select(x => x.TeacherId)
									 .ToList();

			var teachers = _context.Teachers
								   .Where(y => teacherIds.Contains(y.Id))
								   .Select(y => new { y.Id, y.TeacherName })
								   .ToList();

			return Json(teachers);
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var studentFind = await _context.Students.FindAsync(id);
			//var studentFind = _context.Courses.Where(x => x.Id == id).FirstOrDefault();
			vmStudent student = new vmStudent
			{
				StudentName = studentFind.StudentName,
				DOB = studentFind.DOB,
				EducationalLevelId = studentFind.EducationalLevelId,
				CityId = studentFind.CityId,
				CourseId = studentFind.CourseId,
				TeacherId=studentFind.TeacherId,
				IsDeleted = false
			};
			await _context.SaveChangesAsync();
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
			studentFind.TeacherId = viewModel.TeacherId;
			studentFind.IsDeleted = false;
			_context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(int id)
		{
			var student = await _context.Students.FindAsync(id);
			student.IsDeleted = true;
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> RetrieveDeleted()
		{
			var Students = await _context.Students.Where(c => c.IsDeleted).Include(x => x.EducationalLevel).Include(y => y.City).Include(z => z.Course).Include(t => t.Teacher).ToListAsync();
			ViewBag.Action="Deleted";
			return View("Index", Students);
		}
		public IActionResult DeletePermanent(int id)
		{
			var student = _context.Students.Find(id);
			if (student.IsDeleted == true)
			{
				_context.Students.Remove(student);
			}
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> RestoreDeleted(int id)
		{
			var Student = await _context.Students.FindAsync(id);
			Student.IsDeleted = false;
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}
