/**
 * AdminLTE Demo Menu
 * ------------------
 * You should not use this file in production.
 * This file is for demo purposes only.
 */
(function ($, AdminLTE) {

    "use strict";

    /**
     * List of all the available skins
     *
     * @type Array
     */
    var my_skins = [
      "skin-blue",
      "skin-black",
      "skin-red",
      "skin-yellow",
      "skin-purple",
      "skin-green",
      "skin-blue-light",
      "skin-black-light",
      "skin-red-light",
      "skin-yellow-light",
      "skin-purple-light",
      "skin-green-light"
    ];

    //Create the new tab
    var tab_pane = $("<div />", {
        "id": "control-sidebar-theme-demo-options-tab",
        "class": "tab-pane active"
    });

    //Create the tab button
    var tab_button = $("<li />", { "class": "active" })
            .html("<a href='#control-sidebar-theme-demo-options-tab' data-toggle='tab'>"
                    + "<i class='fa fa-wrench'></i>"
                    + "</a>");

    //Add the tab button to the right sidebar tabs
    $("[href='#control-sidebar-home-tab']")
            .parent()
            .before(tab_button);

    //Create the menu
    var demo_settings = $("<div />");

    //Layout options
    demo_settings.append(
            "<h4 class='control-sidebar-heading'>"
            + "Layout Options"
            + "</h4>"
            //Fixed layout
            + "<div class='form-group'>"
            + "<label class='control-sidebar-subheading'>"
            + "<input type='checkbox' data-layout='fixed' class='pull-right'/> "
            + "Fixed layout"
            + "</label>"
            + "<p>Activate the fixed layout. You can't use fixed and boxed layouts together</p>"
            + "</div>"
            //Boxed layout
            + "<div class='form-group'>"
            + "<label class='control-sidebar-subheading'>"
            + "<input type='checkbox' data-layout='layout-boxed'class='pull-right'/> "
            + "Boxed Layout"
            + "</label>"
            + "<p>Activate the boxed layout</p>"
            + "</div>"
            //Sidebar Toggle
            + "<div class='form-group'>"
            + "<label class='control-sidebar-subheading'>"
            + "<input type='checkbox' data-layout='sidebar-collapse' class='pull-right'/> "
            + "Toggle Sidebar"
            + "</label>"
            + "<p>Toggle the left sidebar's state (open or collapse)</p>"
            + "</div>"
            //Sidebar mini expand on hover toggle
            + "<div class='form-group'>"
            + "<label class='control-sidebar-subheading'>"
            + "<input type='checkbox' data-enable='expandOnHover' class='pull-right'/> "
            + "Sidebar Expand on Hover"
            + "</label>"
            + "<p>Let the sidebar mini expand on hover</p>"
            + "</div>"
            //Control Sidebar Toggle
            + "<div class='form-group'>"
            + "<label class='control-sidebar-subheading'>"
            + "<input type='checkbox' data-controlsidebar='control-sidebar-open' class='pull-right'/> "
            + "Toggle Right Sidebar Slide"
            + "</label>"
            + "<p>Toggle between slide over content and push content effects</p>"
            + "</div>"
            //Control Sidebar Skin Toggle
            + "<div class='form-group'>"
            + "<label class='control-sidebar-subheading'>"
            + "<input type='checkbox' data-sidebarskin='toggle' class='pull-right'/> "
            + "Toggle Right Sidebar Skin"
            + "</label>"
            + "<p>Toggle between dark and light skins for the right sidebar</p>"
            + "</div>"
            );
    var skins_list = $("<ul />", { "class": 'list-unstyled clearfix' });

    //Dark sidebar skins
    var skin_blue =
            $("<li />", { style: "float:left; width: 33.33333%; padding: 5px;" })
            .append("<a href='javascript:void(0);' data-skin='skin-blue' style='display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)' class='clearfix full-opacity-hover'>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 7px; background: #367fa9;'></span><span class='bg-light-blue' style='display:block; width: 80%; float: left; height: 7px;'></span></div>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 20px; background: #222d32;'></span><span style='display:block; width: 80%; float: left; height: 20px; background: #f4f5f7;'></span></div>"
                    + "</a>"
                    + "<p class='text-center no-margin'>Blue</p>");
    skins_list.append(skin_blue);
    var skin_black =
            $("<li />", { style: "float:left; width: 33.33333%; padding: 5px;" })
            .append("<a href='javascript:void(0);' data-skin='skin-black' style='display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)' class='clearfix full-opacity-hover'>"
                    + "<div style='box-shadow: 0 0 2px rgba(0,0,0,0.1)' class='clearfix'><span style='display:block; width: 20%; float: left; height: 7px; background: #fefefe;'></span><span style='display:block; width: 80%; float: left; height: 7px; background: #fefefe;'></span></div>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 20px; background: #222;'></span><span style='display:block; width: 80%; float: left; height: 20px; background: #f4f5f7;'></span></div>"
                    + "</a>"
                    + "<p class='text-center no-margin'>Black</p>");
    skins_list.append(skin_black);
    var skin_purple =
            $("<li />", { style: "float:left; width: 33.33333%; padding: 5px;" })
            .append("<a href='javascript:void(0);' data-skin='skin-purple' style='display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)' class='clearfix full-opacity-hover'>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 7px;' class='bg-purple-active'></span><span class='bg-purple' style='display:block; width: 80%; float: left; height: 7px;'></span></div>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 20px; background: #222d32;'></span><span style='display:block; width: 80%; float: left; height: 20px; background: #f4f5f7;'></span></div>"
                    + "</a>"
                    + "<p class='text-center no-margin'>Purple</p>");
    skins_list.append(skin_purple);
    var skin_green =
            $("<li />", { style: "float:left; width: 33.33333%; padding: 5px;" })
            .append("<a href='javascript:void(0);' data-skin='skin-green' style='display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)' class='clearfix full-opacity-hover'>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 7px;' class='bg-green-active'></span><span class='bg-green' style='display:block; width: 80%; float: left; height: 7px;'></span></div>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 20px; background: #222d32;'></span><span style='display:block; width: 80%; float: left; height: 20px; background: #f4f5f7;'></span></div>"
                    + "</a>"
                    + "<p class='text-center no-margin'>Green</p>");
    skins_list.append(skin_green);
    var skin_red =
            $("<li />", { style: "float:left; width: 33.33333%; padding: 5px;" })
            .append("<a href='javascript:void(0);' data-skin='skin-red' style='display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)' class='clearfix full-opacity-hover'>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 7px;' class='bg-red-active'></span><span class='bg-red' style='display:block; width: 80%; float: left; height: 7px;'></span></div>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 20px; background: #222d32;'></span><span style='display:block; width: 80%; float: left; height: 20px; background: #f4f5f7;'></span></div>"
                    + "</a>"
                    + "<p class='text-center no-margin'>Red</p>");
    skins_list.append(skin_red);
    var skin_yellow =
            $("<li />", { style: "float:left; width: 33.33333%; padding: 5px;" })
            .append("<a href='javascript:void(0);' data-skin='skin-yellow' style='display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)' class='clearfix full-opacity-hover'>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 7px;' class='bg-yellow-active'></span><span class='bg-yellow' style='display:block; width: 80%; float: left; height: 7px;'></span></div>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 20px; background: #222d32;'></span><span style='display:block; width: 80%; float: left; height: 20px; background: #f4f5f7;'></span></div>"
                    + "</a>"
                    + "<p class='text-center no-margin'>Yellow</p>");
    skins_list.append(skin_yellow);

    //Light sidebar skins
    var skin_blue_light =
            $("<li />", { style: "float:left; width: 33.33333%; padding: 5px;" })
            .append("<a href='javascript:void(0);' data-skin='skin-blue-light' style='display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)' class='clearfix full-opacity-hover'>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 7px; background: #367fa9;'></span><span class='bg-light-blue' style='display:block; width: 80%; float: left; height: 7px;'></span></div>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 20px; background: #f9fafc;'></span><span style='display:block; width: 80%; float: left; height: 20px; background: #f4f5f7;'></span></div>"
                    + "</a>"
                    + "<p class='text-center no-margin' style='font-size: 12px'>Blue Light</p>");
    skins_list.append(skin_blue_light);
    var skin_black_light =
            $("<li />", { style: "float:left; width: 33.33333%; padding: 5px;" })
            .append("<a href='javascript:void(0);' data-skin='skin-black-light' style='display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)' class='clearfix full-opacity-hover'>"
                    + "<div style='box-shadow: 0 0 2px rgba(0,0,0,0.1)' class='clearfix'><span style='display:block; width: 20%; float: left; height: 7px; background: #fefefe;'></span><span style='display:block; width: 80%; float: left; height: 7px; background: #fefefe;'></span></div>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 20px; background: #f9fafc;'></span><span style='display:block; width: 80%; float: left; height: 20px; background: #f4f5f7;'></span></div>"
                    + "</a>"
                    + "<p class='text-center no-margin' style='font-size: 12px'>Black Light</p>");
    skins_list.append(skin_black_light);
    var skin_purple_light =
            $("<li />", { style: "float:left; width: 33.33333%; padding: 5px;" })
            .append("<a href='javascript:void(0);' data-skin='skin-purple-light' style='display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)' class='clearfix full-opacity-hover'>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 7px;' class='bg-purple-active'></span><span class='bg-purple' style='display:block; width: 80%; float: left; height: 7px;'></span></div>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 20px; background: #f9fafc;'></span><span style='display:block; width: 80%; float: left; height: 20px; background: #f4f5f7;'></span></div>"
                    + "</a>"
                    + "<p class='text-center no-margin' style='font-size: 12px'>Purple Light</p>");
    skins_list.append(skin_purple_light);
    var skin_green_light =
            $("<li />", { style: "float:left; width: 33.33333%; padding: 5px;" })
            .append("<a href='javascript:void(0);' data-skin='skin-green-light' style='display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)' class='clearfix full-opacity-hover'>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 7px;' class='bg-green-active'></span><span class='bg-green' style='display:block; width: 80%; float: left; height: 7px;'></span></div>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 20px; background: #f9fafc;'></span><span style='display:block; width: 80%; float: left; height: 20px; background: #f4f5f7;'></span></div>"
                    + "</a>"
                    + "<p class='text-center no-margin' style='font-size: 12px'>Green Light</p>");
    skins_list.append(skin_green_light);
    var skin_red_light =
            $("<li />", { style: "float:left; width: 33.33333%; padding: 5px;" })
            .append("<a href='javascript:void(0);' data-skin='skin-red-light' style='display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)' class='clearfix full-opacity-hover'>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 7px;' class='bg-red-active'></span><span class='bg-red' style='display:block; width: 80%; float: left; height: 7px;'></span></div>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 20px; background: #f9fafc;'></span><span style='display:block; width: 80%; float: left; height: 20px; background: #f4f5f7;'></span></div>"
                    + "</a>"
                    + "<p class='text-center no-margin' style='font-size: 12px'>Red Light</p>");
    skins_list.append(skin_red_light);
    var skin_yellow_light =
            $("<li />", { style: "float:left; width: 33.33333%; padding: 5px;" })
            .append("<a href='javascript:void(0);' data-skin='skin-yellow-light' style='display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)' class='clearfix full-opacity-hover'>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 7px;' class='bg-yellow-active'></span><span class='bg-yellow' style='display:block; width: 80%; float: left; height: 7px;'></span></div>"
                    + "<div><span style='display:block; width: 20%; float: left; height: 20px; background: #f9fafc;'></span><span style='display:block; width: 80%; float: left; height: 20px; background: #f4f5f7;'></span></div>"
                    + "</a>"
                    + "<p class='text-center no-margin' style='font-size: 12px;'>Yellow Light</p>");
    skins_list.append(skin_yellow_light);

    demo_settings.append("<h4 class='control-sidebar-heading'>Skins</h4>");
    demo_settings.append(skins_list);

    tab_pane.append(demo_settings);
    $("#control-sidebar-home-tab").after(tab_pane);

    setup();

    /**
     * Toggles layout classes
     *
     * @param String cls the layout class to toggle
     * @returns void
     */
    function change_layout(cls) {
        $("body").toggleClass(cls);
        AdminLTE.layout.fixSidebar();
        //Fix the problem with right sidebar and layout boxed
        if (cls == "layout-boxed")
            AdminLTE.controlSidebar._fix($(".control-sidebar-bg"));
        if ($('body').hasClass('fixed') && cls == 'fixed') {
            AdminLTE.pushMenu.expandOnHover();
            AdminLTE.layout.activate();
        }
        AdminLTE.controlSidebar._fix($(".control-sidebar-bg"));
        AdminLTE.controlSidebar._fix($(".control-sidebar"));
    }

    /**
     * Replaces the old skin with the new skin
     * @param String cls the new skin class
     * @returns Boolean false to prevent link's default action
     */
    function change_skin(cls) {
        $.each(my_skins, function (i) {
            $("body").removeClass(my_skins[i]);
        });

        $("body").addClass(cls);
        store('skin', cls);
        return false;
    }

    /**
     * Store a new settings in the browser
     *
     * @param String name Name of the setting
     * @param String val Value of the setting
     * @returns void
     */
    function store(name, val) {
        if (typeof (Storage) !== "undefined") {
            localStorage.setItem(name, val);
        } else {
            window.alert('Please use a modern browser to properly view this template!');
        }
    }

    /**
     * Get a prestored setting
     *
     * @param String name Name of of the setting
     * @returns String The value of the setting | null
     */
    function get(name) {
        if (typeof (Storage) !== "undefined") {
            return localStorage.getItem(name);
        } else {
            window.alert('Please use a modern browser to properly view this template!');
        }
    }

    /**
     * Retrieve default settings and apply them to the template
     *
     * @returns void
     */
    function setup() {
        var tmp = get('skin');
        if (tmp && $.inArray(tmp, my_skins))
            change_skin(tmp);

        //Add the change skin listener
        $("[data-skin]").on('click', function (e) {
            e.preventDefault();
            change_skin($(this).data('skin'));
        });

        //Add the layout manager
        $("[data-layout]").on('click', function () {
            change_layout($(this).data('layout'));
        });

        $("[data-controlsidebar]").on('click', function () {
            change_layout($(this).data('controlsidebar'));
            var slide = !AdminLTE.options.controlSidebarOptions.slide;
            AdminLTE.options.controlSidebarOptions.slide = slide;
            if (!slide)
                $('.control-sidebar').removeClass('control-sidebar-open');
        });

        $("[data-sidebarskin='toggle']").on('click', function () {
            var sidebar = $(".control-sidebar");
            if (sidebar.hasClass("control-sidebar-dark")) {
                sidebar.removeClass("control-sidebar-dark")
                sidebar.addClass("control-sidebar-light")
            } else {
                sidebar.removeClass("control-sidebar-light")
                sidebar.addClass("control-sidebar-dark")
            }
        });

        $("[data-enable='expandOnHover']").on('click', function () {
            $(this).attr('disabled', true);
            AdminLTE.pushMenu.expandOnHover();
            if (!$('body').hasClass('sidebar-collapse'))
                $("[data-layout='sidebar-collapse']").click();
        });

        // Reset options
        if ($('body').hasClass('fixed')) {
            $("[data-layout='fixed']").attr('checked', 'checked');
        }
        if ($('body').hasClass('layout-boxed')) {
            $("[data-layout='layout-boxed']").attr('checked', 'checked');
        }
        if ($('body').hasClass('sidebar-collapse')) {
            $("[data-layout='sidebar-collapse']").attr('checked', 'checked');
        }

    }
})(jQuery, $.AdminLTE);

