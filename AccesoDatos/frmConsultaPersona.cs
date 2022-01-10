using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesoDatos
{
    public partial class frmConsultaPersona : Form
    {
        private string ncedula;

        public frmConsultaPersona(string cedula)
        {
            InitializeComponent();
            this.ncedula = cedula;
            MessageBox.Show("La cédula es: " + ncedula);

            DataTable dt = Clases.Persona.getpersona();
                foreach (DataRow row in dt.Rows)
                {
                    this.txtMostrarCe.Text = row["Cédulas"].ToString();
                    this.txtMostrarNoyAp.Text = row["Nombres Completos"].ToString();
                    this.datotiempos.Value = Convert.ToDateTime(row["Fecha de Nacimiento"].ToString());
                    this.txtMostrarPe.Text = row["Peso"].ToString();
                }

        }

        private void dtRespues_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
