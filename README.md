# tesi (work in progress)
### Augmented Reality Android app that can interact with IoT objects through an AR interface, made using Unity ARFoundation and Android ARCore.
This project was made for my Bachelor degree thesis, named "Digital Twins and Augmented Reality for Constrained IoT devices".

## How it works
The app recognises images in the real world, wich are associated to the ip of a CoAP server. The server communicates with the app, telling what sensors and actuators are available. The app then creates an AR interface based on that informations, and displays it on top of the image.

## Lines of code
I'm using the LOC metric to approximately track the size of the project.<br> 
<b>LOC=580</b>
<br>
cmd command :  
`dir -Recurse *.cs | Get-Content | Measure-Object -Line` 
