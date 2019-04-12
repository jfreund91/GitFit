using SampleApi.Models;

namespace SampleApi.DAL
{
    public interface IProfileDAO
    {
        void CreateProfile(Profile profile);

        Profile GetProfile(int currentUserId);
    }
}