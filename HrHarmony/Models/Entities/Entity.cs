using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.Entities;

public abstract class Entity<TKey> : IEntity<TKey>
{
    [Key]
    public TKey Id { get; set; }
}