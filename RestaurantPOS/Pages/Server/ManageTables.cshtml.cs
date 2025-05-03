using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RestaurantPOS.Pages.Server;

public class ManageTables : PageModel
{
    public IActionResult OnGet()
    {
        var role = HttpContext.Session.GetString("role");
        if (role != "Server")
        {
            return RedirectToPage("/Auth/Login");
        }
        return Page();
    }
}