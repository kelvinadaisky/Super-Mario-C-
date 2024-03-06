//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566

using KacisOyunu.Concrete;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KacisOyunu
{

    public partial class AnaMenu : Form
    {
        public static string playerName = "";

        public AnaMenu()
        {
            InitializeComponent();
        }



        private void AnaMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                if (EnterPlayerName.Text != "")
                {
                    playerName = EnterPlayerName.Text;
                    Level level = new Level();

                    level.Show();

                    this.Hide();
                }
                else
                {
                    errorProvider1.SetError(EnterPlayerName, "Please enter your Name");
                }
               
            }
        }

        private void AnaMenu_Load(object sender, EventArgs e)
        {

        }

        

        private void Oyun_tuş_takımı_bilgisi(object sender, EventArgs e)
        {
            MessageBox.Show("Use the arrow keys to move your avatar." +
                 "\r\nPress P to stop the game.", "Game keypad information",
                MessageBoxButtons.OK);
        }

        private void ShowHighScore(object sender, EventArgs e)
        {
            // Assuming you have an instance of the Oyun class
            Oyun oyunInstance = new Oyun(/* pass necessary parameters */);

            // Load the high scores
            List<ScoreRecord> highScores = oyunInstance.LoadScores();

            // Create a string to display the high scores
            string highScoreMessage = "HIGHSCORE\n\n";
            foreach (var scoreRecord in highScores)
            {
                highScoreMessage += $"{scoreRecord.Player} : {scoreRecord.Score}\n";
            }

            // Display the high scores using a MessageBox
            MessageBox.Show(highScoreMessage, "High Scores", MessageBoxButtons.OK);
        }
    }
}
