//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566

using KacisOyunu.Abstract;
using System.Drawing;


namespace KacisOyunu.Concrete
{
        internal class Rocket : Object
    {
        public Rocket() : base(new Size(70, 70))
        {
            Image = Image.FromFile(@"Gorseller\askerDusman.png"); // Rocket resmini 
            
        }
    }

}
