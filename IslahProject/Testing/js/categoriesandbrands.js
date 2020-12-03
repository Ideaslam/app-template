


function getAllCategories() {
	var allCategories =[];
	callWebServise("Categories.asmx", "getAll", {}, function(categories){
		categories.forEach(function(category){
			var data = {c_name: category.c_name};
			callWebServise("Brands.asmx", "getBrandsByCat", data, function(brands){
				var cat ={
					c_id : category.c_id,
					c_name: category.c_name,
					c_brands:brands
				};
				allCategories.push(cat);
			});
			
	});

console.log(categories);
		console.log(allCategories);
		//displayCategories(categories);
	});
}


function displayCategories(categories){
	

}