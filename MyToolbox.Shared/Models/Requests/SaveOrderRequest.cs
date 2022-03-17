namespace MyToolbox.Shared.Models.Requests;

public class SaveOrderRequest
{
    public Guid? Id { get; set; }

    public double TotalPrice { get; set; }
}