//Personal scripts
$(function () {
    var d = new Date();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    var year = d.getFullYear();

    $('.datepicker').datetimepicker({
        viewMode: 'days',
        format: 'dd-MM-yyyy'
    }); //Initialise any date pickers

    $('.datepickerinput').val(day + '-' + month + '-' + year);



    $('.datepickershort').datetimepicker({
        viewMode: 'months',
        format: 'MM-yyyy'
    }); //Initialise any date pickers

    $('.datepickershortday').datetimepicker({
        viewMode: 'days',
        format: 'dd-MM',
        localDate: new Date
    }); //Initialise any date pickers




});


//Control de campos de tipo date-time
$(document).ready(function () {
    function startTime() {
        var today = new Date();
        var h = today.getHours();
        var m = today.getMinutes();
        var s = today.getSeconds();
        var Y = today.getFullYear();
        var MM = today.getMonth();
        var DD = today.getDay();
        m = checkTime(m);
        s = checkTime(s);
        $('#CurDate').val(Y + "-" + MM + "-" + DD + " " + h + ":" + m + ":" + s);
        var t = setTimeout(startTime, 1000);
    }
    function checkTime(i) {
        if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
        return i;
    }

    startTime();

    function removeTar(id) {
        var tarelim = "#tarjeta_" + id;
        $(tarelim).remove();
        return false;
    }

})


