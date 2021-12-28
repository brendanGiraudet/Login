using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Login.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly ConfigurationDbContext _configurationDbContext;

        [BindProperty]
        public Client Client { get; set; } = new Client();

        public CreateModel(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _configurationDbContext.Clients.Add(Client);
            await _configurationDbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
