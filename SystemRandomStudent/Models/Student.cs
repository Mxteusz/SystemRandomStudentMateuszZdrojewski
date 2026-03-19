namespace SystemRandomStudent.Models;

public class Student
{
    public int IndexNumber { get; set; }    
    public string Name { get; set; }        
    public bool IsPresent { get; set; } = true;
    public bool LuckyNumber { get; set; } = false;

    public override string ToString()
    {
        return $"{IndexNumber}. {Name}";
    }
}