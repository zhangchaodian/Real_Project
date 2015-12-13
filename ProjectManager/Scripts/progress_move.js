// 进度过渡效果

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