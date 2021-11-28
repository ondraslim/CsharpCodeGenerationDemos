using System;

public partial class GeneratedClass
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public override string ToString()
    {
        return $"{Id}, {Name}, {Age}";
    }
}