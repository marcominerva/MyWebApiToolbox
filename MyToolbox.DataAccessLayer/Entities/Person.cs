using MyToolbox.DataAccessLayer.Entities.Common;

namespace MyToolbox.DataAccessLayer.Entities;

public class Person : BaseEntity
{
    public string Name { get; set; }

    public string City { get; set; }
}
