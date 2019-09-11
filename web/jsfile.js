let objectContainer = document.querySelector('a-scene')
let treeContainer = document.querySelector('#tree-container')

function drawNode(container, position, colour, colour2,  name, info, parentPosition) {
  let sphere = document.createElement('a-sphere')
  sphere.setAttribute('node', 'name: ' + name + "; info:" + info)
  sphere.setAttribute('position', position)
  sphere.setAttribute('color', colour)
  sphere.setAttribute('radius', 0.3)
  drawLine(container, position, parentPosition, colour2)
  container.appendChild(sphere)
}

function drawLine(container, position1, position2, colour) {
  let line = document.createElement('a-entity')
  line.setAttribute('line', "start: " + position1 + "; end: " + position2 + "; color: " + colour)
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
  
  while (container.firstChild) {
    container.removeChild(container.firstChild)
}
  
  var tree = JSON.parse(loadFile(filePath))
  
  for (var i = 0; i < tree.length; i++) {
    
    drawNode(container, 
            tree.nodes[i].position,
            tree.nodes[i].colour,
            tree.nodes[i].colour2,
            tree.nodes[i].name,
            tree.nodes[i].distance,
            tree.nodes[tree.nodes[i].parentID].position)
  }
}

var toggle = function() {
  var m = document.getElementById("selectMenu")
  var filename = m.options[m.selectedIndex].value;
  drawTree(filename + '.json', treeContainer)
}

//let hand = document.querySelector('a-entity')//what does this even do


document.querySelector('a-entity').addEventListener('trackpadchanged', function (evt) {
  
  var transform = document.querySelector('#interface-transform')
  var transform2 = document.querySelector('#camera-container')
  
  var newposition = transform.getAttribute('position')
  //newposition  += transform2.getAttribute('position')
  var newrotation = transform.getAttribute('rotation')
  //newrotation += transform2.getAttribute('rotation')
  var interCon = document.querySelector('#interface-container')
  
  interCon.setAttribute('rotation', newrotation)
  //interCon.object3D.rotation.x =  0
  //interCon.object3D.rotation.z =  0
  
  interCon.setAttribute('position', newposition)
  interCon.object3D.position.y += -2 
  interCon.object3D.position.x += 1 
  
})


