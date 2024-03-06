//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566

using KacisOyunu.Enum;
using System;
using System.Drawing;

namespace KacisOyunu.Concrete
{
    internal class MysteryBox : Abstract.Object
    {
        public MysteryBoxType MysteryBoxTuru { get; }

        public MysteryBox(MysteryBoxType mysteryBoxTuru) : base(new Size(70, 70))
        {
            Image = Image.FromFile(@"Gorseller\MysteryBox.png");
            MysteryBoxTuru = mysteryBoxTuru;
            InitializeTrap();
        }

        private void InitializeTrap()
        {
            
            switch (MysteryBoxTuru)
            {
                case MysteryBoxType.Gift:
                    Image = Image.FromFile(@"Gorseller\MysteryBox.png"); 
                    break;
                case MysteryBoxType.Trap:
                    Image = Image.FromFile(@"Gorseller\MysteryBox.png"); 
                    break;
              
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }

   
}
