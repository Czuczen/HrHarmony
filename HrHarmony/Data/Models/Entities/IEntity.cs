using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.Entities;

public interface IEntity<TKey>
{
    [Key]
    public TKey Id { get; set; }
}