using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Login.Areas.Identity.Pages.Administration
{
    public class IdentityRessources : PageModel
    {
        private readonly ILogger<IdentityRessources> _logger;

        public IdentityRessources(ILogger<IdentityRessources> logger)
        {
            _logger = logger;
        }

        public IEnumerable<IdentityServer4.EntityFramework.Entities.IdentityResource> IdentityResources { get; set; } = new List<IdentityServer4.EntityFramework.Entities.IdentityResource>();

        public void OnGet()
        {
        }
    }
}
