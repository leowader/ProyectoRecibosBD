using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace PresentacionGUI
{
    public partial class RecibosGenerados : Form
    {
        public RecibosGenerados()
        {
            InitializeComponent();
        }
        ServicioRecibo ServicioRecibo=new ServicioRecibo();
        Logica.ServicioRecibo logicaRecibo = new Logica.ServicioRecibo();
        Logica.ServicioEstudiante estudiantes = new ServicioEstudiante();
        ServicioEscuela serviceEscuela = new ServicioEscuela();
        FiltrosGrilla FiltrosGrilla = new FiltrosGrilla();
        void cargarGrilla()
        {
            if (ServicioRecibo.Mostrar()==null)
            {
                MessageBox.Show("NO HAY RECIBOS  REGISTRADOS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                GrillaRecibosGenerados.Rows.Clear();
                foreach (var item in ServicioRecibo.Mostrar())
                {
                    GrillaRecibosGenerados.Rows.Add(item.CodigoReferencia, item.estudiante.Id, item.escuela.NombreEscuela, item.Concepto, item.Cantidad,
                    item.FechaLimite.ToShortDateString(), item.EstadoPago);
                    //GrillaRecibosGenerados.Rows.Add(item.CodigoReferencia,item.idEstudiante,item.nombre, item.Concepto, item.valor,
                    //item.FechaLimite.ToShortDateString(), item.EstadoPago);
                }
            }
        }
        private void RecibosGenerados_Load(object sender, EventArgs e)
        {
            cargarGrilla();
        }
        private void btnCobrar_Click(object sender, EventArgs e)
        {
            Cobrar();
            cargarGrilla();
        }
        void Cobrar()
        {
            var recibo2 = BuscarRecibo();
            EstadoRecibo estadoRecibo = new EstadoRecibo();
            var estado=estadoRecibo.Estado(recibo2.CodigoReferencia);
            var estudiante = BuscarEstudiante();
            EstadoInscripcion estadoInscripcion = new EstadoInscripcion();
            var estadoI = estadoInscripcion.Estado(estudiante.Id.ToString());
            if (estado !=true &&estadoI!=true)
            {
                recibo2.EstadoPago = "pagado";
                estudiante.estadoInscripcion = "inscrito";
                estudiantes.upd(estudiante);
                ServicioRecibo.Actualizar(recibo2);
                MessageBox.Show("RECIBO COBRADO CON EXITO", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("EL RECIBO YA FUE PAGADO","INFO",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
              
        }
        Entidades.Recibo BuscarRecibo()
        {
            var referencia = GrillaRecibosGenerados.Rows[indice].Cells[0].Value.ToString();
            var recibo=ServicioRecibo.Buscar(referencia);
            return recibo;
        }
        public Estudiante BuscarEstudiante()
        {
            int id;
            id = int.Parse(GrillaRecibosGenerados.Rows[indice].Cells[1].Value.ToString());
            var estudianteEncontado = estudiantes.Buscar(id);
            return estudianteEncontado;
        }
        public Entidades.Escuela BuscarEscuela()
        {
            string NombreEscuela = GrillaRecibosGenerados.Rows[indice].Cells[2].Value.ToString();
            var escuela = serviceEscuela.BuscarNombre(NombreEscuela);
            return escuela;
        }
        
        int indice;
        private void GrillaRecibosGenerados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indice=e.RowIndex;
        }
        void Eliminar()
        {
            ServicioRecibo.Eliminar(BuscarRecibo());
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
            cargarGrilla();
        }

        private void EliminarReciboMenu_Click(object sender, EventArgs e)
        {
            Eliminar();
            cargarGrilla();
        }
        void Generate()
        {
            PresentacionGUI.Recibo recibo = new Recibo();
            ImRecibo imRecibo = new ImRecibo();
            try
            {
                var recibos = BuscarRecibo();
                //recibo.GenerarRecibo(BuscarEstudiante(), BuscarEscuela(),recibos);
                //recibo.Show();
                imRecibo.GenerarReciboVol2(BuscarEstudiante(), BuscarEscuela(), recibos);
                imRecibo.Show();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

        private void verRecibo_Click(object sender, EventArgs e)
        {
            Generate();
        }

        private void totalCobro_Click(object sender, EventArgs e)
        {
            var totalcobro = total();
            MessageBox.Show($"valor total de cobro respecto a los recibos: {totalcobro}");
        }
        double total()
        {
            var total=serviceEscuela.total(BuscarEscuela());
            return total;
        }

        private void txtfiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtfiltro.Text=="")
            {
                cargarGrilla();
            }
            else
            {
                filtro(txtfiltro.Text);
            }
        }
        void filtro(string referencia)
        {
            GrillaRecibosGenerados.Rows.Clear();
            foreach (var item in FiltrosGrilla.FiltroRecibo(referencia))
            {
                GrillaRecibosGenerados.Rows.Add(item.CodigoReferencia, item.estudiante.Id, item.escuela.NombreEscuela, item.Concepto, item.Cantidad,
                    item.FechaLimite.ToShortDateString(), item.EstadoPago);
            }
        }
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            filtro(txtfiltro.Text);
        }
        
        void totalEscuela()
        {
            double total = 0;
            total=ServicioRecibo.totalRecibo(BuscarEscuela());
            MessageBox.Show($"A LA ESCUELA {BuscarEscuela().NombreEscuela.ToUpper()} LE DEBEN PAGAR : ${total}");    
        }
        private void btnTotal_Click(object sender, EventArgs e)
        {
            totalEscuela();
        }
    }
}
