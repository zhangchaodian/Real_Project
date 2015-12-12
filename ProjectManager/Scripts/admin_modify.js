 $(document).ready(function () {
           // alert("aa");
            $(".edit_btn").click(function () {
               
                var id = $(this).attr("id");
                $.ajax({
                    url: "/Admin/getUserInfo",
                    type: "get",
                    data: { "UserID":id},
                    success: function (response) {
                     // alert(response);
                       var message = JSON.parse(response);
                       $("#UserID").html("用户:"+message.ID);
                       $("#ID").val(message.ID);
                       $("#pwd").val(message.pwd);
                       $("#nickname").val(message.nickname);
                       $("#position").val(message.position);
                       $("#email").val(message.email);
                       $("#phone").val(message.phone);
                       if (message.sex == "男") {
                           $("#sexRadio1").attr("checked", "checked");
                       }
                       else {
                           $("#sexRadio2").attr("checked", "checked");
                       }
                       if (message.type == "b") {
                           $("#u_type2").attr("selected", "selected");
                       }
                       else {
                           $("#u_type1").attr("selected", "selected");

                       }
                    },
                    error: function ErrorCallback(XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown + ":" + textStatus);
                    }
                });
                
               $("#myModal_ChangeInfo").modal({});
                return false;
            });
 
        });
        $(document).ready(function () {
            //alert("aa");
            $("#modify_btn").click(function () {

               
                $.ajax({
                    url: "/Admin/modify",
                    type: "post",
                    data: $("#userChange").serialize(),
                    success: function (response) {
                        //var message = JSON.parse(response);
                        alert(response);
                        location.reload();

                    },
                    error: function ErrorCallback(XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown + ":" + textStatus);
                    }
                });

                
                return false;
            });

        });
        $(function () {
           
            $("#addUser_btn").click(function () {
                //alert("a");
                $.ajax({
                    url: "/Admin/addUser",
                    type: "post",
                    data: $("#userForm").serialize(),
                    dataType: "json",
                    success: function (response) {
                        //var message = JSON.parse(response);
                        alert(response);
                        location.reload();
                        $("#myModal_AddUser").find("input").each(
                            function () {
                                switch (this.type) {
                                    case 'text':
                                        $(this).val('');
                                        break;
                                }
                            });
                    },
                    
                });
                return false;
            });
        });
       

  $(function () {
           // alert("aa");
           $(".del_btn").unbind("click");//关键
            $(".del_btn").click(function () {
            //alert("addUser");
               
               if(confirm("确定要删除吗？")){
                   var id = $(this).attr("id");
                $.ajax({
                    url: "/Admin/deleteUser",
                    type: "get",
                    data: { "UserID":id},
                    success: function (response) {
                     alert(response);
                     location.reload();
                      
                    },
                   /* error: function ErrorCallback(XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown + ":" + textStatus);
                    }*/
                });
               }
                return false;
            });
          });
 
       function savaAddtxt(btn) {
                btn.onclick = onDealing; //防止IE浏览器中提交两次
            }