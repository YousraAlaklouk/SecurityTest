function addBlob() {
    var b = document.createElement('div'),
        size = Math.floor(Math.random() * 200) + 50
    b.className = 'blob'
    b.style.width = size + 'px'
    b.style.aspectRatio = '1/1'
    b.style.background = 'hsl(' + Math.floor(Math.random() * 360) + 'deg, 100%, 50%)'
    b.style.position = 'absolute'
    b.style.left = Math.random() * 100 + '%'
    b.style.top = Math.random() * 100 + '%'
    b.style.zIndex = '-1'
    b.style.borderRadius = '50%'
    b.onanimationend = function () {
        this.remove()
    }
    document.body.appendChild(b)
}

setInterval(addBlob, 100)

function moveText(e) {
    var x = e.clientX,
        y = e.clientY
    document.querySelector('div').style.position = 'absolute'
    document.querySelector('div').style.transform = 'translate(-50%, -25%)'
    document.querySelector('div').style.left = x + 'px'
    document.querySelector('div').style.top = y + 'px'
}

window.addEventListener('mousemove', moveText)