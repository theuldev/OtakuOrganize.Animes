 using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Models.ViewModels;
using AnimesControl.Core.Entities;

namespace AnimesControl.Application.Common.Interfaces.Services
{
    public interface IAnimeService
    {
        Task<IEnumerable<AnimeViewModel>> GetAnimes();
        Task<AnimeViewModel> GetByIdAnimeDetails(Guid id);
        void PostAnime(AnimeInputModel animeDetails);
        void PutAnime(Guid id, AnimeInputModel animeDetails);
        void DeleteAnime(Guid id);
    }
}