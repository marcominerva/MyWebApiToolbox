using MyToolbox.Shared.Enums;
using MyToolbox.Shared.Models.Common;

namespace MyToolbox.Shared.Models;

public class Order : BaseObject
{
    public DateTime CreationDate { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public double TotalPrice { get; set; }

    public OrderStatus Status { get; set; }
}
