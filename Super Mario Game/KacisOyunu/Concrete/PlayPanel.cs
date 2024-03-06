//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566

using System.Drawing;
using System.Windows.Forms;

namespace KacisOyunu.Concrete
{
    internal class PlayPanel : Panel
    {

        public PlayPanel()
        {
            Size = new Size(840,210);
            BorderStyle = BorderStyle.FixedSingle;
            Location = new Point(250, 180);
            BackgroundImage = Image.FromFile(@"Gorseller\13187581_1601.m10.i311.n029.S.c1.jpg");

        }
    }
}