using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Login.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly IConfigurationDbContext _configurationDbContext;

        public List<Client> Clients { get; set; } = new List<Client>();

        public IndexModel(IConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }

        public async Task OnGet()
        {
            Clients = await _configurationDbContext.Clients.ToListAsync();
        }
    }
}
