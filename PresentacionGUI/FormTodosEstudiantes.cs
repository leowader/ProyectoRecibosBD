﻿using System;
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
    public partial class FormTodosEstudiantes : Form
    {
        ServicioEstudiante servicioEstudiante = new ServicioEstudiante();
        FiltrosGrilla FiltrosGrilla= new FiltrosGrilla();
        public FormTodosEstudiantes()
        {
            InitializeComponent();
            CargarGrilla();
        }

        void CargarGrilla()
        {
            if (servicioEstudiante.Mostrar() == null)
            {
                MessageBox.Show("NO HAY ESTUDIANTES REGISTRADOS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                foreach (var item in servicioEstudiante.Mostrar())
                {
                    GrillaEstudiantes.Rows.Add(item.Id, item.Nombres, item.Apellidos, item.Sexo,
                    item.curso,item.Grado, item.PeriodoEstudio, item.idescuela);
                }
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        

        public void Editar()
        {
            FormEditarEstudiantes formEditarEstudiante = new FormEditarEstudiantes();
            try
            {
                formEditarEstudiante.txtId.Text = GrillaEstudiantes.CurrentRow.Cells[0].Value.ToString();
                formEditarEstudiante.txtNombre.Text = GrillaEstudiantes.CurrentRow.Cells[1].Value.ToString();
                formEditarEstudiante.txtApellidos.Text = GrillaEstudiantes.CurrentRow.Cells[2].Value.ToString();
                string hombre = GrillaEstudiantes.CurrentRow.Cells[3].Value.ToString();
            if (hombre == "M")
            {
                formEditarEstudiante.rdHombre.Checked = true;
            }
            else
            {
                formEditarEstudiante.rdMujer.Checked = true;
            }
            
            //formEditarEstudiante.txtCurso.Text = GrillaEstudiantes.CurrentRow.Cells[4].Value.ToString();
            //formEditarEstudiante.txtGrado.Text = GrillaEstudiantes.CurrentRow.Cells[5].Value.ToString();
            formEditarEstudiante.cbPeriodo.Text = GrillaEstudiantes.CurrentRow.Cells[6].Value.ToString();
            formEditarEstudiante.cbEscuela.Text = GrillaEstudiantes.CurrentRow.Cells[7].Value.ToString();
            formEditarEstudiante.ShowDialog();
            RefrescarGrilla();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message, "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        public void RefrescarGrilla()
        {
            GrillaEstudiantes.Rows.Clear();
            GrillaEstudiantes.Refresh();
            CargarGrilla();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarGeneral();
        }

        int fila;
        public void EliminarGeneral()
        {
            var pregunta = MessageBox.Show("¿Esta seguro de eliminar: " + GrillaEstudiantes.CurrentRow.Cells[1].Value.ToString() + "?", "Eliminar Escuela", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            int Id = int.Parse(GrillaEstudiantes.Rows[fila].Cells[0].Value.ToString());
            var estudiante=servicioEstudiante.Buscar(Id);
            if (pregunta == DialogResult.Yes)
            {
                Eliminar(estudiante);
                RefrescarGrilla();
                fila = 0;
            }
        }
        void Eliminar(Estudiante estudiante)
        {
            var mensaje = servicioEstudiante.Eliminar(estudiante);
            MessageBox.Show(mensaje);
        }
        private void GrillaEstudiantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            fila = e.RowIndex;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "editarToolStripMenuItem1":
                    Editar();
                    break;
                case "borrarToolStripMenuItem":
                    contextMenuStrip1.Hide();
                    EliminarGeneral();
                    break;
            }
        }

        private void txtfiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtfiltro.Text=="")
            {
                CargarGrilla();
            }
            else
            {
                filtrar(txtfiltro.Text);
            }
        }
        void filtrar(string nombre)
        {
            GrillaEstudiantes.Rows.Clear();
            foreach (var item in FiltrosGrilla.Filtrostudent(nombre))
            {
                GrillaEstudiantes.Rows.Add(item.Id, item.Nombres, item.Apellidos, item.Sexo,
                item.curso, item.Grado, item.PeriodoEstudio, item.idescuela);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filtrar(txtfiltro.Text);
        }
    }
}
