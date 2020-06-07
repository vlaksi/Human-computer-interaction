﻿using HealthClinic.Dialogs;
using HealthClinic.Models;
using HealthClinic.Utilities;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using HealthClinic.Views.Dialogs.Brisanje;

namespace HealthClinic.ViewModels
{
    public class ZaposleniViewModel:ObservableObject
    {   
        public ZaposleniViewModel()
        {
            PieChart();                // init piecharta

            ucitavanjePodatakaUTabelu();

            DodajZaposlenogCommand = new RelayCommand(DodajZaposlenog);
            IzmeniZaposlenogCommand = new RelayCommand(IzmeniZaposlenog);
            GenerisiIzvestajZaposlenogCommand = new RelayCommand(PrikaziDijalogGenerisanjaIzvestaja);
            RadniKalendarCommand = new RelayCommand(PrikaziRadniKalendar);
            IzbrisiZaposlenogCommand = new RelayCommand(IzbrisiZaposlenog);

            PocetniDatum = DateTime.Now.Date;
            KrajnjiDatum = DateTime.Now.Date;

            // potvrdjujem izmenjene podatke
            PotvrdaIzmenePodatakaCommand = new RelayCommand(PotvrdaIzmenePodataka);

            // potvrdjujem dodavanje podataka
            PotvrdaDodavanjaPodatakaCommand = new RelayCommand(PotvrdaDodavanjaPodataka);

            // potvrdjujem brisanje podataka
            PotvrdaBrisanjaPodatakaCommand = new RelayCommand(PotvrdaBrisanjaPodataka);

        }

        #region Radno vreme zaposlenih

        private DateTime _pocetniDatum;

        public DateTime PocetniDatum
        {
            get { return _pocetniDatum; }
            set { _pocetniDatum = value; OnPropertyChanged("PocetniDatum"); }
        }

        private DateTime _krajnjiDatum;

        public DateTime KrajnjiDatum
        {
            get { return _krajnjiDatum; }
            set { _krajnjiDatum = value; OnPropertyChanged("KrajnjiDatum"); }
        }


        private DateTime _pocetniSat;

        public DateTime PocetniSat
        {
            get { return _pocetniSat; }
            set { _pocetniSat = value; OnPropertyChanged("PocetniSat"); }
        }

        private DateTime _krajnjiSat;

        public DateTime KrajnjiSat
        {
            get { return _krajnjiSat; }
            set { _krajnjiSat = value; }
        }


        #endregion

        #region Selektovani zaposlen

        private Zaposlen _selektovaniZaposleni;


        // Bajndujem na SelectedItem u tabeli i dalje radim sa njim sta hocu
        // mogu ga dalje prikazivati
        // a moze se i proslediti u dijalog
        // tako sto se .DatContext tog dijalog postavi na this
        public Zaposlen SelektovaniZaposleni
        {
            get { return _selektovaniZaposleni; }
            set { _selektovaniZaposleni = value; OnPropertyChanged("SelektovaniZaposleni"); }
        }

        #endregion

        #region Zaposleni za izmenu

        private Zaposlen _zaposleniZaIzmenu;

        public Zaposlen ZaposleniZaIzmenu
        {
            get { return _zaposleniZaIzmenu; }
            set { _zaposleniZaIzmenu = value; OnPropertyChanged("ZaposleniZaIzmenu"); }
        }


        #endregion

        #region Zaposleni za dodavanje

        private Zaposlen _zaposleniZaDodavanje;

        public Zaposlen ZaposleniZaDodavanje
        {
            get { return _zaposleniZaDodavanje; }
            set { _zaposleniZaDodavanje = value; OnPropertyChanged("ZaposleniZaDodavanje"); }
        }


        #endregion

        #region Trenutno aktivni prozor

        /// <summary>
        /// Promenljiva u kojoj cuvam trenutno otvoreni prozor
        /// kako bih kasnije u komandama u zavisnosti od komande
        /// recimo zatvorio trenutni prozor
        /// </summary>
        private Window _trenutniProzor;

        public Window TrenutniProzor
        {
            get { return _trenutniProzor; }
            set { _trenutniProzor = value; }
        }

        #endregion

        #region Komande

        public RelayCommand PotvrdaBrisanjaPodatakaCommand { get; private set; }

        public void PotvrdaBrisanjaPodataka(object obj)
        {
            // sprecavam kada niko nije selektovan da ne dodje do erroa
            if (SelektovaniZaposleni is null)
                return;


            foreach (Zaposlen trenutniZaposleni in Zaposleni)
            {
                if(trenutniZaposleni.KorisnickoIme == SelektovaniZaposleni.KorisnickoIme)
                {
                    MessageBox.Show("Uspesno ste izbrisali radnika sa korisnickim imenom " + trenutniZaposleni.KorisnickoIme);
                    Zaposleni.Remove(trenutniZaposleni);

                    break;
                }
            }

            this.TrenutniProzor.Close();
        }


        public RelayCommand PotvrdaDodavanjaPodatakaCommand { get; private set; }

        public void PotvrdaDodavanjaPodataka(object ojb)
        {
            // dodajem zaposlenog ukoliko je odgovor bio potvrdan
            Zaposleni.Add(ZaposleniZaDodavanje);
            this.TrenutniProzor.Close();
        }

        public RelayCommand PotvrdaIzmenePodatakaCommand { get; private set; }

