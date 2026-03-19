using System.Collections.ObjectModel;

namespace SystemRandomStudent.Models;

public class ClassRoom
{
    public string ClassName { get; set; }
    public ObservableCollection<Student> Students { get; set; } = new();

    public static ObservableCollection<ClassRoom> GetSampleClasses()
    {
        return new ObservableCollection<ClassRoom>
        {
            new ClassRoom
            {
                ClassName = "1A",
                Students = new ObservableCollection<Student>
                {
                    new Student { IndexNumber = 1, Name = "Bartosz Kapustka" },
                    new Student { IndexNumber = 2, Name = "Rafał Augustyniak" },
                    new Student { IndexNumber = 3, Name = "Artur Jędrzejczyk" }
                }
            },
            new ClassRoom
            {
                ClassName = "2B",
                Students = new ObservableCollection<Student>
                {
                    new Student { IndexNumber = 1, Name = "Kacper Urbański" },
                    new Student { IndexNumber = 2, Name = "Kamil Piątkowski" },
                    new Student { IndexNumber = 3, Name = "Jakub Żewłakow" }
                }
            }
        };
    }
}