using ClienteTACOSWeb.Modelos;

namespace ClienteTACOSWeb.Negocio;
public class Credenciales : RespuestaBase
{
    public MiembroModelo? Miembro { get; set; } = new MiembroModelo();
    public StaffModelo? Staff { get; set; } = new StaffModelo();
    public string Token { get; set; }
    public string Expira { get; set; }

    public int IdPuesto => this.Staff is null? -1 : this.Staff.IdPuesto;
    public bool EsMiembro => this.Miembro != null && this.Miembro.Id != 0;
    public bool EsStaff => this.Staff != null && this.Staff.Id > 0 && this.Staff.Id < 3;
}