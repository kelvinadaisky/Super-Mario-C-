//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566

using KacisOyunu.Enum;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KacisOyunu.Concrete
{
    internal class Trap : Abstract.Object
    {
        public TrapType TrapType { get; }

        public Trap(TrapType trapType) : base(new Size(70, 70))
        {
            TrapType = trapType;
            InitializeTrap();
            Visible = false;
        }

        private void InitializeTrap()
        {
           
            switch (TrapType)
            {
                case TrapType.Trap:
                    Image = Image.FromFile(@"Gorseller\TuzakBir.png"); 
                    break;
                case TrapType.Bomb:
                    Image = Image.FromFile(@"Gorseller\TuzakIki.png");
                    break;
                case TrapType.Rocket:
                    Image = Image.FromFile(@"Gorseller\TuzakUc.png");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SizeMode = PictureBoxSizeMode.AutoSize;
        }
    }
}
