namespace APIDocentes.DTo_s
{
    public class DocenteDTO
    {
        public int Id { get; set; }
        public string Numero { get; set; } = null!;
        public string Nombre { get; set;} = null!;
        public string? Correo { get; set; }= null!;

    }
}
