using Backend.Controllers;

namespace Backend.Services
{
    public class PeopleService : IPeopleService
    {
        public bool Validate(People people)
        {
            if (string.IsNullOrWhiteSpace(people.Name) || people.Name.Length > 100)
            {
                return false;
            }

            return true;
        }
    }
}
