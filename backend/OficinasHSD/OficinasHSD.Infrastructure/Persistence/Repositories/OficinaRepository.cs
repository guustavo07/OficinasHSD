using Microsoft.EntityFrameworkCore;
using OficinasHSD.Application.Interfaces.Repositorios;
using OficinasHSD.Domain.Models;
using OficinasHSD.Infrastructure.Persistence.Context;

namespace OficinasHSD.Infrastructure.Persistence.Repositories;

public class OficinaRepository : IOficinaRepository
{
    protected new readonly OficinasHSDDbContext _context;
    protected readonly DbSet<OficinaMecanica> _dbSet;
    public OficinaRepository(OficinasHSDDbContext context) 
    {
        _context = context;
        _dbSet = context.Set<OficinaMecanica>();

    }

    public virtual async Task<OficinaMecanica?> ObterPorIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<OficinaMecanica>> ObterTodosAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task AdicionarAsync(OficinaMecanica entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Atualizar(OficinaMecanica entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Remover(Guid id)
    {
        _dbSet.Remove(new OficinaMecanica { Id = id });
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}