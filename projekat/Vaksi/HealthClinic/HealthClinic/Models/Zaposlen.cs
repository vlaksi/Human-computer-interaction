﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;        // za podatke/tabelu

namespace HealthClinic.Models
{
    public class Zaposlen : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _ime;
        private string _prezime;
        private string _struka;
        private string _sifra;
        private string _radniKalendar;          
        private string _brojOperacijaOveNedelje;

       

        public string Ime
        {
            get
            {
                return _ime;
            }
            set
            {
                if(value != _ime)
                {
                    _ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }

        public string Prezime
        {
            get
            {
                return _prezime;
            }
            set
            {
                if (value != _prezime)
                {
                    _prezime = value;
                    OnPropertyChanged("Prezime");
                }
            }
        }

        public string Struka
        {
            get
            {
                return _struka;
            }
            set
            {
                if (value != _struka)
                {
                    _struka = value;
                    OnPropertyChanged("Struka");
                }
            }
        }

        public string Sifra
        {
            get
            {
                return _sifra;
            }
            set
            {
                if (value != _sifra)
                {
                    _sifra = value;
                    OnPropertyChanged("Sifra");
                }
            }
        }

        public string RadniKalendar
        {
            get
            {
                return _radniKalendar;
            }
            set
            {
                if (value != _radniKalendar)
                {
                    _radniKalendar = value;
                    OnPropertyChanged("RadniKalendar");
                }
            }
        }

        public string BrojOperacijaOveNedelje
        {
            get
            {
                return _brojOperacijaOveNedelje;
            }
            set
            {
                if (value != _brojOperacijaOveNedelje)
                {
                    _brojOperacijaOveNedelje = value;
                    OnPropertyChanged("BrojOperacijaOveNedelje");
                }
            }
        }

    }
}