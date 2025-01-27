namespace ENT
{
    public class Msg
    {
        public string? Usuario { get; set; }
        public string? Mensaje { get; set; }
        public string? Grupo { get; set; }

        public Msg(string? usuario, string? mensaje, string? grupo)
        {
            Usuario = usuario;
            Mensaje = mensaje;
            Grupo = grupo;
        }
    }
}
