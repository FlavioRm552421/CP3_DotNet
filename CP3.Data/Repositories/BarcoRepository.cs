using CP3.Data.AppData;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CP3.Data.Repositories
{
    public class BarcoRepository : IBarcoRepository
    {
        private readonly ApplicationContext _context;

        public BarcoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public BarcoEntity? ObterPorId(int id)
        {
            return _context.Barco.Find(id);
        }

        public IEnumerable<BarcoEntity> ObterTodos()
        {
            return _context.Barco.ToList() ?? new List<BarcoEntity>(); 
        }

        public BarcoEntity? Adicionar(BarcoEntity barco)
        {
            _context.Barco.Add(barco);
            _context.SaveChanges();
            return barco;
        }

        public BarcoEntity? Editar(BarcoEntity barco)
        {
            _context.Barco.Update(barco);
            _context.SaveChanges();
            return barco;
        }

        public BarcoEntity? Remover(int id)
        {
            var barco = ObterPorId(id);
            if (barco != null)
            {
                _context.Barco.Remove(barco);
                _context.SaveChanges();
            }
            return barco;
        }
    }
}
