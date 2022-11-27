﻿using Datos;
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
        }
        Datos.RepositorioEscuela RepositorioEscuela = new Datos.RepositorioEscuela();
        RepositorioEstudiantes RepositorioEstudiantes = new RepositorioEstudiantes();
        ViewRecibos ViewRecibos = new ViewRecibos();
        private void btnconectar_Click(object sender, EventArgs e)
        {
            var estado = RepositorioEscuela.abrirBD();
            MessageBox.Show(estado);
        }
        void cargar()
        {
            //var veri = RepositorioEstudiantes.todo();
            //MessageBox.Show(veri);
            //if (veri==null)
            //{
               
            //}
            grillaescuela.DataSource = ViewRecibos.Leer();
            //grillaescuela.DataSource = RepositorioEscuela.Leer();
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
    }
}
