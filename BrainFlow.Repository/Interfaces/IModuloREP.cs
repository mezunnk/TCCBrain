using BrainFlow.Data.Models;

namespace BrainFlow.Repository.Interfaces
{
    public interface IModuloREP : IBaseREP<ModuloMOD>
    {
        /// <summary>
        /// Busca uma lista de módulos pertencentes a um curso específico.
        /// </summary>
        /// <param name="cursoId">O ID do curso.</param>
        /// <returns>Uma lista de objetos ModuloMOD.</returns>
        Task<List<ModuloMOD>> GetByCursoId(int cursoId);

        /// <summary>
        /// Reordena os módulos de um curso com base em uma lista de IDs.
        /// </summary>
        /// <param name="cursoId"></param>
        /// <param name="modulosIds"></param>
        /// <returns></returns>
        Task Reordenar(int cursoId, List<int> modulosIds);
    }
}