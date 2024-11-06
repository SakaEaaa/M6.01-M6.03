using Microsoft.AspNetCore.Mvc.RazorPages;
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
        private readonly TableServiceClient _tableServiceClient;

        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        public IndexModel(ILogger<IndexModel> logger, TableServiceClient tableServiceClient)
        {
            _logger = logger;
            _tableServiceClient = tableServiceClient;
        }

        public void OnGet()
        {
            // Hent menupunkterne fra tabellen
            var tableClient = _tableServiceClient.GetTableClient("IBASmenu123");
            Pageable<MenuItem> menuItems = tableClient.Query<MenuItem>();

            // Tilføj til liste for at blive vist i HTML
            MenuItems = menuItems.ToList();

            // Sorter menuen efter ugedag
            var DayOrder = new List<string> { "Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag" };
            MenuItems = MenuItems.OrderBy(x => DayOrder.IndexOf(x.RowKey)).ToList();

            // Log hentningen
            _logger.LogInformation("Kantinens menu blev hentet fra Azure Table Storage.");
        }
    }
}
