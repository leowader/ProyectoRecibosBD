using Entidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentacionGUI
{
    public partial class ImRecibo : Form
    {
        public ImRecibo()
        {
            InitializeComponent();
        }

        private void ImRecibo_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        public void GenerarReciboVol2(Entidades.Estudiante ReciboEstudiante, Escuela escuela, Entidades.Recibo recibo)
        {
            ReportParameter[] reportParameters = new ReportParameter[15];//+1
            reportParameters[0] = new ReportParameter("Nit",escuela.NiT);
            reportParameters[1] = new ReportParameter("direccion", escuela.Direccion);
            reportParameters[2] = new ReportParameter("telefono", escuela.Telefono);
            reportParameters[3] = new ReportParameter("correo",escuela.Correo);
            reportParameters[4] = new ReportParameter("NameEscuela",escuela.NombreEscuela);
            reportParameters[5] = new ReportParameter("referencia", recibo.CodigoReferencia);
            reportParameters[6] = new ReportParameter("fechalimite", recibo.FechaLimite.ToShortDateString().ToString());
            reportParameters[7] = new ReportParameter("valor", "$"+recibo.Cantidad.ToString());
            reportParameters[8] = new ReportParameter("cc", ReciboEstudiante.Id.ToString());
            reportParameters[9] = new ReportParameter("nombre", ReciboEstudiante.Nombres+" "+ReciboEstudiante.Apellidos);
            reportParameters[10] = new ReportParameter("periodo", ReciboEstudiante.PeriodoEstudio);
            reportParameters[11] = new ReportParameter("concepto", recibo.Concepto);
            reportParameters[12] = new ReportParameter("observacion", recibo.Observaciones);
            reportParameters[13] = new ReportParameter("fechaimpre", DateTime.UtcNow.ToShortDateString().ToString());
            reportParameters[14] = new ReportParameter("banco", recibo.Banco);
            reportViewer1.LocalReport.SetParameters(reportParameters);
        }
    }
}
