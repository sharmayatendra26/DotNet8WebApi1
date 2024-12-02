using DotNet8WebApi.Model;

namespace DotNet8WebApi.Services
{
    public interface IOurHeroService
    {
        List<OurHero> GetAllHeros(bool? isActive);
        OurHero? GetHeroById(int id);
        OurHero AddHero(AddUpdateOurHero hero);
        OurHero? UpdateHero(int id, AddUpdateOurHero hero);
        bool DeleteHero(int id);
        Task AddHeroByJob();
    }
}
