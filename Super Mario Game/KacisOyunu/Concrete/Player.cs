//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566

using KacisOyunu.Abstract;
using System.Drawing;

namespace KacisOyunu.Concrete
{
    internal class Player : Object
    {
        public Player(Size hareketAlaniBoyutlari) : base(hareketAlaniBoyutlari)
        {
            HareketMesafesi = 70 ;
            Image = Image.FromFile(@"Gorseller\e.png"); 
        }
    }
}
