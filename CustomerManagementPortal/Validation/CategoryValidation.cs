using FluentValidation;
using LogicLayer.Models;

namespace CustomerManagementPortal.Validation
{
    public class CategoryValidation : AbstractValidator<CategoryModel>
    {
        const string categorypattern = @"^\w{3}\d{3}$";
        public CategoryValidation()
        {
            RuleFor(x => x.CategoryCode)
                .NotEmpty().WithMessage("Category Code Is required")
                .Matches(categorypattern).WithMessage("Category Code Should be in the format: ABC123");
        }
    }
}
