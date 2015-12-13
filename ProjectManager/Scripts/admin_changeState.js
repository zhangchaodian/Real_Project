 $(function(){
     $(".enroll").click(function () {
         var id = $(this).attr("id");
         $("#myModal_ChangeStatus #ID").attr("value",id);
         $.ajax({
             url: "/Admin/getRollForm",
             type: "get",
             data: { "p_id": id },
             success: function (response) {
               
                 var message = JSON.parse(response);
                 switch (message.state) {
                     case "a":
                         $("#a").attr("checked", "checked");
                         break;
                     case "b":
                         $("#b").attr("checked", "checked");
                         break;
                     case "c":
                         $("#c").attr("checked", "checked");
                         break;
                     case "d":
                         $("#d").attr("checked", "checked");
                         break;
                     case "e":
                         $("#e").attr("checked", "checked");
                         break;
                     case "f":
                         $("#f").attr("checked", "checked");
                         break;
                     case "g":
                         $("#g").attr("checked", "checked");
                         break;
                     case "h":
                         $("#h").attr("checked", "checked");
                         break;
                     case "i":
                         $("#i").attr("checked", "checked");
                         break;
                 }
                 $("#comment").val(message.Comment);
             },
             error: function ErrorCallback(XMLHttpRequest, textStatus, errorThrown) {
                  alert(errorThrown + ":" + textStatus);
              }
         });
     });
    });
 $(function(){
     $("#modifyComment").click(function () {
        // var id = $(this).attr("id");
       //console.log("ad");
         $.ajax({
             url: "/Admin/modifyComment",
             type: "post",
             data: $("#Commentform").serialize(),
             success: function (response) {
               alert(response);
             location.reload();
             },
             error: function ErrorCallback(XMLHttpRequest, textStatus, errorThrown) {
                  alert(errorThrown + ":" + textStatus);
              }
         });
     });
    });
