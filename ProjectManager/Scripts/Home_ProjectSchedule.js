// 项目进度根据当前系统时间与完成的进度生成状态



function progress_time_judge(stime,etime,per_num,dom_now){
    this.stime = stime;
    this.etime = etime;
    this.per_num = per_num;
    this.U_stime = null; //stime要在原型里面作相应处理再附到这里
    this.U_etime = null;
    this.Diff_time_NowWithS = null; //原型两个不相关功能函数要共享对象同样的时间差数据
    this.Diff_time_EwithS = null;
    this.C_now_time = null;
    this.dom_now = dom_now;
}
progress_time_judge.prototype = {
    constructor : progress_time_judge,
    init : function(){
        var that = this;
        console.log(that.stime);
        (function(){
            //正则表达把2015-1-1的"-"替换才成"/"Date()才可以处理
            var regEx = new RegExp("\\-","gi");
            stime = that.stime.replace(regEx,"/");
            etime = that.etime.replace(regEx,"/");
            that.U_stime = new Date(Date.parse(stime));
            that.U_etime= new Date(Date.parse(etime));
            // 因为自己生成的Unix开始和结束时间都是00:00:00，所以先获取当前时间的年月日再生成也成00:00:00
            var now_time = new Date();
            var now_time_str = now_time.getFullYear();
            that.C_now_time = new Date(now_time.getFullYear(),now_time.getMonth(),now_time.getDate());
            // console.log(that.C_now_time);
            //两个时间差，然后方便百分比与项目百分比做比较来以判断状态
            that.Diff_time_EwithS = that.U_etime - that.U_stime;
            that.Diff_time_NowWithS = that.C_now_time - that.U_stime;
        })();

        (function(){
            if(that.C_now_time >= that.U_etime){
                // console.log("当前时间已经超过该项目的结束时间，但是进度还没到100%,危险");
                $(that.dom_now).find(".progress_move").addClass("progress-bar-danger");
            }
            else{
                if(that.Diff_time_NowWithS*100 <= that.per_num*that.Diff_time_EwithS){
                    // console.log("当前已过时间占总时间的百分比还没有超过该项目进度的百分比，安全");
                    
                    }
                else{
                    // console.log("当前已过时间占总时间的百分比超过该项目进度的百分比，警惕下");
                    $(that.dom_now).find(".progress_move").addClass("progress-bar-warning");
                }    
            }
        })();

    }
}



$(".table_tbody_tr").each(function(){
    var that = this;
    var stime = $(this).find("input").first().val(),
        etime = $(this).find("input").first().next().val(),
        per_num = parseInt($(this).find("input").last().val());    
    var progress_time_judge1 = new progress_time_judge(stime,etime,per_num,that);
    progress_time_judge1.init();
})