//Creació dinámica de vistas parciales Afiliados
$(document).ready(function () {
    //Aca pinto Afiliaciones Vencidas : Si hay.
   //s $('.paintRedL').css("background", "red");
    $('#dwReport').click();// Se auto descarga el Report al crear Afiliado
    // Se esconde el Boton de Objeciones hoteles y Dummis ------------------------------------------------------------------
    $('.open-bar').addClass("hide")
    //Agregar nueva tarjeta de crédito
    $('#newTarjetaBtn').on('click', function () {
        var cantidad = $(".tar-con").size();
        $.ajax({
            url: '/Afiliados/CreateNewTarjeta',
            type: 'post',
            data: { Tarjetas: cantidad },
            success: function (result) {
                $("#tarjetas-container").append(result);
            }
        });
        return false;
    });

    //LLama a funcion sessionOut() para cerrar la Session ---------------------------
    $('#sessionOut').on("click", function () {
        sessionOut();
    });

    //Quitar tarjeta de la lista
    $('.hide-tar').click(function (e) {
        e.preventDefault();
        var id = this.id;
        $('#Tarjetas_' + id + '__NumeroTarjeta').val("");
        $('#tarjeta_' + id).hide();
        $('#Tarjetas_' + id + '__NumeroTarjeta').removeAttr('required');
        return false;
    })


    //Agregar nuevo Teléfono
    $('#newTelefonoBtn').on('click', function () {

        var cantidad = $(".tel-con").size();

        $.ajax({
            url: '/Afiliados/CreateNewTelefono',
            type: 'post',
            data: { Telefonos: cantidad },
            success: function (result) {
                $("#telefonos-container").append(result);
            }
        });
        return false;
    });


    //Agregar nueva Direccion
    $('#newDireccionBtn').on('click', function () {

        var cantidad = $(".dir-con").size();

        $.ajax({
            url: '/Afiliados/CreateNewDireccion',
            type: 'post',
            data: { Direcciones: cantidad },
            success: function (result) {
                $("#direcciones-container").append(result);
            }
        });
        return false;
    });


    //Agregar nuevo Email
    $('#newEmailsBtn').on('click', function () {

        var cantidad = $(".email-con").size();
       // debugger;
        $.ajax({
            url: '/Afiliados/CreateNewEmail',
            type: 'post',
            data: { Emails: cantidad },
            success: function (result) {
                $("#emails-container").append(result);
            }
        });
        return false;
    });

    //Permitir Agregar Meses : Afiliados/Create -- (GetPermision)---------------
    $('#AcceptMonth').on('click', function () {
        var pass = $("#autoriza_pass").val();
        var user = $("#autoriza_user").val();
        var tipo = "Meses";
        $.ajax({
            url: '/Afiliados/GetSpecialsPermissions',
            type: 'post',
            data: { User: user, Password: pass, Tipo: tipo },
            success: function (result) {
                //console.log(result);
                if (result == true) {
                    var newValMeses = $("#autoriza_cant").val();
                    $("#Afiliado_MesAdicionalCan").val(newValMeses);
                    $("#Afiliado_MesAdicionalCan[type='number']").prop('max', newValMeses); //Optimización.
                    $("#myModal").modal("hide");
                }
                else {
                    alert('Estimado usuario a ocurrido un error, por favor intente de nuevo. Si el error persiste quizas se deba a que usted no tiene suficientes permisos.');
                    $("#myModal").modal("hide");
                }
            }
        });
        return false;
    })
    //Permitir Agregar Tarjetas Adicionales : Afiliados/Create -- (GetPermision)---------------
    $('#TarAddModalOkBtn').on('click', function () {
        var pass = $("#autoriza_passTar").val();
        var user = $("#autoriza_userTar").val();
        var tipo = "TARJETAS";
        var nombreReq = $("#tarjetasadicionales_0__nombre").val();
        var cedulaReq = $("#tarjetasadicionales_0__cedula").val();
        var emailReq = $("#tarjetasadicionales_0__email").val();
        var telf1Req = $("#tarjetasadicionales_0__telefono1").val();
        if (nombreReq == "" || cedulaReq == "" || emailReq == "" || telf1Req == "") {
            alert("Por favor complete los campos.");
        }else{
            $.ajax({
                url: '/Afiliados/GetSpecialsPermissions',
                type: 'post',
                data: { User: user, Password: pass, Tipo: tipo },
                success: function (result) {
                    console.log(result);
                    if (result == true) {
                        //-------------------------------------------------------
                        var t = $('.tarjetasadicionalescontainer').size();
                        //alert(t);
                        if (t > 0) {
                            modifyTarAddTable(t);
                            $("#Afiliado_TarjetaAdicionalCan[type='number']").prop('max', t);
                            //alert(t);
                            $("#TarjetaAdicionalModal").modal("hide");
                        }
                        else {
                            alert('No hay tarjetas adicionales que guardar');
                            $("#TarjetaAdicionalModal").modal("hide");
                        }
                    }
                    else {
                        alert('Estimado usuario a ocurrido un error, por favor intente de nuevo. Si el error persiste quizas se deba a que usted no tiene suficientes permisos.');
                        $("#TarjetaAdicionalModal").modal("hide");
                    }

                    $("#autoriza_passTar").val("");
                    $("#autoriza_userTar").val("");
                }
            });
        }
        return false;
    })
})

