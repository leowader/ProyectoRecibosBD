using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logica;
using Entidades;

namespace PresentacionGUI
{
    public partial class FormAgregarEscuela : Form
    {
        ServicioEscuela servicioEscuela = new ServicioEscuela();
        public FormAgregarEscuela()
        {
            InitializeComponent();
            txtTelefono.MaxLength = 10;
            txtNit.Enabled = false;
            txtNit.Text = new Random().Next(100000, 1000000).ToString();
        }
        void Guardar()
        {
            if (txtNit.Text == ""||txtNombre.Text == ""||txtDireccion.Text == ""||txtTelefono.Text == ""||txtCorreo.Text == "")
            {
                MessageBox.Show("FALTAN DATOS POR COMPLETAR", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    Escuela escuela = new Escuela();
                    escuela.NiT= txtNit.Text ;
                    escuela.NombreEscuela = txtNombre.Text;
                    escuela.Direccion = txtDireccion.Text;
                    escuela.Telefono = txtTelefono.Text;
                    escuela.Correo = txtCorreo.Text;
                    var mensaje = servicioEscuela.Guardar(escuela);
                    txtNit.Text = new Random().Next(100000, 1000000).ToString();
                    MessageBox.Show(mensaje, "Regristro escuela", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void FormAgregarFamiliar_Load(object sender, EventArgs e)
        {

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
        }
        public void SoloNumeros(KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SoloLetras(e.KeyChar) == false)
            {
                MessageBox.Show("Solo se permiten Letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        public bool SoloLetras(char e)
        {
            if (e >= 65 && e <= 90 || e == 8 || e >= 97 && e <= 122 || e == 32 || e == 165 && e == 164)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void txtNit_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar(this, panelEscuelas);
        }
        private void Limpiar(Control control, Panel panel)
        {
            foreach(var txt in panel.Controls)
            {
                if (txt is TextBox)
                {
                    ((TextBox)txt).Clear();
                }
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}