Hi, thanks for using SmartCopier!

Use instructions:

   1. Right-click any component in the inspector.
   2. Click "Smart Copy Components" at the bottom of the context menu.
   3. This will open the SmartCopier window where all of the GameObject's Components will be displayed.
   4. Toggle any Component and their respective properties you wish to copy.
   5. Select any amount of GameObjects you wish to copy the Components to and click "Smart Paste Components".
   6. Alternatively, right click on any Component and click "Smart Paste Components" in the context menu.

You can add a [NoCopy] attribute above any property you do not wish to copy, like so:

[NoCopy]
public int Number;

This variable will not show up in the list of properties and fields of the SmartCopier window
and its value will not be copied.
