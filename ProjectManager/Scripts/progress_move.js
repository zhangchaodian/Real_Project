// 进度过渡效果

(function(){
	$(".progress_move").each(function(){
            var that = this;
            var value = parseInt($(this).text().trim());
            var num = 0;
            // console.log(value);
            
            num_add();
            var interval = setInterval(num_add,3);
            function num_add(){
                if(num==value){
                    clearInterval(interval);
                }
                else{
                    num++;
                    $(that).text(num+"%");
                }            
            }
            $(this).animate({
                width : value+"%",
            },
                4*value);
 });

})();