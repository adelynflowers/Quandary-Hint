using System.Drawing;
using System.Windows.Forms;

namespace QuandaryHint
{
    public class Hint
    {
        #region Variables
        //The window we're wrapping
        hintWindow hint;

        //Is it the preview window?
        bool preview;

        //Stores the current font size
        public int hintWindowFont;
        #endregion

        #region Constructors
        /// <summary>
        /// NDC taking a bool
        /// for if it's a preview window or the game window
        /// </summary>
        /// <param name="name"></param>
        public Hint(bool preview)
        {
            hint = new hintWindow();
            this.preview = preview;
            SetDoubleBuffered(hint);
          
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the game options and sets up the label stylistically
        /// </summary>
        /// <param name="gameO"></param>
        public void CopyGameOptions(GameOptions gameO)
        {
            if (preview)
                hint.hintLabel.Font = gameO.previewFont;
            else
                hint.hintLabel.Font = gameO.hintFont;

            hintWindowFont = gameO.hintFontSize;
            hint.hintLabel.ForeColor = gameO.fontColor;
            hint.Text = gameO.gameMode + " Hints";

            if (gameO.gameMode == "The Dynaline Incident")
            {
                hint.hintLabel.TextAlign = ContentAlignment.TopCenter;
               
                hint.hintLabel.Padding = new Padding(0, 50, 0, 0);
            }
            if (gameO.gameMode == "The Candy Shoppe" && !this.preview)
            {
                hint.hintLabel.Padding = new Padding(150, 0, 150, 75);
            }

        }

        /// <summary>
        /// Set the label to a string
        /// </summary>
        /// <param name="hintText"></param>
        public void SetHint(string hintText)
        {
            hint.hintLabel.Text = hintText;
        }

        /// <summary>
        /// Toggles the border on and off
        /// </summary>
        public void ToggleBorder()
        {
            if (hint.FormBorderStyle == FormBorderStyle.Sizable)
            {
                hint.FormBorderStyle = FormBorderStyle.None;
            }
            else
            {
                hint.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }

        /// <summary>
        /// Location getter
        /// </summary>
        /// <returns></returns>
        public Point GetLocation()
        {
            return hint.Location;
        }

        /// <summary>
        /// Location setter
        /// </summary>
        /// <param name="pt"></param>
        public void SetLocation(Point pt)
        {
            hint.Location = pt;
        }

        /// <summary>
        /// Size setter
        /// </summary>
        /// <param name="sz"></param>
        public void SetSize(Size sz)
        {
            hint.Size = sz;
        }

        /// <summary>
        /// Shows the window
        /// </summary>
        public void ShowWindow() => hint.Show();

        /// <summary>
        /// Changes the font size and stores it in a variable for later retrieval
        /// </summary>
        /// <param name="size"></param>
        public void EditFontSize(int size)
        {
            string holderFont = hint.hintLabel.Font.Name;
            hint.hintLabel.Font = new Font(holderFont, size);
            hintWindowFont = size;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Used to keep the window from flickering so much during use
        /// </summary>
        /// <param name="c"></param>
        private static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }
        #endregion

    }
}
