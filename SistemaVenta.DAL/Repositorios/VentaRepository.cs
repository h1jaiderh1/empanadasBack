using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DAL;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.Model;
using SistemaVenta.Model.Models;

namespace SistemaVenta.DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta> , IVentaRepository
    {

        private readonly DbempanadasContext _dbcontext;

        public VentaRepository(DbempanadasContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using (var trasaction = _dbcontext.Database.BeginTransaction())
            {
                try {

                    foreach (Detalleventa dv in modelo.Detalleventa) {
                        
                        Producto producto_encontrado = _dbcontext.Productos.Where(p => p.Idproducto == dv.Idproducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbcontext.Productos.Update(producto_encontrado);
                    }
                    await _dbcontext.SaveChangesAsync();

                    Numerodocumento correlativo = _dbcontext.Numerodocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.Fecharegistro = DateTime.Now;

                    _dbcontext.Numerodocumentos.Update(correlativo);
                    await _dbcontext.SaveChangesAsync();

                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    //00001
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    modelo.Numerodocumento = numeroVenta;

                    await _dbcontext.Venta.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();

                    ventaGenerada = modelo;

                    trasaction.Commit();
 
                } catch {
                    
                    trasaction.Rollback();
                    throw;
                }

                return ventaGenerada;
            
            }



        }
    }
}
