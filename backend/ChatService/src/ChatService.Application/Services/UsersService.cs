using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Application.Services.Interfaces;
using ChatService.Application.Support.Helpers;
using ChatService.Application.Support.Helpers.Exceptions;
using ChatService.Domain.Crypto;
using ChatService.Domain.Entities;
using ChatService.Domain.Repositories;

namespace ChatService.Application.Services;

public class UsersService : IUsersServices
{
    private readonly IUsersRepository _usersRepository;
    private readonly ICryptoManager _cryptoManager;
    private readonly IMapper _mapper;

    public UsersService(IUsersRepository usersRepository, ICryptoManager cryptoManager, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _cryptoManager = cryptoManager;
        _mapper = mapper;
    }
    
    public async Task<UserDto> AddUserAsync(UserDto userDto)
    {
        if (!userDto.Password.Equals(userDto.RepeatPassword)) throw new BusinessException("Passwords not Equal");
        
        var hashedPass = _cryptoManager.GetHashString(userDto.Password, _cryptoManager.GetSalt());

        var newUser = new User
        {
            Name = userDto.Name,
            Password = hashedPass
        };
        
        var mongoUser = await _usersRepository.AddAsync(newUser);

        return _mapper.Map<UserDto>(mongoUser);
    }

    public async Task<bool> IsValidUserAsync(User user)
    {
        var salt = _cryptoManager.GetSalt();
        var mongoUser =  await _usersRepository.FindAsync(user.Name);

        return _cryptoManager.VerifyHash(user.Password, mongoUser?.Password ?? string.Empty, salt);
    }
}