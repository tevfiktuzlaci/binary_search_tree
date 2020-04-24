﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İkiliAramaAgaci
{
    public class İkiliAramaAgaci
    {
        private İkiliAramaAgacDugumu kok;
        private string dugumler;
        public İkiliAramaAgaci()
        {         
        }
        public İkiliAramaAgaci(İkiliAramaAgacDugumu kok)
        {
            this.kok = kok;
        }
        public int DugumSayisi()
        {
            return DugumSayisi(kok);
        }
        public int DugumSayisi (İkiliAramaAgacDugumu dugum)
        {
            throw new NotImplementedException();

        }
        public int YaprakSayisi()
        {
            return YaprakSayisi(kok);
        }
        public int YaprakSayisi(İkiliAramaAgacDugumu dugum)
        {
            throw new NotImplementedException();
  
        }
        public string DugumleriYazdir()
        {
            return dugumler;
        }
        public void PreOrder()
        {
            dugumler = "";
            PreOrderInt(kok);
        }
        private void PreOrderInt(İkiliAramaAgacDugumu dugum)
        {
            if (dugum == null)
                return;
            Ziyaret(dugum);
            PreOrderInt(dugum.sol);
            PreOrderInt(dugum.sag);
        }
        public void InOrder()
        {
            dugumler = "";
            InOrderInt(kok);
        }
        private void InOrderInt(İkiliAramaAgacDugumu dugum)
        {
            if (dugum == null)
                return;
            InOrderInt(dugum.sol);
            Ziyaret(dugum);
            InOrderInt(dugum.sag);
        }
        private void Ziyaret(İkiliAramaAgacDugumu dugum)
        {
            dugumler += dugum.veri + " ";
        }
        public void PostOrder()
        {
            dugumler = "";
            PostOrderInt(kok);
        }
        private void PostOrderInt(İkiliAramaAgacDugumu dugum)
        {
            if (dugum == null)
                return;
            PostOrderInt(dugum.sol);
            PostOrderInt(dugum.sag);
            Ziyaret(dugum);
        }
        public void Ekle(int deger)
        {
            //Yeni eklenecek düğümün parent'ı
            İkiliAramaAgacDugumu tempParent = new İkiliAramaAgacDugumu();
            //Kökten başla ve ilerle
            İkiliAramaAgacDugumu tempSearch = kok;

            while (tempSearch != null)
            {
                tempParent = tempSearch;
                //Deger zaten var, çık.
                if (deger == (int)tempSearch.veri)
                    return;
                else if (deger < (int)tempSearch.veri)
                    tempSearch = tempSearch.sol;
                else
                    tempSearch = tempSearch.sag;
            }
            İkiliAramaAgacDugumu eklenecek = new İkiliAramaAgacDugumu(deger);
            //Ağaç boş, köke ekle
            if (kok == null)
                kok = eklenecek;
            else if (deger < (int)tempParent.veri)
                tempParent.sol = eklenecek;
            else
                tempParent.sag = eklenecek;
        }
        public İkiliAramaAgacDugumu Ara(int anahtar)
        {
            return AraInt(kok, anahtar);
        }
        private İkiliAramaAgacDugumu AraInt(İkiliAramaAgacDugumu dugum, 
                                            int anahtar)
        {
            if (dugum == null)
                return null;
            else if ((int)dugum.veri == anahtar)
                return dugum;
            else if ((int)dugum.veri > anahtar)
                return (AraInt(dugum.sol, anahtar));
            else
                return (AraInt(dugum.sag, anahtar));
        }
        
        public İkiliAramaAgacDugumu MinDeger()
        {
            İkiliAramaAgacDugumu tempSol = kok;
            while (tempSol.sol != null)
                tempSol = tempSol.sol;
            return tempSol;
        }

        public İkiliAramaAgacDugumu MaksDeger()
        {
            İkiliAramaAgacDugumu tempSag = kok;
            while (tempSag.sag != null)
                tempSag = tempSag.sag;
            return tempSag;
        }
        
        private İkiliAramaAgacDugumu Successor(İkiliAramaAgacDugumu silDugum)
        {
            İkiliAramaAgacDugumu temp = silDugum.sag;
            while (temp.sol != null)
            {
                temp = temp.sol;
            }
            return temp;

        }
        
        public bool Sil(int deger)
        {
            İkiliAramaAgacDugumu current = kok;
            İkiliAramaAgacDugumu parent = kok;
            bool issol = true;
            //DÜĞÜMÜ BUL
            while ((int)current.veri != deger)
            {
                parent = current;
                if (deger < (int)current.veri)
                {
                    issol = true;
                    current = current.sol;
                }
                else
                {
                    issol = false;
                    current = current.sag;
                }
                if (current == null)
                    return false;
            }
            //DURUM 1: YAPRAK DÜĞÜM
            if (current.sol == null && current.sag == null)
            {
                if (deger < (int)parent.veri)
                {
                    parent.sol = null;
                }
                else
                {
                    parent.sag = null;
                }
                
            }
            //DURUM 2: TEK ÇOCUKLU DÜĞÜM
            else if (current.sag == null)
            {
                if(parent.sag == current)
                {
                    parent.sag = current.sol;
                    current = null;
                }
                else
                {
                    parent.sol = current.sol;
                    current = null;
                }
            }
            else if (current.sol == null)
            {
                if (parent.sag == current)
                {
                    parent.sag = current.sag;
                    current = null;
                }
                else
                {
                    parent.sol = current.sag;
                    current = null;
                }
                
            }
            //DURUM 3: İKİ ÇOCUKLU DÜĞÜM
            else
            {
                İkiliAramaAgacDugumu successor = Successor(current);
                if (successor != null)
                {
                    object tempVeri = successor.veri;
                    Sil((int)successor.veri);
                    current.veri = tempVeri;
                }
                else
                {
                    return false;
                }

            }
            return true;
        } 

    }
}