$(document).ready(function () {
    $('.id-mask').mask("0000 - 0000", { placeholder: "1234 - 5678" }); //Mascara para id
    $('.cedula-mask').mask("000-0000000-0", { placeholder: "___ - _______ - _" }); //Mascara para cédula
    $('.cedula-maskA').mask("000-0000000-0", { placeholder: "Cedula" }); //Mascara para cédula de Afiliados - tarjetas adicionales
    $('.telefono-mask').mask("000-000-0000", { placeholder: "___ - ___ - ____" }); //Mascara para telèfono 
    $('.telefono-maskA1').mask("000-000-0000", { placeholder: "Telefono 1" }); //Mascara para telèfono 1 de Afiliados - tarjetas adicionales
    $('.telefono-maskA2').mask("000-000-0000", { placeholder: "Telefono 2" }); //Mascara para telèfono 2 de Afiliados - tarjetas adicionales
    $('.tarjeta-mask').mask("0000-0000-0000-0000", { placeholder: "____ - ____ - ____ - ____" }); //Mascara para tarjeta
    $('.rnc-mask').mask("000-000-000", { placeholder: "___ - ___ - ___" }); //Mascara para rnc
    $('.rnc-emp-mask').mask("000-00000-0", { placeholder: "___ - ___ - ___" }); //Mascara para rnc
    $('.pasaporte-mask').mask("SS-0000000", {
        'translation': {
            S: { pattern: /[A-Z]/ },
        },
        placeholder: "AA - 0000000"
    }); //Mascara para pasaporte
})


