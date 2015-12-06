// 项目申报基本dom操作
function declare_dom () {

}
declare_dom.prototype={
    constructor: declare_dom,
    init: function(){
        $("#member_add").click(function(){
            var new_member = "<div class=\"row Check_div\" style=\"margin-top: 20px;display: none\" ><div class=\"col-md-4 col-sm-4 col-xs-4\"><div class=\"input-group\"><input type=\"text\"  class=\"form-control member_name\"  placeholder=\"成员姓名\" name=\"member\"><div class=\"input-group-addon\"><span class=\"glyphicon glyphicon-user text-primary\"></span></div></div></div><p class=\"col-md-4 col-sm-4 col-xs-4 text-danger\" style=\"margin-top: 8px;display: none;\"><span class=\"glyphicon glyphicon-exclamation-sign\" aria-hidden=\"true\"></span>姓名不能为空</p><p class=\"col-md-4 col-sm-4 col-xs-4\" style=\"visibility: hidden\"></p><p class=\"col-md-4 col-sm-4 col-xs-4 text-danger\" style=\"margin-top: 8px;display: none;\"><span class=\"glyphicon glyphicon-ok-circle text-primary\">正确</span></p><div class=\"col-md-4 col-sm-4 col-xs-4 member_AS_container\"><button type=\"button\" class=\"btn btn-danger member_sub\" style=\"float: right\"><span class=\"glyphicon glyphicon-minus\" aria-hidden=\"true\"></span></button></div></div>";            
            $("#allmember_container").append(new_member);
            $("#allmember_container > div").last().slideDown("fast");
               
        });
// 这里是个知识点，js或者jq 对上面动态加载的节点再进行操作，对父容器再进行监听，然后再实现操作
//slideup:通过使用滑动效果，隐藏被选元素，如果元素已显示出来的话。
//slidedown:slideDown() 方法通过使用滑动效果，显示隐藏的被选元素。
        $("#allmember_container").on("click",'.member_sub',function () {
            $(this).parent().parent().slideUp("fast",function(){
                $(this).remove();
            });

        });


// 下面按照上面两个成员dom操作  对进度时间规划的任务也类似这样操作
        $("#task_add").click(function(){
            var new_task = "<div class=\"row task_is_time\" style=\"display: none;margin-top: 15px\"><label for=\"lastname\" class=\"col-sm-2 col-md-2 col-xs-12 control-label\">阶段任务</label><div class=\"col-md-2 col-sm-2 col-xs-3\"><div class=\"input-group date task_date form_date\" data-date=\"\" data-date-format=\"yyyy-mm-dd\" data-link-field=\"dtp_input2\" data-link-format=\"yyyy-mm-dd\"><input class=\"form-control task_is_empty  task_is_stime\" size=\"16\" type=\"text\" value=\"\" placeholder=\"请选择时间\" readonly name=\"ts_time\"><span class=\"input-group-addon\"><span class=\"glyphicon glyphicon-calendar text-primary\"></span></span></div></div><div class=\"col-md-2 col-sm-2 col-xs-3\"><div class=\"input-group date task_date form_date\" data-date=\"\" data-date-format=\"yyyy-mm-dd\" data-link-field=\"dtp_input2\" data-link-format=\"yyyy-mm-dd\"><input class=\"form-control task_is_empty  task_is_etime\" size=\"16\" type=\"text\" value=\"\" placeholder=\"请选择时间\" readonly><span class=\"input-group-addon\" name=\"tf_time\"><span class=\"glyphicon glyphicon-calendar text-primary\"></span></span></div><input type=\"hidden\" id=\"dtp_input2\" value=\"\" /></div><div class=\"col-md-4 col-sm-4 col-xs-4\"><div class=\"input-group\"><input type=\"text\" class=\"form-control task_is_empty\" id=\"lastname\" placeholder=\"需完成任务\" name=\"t_content\"><div class=\"input-group-addon\"><span class=\"glyphicon glyphicon-tasks text-primary\"></span></div> </div></div><div class=\"col-md-2 col-sm-2 col-xs-2\"><button type=\"button\" class=\"btn btn-danger task_sub\"style=\"float: right\"><span class=\"glyphicon glyphicon-minus\" aria-hidden=\"true\"></span></button></div></div>";
            $("#all_tasks_container").append(new_task);
            $("#all_tasks_container > div").last().slideDown("fast");
            var regEx = new RegExp("\\-","gi");
            var stime = $("#project_start_time").val();
            stime = stime.replace(regEx,"/");
            var etime = $("#project_end_time").val();
            etime = etime.replace(regEx,"/");
            stime = new Date(Date.parse(stime));
            etime = new Date(Date.parse(etime));

            // 插件的引入针对动态节点也是需要绑定的
            $('.task_date').datetimepicker({
                language:  'zh-CN',
                format: "yyyy-mm-dd",
                weekStart: 1,
                todayBtn:  1,
                autoclose: 1,
                todayHighlight: 1,
                startView: 2,
                minView: 2,
                forceParse: 0,
                todayBtn: false,
                startDate:stime,
                endDate:etime,
            });
            
    $('.form_date').datetimepicker({
        language: 'zh-CN',
        format: "yyyy-mm-dd",
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        todayBtn: false,
    });
        });
        $("#all_tasks_container").on("click",".task_sub",function(){
            $(this).parent().parent().slideUp("fast",function(){
                $(this).remove();
            });
        }); 


// 下面是对三个步骤的表单进行切换操作
        // $("#first_next_button").click(function(){
        //  $("#form_container_first").fadeOut("normal",function(){
        //      $("#form_container_second").slideDown("fast");
        //  });
        // });
        
        $("#second_prev_button").click(function(){
            $("#form_container_second").fadeOut("normal",function(){
                $("#form_container_first").slideDown("fast");
            });
        });
        $("#third_prev_button").click(function(){
            $("#form_container_third").fadeOut("normal",function(){
                $("#form_container_second").slideDown("fast");
            });
        });



    }
}


var declare_dom = new declare_dom();
declare_dom.init();