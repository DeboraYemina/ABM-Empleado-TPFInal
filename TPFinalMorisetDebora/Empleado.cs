using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinalMorisetDebora
{
    public class Empleado
    {
        //        Id(int)
        //      NombreCompleto(string)
        //DNI (string)
        //      Edad(int)
        //      Casado(boolean, o sea, SI o NO)
        //      Salario(double)

        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string  DNI { get; set; }
        public int Edad { get; set; }
        public bool Casado { get; set; }
        public double Salario { get; set; }

    }
}
