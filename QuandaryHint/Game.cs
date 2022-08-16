using System.IO;
using Newtonsoft.Json;

namespace QuandaryHint
{
    public class Game
    {
        #region Variables

        
        public Audio videoSound;
        public Audio hintSound;
        public Audio loopMusic;

        public Video gameVideo;
        public Video previewVideo;

        public Hint gameHint;
        public Hint previewHint;

        //Holds the pause state of the game
        private bool paused = false;

        //A struct to hold game mode information
        public GameOptions gameOptions;

        //Index of the desired waveout device
        public uint waveOutIndex;

        //Logger
        NLog.Logger logger;

        #endregion

        #region Constructors
        /// <summary>
        /// Non-default constructor that takes a GameOptions parameter
        /// </summary>
        /// <param name="gameOpt"></param>
        public Game(GameOptions gameOpt)
        {
            //init logger 
            logger = NLog.LogManager.GetCurrentClassLogger();
            gameHint = new Hint(false);
            previewHint = new Hint(true);

            loopMusic = new Audio();
            hintSound = new Audio();
            hintSound.SetPath(@"assets\hintSound.wav");

            videoSound = new Audio();

            gameVideo = new Video("Game Window");
            previewVideo = new Video("Preview Window");


            ParseGameOptions(gameOpt);


            gameVideo.DisplayFrame();
            previewVideo.DisplayFrame();


            //Debugs for testing without seperate audio
            
            videoSound.SetVolume(0);
            gameVideo.SetMute(false);

           
            

            
        }
        #endregion

        #region Game setup
        /// <summary>
        /// Passes on the values from the passed struct to appropriate
        /// local variables
        /// </summary>
        /// <param name="game"></param>
        private void ParseGameOptions(GameOptions game)
        {
            logger.Info("Starting ParseGameOptions");
            //10 here is a placeholder value to indicate that there is no waveout device selected
            //In theory this breaks if there are 10 sound outputs on a single computer..but I'm
            //counting on that not happening
            if (gameOptions.waveOut != 10)
                SetAudioOutput((uint)game.waveOut);
            else
                logger.Info("Invalid amount of waveOut devices");

            //Copying of the struct
            gameOptions.welcomeMessage = game.welcomeMessage;
            gameOptions.hintFont = game.hintFont;
            gameOptions.gameMode = game.gameMode;
            gameOptions.fontColor = game.fontColor;
            gameOptions.hintFontSize = game.hintFontSize;
            gameOptions.timerOffset = game.timerOffset;
            gameOptions.gameMode = game.gameMode;
            gameOptions.videoPath = game.videoPath;
            gameOptions.audioPath = game.audioPath;
            gameOptions.previewFont = game.previewFont;
            gameOptions.videoOffset = game.videoOffset;
            System.Console.WriteLine("game vo: " + game.videoOffset);
            logger.Info(gameOptions); 


            //Setup the video classes
            gameVideo.SetPath(game.videoPath);
            gameVideo.videoOffset = game.videoOffset;
            previewVideo.SetPath(game.videoPath);
            previewVideo.videoOffset = game.videoOffset;
           
            //Setup the audio classes
            hintSound.SetVolume(game.hintVolume);

            System.Console.WriteLine("gameVolume is " + game.gameVolume);
            gameVideo.SetVolume(game.gameVolume);
            System.Console.WriteLine("Video volume is " + gameVideo.volume);
            loopMusic.SetVolume(game.loopVolume);
            videoSound.SetPath(game.audioPath);

            //Another function to setup hint classes
            gameHint.CopyGameOptions(game);
            previewHint.CopyGameOptions(game);

            //Set the loop audios
            if (game.gameMode == "The Locked In Dead")
            {
                loopMusic.SetPath(@"assets\deadLoop.mp3");
            }
            else if (game.gameMode == "The Dynaline Incident")
            {
                loopMusic.SetPath(@"assets\dynalineLoop.mp3");
            }
            else if (game.gameMode == "The Candy Shoppe")
            {
                logger.Info("Setting candy shop loop");
                loopMusic.SetPath(@"assets\deadLoop.mp3");
            }
            else
            {
                logger.Warn("No loop audio detected for " + game.gameMode);
            }

            logger.Info("ParseGameOptions finished");
        }

