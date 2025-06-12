using Logic.Exceptions;
using Logic.Managers;
using FestivalApp.Pages.Shared;
using Interfaces.Models;

namespace FestivalApp.Pages.AccountPages
{
    public class RecapDetailModel : UserPageModel
    {
        private readonly RecapManager _recapManager;

        public RecapDetailModel(RecapManager recapManager)
        {
            _recapManager = recapManager;
        }

        public Recap Recap { get; set; } = new Recap();
        public void OnGet(int id)
        {
            try
            {
                Recap = _recapManager.GetRecapById(id);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (TemporaryDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }
            catch (PersistentDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }
    }
}
