$('.like-btn').on('click', function () {
    $(this).toggleClass('is-active');
});

$('.minus-btn').on('click', function (e) {
    e.preventDefault();
    var $this = $(this);
    var $input = $this.closest('div').find('input');
    var value = parseInt($input.val());

    if (value >=1) {
    value = value - 1;
} else {
    value = 0;
}

$input.val(value);
 
});

$('.plus-btn').on('click', function (e) {
    e.preventDefault();
    var $this = $(this);
    var $input = $this.closest('div').find('input');
    var value = parseInt($input.val());

    if (value<100) {
    value = value + 1;
} else {
    value = 100;
}

$input.val(value);
});

$(document).ready(function () {

    $('.radio-group .radio').click(function () {
        $('.radio').addClass('gray');
        $(this).removeClass('gray');
    });
});


//Remove products form the cart
window.onload = function () {
    let elems = document.getElementsByClassName("item");
    for (let i = 0; i < elems.length; i++) {
        elems[i].childNodes[3].lastChild.addEventListener('click', RemoveProduct);
        elems[i].childNodes[3].childNodes[1].addEventListener('change', ChangeQuantity);
    }
}

function RemoveProduct(event) {
    let target = event.currentTarget;
    let number = document.getElementById(target.className.substring(7)).value;
    checkIsProductDeleted(target.id, target.className.substring(7),number);
    target.remove();
    updateTotalPrice();
}

function ChangeQuantity(event) {
    targetNum = event.currentTarget;
    let number = document.getElementById(target.className.substring(7)).value;
    let user = targetNum.id;
    let product = target.className.substring(7);
    ChangeQuantityInDB(user, product, number);
    if (number === 0) {
        targetNum.remove();
    }
    updateCartTotal();
}

function checkIsProductDeleted(userId, productId, addToCartProductQuantity) {
    $.ajax({
        type: "POST",
        url: '@Url.Action("Remove","Cart")',
        data: {
            UserId: userId,
            ProductId: productId,
            AddToCartProductQuantity: addToCartProductQuantity
        },
        dataType: "json",
        success: function () {
            alert("Product has been removed from the cart!")
        },
        error: function () {
            alert("Something went wrong!")
        }
    })
}

function ChangeQuantityInDB(userId, productId, productQuantity) {
    $.ajax({
        type: "POST",
        url: '@Url.Action("EditQuantity", "Cart")',
        data: {
            UserId: userId,
            ProductId: productId,
            ProductQuantity: productQuantity
        },
        dataType: "json",
        success: function () {
            alert("success")
        },
        error: function () {
            alert("error")
        }
    })
}

function updateTotalPrice() {
    
}