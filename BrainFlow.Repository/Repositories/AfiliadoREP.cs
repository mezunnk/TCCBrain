using BrainFlow.Data.Models;
using BrainFlow.Repository.Context;
using BrainFlow.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainFlow.Repository.Repositories
{
    public class AfiliadoREP : IAfiliadoREP
    {
        #region Context
        private readonly BrainFlowContext _context;
        #endregion

        #region Constructor
        public AfiliadoREP(BrainFlowContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region GetByUsuarioId
        /// <summary>
        /// Busca um Afiliado pelo ID do Usuário associado.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public async Task<AfiliadoMOD> GetByUsuarioId(int usuarioId)
        {
            return await _context.Afiliados.FirstOrDefaultAsync(a => a.CdUsuario == usuarioId);
        }
        #endregion

        #endregion
    }
}