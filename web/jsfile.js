let objectContainer = document.querySelector('a-scene')
let treeContainer = document.querySelector('#tree-container')

function drawNode(container, position, colour,  text, parentPosition) {
  let sphere = document.createElement('a-sphere')
  sphere.setAttribute('node', '')
  sphere.setAttribute('position', position)
  sphere.setAttribute('color', colour)
  sphere.setAttribute('value', text)
  drawLine(container, position, parentPosition)
  container.appendChild(sphere)
}

function drawLine(container, position1, position2) {
  let line = document.createElement('a-entity')
  line.setAttribute('line', "start: " + position1 + "; end: " + position2 + "; colour: #ff0")
  container.appendChild(line)
}

function loadFile(filePath) {//extra
  var result = null
  var xmlhttp = new XMLHttpRequest()
  xmlhttp.open("GET", filePath, false)
  xmlhttp.send()
  if (xmlhttp.status==200) {
    result = xmlhttp.responseText
  }
  return result
}

function drawTree(filePath, container) {
  var tree = JSON.parse(loadFile(filePath))
  
  for (var i = 0; i < tree.length; i++) {
    drawNode(container, 
            tree.nodes[i].position,
            tree.nodes[i].colour,
            tree.nodes[i].text,
            tree.nodes[tree.nodes[i].parentID].position)
  }
}

drawTree('input.json', treeContainer)



//future stuff
var toggle = function() {
  //button for future use loading files
}

let hand = document.querySelector('a-entity')
document.querySelector('a-entity').addEventListener('trackpadchanged', function (evt) {
  //button tracking on controller
})


