window.onload = functions()
{
    /* setup event listeners for add to cart buttons */
    let elems = document.getElementsByClassName("addtocart_btn"); //​get all add to cart buttons
    for (let i = 0; i < elems.length; i++) {
        elems[i].addEventListener('click', AddToCartClick);    //addEventListener upon clicking "Add To Cart"
    }
}

function AddToCartClick(event) {
    let target = event.currentTarget; //get target of current clicked button

    let xhr = new XMLHttpRequest();

    xhr.open("Post", "/Gallery/AddToCart"); //Direct this AJAX request to GalleryController, AddToCart action method
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");

    xhr.onreadystatechange = function () { //upon receiving AJAX response, do following
        if (this.readyState === XMLHttpRequest.DONE) {
            if (this.status !== 200)
                return; // do nothing if not 200 OK

            let response = JSON.parse(this.responseText); //unpack JSON object
            let currCount = response.cartCounter; //Read the new cartCounter
            UpdateCartCounter(currCount); //call the function to update Cart Counter
        }
    };

    let addProduct = {
        "ProductId": target.id  //assign ProductID in a JS object
    };
    xhr.send(JSON.stringify(addProduct)); //Convert JS object to JSON and send via AJAX
}


function UpdateCartCounter(cartCount) {
    let elem = document.getElementById("cartCounter"); //get the counter element from HTML
    elem.value = cartCount; //change the value of the cart Counter
}