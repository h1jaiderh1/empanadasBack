using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;
using SistemaVenta.Model.Models;


namespace SistemaVenta.BLL.Servicios
{
    public class VentaService: IVentaService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepository<Detalleventa> _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepository ventaRepositorio, 
            IGenericRepository<Detalleventa> detalleVentaRepositorio,
            IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _mapper = mapper;
        }


        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try {

                var ventaGenerada = await _ventaRepositorio.Registrar(_mapper.Map<Venta>(modelo));

                if(ventaGenerada.Idventa == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<VentaDTO>(ventaGenerada);
            
            } catch {
                throw;
            }
        }

        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = await _ventaRepositorio.Consultar();
            var ListaResultado = new List<Venta>();

            try {

                if (buscarPor == "fecha")
                {
                    DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                    DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                    ListaResultado = await query.Where(v =>
                        v.Fecharegistro.Value.Date >= fech_Inicio.Date &&
                        v.Fecharegistro.Value.Date <= fech_Fin.Date
                    ).Include(dv => dv.Detalleventa)
                    .ThenInclude(p => p.IdproductoNavigation)
                    .ToListAsync();
                }
                else {
                    ListaResultado = await query.Where(v => v.Numerodocumento == numeroVenta
                      ).Include(dv => dv.Detalleventa)
                      .ThenInclude(p => p.IdproductoNavigation)
                      .ToListAsync();

                }


            } catch {
                throw;
            }

            return _mapper.Map<List<VentaDTO>>(ListaResultado);

        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<Detalleventa> query = await _detalleVentaRepositorio.Consultar();
            var ListaResultado = new List<Detalleventa>();

            try {

                DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                ListaResultado = await query
                    .Include(p => p.IdproductoNavigation)
                    .Include(v => v.IdventaNavigation)
                    .Where(dv => 
                        dv.IdventaNavigation.Fecharegistro.Value.Date >= fech_Inicio.Date &&
                        dv.IdventaNavigation.Fecharegistro.Value.Date <= fech_Fin.Date
                    ).ToListAsync();

            } catch {
                throw;
            }

            return _mapper.Map<List<ReporteDTO>>(ListaResultado);
        }
    }
}
