﻿using System;
using System.Collections.Generic;
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
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar();
                var listaUsuarios = queryUsuario.Include(rol => rol.IdrolNavigation).ToList();

                return _mapper.Map<List<UsuarioDTO>>(listaUsuarios);
            }
            catch {
                throw;
            }


        }

        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar(u =>
                    u.Correo == correo &&
                    u.Clave == clave
                );

                if(queryUsuario.FirstOrDefault() == null)
                    throw new TaskCanceledException("El usuario no existe");

                Usuario devolverUsuario = queryUsuario.Include(rol => rol.IdrolNavigation).First();

                return _mapper.Map<SesionDTO>(devolverUsuario);

            }
            catch {
                throw;
            }


        }

        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try {

                var usuarioCreado = await _usuarioRepositorio.Crear(_mapper.Map<Usuario>( modelo));

                if(usuarioCreado.Idusuario == 0)
                    throw new TaskCanceledException("No se pudo crear");

                var query = await _usuarioRepositorio.Consultar(u => u.Idusuario == usuarioCreado.Idusuario);

                usuarioCreado = query.Include(rol => rol.IdrolNavigation).First();

                return _mapper.Map<UsuarioDTO>(usuarioCreado);

            }
            catch {
                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>( modelo);

                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.Idusuario == usuarioModelo.Idusuario);

                if(usuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                usuarioEncontrado.Nombrecompleto = usuarioModelo.Nombrecompleto;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.Idrol = usuarioModelo.Idrol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.Esactivo = usuarioModelo.Esactivo;

                bool respuesta = await _usuarioRepositorio.Editar(usuarioEncontrado);

                if(!respuesta)
                    throw new TaskCanceledException("No se pudo editar");

                return respuesta;
            }
            catch {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var usuarioEcontrado = await _usuarioRepositorio.Obtener(u => u.Idusuario == id);

                if(usuarioEcontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                bool respuesta = await _usuarioRepositorio.Eliminar(usuarioEcontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo eliminar");

                return respuesta;
            }
            catch {
                throw;
            }
        }

      
    }
}
