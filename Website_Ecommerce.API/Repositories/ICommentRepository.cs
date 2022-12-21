using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelQueries;

namespace Website_Ecommerce.API.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IQueryable<Comment> Comments { get; }
        void Add(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);

        Task<List<UserCommentQueryModel>> GetComments();
    }
}