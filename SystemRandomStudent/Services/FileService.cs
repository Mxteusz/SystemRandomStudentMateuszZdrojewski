using System;
using System.Linq;
using System.IO;
using System.Text;
using SystemRandomStudent.Models;
using System.Collections.ObjectModel;

namespace SystemRandomStudent.Services;

public static class FileService
{
    private static string basePath = FileSystem.AppDataDirectory;

    public static void SaveClass(ClassRoom classRoom)
    {
        string filePath = Path.Combine(basePath, classRoom.ClassName + ".txt");
        var sb = new StringBuilder();
        foreach (var student in classRoom.Students)
        {
            sb.AppendLine($"{student.IndexNumber}|{student.Name}|{student.IsPresent}|{student.LuckyNumber}");
        }
        File.WriteAllText(filePath, sb.ToString());
    }

    public static ClassRoom LoadClass(string className)
    {
        string filePath = Path.Combine(basePath, className + ".txt");
        var classRoom = new ClassRoom { ClassName = className };
        if (!File.Exists(filePath)) return classRoom;

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Split('|');
            if (parts.Length == 4)
            {
                classRoom.Students.Add(new Student
                {
                    IndexNumber = int.Parse(parts[0]),
                    Name = parts[1],
                    IsPresent = bool.Parse(parts[2]),
                    LuckyNumber = bool.Parse(parts[3])
                });
            }
        }
        return classRoom;
    }

    public static Student DrawRandomStudent(ClassRoom classRoom)
    {
        var availableStudents = classRoom.Students
            .Where(s => s.IsPresent && !s.LuckyNumber)
            .ToList();

        if (!availableStudents.Any()) return null;

        var rand = new Random();
        int index = rand.Next(availableStudents.Count);
        return availableStudents[index];
    }
}