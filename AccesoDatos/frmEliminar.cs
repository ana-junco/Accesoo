using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesoDatos
{
    public partial class frmEliminar : Form
    {
        public frmEliminar()
        {
            InitializeComponent();
        }
        SqlConnection conexion = new SqlConnection(@"server=DESKTOP-0IJTAIU\SQLEXPRESS; database=TDI2021; Integrated Security=true");

        public void Mostrar (object sender, EventArgs e)
        {
            DataTable dt = getpersona();
            this.comboPersona.DataSource = dt;
            this.comboPersona.DisplayMember = "nombreCompleto";
            this.comboPersona.ValueMember = "cedula";
        }
        private DataTable getpersona(string cedula = "")
        {
            string sql = "";
            if (cedula == "")
            {
                sql = "select cedula, apellidos, nombres, upper(apellidos+ ' ' + nombres) as nombreCompleto, fechaNacimiento, peso ";
                sql += "from personas order by apellidos, nombres";
            }
            else
            {
                sql = "select cedula, apellidos, nombres, upper(apellidos+ ' ' + nombres) as nombreCompleto, fechaNacimiento, peso ";
                sql += "from personas where cedula=@cedula order by apellidos, nombres";
            }

            SqlCommand comando = new SqlCommand(sql, conexion);
            if (cedula != "")
            {
                comando.Parameters.Add(new SqlParameter("@cedula", cedula));
            }
            SqlDataAdapter ad1 = new SqlDataAdapter(comando);

            //pasar los datos del adaptador a un datatable
            DataTable dt = new DataTable();
            ad1.Fill(dt);
            return dt;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿ESTAS SEGURO QUE DESEAS ELIMINAR?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                conexion.Open();
                string eliminar = "DELETE FROM personas WHERE cedula = @cedula";
                SqlCommand cmd3 = new SqlCommand(eliminar, conexion);
                cmd3.Parameters.AddWithValue("@cedula", this.txtCedula.Text);
                cmd3.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("PERSONA ELIMINADA CON EXITO", "ELIMINO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DataTable dt = getpersona();
                this.comboPersona.DataSource = dt;
                this.comboPersona.DisplayMember = "nombreCompleto";
                this.comboPersona.ValueMember = "cedula";
                txtApellido.Clear();
                txtCedula.Clear();
                txtNombres.Clear();
                txtPeso.Clear();
            }
            else
            {
                MessageBox.Show("NO SE ELIMINO NINGUN DATO", "CANCELACION", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnMostrar_Click_1(object sender, EventArgs e)
        {
            //hola
            DataTable dt = getpersona(this.comboPersona.SelectedValue.ToString());
            //mostrar la informacion
            foreach (DataRow row in dt.Rows)
            {
                this.txtCedula.Text = row["cedula"].ToString();
                this.txtNombres.Text = row["nombres"].ToString();
                this.txtApellido.Text = row["apellidos"].ToString();
                this.datoFecha.Value = Convert.ToDateTime(row["fechaNacimiento"].ToString());
                this.txtPeso.Text = row["peso"].ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿ESTAS SEGURO QUE DESEAS ACTUALIZAR?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    conexion.Open();
                    string actualizar = "UPDATE personas SET apellidos=@apellidos, nombres=@nombres, fechaNacimiento=@fechaNacimiento, peso=@peso WHERE cedula=@cedula";
                    SqlCommand cmd2 = new SqlCommand(actualizar, conexion);
                    cmd2.Parameters.AddWithValue("@cedula", this.txtCedula.Text);
                    cmd2.Parameters.AddWithValue("@apellidos", this.txtApellido.Text);
                    cmd2.Parameters.AddWithValue("@nombres", this.txtNombres.Text);
                    cmd2.Parameters.AddWithValue("@fechaNacimiento", datoFecha.Value);
                    cmd2.Parameters.AddWithValue("@peso", this.txtPeso.Text);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Los datos han sido actualizados");
                    conexion.Close();
                    DataTable dt = getpersona();
                    this.comboPersona.DataSource = dt;
                    this.comboPersona.DisplayMember = "nombreCompleto";
                    this.comboPersona.ValueMember = "cedula";
                    txtApellido.Clear();
                    txtCedula.Clear();
                    txtNombres.Clear();
                    txtPeso.Clear();
                }   
                catch(SqlException e1)
                {
                    MessageBox.Show(e1.Message.ToString(), "Cierre el programa e intente reemplazando la coma por el punto en el peso");

                }
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
