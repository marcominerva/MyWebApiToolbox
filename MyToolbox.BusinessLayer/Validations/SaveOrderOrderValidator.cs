using FluentValidation;
using MyToolbox.Shared.Models.Requests;

namespace MyToolbox.BusinessLayer.Validations;

public class SaveOrderRequestValidator : AbstractValidator<SaveOrderRequest>
{
    public SaveOrderRequestValidator()
    {
        RuleFor(o => o.TotalPrice).GreaterThan(0).WithMessage("Unable to create an order with 0 or negative total price");
    }
}
