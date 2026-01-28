using ECommerceTask.Core.Entities;
using ECommerceTask.Core.Interfaces;
using MediatR;

namespace ECommerceTask.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = request.Password,
                Role = "User"
            };

            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}