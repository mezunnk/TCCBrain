using BrainFlow.Data.Models;
using BrainFlow.Repository.Context;
using BrainFlow.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

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

        /// <summary>
        /// Verifica se um e-mail já existe no banco (Async)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UsuarioMOD> GetByEmailAsync(string email)
        {
            return await _context.Usuarios
                .Include(u => u.CdTipoUsuarioNavigation)
                .Include(u => u.UsuarioLogins)
                .FirstOrDefaultAsync(u => u.TxEmail == email && u.SnAtivo == true);
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

        /// <summary>
        /// Adiciona um novo usuário (Async)
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public async Task<UsuarioMOD> CreateAsync(UsuarioMOD usuario, string senha)
        {
            // Criar usuário
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // Gerar hash da senha
            var senhaHash = GerarHashSenha(senha);

            // Criar registro de login com senha
            var usuarioLogin = new UsuarioLoginMOD
            {
                CdUsuario = usuario.CdUsuario,
                TxSenhaHash = senhaHash,
                DtCadastro = DateTime.Now
            };

            await _context.UsuarioLogins.AddAsync(usuarioLogin);
            await _context.SaveChangesAsync();

            return usuario;
        }

        /// <summary>
        /// Gera hash da senha usando BCrypt.Net-Next conforme especificado no TCC
        /// </summary>
        /// <param name="senha"></param>
        /// <returns></returns>
        private string GerarHashSenha(string senha)
        {
            // Usar BCrypt.Net-Next conforme requisitos técnicos do TCC
            return BCrypt.Net.BCrypt.HashPassword(senha, workFactor: 12);
        }
        #endregion

        #region RegistrarLogin
        /// <summary>
        /// Registra um login do usuário
        /// </summary>
        /// <param name="cdUsuario"></param>
        /// <returns></returns>
        public async Task RegistrarLoginAsync(int cdUsuario)
        {
            // Para registro de login, vamos apenas atualizar a data de alteração
            var usuario = await _context.Usuarios.FindAsync(cdUsuario);
            if (usuario != null)
            {
                usuario.DtAlteracao = DateTime.Now;
                await _context.SaveChangesAsync();
            }
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