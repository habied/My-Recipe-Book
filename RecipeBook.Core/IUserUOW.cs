using System;
using System.Threading.Tasks;
using RecipeBook.Core.Entities;

namespace RecipeBook.Core.Interfaces
{
    public interface IUserUOW : IDisposable
    {
        IBaseRepository<User> Users { get; }

        Task<int> Complete(CancellationToken token);
    }
}
