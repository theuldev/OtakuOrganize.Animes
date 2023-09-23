 using OtakuOrganize.Application.Models.InputModels;
using OtakuOrganize.Application.Models.ViewModels;
using OtakuOrganize.Core.Entities;

namespace OtakuOrganize.Application.Common.Interfaces.Services
{
    public interface IAnimeService
    {
        Task<IEnumerable<AnimeViewModel>> GetAnimes();
        Task<AnimeViewModel> GetByIdAnimeDetails(Guid id);
        Task PostAnime(AnimeInputModel animeDetails);
        Task PutAnime(Guid id, AnimeInputModel animeDetails);
        Task DeleteAnime(Guid id);
    }
}