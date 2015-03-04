
//Refresh session
function heartbeat() {
    $.post(
        "/HeartBeat/Pumping/",
        null,
        function (data) {
            setHeartbeat();
        },
        "json"
    );
}

//Reconnect session in every 5mins
function setHeartbeat() {
    //setTimeout("heartbeat()", 300000);    // every 5 min
    setTimeout("heartbeat()", 180000);      // every 3 min
}