$(document).ready(function () {
    $('.datatableUI').DataTable({
        columnDefs: [{
            targets: [0],
            orderData: [0, 1],
        }, {
            targets: [1],
            orderData: [1, 0]
        }, {
            targets: [4],
            orderData: [4, 0]
        }],

        responsive: true
    });

    $('.datatableUI2').DataTable({
        responsive: true
    });

    var table = $('.datatableSA').DataTable({
        responsive: true,
        "bLengthChange": false,
        "language": {
            "search": "Buscar Afiliado:"
        },
        "pageLength": 4,
        "pagingType": "simple"
    });


    $('.datatableSA tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            AsignarAfiliado();
        }
    });

    function AsignarAfiliado() {
       
        $('#Afi-ID').val($.trim($('.datatableSA tbody .selected td:eq(4)').text()));
        $('#Afi-IDa').val($.trim($('.datatableSA tbody .selected td:eq(0)').text()));
        $('#Afi-Name').val($.trim($('.datatableSA tbody .selected td:eq(1)').text()));
        $('#Afi-Ced').val($.trim($('.datatableSA tbody .selected td:eq(2)').text()));
        $('#Afi-Mem').val($.trim($('.datatableSA tbody .selected td:eq(3)').text()));        
        var Id = $.trim($('.datatableSA tbody .selected td:eq(4)').text())
        //alert(Id);
        CargarTarjetas(Id);
    }

    function CargarTarjetas(id) {
        $('#TarjetaUsuario_TarjetaUsuarioID').empty();
        $.ajax({
            url: '/Pagos/GetTarjetaUsuario',
            type: 'post',
            data: { id: id },
            success: function (result) {
                $.each(result, function (i, index) {                    
                    $('#TarjetaUsuario_TarjetaUsuarioID').append('<option value = "' + index.TarjetaUsuarioID + '">' + index.Banco + " " + index.Tipo + " " + index.NumeroTarjeta + '</option>')
                })

            }
        });
        return false;
    }

});


$(document).ready(function () {
    $("input").keypress(function (event) {
        //event.preventDefault();
        if (event.which == 13) {
            event.preventDefault();
            //$("form").submit();
            //$(this).next().focus();
            var $input = $('form input');
            if ($(this).is($input.last())) {
                //Time to submit the form!!!!
                $("form").submit();
            }
            else {
                $input.eq($input.index(this) + 1).focus();
            }
        }
    });
    $('.txtmesadd').prop('disabled', true);
    $('.txttaradd').prop('disabled', true);
    $('.top-special-span').hide();

    $('#Afiliado_MesesAdicionales').change(function () {
        if ($(this).is(':checked')) {
            $(".txtmesadd").prop("disabled", false);
            $('.mes-permission-btn').show();
        }
        else {
            $(".txtmesadd").prop("disabled", true);
            $('.mes-permission-btn').hide();
            $(".txtmesadd").val("0");
        }

    });

    $('#Afiliado_TarjetaAdicional').change(function () {
        if ($(this).is(':checked')) {
            $(".txttaradd").prop("disabled", false);
            $('.tar-permission-btn').show();
        }

        else {
            $(".txttaradd").prop("disabled", true);
            $('.tar-permission-btn').hide();
            $(".txttaradd").val("0");
        }

    });


    //Limpiar modal de tarjeta adicional si se cancela
    $('#TarAddModalCloseBtn').click(function () {
        $('#TarjetasAdicionalesContainer').empty();
        $('#TarjetaAdicionalModal').modal('hide');
    })



   

})


//Funcion para habilitar/deshabilitar extension del telefono

function manageExt(id) {
    var toDis = '.tel-' + id + '-ext';

    if ($('.tel-' + id).val() == "OFICINA") {
        $(toDis).removeAttr('disabled');
    } else {
        $(toDis).prop('disabled', true);
    }
}

function CambiarFechaDeMembresia(i) {//-------------------------------------------------------------------
    
   var id = $('#slMem option:selected').attr('data-costo');
    //var cos1 = $('#slMem').dataset;
    //alert(id);
    $("#Afiliado_CostoMembresia").attr("value", id);
    $("#Afiliado_CostoMembresia").attr("text", id);
    $("#Afiliado_CostoMembresia").val(id);
    $.ajax({
        url: '/Afiliados/GetFechaVencimiento',
        type: 'post',
        data: { id: i},
        success: function (result) {
            $("#Afiliado_FechaVencimiento").val(result);
            
            //alert($("#Afiliado_FechaVencimiento").val());
        }
    });
    return false;
    
        
}

//Abrir modal de tarjetas adicionales
function ShowTarjetasAdicionalesModal(q) {
    modifyContentTarAddModal(q);
}

//Abrir modal de tarjetas adicionales sin parámetros
function openmodalempty() {
    $('#TarjetaAdicionalModal').modal('show');
    return false;
}


