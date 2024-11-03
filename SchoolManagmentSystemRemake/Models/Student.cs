using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmentSystem.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string StudentName { get; set; }

        public DateTime DOB { get; set; }

        [ForeignKey(nameof(EducationalLevel))]
        public int EducationalLevelId { get; set; }
        public EducationalLevel EducationalLevel { get; set; }


        [ForeignKey(nameof(CityId))]
        public int CityId { get; set; }
        public City City { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; }

}
}
