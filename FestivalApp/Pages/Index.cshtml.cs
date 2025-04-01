using FestivalApp.Data;
using FestivalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FestivalApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DatabaseConnection _databaseConnection;
        //public List<Rave> LatestRaves { get; private set; } = new();


        public bool IsConnected { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, DatabaseConnection databaseConnection)
        {
            _logger = logger;
            _databaseConnection = databaseConnection;
        }

        public void OnGet()
        {
            IsConnected = _databaseConnection.TestConnection();
            //LatestRaves = _databaseService.GetLatestRaves();

        }
    }
}
