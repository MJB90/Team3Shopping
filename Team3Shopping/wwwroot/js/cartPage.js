$('.like-btn').on('click', function () {
    $(this).toggleClass('is-active');
});

$(document).ready(function () {

    $('.radio-group .radio').click(function () {
        $('.radio').addClass('gray');
        $(this).removeClass('gray');
    });
});

//Event Listener
window.onload = function () {
    let elems = document.getElementsByClassName("remove");
    for (let i = 0; i < elems.length; i++) {
        elems[i].addEventListener('click', RemoveProduct);
    }
    let elemsNum = document.getElementsByClassName("productQuantity");
    for (let i = 0; i < elemsNum.length; i++) {
        elemsNum[i].addEventListener('change', ChangeQuantity);
    }
    let elemCheckOut = document.getElementById("checkout")
    elemCheckOut.addEventListener('click', CheckOut);
}

//Remove the product from the cart, update the database, and update the price
function RemoveProduct(event) {
    let target = event.currentTarget;
    checkIsProductDeleted(target.id, target.className.substring(6));
    target.parentElement.parentElement.remove();

    updateTotalPrice();
}

//Increment or decrement the database quantity and update the price
function ChangeQuantity(event) {
    let targetNum = event.currentTarget;
    let number = targetNum.value;
    let user = targetNum.className.substring(16);
    let product = targetNum.id;
    ChangeQuantityInDB(user, product, number);
    if (number == 0) {
        targetNum.parentElement.parentElement.remove();
    }
    updateTotalPrice();
}

//Use ajax to pass UserId and ProductId to the Remove Action.
function checkIsProductDeleted(userId, productId) {
    $.ajax({
        type: "POST",
        url: "/Cart/Remove",
        data: {
            UserId: userId,
            ProductId: productId,
        },
        dataType: "json",
        success: function (data) {
            alert("Product has been removed from the cart!")
            UpdateCartCounter(data.cartCount);
        },
        error: function () {
            alert("There is something wrong!")
        }
    })
};

//Use ajax to pass userId, productId, and productQuantity to the EditQuantity action. 
function ChangeQuantityInDB(userId, productId, productQuantity) {
    $.ajax({
        type: "POST",
        url: "/Cart/EditQuantity",
        data: {
            UserId: userId,
            ProductId: productId,
            ProductQuantity: productQuantity
        },
        dataType: "json",
        success: function (data) {
            UpdateCartCounter(data.cartCount);

        },
        error: function () {
            alert("error")
        }
    })
};

//Update the price
function updateTotalPrice() {
    let parent = document.getElementById("updateShoppingPrice");
    let subtotal = 0.0;
    for (let i = 0; i < parent.children.length; i++) {
        let unitPrice = parseFloat(parent.children[i].children[2].children[2].innerHTML.replace('$',''));
        let quantity = parseFloat(parent.children[i].children[3].children[0].value);
        let price = unitPrice * quantity;
        parent.children[i].children[4].innerHTML = '$' + price;
        subtotal += price;
    }
    document.getElementById("subtotal").innerHTML = '$' + subtotal;
    document.getElementById("total").innerHTML = '$' + subtotal;   
}

//Update the cart products count 
function UpdateCartCounter(cartCount) {
    let elem = document.getElementById("cartCounter"); //get the counter element from HTML
    elem.innerHTML = cartCount; //change the value of the cart Counter
}

//Checking cart quantity before checkout
function CheckOut() {
    if (document.getElementById("updateShoppingPrice").children.length == 0) {
        alert("The cart is empty");
        return
    }
    window.location.href = "/Cart/PurchaseDone";
}