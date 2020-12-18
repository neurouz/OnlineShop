var i = 0;
var text = "I'm Neurouz. A web developer.";
var speed = 130;

function keyboardWrite() {
    if (i < text.length) {
      document.getElementById("h3element").innerHTML += text.charAt(i);
      i++;
      setTimeout(keyboardWrite, speed);
    }
}

window.addEventListener("resize", function () {
    document.getElementById("welcome-section").style.height = window.innerHeight + "px";
    this.document.getElementById("projects").style.top = window.innerHeight + "px";
  })