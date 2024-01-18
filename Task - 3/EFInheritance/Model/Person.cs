using System.ComponentModel.DataAnnotations;

namespace EFInheritance.Models;

public class Person
{
    [Key]
    public int id { get; set; }

    public string? Name { get; set; }

    public int Age { get; set; }

    public string? Gender {get;set;}

}
public class Student:Person{

    public int classs {get;set;}
    public string? Section {get;set;}
}
public class Teacher:Person{

    public int Salaray {get;set;}
}