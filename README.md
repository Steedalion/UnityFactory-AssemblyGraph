# AssemblyGraph_unity_module

This module adds the assembly graph components to Unity. It comes with basic editor functionality, see below for description.
See video on for example [youtube](https://www.youtube.com/watch?v=EvswdwAIvpQ&feature=youtu.be). Simply place this folder in the unity project under the `Assets` folder.

## How to use it
Attach a `GraphInspector` component to the workstation surface. The radius adjusts reach of the inspection.  The red-sphere gizmo visualizes this.

<img src='Inspector.PNG' >

Attach a `RootComponent` to a gameobject and place it on the workstation surface. You can also attach `AssemblyComponent` to other gameobjects.

<img src='Inspector.PNG' >

Attach `AssemblyConnectors` to these components.

<img src='Connector.PNG'>

Execute the `GraphInspector.BuildAndDestroy` to begin the process. Usually a button is used to execute the method.


<img src='Assembly.PNG'>


An example assembly.

## The C# dll

`GraphDomain.dll` is a pure C# dll that was used here. The dll was compiled from [this library](https://github.com/Steedalion/AssemblyGraph)
