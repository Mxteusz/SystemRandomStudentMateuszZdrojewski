using System;
using System.Linq;
using Microsoft.Maui.Controls;
using SystemRandomStudent.Models;
using SystemRandomStudent.Services;

namespace SystemRandomStudent.Views;

public partial class EditClassPage : ContentPage
{
    private ClassRoom currentClass;

    public EditClassPage(ClassRoom classRoom)
    {
        InitializeComponent();
        currentClass = classRoom;

        StudentsCollection.ItemsSource = currentClass.Students;
    }

    private void OnAddStudentClicked(object sender, EventArgs e)
    {
        string name = StudentNameEntry.Text?.Trim();
        if (string.IsNullOrEmpty(name))
        {
            DisplayAlert("B³¹d", "WprowadŸ imiê ucznia", "OK");
            return;
        }

        int newIndex = currentClass.Students.Any()
            ? currentClass.Students.Max(s => s.IndexNumber) + 1
            : 1;

        var student = new Student
        {
            IndexNumber = newIndex,
            Name = name,
            IsPresent = true,
            LuckyNumber = false
        };

        currentClass.Students.Add(student);

        StudentsCollection.ItemsSource = null;
        StudentsCollection.ItemsSource = currentClass.Students;

        FileService.SaveClass(currentClass);

        StudentNameEntry.Text = string.Empty;
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        FileService.SaveClass(currentClass);
        DisplayAlert("Gotowe!", $"Klasa {currentClass.ClassName} zosta³a zapisana.", "OK");
    }

    private void OnIsPresentChanged(object sender, CheckedChangedEventArgs e)
    {
        if (!(sender is CheckBox checkBox)) return;
        var student = checkBox.BindingContext as Student;
        if (student == null) return;

        StudentsCollection.ItemsSource = null;
        StudentsCollection.ItemsSource = currentClass.Students;

        FileService.SaveClass(currentClass);

        if (!currentClass.Students.Any(s => s.IsPresent))
            DisplayAlert("Uwaga", "Wszyscy uczniowie s¹ nieobecni!", "OK");
    }

    private void OnLuckyNumberChanged(object sender, CheckedChangedEventArgs e)
    {
        if (!(sender is CheckBox checkBox)) return;
        var student = checkBox.BindingContext as Student;
        if (student == null) return;

        if (e.Value)
        {
            foreach (var s in currentClass.Students)
            {
                if (s != student && s.LuckyNumber)
                    s.LuckyNumber = false;
            }

            StudentsCollection.ItemsSource = null;
            StudentsCollection.ItemsSource = currentClass.Students;

            FileService.SaveClass(currentClass);
        }
        else
        {
            FileService.SaveClass(currentClass);
        }
    }
}