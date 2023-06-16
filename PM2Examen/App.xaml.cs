﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2Examen.Views;
using PM2Examen.Controllers;
using PM2Examen.Models;
using System.IO;

namespace PM2Examen
{
    public partial class App : Application
    {
      
        static dBSites instancia;

        public static dBSites Instancia
        {
            get {
                if (instancia == null)
                {
                    instancia = new dBSites(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SitiosDB.db3"));
                }
                return instancia;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
