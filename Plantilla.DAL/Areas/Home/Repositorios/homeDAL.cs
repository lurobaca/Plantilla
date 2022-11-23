using Plantilla.DAL.Areas.Generales.Interfaces;
using Plantilla.DAL.Areas.Generales.Repositorios;
using Plantilla.DAL.Areas.Home.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantilla.DAL.Areas.Home.Repositorios
{
    /// <summary>
    /// Contiene los metodos que permite el acceso a los datos de la base de datos
    /// Creado:10/11/2022
    /// Autor:Luis Roberto Bastos C
    /// </summary>
    public class homeDAL:IHome
    {
        private readonly IHome _Home;
        public homeDAL(IHome home) 
        {
            _Home = home;
        }



    }
}
