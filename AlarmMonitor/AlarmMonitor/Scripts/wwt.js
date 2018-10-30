
/*******页面背景填充*******/
$(".frame-wrapper").css('min-height', $(window).height() - $('.main-footer').outerHeight());

/*******搜索DIV点击*******/
$('.dropdown-search input, .dropdown-search label,.dropdown-search .form-group').click(function (e) {
    e.stopPropagation();
});

/*****************************扩展Form提交转JSON格式*******************************/
$.fn.serializeJson = function () {
    var serializeObj = {}; 
    var array = this.serializeArray(); 
    $(array).each(function () {
        if (serializeObj[this.name]) {
            if ($.isArray(serializeObj[this.name])) {
                serializeObj[this.name].push(this.value); 
            }
            else {
                serializeObj[this.name] = [serializeObj[this.name], this.value];
            }
        }
        else {
            serializeObj[this.name] = this.value;
        }
    });
    return serializeObj;
};

/*******初始化*********/
function InitDatatables(dataTableObj, actionUrl, aoColumns, oTable, paging, rowCallBack, pageSize, initComplete, ordering, checkbox, drawCallback, additionControl, sPaginationType, index, isclick) {
    if (paging == null) {
        paging = true;
    }
    if (pageSize == null) {
        pageSize = 20;
    }
    if (ordering == null) {
        ordering = true
    }
    if (checkbox == null) {
        checkbox = false;
    }
    if (sPaginationType == null)
    {
        sPaginationType = "full_numbers";
    }
    if (index == null) {
        index = true;
    }
    if (isclick == null) {
        isclick = true;
    }
    checkbox = checkbox && true;
    oTable = dataTableObj.dataTable({
        "bJQueryUI": false,
        "sPaginationType": sPaginationType,
        'bLengthChange': true,
        "bFilter": false,
        "bInfo": true,
        "ordering": ordering,
        'bPaginate': paging,
        "bProcessing": true,
        "bAutoWidth": false,
        "bServerSide": true,
        "bStateSave": false,
        "iDisplayLength": pageSize,
        "aLengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "sAjaxSource": actionUrl,
        "aoColumns": aoColumns,
        "dom": 'rt<"bottom"<"col-md-3"i><"col-md-9"p><"clear">>',
        "fnCreatedRow": function (nRow, aData, iDataIndex) {
            if (oTable.fnSettings().aoColumns[0].bVisible) {
                if (index) {
                    $(nRow).prepend("<td>" + (iDataIndex + 1) + "</td>");
                }
                if (checkbox) {
                    $(nRow).prepend("<td style='text-align:center;' data-id=''><input type='checkbox' class='tdchk'  /></td>");
                }
                if (additionControl != null) {
                    $(nRow).append("<td style='text-align:center;'>" + additionControl + "</td>");
                }
            }
        },
        "drawCallback": function (settings) {
            if (oTable.api().ajax.params().sEcho == 1) {
                var params = oTable.api().ajax.params();
                var sortIndex = "0";
                $.each(params, function (i, o) {
                    if (i.indexOf("mDataProp_") > -1) {
                        if (o == "sort") {
                            sortIndex = i.substr(10);
                        }
                    }
                });
                oTable.api().order([[sortIndex, 'asc']]);
            }
            if (drawCallback && typeof (drawCallback)) {
                drawCallback();
            }
        },
        "fnRowCallback": rowCallBack,
        "initComplete": initComplete,
        "language": {
            "lengthMenu": "每页 _MENU_ 条记录",
            "zeroRecords": "没有找到记录",
            "info": "第 _PAGE_ 页 ( 总共 _PAGES_ 页 )",
            "infoEmpty": "无记录",
            "paginate": {
                "first": "首页",
                "last": "尾页",
                "next": "下一页",
                "previous": "上一页"
            }

        }
    });
    if (index) {
        oTable.find("thead tr").prepend("<th width='20'></th>");
    }
    if (checkbox) {
        oTable.find("thead tr").prepend("<th width='40' style='text-align:center;padding:0px;margin:0px;vertical-align:middle;'><input type='checkbox' class='thchk' /></th>");
    }
    if (isclick) {
        oTable.find("tbody").on('click', 'tr', function (event) {
            if ($(this).find("td").attr("class") == "dataTables_empty") {
                return false;
            }
            //if (event.target.nodeName.toLowerCase() == "td") {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                oTable.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
            //}
        });
    }
    return oTable;
}

$("table").on("click", "input[type='checkbox'].thchk", function () {
    if ($(this).is(":checked")) {
        oTable.find("input[type='checkbox'].tdchk").prop("checked", true);
    }
    else {
        oTable.find("input[type='checkbox'].tdchk").prop("checked", false);
    }
});

