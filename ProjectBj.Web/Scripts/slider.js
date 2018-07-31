var slider = document.getElementById("bet_range");
var output = document.getElementById("bet_output");

slider.oninput = function() {
    output.innerHTML = this.value;
}