        public void PotvrdaIzmenePodataka(object obj)
        {
            // selektovani objekat prima vrednosti od menjanog objekta
            SelektovaniZaposleni.Ime = ZaposleniZaIzmenu.Ime;
            SelektovaniZaposleni.Prezime = ZaposleniZaIzmenu.Prezime;
            SelektovaniZaposleni.Struka = ZaposleniZaIzmenu.Struka;
            SelektovaniZaposleni.Sifra = ZaposleniZaIzmenu.Sifra;
            SelektovaniZaposleni.KorisnickoIme = ZaposleniZaIzmenu.KorisnickoIme;

            this.TrenutniProzor.Close();
        }

        public RelayCommand RadniKalendarCommand { get; private set; }

        public void PrikaziRadniKalendar(object obj)
        {
            var dijalog = new RadniKalendarDijalog();
            dijalog.DataContext = this;             // kako bi povezao i ViewModel Zaposlenih za ovaj dijalog
            dijalog.ShowDialog();
        }

        public RelayCommand GenerisiIzvestajZaposlenogCommand { get; private set; }

        public void PrikaziDijalogGenerisanjaIzvestaja(object obj)
        {
            using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for a page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                //Draw the text
                graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));

                //Save the document
                document.Save("C:\\Users\\Vaxi\\Desktop\\6-semestar\\HCI\\projekat\\Vaksi\\HealthClinic\\IzvestajZaposlenih.pdf");
            }

            var dijalog = new GenerisiIzvestajZaposlenihDijalog();
            dijalog.ShowDialog();
        }

        public RelayCommand DodajZaposlenogCommand { get; private set; }

        public void DodajZaposlenog(object obj)
        {
            // kreiram novog zaposlenog da u slucaju potvrde mogu da ga dodam u listu zaposlenih
            ZaposleniZaDodavanje = new Zaposlen();

            // prikaz dijaloga za dodavanje
            TrenutniProzor = new DodajZaposlenogDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();
        }

        public RelayCommand IzmeniZaposlenogCommand { get; private set; }

        public void IzmeniZaposlenog(object obj)
        {
            // Zaposleni za izmenu/stimanje preuzima podatke od selektovanog zaposlenog
            if (!(SelektovaniZaposleni is null))
                ZaposleniZaIzmenu = new Zaposlen() {
                    Ime = SelektovaniZaposleni.Ime,
                    Prezime = SelektovaniZaposleni.Prezime,
                    Sifra = SelektovaniZaposleni.Sifra,
                    Struka = SelektovaniZaposleni.Struka,
                    KorisnickoIme = SelektovaniZaposleni.KorisnickoIme
                };


            // podesavanje prikaza dijaloga izmene
            TrenutniProzor = new IzmeniZaposlenogDijalog();
            TrenutniProzor.DataContext = this;             // kako bi prebacio podatke iz ovog prozora u dijalog
            TrenutniProzor.ShowDialog();                   // podesavam da i dijalog moze upravljati istim podacima
        }

        public RelayCommand IzbrisiZaposlenogCommand { get; private set; }

        public void IzbrisiZaposlenog(object obj)
        {
            // TODO: Mozda dodati nekad da pise tacno kog zaposlenog brisemo ali u nekim buducim verzijama
            TrenutniProzor = new ObrisiZaposlenogDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();
        }

        #endregion

        #region Tabela

        private ObservableCollection<Zaposlen> _zaposleni;

        public ObservableCollection<Zaposlen> Zaposleni
        {
            get { return _zaposleni; }
            set { _zaposleni = value; OnPropertyChanged("Zaposleni"); }
        }

        private void ucitavanjePodatakaUTabelu()
        {
            //Tabela - popunjavanje
            Zaposleni = new ObservableCollection<Zaposlen>();
            Zaposleni.Add(new Zaposlen() { KorisnickoIme="zikaa", Ime = "Zika", Prezime = "Vojvodic", Struka = "Otorinolaringolog", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme="dzoni", Ime = "Nikola", Prezime = "Zigic", Struka = "Oftamolog", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme="markoni", Ime = "Marko", Prezime = "Bogdanovic", Struka = "Kardio hirurg", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme="bobi", Ime = "Boban", Prezime = "Jokic", Struka = "Pedijatar", Sifra = "*****" });
            Zaposleni.Add(new Zaposlen() { KorisnickoIme = "niki", Ime = "Nikola", Prezime = "Marjanovic", Struka = "Doktor opste prakse", Sifra = "*****" });
            Zaposleni.Add(new Zaposlen() { KorisnickoIme = "zare", Ime = "Zika", Prezime = "Vojvodic", Struka = "Otorinolaringolog", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme = "nidroni", Ime = "Nikola", Prezime = "Zigic", Struka = "Oftamolog", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme = "maron", Ime = "Marko", Prezime = "Bogdanovic", Struka = "Kardio hirurg", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme = "bobi2", Ime = "Boban", Prezime = "Jokic", Struka = "Pedijatar", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme = "dzoni2", Ime = "Nikola", Prezime = "Marjanovic", Struka = "Doktor opste prakse", Sifra = "*****"});
        }


        #endregion

        #region Grafikon
        public Func<ChartPoint, string> PointLabel { get; set; }

        public void PieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
       

        }
        #endregion

    }
}
