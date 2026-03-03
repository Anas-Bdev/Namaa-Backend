namespace Namaa.Domain.Common;
public abstract class AuditableEntity : BaseEntity
{
    public DateTimeOffset CreatedAtUtc {get;set;}
    public string? CreatedBy {get;set;}
    public DateTimeOffset? LastModifiedUtc {get;set;}
    public string? LastModifiedBy {get;set;}
    protected AuditableEntity(){}
    
    
    protected AuditableEntity(Guid id):base(id){}
   
}