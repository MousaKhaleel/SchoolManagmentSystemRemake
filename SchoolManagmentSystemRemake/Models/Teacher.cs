using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagmentSystemRemake.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TeacherName { get; set; }

        [ForeignKey(nameof(MajorId))]
        public int MajorId { get; set; }
        public Major Major { get; set; }

        public string PricePerHour { get; set; }

        [ForeignKey(nameof(StudentsIds))]
        public List<int>? StudentsIds { get; set; }
        public List<Student>? Students { get; set; }
        public bool IsDeleted { get; set; }

        public List<CourseTeacher> CourseTeachers { get; set; }
    }
}
