using MenuDigital.Api.DTOs;
using MenuDigital.Api.Entities;
using MenuDigital.Api.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MenuDigital.Api.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ITokenService _tokenService;

    public AccountService(
        IUserRepository userRepository,
        IRestaurantRepository restaurantRepository,
        IPasswordHasher<User> passwordHasher,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _restaurantRepository = restaurantRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto dto)
    {
        var email = NormalizeEmail(dto.Email);
        if (await _userRepository.EmailExistsAsync(email))
        {
            return null;
        }

        var restaurant = new Restaurant
        {
            Name = dto.RestaurantName,
            Address =dto.Address,
            Phone = dto.Phone,
            OpeningTime = dto.OpeningTime,
            ClosingTime = dto.ClosingTime,
        };

        var user = new User
        {
            Name = dto.Name,
            Email = email,
            Restaurant = restaurant
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        await _userRepository.AddAsync(user);
        return BuildResponse(user, restaurant.Id);
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(NormalizeEmail(dto.Email));
        if (user == null)
        {
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        if (result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            await _userRepository.UpdateAsync(user);
        }

        var restaurant = await _restaurantRepository.GetByUserIdAsync(user.Id);
        return BuildResponse(user, restaurant?.Id);
    }

    public async Task<AccountDto?> GetAccountAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        var restaurant = await _restaurantRepository.GetByUserIdAsync(userId);
        if (user == null || restaurant == null)
        {
            return null;
        }

        return BuildAccountDto(user, restaurant);
    }

    public async Task<AccountDto?> UpdateAccountAsync(int userId, UpdateAccountRequestDto dto)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        var restaurant = await _restaurantRepository.GetByUserIdAsync(userId);
        if (user == null || restaurant == null)
        {
            return null;
        }

        user.Name = dto.Name;
        restaurant.Name = dto.RestaurantName;
        restaurant.Address = dto.Address;
        restaurant.Phone = dto.Phone;
        restaurant.OpeningTime = dto.OpeningTime;
        restaurant.ClosingTime = dto.ClosingTime;

        await _userRepository.UpdateAsync(user);
        await _restaurantRepository.UpdateAsync(restaurant);
        return BuildAccountDto(user, restaurant);
    }

    public async Task<bool> DeleteAccountAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        await _userRepository.DeleteAsync(user);
        return true;
    }

    private static AccountDto BuildAccountDto(User user, Restaurant restaurant)
    {
        return new AccountDto
        {
            UserId = user.Id,
        Name = user.Name,
        Email = user.Email,
        RestaurantId = restaurant.Id,
        RestaurantName = restaurant.Name,
        Address = restaurant.Address,
        Phone = restaurant.Phone,
        OpeningTime = restaurant.OpeningTime,
        ClosingTime = restaurant.ClosingTime
        };
    }

    private AuthResponseDto BuildResponse(User user, int? restaurantId)
    {
        return new AuthResponseDto
        {
            Token = _tokenService.CreateToken(user),
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            RestaurantId = restaurantId
        };
    }

    private static string NormalizeEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }
}