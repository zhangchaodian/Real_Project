$(function(){
	$(".upload").click(function(){

		
		
		 var id = $(this).attr("id");
		
		 $("#report").attr("href","/Admin/DownloadFile/"+id+"/report_file");
		 $("#paper").attr("href","/Admin/DownloadFile/"+id+"/paper_file");
		 $("#whole").attr("href","/Admin/DownloadFile/"+id+"/whole_pack_file");
	
	});

});