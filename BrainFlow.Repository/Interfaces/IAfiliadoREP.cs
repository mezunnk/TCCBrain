using BrainFlow.Data.Models;

namespace BrainFlow.Repository.Interfaces
{
    public interface IAfiliadoREP
    {
        /// <summary>
        /// Busca um Afiliado pelo ID do Usuário associado.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        Task<AfiliadoMOD> GetByUsuarioId(int usuarioId);
    }
}