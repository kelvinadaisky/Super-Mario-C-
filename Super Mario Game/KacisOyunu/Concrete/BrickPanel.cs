//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566


using System.Drawing;
using System.Windows.Forms;

namespace KacisOyunu.Concrete
{
    internal class BrickPanel : Panel
    {
        public BrickPanel()
        {
            Size = new Size (700, 210);
            Location = new Point(70,0);
            BackgroundImage = Image.FromFile(@"Gorseller\bloc.png");
            BackColor = Color.Transparent;
        }
    }
}
