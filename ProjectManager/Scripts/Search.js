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
             $("#b1").empty();
             $("#b1").html(response);
            },
            error:function(){
                 //$("#b1").empty();
            }
        });
        return false;
    });
});


//详情按钮的ajax实现
  $(document).ready(function () {
             $(".detail").click(function () {
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









$(function () {
    $("#btn2").click(function () {
        //alert("aaaa211");
       // $("#b1").empty();
        $.ajax({
            url: "/UserManager/GetSearch_Schedule",
            type: "post",
            data: $('#form2').serialize(),
            dataType: "json",
            success: function (response) {
                // $("#Id").text(response.Id);
                // $("#Name").text(response.Name);
                // $("#Age").text(response.Age);
                $("#b2").empty();
                $("#b2").html(response);   
              
                (function(){
    $(".progress_move").each(function(){
            var that = this;
            var value = parseInt($(this).text().trim());
            var num = 0;
            // console.log(value);
            function num_add(){
                if(num==value){
                    clearInterval();
                }
                else{
                    num++;
                    $(that).text(num+"%");
                }            
            }
            num_add();
            setInterval(num_add,3);
            $(this).animate({
                width : value+"%",
            },
                4*value);
 });

})();
        
                $(".table_tbody_tr").each(function(){
    var that = this;
    var stime = $(this).find("input").first().val(),
        etime = $(this).find("input").first().next().val(),
        per_num = parseInt($(this).find("input").last().val());    
    var progress_time_judge1 = new progress_time_judge(stime,etime,per_num,that);
    progress_time_judge1.init();
});


            },
        error: function ErrorCallback(XMLHttpRequest, textStatus, errorThrown) {
                         alert(errorThrown + ":" + textStatus);
                     },


        });
        return false;
    });
});


$(function(){
    $("#btn3").click(function(){

        $.ajax({
            url:"/UserManager/getSearch_Schedule2",
            type:"post",
            data:$("#form3").serialize(),
            dataType:"json",
            success:function(response){
             $("#b2").empty();
             $("#b2").html(response);
             (function(){
    $(".progress_move").each(function(){
            var that = this;
            var value = parseInt($(this).text().trim());
            var num = 0;
            // console.log(value);
            function num_add(){
                if(num==value){
                    clearInterval();
                }
                else{
                    num++;
                    $(that).text(num+"%");
                }            
            }
            num_add();
            setInterval(num_add,3);
            $(this).animate({
                width : value+"%",
            },
                4*value);
 });

})();
        
                $(".table_tbody_tr").each(function(){
    var that = this;
    var stime = $(this).find("input").first().val(),
        etime = $(this).find("input").first().next().val(),
        per_num = parseInt($(this).find("input").last().val());    
    var progress_time_judge1 = new progress_time_judge(stime,etime,per_num,that);
    progress_time_judge1.init();
});

            },
            error:function(){
                 //$("#b1").empty();
            }
        });
        return false;
    });
});