using Microsoft.EntityFrameworkCore;
using static System.Console;
namespace CoursesAndStudents;


public class Academy : DbContext
{
    public DbSet<Student>? Students { get; set; }
    public DbSet<Course>? Courses { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Academy.db");
       
        WriteLine($"Using {path} database file.");
        //optionsBuilder.UseSqlite($"Filename={path}");
        //optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog= Academy; Integrated Security = true; MultipleActiveResultSets = true; ");

        string connection = "Data Source = NKMS0310232\\MSSQLSERVER_CORE; Initial Catalog = Academy; User ID = sa; Password = VjzLjxf2020!; Encrypt = False;MultipleActiveResultSets=True;";
        optionsBuilder.UseSqlServer(connection);

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // правила проверки Fluent API
        modelBuilder.Entity<Student>().Property(s => s.LastName).HasMaxLength(30).IsRequired();
       
        // заполнение базы данных образцами данных
        Student alice = new()
        {
            StudentId = 1,
            FirstName = "Alice",
            LastName = "Jones"
        };
        Student bob = new()
        {
            StudentId = 2,
            FirstName = "Bob",
            LastName = "Smith"
        };
        Student cecilia = new()
        {
            StudentId = 3,
            FirstName = "Cecilia",
            LastName = "Ramirez"
        };
        Course csharp = new()
        {
            CourseId = 1,
            Title = "C# 10 and .NET 6",
        };
        Course webdev = new()
        {
            CourseId = 2,
            Title = "Web Development",
        };
        Course python = new()
        {
            CourseId = 3,
            Title = "Python for Beginners",
        };


        modelBuilder.Entity<Student>().HasData(alice, bob, cecilia);
        
        modelBuilder.Entity<Course>().HasData(csharp, webdev, python);
       
        modelBuilder.Entity<Course>()
        .HasMany(c => c.Students)     // на курсе может быть много студентов
        .WithMany(s => s.Courses)     // студент может быть записан на несколько курсов
        .UsingEntity(e => e.HasData(  // Заполнение - таблицы с внешними ключами
        new { CoursesCourseId = 1, StudentsStudentId = 1 },
        new { CoursesCourseId = 1, StudentsStudentId = 2 },
        new { CoursesCourseId = 1, StudentsStudentId = 3 },
        new { CoursesCourseId = 2, StudentsStudentId = 2 },
        new { CoursesCourseId = 3, StudentsStudentId = 3 }
        ));
    }
}

