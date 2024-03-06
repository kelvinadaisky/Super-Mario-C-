//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566

using KacisOyunu.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KacisOyunu.Enum;
using System.IO;


namespace KacisOyunu.Concrete
{
    public class ScoreRecord
    {
        public string Player { get; set; }
        public int Score { get; set; }
    }

    public class Oyun : IOyun
    {

        #region Alanalar

        private readonly Timer _elapsedTimeTimer = new Timer { Interval = 1000 };
        private Timer _movingBombTimer = new Timer { Interval = 3000 };
        private Timer _addRocketTimer = new Timer { Interval = 2000 };
        private Timer _movingRocketTimer = new Timer { Interval = 1000 };
        private Timer _mysteryBlockTimer = new Timer { Interval = 10000 };



        private Trap _trap;
        private Bomb _bomba;
        private MysteryBox _mysteryBox;
        private Rocket _rocket; 
        private TimeSpan _elapsedTime;
        private string _playerName;
        private Panel _gameAreaPanel;
        private Panel _brickPanel;
        private Panel _anaPanel;
        private Player _player;
        private Random _random = new Random();
        private int _remainingLife;
        private int _gameLevel;
        private bool _gameFinished;
        public int _highScore;
        // Add a boolean variable to track whether the game is paused
        private bool _gamePaused = false;

        private const string FileScores = "Highscore.txt";


        #endregion

        #region Olaylar

        public event EventHandler ElapsedTimeChanged;
        public event EventHandler GameLevelChanged;
        public event EventHandler FinishGame;
        public event EventHandler RemainingLifeChanged;
        public event EventHandler HighScoreChanged;

        #endregion

