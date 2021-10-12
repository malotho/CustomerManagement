using DataAccessLayer.Models;
using FluentValidation;

namespace CustomerManagementPortal.Validation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(x => x.username).EmailAddress()
                .WithMessage("Username should be an email");
        }
    }
}
