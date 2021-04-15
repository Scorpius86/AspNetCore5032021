using Net5.AspNet.MVC.Infrastructure.Dtos;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Agents.Comments
{
    public interface ICommentsAgent
    {
        Task<ComentarioDto> InsertCommentsAsync(int id, ComentarioDto comentarioDto);
    }
}