using MyToolbox.DataAccessLayer.Entities.Common;
using MyToolbox.Shared.Enums;

namespace MyToolbox.DataAccessLayer.Entities;

public class Order : BaseEntity
{
    public DateTime CreationDate { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public double TotalPrice { get; set; }

    public OrderStatus Status { get; set; }
}