        /// <summary>
        /// Sets the game video up properly on the monitor
        /// </summary>
        public void SetupGameWindow()
        {
            logger.Info("SetupGameWindow starting");
            gameVideo.AdjustVideo();
            gameVideo.ToggleBorder();
            SetupHintWindows();
            AlignHintWindows();
            ResetGame();
            loopMusic.PlayLoop();
            logger.Info("SetupGameWindow finished");
        }
        #endregion

        #region Audio handling
        /// <summary>
        /// Sets the waveout device for the audio objects
        /// </summary>
        /// <param name="i"></param>
        public void SetAudioOutput(uint i)
        {
            logger.Info("Setting audio output to device " + i);
            videoSound.SetAudioOutput(i);
            hintSound.SetAudioOutput(i);
            waveOutIndex = i;
        }

        /// <summary>
        /// Plays the sound for pushing hints
        /// </summary>
        public void PlayHint() => hintSound.StartPlayback();
        #endregion

        #region Game State Methods
        /// <summary>
        /// Starts up the video/audio
        /// </summary>
        public void StartGame()
        {
            logger.Info("StartGame starting");
            logger.Info("Setting position of videos");
            gameVideo.SetPosition(0.0, 0.0);
            previewVideo.SetPosition(0.0, 0.0);
           

            logger.Info("Pausing loop music and unmuting game audio");
            loopMusic.PausePlayback();
            gameVideo.SetMute(false);

            logger.Info("Resuming playback for gameVideo, videoSound, and previewVideo");
            gameVideo.ResumePlayback();
            videoSound.StartPlayback();
            previewVideo.ResumePlayback();

            logger.Info("StartGame finished");
           
        }

        /// <summary>
        /// Resumes video/audio
        /// </summary>
        public void ResumeGame()
        {
            logger.Info("ResumeGame starting");
            gameVideo.ResumePlayback();
            videoSound.ResumePlayback();
            previewVideo.ResumePlayback();
            logger.Info("ResumeGame finished");
        }

        /// <summary>
        /// Pauses the video/audio
        /// </summary>
        public void PauseGame()
        {
            logger.Info("PauseGame starting");
            gameVideo.PausePlayback();
            previewVideo.PausePlayback();
            videoSound.PausePlayback();
            gameVideo.UpdatePlaybackPosition();
            logger.Info("PauseGame finished");
        }

        /// <summary>
        /// Rewinds the video/audio
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public void RewindGame(double minutes, double seconds)
        {
            logger.Info("Rewinding game by " + minutes + " minutes and " + seconds + " seconds");

            gameVideo.Rewind(minutes, seconds);
            previewVideo.Rewind(minutes, seconds);
            videoSound.Rewind(minutes, seconds);
        }

        /// <summary>
        /// Fast forwards the video/audio
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public void FastForward(double minutes, double seconds)
        {
            logger.Info("FastForwarding game by " + minutes + " minutes and " + seconds + " seconds");
            gameVideo.FastForward(minutes, seconds);
            previewVideo.FastForward(minutes, seconds);
            videoSound.FastForward(minutes, seconds);
        }

        /// <summary>
        /// Toggles the pausing of the video and audio
        /// </summary>
        public void TogglePaused()
        {
            logger.Info("TogglePaused starting, updating playback position");
            gameVideo.UpdatePlaybackPosition();
            if (!paused)
            {
                logger.Info("Pausing game");
                PauseGame();
                
                paused = true;
               
            }
            else
            {
                logger.Info("Resuming game");
                ResumeGame();
                paused = false;
            }

            logger.Info("TogglePaused finished");

        }

        /// <summary>
        /// Pauses the video, plays victory video for Dynaline games
        /// </summary>
        public void Escape()
        {
            logger.Info("Escape started, toggling pause state");
            TogglePaused();
            if (gameOptions.gameMode == "The Dynaline Incident")
            {
                logger.Info("Dynaline detected, beginning victory video");
                gameVideo.SetPath(@"C:\DI_Victory.wmv");
                previewVideo.SetPath(@"C:\DI_Victory.wmv");
                videoSound.SetPath(@"assets\DI_Victory.mp3");
                videoSound.SetVolume(0);
                gameVideo.SetMute(false);
                StartGame();
            }

            logger.Info("Escape finished");
        }

