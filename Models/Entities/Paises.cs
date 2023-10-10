using System;
using System.Collections.Generic;

namespace Tarea2Perros.Models.Entities;

public partial class Paises
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Razas> Razas { get; set; } = new List<Razas>();
}
