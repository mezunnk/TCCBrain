using BrainFlow.Data.Models;
using BrainFlow.Repository.Context;
using BrainFlow.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainFlow.Repository.Repositories
{
    public class UsuarioREP : IUsuarioREP
    {
        #region Context
        private readonly BrainFlowContext _context;
        #endregion

        #region Constructor
        public UsuarioREP(BrainFlowContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region GetByEmail
        /// <summary>
        /// Verifica se um e-mail já existe no banco
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UsuarioMOD> GetByEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.TxEmail == email);
        }
        #endregion

        #region Add
        /// <summary>
        /// Adiciona um novo usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<UsuarioMOD> Add(UsuarioMOD usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
        #endregion

        #region GetByEmailWithLogin
        /// <summary>
        /// Busca um usuário pelo e-mail, incluindo seus dados de login.
        /// </summary>
        /// <param name="email">Email a ser buscado.</param>
        /// <returns>O usuário encontrado ou nulo.</returns>
        public async Task<UsuarioMOD> GetByEmailWithLogin(string email)
        {
            return await _context.Usuarios
                                 .Include(u => u.UsuarioLogins)
                                 .FirstOrDefaultAsync(u => u.TxEmail == email);
        }
        #endregion

        #region GetByToken
        /// <summary>
        /// Busca um usuário pelo token de redefinição de senha.
        /// </summary>
        /// <param name="token">Token a ser buscado.</param>
        /// <returns>O usuário encontrado ou nulo.</returns>
        public async Task<UsuarioMOD> GetByToken(string token)
        {
            return await _context.Usuarios
                                 .Include(u => u.UsuarioLogins)
                                 .FirstOrDefaultAsync(u => u.UsuarioLogins.Any(l => l.TxTokenRecuperacao == token && l.DtValidadeToken > DateTime.Now));
        }
        #endregion

        #region Update
        /// <summary>
        /// Atualiza os dados de um usuário no banco.
        /// </summary>
        /// <param name="usuario">Objeto usuário com dados modificados.</param>
        public async Task Update(UsuarioMOD usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
        #endregion

        #endregion
    }
}