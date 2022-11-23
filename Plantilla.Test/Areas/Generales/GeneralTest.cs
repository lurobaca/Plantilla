using Plantilla.TEST.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantilla.TEST.Areas.Generales
{
    //En esta prueba iran todas aquellas funcionalidades general del sistema como lo pueden ser:
    //alertas mediante correos,Generacion de pdf,Obtencion de tipo de cambio
    internal class GeneralTest: BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
