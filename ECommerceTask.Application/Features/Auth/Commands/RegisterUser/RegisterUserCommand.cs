using MediatR;

namespace ECommerceTask.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<int>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}