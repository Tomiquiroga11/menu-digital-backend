using MenuDigital.Api.DTOs;
using MenuDigital.Api.Extensions;
using MenuDigital.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigital.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<ActionResult<AccountDto>> Get()
    {
        var account = await _accountService.GetAccountAsync(User.GetUserId());
        if (account == null)
        {
            return NotFound();
        }

        return Ok(account);
    }

    [HttpPut]
    public async Task<ActionResult<AccountDto>> Update(UpdateAccountRequestDto dto)
    {
        var account = await _accountService.UpdateAccountAsync(User.GetUserId(), dto);
        if (account == null)
        {
            return NotFound();
        }

        return Ok(account);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        var deleted = await _accountService.DeleteAccountAsync(User.GetUserId());
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}