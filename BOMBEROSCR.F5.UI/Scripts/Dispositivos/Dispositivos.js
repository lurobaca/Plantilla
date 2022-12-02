
//const IdDataTable = "";

//function FnDataTable(lista) {

//    let dataJSONArray = ConvertirLista(lista);
//    DataTable = $(IdDataTable).DataTable({
//        language: idiomaDT,
//        dom: domDT,
//        fnDrawCallback: buscadorDT,
//        lengthMenu: [[5, 10, 20, 25, 50], [5, 10, 20, 25, 50]],
//        iDisplayLength: 5,
//        responsive: true,
//        pagingType: 'full_numbers',
//        data: dataJSONArray,
//        aaSorting: [],
//        columnDefs: [
//            {
//                targets: -1,
//                title: 'Acciones',
//                orderable: false,
//                render: function (id, tipo, pantalla, meta) {
//                    return Acciones(id, pantalla);
//                },
//            },
//        ],
//        initComplete: function () {
     
//        },
//    });
//}

//function Acciones(id, pantalla) {

//    let Detalle = `<a class="dropdown-item" href="${Url.Formulario + id}" title="Ver detalle">
//                    ${IconoVerDetalle}
//                  </a>`;

//    let Eliminar = `<a class="dropdown-item" onclick="EliminarGenerico(this)" data-id="${id}" data-nombre="${pantalla[0]}" title="Eliminar" style="cursor: pointer;">
//                        ${IconoEliminar}
//                    </a>`;

//    let CambiarEstado = ObtenerBotonCambioEstado(id, pantalla);

//    const opciones = [Detalle, Eliminar, CambiarEstado];

//    return MostrarAcciones(opciones);
//}
