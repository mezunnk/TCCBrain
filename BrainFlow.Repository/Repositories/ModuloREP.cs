using BrainFlow.Data.Models;
using BrainFlow.Repository.Context;
using BrainFlow.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainFlow.Repository.Repositories
{
    public class ModuloREP : BaseREP<ModuloMOD>, IModuloREP
    {
        #region Constructor
        /// <summary>
        /// Construtor que passa o contexto do banco de dados para a classe base.
        /// </summary>
        /// <param name="context">O contexto do EF Core.</param>
        public ModuloREP(BrainFlowContext context) : base(context)
        {
        }
        #endregion

        #region Methods

        #region GetByCursoId
        /// <summary>
        /// Busca uma lista de módulos pertencentes a um curso específico, ordenados por NrOrdem.
        /// </summary>
        /// <param name="cursoId">O ID do curso.</param>
        /// <returns>Uma lista de objetos ModuloMOD.</returns>
        public async Task<List<ModuloMOD>> GetByCursoId(int cursoId)
        {
            return await _context.Modulos
                                 .Where(m => m.CdCurso == cursoId)
                                 .OrderBy(m => m.NrOrdem)
                                 .ToListAsync();
        }
        #endregion

        #region Reordenar
        /// <summary>
        /// Reordena os módulos de um curso com base em uma lista de IDs.
        /// </summary>
        /// <param name="cursoId"></param>
        /// <param name="modulosIds"></param>
        /// <returns></returns>
        public async Task Reordenar(int cursoId, List<int> modulosIds)
        {
            var modulos = await GetByCursoId(cursoId);

            for (int i = 0; i < modulosIds.Count; i++)
            {
                var modulo = modulos.FirstOrDefault(m => m.CdModulo == modulosIds[i]);
                if (modulo != null)
                {
                    modulo.NrOrdem = i + 1;
                }
            }
            await _context.SaveChangesAsync();
        }
        #endregion

        #endregion
    }
}