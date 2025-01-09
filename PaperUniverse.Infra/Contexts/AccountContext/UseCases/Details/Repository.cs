using Microsoft.EntityFrameworkCore;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Details.Contracts;
using PaperUniverse.Infra.Data;

namespace PaperUniverse.Infra.Contexts.AccountContext.UseCases.Details;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetUserAsync(string email) =>
        await _context.User.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email);
}