using Net5.Fundamentals.EF.CodeFirst.Data.Entities;
using Net5.Fundamentals.EF.CodeFirst.Data.Repositories.Base;
using System.Collections.Generic;

namespace Net5.Fundamentals.EF.CodeFirst.Data.Repositories
{
    public interface IPostRepository: IGenericRepository<Post>
    {
        Post GetById(int postId);
        List<Post> GetPosts();
    }
}