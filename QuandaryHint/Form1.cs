using System;
using System.Windows.Forms;
using RawInput_dll;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using NLog;



/************************************
 * TODO LIST
 * Ctrl-f TODO for specific functions
 * Use enum for gametype
 * seperate game options struct into seperate file
 * ***********************************/
namespace QuandaryHint
{
    public partial class Form1 : Form
    {
        //Imports the RegisterHotKey method for global hotkeys
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        //Constants for our key IDs
        const int PAGEUP_ID = 1;
        const int PAGEDOWN_ID = 2;
        const int F5_ID = 3;

        #region Variables

        //logger
        NLog.Logger logger;
        //Holds specific options for each game mode
        public GameOptions inheritOptions;

        //Our configuration window
        private configWin _configWin;

        //Bool connected to a global switch for audio
        private bool audioOn = true;

        //Hint counter
        private int hintCount = 0;

        //Spreadsheet

        //For cancelling the writing of config files
        bool writeFile = true;

        //RawInput TODO: Remove/Archive
        /*RawInputKeyboard rawInputKeyboard;
        int cursorPos = 0;
        RawInput rawInput;
        public bool captureOnlyInForeground = false;
        string selectedSource = "void";
        bool TeamEntryFocused = false; */

        //Game object where the magic happens
        public Game testGame;

        //Timer for introducing to welcome message automatically
        Timer welcomeTimer = new Timer();
       

        #endregion

        #region Constructors
        public Form1(gameSelect gameSel)
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Beginning new " + gameSel.gameOptions.gameMode + " game");
            //Start the form
            InitializeComponent();

            //Let's us use form wide keyboard shortcuts
            this.KeyPreview = true;

            //Get our game options, then override them if there's a config file
            ParseGameOptions(gameSel.gameOptions);
            ReadConfigFile(ref inheritOptions);

            //Startup our main class
            logger.Info("Initializing windows");
            testGame = new Game(inheritOptions);

            //Open up the configuration window
            _configWin = new configWin(testGame);

            //TODO: Look into data linking to get rid of this
            audioToggle.Checked = audioOn;
            logger.Info("Registering welcomeTimer");
            //Setting up the welcome message timer
            welcomeTimer.Interval = (int)(inheritOptions.timerOffset * 1000);
            welcomeTimer.Tick += new EventHandler(timer_Tick);

            //Global hotkey setup
            logger.Info("Registering hotkeys");

            //Set the hotkey triggerer for the pageup key
            int PageUpKey = (int)Keys.PageUp;
            int pgDownKey = (int)Keys.PageDown;
            int f5Key = (int)Keys.F5;
            //Register the hotkey
            RegisterHotKey(this.Handle, PAGEUP_ID, 0x0000, PageUpKey);
            RegisterHotKey(this.Handle, PAGEDOWN_ID, 0x0000, pgDownKey);
            RegisterHotKey(this.Handle, F5_ID, 0x0000, f5Key);
           

            #region RawInput setup DO NOT TOUCH
           // rawInputKeyboard = new RawInputKeyboard();
           //// rawInput = new RawInput(Handle, captureOnlyInForeground);
            //rawInput.AddMessageFilter();
            hintEntry.Text = "";
            //rawInput.KeyPressed += OnKeyPressed;
            #endregion
        }


        #endregion

        #region Config file handling

        /// <summary>
        /// Handles the reading of config file settings into variables. If there isn't one it'll create one for future
        /// use with no data in it.
        /// </summary>
        /// <param name="inheritOptions"></param>
        private void ReadConfigFile(ref GameOptions inheritOptions)
        {
            logger.Info("ReadConfigFile starting");
            string configPath = inheritOptions.gameMode + ".json";

            //If there isn't a file
            if (!File.Exists(configPath))
            {
                logger.Error("No file found");
                //Make one with some junk in it
                StreamWriter sw = new StreamWriter(configPath);
                sw.WriteLine("blankbook");
                sw.Close();
                Console.WriteLine("No config found");
               
            }
            //If there is...
            else 
            {
                //Open up the file
                StreamReader sr = new StreamReader(configPath);

                //Read in our data
                try
                {
                    logger.Info("Attempting to read in data");
                    string json = sr.ReadLine();
                    inheritOptions = JsonConvert.DeserializeObject<GameOptions>(json);
                    logger.Info("hint volume is " + inheritOptions.hintVolume);
                    sr.Close();
                }
                catch (IOException e)
                {
                    logger.Error("IOException: " + e);
                    System.Console.WriteLine(e);
                    MessageBox.Show("Invalid config file.");
                    File.Delete(configPath);
                }
                finally
                {
                    sr.Close();
                }
            }
            logger.Info("ReadConfigFile finishing");
        }