//Llamar ajax para modificar tarjeta adicional
function modifyContentTarAddModal(q) {
    $.ajax({
        url: '/Afiliados/CreateNewTarjetaAdicional',
        type: 'post',
        data: { Tarjetas: q },
        success: function (result) {
            $("#TarjetasAdicionalesContainer").html(result);
            //$("#TarjetasAdicionalesHidden").html(result);
            $('#TarjetaAdicionalModal').modal('show');
        }
    });
    return false;
}



//Precargar datos en tabla de tarjtas adicionales
function modifyTarAddTable(t) {
    $('#TarAddTable > tbody > tr').empty();
    for (var i = 0; i < t ; i++) {

        var name = $('#tarjetasadicionales_' + i + '__nombre').val();
        var ced = $('#tarjetasadicionales_' + i + '__cedula').val();
        var email = $('#tarjetasadicionales_' + i + '__email').val();
        var tel1 = $('#tarjetasadicionales_' + i + '__telefono1').val();
        var tel2 = $('#tarjetasadicionales_' + i + '__telefono2').val();
        var dir = $('#tarjetasadicionales_' + i + '__direccion').val();
        var car = $('#tarjetasadicionales_' + i + '__cargo').val();
       
        $('#TarAddTable > tbody').append('<tr><td>' + name + '</td><td>' + ced + '</td><td>' + tel1 + '</td><td>' + tel2 + '</td><td>' + email + '</td><td>' + dir + '</td>' + '</td><td>' + car + '</td>');
    }

}


//Manejo de permisos de usuario del lado del front 
$(document).ready(function () {

    $('.chkPermisos[data-checked="true"]').prop('checked', true);
    //Detectar el check para cambiar input master 
    $('.chkPermisos').change(function () {
        var group = $(this).data('group');
        var module = $(this).data('module');
        ChangeValueInput(group, module);
    })

    function ChangeValueInput(f, m) {
        module = $('#' + m);
        var C = ($('#' + m + '-chkC').is(':checked') ? '1' : '0');
        var R = ($('#' + m + '-chkR').is(':checked') ? '1' : '0');
        var U = ($('#' + m + '-chkU').is(':checked') ? '1' : '0');
        var D = ($('#' + m + '-chkD').is(':checked') ? '1' : '0');
        var RE = ($('#' + m + '-chkRE').is(':checked') ? '1' : '0');
        var CI = ($('#' + m + '-chkCI').is(':checked') ? '1' : '0');
        module.val(C + R + U + D + RE + CI);
    }



})




function OnlySave() {

    Prospecto = {
        Id: $("#ProspectoID").val(),
        Nombres: $("#Nombres").val(),
        Apellido1: $("#Apellido1").val(),
        Apellido2: $("#Apellido2").val(),
        Cedula: $("#Cedula").val(),
        Respuesta: $("#Respuesta").val(),
        Fax: $("#Fax").val(),
        FechaDeNacimiento: $("#FechaDeNacimiento").val(),
        Email: $("#Email").val(),
        Comentario: $("#Comentario").val(),
        Tipo1: "CASA",
        Descripcion1: $("#Telefonos_0__Descripcion").val(),
        Extension1: $("#Telefonos_0__Extension").val(),
        Tipo2: "MOVIL",
        Descripcion2: $("#Telefonos_1__Descripcion").val(),
        Extension2: $("#Telefonos_1__Extension").val(),
        Tipo3: "OFICINA",
        Descripcion3: $("#Telefonos_2__Descripcion").val(),
        Extension3: $("#Telefonos_2__Extension").val(),
        Direccion1: $("#Direcciones_0__Descripcion").val(),
        Direccion2: $("#Direcciones_1__Descripcion").val()
    };

    $.ajax({
        url: '/Prospectos/SoloGuardar',
        type: 'get',
        data: Prospecto,
        success: function (result) {
            alert(result);
            location.reload();
        }
    })
}



