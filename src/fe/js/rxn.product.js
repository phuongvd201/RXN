(function ($) {

    rxn.createNamespace(rxn, "product");

    rxn.product = (function () {

        function doInit() {
            _initDropDownListData();
            _initEvents();
        }

        var _initDropDownListData = function () {
            rxn.ajax({
                url: rxn.constant.api_base_url + "Data/PrePackTypes",
                type: "GET",
                success: function (data) {
                    var optionTags;
                    $.each(data, function (key, value) {
                        optionTags += "<option value = '" + value.id + " '>" + value.name + " </option>";
                    });
                    $('#imPrePackStyle').append(optionTags);
                }
            });
        }

        var _initEvents = function () {
            $("#upload-logo").change(function () {
                var input = this;
                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#imLogo')
                            .attr('src', e.target.result);
                    };

                    reader.readAsDataURL(input.files[0]);
                }
            });

            $("#js-discard-logo").on("click", function () {
                $('#imLogo').attr('src', "images/no-image.png");
            });

            $("#imIsPrePack").on("change", function () {
                $("#imPrePackStyle").val("0")
                $("#imPrePackStyle").prop("disabled", !$('#imIsPrePack').is(':checked'));
            });

            $("#imWidth,#imLength,#imHeight").on("keyup", function () {
                $("#imCube").val(($("#imHeight").val() * $("#imLength").val() * $("#imWidth").val()).toFixed(3));
            });

            $(".js-submit-form").on("click", function () {
                if (_validateForm()) {
                    rxn.ui.block();
                    rxn.ajax({
                        url: rxn.constant.api_base_url + "product/createorupdate",
                        data: JSON.stringify(_getFormValue()),
                        success: function (data) {
                            rxn.notify.success("Success !")
                        },
                        complete: function () {
                            rxn.ui.unblock();
                        }
                    });
                }

            });

            var _validateForm = function () {
                if (!($("#itemMasterID").val() > 0 && $("#itemMasterID").val().length <= 9)) {
                    $("#itemMasterID").addClass("error");
                    $(".validation-summary-errors").html("<li>Product Number is greater than 0 and maximum 9 digits</li>");
                    $(".validation-summary").show();

                    return false;
                } else {
                    $("#itemMasterID").removeClass("error");
                    $(".validation-summary").hide();
                    return true;
                }
            }

            $("#jqGrid").jqGrid({
                styleUI: 'Bootstrap',
                colModel: [{
                        label: 'Location',
                        name: 'imiSiteName',
                    },
                    {
                        label: 'On-Hand',
                        name: 'imiQtyOnHand',
                    },
                    {
                        label: 'On-Hand Pcs',
                        name: 'imiQtyOnHandPcs',
                    },
                    {
                        label: 'Allocated',
                        name: 'imiQtyAllocated',
                    },
                    {
                        label: 'Allocation Pcs',
                        name: 'imiQtyAllocatedPcs',
                    },
                    {
                        label: 'Available',
                        name: 'imiQtyAvailable',
                    },
                    {
                        label: 'Available Pcs',
                        name: 'imiQtyAvailablePcs',
                    }
                ],
                datatype: 'local',
                viewrecords: true,
                width: 1110,
                rowNum: 20,
            });

            function fetchGridData(itemMasterID) {

                // show loading message
                $("#jqGrid")[0].grid.beginReq();
                rxn.ajax({
                    url: rxn.constant.api_base_url + "product/GetInventorySites",
                    data: JSON.stringify({
                        id: itemMasterID,
                    }),
                    success: function (result) {
                        var gridArrayData = [];
                        var summaryItem = {
                            imiSiteID: 0,
                            imiSiteName: 'ALL SITE',
                            imiQtyOnHand: 0,
                            imiQtyOnHandPcs: 0,
                            imiQtyAllocated: 0,
                            imiQtyAllocatedPcs: 0,
                            imiQtyAvailable: 0,
                            imiQtyAvailablePcs: 0,
                        };
                        if (result.length > 0) {
                            var imPack = $("#imPack").val();
                            for (var i = 0; i < result.length; i++) {
                                var item = result[i];
                                var gridItem = {
                                    imiSiteID: item.imiSiteID,
                                    imiSiteName: item.imiSiteName,
                                    imiQtyOnHand: item.imiQtyOnHand,
                                    imiQtyOnHandPcs: item.imiQtyOnHand * imPack,
                                    imiQtyAllocated: item.imiQtyAllocated,
                                    imiQtyAllocatedPcs: item.imiQtyAllocated * imPack,
                                    imiQtyAvailable: item.imiQtyOnHand - item.imiQtyAllocated,
                                    imiQtyAvailablePcs: (item.imiQtyOnHand - item.imiQtyAllocated) * imPack,
                                }
                                gridArrayData.push(gridItem);

                                summaryItem.imiQtyOnHand += gridItem.imiQtyOnHand;
                                summaryItem.imiQtyOnHandPcs += gridItem.imiQtyOnHandPcs;
                                summaryItem.imiQtyAllocated += gridItem.imiQtyAllocated;
                                summaryItem.imiQtyAllocatedPcs += gridItem.imiQtyAllocatedPcs;
                                summaryItem.imiQtyAvailable += gridItem.imiQtyAvailable;
                                summaryItem.imiQtyAvailablePcs += gridItem.imiQtyAvailablePcs;
                            }

                            gridArrayData.push(summaryItem);
                        }


                        $("#jqGrid").jqGrid('clearGridData');
                        // set the new data
                        $("#jqGrid").jqGrid('setGridParam', {
                            data: gridArrayData
                        });
                        // hide the show message
                        $("#jqGrid")[0].grid.endReq();
                        // refresh the grid              
                        $("#jqGrid").trigger('reloadGrid');
                    }
                });
            }

            $("input#itemMasterID").autocomplete({
                source: function (request, response) {
                    var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");

                    rxn.ajax({
                        url: rxn.constant.api_base_url + "product/productnumbers",
                        method: "GET",
                        success: function (result) {
                            response($.map(result, function (v, i) {
                                var text = v;
                                if (text && (!request.term || matcher.test(text))) {
                                    return {
                                        label: v,
                                        value: v
                                    };
                                }
                            }));
                        }
                    });
                },
                select: function (event, selectedItem) {
                    rxn.ajax({
                        url: rxn.constant.api_base_url + "product/GetItemMaster",
                        data: JSON.stringify({
                            id: selectedItem.item.value,
                        }),
                        success: function (result) {
                            _setFormValue(result);
                            $("#imPrePackStyle").prop("disabled", !result.imIsPrePack);

                        }
                    });
                    fetchGridData(selectedItem.item.value);
                },
                minLength: 1
            });

            var _setFormValue = function (product) {
                $("#imIsHazardousMaterial").prop("checked", product.imIsHazardousMaterial);
                $("#imPack").val(product.imPack);
                $("#imDescription").val(product.imDescription);
                product.imImageData ? $("#imLogo").attr("src", 'data:image\/png;base64,' + product.imImageData) : $('#imLogo').attr('src', "images/no-image.png");;
                $("#imCostCenterCode").val(product.imCostCenterCode);
                !product.imExpirationDate ? $("#imExpirationDate").val("") : $("#imExpirationDate").datepicker('update', new Date(product.imExpirationDate));

                $("#imUnitPrice").val(product.imUnitPrice && product.imUnitPrice.toFixed(2));
                $("#imWidth").val(product.imWidth && product.imWidth.toFixed(2));
                $("#imLength").val(product.imLength && product.imLength.toFixed(2));
                $("#imHeight").val(product.imHeight && product.imHeight.toFixed(2));
                $("#imCube").val((product.imWidth * product.imHeight * product.imLength).toFixed(3));
                $("#imIsPrePack").prop("checked", product.imIsPrePack);
                $("#imPrePackStyle").val(product.imPrePackStyle);
            }

            var _getFormValue = function () {
                var logoSrc = $("#imLogo").attr("src");
                if (logoSrc && logoSrc.indexOf("base64") > -1) {
                    logoSrc = logoSrc.replace(/^data:image\/(png|jpg|jpeg);base64,/, "");
                } else {
                    logoSrc = null;
                }

                return {
                    itemMasterID: $("#itemMasterID").val(),
                    imPack: $("#imPack").val(),
                    imImageData: logoSrc,
                    imDescription: $("#imDescription").val(),
                    imIsHazardousMaterial: $("#imIsHazardousMaterial").is(':checked'),
                    imCostCenterCode: $("#imCostCenterCode").val(),
                    imExpirationDate: $("#imExpirationDate").val(),
                    imUnitPrice: $("#imUnitPrice").val(),
                    imWidth: $("#imWidth").val(),
                    imLength: $("#imLength").val(),
                    imHeight: $("#imHeight").val(),
                    imIsPrePack: $("#imIsPrePack").is(':checked'),
                    imPrePackStyle: $("#imPrePackStyle").val(),
                };
            }
        }

        return {
            init: doInit,
        };

    })();

    $(document).ready(function () {
        rxn.product.init();
    });

})(jQuery);