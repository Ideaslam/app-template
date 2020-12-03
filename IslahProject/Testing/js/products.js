function getProductsByCat(cat)
{

var data ={cat_name :cat};

callWebService("Products.asmx","getProductsByCat",data,function(products){
displayProducs(products);




} )
}
function setSingleProduct(id)
{
localStorage.setItem('id' ,id) ;

}


function getSingleProduct()
{
 
var data={id :localStorage.getItem('id')}
callWebService("Products.asmx" ,"getOne" ,data,function(singleProduct){
displaySingleProduct(singleProduct);
console.log(singleProduct);
});
}



function displayProducs(products) 
{
	var html =`` ;
	html+=`<div class="row rowedit">`;

for(var i =0 ;i<products.length ; i++)
{
	var images =JSON.parse(products[i].p_images);  
	html+=`<div class="col-md-3 product">
		<h2>${products[i].p_name}</h2>
		<img src="../admin/images/${images[0]}" alt="">
		<button class="btn btn-default "><span class="glyphicon glyphicon-shopping-cart">Cart</span></button>
		<a href=singleProduct.html class="btn btn-success pull-right" onclick="setSingleProduct(${products[i].p_id})">Details</a>
	</div>`;
 
}
html+=`</div>`;



$('#product').html(html) ;

}

function displaySingleProduct(product)
{
	var images =JSON.parse(product[0].p_images);
   var html='';
	html+=
	`<div class="row">
		<div class="col-md-5 product-images">
			<div id="carouselProduct" class="carousel slide" data-ride="carousel">
			<ol class="carousel-indicators">`;
				for(var i=0; i<images.length; i++){
					html+=`<li data-target="#carouselProduct" data-slide-to="${i}"></li>`;
				}
	html+=		`</ol>
			<div class="carousel-inner">`;
				for(var i=0; i<images.length; i++){
					html+=i==0? 
					`<div class="item active">
						<img src="../admin/images/${images[i]}">
					</div>`:
					`<div class="item">
						<img src="../admin/images/${images[i]}">
					</div>`;
				}
	html+=		`</div>
			<a href="#carouselProduct" data-slide="prev" class="left carousel-control">
				<span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
			</a>
			<a href="#carouselProduct" data-slide="next" class="right carousel-control">
				<span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
			</a>
		</div>
		</div>
		<div class="col-md-7 product-details">
			<h2>${product[0].p_name}</h2>
			<p><b>Brand: </b>${product[0].b_name}</p>
			<p><b>Price: </b> ${product[0].p_price}</p>
			<p><b>Pieces Avilable :</b> <b>${product[0].p_quentity}</b></p>
			<p><b>Description: </b> ${product[0].p_desc}</p>
			<button class="btn btn-success " style="margin-left: 70px; margin-top: 30px;"><span class="glyphicon glyphicon-shopping-cart" 
			onclick=addCart(${product[0].p_id},${product[0].p_name},${product[0].p_price});>Cart</span></button>
		</div>
		<div class="row" style="clear: both; margin-left: 10px;">
			<div class="col-md-4">
			<h2>Specification:</h2>
			<table class="table table-bordered table-striped">`;

			var spec =JSON.parse(product[0].p_spec);

			for(var i=0 ; i<spec.length ; i++ )
			{

				html+=`<tr>

					<td>${spec[i].key}</td>
					<td>${spec[i].value}</td>
				</tr>`;
			}
			
		html+=`	</table>
		</div>

		<div class="col-md-4">
			<h2>Key Features:</h2>
			<table class="table table-bordered table-striped">`;

	        var kf =JSON.parse(product[0].p_kf);

			for(var i=0 ; i<kf.length ; i++ )
			{

				html+=`<tr>
					<td>${kf[i].key}</td>
					<td>${kf[i].value}</td>
				</tr>`;
			}

				
			html+=`</table>
		</div>
		</div>
	</div>`;


	$('#singleproduct').html(html);
}



function addCart()
{


}
	


