using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RestaurantPOS.Pages.Admin;

public class ManageMenu : PageModel
{
    public IActionResult OnGet()
    {
        var role = HttpContext.Session.GetString("role");
        if (role != "Admin")
        {
            return RedirectToPage("/Auth/Login");
        }
        return Page();
    }
}
