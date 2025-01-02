using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using SysVenta.DAL.DBContext;
using SysVenta.DAL.Interfaces;
using SysVenta.Entity;

namespace SysVenta.DAL.Implementacion
{
    public class VentaRepository : GenericRepository<Venta>, InterfazVentaRepository
    {
        private readonly DbventaContext _dbventaContext;

        public VentaRepository(DbventaContext dbventaContext):base(dbventaContext)
        {
            _dbventaContext = dbventaContext;
        }

        public async Task<Venta> Registrar(Venta entidad)
        {
            Venta ventaGenerada = new Venta();
            using (var transaction = _dbventaContext.Database.BeginTransaction()) {
                try
                {
                    foreach (DetalleVenta dv in entidad.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbventaContext.Productos.Where(P => P.IdProducto == dv.IdProducto).First();
                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbventaContext.Productos.Update(producto_encontrado);
                    }
                    await _dbventaContext.SaveChangesAsync();
                    NumeroCorrelativo correlativo = _dbventaContext.NumeroCorrelativos.Where(n => n.Gestion == "Venta").First();
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaActualizada = DateTime.Now;

                    _dbventaContext.NumeroCorrelativos.Update(correlativo);
                    await _dbventaContext.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);

                    await _dbventaContext.Venta.AddAsync(entidad);
                    await _dbventaContext.SaveChangesAsync();

                    ventaGenerada = entidad;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return ventaGenerada;
        }

        public async Task<List<DetalleVenta>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleVenta> listaResumen = await _dbventaContext.DetalleVenta
                .Include(v => v.IdVentaNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(v => v.IdVentaNavigation)
                .ThenInclude(tdv => tdv.IdTipoDocumentoVentaNavigation)
                .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= FechaInicio.Date && dv.IdVentaNavigation.FechaRegistro.Value.Date <= FechaFin.Date)
                .ToListAsync();

            return listaResumen;
        }
    }
}
