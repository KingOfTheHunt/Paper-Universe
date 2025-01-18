using Microsoft.EntityFrameworkCore;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Contracts;
using PaperUniverse.Infra.Data;

namespace PaperUniverse.Infra.Contexts.AccountContext.UseCases.SendPasswordResetCode;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken) => 
        await _context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
}