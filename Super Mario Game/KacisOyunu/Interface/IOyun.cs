//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566

using KacisOyunu.Enum;
using System;

namespace KacisOyunu.Interface
{
    internal interface IOyun
    {
        event EventHandler ElapsedTimeChanged;
        event EventHandler GameLevelChanged;
        event EventHandler FinishGame;
        event EventHandler RemainingLifeChanged;
        event EventHandler HighScoreChanged;
        TimeSpan ElapsedTime { get;}
        void StartGame();
        void GameFinish();
        void PauseMu();
        void MovingPlayer(Direction direction);
    }
}
