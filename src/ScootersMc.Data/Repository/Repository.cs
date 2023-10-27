using Microsoft.EntityFrameworkCore;
using ScootersMc.Business.Interfaces;
using ScootersMc.Business.Models;
using ScootersMc.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(MeuDbContext db)
        {
            _context = db;
            _dbSet = db.Set<TEntity>();

        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Task<List<TEntity>> ObterTodos()
        {
            return _dbSet.ToListAsync();
        }

        public async Task Adicionar(TEntity entity)
        {
            _context.Add(entity); // Guarda o obj na memoria para poder salvar no banco.
            await SaveChange();
        }

        public async Task Atualizar(TEntity entity)
        {
            _context.Update(entity);
            await SaveChange();
        }

        public async Task Remover(Guid id)
        {
            _dbSet.Remove(new TEntity { Id = id });
            await SaveChange();
        }

        public async Task<int> SaveChange()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
