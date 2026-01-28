using MediatR;

namespace ECommerceTask.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}