using Backend.Controllers;

namespace Backend.Services
{
    public class People2Service : IPeopleService // IPeopleService indicades us How has to do
    {
        public bool Validate(People people)
        {
            if (string.IsNullOrWhiteSpace(people.Name) || people.Name.Length > 6 || people.Name.Length < 3) // What has to do
            {
                return false;
            }

            return true;
        }
    }
}
