using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuandaryHint
{
    /// <summary>
    /// Struct definition for storing game options
    /// </summary>
    public struct GameOptions
    {
        public string welcomeMessage;
        public string gameMode;
        public Font hintFont;
        public Font previewFont;
        public Color fontColor;
        public string videoPath;
        public string audioPath;
        public double timerOffset;
        public double videoOffset;
        public int gameVolume;
        public int hintVolume;
        public int loopVolume;
        public int gameColumn;
        public int hintFontSize;
        public int waveOut;
        
        

    }

    public partial class gameSelect : Form
    {
        #region Variables
        //Struct to store options
        public GameOptions gameOptions;
        #endregion

        #region Constructors

        /// <summary>
        /// Sets the waveout to 10, since that is a universal default
        /// </summary>
        public gameSelect()
        {
            InitializeComponent();
            gameOptions.waveOut = 10;
            gameOptions.loopVolume = 10;
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// Sets game options for the Locked in Dead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lockedInDead_Click(object sender, EventArgs e)
        {
            gameOptions.welcomeMessage = "Welcome to the Locked in Dead...";
            gameOptions.hintFontSize = 51;
            gameOptions.hintFont = new Font("Chiller", gameOptions.hintFontSize);
            gameOptions.previewFont = new Font("Chiller", 12);
            gameOptions.fontColor = Color.White;
            gameOptions.gameMode = "The Locked In Dead";
            gameOptions.audioPath = @"assets\deadSound.mp3";
            gameOptions.videoPath = @"C:\LID.wmv";
            gameOptions.timerOffset = 108;
            gameOptions.videoOffset = 0;
            gameOptions.hintVolume = 5;
            gameOptions.gameVolume = 9;
            gameOptions.gameColumn = 2;
            this.Close();
        }

        /// <summary>
        /// Sets game options for The Runaway Train
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void candyShoppe_Click(object sender, EventArgs e)
        {
            gameOptions.welcomeMessage = "Welcome to the Candy Shoppe...";
            gameOptions.hintFontSize = 22;
            gameOptions.hintFont = new Font("Lucida Console", gameOptions.hintFontSize);
            gameOptions.previewFont = new Font("Lucida Console", 12);
            gameOptions.fontColor = Color.White;
            gameOptions.gameMode = "The Candy Shoppe";
            gameOptions.audioPath = @"assets\trainSound.mp3";
            gameOptions.videoPath = @"C:\CS.wmv";
            gameOptions.timerOffset = 60;
            gameOptions.videoOffset = 57;
            gameOptions.hintVolume = 10;
            gameOptions.gameVolume = 10;
            gameOptions.gameColumn = 8;
            this.Close();
        }

        /// <summary>
        /// Sets game options for the Dynaline Incident
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void psychWard_Click(object sender, EventArgs e)
        {
            gameOptions.gameMode = "The Dynaline Incident";
            gameOptions.welcomeMessage = "Welcome to the Dynaline Incident";
            gameOptions.hintFontSize = 22;
            gameOptions.hintFont = new Font("Ailerons", gameOptions.hintFontSize);
            gameOptions.previewFont = new Font("Ailerons", 12);
            gameOptions.fontColor = Color.White;
            gameOptions.audioPath = @"assets\dynalineSound.mp3";
            gameOptions.videoPath = @"C:\DI.wmv";
            gameOptions.timerOffset = 95;
            gameOptions.videoOffset = 95;
            gameOptions.hintVolume = 1;
            gameOptions.gameVolume = 5;
            gameOptions.gameColumn = 14;
            this.Close();
        }

        #endregion
    }
}
