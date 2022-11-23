using Plantilla.DAL.Areas.Generales.Interfaces;
using Plantilla.ENT.Areas.Generales.DTO;

namespace Plantilla.BLL.Areas.Generales
{
    /// <summary>
    ///    Esta archivo contiene la logica de negocio general del sistema
    ///    creada: 10/11/2022
    ///    autor: Luis Roberto Bastos C
    /// </summary>
    public class GeneralBL
    {
        private readonly IGeneral _GeneralDAL;

        public GeneralBL(IGeneral GeneralDAL)
        {

            _GeneralDAL = GeneralDAL;

        }

        #region CRUD
        /// <summary>
        ///Obtiene la lista de General
        /// </summary>
        /// <returns>Objeto lista GeneralDTO</returns>
        public async Task<List<GeneralDTO>> Listar()
        {
            return await _GeneralDAL.Listar();
        }
        #endregion
    }
}
