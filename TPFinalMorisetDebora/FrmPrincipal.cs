using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPFinalMorisetDebora
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            CargarDgvEmpleados();
        }

        //Metodo para cargar los empleados en el datagridview
        private void CargarDgvEmpleados()
        {
            try
            {
                dgvListaEmpleados.DataSource = EmpleadoConexion.CargarLista();
                dgvListaEmpleados.Columns["ID"].Visible = false;

            }
            catch (Exception)
            {

                MessageBox.Show("Error de conexión a base de datos");
            }
        }

        //Agregar empleados al dgv
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmEmpleado frmNuevo = new FrmEmpleado("Agregar");
            frmNuevo.ShowDialog();
            CargarDgvEmpleados();
        }

        //Modificar empleados del dgv
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Empleado aux = new Empleado ();
                aux = (Empleado)dgvListaEmpleados.CurrentRow.DataBoundItem;
                FrmEmpleado frmNuevo = new FrmEmpleado(aux,"Modificar");
                frmNuevo.ShowDialog();
                CargarDgvEmpleados();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Primero debe seleccionar un empleado");
            }
        }

        //Eliminar empleado del dgv
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Desea eliminar el empleado?", "Eliminar", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    EmpleadoConexion.EliminarEmpleado((Empleado)dgvListaEmpleados.CurrentRow.DataBoundItem);
                    MessageBox.Show("Empleado eliminado");
                    CargarDgvEmpleados();

                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Debe seleccionar un empleado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        //Busco en db Empleados por nombre
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            LimpiarDgvEmpleados();
            CargarDgvEmpleados();
                
            string a = txtFiltro.Text;
            List<Empleado> lista = new List<Empleado>();
            
            foreach (DataGridViewRow fila in dgvListaEmpleados.Rows)
            {
                if (Convert.ToString(fila.Cells["NombreCompleto"].Value) == a)
                {
                    lista.Add((Empleado)fila.DataBoundItem);
                }
            }
            LimpiarDgvEmpleados();
            dgvListaEmpleados.DataSource= lista;
            dgvListaEmpleados.Columns["ID"].Visible = false;
        }

        private void LimpiarDgvEmpleados()
        {
            dgvListaEmpleados.DataSource=null;
        }

        //Metodo para eliminar los empleados filtados anteriormente. Vuelve a cargar la dgv con bd empleados
        private void TextBoxVacio(object sender, EventArgs e)
        {
            if (txtFiltro.Text == "")
                CargarDgvEmpleados();
        }
    }
}
