﻿using HealthClinic.Models;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthClinic.ViewModels
{
    public class LekoviViewModel
    {

        public LekoviViewModel()
        {
            ucitavanjeLekova();
            PieChart();
        }

        #region Tabela
        public ObservableCollection<Lek> Lekovi { get; set; }

        private void ucitavanjeLekova()
        {
            Lekovi = new ObservableCollection<Lek>();
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan", SifraLeka = "0x21FDAF", Kolicina = "10" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan2", SifraLeka = "0x22FDAF", Kolicina = "11" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan3", SifraLeka = "0x23FDAF", Kolicina = "12" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan4", SifraLeka = "0x24FDAF", Kolicina = "13" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan", SifraLeka = "0x21FDAF", Kolicina = "10" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan2", SifraLeka = "0x22FDAF", Kolicina = "11" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan3", SifraLeka = "0x23FDAF", Kolicina = "12" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan4", SifraLeka = "0x24FDAF", Kolicina = "13" });
        }

        #endregion

        #region Grafikon
        public Func<ChartPoint, string> PointLabel { get; set; }

        private void PieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);

        }

        #endregion
    }
}
