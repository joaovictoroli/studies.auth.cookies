namespace studies.auth.cookies.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string KnownAs { get; set; }
        public List<AdressViewModel> Addresses { get; set; }
    }

    public class AdressViewModel
    {
        public string CEP { get; set; }
        public string Bairro { get; set; }
    }
}
