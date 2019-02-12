# PyBasic Syntax and notes
PyBasic is a code translator for Microsoft BASIC to Python 2.7 syntax

What follows is a brief syntax description for the keywords recognized by the PyBasic translator. 

## Pre-Processor

Note that commands are NOT case sensitive. So *print* and *PRINT* evalaute to the same statement. 
BASIC symbols are converted to lower case by default. Text in strings will retain the original case. 

10 PRINT "Hello"
becomes
print "Hello"

### $PYTHON
Interpret subsequent lines of code as Python. Leading line numbers will be ignored. The first space after the line number will be ignored. 

10 $PYTHON
20 for r in range(5,10):
30     print r

The line numbers are removed and the code will be interpreted as:
for r in range(5,10):
    print r

### $BASIC
Evaluate subsequent lines of code as BASIC

10 $BASIC
20 FOR R = 1 TO 10
30 PRINT R
40 NEXT

this will generate the following Python code snippet:
for r in range(1,10)
	print r

$ESCAPE 0|1

Turn on Python escape sequences in strings. Using the backslash (\) in a string will send the appropriate Python escape character.
$ESCAPE 1
print "\1b[2J;";
Clears the screen by sending the ANSI sequence ^[[2J;

## Statements 

### PRINT object [[;|,]object]... [;|,]

Print a string to the console. If $ESCAPE 1 is set, Python escape sequences will be honored. If $ESCAPE 0 is set, 
Python escape sequences will be filtered out (by escaping the backslash whenever it is encountered in a text string.)

