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
    public partial class DatosRecibos : Form
    {
        public DatosRecibos()
        {
            InitializeComponent();
            txtReferencia.Enabled=false;
            txtReferencia.Text= new Random().Next(100000, 1000000).ToString();
        }
        Logica.ServicioRecibo logicaRecibo=new Logica.ServicioRecibo();
        Logica.ServicioEstudiante estudiantes = new ServicioEstudiante();
        
        ServicioEscuela serviceEscuela = new ServicioEscuela();
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Guardar();
            TieneRecibo();
            CargarGrilla();
            GenerarRecibo();

        }
        void Guardar()
        {
            try
            {
                Entidades.Recibo recibo = new Entidades.Recibo();
                recibo.CodigoReferencia = txtReferencia.Text;
                recibo.Concepto = CbConcepto.SelectedItem.ToString();
                recibo.Cantidad = double.Parse(txtValor.Text);
                recibo.Banco = CbBanco.SelectedItem.ToString();
                recibo.FechaLimite = DateTime.Parse(DateLimete.Value.ToString());
                recibo.FechaExtraordinaria = DateTime.Parse(DateExtra.Value.ToString());
                recibo.Observaciones = txtObservacion.Text;
                recibo.EstadoPago = "pendiente";
                recibo.estudiante =estudiantes.Buscar( int.Parse(GrillaSelect.Rows[indice].Cells[0].Value.ToString()));
                var mensage = logicaRecibo.Guardar(recibo);
                MessageBox.Show(mensage, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReferencia.Text = new Random().Next(100000, 1000000).ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        void Limpiar()
        {
            txtObservacion.Clear();
            txtReferencia.Clear();
            txtReferencia.Clear();
            txtValor.Clear();
            CbBanco.SelectedIndex = -1;
            CbConcepto.SelectedIndex = -1;
            DateLimete.Text = DateTime.UtcNow.ToString();
            DateExtra.Text = DateTime.UtcNow.ToString();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void CargarGrilla()
        {
            if (estudiantes.Mostrar() == null)
            {
                MessageBox.Show("NO HAY ESTUDIANTES  REGISTRADOS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                GrillaSelect.Rows.Clear();
                foreach (var item in estudiantes.Mostrar())
                {
                    if (item.TieneRecibo.Equals('N'))
                    {
                        GrillaSelect.Rows.Add(item.Id, item.Escuela.NombreEscuela, item.Grado);
                    }
                }
            }
        }
        private void DatosRecibos_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            
        }
        int indice;
        int columna;
        void GenerarRecibo()
        {
            PresentacionGUI.Recibo recibo = new Recibo();
            try
            {
                recibo.GenerarRecibo(BuscarEstudiante(), BuscarEscuela(), ReciboInfo());
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
        public Estudiante BuscarEstudiante()
        {
            int id;
            id = int.Parse(GrillaSelect.Rows[indice].Cells[0].Value.ToString());
            var estudianteEncontado = estudiantes.Buscar(id);
            return estudianteEncontado;
        }
        public void TieneRecibo()
        {
            var estudiantes1 = BuscarEstudiante();
            estudiantes1.TieneRecibo = 'S';
            estudiantes.upd(estudiantes1);
        }
        public Entidades.Escuela BuscarEscuela()
        {
            string NombreEscuela= GrillaSelect.Rows[indice].Cells[1].Value.ToString();
            var escuela = serviceEscuela.BuscarNombre(NombreEscuela);
            return escuela;
        }
        public Entidades.Recibo ReciboInfo()
        {
            var InfoRecibo = new Entidades.Recibo();
            InfoRecibo.Cantidad = double.Parse(txtValor.Text);
            InfoRecibo.Observaciones=txtObservacion.Text;
            InfoRecibo.Banco = CbBanco.SelectedItem.ToString();
            InfoRecibo.CodigoReferencia = txtReferencia.Text;
            InfoRecibo.Concepto = CbConcepto.SelectedItem.ToString();
            InfoRecibo.FechaLimite = DateTime.Parse(DateLimete.Value.ToString());
            InfoRecibo.FechaExtraordinaria = DateTime.Parse(DateExtra.Value.ToString());
            return InfoRecibo;
        }
        private void GrillaSelect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
            columna = e.ColumnIndex;
            labelPrueba2.Text= GrillaSelect.Rows[indice].Cells[1].Value.ToString();
            labelPrueba.Text=(GrillaSelect.Rows[indice].Cells[0].Value.ToString());
        }
    }
}
