using ECommerceTask.Core.Interfaces;
using MediatR;

namespace ECommerceTask.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null || user.PasswordHash != request.Password)
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı.");
            }

            return _tokenService.GenerateToken(user);
        }
    }
}