using SchoolManagmentSystemRemake.Models;

namespace SchoolManagmentSystemRemake.ViewModels
{
    public class vmCourseTeacher
    {
		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }
		public int CourseId { get; set; }
		public Course Course { get; set; }
	}
}
