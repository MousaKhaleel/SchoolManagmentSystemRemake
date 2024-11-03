using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmentSystemRemake.Models
{
    public class CourseTeacher
    {


        [ForeignKey(nameof(TeacherId))]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        [ForeignKey(nameof(CourseId))]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