function OpenDetailsModal(id, type) {
    console.log(type);
    if (type == "O") {
        $.ajax({
            url: '/Objeciones/GetObjecionesById',
            type: 'get',
            data: { id: id },
            success: function (result) {
                console.log(result);
                $("#ModalGenericTitle").text(result.Titulo);
                $("#ModalGenericBody").text(result.Descripcion);
                $("#ModalGeneric").modal("show");
            }
        });
    }

    if (type == "H") {
        $.ajax({
            url: '/HotelesDummys/GetHotelesById',
            type: 'get',
            data: { id: id },
            success: function (result) {
                console.log(result);
                $("#ModalGenericTitle").text(result.Ubicacion);
                $("#ModalGenericBody").text(result.Hotel);
                $("#ModalGeneric").modal("show");
            }
        });
    }

    if (type == "S") {
        $.ajax({
            url: '/ScriptDummies/GetScriptsById',
            type: 'get',
            data: { id: id },
            success: function (result) {
                console.log(result);
                $("#ModalGenericTitle").text(result.Titulo);
                $("#ModalGenericBody").text(result.Descripcion);
                $("#ModalGeneric").modal("show");
            }
        });
    }



}
//Hoteles
$(document).ready(function () {

   //Acá iban las Objeciones.

   //Acá iban los Hoteles.

   //Acá iban los Scripts.

    $('.aplicar-certificado').change(function () {
        if ($(this).is(':checked')) {
            $(".certificado").prop("disabled", false);
        }
        else {
            $(".certificado").prop("disabled", true);
        }

    });


    $("input:text").keyup(function () {
        this.value = this.value.toUpperCase();
    })

   
    $('#ni1, #ad1, #hab-more, #hab-less, .reserva-body > div > div > div > input').attr('disabled', true);
    //$('#hab-more, #hab-less').attr('disabled', true);
    //$('#hotel-continer > div > div > div > div > input')
    $(".spinners").on("change", function () {

        var hab = $(this).data("hab");
        var vale = $(this).val();
        var type = $(this).data("type");

        if (type == "a") {
            if (vale == 1) {
                $("#ni" + hab).attr("max", 0);
                $("#ni" + hab).attr("min", 0);
            }
            if (vale > 1)
                $("#ni" + hab).attr("max", 4 - vale);
        }

        if (type == "n") {
            if (vale == 2) {
                $("#ad" + hab).attr("min", 2);
                $("#ad" + hab).attr("max", 2);
            } if (vale == 1) {
                $("#ad" + hab).attr("min", 2);
                $("#ad" + hab).attr("max", 3);
            } if (vale == 0) {
                $("#ad" + hab).attr("min", 0);
                $("#ad" + hab).attr("max", 4);
            }

        }
        CalcularPrecio();
    });

    $('.spinners[type="number"]').keyup(function () {
       // alert("El valor maximo es: " + $(this).attr("max"));
        if ($(this).val() > $(this).attr("max")) {            
            $(this).value=$(this).attr("max");
            $(this).val($(this).attr("max"));
            //alert($(this).attr("max"));
        } 
    })


    $('#cant-hab').change(function () {
        CalcularPrecio();
    })



    $('#Dias').change(function () {
        var val = $('#Dias').val();
        if (val > 0) {
            $("#pHabc").removeClass("hide");
            $('#hotel-1').removeClass("hide");
            $('#ni1, #ad1, #hab-more, #hab-less, .box-body > div > div > div > input').attr('disabled', false);
        }
        else {
            $('#ni1, #ad1, #hab-more, #hab-less, .box-body > div > div > div > input').attr('disabled', true);
            $('#pHabc').addClass('hide');
            $('#hotel-1').addClass("hide");
            $(this).attr('disabled', false);
        }
        CalcularPrecio();
    })

    $('.hotelAjax').change(function () {

        var id = $('.hotelAjax').val();

        if (id != 0)
            $('#Dias').attr('disabled', false);

        if (id == 0) {
            $('#hotelSGL').val("");
            $('#hotelDBL').val("");
            $('#hotelTPL').val("");
            $('#hotelCPL').val("");
            $('#hotelCHD').val("");
            $('#hotelCHDF').val("");
            $('#collapse-price').click();
        } else {
            $.ajax({
                url: '/Hoteles/GetHotelById',
                type: 'post',
                data: { id: id },
                success: function (result) {

                    $('#hotelSGL').val(result.Simple);
                    $('#hotelDBL').val(result.Doble);
                    $('#hotelTPL').val(result.Triple);
                    $('#hotelCPL').val(result.Cuadruple);
                    $('#hotelCHD').val(result.Ninio);
                    $('#hotelCHDF').val(result.NinioGratis);
                    if ($('#box-price-hot').hasClass('collapsed-box')) {
                        CalcularPrecio();
                        $('#collapse-price').click();
                    } else {
                        return false;
                    }

                }
            });
        }
        return false;
    });

    $('.calc-hab').click(function () {
        var type = $(this).data('type');
        ControlHabs(type);
        return false;
    })

});
//End --- Dummyes
function AgregarHabitacion() {

    var Habs = $('#hotel-container .con-hab:last-child').data('hotel') + 1;
    $.ajax({
        url: '/Reservas/CreateNewHabitacion',
        type: 'post',
        data: { Habs: Habs },
        success: function (result) {
            $("#hotel-container").append(result);
            CalcularPrecio();
        }
    });
    return false;
}

function QuitarHabitacion() {
    $('#hotel-container .con-hab:last-child').remove();
    CalcularPrecio();
}
function ControlHabs(r) {
    var b = $('#cant-hab').val();
    var a = parseInt(b);
    if (a == 1 && r != '+') {
        alert('Debe haber al menos una habitaci\u00f3n');
    }
    else {
        if (r == '+') {
            $('#cant-hab').val(a + 1);
            AgregarHabitacion();

        } else {
            $('#cant-hab').val(a - 1);
            QuitarHabitacion();

        }
    }
}


function RemoveHotel(id) {
    $('#hotel-' + id).remove();
    var hb = $('#cant-hab').val();
    $('#cant-hab').val(hb - 1);
    return false;
}

