$(function () {
    $("#btn3").click(function () {
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
    $("#btn4").click(function(){

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