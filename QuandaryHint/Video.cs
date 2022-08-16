using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace QuandaryHint
{
    public class Video
    {
        #region Variables
        //The form we're wrapping
        private videoPane video;

        //The hint window associated with it
        //  private hintWindow hint;

        public int volume = 10;

       

        //An integer to track if we need the video offset
        private int playCount = 0;

        //Tracks where we are in the video
        public double playbackPosition = 0;

        //The offset for the first playback of the video
        public double videoOffset;
        #endregion

        #region Public methods
        /// <summary>
        /// Default constructor
        /// </summary>
        public Video()
        {
            video = new videoPane();
            video.Show();

        }

        #region Video window
        /// <summary>
        /// NDC, takes a string for the window title
        /// </summary>
        /// <param name="name"></param>
        public Video(string name)
        {
            video = new videoPane();
            video.Text = name;

            video.Show();
        }

        /// <summary>
        /// Mute/unmute the video
        /// </summary>
        /// <param name="mute"></param>
        public void SetMute(bool mute)
        {
            video.axWindowsMediaPlayer1.settings.mute = mute;
        }
        /// <summary>
        /// Set the filepath for the video
        /// </summary>
        /// <param name="path"></param>
        public void SetPath(string path)
        {
            video.axWindowsMediaPlayer1.URL = path;
            video.axWindowsMediaPlayer1.settings.mute = true;


        }

        /// <summary>
        /// Used to display a frame when the video starts, eliminating
        /// video offsets and delays forever
        /// </summary>
        /// <returns></returns>
        public async Task DisplayFrame()
        {
            double offset = videoOffset;
            SetPosition(0.0, offset);
            Task wait = Task.Delay(600);
            ResumePlayback();
            await wait;
            PausePlayback();
            System.Console.WriteLine("Pause playback reached");

           

        }

        /// <summary>
        /// Returns the current playback position stored
        /// </summary>
        /// <returns></returns>
        public double GetPlaybackPosition() { return playbackPosition; }
        
        /// <summary>
        /// Starts video playback
        /// </summary>
        public void StartPlayback()
        {

             if (playCount == 0) //The first playback needs a delay for proper sync
             {
                 SetPosition(0.0, videoOffset);
             }
             else //Weirdly enough future playbacks do not.
             {
                 SetPosition(0.0, 0.0);
             }

            SetPosition(0.0, 0.0);
            video.axWindowsMediaPlayer1.Ctlcontrols.play();
            playCount++;
        }

        /// <summary>
        /// Pause video playback
        /// </summary>
        public void PausePlayback() => video.axWindowsMediaPlayer1.Ctlcontrols.pause();

        /// <summary>
        /// Resume video playback
        /// </summary>
        public void ResumePlayback() => video.axWindowsMediaPlayer1.Ctlcontrols.play();

        /// <summary>
        /// Toggles the border of the window from sizable to invisible
        /// </summary>
        public void ToggleBorder()
        {
            if (video.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
            {
                video.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            }
            else
                video.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        /// <summary>
        /// Maximize the window, and resize the video appropriately
        /// </summary>
        public void AdjustVideo()
        {
            video.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            video.axWindowsMediaPlayer1.Size = new System.Drawing.Size(video.Width, video.Height);
        }

        /// <summary>
        /// Fast forwards the video a desired amount
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public void FastForward(double minutes, double seconds)
        {
            double sec = ComputeSeconds(minutes, seconds);
            video.axWindowsMediaPlayer1.Ctlcontrols.currentPosition += sec;
        }

        /// <summary>
        /// Rewinds the video a desired amount
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public void Rewind(double minutes, double seconds)
        {
            double sec = ComputeSeconds(minutes, seconds);

            video.axWindowsMediaPlayer1.Ctlcontrols.currentPosition -= sec;
        }

        /// <summary>
        /// Sets the volume for the player
        /// </summary>
        /// <param name="volume"></param>
        public void SetVolume(int volume)
        {
            if (volume <= 10)
                volume *= 10;

            video.axWindowsMediaPlayer1.settings.volume = volume;
            this.volume = volume / 10;
        }

        /// <summary>
        /// Sets the video position to a desired location
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public void SetPosition(double minutes, double seconds)
        {
            double sec = ComputeSeconds(minutes, seconds);

            video.axWindowsMediaPlayer1.Ctlcontrols.currentPosition = sec;
        }

        /// <summary>
        /// Grabs playback position
        /// </summary>
        public void UpdatePlaybackPosition() => playbackPosition = video.axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

        /// <summary>
        /// Location getter
        /// </summary>
        /// <returns></returns>
        public Point GetLocation()
        {
            return video.Location;
        }

        /// <summary>
        /// Size getter
        /// </summary>
        /// <returns></returns>
        public Size GetSize()
        {
            return video.Size;
        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Turns minutes and seconds into just seconds
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        private double ComputeSeconds(double minutes, double seconds)
        {
            double newSeconds = seconds;
            newSeconds += minutes * 60.0;

            return newSeconds;
        }

     
        #endregion
    }
}
