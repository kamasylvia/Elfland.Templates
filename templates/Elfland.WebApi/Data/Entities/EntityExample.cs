using System.ComponentModel.DataAnnotations;
using MassTransit;

namespace Elfland.WebApi.Data.Entities;

public class EntityExample
{
    [Key]
    public NewId Id { get; set; }

    public string? Name { get; set; }
}
