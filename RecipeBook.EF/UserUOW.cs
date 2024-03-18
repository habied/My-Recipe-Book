using RecipeBook.Core.Entities;
using RecipeBook.Core.Interfaces;
using RecipeBook.EF.Repositories;

namespace RecipeBook.EF
{
    public class UserUOW : IUserUOW
    {
        private readonly RecipeBookDBContext _context;

        public IBaseRepository<User> Users { get; private set; }


        public UserUOW(RecipeBookDBContext context)
        {
            _context = context;
            Users = new BaseRepository<User>(_context);
        }

        public async Task<int> Complete(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
