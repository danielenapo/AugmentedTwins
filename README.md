# AR Digital Twins (thesis)
### Augmented Reality Android app that can interact with IoT objects through an AR interface, made using Unity ARFoundation and CoAP (Constrained Application Protocol).
This project was made for my Bachelor degree thesis, named "Digital Twins and Augmented Reality for Constrained IoT devices".

## How it works
The app recognises images in the real world, wich are associated to the ip of a CoAP server. The server communicates with the app, telling what sensors and actuators are available. The app then creates an AR interface based on that informations, and displays it on top of the image.<br><br>

<p align="center">
  <b> COFFEE MACHINE: </b><br>
 <img align="center" src="https://github.com/danielenapo/AugmentedTwins/blob/master/coffeeMachine.gif" /><br>
   <b> AIR CONDITIONER: </b><br>
 <img align="center" src="https://github.com/danielenapo/AugmentedTwins/blob/master/airConditioner.gif" /><br>
</p>

## Import project
The files in this repo are part of a Unity project, so just import all the files in Unity (editor version 2020.3.14f1)<br>
All the useful files are found in the Assets folder. Here's a quick info on the type of files you will find in this project:
- <b>PREFABS:</b> (Assets/prefabs) the "prefabs" that are instantiated as 3D objects (or UI elements) by the Factory class.
- <b>PLUGIN:</b> (Assets/Plugins/Android) the native plugin pre-compiled in Java. the source code is in [this repo.](https://github.com/danielenapo/CoAPClientPlugin_thesis)
- <b>CODE FILES:</b> (Assets/code) All in C#, the folders are divided in modules, you can follow this [class diagram](https://github.com/danielenapo/AugmentedTwins/blob/master/Assets/Code/classDiagram.png) to understand code architecture and structure.

## Software architecture
![architecture](https://github.com/danielenapo/AugmentedTwins/blob/master/schema.png)

## Related repositories
This repository only contains the Unity project of the client AR part of the whole software system.<br>
Other useful related repos are:
- <b>[SERVERS:](https://github.com/danielenapo/Servers_Thesis)</b> they're <b>mandatory</b> to test the app (instructions on the readme)
- <b>[PLUGIN: ](https://github.com/danielenapo/CoAPClientPlugin_thesis) </b> client code of the java natve plugin (you don't need this, it's just to better understand the code)
<b>NOTE:</b><br>
To test the app, you also have to run the servers (HTTP and CoAP) on a local network, then open the settings in the app, insert the HTTP server's local IP, and then press the button "call HTTP Server".

<!--## Lines of code
I'm using the LOC metric to approximately track the size of the project.<br> 
<b>LOC=1232</b>
<br>
cmd command :  
`dir -Recurse *.cs | Get-Content | Measure-Object -Line`!-->
