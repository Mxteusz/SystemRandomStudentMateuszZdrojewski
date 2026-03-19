using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SystemRandomStudent.Models;

namespace SystemRandomStudent.ViewModels;

public class ClassViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Student> Students { get; set; } = new();
    public string ClassName { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}