Author: Christopher Fish
Developed for CS3500-010 Fall 2020

General Notes: 
The spreadsheet is interactable with both the mouse and the arrow keys. Upon selecting a cell, you can begin typing immediately to enter data
Data is displayed in multiple ways:
The name of the selected cell is displayed in the Cell Name box
The value of the selected cell is displayed both in the Cell Value box, and in the cell itself in the spreadsheet panel
The contents of the selected cell are displayed in the Cell Contents box.
Any error messages to do with inputting a formula are disaplyed in the box located directly below the Cell Contents box
Saving/Opening files, Closing the program, and creating a new spreadsheet can all be performed by selecting the appropriate option under "File"
Selecting "Help" will open the help window, where informaton about the program can be accessed 
Selecting "Formula Wizard" will open this spreadsheet's unique functionality, the Formula Wizard. More details are listed in the "Special Functionality" section

NOTE: To enter data into a cell, you must hit the enter key. If you do not hit the enter key, any typed data will be lost when you select a new cell

Special Functionality:
This program features a "Formula Wizard". This is a tool that will help you to create formulas and ensure correct formatting is used
Upon opening the wizard, you will see a few options:

Target Cell: 
This is the cell you want to insert your formula into. Select the row and column using the drop down boxes

Add Constant:
Type a number (integer or double) into the box and click "Add" to add a constant to your formula

Add Operator:
These buttons will add an operator into your formula. Each button will add its respective operator.

Add Variable:
This button will add the selected cell in the main spreadsheet window to your formula as a variable. Be sure to select the desired cell before you hit the button

Backspace:
Removed the last character in the current formula

Result:
This box will show your formula in progress

Error Output:
This is the box below the Result box. If you enter an invalid formula, this box will display a message explaining why

Insert Button: 
This button will attempt to insert the created formula into the spreadsheet at the target cell

Once you have created your formula, clicking on the insert button will insert the created formula into the spreadsheet at the selected cell, and close the formula wizard


Limitations:
If you open two instances of the main Spreadsheet program through the use of the "new" menu option, and then open a formula wizard on the other, you will be unable to open a formula wizard on the second window
The formula wizard can be opened on the second window as long as there is not an already existing formula wizard open. I don't know why this is

The formula wizard cannot detect division by zero unless it explicitly contains "/0" in it. Without access to the values of the cells, and the lookup function used to evaluate, there is no way to check for this in real time without creating a copy of the spreadsheet


External Code Resources:
https://stackoverflow.com/questions/37436026/c-sharp-allow-switching-of-input-fields-using-arrow-keys - Used to detect arrow key input
https://stackoverflow.com/questions/6082535/open-new-instance-of-c-sharp-windows-application - Used to open a new instance of the current application
https://social.msdn.microsoft.com/Forums/en-US/ec5b0667-8d40-4f75-aad7-f6b64eefd5f9/how-do-i-stop-the-form-from-closing-when-the-user-clicks-the-close-button?forum=Vsexpressvb - Used to interupt the closing of a form
https://www.c-sharpcorner.com/UploadFile/c713c3/how-to-exit-in-C-Sharp/ - Used to bypass the FormClosing event by ending the main thread of the form
https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-save-files-using-the-savefiledialog-component?view=netframeworkdesktop-4.8 - Used to get the syntax for using a SaveFileDialog and OpenFileDialog components]
https://stackoverflow.com/questions/12905346/how-to-play-a-specific-windows-error-sound-in-c-sharp - Used to find the syntax for playing an error sound
http://csharphelper.com/blog/2017/12/position-a-form-over-another-form-in-c/ - Used to position forms relative to other forms

