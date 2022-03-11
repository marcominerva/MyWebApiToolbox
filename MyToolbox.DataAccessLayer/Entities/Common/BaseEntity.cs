namespace MyToolbox.DataAccessLayer.Entities.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public Guid LastModifiedUserId { get; set; }

    public DateTime? DeletedDate { get; set; }
}
