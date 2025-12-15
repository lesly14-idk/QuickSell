namespace QuicksellAPI.Models;

public class Categoria
{
    public int id_categoria { get; set; }
    public string nombre_categoria { get; set; } = string.Empty;

    // Relaciones
    public ICollection<Producto>? Productos { get; set; }
}