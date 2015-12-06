<!-- 甘特图js算法 -->
// 记住这里定义构造函数的变量不是var而是this.  因为后面new一个对象，是把this指针赋给这个对象，如果是var,这些东西没有被this指针指向，找不到,var是局部变量所用，一般跟函数捆绑在一起
    function gantt(){
        this.data = [
            {
                id : 1,
                task : '任务1',
                leader : '张朝钿',
                stime: ['2015','11','1'],
                etime: ['2015','11','9'],
            },
            {
                id : 2,
                task : '任务2',
                leader : '张朝钿',
                stime: ['2015','11','14'],
                etime: ['2015','11','25'],
            },
            {
                id : 3,
                task : '任务3',
                leader : '张朝钿',
                stime: ['2015','11','5'],
                etime: ['2015','12','8'],
            },
            {
                id : 4,
                task : '任务5',
                leader : '张朝钿',
                stime: ['2015','11','5'],
                etime: ['2015','11','11'],
            },
            {
                id : 5,
                task : '任务6',
                leader : '张朝钿',
                stime: ['2015','11','5'],
                etime: ['2015','11','10'],
            }    
        ];
        this.stime = ['2015','11','1'];
        this.etime = ['2015','12','28'];
    };
    gantt.prototype = {
        constructor : gantt,
        init : function(){
            var that = this;
            var gantt_table_tr1 = $("#gantt_table_tr1");
                // gantt_table = document.getElementById("gantt_table");
                


            //下面函数是生成表头起始和结束时间
            var stime = [],
                etime = [];
                for(var se_i=0;se_i<that.stime.length;se_i++){
                    stime[se_i] = parseInt(that.stime[se_i]);
                    etime[se_i] = parseInt(that.etime[se_i]);
                }
            (function(){
                var docFragment = document.createDocumentFragment();               
                var D_year = etime[0]-stime[0];
                var D_month = etime[1]-stime[1];
                var count_day = null;
                docFragment = document.createDocumentFragment();
                switch(D_year){
                    case 0:
                    switch(D_month){
                        case 0:
                        count_day = etime[2]-stime[2];
                        for(var i=1;i<=count_day+1;i++){
                            var th_item = $("<th>");
                            if (i==etime[2] || i==stime[2]) {
                                    th_item.text(i + '/'+etime[0]+'.'+etime[1]);
                                }
                                else{
                                    th_item.text(i);
                                }
                                th_item.appendTo(gantt_table_tr1);
                        }
                        break;
                        default:
                        for(var i=stime[1];i<=etime[1];i++){
                        if(i<=7){
                            if(i%2!=0){
                                count_day = 31;
                            }
                            else if(i==2){
                                if(((etime[0]%4==0)&&(etime[0]%100!=0))||(etime[0]%400==0)){
                                    count_day = 29;
                                }
                                else{
                                    count_day = 28;
                                }
                            }
                            else{
                                count_day = 30;
                            }
                        }
                        else{
                            if(i%2==0){
                                count_day = 31;
                            }
                            else{
                                count_day = 30;
                            }
                        }
                        if(i==stime[1]){
                            D_count_day = count_day-stime[2]+1;
                            for(var s_j=stime[2];s_j<=count_day;s_j++){
                                var th_item = $("<th>");
                                if (s_j==stime[2] || s_j==count_day) {
                                    th_item.text(s_j+'/'+stime[0]+'.'+stime[1]);
                                }
                                else{
                                    th_item.text(s_j);
                                }
                                th_item.appendTo(gantt_table_tr1);                                
                            }
                        }
                        else if (i==etime[1]) {
                            for(var e_j=1;e_j<=etime[2];e_j++){
                                var th_item = $("<th>");
                                if (e_j==etime[2] || e_j==1) {
                                    th_item.text(e_j + '/'+etime[0]+'.'+etime[1]);
                                }
                                else{
                                    th_item.text(e_j);
                                }
                                th_item.appendTo(gantt_table_tr1);
                            }
                        }
                        else{
                            for(var j=1;j<=count_day;j++){
                                var th_item = $("<th>");
                                if (j==1 || j==count_day) {
                                    th_item.text(j+'/'+stime[0]+'.'+i);
                                }
                                else{
                                    th_item.text(j);
                                }
                                th_item.appendTo(gantt_table_tr1);
                            }
                        }

                    }
                    break;
                    }
                    
                    break;


                    case 1:
                    for(var i=stime[1];i<=12;i++){
                        if(i<=7){
                            if(i%2!=0){
                                count_day = 31;
                            }
                            else if(i==2){
                                if(((stime[0]%4==0)&&(stime[0]%100!=0))||(stime[0]%400==0)){
                                    count_day = 29;
                                }
                                else{
                                    count_day = 28;
                                }
                            }
                            else{
                                count_day = 30;
                            }
                        }
                        else{
                            if(i%2==0){
                                count_day = 31;
                            }
                            else{
                                count_day = 30;
                            }
                        }
                        if(i==stime[1]){
                            D_count_day = count_day-stime[2]+1;
                            for(var s_j=stime[2];s_j<=count_day;s_j++){
                                var th_item = $("<th>");
                                if (s_j==stime[2] || s_j==count_day) {
                                    th_item.text(s_j+'/'+stime[0]+'.'+stime[1]);
                                }
                                else{
                                    th_item.text(s_j);
                                }
                                th_item.appendTo(gantt_table_tr1);                                
                            }
                        }
                        else{
                            for(var j=1;j<=count_day;j++){
                                var th_item = $("<th>");
                                if (j==1 || j==count_day) {
                                    th_item.text(j+'/'+stime[0]+'.'+i);
                                }
                                else{
                                    th_item.text(j);
                                }
                                th_item.appendTo(gantt_table_tr1);
                            }
                        }
                    }
                    for(var i=1;i<=etime[1];i++){
                        if(i<=7){
                            if(i%2!=0){
                                count_day = 31;
                            }
                            else if(i==2){
                                if(((etime[0]%4==0)&&(etime[0]%100!=0))||(etime[0]%400==0)){
                                    count_day = 29;
                                }
                                else{
                                    count_day = 28;
                                }
                            }
                            else{
                                count_day = 30;
                            }
                        }
                        else{
                            if(i%2==0){
                                count_day = 31;
                            }
                            else{
                                count_day = 30;
                            }
                        }
                        if (i==etime[1]) {
                            for(var e_j=1;e_j<=etime[2];e_j++){
                                var th_item = $("<th>");
                                if (e_j==etime[2] || e_j==1) {
                                    th_item.text(e_j + '/'+etime[0]+'.'+etime[1]);
                                }
                                else{
                                    th_item.text(e_j);
                                }
                                th_item.appendTo(gantt_table_tr1);
                            }
                        }
                        else{
                            for(var j=1;j<=count_day;j++){
                                var th_item = $("<th>");
                                if (j==1 || j==count_day) {
                                    th_item.text(j+'/'+etime[0]+'.'+i);
                                }
                                else{
                                    th_item.text(j);
                                }
                                th_item.appendTo(gantt_table_tr1);
                            }
                        }

                    }
                    break;


                    default :
                    for(var i=stime[1];i<=12;i++){
                        if(i<=7){
                            if(i%2!=0){
                                count_day = 31;
                            }
                            else if(i==2){
                                if(((stime[0]%4==0)&&(stime[0]%100!=0))||(stime[0]%400==0)){
                                    count_day = 29;
                                }
                                else{
                                    count_day = 28;
                                }
                            }
                            else{
                                count_day = 30;
                            }
                        }
                        else{
                            if(i%2==0){
                                count_day = 31;
                            }
                            else{
                                count_day = 30;
                            }
                        }
                        if(i==stime[1]){
                            D_count_day = count_day-stime[2]+1;
                            for(var s_j=stime[2];s_j<=count_day;s_j++){
                                var th_item = $("<th>");
                                if (s_j==stime[2] || s_j==count_day) {
                                    th_item.text(s_j+'/'+stime[0]+'.'+stime[1]);
                                }
                                else{
                                    th_item.text(s_j);
                                }
                                th_item.appendTo(gantt_table_tr1);                                
                            }
                        }
                        else{
                            for(var j=1;j<=count_day;j++){
                                var th_item = $("<th>");
                                if (j==1 || j==count_day) {
                                    th_item.text(j+'/'+stime[0]+'.'+i);
                                }
                                else{
                                    th_item.text(j);
                                }
                                th_item.appendTo(gantt_table_tr1);
                            }
                        }
                    }
                    for(var y=0;y<D_year-1;y++){
                        var realyear = stime[0]+y+1;
                        for(var i=1;i<=12;i++){
                        if(i<=7){
                            if(i%2!=0){
                                count_day = 31;
                            }
                            else if(i==2){
                                
                                if(((realyear%4==0)&&(realyear%100!=0))||(realyear%400==0)){
                                    count_day = 29;
                                }
                                else{
                                    count_day = 28;
                                }
                            }
                            else{
                                count_day = 30;
                            }
                        }
                        else{
                            if(i%2==0){
                                count_day = 31;
                            }
                            else{
                                count_day = 30;
                            }
                        }
                        for(var j=1;j<=count_day;j++){
                                var th_item = $("<th>");
                                if (j==1 || j==count_day) {
                                    th_item.text(j+'/'+realyear+'.'+i);
                                }
                                else{
                                    th_item.text(j);
                                }
                                th_item.appendTo(gantt_table_tr1);
                            }
                        
                        
                            
                        
                        }
                    }
                    for(var i=1;i<=etime[1];i++){
                        if(i<=7){
                            if(i%2!=0){
                                count_day = 31;
                            }
                            else if(i==2){
                                if(((etime[0]%4==0)&&(etime[0]%100!=0))||(etime[0]%400==0)){
                                    count_day = 29;
                                }
                                else{
                                    count_day = 28;
                                }
                            }
                            else{
                                count_day = 30;
                            }
                        }
                        else{
                            if(i%2==0){
                                count_day = 31;
                            }
                            else{
                                count_day = 30;
                            }
                        }
                        if (i==etime[1]) {
                            for(var e_j=1;e_j<=etime[2];e_j++){
                                var th_item = $("<th>");
                                if (e_j==etime[2] || e_j==1) {
                                    th_item.text(e_j + '/'+etime[0]+'.'+etime[1]);
                                }
                                else{
                                    th_item.text(e_j);
                                }
                                th_item.appendTo(gantt_table_tr1);
                            }
                        }
                        else{
                            for(var j=1;j<=count_day;j++){
                                var th_item = $("<th>");
                                if (j==1 || j==count_day) {
                                    th_item.text(j+'/'+stime[0]+'.'+i);
                                }
                                else{
                                    th_item.text(j);
                                }
                                th_item.appendTo(gantt_table_tr1);
                            }
                        }
                    }
                    break;


                }
                // gantt_table_tr1.appendChild(docFragment);
                
            })();


            //下面函数是生成各个阶段任务进度   
            (function(){
                var gantt_table = $("#gantt_table"),
                    // count_date_th = gantt_table_tr1_again.length-5,
                    docFragment1 = document.createDocumentFragment(),
                    
                    docFragment3 = document.createDocumentFragment();
                    // console.log(gantt_table_tr1_again.length);
                for(var i=that.data.length-1;i>=0;i--){
                    
                    var table_tr_item = $('<tr>');
                   
                    for (var propName in that.data[i]){
                        var table_td_item = $('<td>');
                        if(propName=='stime' || propName=='etime'){
                            var date_str = that.data[i][propName][0]+"-"+that.data[i][propName][1]+"-"+that.data[i][propName][2];
                            table_td_item.text(date_str);
                        }
                        else{table_td_item.text(that.data[i][propName]);}
                        table_td_item.appendTo(table_tr_item);
                    }
                    
                        
                    //下面是生成每个任务的初始与结束时间间隔 
                    var each_task_s = [],
                        each_task_e = [];
                    for(var p = 0;p<3;p++){
                        each_task_s[p] = parseInt(that.data[i].stime[p]);
                        each_task_e[p] = parseInt(that.data[i].etime[p]);
                    }   
                    var D_count_task_day = 0;
                        D_each_task_y = each_task_e[0]-each_task_s[0],
                        D_each_task_m = each_task_e[1]-each_task_s[1];
                    // console.log(D_each_task_y);
                    switch(D_each_task_y){
                        case 0:
                        switch(D_each_task_m){
                            case 0:
                            D_count_task_day = each_task_e[2]-each_task_s[2]+1;
                            break;
                            default:
                            if(((each_task_s[0]%4==0)&&(each_task_s[0]%100!=0))||(each_task_s[0]%400==0)){var year_count = 29;}else{var year_count = 28;}
                            for(var m=each_task_s[1];m<=each_task_e[1];m++){
                                if(m==each_task_s[1]){
                                    if ((m<=7&&m%2!=0)||(m>7&&m%2==0)) {
                                        D_count_task_day += 31-each_task_s[2]+1;
                                    }
                                    else if(m==2){
                                        
                                        D_count_task_day += year_count-each_task_s[2]+1;
                                    }
                                    else{
                                        D_count_task_day += 30-each_task_s[2]+1;
                                    }
                                }
                                else if(m==each_task_e[1]){
                                    D_count_task_day += each_task_e[2];
                                }
                                else{
                                    if ((m<=7&&m%2!=0)||(m>7&&m%2==0)) {
                                        D_count_task_day += 31
                                    }
                                    else if(m==2){
                                        D_count_task_day += year_count
                                    }
                                    else{
                                        D_count_task_day += 30
                                    }
                                }
                            }
                            break;
                        }
                        break;
                        default:
                        if(((each_task_s[0]%4==0)&&(each_task_s[0]%100!=0))||(each_task_s[0]%400==0)){var year_count = 29;}else{var year_count = 28;}
                        for(var m=each_task_s[1];m<=12;m++){
                                if(m==each_task_s[1]){
                                    if ((m<=7&&m%2!=0)||(m>7&&m%2==0)) {
                                        D_count_task_day += 31-each_task_s[2]+1;
                                    }
                                    else if(m==2){
                                        D_count_task_day += year_count-each_task_s[2]+1;
                                    }
                                    else{
                                        D_count_task_day += 30-each_task_s[2]+1;
                                    }
                                }
                                else{
                                    if ((m<=7&&m%2!=0)||(m>7&&m%2==0)) {
                                        D_count_task_day += 31
                                    }
                                    else if(m==2){
                                        D_count_task_day += year_count
                                    }
                                    else{
                                        D_count_task_day += 30
                                    }
                                }
                        }
                        if(((each_task_e[0]%4==0)&&(each_task_e[0]%100!=0))||(each_task_e[0]%400==0)){var year_count_e = 29;}else{var year_count_e = 28;}
                        for(var m=1;m<=each_task_e[1];m++){
                                if(m==each_task_e[1]){
                                    D_count_task_day += each_task_e[2];
                                }
                                else{
                                    if ((m<=7&&m%2!=0)||(m>7&&m%2==0)) {
                                        D_count_task_day += 31
                                    }
                                    else if(m==2){
                                        D_count_task_day += year_count_e
                                    }
                                    else{
                                        D_count_task_day += 30
                                    }
                                }
                        }

                        for(var yy=0;yy<D_each_task_y-1;yy++){
                                var ano_real_year = each_task_s[0]+yy+1;
                                if(((ano_real_year%4==0)&&(ano_real_year%100!=0))||(ano_real_year%400==0)){var year_count_yy = 29;}else{var year_count_yy = 28;}
                                for(var m=1;m<=12;m++){
                                    if ((m<=7&&m%2!=0)||(m>7&&m%2==0)) {
                                        D_count_task_day += 31-each_task_s[2]+1;
                                    }
                                    else if(m==2){
                                        D_count_task_day += year_count_yy-each_task_s[2]+1;
                                    }
                                    else{
                                        D_count_task_day += 30-each_task_s[2]+1;
                                    }
                                }
                        }
                        break;
                    }
                    // console.log(D_count_task_day);


                    // //下面生成与初始时间的间隔
                    // console.log(each_task_s,stime);    
                    var DD_count_task_day = 0,
                        DD_each_task_y = each_task_s[0]-stime[0],
                        DD_each_task_m = each_task_s[1]-stime[1];

                    switch(DD_each_task_y){
                        case 0:
                        switch(DD_each_task_m){
                            case 0:
                            DD_count_task_day = each_task_s[2]-stime[2]+1;
                            break;
                            default:
                            if(((stime[0]%4==0)&&(stime[0]%100!=0))||(stime[0]%400==0)){var year_count_B = 29;}else{var year_count_B = 28;}
                            for(var m=stime[1];m<=each_task_s[1];m++){

                                if(m==stime[1]){
                                    if ((m<=7&&m%2!=0)||(m>7&&m%2==0)) {
                                        DD_count_task_day += 31-stime[2]+1;
                                    }
                                    else if(m==2){
                                        DD_count_task_day += year_count_B-stime[2]+1;
                                    }
                                    else{
                                        DD_count_task_day += 30-stime[2]+1;
                                    }
                                }
                                else if(m==each_task_s[1]){
                                    DD_count_task_day += each_task_s[2];
                                }
                                else{
                                    if ((m<=7&&m%2!=0)||(m>7&&m%2==0)) {
                                        DD_count_task_day += 31
                                    }
                                    else if(m==2){
                                        DD_count_task_day += year_count_B
                                    }
                                    else{
                                        DD_count_task_day += 30
                                    }
                                }
                            }
                            break;
                        }
                        break;
                        default:
                        if(((stime[0]%4==0)&&(stime[0]%100!=0))||(stime[0]%400==0)){var year_count_B = 29;}else{var year_count_B = 28;}
                        for(var m=stime[1];m<=12;m++){
                                if(m==stime[1]){
                                    if ((m<=7&&m%2!=0)||(m>7&&m%2==0)) {
                                        DD_count_task_day += 31-stime[2]+1;
                                    }
                                    else if(m==2){
                                        DD_count_task_day += year_count_B-stime[2]+1;
                                    }
                                    else{
                                        DD_count_task_day += 30-stime[2]+1;
                                    }
                                }
                                else{
                                    if ((m<=7&&m%2!=0)||(m>7&&m%2==0)) {
                                        DD_count_task_day += 31
                                    }
                                    else if(m==2){
                                        DD_count_task_day += year_count_B
                                    }
                                    else{
                                        DD_count_task_day += 30
                                    }
                                }
                        }
                        if(((each_task_s[0]%4==0)&&(each_task_s[0]%100!=0))||(each_task_s[0]%400==0)){var year_count_C = 29;}else{var year_count_C = 28;}
                        for(var m=1;m<=each_task_s[1];m++){
                                if(m==each_task_s[1]){
                                    DD_count_task_day += each_task_s[2];
                                }
                                else{
                                    if ((m<=7&&m%2!=0)||(m>7&&m%2==0)) {
                                        DD_count_task_day += 31
                                    }
                                    else if(m==2){
                                        DD_count_task_day += year_count_C
                                    }
                                    else{
                                        DD_count_task_day += 30
                                    }
                                }
                        }
                        for(var yy=0;yy<DD_each_task_y-1;yy++){
                                var realyear_B = stime[0]+yy+1;
                                if(((realyear_B%4==0)&&(realyear_B%100!=0))||(realyear_B%400==0)){var year_count_D = 29;}else{var year_count_D = 28;}
                                for(var m=1;m<=12;m++){
                                    if ((m<=7&&m%2!=0)||(m>7&&m%2==0)) {
                                        DD_count_task_day += 31-stime[2]+1;
                                    }
                                    else if(m==2){
                                        DD_count_task_day += year_count_D-each_task_s[2]+1;
                                    }
                                    else{
                                        DD_count_task_day += 30-each_task_s[2]+1;
                                    }
                                }
                        }
                        break;
                    }
                    // console.log(DD_count_task_day);
 


                    //下面是对应上面两个count输出td
                    // console.log(D_count_task_day);
                    for(var n=1;n<=DD_count_task_day-1;n++){
                        var table_td_item = $('<td>');
                        table_td_item.appendTo(table_tr_item);
                    }
                    // var table_td_item = document.createElement('td');
                    // table_tr_item.appendChild(table_td_item);
                    var table_td_item = $('<td>');
                    table_td_item.attr("colspan",D_count_task_day);
                    table_td_item.addClass('danger');
                    



                    var task_js_sdate = new Date(each_task_s[0],each_task_s[1]-1,each_task_s[2]);
                    
                    var nowdate = new Date();  //当前时间
                    nowdate = new Date(nowdate.getFullYear(),nowdate.getMonth(),nowdate.getDate());
                    console.log(task_js_sdate);
                    var task_js_edate = new Date(each_task_e[0],each_task_e[1]-1,each_task_e[2]);
                    console.log(task_js_edate,that.data[i].etime);
                    var D_now_date = nowdate-task_js_sdate;
                    var D_schedule_date = task_js_edate-task_js_sdate;
                    var N_now_date = Math.round(D_now_date/86400000);
                    // console.log(N_now_date);
                    var Per_now_date = Math.round((D_now_date/D_schedule_date)*100); 

                    var gannt_show_message = '时间已到期';
                    // console.log(nowdate,task_js_sdate,task_js_edate);
                    if(nowdate>=task_js_edate){
                        table_td_item.html("<a tabindex=\"0\"  role=\"button\" data-toggle=\"popover\" data-placement=\"top\"data-trigger=\"focus\" title=\""+that.data[i].task+"\" data-content=\""+gannt_show_message+"\"><div class='progress' title ='"+gannt_show_message+"' style='cursor:pointer;cursor:hand'><div class='progress-bar progress-bar-danger progress-bar-striped active progress_move' role='progressbar' aria-valuenow='60' aria-valuemin='0' aria-valuemax='100' style='width: 2em;min-width: 2em;'>100%</div></div></a>");
                        

                    }
                    else{
                        if(D_now_date >0){

                            if(10*D_now_date < 7*D_schedule_date){
                            gannt_show_message = ',还未过计划时间的70%';
                            table_td_item.html("<a tabindex=\"0\"  role=\"button\" data-toggle=\"popover\" data-placement=\"top\"data-trigger=\"focus\" title=\""+that.data[i].task+"\" data-content=\"当前处在计划时间的第"+N_now_date+"天"+gannt_show_message+"\"><div class='progress'title ='当前处在计划时间的第"+N_now_date+"天"+gannt_show_message+"'style='cursor:pointer;cursor:hand'><div class='progress'title ='当前处在计划时间的第"+N_now_date+"天"+gannt_show_message+"'style='cursor:pointer;cursor:hand'><div class='progress-bar progress-bar-info progress-bar-striped active progress_move' role='progressbar' aria-valuenow='60' aria-valuemin='0' aria-valuemax='100' style='width: 2em;min-width: 2em'>"+Per_now_date+"%</div></div></a>");
                            }
                            else{
                                gannt_show_message = ",要抓紧了";
                                table_td_item.html("<a tabindex=\"0\"  role=\"button\" data-toggle=\"popover\" data-placement=\"top\"data-trigger=\"focus\" title=\""+that.data[i].task+"\" data-content=\"当前处在计划时间的第"+N_now_date+"天"+gannt_show_message+"\"><div class='progress'title ='当前处在计划时间的第"+N_now_date+"天"+gannt_show_message+"'style='cursor:pointer;cursor:hand'><div class='progress-bar progress-bar-warning progress-bar-striped active progress_move' role='progressbar' aria-valuenow='60' aria-valuemin='0' aria-valuemax='100' style='width: 2em;min-width: 2em'>"+Per_now_date+"%</div></div></a>");
                                // console.log('当前时间进度超过计划时段的70%，抓紧了');
                            }
                        }
                        else{
                            N_now_date = 0;
                            // console.log(D_now_date);
                            
                            gannt_show_message = ",未到开始时间";
                            table_td_item.html("<a tabindex=\"0\"  role=\"button\" data-toggle=\"popover\" data-placement=\"top\"data-trigger=\"focus\" title=\""+that.data[i].task+"\" data-content=\"当前处在计划时间的第"+N_now_date+"天"+gannt_show_message+"\"><div class='progress'title ='当前处在计划时间的第"+N_now_date+"天"+gannt_show_message+"'style='cursor:pointer;cursor:hand'><div class='progress' title ='当前处在计划时间的第"+N_now_date+"天"+gannt_show_message+"'style='cursor:pointer;cursor:hand'><div class='progress-bar progress-bar-info progress-bar-striped' role='progressbar' aria-valuenow='60' aria-valuemin='0' aria-valuemax='100' style='width: 0%;min-width: 2em;'>0%</div></div></a>");
                            // console.log('任务计划还未到进行时间');
                        }
                    }


                    
                    

                    
                     // console.log(table_td_item.getAttribute("colspan"));
                    table_td_item.appendTo(table_tr_item);
                     // console.log(table_tr_item);


                    var all_date_tr_count = gantt_table_tr1.find("th").length-5;
                    // console.log(all_date_tr_count);
                    for(var left_date_td=1;left_date_td<=all_date_tr_count-D_count_task_day-DD_count_task_day+1;left_date_td++){
                        var table_td_item = $("<td>");
                        table_td_item.appendTo(table_tr_item);
                    }
                    

                    // console.log(table_tr_item);
                     $("#gantt_table_tr1").after(table_tr_item);

                     // console.log(N_now_date);


                }

    
            })();

        }
    }

 


    var gantt = new gantt();
    gantt.init();