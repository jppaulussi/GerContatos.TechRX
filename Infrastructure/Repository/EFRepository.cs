using Core.Interfaces.Repositories;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;

public class EFRepository<T> : IRepository<T> where T : EntityBase
{
    protected AppDbContext _context;
    protected DbSet<T> _dbSet;

    public EFRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task Create(T entidade)
    {
        _dbSet.Add(entidade);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
         _dbSet.Remove(await GetById(id));
        await _context.SaveChangesAsync();
    }

    public async Task<IList<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetById(int id)
        => await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);

    public async Task Update(T entidade)
    {
        _dbSet.Update(entidade);
        await _context.SaveChangesAsync();
    }
}
