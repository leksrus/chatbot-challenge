using System.Threading.Tasks;
using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Application.Services;
using ChatService.Application.Services.Interfaces;
using ChatService.Application.Support.Helpers.Exceptions;
using ChatService.Domain.Auth;
using ChatService.Domain.Entities;
using Moq;
using Xunit;

namespace ChatService.UnitTests.Application.Services;

public class AuthServiceTest
{
    [Theory]
    [InlineData("test", "test")]
    public async Task When_GetTokenAsync_ExpectToObtainToken(string name, string password)
    {
        var usersServicesMock = new Mock<IUsersServices>();
        var mapperMock = new Mock<IMapper>();
        var jwtManagerMock = new Mock<IJwtManager>();

        var userAuthDto = new UserAuthDto
        {
            Name = name,
            Password = password
        };

        usersServicesMock.Setup(r => r.IsValidUserAsync(It.IsAny<User>()))
            .ReturnsAsync(true);

        var expectedResult = new AuthDto
        {
            Token = "test"
        };

        jwtManagerMock.Setup(r => r.GetJwt(It.IsAny<User>()))
            .Returns("test");

        var botCommandServiceService =
            new AuthService(usersServicesMock.Object, mapperMock.Object, jwtManagerMock.Object);

        var result = await botCommandServiceService.GetTokenAsync(userAuthDto);

        Assert.Equal(expectedResult.Token, result.Token);
    }

    [Theory]
    [InlineData("test", "test")]
    public async Task When_GetTokenAsync_NotValidUserOrPassword_ExpectThrowException(string name, string password)
    {
        var usersServicesMock = new Mock<IUsersServices>();
        var mapperMock = new Mock<IMapper>();
        var jwtManagerMock = new Mock<IJwtManager>();

        var userAuthDto = new UserAuthDto
        {
            Name = name,
            Password = password
        };

        usersServicesMock.Setup(r => r.IsValidUserAsync(It.IsAny<User>()))
            .ReturnsAsync(false);

        jwtManagerMock.Setup(r => r.GetJwt(It.IsAny<User>()))
            .Returns("test");

        var botCommandServiceService =
            new AuthService(usersServicesMock.Object, mapperMock.Object, jwtManagerMock.Object);

        await Assert.ThrowsAsync<UnauthorizedException>(() => botCommandServiceService.GetTokenAsync(userAuthDto));
    }
}