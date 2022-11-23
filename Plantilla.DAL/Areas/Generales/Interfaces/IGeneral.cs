using Plantilla.ENT.Areas.Generales.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantilla.DAL.Areas.Generales.Interfaces
{
    /// <summary>
    /// Acceso a datos para el archivo General
    /// Autor : Luis Roberto Bastos C
    /// Fecha : 10/11/2022
    /// </summary>
    public interface IGeneral
    {
        #region CRUD

        Task<List<GeneralDTO>> Listar();

        //Task Crear(Actividades registroGuardado);

        //Task Editar(Actividades registroEditado);

        ///// Funcion creada para editar listas secundarias.
        //Task EditarSecundarias(List<Actividades> listaEditada, int estado, string usuario, int id);
        //Task Eliminar(Actividades registroEliminado);

        #endregion
    }
}
