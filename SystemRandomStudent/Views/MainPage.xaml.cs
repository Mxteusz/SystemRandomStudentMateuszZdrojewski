using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Maui.Controls;
using SystemRandomStudent.Models;
using SystemRandomStudent.Services;

namespace SystemRandomStudent.Views;

public partial class MainPage : ContentPage
{
    private ObservableCollection<ClassRoom> Classes;
    private ClassRoom currentClass;

    public MainPage()
    {
        InitializeComponent();

        Classes = new ObservableCollection<ClassRoom>();

        foreach (var file in Directory.GetFiles(FileSystem.AppDataDirectory, "*.txt"))
        {
            string className = Path.GetFileNameWithoutExtension(file);
            Classes.Add(FileService.LoadClass(className));
        }

        if (!Classes.Any())
        {
            var sampleClasses = ClassRoom.GetSampleClasses();
            foreach (var c in sampleClasses)
            {
                Classes.Add(c);
                FileService.SaveClass(c);
            }
        }

        ClassPicker.Items.Clear();
        foreach (var c in Classes)
            ClassPicker.Items.Add(c.ClassName);
    }

    private void OnClassChanged(object sender, EventArgs e)
    {
        if (ClassPicker.SelectedIndex != -1)
        {
            currentClass = Classes[ClassPicker.SelectedIndex];
            StudentsCollection.ItemsSource = currentClass.Students;
        }
    }

    private void OnDrawClicked(object sender, EventArgs e)
    {
        if (currentClass == null)
        {
            DisplayAlert("Błąd", "Wybierz najpierw klasę!", "OK");
            return;
        }

        var student = FileService.DrawRandomStudent(currentClass);

        if (student == null)
            ResultLabel.Text = "Brak dostępnych uczniów!";
        else
            ResultLabel.Text = $"Wylosowano: {student.IndexNumber}. {student.Name}";
    }

    private async void OnEditClassClicked(object sender, EventArgs e)
    {
        if (currentClass != null)
            await Navigation.PushAsync(new EditClassPage(currentClass));
        else
            await DisplayAlert("Błąd", "Najpierw wybierz klasę!", "OK");
    }

    private void OnAddClassClicked(object sender, EventArgs e)
    {
        string className = NewClassEntry.Text?.Trim();
        if (string.IsNullOrEmpty(className))
        {
            DisplayAlert("Błąd", "Wprowadź nazwę klasy.", "OK");
            return;
        }

        if (Classes.Any(c => c.ClassName.Equals(className, StringComparison.OrdinalIgnoreCase)))
        {
            DisplayAlert("Błąd", "Klasa już istnieje.", "OK");
            return;
        }

        var newClass = new ClassRoom { ClassName = className };
        Classes.Add(newClass);
        ClassPicker.Items.Add(newClass.ClassName);

        FileService.SaveClass(newClass);

        NewClassEntry.Text = string.Empty;
        DisplayAlert("Gotowe!", $"Klasa {className} została dodana.", "OK");
    }
}