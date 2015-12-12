(function() {
	var form_valid = {
		firststep_status: new Array(),
        secondstep_status: new Array(),
        project_time: new Array(),
		init: function(){
			this.firststep_each_focusout();
			this.click_first_next_button();
			this.level_change();
			this.project_time_change();
			this.file_input_change();
			this.click_second_next_button();
			this.click_submit();
		},
		match_str: function(reg,str){
			if(reg.test(str)){
					return true;
				}
				else{
					return false;
				}
		},

		show_info: function(str_bool,instance){
				if(str_bool==true){
					$(instance).parents(".Check_div").find("p").last().fadeIn("fast");
					$(instance).parents(".Check_div").find("p").first().css("display","none");
				}
				else{
					$(instance).parents(".Check_div").find("p").first().fadeIn("fast");
					$(instance).parents(".Check_div").find("p").last().css("display","none");

				}
	     },

	     firststep_each_focusout: function(){
	     	// 定义的函数里面的this是包着它的对象
            var that = this; 
	     	$("#project_name").focusout(function(){
				var value  = $(this).val();
				if(value=='') {
					that.firststep_status['project_name'] = false;
					that.show_info(false,this);
				}
				else{
					that.firststep_status['project_name'] = true;
					$(this).value = value;
					that.firststep_True_num++;
					// console.log(that.firststep_True_num);
					that.show_info(true,this);
				}
			});
			$("#mainer_name").focusout(function(){
				var value  = $(this).val();
				// console.log(value);
				if(value=='') {
					that.firststep_status['mainer_name'] = false;
					that.show_info(false,this);
				}
				else{
					that.firststep_status['mainer_name'] = true;
					that.firststep_True_num++;
					that.show_info(true,this);
				}
				$("#Auto_Mainer_Name").val(value);
			});
			$("#mainer_phone").focusout(function(){
				var value  = $(this).val();
				var reg = /^1[358][0-9]{8}[0-9]$/;
				if(!that.match_str(reg,value)) {
					that.firststep_status['mainer_phone'] = false;
					that.show_info(false,this);
				}
				else{
					that.firststep_status['mainer_phone'] = true;
					that.firststep_True_num++;
					that.show_info(true,this);
				}
			});
			$("#mainer_email").focusout(function(){
				var value  = $(this).val();
				var reg = /^([a-zA-Z0-9]+)@[a-zA-Z0-9]+\.([a-zA-Z0-9]+)$/;
				if(!that.match_str(reg,value)) {
					that.firststep_status['mainer_email'] = false;
					that.show_info(false,this);
				}
				else{
					that.firststep_status['mainer_email'] = true;
					that.firststep_True_num++;
					that.show_info(true,this);
				}
			});
			$("#mainer_ID_card").focusout(function(){
				var value  = $(this).val();
				var reg = /^[0-9][0-9]{16}[0-9]$/;
				if(!that.match_str(reg,value)) {
					that.firststep_status['mainer_ID_card'] = false;
					that.show_info(false,this);
				}
				else{
					that.firststep_status['mainer_ID_card'] = true;
					that.firststep_True_num++;
					that.show_info(true,this);
				}

			});
			$("#project_sort").focusout(function(){
				var value  = $(this).val();
				if(value=='') {
					that.firststep_status['project_sort'] = false;
					that.show_info(false,this);
				}
				else{
					that.firststep_status['project_sort'] = true;
					that.firststep_True_num++;
					that.show_info(true,this);
				}
			});
			$("#allmember_container").on("focusout",".member_name",function(){
				    var index = $(this).parents(".Check_div").index();//返回索引值，很重要
				    // console.log(index);
					var value  = $(this).val();
					if(value=='') {
						that.firststep_status['member_name'+index] = false;
						that.show_info(false,this);
					}
					else{
						that.firststep_status['member_name'+index] = true;
						that.firststep_True_num++;
						that.show_info(true,this);
					}
					$(this).parents(".Check_div").find("p").first().next().css("display","none");
			});
			$("#project_money").focusout(function(){
				var value  = $(this).val();
				var reg = /^[1-9][0-9]*$/;
				if(!that.match_str(reg,value)) {
					that.firststep_status['project_money'] = false;
					that.show_info(false,this);
				}
				else{
					that.firststep_status['project_money'] = true;
					that.firststep_True_num++;
					that.show_info(true,this);
				}
			});

	     },

	     level_change: function(){
	     	var that = this;
	     	that.firststep_status['Target_Level'] = true;
	     	that.firststep_status['Now_Level'] = true;
	     	$("#Target_Level").change(function(){
	     		var target_level = parseInt($(this).val());
	     		var now_level= parseInt($("#Now_Level").val());
	     		// console.log(now_level,target_level);
	     		if(now_level>=target_level){
	     			that.show_info(false,this);
	     			that.show_info(false,"#Now_Level");
	     			that.firststep_status['Target_Level'] = false;
	     			that.firststep_status['Now_Level'] = false;
	     		}
	     		else{
	     			that.show_info(true,this);
	     			that.show_info(true,"#Now_Level");
	     			that.firststep_status['Target_Level'] = true;
	     			that.firststep_status['Now_Level'] = true;
	     		}
	     	});
	     	$("#Now_Level").change(function(){
	     		var now_level = parseInt($(this).val());
	     		var target_level= parseInt($("#Target_Level").val());
	     		// console.log(now_level,target_level);
	     		if(now_level>=target_level){
	     			that.show_info(false,this);
	     			that.show_info(false,"#Target_Level");
	     			that.firststep_status['Target_Level'] = false;
	     			that.firststep_status['Now_Level'] = false;
	     		}
	     		else{
	     			that.show_info(true,this);
	     			that.show_info(true,"#Target_Level");
	     			that.firststep_status['Target_Level'] = true;
	     			that.firststep_status['Now_Level'] = true;
	     		}
	     	});
	     },

	     project_time_change: function(){
	     	var that = this;
	     	var default_stime = new Date();
	     	var default_stime_month = default_stime.getMonth()+1;
	     	var default_etime_year  = default_stime.getFullYear()+1;
	     	$("#project_start_time").val(default_stime.getFullYear()+"-"+default_stime_month+"-"+default_stime.getDate());
	     	$("#project_end_time").val(default_etime_year+"-01"+"-01");
            that.firststep_status['project_end_time'] = true;
	     	that.firststep_status['project_start_time'] = true;
	     	var default_etime = new Date(default_etime_year,0,1);
	     	 that.project_time['stime'] = default_stime;
	     	 that.project_time['etime'] = default_etime;
	     	$("#project_start_time").change(function(){
	     		var start_time = $(this).val();
	     		// var regEx = new RegExp("\\-","gi"),
	     		start_time = that.RegReplacy("-",start_time,"/");
	     		// console.log(start_time);
	     		start_time = new Date(Date.parse(start_time));
	     		var end_time = $("#project_end_time").val();
	     		end_time = that.RegReplacy("-",end_time,"/");
	     		end_time = new Date(Date.parse(end_time));
	     		if(start_time>=end_time){
	     			that.show_info(false,this);
	     			that.show_info(false,"#project_end_time");
	     			that.firststep_status['project_end_time'] = false;
	     			that.firststep_status['project_start_time'] = false;
	     		}
	     		else
	     		{
	     			that.show_info(true,this);
	     			that.show_info(true,"#project_end_time");
	     			that.firststep_status['project_end_time'] = true;
	     			that.firststep_status['project_start_time'] = true;
	     			that.project_time['stime'] = start_time;
	     			that.project_time['etime'] = end_time;

	     		}
	     	});
	     	$("#project_end_time").change(function(){
	     		var end_time = $(this).val();
	     		// var regEx = new RegExp("\\-","gi");
	     		end_time = that.RegReplacy("-",end_time,"/");
	     		// console.log(start_time);
	     		end_time = new Date(Date.parse(end_time));
	     		var start_time = $("#project_start_time").val();
	     		start_time = RegReplacy("-",start_time,"/");
	     		start_time = new Date(Date.parse(start_time));
	     		if(start_time>=end_time){
	     			that.show_info(false,this);
	     			that.show_info(false,"#project_start_time");
	     			that.firststep_status['project_end_time'] = false;
	     			that.firststep_status['project_start_time'] = false;
	     		}
	     		else
	     		{
	     			that.show_info(true,this);
	     			that.show_info(true,"#project_start_time");
	     			that.firststep_status['project_end_time'] = true;
	     			that.firststep_status['project_start_time'] = true;
	     			that.project_time['stime'] = start_time;
	     			that.project_time['etime'] = end_time;
	     		}
	     	});
	     },

	     click_first_next_button: function(){
	     	var that = this;
	     	$("#first_next_button").click(function(){
				var firststep_Ntrue_num= $(".Check_div").size();
				// console.log(firststep_Ntrue_num,that.firststep_True_num);
				var num = 0;
				for(var index in that.firststep_status){
					if(that.firststep_status[index]==true){
						num++;
					}
				}
				if(num==firststep_Ntrue_num){
					$("#form_container_first").slideUp("normal",function(){
		        		$("#form_container_second").slideDown("fast");
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
					        startDate:new Date(that.project_time['stime'].getFullYear(),that.project_time['stime'].getMonth(),that.project_time['stime'].getDate()),
					        endDate:new Date(that.project_time['etime'].getFullYear(),that.project_time['etime'].getMonth(),that.project_time['etime'].getDate()), 
					    });
		        	});
				}
				else{
					alert("您上面有缺漏未填或者填写不正确的信息!");
				}
	        	
	        });
	     },

	     getFileName: function(str){
	     	var attr = str.split("\.");
	     	return attr[attr.length-1];
	     },
	     RegReplacy: function(Ori,str,Target){
	     	var regEx = new RegExp("\\"+Ori,"gi");
	     	return str.replace(regEx,Target);
	     },
	     file_input_change: function(){
	     	var that = this;
	     	$(".file1").each(function(){
	     		$(this).change(function(){
	     			var str = $(this).val();
	     			if(that.getFileName(str)=="doc"||that.getFileName(str)=="docx"){
	     				that.secondstep_status[$(this).attr("id")] = true;
	     			}
	     			else{
	     				that.secondstep_status[$(this).attr("id")] = false;
	     			}
	     		});
	     	});
	     	$(".file2").change(function(){
	     			var str = $(this).val();
	     			// console.log(that.getFileName(str));
	     			if(that.getFileName(str)=="zip"||that.getFileName(str)=="rar"){
	     				that.secondstep_status[$(this).attr("id")] = true;
	     			}
	     			else{
	     				that.secondstep_status[$(this).attr("id")] = false;
	     			}
	     	});
	     },
	     click_second_next_button: function(){
	     	var that = this;
	     	$("#second_next_button").click(function(){
	     		var sum = 0;
	     		for(var index in that.secondstep_status){
	     			if(that.secondstep_status[index]==true){
	     				sum++;
	     			}
	     		}
	     		if(sum==3){
	     			$("#form_container_second").fadeOut("normal",function(){
		        		$("#form_container_third").slideDown("fast");
		        	});
	     		}
	     		else{
	     			alert("您上面的文件有未选或者格式不对的地方!");
	     		}
	        });
	     },


	     click_submit: function(){
	     	var that = this;
	     	$("#submit").click(function(){
				var check_empty = true;
				var check_time  = true;
		     	$(".task_is_empty").each(function(){
		     		if($(this).val()==''){
		     			check_empty = false;
		     		}
		     	});
		     	// console.log($("#form_container_third > .task_is_time"));
		     	$("#form_container_third .task_is_time").each(function(){
		     		var stime = $(this).find(".task_is_stime").val();
		     		var etime = $(this).find(".task_is_etime").val();
		     		stime  = new Date(Date.parse(that.RegReplacy("-",stime,"/")));
		     		etime  = new Date(Date.parse(that.RegReplacy("-",etime,"/")));
		     		// console.log(stime,etime);
		     		if(stime>=etime){
		     			check_time = false;
		     		}
		     	});
		        if(check_empty==false){
		        	alert("您第三步有未填录的信息");
		        }
		        else{
		        	if(check_time==false){
		        		alert("每个阶段任务的起始时间要小于结束时间");
		        	}
		        }
	     	});
	     	
	     },


	};
	form_valid.init();
})();