using SampleApi.Models;

namespace SampleApi.DAL
{
    public interface IProfileDAO
    {
        Profile CreateProfile(Profile profile);

        Profile GetProfile(int currentUserId);
    }
}