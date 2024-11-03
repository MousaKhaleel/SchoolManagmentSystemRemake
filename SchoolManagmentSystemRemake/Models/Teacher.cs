using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmentSystem.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TeacherName { get; set; }

        [ForeignKey("Id")]
        public int MajorId { get; set; }
        public Major Major { get; set; }

        public string PricePerHour { get; set; }

        public bool IsDeleted { get; set; }

        public List<Course> Courses { get; set; }
    }
}
