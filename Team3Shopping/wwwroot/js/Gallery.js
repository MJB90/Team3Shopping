

window.onload = function () {
    /* setup event listeners for tasks selection */

    let elems = document.getElementsByClassName("addtocart_btn"); //all task box elements
    for (let i = 0; i < elems.length; i++) {
        elems[i].addEventListener('click', AddToCartClick);    //addListener after DOM is ready
    }

}


function AddToCartClick(event) {

    let target = event.currentTarget; //get target of current clicked button

    let xhr = new XMLHttpRequest();

    xhr.open("POST", "/Gallery/AddToCart"); //Direct this AJAX request to GalleryController, AddToCart action method
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
        "Id": target.id  //assign ProductID in a JS object
    };
    xhr.send(JSON.stringify(addProduct)); //Convert JS object to JSON and send via AJAX
}


function UpdateCartCounter(cartCount) {
    let elem = document.getElementById("cartCounter"); //get the counter element from HTML
    elem.innerHTML = cartCount; //change the value of the cart Counter
}