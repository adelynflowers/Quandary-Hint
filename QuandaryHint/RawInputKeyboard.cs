using RawInput_dll;


namespace QuandaryHint
{
    class RawInputKeyboard
    {

        #region Variables
        //Holds the key state of modifier keys
        bool ctrl = false;
        bool shift = false;

        //The eventarg we're processing
        RawInputEventArg e;

        //Arrays to use for processing, represenatative of keyboard layouts
        static char[] shiftNumberRow = ")!@#$%^&*(".ToCharArray();
        static char[] miscChars = "`-=[]\\;',./".ToCharArray();
        static int[] miscKeys = { 192, 189, 187, 219, 221, 220, 186, 222, 188, 190, 191 };
        static char[] shiftMiscChars = "~_+{}|:\"<>?".ToCharArray();

        //For the finished product
        public string processed;

        //Stores vKey of the current key press
        int vKey;

        //State of the key
        bool make;

        //Device sending the key press
        public string source = "void";

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor, does nothing.
        /// </summary>
        public RawInputKeyboard()
        {
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Main class method that turns an event arg into a string
        /// </summary>
        /// <param name="raw"></param>
        public void ProcessInput(RawInputEventArg raw)
        {
            //Set up the variables for our logic trees
            e = raw;
            vKey = e.KeyPressEvent.VKey;
            source = e.KeyPressEvent.Source;

            //Using a variable makes the code look a lot nicer
            if (e.KeyPressEvent.KeyPressState == "MAKE")
                make = true;
            else
                make = false;
            
            //Logic tree, methods explain what's happening
            if (make)
            {
                if (vKey >= 65 && vKey <= 90)
                {
                    ProcessLetters();
                }
                else if (vKey >= 48 && vKey <= 57)
                {
                    ProcessNumbers();
                }
                else if (vKey > 100)
                {
                    ProcessCharacters();
                }
                if (vKey == 16 || vKey == 17)
                {
                    ProcessModifiers();
                }
                else
                {
                    ProcessSpecial();
                }
            }

            //Release our modifier keys, set the string to void as to not trigger when the key
            //is in a BREAK state
            if (!make)
            {
                if (vKey == 16)
                    shift = false;
                if (vKey == 17)
                    ctrl = false;

                processed = "void";
            }


           
        }

        /// <summary>
        /// Checks to see if we are ready to return an output string
        /// </summary>
        /// <returns></returns>
        public bool OutputReady()
        {
            if (processed == "void")
                return false;
            else
                return true;
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Handles the pressing of letters
        /// </summary>
        private void ProcessLetters()
        {
            char letter = (char)vKey;
            if (shift)
            {
                processed = letter.ToString();
            }
            else
            {
                processed = letter.ToString().ToLower();
            }

            if (ctrl)
            {
                processed = "CTRL" + processed;
            }
        }

        /// <summary>
        /// Handles the pressing of numbers
        /// </summary>
        private void ProcessNumbers()
        {
            if (shift)
            {
                int index = vKey - 48;
                processed = shiftNumberRow[index].ToString();
            }
            else
            {
                char num = (char)vKey;
                processed = num.ToString();
            }

        }

        /// <summary>
        /// Handles the pressing of other keys
        /// </summary>
        private void ProcessCharacters()
        {
            int index = 0;
            for (int i = 0; i < 11; i++)
            {
                if (vKey == miscKeys[i])
                {
                    index = i;
                }
            }

            if (shift)
            {
                processed = shiftMiscChars[index].ToString();
            }
            else
            {
                processed = miscChars[index].ToString();
            }

        }

        /// <summary>
        /// Handles the state of the shift and control keys, used for keyboard shortcuts
        /// </summary>
        private void ProcessModifiers()
        {
            if (vKey == 16)
                shift = true;
            if (vKey == 17)
                ctrl = true;
        }

        /// <summary>
        /// Special key presses used to simulate normal keyboard input
        /// </summary>
        private void ProcessSpecial()
        {
            if (vKey == 32)
                processed = "SPACE";
            if (vKey == 8)
                processed = "BACKSPACE";
            if (vKey == 13)
            {
                if (shift)
                    processed = "SHIFTenter";
                else
                    processed = "ENTER";
            }
                
        }
        #endregion

    }
}
