using Microsoft.EntityFrameworkCore;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts;
using PaperUniverse.Infra.Data;

namespace PaperUniverse.Infra.Contexts.AccountContext.UseCases.ResendVerification;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.User
            .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
    }

    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        _context.User.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}