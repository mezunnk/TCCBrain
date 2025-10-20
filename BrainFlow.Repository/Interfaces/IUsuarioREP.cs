using BrainFlow.Data.Models;

namespace BrainFlow.Repository.Interfaces
{
    public interface IUsuarioREP
    {
        /// <summary>
        /// Verifica se um e-mail já existe no banco
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UsuarioMOD> GetByEmail(string email);

        /// <summary>
        /// Verifica se um e-mail já existe no banco (Async)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UsuarioMOD> GetByEmailAsync(string email);

        /// <summary>
        /// Adiciona um novo usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<UsuarioMOD> Add(UsuarioMOD usuario);

        /// <summary>
        /// Adiciona um novo usuário (Async)
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        Task<UsuarioMOD> CreateAsync(UsuarioMOD usuario, string senha);

        /// <summary>
        /// Registra um login do usuário
        /// </summary>
        /// <param name="cdUsuario"></param>
        /// <returns></returns>
        Task RegistrarLoginAsync(int cdUsuario);

        /// <summary>
        /// Busca um usuário pelo e-mail, incluindo seus dados de login.
        /// </summary>
        /// <param name="email">Email a ser buscado.</param>
        /// <returns>O usuário encontrado ou nulo.</returns>
        Task<UsuarioMOD> GetByEmailWithLogin(string email);

        /// <summary>
        /// Busca um usuário pelo token de redefinição de senha.
        /// </summary>
        /// <param name="token">Token a ser buscado.</param>
        /// <returns>O usuário encontrado ou nulo.</returns>
        Task<UsuarioMOD> GetByToken(string token);

        /// <summary>
        /// Atualiza os dados de um usuário no banco.
        /// </summary>
        /// <param name="usuario">Objeto usuário com dados modificados.</param>
        Task Update(UsuarioMOD usuario);

    }
}