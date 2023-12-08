using System.ComponentModel.DataAnnotations;

public class Person
{
    [Key]
    public int id { get; set; }

    public string firstname { get; set; }
    public string lastname { get; set; }
}