using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jso.Annotationary.Domain.Entities;

public class ActivityLog
{
    [Key]
    public Guid Log_Id { get; set; }
    
    
    
    // Foreign key reference
    public Guid User_Id { get; set; }
    [ForeignKey(nameof(User_Id))]
    public virtual User User { get; set; }
}