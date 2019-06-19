# CRTERM

Windows terminal emulator for Serial and Telnet connections.
Includes special editing modes for recent command recall and full screen BASIC editing. 

Designed as a companion for CP/M emualtors like the Altairduino or RunCPM, CRTERM has some specific features for CP/M use.

Press F11 to go full screen. This will cover the task bar and give you a vintage-style terminal with no Windows distractions. 

Press F12 to switch to BASIC mode. You can edit text on the screen, and when you press ENTER, that line of text is transmitted to the
host. While in BASIC mode, you have some additional control keys:

Arrow keys: move the cursor
Home: move to the start of the line
End: move to the last non-space character on the line.
Control-End: Delete the rest of the line after the cursor. 
Control-Home: Clear the screen
Insert: Switch to insert mode. Text typed will push the rest of the line to the right. Be careful, as you may lose text if you 
push text off the right side. 

Typing RUN will switch the terminal back to terminal mode and run your program. 

Pressing F12 again will switch back to standard terminal mode and place the cursor below the last line of text on the screen. 

# License

GPL 2.0. Please see LICENSE files for terms of the GPL.
