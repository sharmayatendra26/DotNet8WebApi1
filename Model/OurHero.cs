namespace DotNet8WebApi.Model
{
    public class OurHero
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    public class AddUpdateOurHero
    {
        public required string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
