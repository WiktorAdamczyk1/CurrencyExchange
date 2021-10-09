using DataAccessLibrary.Models;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IAspNetUsersData
    {
        Task<string> GetUserId(string userName);
    }
}