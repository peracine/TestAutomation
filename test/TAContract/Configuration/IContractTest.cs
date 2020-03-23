using System.Threading.Tasks;

namespace TAContract.Tests
{
    interface IContractTest
    {
        Task Consumer();
        void Provider();
    }
}