function CalcularPrecio() {

    //BEGIN recoger valores de variables

    var precio = 0;//Globales para calculo de precio y Ubicación de personas

    //BEGIN precios

    var Sgl = $('#hotelSGL').val();

    var Dbl = $('#hotelDBL').val();

    var Tpl = $('#hotelTPL').val();

    var Cpl = $('#hotelCPL').val();

    var ChP = $('#hotelCHD').val();//Capturo cuanto pagan los niños adicionales

    var chF = $('#hotelCHDF').val();//Cuantos niños pasan gratis

    var dias = $('#Dias').val();//Capturamos cuantos dias van
    dias = parseInt(dias);
    //END precios

    var Simple = 0, Doble = 0, Triple = 0, Cuadruple = 0;

    //END recoger valores de variabes, y comienza la accion ...
    var adt = 0, nt = 0;
    var ch = $('.con-hab').size(); //Vemos cuantas habitaciones solicitaron
    for (var i = 1; i <= ch; i++) {
        var hot = $('#hotel-container .con-hab:nth-child(' + i + ')');
        var dat = hot.data('hotel');
        var nin = $('#hotel-container #ni' + dat).val();//Cuantos niños para cada hotel
        var ad = $('#hotel-container #ad' + dat).val();//Cuantos adultos para cada hotel
        var calc = 0;

        adt = parseInt(adt) + parseInt(ad);
        nt = parseInt(nt) + parseInt(nin);


        ad = parseInt(ad);
        console.log('dias: ' + dias);
        console.log('ad: ' + ad);
        if (ad == 1) {
            calc = parseInt(Sgl);
            Simple = Simple + 1;
        }
        if (ad == 2) {
            calc = parseInt(Dbl);
            Doble = Doble + 1;
        }
        if (ad == 3) {
            calc = parseInt(Tpl);
            Triple = Triple + 1;
        }
        if (ad == 4) {
            calc = parseInt(Cpl);
            Cuadruple = Cuadruple + 1;
        }


        calc = calc * ad;

        if (nin <= chF) { //La formula cambia si hy niños que pasan gratis
            //Por fin sacamos la cuenta
            precio = precio + (calc * dias);
        } else {
            var ni = chF - nin;
            ni = ni * ChP;
            precio = precio + (calc * dias) + ni;
        }

        $('#Costo').val(precio);
        $('#CantidadAdultos').val(adt);
        $('#CantidadNinios').val(nt);

        var create = '<input value="' + Simple + '" class="form-control text-box single-line" id="Habitaciones_0__Cantidad" name="Habitaciones[0].Cantidad" type="text"><input value="SGL" class="form-control text-box single-line" id="Habitaciones_0__Tipo" name="Habitaciones[0].Tipo" type="text"><input value="' + Doble + '" class="form-control text-box single-line" id="Habitaciones_1__Cantidad" name="Habitaciones[1].Cantidad" type="text"><input value="DBL" class="form-control text-box single-line" id="Habitaciones_1__Tipo" name="Habitaciones[1].Tipo" type="text"><input value="' + Triple + '" class="form-control text-box single-line" id="Habitaciones_2__Cantidad" name="Habitaciones[2].Cantidad" type="text"><input value="TPL" class="form-control text-box single-line" id="Habitaciones_2__Tipo" name="Habitaciones[2].Tipo" type="text"><input value="' + Cuadruple + '" class="form-control text-box single-line" id="Habitaciones_3__Cantidad" name="Habitaciones[3].Cantidad" type="text"><input value="CPL" class="form-control text-box single-line" id="Habitaciones_3__Tipo" name="Habitaciones[3].Tipo" type="text">';
        $('#Habs').html(create);

        $('#habs-table tbody').html('<tr><td>SIMPLE</td><td>' + Simple + '</td><tr> <tr><td>DOBLE</td><td>' + Doble + '</td><tr> <tr><td>TRIPLE</td><td>' + Triple + '</td><tr> <tr><td>CUADRUPLE</td><td>' + Cuadruple + '</td><tr>');
    }

}

//Ajax para cerrar session ---------------------------------------------
function sessionOut(q) {
    $.ajax({
        url: '/Login/SessionOut',
        type: 'post',
        success: function (result) {
            window.location.href = '/login';
        }
    });
    return false;
}

$(document).ready(function () {
    $('.certificado-tipo').change(function () {
        if ($('.certificado-tipo').val() == 'PORCENTAJE') {
            $('.certificado-cantidad').attr('readonly','readonly');
            $('.certificado-descuento').removeAttr('readonly');
        } else {
            $('.certificado-cantidad').removeAttr('readonly');
            $('.certificado-descuento').attr('readonly','readonly');
        }
    })

    //$(".datepickershort input[type='datetime']").datetimepicker();
    //$(".datepickershort input[type='datetime']").attr("value", "07-12")
    //alert($(".datepickershort input[type='datetime']").val());
    //$(".datepickershort input[type='datetime']").datetimepicker({
    //    onSelect: function (value, date) {
    //        alert('The chosen date is ' + value);
    //    }
    //});
    //$(".datepickershort input[type='datetime']").value("07-12");
    //$(".datepickershort input[type='datetime']").change(function () {
    //    alert($(this).attr("value"));
    //})
    //###################################### Hoteles Dummy
    //$.ajax({
    //    url: '/HotelesDummys/CreateNewHabitacion',
    //    type: 'post',
    //    success: function (result) {
    //        $("#hotel-container").append(result);
    //    }
    //});

    $("#id-helper").focusout(function () {
        var newID = $(this).val().replace(" - ", "");
        $("#Afiliado_AfiliadoIndice").val(newID);
    })

})

// Generar Reportes
function GenerarADM() {
    //alert("Por generar.");
    //window.history.pushState(null, "Nómina", "GenerarNomina");
    $.ajax({
        url: '/Archivos/GenerarNomina',
        type: 'post',
        success: function (result) {
            //$("#direcciones-container").append(result);                                      
            window.open(result[0], '_blank');
            DeleteFile(result[1]);
        }
    });
    return false;
}
function DeleteFile(q) {
    alert(q);
    $.ajax({
        url: 'Archivos/DeleteFile',
        type: 'post',
        data: { path: q },
        success: window.location.reload()
    });
    return false;
}

$('.datepicker').on('changeDate', function (ev) {
    alert('Hey')//$('#date-daily').change();
});
//$('#date-daily').val('0000-00-00');
//$('#date-daily').change(function () {
//    console.log($('#date-daily').val());
//});

function List607() {

    var list = [];
    var aux = $("tbody tr").length;
    if (aux > 0) {
        var n = 0;
        $(".table tbody tr").each(function (index) {
            n++;
            var ns = 0
            var list2 = [];
            $(this).children("td").each(function (index2) {
                ns++;
                list2.push($(this).text());
            })
            //alert("Hay: " + ns + " " + list2)
            list.push(list2)
        })
        Generar607(list);
        //console.log(list);
        //alert("hay: " + n + " registros "+list);
        //for (var i = 0; i < n; i++) {
        //    alert(list[i]);
        //}
    }
}
function Generar607(e) {
   
    $.ajax({
        url: '/Archivos/Reporte607',
        type: 'post',
        data: { List : e },
        success: function (result) {
            //$("#direcciones-container").append(result);                                      
            window.open(result[0], '_blank');
            DeleteFile(result[1]);
            //alert(result);
        }
    });
    return false;
}