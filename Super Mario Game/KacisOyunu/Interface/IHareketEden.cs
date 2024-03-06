//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566

using KacisOyunu.Enum;
using System.Drawing;

namespace KacisOyunu.Interface
{
    internal interface IHareketEden
    {
        Size HareketAlaniBoyutlari { get; }

        int HareketMesafesi {  get; }
        /// <summary>
        /// Cismi istenilen yonde hareket ettirir
        /// </summary>
        /// <param name="yon">Hangi yone hareket edilecegi</param>
        /// <returns>Cisim duvara carparsa  true dondurur.</returns>
        bool HareketEttir(Direction yon);
    }
}
