using Plantilla.DAL.Areas.Generales.Interfaces;
using Plantilla.ENT.Areas.Generales.DTO;

namespace Plantilla.DAL.Areas.Generales.Repositorios
{

    /// <summary>
    /// Contiene los metodos que permite el acceso a los datos de la base de datos
    /// Creado:10/11/2022
    /// Autor:Luis Roberto Bastos C
    /// </summary>
    public class GeneralDAL : IGeneral
    {
        private readonly IGeneral _General;

        public GeneralDAL(IGeneral general)
        {
            _General = general;
        }

        /// <summary>
        /// Obtiene la lista de General
        /// </summary>
        /// <returns>Lista de ActividadesPlanTrabajo</returns>
        public async Task<List<GeneralDTO>> Listar()
        {
         
            return List<GeneralDTO>;
        }
    }
}
