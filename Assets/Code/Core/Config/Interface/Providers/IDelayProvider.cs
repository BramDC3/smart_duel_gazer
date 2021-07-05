using System.Threading.Tasks;

namespace AssemblyCSharp.Assets.Code.Core.Config.Interface.Providers
{
    public interface IDelayProvider
    {
        Task Wait(int milliSeconds);
    }
}