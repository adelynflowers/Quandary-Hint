using System.Drawing;
using System.Windows.Forms;

namespace QuandaryHint
{
    public partial class videoPane : Form
    {


        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public videoPane()
        {
            InitializeComponent();

            //Sets up the video player properly
            axWindowsMediaPlayer1.Location = new Point(0, 0);
            axWindowsMediaPlayer1.settings.mute = true;
            axWindowsMediaPlayer1.settings.autoStart = false;
            axWindowsMediaPlayer1.Size = new Size(this.Width, this.Height);
            
            
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Sets the video to the last second after it ends
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 1) 
            {
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.currentMedia.duration - 1.001;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
        }
        #endregion

    }
}
