namespace BrainFlow.Data.Models;

public partial class UsuarioTipoMOD
{
    public int CdTipoUsuario { get; set; }

    public string NoTipoUsuario { get; set; } = null!;

    public virtual ICollection<UsuarioMOD> Usuarios { get; set; } = new List<UsuarioMOD>();
}
