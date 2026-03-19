using SystemRandomStudent.Models;

namespace SystemRandomStudent.ViewModels;

public class StudentViewModel
{
    public Student Student { get; set; }

    public StudentViewModel(Student student)
    {
        Student = student;
    }
}