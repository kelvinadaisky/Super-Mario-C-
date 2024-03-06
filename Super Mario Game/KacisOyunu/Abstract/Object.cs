
//STACY KELVIN ADAISKY IRUTINGABO
//BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
//B211200566

using KacisOyunu.Enum;
using KacisOyunu.Interface;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KacisOyunu.Abstract
{
    internal abstract class Object : PictureBox , IHareketEden
    {
       

        public Size HareketAlaniBoyutlari { get; }
        public int HareketMesafesi { get; protected set; }


        public new int Right 
        {
            get => base.Right;
            set => Left = value - Width; 
        }
        public new int Bottom
        {
            get => base.Bottom;
            set => Top = value - Width;
        }

        protected Object(Size hareketAlaniBoyutlari)
        {
            HareketAlaniBoyutlari = hareketAlaniBoyutlari;
        }
        public bool HareketEttir(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return YukariHareketettir();
                    
                case Direction.Down:
                    return AsagiHareketettir();
                    
                case Direction.Right:
                    return SagaHareketettir();
                  
                case Direction.Left:
                    return SolaHareketettir();
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private bool SolaHareketettir()
        {
            if (Left == 0) return true;
            var yeniLeft = Left - HareketMesafesi;
            var tasacakMi = yeniLeft < 0;
            Left = tasacakMi ? 0 : yeniLeft;
            return Left == 0;
        }

        private bool SagaHareketettir()
        {
            if (Right == HareketAlaniBoyutlari.Width) return true;
            var yeniRight = Right + HareketMesafesi;
            var tasacakMi = yeniRight > HareketAlaniBoyutlari.Width;
            Right = tasacakMi ? HareketAlaniBoyutlari.Width  : yeniRight;
            
            return Right == HareketAlaniBoyutlari.Width;
        }

        private bool AsagiHareketettir()
        {

            if (Bottom == HareketAlaniBoyutlari.Height) return true;
            var yeniBottom = Bottom + HareketMesafesi;
            var tasacakMi = yeniBottom > HareketAlaniBoyutlari.Width;
            Bottom = tasacakMi ? HareketAlaniBoyutlari.Height : yeniBottom;

            return Bottom == HareketAlaniBoyutlari.Height;
        }

        private bool YukariHareketettir()
        {
            if (Top == 0) return true;
            var yeniTop = Top - HareketMesafesi;
            var tasacakMi = yeniTop < 0;
            Top = tasacakMi ? 0 : yeniTop;
            return Top == 0;
        }

      
    }
}
