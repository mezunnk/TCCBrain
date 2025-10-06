using BrainFlow.Data.Models;

namespace BrainFlow.Repository.Interfaces
{
    public interface ICursoREP
    {
        /// <summary>
        /// Busca todos os cursos de um afiliado específico.
        /// </summary>
        /// <param name="afiliadoId"></param>
        /// <returns></returns>
        Task<List<CursoMOD>> GetByAfiliadoId(int afiliadoId);

        /// <summary>
        /// Busca todos os cursos disponíveis publicamente.
        /// </summary>
        /// <returns></returns>
        Task<List<CursoMOD>> GetAll();

        /// <summary>
        /// Busca cursos públicos com filtro por nome.
        /// </summary>
        /// <param name="nomeCurso"></param>
        /// <returns></returns>
        Task<List<CursoMOD>> GetAllFiltered(string nomeCurso);

        /// <summary>
        /// Busca cursos de um afiliado, com filtro opcional por nome.
        /// </summary>
        /// <param name="afiliadoId"></param>
        /// <param name="nomeCurso"></param>
        /// <returns></returns>
        Task<List<CursoMOD>> GetByAfiliadoIdFiltered(int afiliadoId, string nomeCurso);

        /// <summary>
        /// Adiciona um novo curso no banco de dados.
        /// </summary>
        /// <param name="curso"></param>
        /// <returns></returns>
        Task<CursoMOD> Add(CursoMOD curso);

        /// <summary>
        /// Busca um curso específico pelo seu ID.
        /// </summary>
        /// <param name="cursoId"></param>
        /// <returns></returns>
        Task<CursoMOD> GetById(int cursoId);

        /// <summary>
        /// Atualiza os dados de um curso existente.
        /// </summary>
        /// <param name="curso"></param>
        /// <returns></returns>
        Task<CursoMOD> Update(CursoMOD curso);

        /// <summary>
        /// Exclui um curso do banco de dados.
        /// </summary>
        /// <param name="cursoId"></param>
        /// <returns></returns>
        Task<bool> Delete(int cursoId);
    }
}