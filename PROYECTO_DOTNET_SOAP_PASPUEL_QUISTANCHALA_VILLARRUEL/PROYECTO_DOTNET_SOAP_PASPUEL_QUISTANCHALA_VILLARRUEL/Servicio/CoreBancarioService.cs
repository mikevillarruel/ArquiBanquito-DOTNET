using PROYECTO_DOTNET_SOAP_PASPUEL_QUISTANCHALA_VILLARRUEL.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_DOTNET_SOAP_PASPUEL_QUISTANCHALA_VILLARRUEL.Servicio
{
    public class CoreBancarioService
    {
        public Cliente verificarCliente(String cedula, String cuenta)
        {
            Cliente rec = new Cliente();
            try
            {
                DB.corebancarioEntities db = new DB.corebancarioEntities();

                var cliente = (from c in db.CUENTA
                               join cl in db.CLIENTE on c.ID_CLIENTE equals cl.ID_CLIENTE
                               where c.NRO_CUENTA == cuenta && cl.CEDULA_CLIENTE == cedula
                               select cl).FirstOrDefault();

                if (cliente != null)
                {
                    rec.apellidos_cliente = cliente.APELLIDOS_CLIENTE;
                    rec.cedula_cliente = cliente.CEDULA_CLIENTE;
                    rec.direccion_cliente = cliente.DIRECCION_CLIENTE;
                    rec.correo_cliente = cliente.CORREO_CLIENTE;
                    rec.nombres_cliente = cliente.NOMBRES_CLIENTE;
                    rec.id_cliente = cliente.ID_CLIENTE;
                    rec.telefono_cliente = cliente.TELEFONO_CLIENTE;


                    DB.USUARIO usuario = new DB.USUARIO();
                    DB.bancaelectronicaEntities dbBanca = new DB.bancaelectronicaEntities();

                    usuario.CEDULA_CLIENTE = cliente.CEDULA_CLIENTE;
                    usuario.ID_CLIENTE = cliente.ID_CLIENTE;
                    usuario.CAMBIO_USUARIO = 0;

                    //GENERAR CONTRASEÑA
                    string contrasenia = GenerarContrasenia();

                    //ENVIAR CORREO


                    //ENCRIPTAR CONTRASEÑA
                    usuario.PASSWORD_USUARIO = Encriptar(contrasenia);

                    string nombreUsuario = cliente.NOMBRES_CLIENTE[0] + cliente.APELLIDOS_CLIENTE;


                    //VERIFICA QUE EL CLIENTE NO TENGA USUARIO
                    var cl = (from u in dbBanca.USUARIO
                              where u.ID_CLIENTE == cliente.ID_CLIENTE
                              select u).FirstOrDefault();
                    if (cl == null)
                    {
                        int i = (from u in dbBanca.USUARIO where u.NOMBRE_USUARIO.StartsWith(nombreUsuario) select u).ToList<DB.USUARIO>().Count;

                        if (i == 0)
                        {
                            usuario.NOMBRE_USUARIO = nombreUsuario;
                        }
                        else
                        {
                            usuario.NOMBRE_USUARIO = nombreUsuario + i;
                        }

                        dbBanca.USUARIO.Add(usuario);
                        dbBanca.SaveChanges();

                    }
                    else
                    {
                        return new Cliente();
                    }
                }

                return rec;
            }
            catch (Exception e)
            {
                return rec;
            }

        }

        public String GenerarContrasenia()
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 6;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }
            return contraseniaAleatoria;
        }

        public String GenerarCuenta()
        {
            Random rdn = new Random();
            string caracteres = "0123456789";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 8;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }
            return contraseniaAleatoria;
        }

        public String Encriptar(String contrasenia)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(contrasenia);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public Boolean inicioSesion(String usuarioNombre, String contrasena)
        {

            DB.bancaelectronicaEntities db = new DB.bancaelectronicaEntities();
            //contrasena = Encriptar(contrasena); // EN CLIENTE
            var sql = (from u in db.USUARIO
                       where u.NOMBRE_USUARIO == usuarioNombre && u.PASSWORD_USUARIO == contrasena
                       select u).FirstOrDefault();

            return sql == null ? false : true;
        }

        public List<Cuenta> posicionConsolidada(String cedula)
        {
            List<Cuenta> lista = new List<Cuenta>();
            DB.corebancarioEntities db = new DB.corebancarioEntities();
            var sqlLista = (from c in db.CUENTA
                            where c.ID_CLIENTE == (from cl in db.CLIENTE
                                                   where cl.CEDULA_CLIENTE == cedula
                                                   select cl.ID_CLIENTE).FirstOrDefault()
                            select c).ToList<DB.CUENTA>();



            if (sqlLista != null)
            {
                foreach (DB.CUENTA item in sqlLista)
                {
                    Cuenta c = new Cuenta();
                    c.id_cliente = item.ID_CLIENTE;
                    c.id_cuenta = item.ID_CUENTA;
                    c.nro_cuenta = item.NRO_CUENTA;
                    c.saldo_cuenta = (decimal)item.SALDO_CUENTA;
                    c.tipo_cuenta = item.TIPO_CUENTA;
                    lista.Add(c);
                }
                return lista;
            }
            else
            {
                return lista;
            }

        }
        public List<Movimiento> detalleMovimientos(String cuenta)
        {
            List<Movimiento> lista = new List<Movimiento>();
            DB.corebancarioEntities db = new DB.corebancarioEntities();

            var sqlLista = (from m in db.MOVIMIENTO
                            where m.CUENTA.NRO_CUENTA == cuenta
                            select m).ToList<DB.MOVIMIENTO>();

            if (sqlLista != null)
            {
                foreach (DB.MOVIMIENTO item in sqlLista)
                {
                    Movimiento m = new Movimiento();
                    m.id_cuenta = item.ID_CUENTA;
                    m.id_movimiento = item.ID_MOVIMIENTO;
                    m.importe_movimiento = (decimal)item.IMPORTE_MOVIMIENTO;
                    m.saldo_movimiento = (decimal)item.SALDO_MOVIMIENTO;
                    m.tipo_movimiento = item.TIPO_MOVIMIENTO;
                    m.fecha_movimiento = (DateTime)item.FECHA_MOVIMIENTO;
                    m.destino_movimiento = item.DESTINO_MOVIMIENTO;
                    lista.Add(m);
                }
                return lista;
            }
            else
            {
                return lista;
            }

        }

        public Boolean transferencias(String cuentaOrigen, Double importe, String cuentaDestino)
        {
            try
            {
                DB.corebancarioEntities db = new DB.corebancarioEntities();
                DB.MOVIMIENTO m = new DB.MOVIMIENTO();

                //VERIFICAR SALDO

                DB.CUENTA cuentaO = (from c in db.CUENTA
                                     where c.NRO_CUENTA == cuentaOrigen
                                     select c).FirstOrDefault();

                DB.CUENTA cuentaD = (from c in db.CUENTA
                                     where c.NRO_CUENTA == cuentaDestino
                                     select c).FirstOrDefault();

                //VALIDAR SALDO EN ORIGEN  // CUENTA DESTINO VÁLIDA  // CUENTA DISTINTA
                if (cuentaO.SALDO_CUENTA >= (decimal)importe && cuentaD != null && cuentaO.ID_CUENTA != cuentaD.ID_CUENTA)
                {

                    cuentaO.SALDO_CUENTA = cuentaO.SALDO_CUENTA - (decimal)importe;
                    cuentaD.SALDO_CUENTA = cuentaD.SALDO_CUENTA + (decimal)importe;

                    m.ID_CUENTA = cuentaO.ID_CUENTA;
                    m.DESTINO_MOVIMIENTO = cuentaDestino;
                    m.FECHA_MOVIMIENTO = DateTime.Now;
                    m.IMPORTE_MOVIMIENTO = (Decimal)importe;
                    m.TIPO_MOVIMIENTO = "Débito";
                    m.SALDO_MOVIMIENTO = cuentaO.SALDO_CUENTA;

                    db.MOVIMIENTO.Add(m);

                    DB.MOVIMIENTO m2 = new DB.MOVIMIENTO();
                    m2.ID_CUENTA = cuentaD.ID_CUENTA;
                    m2.DESTINO_MOVIMIENTO = cuentaOrigen;
                    m2.FECHA_MOVIMIENTO = DateTime.Now;
                    m2.IMPORTE_MOVIMIENTO = (Decimal)importe;
                    m2.TIPO_MOVIMIENTO = "Crédito";
                    m2.SALDO_MOVIMIENTO = cuentaD.SALDO_CUENTA; ;

                    db.MOVIMIENTO.Add(m2);

                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public Boolean registrarCuentaBancaria(int idCliente, String tipoCuenta, double saldoInicial)
        {

            try
            {
                DB.corebancarioEntities db = new DB.corebancarioEntities();
                DB.CUENTA cuenta = new DB.CUENTA();
                cuenta.ID_CLIENTE = idCliente;
                cuenta.TIPO_CUENTA = tipoCuenta;
                cuenta.SALDO_CUENTA = (decimal)saldoInicial;
                cuenta.NRO_CUENTA = GenerarCuenta();
                var sql = (from c in db.CUENTA
                           where c.NRO_CUENTA == cuenta.NRO_CUENTA
                           select c).FirstOrDefault();
                while (sql != null)
                {
                    cuenta.NRO_CUENTA = GenerarCuenta();
                    sql = (from c in db.CUENTA
                           where c.NRO_CUENTA == cuenta.NRO_CUENTA
                           select c).FirstOrDefault();
                }

                db.CUENTA.Add(cuenta);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Usuario obtenerUsuario(string nombreUsuario) {

            DB.bancaelectronicaEntities dbBanca = new DB.bancaelectronicaEntities();
            var usuario = (from u in dbBanca.USUARIO
                           where u.NOMBRE_USUARIO == nombreUsuario
                           select u).FirstOrDefault();

            Usuario us = new Usuario();

            if (usuario != null)
            {
                us.cambio_usuario = (int)usuario.CAMBIO_USUARIO;
                us.cedula_cliente = usuario.CEDULA_CLIENTE;
                us.id_cliente = (int)usuario.ID_CLIENTE;
                us.id_usuario = usuario.ID_USUARIO;
                us.password_usuario = usuario.PASSWORD_USUARIO;
                us.nombre_usuario = usuario.NOMBRE_USUARIO;
            }

            return us;
            
        }

        public Boolean actualizarContrasena(String nombreUsuario, String contrasenia) {

            try
            {
                DB.bancaelectronicaEntities dbBanca = new DB.bancaelectronicaEntities();
                var usuario = (from u in dbBanca.USUARIO
                               where u.NOMBRE_USUARIO == nombreUsuario
                               select u).FirstOrDefault();
                usuario.PASSWORD_USUARIO = Encriptar(contrasenia);
                dbBanca.SaveChanges();
                return true;
            }
            catch (Exception e) {
                return false;
            }

        }
    }
}