# FileToSplatoon
A program to convert small files into a Splatoon 1/2/3 post
## How To Use
Run the program, you should see a small window with some UI.\
To convert a file, click the ``Open`` button and select a file under 4.8kb.\
(Note that Windows might show the file size rounded to the nearest whole. To see the real size, right click the file and select "Properties". You can find the actual size under "Size:".\
Once your file is selected in the File Explorer window, click "Open" and the file will convert and a PNG will be saved to the same directory as the original file.\
Optionally, you can choose to invert the image using the ``Invert`` CheckBox.
## How It Works
The output file contains every bit of every byte converted to a black and white image.\
Since a bit (8 bits = 1 byte) can have two states (0 or 1) it can be converted into a pixel on a black and white image, such as those seen in the Splatoon games.\
You may also notice a text file being output on every conversion, this is a file of every bit converted to an ASCII character. (every bit is a 0 or 1)\
