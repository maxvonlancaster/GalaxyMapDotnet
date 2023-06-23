var zoom = 1;
// var rescale = 0.8;
var x = 0;
var y = 27000;
let mapX = $("#container").width() / 2;
let mapY = $("#container").height() / 2;
let width = $("#container").width();
let height = $("#container").height();

var canvas = document.querySelector('#map');

//var divoffsetX = document.querySelector('#offsetX');
//var divoffsetY = document.querySelector('#offsetY');
//var divmapX = document.querySelector('#mapX');
//var divmapY = document.querySelector('#mapY');
//var divX = document.querySelector('#X');
//var divY = document.querySelector('#Y');

fitToContainer(canvas);
function fitToContainer(canvas) {
    // Make it visually fill the positioned parent
    canvas.style.width = '100%';
    canvas.style.height = '100%';
    // ...then set the internal size to match
    canvas.width = canvas.offsetWidth;
    canvas.height = canvas.offsetHeight;
}

$(document).ready(function () {
    getMap(mapX, mapY, 0);
});

canvas.addEventListener("wheel", function (e) {
    e.preventDefault();
    if (e.deltaY < 0 && zoom < 35) {
        getMap(e.offsetX, e.offsetY, 1)
    }
    else {
        if (e.deltaY > 0 && zoom > 1) {
            getMap(e.offsetX, e.offsetY, -1)
        }
    }
});


var initX = 0;
var initY = 0;
var deltaX = 0;
var deltaY = 0;
var isDown = false;
canvas.addEventListener('mousedown', function (e) {
    console.log(e);
    initX = e.clientX;
    initY = e.clientY;
    isDown = true;
}, true);

canvas.addEventListener('mouseup', function (e) {
    isDown = false;
    deltaX = e.clientX - initX;
    deltaY = e.clientY - initY;
    console.log(deltaX, deltaY);
    mapX = mapX - deltaX*4;
    mapY = mapY - deltaY * 4;

    initX = 0;
    initY = 0;

    getMap(mapX, mapY, 0);
}, true);

canvas.addEventListener('mousemove', function (event) {
    event.preventDefault();
    if (isDown) {
    }
}, true);



function getMap(mapX, mapY, z){
    var data = {
        mapX: mapX,
        mapY: height - mapY,
        zoom: zoom + z,
        x: x,
        y: y,
        width: width,
        height: height
    }

    $.ajax({
        type: 'POST',
        url: 'GetMap',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (result) {
            redraw(result);
        }
    });
}

function redraw(data) {
    x = data.x;
    y = data.y;
    zoom = data.zoom;
    mapX = $("#container").width() / 2;
    mapY = $("#container").height() / 2;

    //console.log(data);

    let canvas = document.getElementById("map");

    let ctx = canvas.getContext("2d");

    ctx.fillStyle = "#000000";
    ctx.beginPath();
    ctx.fillRect(0, 0, canvas.width, canvas.height);
    ctx.stroke();

    ctx.fillStyle = "#ffffff";
    //ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.font = "15px Consolas";
    data.points.forEach(p => {
        if (p.category == 1) {
            ctx.fillRect(p.mapX, height - p.mapY, 10 - p.priority * 2, 10 - p.priority * 2);
        } else if (p.category == 2) {
            ctx.beginPath();
            ctx.arc(p.mapX, height - p.mapY, 3, 0, 2 * Math.PI)
            ctx.stroke();
        }
        ctx.fillText(p.name, p.mapX, height - p.mapY);
    });
    ctx.stroke();

    ctx.font = "italic 15px Consolas";
    data.points.filter(p => p.category == 3).forEach(p => {
        ctx.fillText(p.name, p.mapX, height - p.mapY);
    })

    data.regions.forEach(r => {
        ctx.strokeStyle = r.colour;
        ctx.beginPath();
        ctx.moveTo(r.counturPoints.split('|')[1].split(',')[0], height - r.counturPoints.split('|')[1].split(',')[1]);
        r.counturPoints.split('|').forEach(p => {
            ctx.lineTo(p.split(',')[0], height - p.split(',')[1]);
        })
        ctx.lineTo(r.counturPoints.split('|')[1].split(',')[0], height - r.counturPoints.split('|')[1].split(',')[1]);
        ctx.stroke();
    })

    data.circles.forEach(c => {
        ctx.strokeStyle = "white";
        ctx.beginPath();
        ctx.arc(c.x, height - c.y, c.r, 0, 2 * Math.PI);
        ctx.stroke();
    })

    data.lines.forEach(l => {
        ctx.strokeStyle = "white";
        ctx.beginPath();
        ctx.moveTo(l.xStart, height - l.yStart);
        ctx.lineTo(l.xEnd, height - l.yEnd);
        ctx.stroke();
    })
    
}