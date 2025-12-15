namespace QuicksellAPI.Models;

public class Pago
{
    public int id_pagos { get; set; }
    public int id_solicitud { get; set; }
    public string metodo_pago { get; set; } = string.Empty;
    public decimal monto { get; set; }
    public string estado_pago { get; set; } = string.Empty;
    public DateTime fecha_pago { get; set; }
    public string validado_admin { get; set; } = "FALSE";

    // Relaciones
    public SolicitudDeCompra? SolicitudDeCompra { get; set; }
}