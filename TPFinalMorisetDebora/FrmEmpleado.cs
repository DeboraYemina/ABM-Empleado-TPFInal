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
    public partial class FrmEmpleado : Form
    {
        int id;
        //accion para saber si modifico o agrego
        string accion;
        public FrmEmpleado(string accion)
        {
            InitializeComponent();
            this.accion = accion;
        }
        public FrmEmpleado(Empleado UnEmpleado, string accion)
        {
            InitializeComponent();
            CargarCasado();
            txtDNI.Text = UnEmpleado.DNI;
            txtEdad.Text = UnEmpleado.Edad.ToString();
            txtNombreCompleto.Text = UnEmpleado.NombreCompleto.ToString();
            txtSalario.Text = UnEmpleado.Salario.ToString();
            if (UnEmpleado.Casado == true)
                cmbCasado.SelectedItem = "Si";
            else
                cmbCasado.SelectedItem = "No";
            id = UnEmpleado.Id;
            this.accion = accion;
        }

        //Carga combobox
        private void CargarCasado()
        {
            cmbCasado.Items.Add("Si");
            cmbCasado.Items.Add("No");
        }

        //cargo o modifico el empleado de bd
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Empleado aux = new Empleado();

                ValidarDatosNulos(txtNombreCompleto.Text, txtDNI.Text, txtEdad.Text);

                aux.NombreCompleto = txtNombreCompleto.Text;
                aux.DNI = txtDNI.Text;
                aux.Edad = int.Parse(txtEdad.Text);
                if (cmbCasado.SelectedItem.ToString() == "Si")
                    aux.Casado = true;
                else if (cmbCasado.SelectedItem.ToString() == "No")
                    aux.Casado = false;
                if (txtSalario.Text == "")
                    aux.Salario = 0;
                else
                    aux.Salario = double.Parse(txtSalario.Text);

                if (accion == "Agregar")
                {
                    try
                    {
                        EmpleadoConexion.GuardarEmpleado(aux);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (accion == "Modificar")
                {
                    try
                    {
                        aux.Id = id;
                        EmpleadoConexion.ModificarEmpleado(aux);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }

            catch (FormatException)
            {
                MessageBox.Show("Debe ingresar datos válidos");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Debe completar todos los campos");
            }

            finally
            {
                LimpiarFormulario();
            }
        }

        //Valido los text que no queden nulos
        private void ValidarDatosNulos(string nombre, string dni, string edad)
        {
            if (nombre == "" || dni == "" || edad == "")
                throw new NullReferenceException (); 
        }

        private void LimpiarFormulario()
        {
            txtDNI.Clear();
            txtEdad.Clear();
            txtNombreCompleto.Clear();
            txtSalario.Clear();
            cmbCasado.SelectedItem = null;
        }

        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            if (cmbCasado.Items.Count == 0)
                CargarCasado();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}


