ReadMe alpha v0.2
===

## Instructions

Color Snail applies user-friendly UI/UX. The tool is intuitively easy to utilize. 

To download the source code, please fork or clone the repository. I am more than willing to hear from users. Please open an issue or email me directly at hj5fb@virginia.edu if you prefer it. 

To download the executable only (the .exe you run), open the folder "Release [version number]" and download "ColorSnail.exe". This standalone executable should be able to run if .Net framework is installed properly on your machine.

### 1. Grab Color

To grab a color from anywhere on the screen, click the "Color Straw" button. You will see the text beside it instantly changes. Click anywhere you want on the screen, and the program will read the color at the pixel your mouse is resting. The color will be automatically added to the main list.

![image](http://powersnail.github.io/images/add_color.gif)

### 2. Use Color

To use the colors stored in the list, you can either 1). remember the color code displaying or 2). click on the square showing the color, during which the color code will be copied to your clipboard. You can put your caret at the place you need to insert the color code, and paste. Simple!

### 3. Other operations

Double click on the top bar, and you will be able to see the About and Credit information.

## Latest Version

alpha v0.2 [goto Release Notes](#Releases)

## TODOs

- Add animations when mouse move over clickable items to provide better interactive responses
- Enable saving the list of color to a local file 
- Enable loading the list of color from a local file 
- Add tutorial animation
- Perfecting the ReadMe.md file: a). add icons to places needed; b). add screenshots of the program

## Releases

### 2015 Dec. 21: alpha v0.2
New Features:

1. When selecting pixel, you can click right button to exit the selecting mode.
2. A border that indicates the mode of the program: blue for normal and red for selecting mode.
3. Animation added for adding and removing color items.
4. Refactored some confusing method names.
5. Effect for mouse moving over close button.
6. Save colors to file.
7. Load colors from file.

bug fixed:

1. window no longer acquire focus every time its deactivated. Main window and About window both apply topmost property; Main window will acquire focus when user is selecting pixel from the screen.

### 2015 Dec. 12: alpha v0.1
---
>>>>>>> origin/master
1. Get Color from any pixel on the screen
2. Keep main window active while getting the color
3. List colors, with a square demonstrating its color, a textBlock writing its color code and a 'x' button to close the color item
4. The square showing color is clickable, copying the color code into clipboard
5. Double click on top bar: About Window will show
