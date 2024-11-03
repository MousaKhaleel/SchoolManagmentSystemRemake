using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmentSystem.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CourseName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [ForeignKey("Id")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Price { get; set; }

        public bool IsDeleted { get; set; }

        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
}
}
