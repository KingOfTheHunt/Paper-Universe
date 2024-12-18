using Microsoft.EntityFrameworkCore;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using PaperUniverse.Infra.Data;

namespace PaperUniverse.Infra.Contexts.AccountContext.UseCases.Create;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AnyEmailAsync(string email, CancellationToken cancellationToken) => 
        await _context.User.AnyAsync(x => x.Email.Address == email, cancellationToken);

    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        await _context.User.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}