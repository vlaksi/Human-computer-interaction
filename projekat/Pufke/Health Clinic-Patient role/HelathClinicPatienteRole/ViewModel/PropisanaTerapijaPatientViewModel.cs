﻿using C_Validation_ByCustom;
using HelathClinicPatienteRole.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using HelathClinicPatienteRole.Model;

namespace HelathClinicPatienteRole.ViewModel
{
    class PropisanaTerapijaPatientViewModel : ObservableObject
    {
        public ObservableCollection<Meeting> Meetings { get; set; }
        List<string> eventNameCollection;
        List<Brush> colorCollection;
        public PropisanaTerapijaPatientViewModel()
        {
            GenerisiIzvestajCommand = new RelayCommand(GenerisiIzvestaj);
            Meetings = new ObservableCollection<Meeting>();
            CreateEventNameCollection();
            CreateColorCollection();
            CreateAppointments();
        }

        private void CreateAppointments()
        {
            Random randomTime = new Random();
            List<Point> randomTimeCollection = GettingTimeRanges();
            DateTime date;
            DateTime DateFrom = DateTime.Now.AddDays(-10);
            DateTime DateTo = DateTime.Now.AddDays(10);
            DateTime dataRangeStart = DateTime.Now.AddDays(-3);
            DateTime dataRangeEnd = DateTime.Now.AddDays(3);


            for (date = DateFrom; date < DateTo; date = date.AddDays(1))
            {
                if ((DateTime.Compare(date, dataRangeStart) > 0) && (DateTime.Compare(date, dataRangeEnd) < 0))
                {
                    for (int AdditionalAppointmentIndex = 0; AdditionalAppointmentIndex < 3; AdditionalAppointmentIndex++)
                    {
                        Meeting meeting = new Meeting();
                        int hour = (randomTime.Next((int)randomTimeCollection[AdditionalAppointmentIndex].X, (int)randomTimeCollection[AdditionalAppointmentIndex].Y));
                        meeting.From = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                        meeting.To = (meeting.From.AddHours(1));
                        meeting.EventName = eventNameCollection[randomTime.Next(9)];
                        meeting.Color = colorCollection[randomTime.Next(9)];
                        if (AdditionalAppointmentIndex % 3 == 0)
                            meeting.AllDay = true;
                        Meetings.Add(meeting);
                    }
                }
                else
                {
                    Meeting meeting = new Meeting();
                    meeting.From = new DateTime(date.Year, date.Month, date.Day, randomTime.Next(9, 11), 0, 0);
                    meeting.To = (meeting.From.AddHours(1));
                    meeting.EventName = eventNameCollection[randomTime.Next(9)];
                    meeting.Color = colorCollection[randomTime.Next(9)];
                    Meetings.Add(meeting);
                }
            }
        }

        /// <summary>  
        /// Creates event names collection.  
        /// </summary>  
        private void CreateEventNameCollection()
        {
            eventNameCollection = new List<string>();
            eventNameCollection.Add("Lek za pritisak");
            eventNameCollection.Add("Lek za krvne sudove");
            eventNameCollection.Add("Brufen");
            eventNameCollection.Add("Lek za pritisak");
            eventNameCollection.Add("Masaza");
            eventNameCollection.Add("Panadol");
            eventNameCollection.Add("Lek za imunitet");
            eventNameCollection.Add("Vitamin C");
            eventNameCollection.Add("Lek za pritisak");
            eventNameCollection.Add("Lek za pritisak");
        }

        /// <summary>  
        /// Creates color collection.  
        /// </summary>  
        private void CreateColorCollection()
        {
            colorCollection = new List<Brush>();
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF339933")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00ABA9")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE671B8")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1BA1E2")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD80073")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA2C139")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA2C139")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD80073")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF339933")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE671B8")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00ABA9")));
        }

        /// <summary>
        /// Gets the time ranges.
        /// </summary>
        private List<Point> GettingTimeRanges()
        {
            List<Point> randomTimeCollection = new List<Point>();
            randomTimeCollection.Add(new Point(9, 11));
            randomTimeCollection.Add(new Point(12, 14));
            randomTimeCollection.Add(new Point(15, 17));
            return randomTimeCollection;
        }



        public RelayCommand GenerisiIzvestajCommand { get; private set; }

        public void GenerisiIzvestaj(object obj)
        {
            using (PdfDocument document = new PdfDocument())
            {
          
                PdfPage page = document.Pages.Add();

                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
                page.Graphics.DrawString("Izvestaj o uzimanju terapije!", font, PdfBrushes.Black, new System.Drawing.PointF(0, 0));
                page.Graphics.DrawString("Ponedeljak 18h lek za pritisak", font, PdfBrushes.Black, new System.Drawing.PointF(0, 40));
                page.Graphics.DrawString("Utorak 18h lek za pritisak", font, PdfBrushes.Black, new System.Drawing.PointF(0, 80));
                page.Graphics.DrawString("Cetvrtak 18h lek za pritisak", font, PdfBrushes.Black, new System.Drawing.PointF(0, 120));
                page.Graphics.DrawString("Petak 18h lek za pritisak", font, PdfBrushes.Black, new System.Drawing.PointF(0, 160));
                page.Graphics.DrawString("Terapiju uzimati u trajanju od tri nedelje", font, PdfBrushes.Black, new System.Drawing.PointF(0, 200));

                document.Save("C:\\Users\\Pufke\\Desktop\\Izvestaj.pdf");
                MessageBox.Show("Izvestaj je izgenerisan da Desktop vaseg racunara");
            }
            
        }
    }


   
}
