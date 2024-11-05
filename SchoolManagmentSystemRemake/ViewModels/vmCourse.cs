using SchoolManagmentSystemRemake.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagmentSystemRemake.ViewModels
{
	public class vmCourse
	{
		public int Id { get; set; }

		public string CourseName { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public int CategoryId { get; set; }
		public Category Category { get; set; }

		public string Price { get; set; }

		public bool IsDeleted { get; set; }

		//public List<Student> Students { get; set; }

		//public List<CourseTeacher> CourseTeachers { get; set; }
	}
}
