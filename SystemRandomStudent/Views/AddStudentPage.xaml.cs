using SystemRandomStudent.Models;

namespace SystemRandomStudent.Views;

public partial class AddStudentPage : ContentPage
{
    private ClassRoom classRoom;

    public AddStudentPage(ClassRoom selectedClass)
    {
        InitializeComponent();
        classRoom = selectedClass;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(StudentNameEntry.Text))
        {
            classRoom.Students.Add(new Student
            {
                Name = StudentNameEntry.Text,
                IsPresent = true,
                LuckyNumber = false
            });

            StudentNameEntry.Text = "";
            await DisplayAlert("Dodano", "Uczeñ dodany.", "OK");
        }
    }
}