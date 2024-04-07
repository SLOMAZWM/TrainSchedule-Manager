﻿using ProjektLAB.TrainService.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektLAB.TrainService.Pages.DetailWindow
{
    /// <summary>
    /// Interaction logic for ShowTrainDetailsPage.xaml
    /// </summary>
    public partial class ShowTrainDetailsPage : Page
    {
        private TrainSchedule selectedSchedule;
        private ShowDetailWindow showDetailWindow;

        public ShowTrainDetailsPage(TrainSchedule selectedSch, ShowDetailWindow DetailWindow)
        {
            InitializeComponent();
            selectedSchedule = selectedSch;
            showDetailWindow = DetailWindow;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            showDetailWindow.ContentDetailFrame.NavigationService.GoBack();
        }
    }
}