        /// <summary>
        /// Write the current settings to a config file whenever we close out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            logger.Info("Form closing");
           if(writeFile)
                testGame.WriteConfigFile();

        }

        #endregion

        #region Hint Pushing


        /// <summary>
        /// Pushes the hint text to the hint windows, and plays sound if desired
        /// </summary>
        /// <param name="sound"></param>
        private void pushHint(bool sound)
        {
            logger.Info("Pushing a hint, Sound?: " + sound);
            //Play the sound
            if (audioOn && sound)
                testGame.PlayHint();

           
            //Set the labels
            testGame.SetHintText(hintEntry.Text);

            //Reset the textbox
            hintEntry.Text = "";
            
            //Update the hintcount
            updateHintCount(1);
            
        }

        /// <summary>
        /// Updates the hint count
        /// </summary>
        /// <param name="adjustment"></param>
        private void updateHintCount(int adjustment)
        {
            logger.Info("Updating hint count with adjustment " + adjustment);
            //Change the hint count
            hintCount += adjustment;

            
        }
        #endregion

        #region Keyboard interaction

        /// <summary>
        /// Takes in the RawInputEvent and sends it to be processed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       /* private void OnKeyPressed(object sender, RawInputEventArg e)
        {
            rawInputKeyboard.ProcessInput(e);
            if (selectedSource == "void" || selectedSource == rawInputKeyboard.source)
            {
                if (!captureOnlyInForeground)
                {
                    if (TeamEntryFocused)
                        HandleInput(rawInputKeyboard.processed, TeamNameEntry);
                    else
                        HandleInput(rawInputKeyboard.processed, hintEntry);
                }
            }

        }*/

        /// <summary>
        /// Turns rawinput into meaningful text entry
        /// </summary>
        /// <param name="output"></param>
       /* void HandleInput(string output, TextBox hintEntry)
        {


            if (output == "SPACE")
            {
                hintEntry.Text += " ";
                cursorPos += 2;
                hintEntry.SelectionStart = cursorPos;
            }
            else if (output == "BACKSPACE")
            {
                if (hintEntry.Text.Length > 0)
                    hintEntry.Text = hintEntry.Text.Remove(hintEntry.Text.Length - 1);
                cursorPos -= 2;
                if (cursorPos < 0)
                {
                    cursorPos = 0;
                }
                hintEntry.SelectionStart = cursorPos;
            }
            else if (output == "CTRLk")
            {
                selectedSource = rawInputKeyboard.source;
            }
            else if (output == "CTRLr")
            {
                hintEntry.Text = "";
                pushHint(false);
                cursorPos = 0;

                //This is to compensate for the pushHint upping it one
                updateHintCount(-1);

                hintEntry.SelectionStart = 0;
            }
            else if (output == "CTRLa")
            {
                hintEntry.SelectAll();
            }
            else if (output == "SHIFTenter")
            {
                pushHint(false);
            }
            else if (output == "CTRLw")
            {
                hintEntry.Text = inheritOptions.welcomeMessage;
                pushHint(false);
                hintCount = 0;

                //Compensate for pushHint
                updateHintCount(0);

            }

            else if (output == "ENTER")
            {
                pushHint(true);
                hintEntry.Text = "";
                cursorPos = 0;
                hintEntry.SelectionStart = cursorPos;
            }
            else if (output != "void")
            {
                hintEntry.Text += output;
                cursorPos += 2;
                hintEntry.SelectionStart = cursorPos;
                if (this.hintEntry.Text != "")
                    testGame.DecoderMessage();
            }

        }*/


        #endregion

        #region Misc Private methods

        /// <summary>
        /// When the timer hits the tick(start of the game), it displays the
        /// welcome message and then stops the timer so we can use it next
        /// round
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            testGame.SetHintText(inheritOptions.welcomeMessage);
            logger.Info("timer tick");
            if (inheritOptions.gameMode == "The Candy Shoppe")
            {
                logger.Info("candy shoppe tick");
                testGame.loopMusic.PlayLoop();
            }
            welcomeTimer.Stop();
        }

        /// <summary>
        /// Parses the game mode information from the first window
        /// </summary>
        /// <param name="game"></param>
        private void ParseGameOptions(GameOptions game)
        {
            inheritOptions.welcomeMessage = game.welcomeMessage;
            inheritOptions.videoPath = game.videoPath;
            inheritOptions.hintFont = game.hintFont;
            inheritOptions.gameMode = game.gameMode;
            inheritOptions.fontColor = game.fontColor;
            inheritOptions.audioPath = game.audioPath;
            inheritOptions.timerOffset = game.timerOffset;
            inheritOptions.previewFont = game.previewFont;
            inheritOptions.gameColumn = game.gameColumn;
            inheritOptions.gameVolume = game.gameVolume;
            inheritOptions.hintVolume = game.hintVolume;
            inheritOptions.videoOffset = game.videoOffset;
            inheritOptions.hintFontSize = game.hintFontSize;
        }

        /// <summary>
        /// Background worker1's task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       /* private void bwExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            excel = new Excel(excelPath, 1, inheritOptions.gameColumn);
        }*/

        #endregion

        #region Clicking events

        /// <summary>
        /// Pushes the hint with sound, just in case the keyboard doesn't work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HintPusher_Click(object sender, EventArgs e)
        {
            pushHint(true);
        }
    
        /// <summary>
        /// Shows the configuration window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configButton_Click(object sender, EventArgs e) => _configWin.Show();

        /// <summary>
        /// Toggles hint audio on and off
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void audioToggle_CheckStateChanged(object sender, EventArgs e)
        {
            audioOn = audioToggle.Checked;
        }

        /// <summary>
        /// Begins a round
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartVideoBtn_Click(object sender, EventArgs e)
        {
            testGame.StartGame();
            welcomeTimer.Start();
            Console.WriteLine("Timer started");
        }

        /// <summary>
        /// Aligns the hint windows to their videos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlignHintsBtn_Click(object sender, EventArgs e)
        {
            testGame.AlignHintWindows();
        }
       
        /// <summary>
        /// Toggles the paused state of the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayPauseBtn_Click(object sender, EventArgs e)
        {
            testGame.TogglePaused();
            label3.Text = testGame.GetEscapeTime(false);
        }

        /// <summary>
        /// Manually updates the hint count for the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hintCounter_ValueChanged(object sender, EventArgs e)
        {
            
            updateHintCount(0);
        }

        /// <summary>
        /// Plays the hint sound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HintSoundBtn_Click(object sender, EventArgs e)
        {
            testGame.PlayHint();
        }

        /// <summary>
        /// Calls the escape method and sets the label to the escape time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EscapeBtn_Click(object sender, EventArgs e)
        {
           
            testGame.Escape();
            Console.WriteLine("Escape method");
            label3.Text = testGame.GetEscapeTime(false);
            Console.WriteLine("get escape time");

            try
            {
                //excel.AppendToDocument((int)TeamSizeEntry.Value, TeamNameEntry.Text, testGame.GetEscapeTime(true), true);
            }
            catch (NullReferenceException ex)
            {
                System.Console.WriteLine(ex);
                MessageBox.Show("No excel file found! Please restart the program.");
                string path = inheritOptions.gameMode + "_config.txt";
                File.Delete(path);
                writeFile = false;
                this.Close();
            }
            finally
            {
               
                testGame.SetHintText("");
            }
            
            Console.WriteLine("appended to doc");
        }

        /// <summary>
        /// Does all the initial setup gruntwork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void easyStartBtn_Click(object sender, EventArgs e)
        {
            testGame.SetupGameWindow();
        }

        /// <summary>
        /// Resets the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            testGame.ResetGame();
            
            label3.Text = "Escape Time";
            testGame.SetHintText("");
            welcomeTimer.Stop();

            Console.WriteLine("appended to doc");
        }

        /// <summary>
        /// Toggles whether or not RawInput gets passed the the processor function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*private void CaptureInputCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (captureOnlyInForeground)
                captureOnlyInForeground = false;
            else
                captureOnlyInForeground = true;
        }*/

        

      


       
        

        /// <summary>
        /// Brings the RawInput events to the team entry text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*private void TeamNameEntry_Click(object sender, EventArgs e)
        {
            TeamEntryFocused = true;
            if (TeamNameEntry.Text == "blank")
                TeamNameEntry.Text = "";

        }*/

        /// <summary>
        /// Brings the RawInput events to the hint entry text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*private void hintEntry_Click(object sender, EventArgs e)
        {
            TeamEntryFocused = false;
        }*/


        #endregion

        private void hintEntry_TextChanged(object sender, EventArgs e)
        {

        }

        private void TeamNameEntry_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles form wide keyboard shortcuts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        /// <summary>
        /// Handles keyboard shortcuts for the hintEntry textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hintEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue != 16 || e.KeyValue != 17) && hintEntry.Text != "")
            {
                testGame.DecoderMessage();
            }
            //ENTER: Push a hint, prevent normal event from occurring
            if (e.KeyValue == 13)
            {
                logger.Info("Enter pressed, pushing hint");
                if (e.Shift)
                    pushHint(false);
                else
                    pushHint(true);
                e.SuppressKeyPress = true;
            }
            if (e.KeyValue == 82 && e.Control) //Remove hint from entry and screen
            {
                logger.Info("Clearing hintEntry");
                hintEntry.Text = "";
                pushHint(false);
                e.SuppressKeyPress = true;
            }
            
        }

        protected override void WndProc(ref Message m)
        {
           
            //Catch when a hotkey is pressed
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();
                if (id == PAGEDOWN_ID)
                {
                    logger.Info("Detected pagedown, starting game");
                    testGame.StartGame();
                    welcomeTimer.Start();
                }
                if (id == PAGEUP_ID)
                {
                    logger.Info("Detected pageup, playing hint sound");
                    testGame.PlayHint();
                }
                if (id == F5_ID)
                {
                    logger.Info("Detected f5, escaping");
                    testGame.Escape();
                }
                
            }
            base.WndProc(ref m);
        }
    }
}
