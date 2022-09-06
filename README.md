# AR Digital Twins (thesis)
### Augmented Reality Android app that can interact with IoT objects through an AR interface, made using Unity ARFoundation and CoAP (Constrained Application Protocol).
This project was made for my Bachelor degree thesis, named "Digital Twins and Augmented Reality for Constrained IoT devices".

## How it works
The app recognises images in the real world, wich are associated to the ip of a CoAP server. The server communicates with the app, telling what sensors and actuators are available. The app then creates an AR interface based on that informations, and displays it on top of the image.<br><br>
![thesis img](https://github.com/danielenapo/danielenapo.github.io/blob/master/images/thesis.png)
<b>NOTE:</b><br>
To test the app, you have to run the servers on a local network, then open the settings in the app, insert the HTTP server's local IP, and then press the button "call HTTP Server".

## Related repositories
This repository only contains the final code of the client part of the whole application.<br>
Other useful related repos are:
- <b>[SERVERS:](https://github.com/danielenapo/Servers_Thesis)</b> they're mandatory to test the app (instructions on the readme)
- <b>[PLUGIN: ](https://github.com/danielenapo/CoAPClientPlugin_thesis) </b> client code of the java natve plugin (you don't need this, it's just to better understand the code)



## Lines of code
I'm using the LOC metric to approximately track the size of the project.<br> 
<b>LOC=1232</b>
<br>
cmd command :  
`dir -Recurse *.cs | Get-Content | Measure-Object -Line` 
