using System.Collections.ObjectModel;
using System.Windows.Input;
using SystemRandomStudent.Models;
using SystemRandomStudent.Services;

namespace SystemRandomStudent.ViewModels;

public class MainViewModel
{
    public ObservableCollection<ClassRoom> Classes { get; set; } = new();

    public ClassRoom SelectedClass { get; set; }

    public Student DrawnStudent { get; set; }

    public ICommand DrawCommand { get; }

    public MainViewModel()
    {
        Classes.Add(new ClassRoom { ClassName = "1A" });
        Classes.Add(new ClassRoom { ClassName = "2B" });

        DrawCommand = new Command(DrawStudent);
    }

    private void DrawStudent()
    {
        if (SelectedClass != null)
            DrawnStudent = FileService.DrawRandomStudent(SelectedClass);
    }
}