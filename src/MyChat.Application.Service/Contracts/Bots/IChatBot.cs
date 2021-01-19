using System.Threading.Tasks;

namespace MyChat.Application.Service.Contracts.Bots
{
    public interface IChatBot
    {
        Task<string> CallAsync(string param1);
    }
}
