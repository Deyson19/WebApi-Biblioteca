
namespace WebApi_Helpers
{
    public static class CantidadMaximaPrestamo
    {
        public static DateTime FechaMaximaPrestamo(int idTipoUsuario)
        {
            DateTime fechaMaxima = DateTime.Now;

            switch (idTipoUsuario)
            {
                case 1:
                    fechaMaxima = fechaMaxima.AddDays(10);
                    break;
                case 2:
                    fechaMaxima = fechaMaxima.AddDays(8);
                    break;
                case 3:
                    fechaMaxima = fechaMaxima.AddDays(7);
                    break;
            }

            if (fechaMaxima.DayOfWeek == DayOfWeek.Saturday)
            {
                fechaMaxima = fechaMaxima.AddDays(2); //lunes
            }
            else if (fechaMaxima.DayOfWeek == DayOfWeek.Sunday)
            {
                fechaMaxima = fechaMaxima.AddDays(1); // lunes
            }

            return fechaMaxima;
        }
    }
}