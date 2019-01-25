namespace TerminalControl.Terminals
{
    public enum EditModes
    {
        /// <summary>
        /// Do not locally edit text. Cursor keys are sent to the host.
        /// </summary>
        None=0,
        /// <summary>
        /// Echo the typed character or cursor command. Keystrokes are also sent to the host.
        /// </summary>
        LocalEcho=1,
        /// <summary>
        /// Edit text on the current line. Up and Down selects recently entered lines for editing. 
        /// Cursor command keystrokes (Up, Down, Left, Right, Home, End) are not sent to the host.
        /// </summary>
        LineEdit=2,
        /// <summary>
        /// Full screen editing. Moving the cursor selects a line for editing, and pressing RETURN or ENTER 
        /// transmits that line of text to the host. 
        /// Cursor command keystrokes (Up, Down, Left, Right, Home, End) are not sent to the host.
        /// </summary>
        FullScreen = 3
    }
}