        /// <summary>
        /// Resets the file paths and displays the starter frame
        /// </summary>
        public void ResetGame()
        {
            // PauseGame();
            logger.Info("ResetGame starting");
            System.Console.WriteLine("video path is " + gameOptions.videoPath);
            gameVideo.SetPath(gameOptions.videoPath);
            previewVideo.SetPath(gameOptions.videoPath);
            videoSound.SetPath(gameOptions.audioPath);
            paused = true;

            gameVideo.DisplayFrame();
            previewVideo.DisplayFrame();
            loopMusic.PlayLoop();
            logger.Info("ResetGame finished");
        }
        #endregion

        #region Hint window methods

        /// <summary>
        /// Sets the hint text of both windows
        /// </summary>
        /// <param name="text"></param>
        public void SetHintText(string text)
        {
            gameHint.SetHint(text);
            previewHint.SetHint(text);
        }

        /// <summary>
        /// Message to be displayed while typing a hint
        /// </summary>
        public void DecoderMessage()
        {
            SetHintText("Incoming Communication...\n\n");
        }

        /// <summary>
        /// Toggles the hint borders
        /// </summary>
        private void ToggleHintBorders()
        {
            gameHint.ToggleBorder();
            previewHint.ToggleBorder();

        }

        /// <summary>
        /// Sets the hint windows at the same location as the video counterparts
        /// </summary>
        public void AlignHintWindows()
        {
            gameHint.SetLocation(gameVideo.GetLocation());
            previewHint.SetLocation(previewVideo.GetLocation());
        }

        /// <summary>
        /// Sets the hint windows to the size of the video counterparts
        /// </summary>
        private void ResizeHintWindows()
        {
            gameHint.SetSize(gameVideo.GetSize());
            previewHint.SetSize(previewVideo.GetSize());
        }

        /// <summary>
        /// Sets up the hint windows for use using private methods
        /// </summary>
        private void SetupHintWindows()
        {
            ToggleHintBorders();
            AlignHintWindows();
            ResizeHintWindows();

            gameHint.ShowWindow();
            previewHint.ShowWindow();
            
        }

        #endregion

        #region Misc 
        /// <summary>
        /// Returns the current escape time as a string
        /// </summary>
        /// <returns></returns>
        public string GetEscapeTime(bool excel)
        {
            logger.Info("GetEscapeTime starting");
            string escape;

            double minutes = (gameVideo.GetPlaybackPosition() - gameOptions.timerOffset) / 60;
            double seconds = (gameVideo.GetPlaybackPosition() - gameOptions.timerOffset) % 60;
            string sec;

            //Make sure seconds under 10 are formatted correctly
            if (seconds < 10)
                sec = "0" + (int)seconds;
            else
                sec = ((int)seconds).ToString();

            if (excel)
                escape = (int)minutes + "." + sec;
            else
                escape = (int)minutes + ":" + sec;


            logger.Info("GetEscapeTime finished with time " + escape);
            return escape;
        }

        /// <summary>
        /// Writes the excel path, volumes, and audio output device to a text file
        /// that will be read upon future startups
        /// </summary>
        /// <param name="excelPath"></param>
        public void WriteConfigFile()
        {
            logger.Info("WriteConfigFile starting");
            gameOptions.videoOffset = gameVideo.videoOffset;
            System.Console.WriteLine("vo: " + gameOptions.videoOffset);
            gameOptions.hintFontSize = gameHint.hintWindowFont;
            gameOptions.hintVolume = hintSound.volume;
            gameOptions.gameVolume = gameVideo.volume;
            gameOptions.loopVolume = loopMusic.volume;
            string json = JsonConvert.SerializeObject(gameOptions);
            StreamWriter sw = new StreamWriter(gameOptions.gameMode + ".json");
            sw.WriteLine(json);
            sw.Close();
            logger.Info("WriteConfigFile finished, wrote to " + gameOptions.gameMode + ".json");
        }
        #endregion
    }
}
