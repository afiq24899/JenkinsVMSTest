using System.Threading.Tasks;

namespace Lingkail.VMS.Controllers
{
    public interface ITaskSchedule
    {
        Task UpdateStatusBoardAsync();
        void Setdefaultvalue();
    }
}