/*************************/
function createBackDrop() {
    var div = document.createElement("div");
    div.className = "modal-backdrop fade in";
    div.innerHTML = "<div style='font-size:16px;position:absolute;left:50%;top:50%;'>Loading...</div>";
    div.style.color = "#fff";
    document.body.appendChild(div);
}

function removeBackDrop() {
    if (document.getElementsByClassName("modal-backdrop").length > 0) {
        $(document.getElementsByClassName("modal-backdrop")).remove();
    }
}

/*******弹出表单*********/
function ShowModal(actionUrl, param, title, width) {

    //createBackDrop();

    //表单初始化
    $(".modal-title").html(title);
    $("#modal-content").attr("action", actionUrl);
    if (width == 2) {
        //$(".modal-dialog").addClass("modal-lg-99");
        //var height = $(window).height() - 152;
        //$(".modal-dialog .modal-body").height(height);
        //$(".modal-dialog .modal-body").css({ "overflow-y": "auto" });
        $(".modal-dialog").addClass("modal-lg-99");
        var height = $(window).height();
    }
    else if (width == true) {
        $(".modal-dialog").addClass("modal-lg");
    }
    else if (width != null) {
        $(".modal-dialog").width(width);
    }

    $.ajax({
        type: "GET",
        url: actionUrl,
        data: param,
        async: false,
        beforeSend: function () {
            //
        },
        success: function (result) {
            $("#modal-content").html(result);
            //removeBackDrop();
            $('#modal-form').modal('show');
            //RegisterForm();
        },
        error: function () {
            //
        },
        complete: function () {
            //
        }
    });
}

/*******弹出表单同步标记*********/
function ShowModalByTag(tag,actionUrl, param, title, width) {

    //createBackDrop();

    //表单初始化
    $("."+tag+"-modal-title").html(title);
    $("#" + tag + "-modal-content").attr("action", actionUrl);
    if (width == 2) {
        //$(".modal-dialog").addClass("modal-lg-99");
        //var height = $(window).height() - 152;
        //$(".modal-dialog .modal-body").height(height);
        //$(".modal-dialog .modal-body").css({ "overflow-y": "auto" });
        $(".modal-dialog").addClass("modal-lg-99");
        var height = $(window).height();
    }
    else if (width == true) {
        $(".modal-dialog").addClass("modal-lg");
    }
    else if (width != null) {
        $(".modal-dialog").width(width);
    }

    $.ajax({
        type: "GET",
        url: actionUrl,
        data: param,
        async: false,
        beforeSend: function () {
            //
        },
        success: function (result) {
            $("#" + tag + "-modal-content").html(result);
            //removeBackDrop();
            $('#'+tag+'-modal-form').modal('show');
            //RegisterForm();
        },
        error: function () {
            //
        },
        complete: function () {
            //
        }
    });
}

/*******弹出Dialog*********/
function ShowDialogModal(actionUrl, param, title) {

    //Dialog初始化
    $(".model-title").html(title);

    $.ajax({
        type: "GET",
        url: actionUrl,
        data: param,
        beforeSend: function () {
            //
        },
        success: function (result) {
            $("#dialogModal-content").html(result);
            $('#dialogModal-form').modal('show');
            //RegisterForm();
        },
        error: function () {
            //
        },
        complete: function () {
            //
        }
    });
}

/*******注册验证脚本*********/
//function RegisterForm() {
//    $('#modal-content').removeData('validator');
//    $('#modal-content').removeData('unobtrusiveValidation');
//    $.validator.unobtrusive.parse('#modal-content');
//}

/*******关闭弹出框*********/
function CloseModal() {
    $('#modal-form').modal('hide');
    ClearForm($("#modal-content"));
}
/*******关闭弹出框根据标记*********/
function CloseModalByTag(tag) {
    $('#'+tag+'-modal-form').modal('hide');
    ClearForm($("#"+tag+"-modal-content"));
}


/*******清除表单数据*********/
function ClearForm(obj) {
    obj.find(':input').not(':button, :submit, :reset').val('').removeAttr('checked').removeAttr('selected');
}

