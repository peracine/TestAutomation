using System.Threading.Tasks;

namespace TestAutomation.Interfaces
{
    public interface ILog
    {
        Task WriteAsync(string text);
    }
}