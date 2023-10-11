namespace Tarea2Perros.Models.ViewModels
{
    public class PaisViewModel
    {
        public string Pais { get; set; } = null!;
        public IEnumerable<PerroModel> PerrosxPais { get; set; } = null!;
    }

    public class PerroModel
    {
        public uint Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
