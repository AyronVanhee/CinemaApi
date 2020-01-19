using Cinema.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
{
    public DBContext _context { get; set; }

    //ctor dependancy van de applicatie context:
    public GenericRepo(DBContext context)
    {
        this._context = context;
    }

    //interface implementatie:
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await this._context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression )
    {
        //voorbeeld bij search: expression = "p => p.ProductID == id"  -- je kent immers de ID property niet.
        //returnt wel een collectie! Gebruik desnoods First().
        return await this._context.Set<TEntity>().Where(expression).AsNoTracking().ToListAsync();
    }

    public async Task Create(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task Update(TEntity entity)
    {
        await Task.Factory.StartNew(() =>
        {
              _context.Set<TEntity>().Update(entity);

        });
    }

    public async Task Delete(TEntity entity)
    {
        await Task.Factory.StartNew(() =>
        {
            _context.Set<TEntity>().Remove(entity);

        });
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}