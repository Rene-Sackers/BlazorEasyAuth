using System.Threading.Tasks;
using BlazorEasyAuth.Models;

namespace BlazorEasyAuth.Providers.Interfaces
{
    public interface IUserProvider
    {
        Task<IUser> GetById(string id);
    }
}