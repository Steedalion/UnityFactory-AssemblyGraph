# AssemblyGraph_unity_module

This module adds the assembly graph components to Unity.

## How to use it
Attach a 'GraphInspector' component to the workstation surface. 

Attach a 'RootComponent' to a gameobject and place it on the workstation surface.

Execute the "GraphInspector.BuildAndDestroy" to begin the process. Usually a button is used to execute the method.

## The C# dll
There is a pure C# dll that was used here. The dll was compiled from [this library](https://github.com/Steedalion/AssemblyGraph)
