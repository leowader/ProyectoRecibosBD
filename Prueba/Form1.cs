using Datos;
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

namespace Prueba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cargarCb();
        }
        Datos.RepositorioEscuela RepositorioEscuela = new Datos.RepositorioEscuela();
        RepositorioEstudiantes RepositorioEstudiantes = new RepositorioEstudiantes();
        RepositorioRecibos RepositorioRecibos= new RepositorioRecibos();
        private void btnconectar_Click(object sender, EventArgs e)
        {
            var estado = RepositorioEscuela.abrirBD();
            MessageBox.Show(estado);
        }
        ServicioEscuela ServicioEscuela = new ServicioEscuela();

        void cargarCb()
        {
            cbPrueba.DataSource = ServicioEscuela.Mostrar();
            cbPrueba.DisplayMember = "NombreEscuela";
        }
        void guardar()
        {
            var escuela = new Escuela();
            escuela = ServicioEscuela.BuscarNombre(cbPrueba.SelectedItem.ToString());
            var nombre = escuela.NombreEscuela;
            MessageBox.Show(nombre);
        }
        void cargar()
        {
            //var veri = RepositorioEstudiantes.todo();
            //MessageBox.Show(veri);
            //if (veri==null)
            //{

            //}
            //grillaescuela.DataSource = RepositorioRecibos.Leer();
            grillaescuela.DataSource = RepositorioEscuela.Leer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void btnestado_Click(object sender, EventArgs e)
        {
            var estado=RepositorioEscuela.Miconexion();
            MessageBox.Show(estado.State.ToString());
        }

        private void btnNum_Click(object sender, EventArgs e)
        {
            txtprueba.Text = new Random().Next(100000,1000000).ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardar();

        }
    }
}
