using Plantilla.TEST.Areas.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantilla.TEST.Areas.Home
{
    //En este archivo iran todas las pruebas de area home, esta hereda cualquier elemento de la pruebas GeneralTest
    //para permitir la reutilizacion de codigo
    internal class HomeTest: GeneralTest

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
