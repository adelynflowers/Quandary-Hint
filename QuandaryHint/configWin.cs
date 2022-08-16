using System;
using System.Windows.Forms;
using libZPlay;

namespace QuandaryHint
{
    public partial class configWin : Form
    {
        #region Variables
        private Game testGame;
        private ZPlay zplayer;
      
        #endregion

        #region Constructors
        public configWin(Game testGame)
        {
            InitializeComponent();
            

            //Set objects
            this.testGame = testGame;
            zplayer = new ZPlay();

            //Set data-dependent tools
            FontAdjuster.Value = testGame.gameOptions.hintFontSize;
            HintSoundUpDown.Value = testGame.hintSound.volume;
            VideoSoundUpDown.Value = testGame.gameVideo.volume;
            numericUpDown1.Value = testGame.loopMusic.volume;
            

        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Hides the window upon clicking the X
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// Sets the audio output for the game based on the selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void audioSetterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < audioSetterBox.Items.Count; i++)
            {
                if (audioSetterBox.Items[i] == audioSetterBox.Items[audioSetterBox.SelectedIndex])
                {
                    testGame.SetAudioOutput((uint)i);
                }

            }
        }

        /// <summary>
        /// Adjusts the font size of the game window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontAdjuster_ValueChanged(object sender, EventArgs e)
        {
            testGame.gameHint.EditFontSize((int)FontAdjuster.Value);
        }

        /// <summary>
        /// Fills the box with a list of possible audio outputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void audioSetterBox_Click(object sender, EventArgs e)
        {
            audioSetterBox.Items.Clear();
            TWaveOutInfo WaveOutInfo = new TWaveOutInfo();
            int WaveOutNum = zplayer.EnumerateWaveOut();
            uint i;
            for (i = 0; i < WaveOutNum; i++)
            {
                if (zplayer.GetWaveOutInfo(i, ref WaveOutInfo))
                {
                    audioSetterBox.Items.Add(WaveOutInfo.ProductName);
                }
            }
        }

        /// <summary>
        /// Rewinds the game based on the minutes and seconds boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTimeBtn_Click(object sender, EventArgs e)
        {
            testGame.RewindGame((double)minAdjust.Value, (double)secAdjust.Value);
        }
      
        /// <summary>
        /// Adjusts the video sound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VideoSoundUpDown_ValueChanged(object sender, EventArgs e)
        {

            testGame.gameVideo.SetVolume((int)VideoSoundUpDown.Value);
            
            

           
        }

        /// <summary>
        /// Adjusts the hint boop sound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HintSoundUpDown_ValueChanged(object sender, EventArgs e)
        {
            testGame.hintSound.SetVolume((int)HintSoundUpDown.Value);
        }

        /// <summary>
        /// Subtracts time from the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubtractTimeBtn_Click(object sender, EventArgs e)
        {
            testGame.FastForward((double)minAdjust.Value, (double)secAdjust.Value);
        }





        #endregion

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            testGame.loopMusic.SetVolume((int)numericUpDown1.Value);
        }
    }
}