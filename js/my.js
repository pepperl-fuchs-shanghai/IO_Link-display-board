function ChangeOnOffStatus(serverData) {
    
    var status = serverData[0].status + serverData[1].status + serverData[2].status + serverData[3].status;
    $("#LinkIoimg").attr("src", "/images/status/" + status + ".jpg");
    (serverData[0].status == 1) ? $("#img1").attr("src", "/images/Products/" + serverData[0].device_name + ".jpg") : $("#img1").attr("src", "/images/Products/NoConnection.jpg");
    (serverData[1].status == 1) ? $("#img2").attr("src", "/images/Products/" + serverData[1].device_name + ".jpg") : $("#img2").attr("src", "/images/Products/NoConnection.jpg");
    (serverData[2].status == 1) ? $("#img3").attr("src", "/images/Products/" + serverData[2].device_name + ".jpg") : $("#img3").attr("src", "/images/Products/NoConnection.jpg");
    (serverData[3].status == 1) ? $("#img4").attr("src", "/images/Products/" + serverData[3].device_name + ".jpg") : $("#img4").attr("src", "/images/Products/NoConnection.jpg");
}


function kaishi() {


    var docElm = document.documentElement;
    //W3C   
    if (docElm.requestFullscreen) {
        docElm.requestFullscreen();
    }
    //FireFox   
    else if (docElm.mozRequestFullScreen) {
        docElm.mozRequestFullScreen();
    }
    //Chrome等   
    else if (docElm.webkitRequestFullScreen) {
        docElm.webkitRequestFullScreen();
    }
    //IE11   
    else if (elem.msRequestFullscreen) {
        elem.msRequestFullscreen();
    }
}

function guanbi() {


    if (document.exitFullscreen) {
        document.exitFullscreen();
    }
    else if (document.mozCancelFullScreen) {
        document.mozCancelFullScreen();
    }
    else if (document.webkitCancelFullScreen) {
        document.webkitCancelFullScreen();
    }
    else if (document.msExitFullscreen) {
        document.msExitFullscreen();
    }
}




document.addEventListener("fullscreenchange", function () {

    fullscreenState.innerHTML = (document.fullscreen) ? "" : "not ";
}, false);



document.addEventListener("mozfullscreenchange", function () {

    fullscreenState.innerHTML = (document.mozFullScreen) ? "" : "not ";
}, false);



document.addEventListener("webkitfullscreenchange", function () {

    fullscreenState.innerHTML = (document.webkitIsFullScreen) ? "" : "not ";
}, false);

document.addEventListener("msfullscreenchange", function () {

    fullscreenState.innerHTML = (document.msFullscreenElement) ? "" : "not ";
}, false);