        #region Ozellikler
        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                _elapsedTime = value;
                ElapsedTimeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public int GameLevel
        {
            get => _gameLevel;
            set
            {
                _gameLevel = value;
                GameLevelChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public bool GameFinished
        {
            get => _gameFinished;
            set
            {
                _gameFinished = value;
                FinishGame?.Invoke(this, EventArgs.Empty);
            }
        }
        public int RemainingLife
        {
            get => _remainingLife;
            set
            {
                _remainingLife = value;
                RemainingLifeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int HighScore
        {
            get => _highScore;
            set
            {
                _highScore = value;
                HighScoreChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Metotlar

        public Oyun(Panel anaPanel , String PlayerName)
        {
            _anaPanel = anaPanel;
            _elapsedTimeTimer.Tick += ElapsedTime_Tick;
            _movingBombTimer.Tick += MovingBombTimer_Tick;
            _addRocketTimer.Tick += AddRocketTimer_Tick;
            _movingRocketTimer.Tick += MovingRocketTimer_Tick;
            _mysteryBlockTimer.Tick += MysteryBlockTimer_Tick;
            _remainingLife = Level.RemainingLife;
            _gameLevel = Level.GameLevel;
            _elapsedTime = Level.ElapsedTime;
            _highScore = Level.Score;
            _playerName = PlayerName;

        }

        public Oyun()
        {

        }

        public void StartGame()
        {
            
            _gameAreaPanel = PlayPanelEkle();

            _gameAreaPanel.Controls.Add(BrickPanelEkle());

            _brickPanel = BrickPanelEkle();

            _mysteryBlockTimer.Start();

            _elapsedTimeTimer.Start();

            switch (_gameLevel)
            {
                case 1:
                    AddTrap(_brickPanel);
                    MysteryBoxEkle(_brickPanel);
                    break;
                case 2:
                    ++RemainingLife;
                    _movingBombTimer.Start();
                    BombaEkle(_brickPanel);
                    MysteryBoxEkle(_brickPanel);
                    break;
                case 3:
                    ++RemainingLife;
                    _addRocketTimer.Start();
                    _movingRocketTimer.Start();
                    AddRocket(_brickPanel);
                    MysteryBoxEkle(_brickPanel);
                    break;
            }

            CreatePlayer();

        }

        private Panel PlayPanelEkle()
        {
            PlayPanel playPanel = new PlayPanel();
            _anaPanel.Controls.Add(playPanel);
            return playPanel;
        }

        private Panel BrickPanelEkle()
        {
            BrickPanel brickPanel = new BrickPanel();
            return brickPanel;
        }


        private void CreatePlayer()
        {

            _player = new Player(_gameAreaPanel.Size)
            {
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            _gameAreaPanel.Controls.Add(_player);
            _player.Location = new Point(0, 70);
            _player.BringToFront();
        }


        private List<Point> GenerateRandomLocations(int numberOfLocations)
        {
            List<Point> locations = new List<Point>();

            for (int i = 0; i < numberOfLocations; i++)
            {
                // Randomly generate row and column indices
                int ligne = _random.Next(3);
                int colonne = _random.Next(10) + 1;

                Point location = new Point(colonne, ligne);

                // Make sure the same location is not added twice
                if (!locations.Contains(location))
                {
                    locations.Add(location);
                }
                else
                {
                    i--; // Repeat the iteration to generate a new unique location
                }
            }

            return locations;
        }
        private void MysteryBlockTimer_Tick(object sender, EventArgs e)
        {
            MysteryBoxEkle(_brickPanel);
        }

        private void MysteryBoxEkle(Panel carreauxPanel)
        {
                List<Point> randomLocations = GenerateRandomLocations(1);

            int percentage = _random.Next(1, 101);

            MysteryBoxType mysteryBoxTuru; 


            if (percentage <= 80)
            {
                mysteryBoxTuru = MysteryBoxType.Gift;
            }
            else
            {
                mysteryBoxTuru = MysteryBoxType.Trap;
            }

            foreach (Point location in randomLocations)
                {

                _mysteryBox = new MysteryBox(mysteryBoxTuru)
                {
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Location = new Point(location.X * 70, location.Y * 70)
                };
                _gameAreaPanel.Controls.Add(_mysteryBox);
                _mysteryBox.BringToFront();
                }
        }


        private void AddTrap(Panel carreauxPanel)
        {
            List<Point> emplacementsAleatoires = GenerateRandomLocations(10);

            foreach (Point emplacement in emplacementsAleatoires)
            {
                TrapType tuzakTuru = (TrapType)_random.Next(System.Enum.GetValues(typeof(TrapType)).Length);

                _trap = new Trap(tuzakTuru)
                {
                    SizeMode = PictureBoxSizeMode.AutoSize
                };
                _trap.Location = new Point(emplacement.X * 70, emplacement.Y * 70);
                _gameAreaPanel.Controls.Add(_trap);
                _trap.BringToFront();
            }

        }


        private void BombaEkle(Panel brickPanel)
        {
            // Check if the game is paused
            if (_gamePaused)
                return;
            // Delete all existing bombs
            foreach (Control control in _gameAreaPanel.Controls.OfType<Bomb>().ToList())
            {
                _gameAreaPanel.Controls.Remove(control);
                control.Dispose(); 
            }

            // Add new bombs at random positions
            List<Point> bombLocations = GenerateRandomLocations(10);

            foreach (Point konum in bombLocations)
            {
                _bomba = new Bomb()
                {
                    SizeMode = PictureBoxSizeMode.AutoSize
                };
                _bomba.Location = new Point(konum.X * 70, konum.Y * 70);
                _gameAreaPanel.Controls.Add(_bomba);
                _bomba.BringToFront();

                if (_player != null)
                    if (_player.Bounds.IntersectsWith(_bomba.Bounds))
                    {
                        --RemainingLife;
                        if (RemainingLife == -1)
                        {
                            RemainingLife = 0;
                            _elapsedTimeTimer.Stop();
                            _addRocketTimer.Stop();
                            _movingRocketTimer.Stop();
                            _movingBombTimer.Stop();
                            MessageBox.Show("You lost. You can try again", "Defeat", MessageBoxButtons.OK);
                            GameFinish();
                        }

                    }

            }

        }


        private void MovingBombTimer_Tick(object sender, EventArgs e)
        {
            BombaEkle(_brickPanel);
        }


        private void AddRocket(Panel brickPanel)
        {
            List<Point> dusmanKonumlari = GenerateRandomLocations(1);

            foreach (Point konum in dusmanKonumlari)
            {
                _rocket = new Rocket()
                {
                    SizeMode = PictureBoxSizeMode.AutoSize
                };
                _rocket.Location = new Point((brickPanel.Width), konum.Y * 70);
                _gameAreaPanel.Controls.Add(_rocket);
                _rocket.BringToFront();
            }
        }
        private void RocketHareketEttir()
        {
            if (_gamePaused)
                return;
            foreach (Control control in _gameAreaPanel.Controls.OfType<Rocket>().ToList())
            {
                control.Left -= 70; // Move the block forward

                if (control.Location.X == 70)
                {
                    _gameAreaPanel.Controls.Remove(control);
                    control.Dispose(); // Free up resources
                }

                if (_player != null)
                    if (_player.Bounds.IntersectsWith(control.Bounds))
                    {
                        --RemainingLife;

                        if (RemainingLife == -1)
                        {
                            RemainingLife = 0;
                            _elapsedTimeTimer.Stop();
                            _addRocketTimer.Stop();
                            _movingRocketTimer.Stop();
                            _movingBombTimer.Stop();
                            MessageBox.Show("You lost. You can try again", "Defeat", MessageBoxButtons.OK);
                            GameFinish();
                        }
                    }

            }
        }


        private void MovingRocketTimer_Tick(object sender, EventArgs e)
        {
            RocketHareketEttir();

        }

        private void AddRocketTimer_Tick(object sender, EventArgs e)
        {
            AddRocket(_brickPanel);

        }

        public void MovingPlayer(Direction yon)
        {
            // Check if the game is paused
            if (_gamePaused)
                return;
            if (_player.Location.X == 770) return;

            _player.HareketEttir(yon);


            foreach (Control control in _gameAreaPanel.Controls)
            {
                if (control.Bounds.IntersectsWith(_player.Bounds))
                {
                    if (control is Trap tuzak)
                    {
                        tuzak.Visible = true;
                        tuzak.BringToFront();
                        --RemainingLife;
                    }
                    else if (control is Bomb)
                    {
                        --RemainingLife;
                    }
                    else if (control is Rocket)
                    {
                        --RemainingLife;
                    }
                    else if (control is MysteryBox mysteryBox)
                    {
                        if (mysteryBox.MysteryBoxTuru == MysteryBoxType.Gift)
                        {
                            ++RemainingLife;
                        }
                        else
                        {
                            --RemainingLife;
                        }

                        _gameAreaPanel.Controls.Remove(control);
                        control.Dispose();
                    }
                    if (RemainingLife == -1)
                    {
                        RemainingLife = 0;
                        _elapsedTimeTimer.Stop();
                        _addRocketTimer.Stop();
                        _movingRocketTimer.Stop();
                        _movingBombTimer.Stop();
                        MessageBox.Show("You lost.You can try again", "Defeat", MessageBoxButtons.OK);
                        GameFinish();
                    }
                }
            }

            if (_player.Location.X >= 70 && _player.Location.X <= 770)
            {
                _player.Image = Image.FromFile(@"Gorseller\kosanAdam.png");

            }
            if (_player.Location.X < 70)
            {
                _player.Image = Image.FromFile(@"Gorseller\e.png");
            }
            if (_player.Location.X == 770)
            {

                _player.Image = Image.FromFile(@"Gorseller\kosanAdamSeviyeBitti.png");
                _elapsedTimeTimer.Stop();
                _addRocketTimer.Stop();
                _movingRocketTimer.Stop();
                _movingBombTimer.Stop();
                UptadeHighscore();

                if (GameLevel == 3)
                {
                    
                    MessageBox.Show("Congratulation " + _playerName + "!You completed the game with the score of" + Level.Score, "Victory", MessageBoxButtons.OK);
                    GameFinish();
                    
                }
                else
                {
                    // Display a message to inform that the player will move to the next level
                    MessageBox.Show($"Well done! You will move to level {GameLevel + 1}!", "Next level", MessageBoxButtons.OK);

                    // Go to the next level
                    GameLevelIncrease();

                }
            }

        }

        private void ElapsedTime_Tick(object sender, EventArgs e)
        {
            ElapsedTime += TimeSpan.FromSeconds(1);
        }
       
        private void GameLevelIncrease()
        {
            Level.ElapsedTime = ElapsedTime;
            Level.GameLevel = ++GameLevel;
        }


        public void GameFinish()
        {
            _elapsedTimeTimer.Stop();
            GameFinished = true;

        }
        public void PauseMu()
        {

                _gamePaused = !_gamePaused;

                if (_gamePaused)
                {
                    // Pause the timers
                    _elapsedTimeTimer.Stop();
                    _addRocketTimer.Stop();
                    _movingRocketTimer.Stop();
                    _movingBombTimer.Stop();
                    _mysteryBlockTimer.Stop();
                }
                else
                {
                    _elapsedTimeTimer.Start();
                    _mysteryBlockTimer.Start();
                switch (_gameLevel)
                {
                    case 2:
                        _movingBombTimer.Start();
                        break;
                    case 3:
                        _addRocketTimer.Start();
                        _movingRocketTimer.Start();
                        break;
                }

                }

        }

        public void UptadeHighscore()
        {
            // Calculate points based on level, time remaining, etc.
        
            HighScore += RemainingLife * 500 + (1000 - Convert.ToInt32(ElapsedTime.TotalSeconds.ToString("0")));

            MessageBox.Show($"you earned {HighScore} points!", "Points Earned", MessageBoxButtons.OK);
        }



        // Method to record the score
        public void SaveScore(string joueur, int score)
        {
            // Load existing scores
            List<ScoreRecord> scores = LoadScores();

            // Add the new score
            scores.Add(new ScoreRecord { Player = joueur, Score = score });

            // Sort scores in descending order
            scores.Sort((x, y) => y.Score.CompareTo(x.Score));

            // Keep only the 5 best scores
            scores = scores.GetRange(0, Math.Min(scores.Count, 5));

            // Save scores to file
            SaveScores(scores);
        }

        // Method to load scores from file
        public List<ScoreRecord> LoadScores()
        {
            List<ScoreRecord> scores = new List<ScoreRecord>();

            try
            {
                if (File.Exists(FileScores))
                {
                    using (StreamReader sr = new StreamReader(FileScores))
                    {
                        string ligne;
                        while ((ligne = sr.ReadLine()) != null)
                        {
                            string[] elements = ligne.Split(':');
                            if (elements.Length == 2 && int.TryParse(elements[1], out int score))
                            {
                                scores.Add(new ScoreRecord { Player = elements[0], Score = score });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading scores : {ex.Message}");
            }

            return scores;
        }

        // Method to save scores to file
        public void SaveScores(List<ScoreRecord> scores)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FileScores))
                {
                    sw.WriteLine("HIGHSCORE");
                    sw.WriteLine("");
                    foreach (var record in scores)
                    {
                        sw.WriteLine($"{record.Player} : {record.Score}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving scores : {ex.Message}");
            }
        }

            #endregion
    }
}
