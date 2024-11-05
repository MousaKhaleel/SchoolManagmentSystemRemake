using SchoolManagmentSystemRemake.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmentSystemRemake.ViewModels
{
	public class vmTeacher
	{
		public int Id { get; set; }

		public string TeacherName { get; set; }

		public int MajorId { get; set; }
		public Major Major { get; set; }

		public string PricePerHour { get; set; }

		public bool IsDeleted { get; set; }

		//public List<CourseTeacher> CourseTeachers { get; set; }
	}
}
