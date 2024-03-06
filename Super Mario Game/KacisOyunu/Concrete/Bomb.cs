//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566

using KacisOyunu.Abstract;
using System.Drawing;

namespace KacisOyunu.Concrete
{
    // Bomba sınıfı
    internal class Bomb : Object
    {
        public Bomb() : base(new Size(70, 70))
        {
            Image = Image.FromFile(@"Gorseller\Bomba.png"); 
        }
    }

}