/*******保存表单*********/
function SaveModal(oTable, pActionUrl, pData, callback, modelHide) {
    var $form = $("#modal-content");
    var actionUrl = pActionUrl || $form.attr("action");
    //alert(actionUrl);
    var data = pData || $form.serialize();
    modelHide = (modelHide == null ? true : modelHide);
    if (!$form.valid()) {
        return;
    }
    //alert(data);
    $.ajax({
        type: "POST",
        url: actionUrl,
        data: data,
        async: false,
        success: function (data) {
            //判断返回值，若为Object类型，即操作成功
            var result = ((typeof data == 'object') && (data.constructor == Object));
            if (result) {
                if (data.ResultType == "0") {
                    toastr.success(data.Message);
                    if (modelHide) {
                        $('#modal-form').modal('hide');
                    }
                }
                else if (data.ResultType == "6") {
                    toastr.warning(data.Message);
                }
                else if (data.ResultType == "7") {
                    toastr.error(data.Message);
                }
                if (oTable) {
                    ReloadDataTable(oTable);
                }
                if (callback && typeof (callback) == "function") {
                    callback();
                }
            }
            else {
                $("#modal-content").html(data);
            }
        },
        error: function () {
            var result = ((typeof data == 'object') && (data.constructor == Object));
            if (result) {
                toastr.error(data.Message);
            }
            else {
                $("#modal-content").html(data);
            }
        }
    });
}

function SaveModalGetParams(oTable, actionUrl, data) {
    var $form = $("#modal-content");

    if (!$form.valid()) {
        return;
    }

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: data,
        success: function (data) {
            //判断返回值，若为Object类型，即操作成功
            var result = ((typeof data == 'object') && (data.constructor == Object));
            if (result) {
                toastr.success(data.Message);
                $('#modal-form').modal('hide');
                ReloadDataTable(oTable);
            }
            else {
                $("#modal-content").html(data);
            }
        }
    });
}

/*******刷新表格*********/
function ReloadDataTable(obj) {
    obj = obj.fnDraw(false);
}

/*******删除操作*********/
function DeleteRecord(actionUrl, param, oTable, callback) {
    bootbox.confirm("确定删除此条数据?", function (r) {
        if (r) {
            $.ajax({
                type: "POST",
                url: actionUrl,
                data: param,
                async: false,
                beforeSend: function () {
                    //
                },
                success: function (result) {
                    if (result.ResultType == 0) {
                        toastr.success(result.Message);
                    }
                    else if (result.ResultType == 6) {
                        toastr.warning(result.Message);
                    }
                    else if (result.ResultType == 7) {
                        toastr.error(result.Message);
                    }
                    ReloadDataTable(oTable);
                    if (callback && typeof (callback) == "function") {
                        callback();
                    }
                },
                error: function (result) {
                    if (result) {
                        toastr.error(data.Message);
                    }
                    else {
                        $("#modal-content").html(data);
                    }
                    //
                },
                complete: function () {
                    //
                }
            });
        }
    });
}
/********点击Table的首列Checkbox则选择该行*********/
//oTable.find("tbody").on("click", "input:checkbox[class='tdchk']", function () {
//    $(this).parents("tr").addClass("selected");
//    return;
//})
/*******获取行值*********/
function getSelectRow(oTable, field) {
    var $select = oTable.$('tr.selected');
    if ($select.length > 0)
        return oTable.fnGetData(oTable.$('tr.selected').get(0))[field];
    else
        return null
}

/*******获取多行值*********/
function getSelectRows(oTable, field, sb) {
    var $select = oTable.find("input[class='tdchk']:checked");
    if ($select.length > 0) {
        if (sb == null) {
            sb == ",";
        }
        var str = $select.map(function (i, o) { return oTable.fnGetData($(this).parents("tr").get(0))[field] }).get().join(sb);
        return str
    }
    else {
        return null;
    }
}

/*******获取参数值*********/
function getRegexParam(name, url) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    if (url != null && url.length > 0) {
        var r = url.substr(1).match(reg);
    }
    else {
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    }
    if (r != null) return unescape(r[2]); return null; //返回参数值
}

/********** Show Dialog ************/

function ShowBootBoxDialog(title, url, data, content, width, buttons) {
    var msg;
    $.ajax({
        type: "GET",
        url: url,
        data: data,
        async: false,
        beforeSend: function () {
            //
        },
        success: function (result) {
            msg = result;
        },
        error: function () {
            //
        },
        complete: function () {
            //
        }
    });
    bootbox.dialog({
        title: title,
        message: msg,
        buttons: buttons
    });
    //if (className != null) {
    //if (className == "modal-lg-99") {
    //    var height = $(window).height() - 152;
    //    $(".bootbox .modal-dialog .modal-body").height(height);
    //    $(".bootbox .modal-dialog .modal-body").css({ "overflow-y": "auto" });
    //}
    $(".bootbox .modal-dialog").width(width);
    // }
}


/************INPUT 输入验证（数字）***************/
function ValidInt(self) {
    return self.value.replace(/[^\d]/g, '');
}

function ValidPostInt(self) {
    return self.value.clipboardData.setData('text', clipboardData.getData('text').replace(/[^\\d]/g, ''));
}

function ValidFloat(self) {
    return self.value.replace(/[^-(\d)(.)?]/g, '');
}
/**********INPUT 输入验证（数字）END*************/
