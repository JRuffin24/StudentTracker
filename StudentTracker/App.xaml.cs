﻿using StudentTracker.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using StudentTracker.Services;
using Settings = StudentTracker.Services.Settings;

namespace StudentTracker
{
    public partial class App : Application
    {
        const int smallWightResolution = 768;
        const int smallHeightResolution = 1280;
        public App()
        {
            InitializeComponent();
            LoadStyles();

            if (Settings.FirstRun == true)
            {
                DatabaseService.LoadSampleData();

                Settings.FirstRun = false;
            }

            // DatabaseService.LoadSampleData();

            var mainPage = new MainPage();            
            var navPage = new NavigationPage(mainPage);
            MainPage = navPage;

            
        }
        void LoadStyles()
        {
            if (IsASmallDevice())
            {
                dictionary.MergedDictionaries.Add(SmallDevicesStyle.SharedInstance);
            }
            else
            {
                dictionary.MergedDictionaries.Add(GeneralDevicesStyle.SharedInstance);
            }
        }
        public static bool IsASmallDevice()
        {
            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            return (width <= smallWightResolution && height <= smallHeightResolution);
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
