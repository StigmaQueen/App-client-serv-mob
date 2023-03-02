public static class Estados
{
    public static string Programado { get; private set; } = "Programado";
    public static string Retrasado { get; private set; } = "Retrasado";
    public static string Desviado { get; private set; } = "Desviado";
    public static string Cancelado { get; private set; } = "Cancelado";
    public static string Abordando { get; private set; } = "Abordando";
    public static string EnVuelo { get; private set; } = "En Vuelo";
    public static string ATiempo { get; private set; } = "A Tiempo";
    public static string Aterrizo { get; private set; } = "Aterrizo";
}
