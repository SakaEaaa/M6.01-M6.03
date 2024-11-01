using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using System.Collections.Generic;
using System.Linq;
using Azure;

namespace IBAS_kantine.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            // Hent forbindelse til Azure Table Storage
            string connectionString = _configuration.GetConnectionString("AzureTableStorage");
            TableClient tableClient = new TableClient(connectionString, "IBASmenu123");


            // Hent menupunkterne fra tabellen
            Pageable<MenuItem> menuItems = tableClient.Query<MenuItem>();


            // Tilføj til liste for at blive vist i HTML
            MenuItems = menuItems.ToList();

            // Log hentningen
            _logger.LogInformation("Kantinens menu blev hentet fra Azure Table Storage.");
        }
    }
}
