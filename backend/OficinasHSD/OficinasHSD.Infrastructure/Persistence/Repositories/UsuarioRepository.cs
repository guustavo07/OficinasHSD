using Microsoft.EntityFrameworkCore;
using OficinasHSD.Application.Interfaces.Repositorios;
using OficinasHSD.Domain.Models;
using OficinasHSD.Infrastructure.Persistence.Context;

namespace OficinasHSD.Infrastructure.Persistence.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    protected new readonly OficinasHSDDbContext _context;
    protected readonly DbSet<Usuario> _dbSet;
    public UsuarioRepository(OficinasHSDDbContext context)
    {
        _context = context;
        _dbSet = context.Set<Usuario>();
    }

    public async Task<Usuario> ObterPorUsername(string username)
    {
        return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
    }

    public virtual async Task AdicionarAsync(Usuario entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}