//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566

using KacisOyunu.Concrete;
using System;
using System.Windows.Forms;

namespace KacisOyunu
{
    public partial class Level : Form
    {

        public static int GameLevel { get; set; } = 1;
        public static int RemainingLife { get; set; } = 3;

        public static TimeSpan ElapsedTime;

        public static int Score { get; set; } = 0;


        private Oyun _oyun;
        public Level()
        {
            
            InitializeComponent();
            _oyun = new Oyun(anaPanel, AnaMenu.playerName);
            _oyun.ElapsedTimeChanged += Oyun_ElapsedTimeChanged;
            _oyun.GameLevelChanged += _Oyun_GameLevelChanged;
            _oyun.HighScoreChanged += _oyun_HighScoreDegisti;
            _oyun.RemainingLifeChanged += _Oyun_KalanCanDegisti;
            _oyun.FinishGame += _oyun_FinishGame;
            OyuncuIsmi.Text = AnaMenu.playerName;
            OyunSeviyesiLabel.Text = GameLevel.ToString();
            KalanCanLabel.Text = RemainingLife.ToString();
            PuanLabel.Text = Score.ToString();
            sureLabel.Text = ElapsedTime.TotalSeconds.ToString("0");

        }

        private void _oyun_HighScoreDegisti(object sender, EventArgs e)
        {
            Score = _oyun.HighScore;
            PuanLabel.Text = Score.ToString();
        }

        private void _Oyun_KalanCanDegisti(object sender, EventArgs e)
        {
            RemainingLife = _oyun.RemainingLife;
            KalanCanLabel.Text = RemainingLife.ToString();
        }

        private void _oyun_FinishGame(object sender, EventArgs e)
        {
            _oyun.SaveScore(OyuncuIsmi.Text,_oyun.HighScore);

            // Reset properties
            GameLevel = 1;
            RemainingLife = 3;
            ElapsedTime = TimeSpan.Zero;
            Score = 0;

            // Reset labels
            OyunSeviyesiLabel.Text = GameLevel.ToString();
            KalanCanLabel.Text = RemainingLife.ToString();
            PuanLabel.Text = Score.ToString();
            sureLabel.Text = ElapsedTime.TotalSeconds.ToString("0");

            AnaMenu newAnaMenu = new AnaMenu();
            newAnaMenu.Show();

            this.Close();

        }

        private void _Oyun_GameLevelChanged(object sender, EventArgs e)
        {
            GameLevel = _oyun.GameLevel;
            // Create and display a new Level form
            Level newLevel = new Level();
            newLevel.Show();
            // Close the current form
            this.Close();
        }

        private void LevelOne_Load(object sender, EventArgs e)
        {
            _oyun.StartGame();
            OyunSeviyesiLabel.Text = _oyun.GameLevel.ToString();
        }
        private void Oyun_ElapsedTimeChanged(object sender, EventArgs e)
        {
            ElapsedTime = _oyun.ElapsedTime;
            sureLabel.Text = _oyun.ElapsedTime.TotalSeconds.ToString("0");
           
        }

        private void LevelOne_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    _oyun.MovingPlayer(Enum.Direction.Up);
                    break;
                case Keys.Down:
                    _oyun.MovingPlayer(Enum.Direction.Down);
                    break;
                case Keys.Left:
                    _oyun.MovingPlayer(Enum.Direction.Left);
                    break;
                case Keys.Right:
                    _oyun.MovingPlayer(Enum.Direction.Right);
                    break;
                case Keys.P:
                    _oyun.PauseMu();
                    break;

            }
        }
    }
}
