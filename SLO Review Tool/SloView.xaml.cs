﻿using SloReviewTool.Model;
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
using System.Windows.Shapes;
using Xamarin.Forms.PlatformConfiguration;

namespace SloReviewTool
{
    /// <summary>
    /// Interaction logic for SloView.xaml
    /// </summary>
    public partial class SloView : Window
    {
        SloRecord slo_;
        DataGrid resultsDataGrid_;
        SloQueryManager queryManager_;


        public SloView(SloRecord slo, DataGrid resultsDataGrid)
        {
            queryManager_ = new SloQueryManager();
            slo_ = slo;
            resultsDataGrid_ = resultsDataGrid;
            this.DataContext = slo_;
            InitializeComponent();
            this.DataSourcesListView.ItemsSource = slo_.SloDefinition.DataSources;
            this.SloGroupssListView.ItemsSource = slo_.SloDefinition.SloGroups;
        }

        public SloDefinition Definition {
            get => slo_.SloDefinition;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

            Update.IsEnabled = false;
            var reviews = new List<SloManualReview>();
            reviews.Add(new SloManualReview(slo_));
            await queryManager_.PublishManualReviews(reviews);
            Update.Content = "Updated";
            Update.Background = new SolidColorBrush(Colors.Green);
            Update.Foreground = Brushes.White;
            Update.IsEnabled = true;
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void PreviousRecord_Click(object sender, RoutedEventArgs e)
        {
        }

        private void NextRecord_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
