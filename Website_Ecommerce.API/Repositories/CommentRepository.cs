using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelQueries;

namespace Website_Ecommerce.API.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _dataContext;

        public CommentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IQueryable<Comment> Comments => _dataContext.Comments;

        public IUnitOfWork UnitOfWork => _dataContext;

        public void Add(Comment comment)
        {
            _dataContext.Comments.Add(comment);
        }

        public void Delete(Comment comment)
        {
            _dataContext.Entry(comment).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }

        public void Update(Comment comment)
        {
            _dataContext.Entry(comment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task<List<UserCommentQueryModel>> GetComments()
        {
            var listComment = from comment in _dataContext.Comments 
                            join user in _dataContext.Users  on comment.UserId equals user.Id
                            select new UserCommentQueryModel {
                                Username = user.Username,
                                Avatar = user.UrlAvatar,
                                Content = comment.Content
                            };
            return (List<UserCommentQueryModel>)listComment;               
            
        }

    }
}