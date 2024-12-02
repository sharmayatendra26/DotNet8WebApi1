using DotNet8WebApi.Model;

namespace DotNet8WebApi.Services
{
    public class OurHeroService: IOurHeroService    
    {
        private readonly static List<OurHero> _heroes = new List<OurHero>()
            {
                new OurHero() { Id = 1, FirstName = "Yatendra", LastName = "Sharma", IsActive = true}
            };

        public OurHeroService() {
            
        }

        public Task AddHeroByJob()
        {
            OurHero ourHero = new OurHero
            { Id = _heroes.Max(hero => hero.Id) + 1, FirstName = "Added", LastName = "ByJob", IsActive = false };
            _heroes.Add(ourHero);
            return Task.CompletedTask;
        }
        public OurHero AddHero(AddUpdateOurHero hero)
        {
            OurHero ourHero = new OurHero
            { Id = _heroes.Max(hero => hero.Id) + 1, FirstName = hero.FirstName, LastName = hero.LastName, IsActive = hero.IsActive };
            _heroes.Add(ourHero);
            return ourHero;
        }

        public bool DeleteHero(int id)
        {
            var Index = _heroes.FindIndex(hero => hero.Id == id);
            if (Index >= 0)
            {
                _heroes.RemoveAt(Index);
            }
            return Index >= 0;
        }

        public List<OurHero> GetAllHeros(bool? isActive)
        {
            return isActive == null ? _heroes : _heroes.Where(x => x.IsActive == isActive).ToList(); 
        }

        public OurHero? GetHeroById(int id)
        {
            return _heroes.FirstOrDefault(x => x.Id == id);
        }

        public OurHero? UpdateHero(int id, AddUpdateOurHero hero)
        {
            var index = _heroes.FindIndex(_hero => _hero.Id == id);
            if (index >= 0)
            {
                OurHero ourHero = _heroes[index];
                ourHero.FirstName = hero.FirstName;
                ourHero.LastName = hero.LastName;
                ourHero.IsActive = hero.IsActive;
                _heroes[index] = ourHero;
                return ourHero;
            }
            return null;
        }
    }
}
