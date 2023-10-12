using NuGet.Common;

namespace Tarea2Perros.Models.ViewModels
{
    public class IndexRazasViewModel
    {
        public IEnumerable<ListaPerrosModel> ListaRazas { get; set; } = null!;
        public IEnumerable<char> ListaLetrasAbecedario { get; set; } = null!;
    }

    public class ListaPerrosModel
    {
        public uint Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}