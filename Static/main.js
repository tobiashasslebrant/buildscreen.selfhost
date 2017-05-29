(function() {
    setInterval(function () {
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById("content").innerHTML = this.responseText;
                document.getElementById("last-loaded").innerHTML = 'Last loaded: ' + new Date().toLocaleString();
            }
        };
        xhttp.open("GET", "builds", true);
        xhttp.send();
    }, 10000);
})();