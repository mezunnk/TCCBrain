using BrainFlow.Data.Models;
using BrainFlow.Repository.Context;
using BrainFlow.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainFlow.Repository.Repositories
{
    public class CursoREP : ICursoREP
    {
        #region Context
        private readonly BrainFlowContext _context;
        #endregion

        #region Constructor
        public CursoREP(BrainFlowContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region GetByAfiliadoId
        /// <summary>
        /// Busca todos os cursos de um afiliado específico.
        /// </summary>
        public async Task<List<CursoMOD>> GetByAfiliadoId(int afiliadoId)
        {
            return await _context.Cursos
                                 .Where(c => c.CdAfiliado == afiliadoId)
                                 .ToListAsync();
        }
        #endregion

        #region GetByAfiliadoIdFiltered
        /// <summary>
        /// Busca cursos de um afiliado, com filtro opcional por nome.
        /// </summary>
        /// <param name="afiliadoId"></param>
        /// <param name="nomeCurso"></param>
        /// <returns></returns>
        public async Task<List<CursoMOD>> GetByAfiliadoIdFiltered(int afiliadoId, string nomeCurso)
        {
            var query = _context.Cursos
                                .Where(c => c.CdAfiliado == afiliadoId);

            if (!string.IsNullOrEmpty(nomeCurso))
            {
                query = query.Where(c => c.NoCurso.Contains(nomeCurso));
            }

            return await query.ToListAsync();
        }
        #endregion

        #region GetAll
        /// <summary>
        /// Busca todos os cursos disponíveis publicamente.
        /// </summary>
        public async Task<List<CursoMOD>> GetAll()
        {
            return await _context.Cursos
                                 .Where(c => c.SnAtivo && c.SnAprovado)
                                 .OrderByDescending(c => c.DtCadastro)
                                 .ToListAsync();
        }
        #endregion

        #region GetAllFiltered
        /// <summary>
        /// Busca cursos públicos com filtro por nome.
        /// </summary>
        /// <param name="nomeCurso"></param>
        /// <returns></returns>
        public async Task<List<CursoMOD>> GetAllFiltered(string nomeCurso)
        {
            var query = _context.Cursos
                                .Where(c => c.SnAtivo && c.SnAprovado);

            if (!string.IsNullOrEmpty(nomeCurso))
            {
                query = query.Where(c => c.NoCurso.Contains(nomeCurso));
            }

            return await query.OrderByDescending(c => c.DtCadastro)
                              .ToListAsync();
        }
        #endregion

        #region Add
        /// <summary>
        /// Adiciona um novo curso no banco de dados.
        /// </summary>
        public async Task<CursoMOD> Add(CursoMOD curso)
        {
            await _context.Cursos.AddAsync(curso);
            await _context.SaveChangesAsync();
            return curso;
        }
        #endregion

        #region GetById
        /// <summary>
        /// Busca um curso específico pelo seu ID.
        /// </summary>
        /// <param name="cursoId"></param>
        /// <returns></returns>
        public async Task<CursoMOD> GetById(int cursoId)
        {
            return await _context.Cursos.FirstOrDefaultAsync(c => c.CdCurso == cursoId);
        }
        #endregion

        #region Update
        /// <summary>
        /// Atualiza os dados de um curso existente.
        /// </summary>
        /// <param name="curso"></param>
        /// <returns></returns>
        public async Task<CursoMOD> Update(CursoMOD curso)
        {
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
            return curso;
        }
        #endregion

        #region Delete
        /// <summary>
        /// Exclui um curso do banco de dados.
        /// </summary>
        /// <param name="cursoId"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int cursoId)
        {
            var curso = await GetById(cursoId);
            if (curso == null) return false;

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #endregion
    }
}