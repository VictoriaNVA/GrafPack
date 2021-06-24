# GrafPack
A shape-drawing application in C#, with a WinForms GUI

## Running instructions
Clicking Create opens up a submenu offering a selection of three shapes: square, triangle and circle. Whichever of them is selected, a dialog prompts the user to click the screen twice to create the shape. Clicking on the screen in 2 different places will display the shape with a blue fill and a black outline. Once one or more shapes are displayed, shapes can be selected using “Select” option from the main menu. After “Select” is activated, clicking a shape will change its color to yellow, and display a submenu of actions: move, rotate, scale, delete. Moving a shape is done by dragging it around, rotating and scaling take input values (degrees, respectively scale X and scale Y), and delete is self-explanatory. Each time a transformation is finished, the user must click “Select” again to select another (or the same) shape, and do other transformations. To exit the program, click on the “X” at the top right corner of the canvas.
![image](https://user-images.githubusercontent.com/36083446/123250402-eafb4d00-d4e1-11eb-8d26-634f8bfb5e29.png)

