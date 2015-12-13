//第一个查询的ajax提交表单 首页
$(function () {
    $("#btn1").click(function () {
        // alert("aaaa");
       // $("#b1").empty();
        $.ajax({
            url: "/UserManager/GetSearch",
            type: "post",
            data: $('#form1').serialize(),
            dataType: "json",
            success: function (response) {
                // $("#Id").text(response.Id);
                // $("#Name").text(response.Name);
                // $("#Age").text(response.Age);
                $("#b1").empty();
                $("#b1").html(response);
            },
             error: function ErrorCallback(XMLHttpRequest, textStatus, errorThrown) {
                         alert(errorThrown + ":" + textStatus);
                     },


        });
        return false;
    });
});

$(function(){
    $("#btn2").click(function(){
     
        $.ajax({
            url:"/UserManager/GetSearch2",
            type:"post",
            data:$("#form2").serialize(),
            dataType:"json",
            success:function(response){
              //  alert("aaaa");
             $("#b1").empty();
             $("#b1").html(response);
            },
            error:function(){
                 $("#b1").empty();
            }
        });
        return false;
    });
});


//详情按钮的ajax实现
  $(document).ready(function () {
             $(".detail").click(function () {
                //alert("aa");
                 var id = $(this).attr("id");
                 $.ajax({
                     url: "/UserManager/GetDetail/",
                     type:"get",
                     // contentType: "application/json; charset=utf-8",
                     data:{ p_id:id },
                     dataType: "json",
                     success: function (details) {
                         var message = JSON.parse(details);
                       $("#p_name").html(message.p_name);
                       $("#p_type").html(message.type);
                       $("#st_name").html(message.st_name);
                       $("#st_detail").html(message.st_detail);
                       $("#group").html(message.group);
                       $("#rank").html(message.rank);
                       $("#s_time").html(message.s_time);
                       $("#f_time").html(message.f_time);
                     },
                     error: function ErrorCallback(XMLHttpRequest, textStatus, errorThrown) {
                         alert(errorThrown + ":" + textStatus);
                     }
                 });
           $("#myModal_ShowDetail").modal({});
                 return false;
             });

         });









