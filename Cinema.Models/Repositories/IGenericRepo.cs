using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IGenericRepo<T>
    {
        //T: generic EntityType
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByExpressionAsync(Expression<Func<T, bool>> expression); //LINQ expressie
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task SaveAsync();
}