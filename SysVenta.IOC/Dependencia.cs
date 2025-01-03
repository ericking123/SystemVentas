﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SysVenta.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using SysVenta.DAL.Interfaces;
using SysVenta.DAL.Implementacion;
using SysVenta.BLL.Interfaces;
using SysVenta.BLL.Implementacion;

namespace SysVenta.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration Configuration){
            services.AddDbContext<DbventaContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CadenaSQL"));
            });
            services.AddTransient(typeof(InterfazGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<InterfazVentaRepository, VentaRepository>();
            services.AddScoped<InterfazCorreoService, CorreoService>();
            services.AddScoped<InterfazFireBaseService, FireBaseService>();
        }
    }
}
