using EjercisioRifas.Models;

namespace EjercisioRifas.Repositories
{
    public class BoletosRepository
    {
        private readonly Sistem21RifasContext context;

        public BoletosRepository(Sistem21RifasContext context)
        {
            this.context = context;
        }

        public IEnumerable<Boletos> GetAll()
        {
            return context.Boletos.OrderBy(x => x.NumeroBoleto).Where(x => x.Eliminado == 0);
        }
        public IEnumerable<Boletos> GetAllByFecha(DateTime fecha)
        {
            return context.Boletos.OrderBy(x => x.NumeroBoleto).Where(x => x.FechaModificacion>fecha);
        }

        public Boletos? GetById(int id)
        {
            return context.Boletos.Find(id);
        }

        public void Insert(Boletos boleto)
        {
            context.Boletos.Add(boleto);
            context.SaveChanges();
        }

        public void Update(Boletos boleto)
        {
            context.Boletos.Update(boleto);
            context.SaveChanges();
        }

        public void Delete(Boletos boleto)
        {
            context.Boletos.Remove(boleto);
            context.SaveChanges();
        }

        public bool Validate(Boletos boleto, out List<string> errores)
        {
            errores = new();

            if (boleto.NumeroBoleto <= 0)
                errores.Add("Debe indicar el número de boleto");

            if (context.Boletos.Any(x => x.NumeroBoleto == boleto.NumeroBoleto && x.Id != boleto.Id
                && x.Eliminado == 0))
                errores.Add("Este boleto ya ha sido vendido, eliga otro numero de boleto");

            if (string.IsNullOrWhiteSpace(boleto.NombrePersona))
                errores.Add("Debe indicar el nombre de la persona a la cual le vendio el boleto");

            return errores.Count == 0;
        }
    }
}
