using SchoolManagmentSystem.Models;

namespace SchoolManagmentSystemRemake.ViewModels
{
	public class vmStudent
	{
		public string StudentName { get; set; }
		public DateTime DOB { get; set; }

		public EducationalLevel EducationalLevel { get; set; }

		public City City { get; set; }

		public bool IsDeleted { get; set; }

		public Course Course { get; set; }
	}
}
