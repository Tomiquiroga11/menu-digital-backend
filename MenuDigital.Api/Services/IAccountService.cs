using MenuDigital.Api.DTOs;

namespace MenuDigital.Api.Services;

public interface IAccountService
{
    Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto dto);
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto);
    Task<AccountDto?> GetAccountAsync(int userId);
    Task<AccountDto?> UpdateAccountAsync(int userId, UpdateAccountRequestDto dto);
    Task<bool> DeleteAccountAsync(int userId);
    Task<AccountUpdateResult> ChangePasswordAsync(int userId, ChangePasswordRequestDto dto);
    Task<AccountUpdateResult> ChangeEmailAsync(int userId, ChangeEmailRequestDto dto);
}