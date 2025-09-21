using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CoreIdentity.Controllers;

public class RoleController : Controller
{
    private RoleManager<IdentityRole> _role;
    private UserManager<IdentityUser> _usr;

    public RoleController(RoleManager<IdentityRole> role, UserManager<IdentityUser> usr)
    {
        _role = role;
        _usr = usr;
    }

    [Authorize]
    public async Task<IActionResult> Create()
    {
        string roleName = "Admin";
        var exist = await _role.RoleExistsAsync(roleName);
        if (!exist)
        {
            await _role.CreateAsync(new IdentityRole(roleName));
        }

        var current = await _usr.GetUserAsync(User);
        if (current != null)
        {
            await _usr.AddToRoleAsync(current, roleName);
        }

        return Content($"現在のユーザーを{roleName}ロールに追加しました。");
    }
}

