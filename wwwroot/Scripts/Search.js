$(document).ready(function () {

	//$("#search").keyup(function() {		
	//	var data = $(this).val();
	//	$.ajax({
	//		url: "/Product/Search",
	//		type: "Get",
	//		data: { searchText: data },
	//		success: function(cevap) { // gelen cevap function'a parametre ile gönderilir...parametre isminin ne olduğunun hiç bir önemi yoktur...
	//                     $("#searchResult").html(ceavp);
	//		}
	//	});
	//});

	$("#search").keyup(function () {

		var data = $(this).val();

		$.ajax({
			url: "/Product/SearchV2",
			type: "Get",
			data: { searchText: data },
			success: function (response) {

				var dat = response;
				console.log(dat.length);

				var resultHtml = "<ul class='list-group'>";
				for (var i = 0; i < dat.length; i++) {

					var obj = dat[i]; // tek bir obj olarak al...
					console.log(obj);
					//console.log(dat[i].productName +" "+ dat[i].unitPrice);
					resultHtml += "<li class='list-group-item'>";
					resultHtml += "<a href='/Product/Detay?ProductId" + obj.ProductId + "'>" + obj.ProductName + "</a>";
					resultHtml += "</li>"
				}
				resultHtml += "</ul>";
				$("#searchResult").html(resultHtml);
				//console.log(response);
				//alert(response);
			}
		});

	});
});