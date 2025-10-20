namespace BrainFlow.UI.Web.ViewModels
{
    public class UserInfoViewMOD
    {
        public int CdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string TipoUsuario { get; set; }
        public int CdTipoUsuario { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsAfiliado { get; set; }
        public bool IsComum { get; set; }
    }

    public class AuthResponseViewMOD
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserInfoViewMOD UserInfo { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}