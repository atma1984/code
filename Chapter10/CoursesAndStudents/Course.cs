using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAndStudents
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required]
        [StringLength(60)]
        public string? Title { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
