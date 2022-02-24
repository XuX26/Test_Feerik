Ulysse RAILLON | 02/24/22 16h45

--- Patchnote :
- New Gizmo Editor Window
- GizmoAsset Inspector (to open the new window by a button)
- Menu Item Window>Custom>Show Gizmos (to open the new window)
- Gizmo Editor Window content and feature :
	- Select GizmoAsset from project
	- Gizmo name
	- Gizmo Position
	- Copy Button (Position in clipboard)
	- Edit Button
	- Tool tweaks : Sphere Size & Color
- More than a window it's a tool, so we can in the Scene View :
	- Display : Sphere, Gizmo move tool, Gizmo name
	- Move Gizmo (when editable)
	- Undo positions changes (Ctrl+Z)

Basically, I made every requirements except the last one.
I tried to detect clic with controlID's Handles, but I'm out of time.
So I cleaned a bit the tool and added some bonus features.


--- Dev words :
First of all, thank you for this test!
I love Gameplay tool and practiced, but I learned a lot from this test.
I usually use GameObjects so it was a bit harder.

I'm not proud of the current architecture, usually I use MVC Observer pattern.
But as I'm not that much confortable with Editor I prefered to not use them,
and focused on developping a MVP first.
More over my Window script is pretty long, I think, I maybe should split the GUI and Scene behaviors in two script.

However, I am still proud of what I done within the time allowed.

If I had to continue this tool in a professionnal purpose, I would rework the architecture as i said
(if the tool have a well time spent/time saved ratio).
More over I would add a key (like shift) to move multiple editable Gizmo at the same time.
A focus button in Gizmo window, I know how to do with GameObject but it's maybe possible without.