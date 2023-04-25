using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Application.Services.Interfaces;
using ChatService.Application.Support.Helpers.Exceptions;
using ChatService.Domain.Auth;
using ChatService.Domain.Entities;

namespace ChatService.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsersServices _usersServices;
    private readonly IMapper _mapper;
    private readonly IJwtManager _jwtManager;

    public AuthService(IUsersServices usersServices, IMapper mapper, IJwtManager jwtManager)
    {
        _usersServices = usersServices;
        _mapper = mapper;
        _jwtManager = jwtManager;
    }
    
    public async Task<AuthDto> GetToken(UserAuthDto userAuthDto)
    {
        var user = _mapper.Map<User>(userAuthDto);

        var result = await _usersServices.IsValidUserAsync(user);

        return result ? new AuthDto { Token = _jwtManager.GetJwt(user) } : throw new UnauthorizedException("Invalid Credentials");
    }
}