using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WikiApp.Commands;
using WikiApp.Models;
using WikiApp.ProxyPattern;
using WikiApp.Services;

namespace WikiApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        ICache cache = new CacheProxy();
        List<string> Datas = new List<string>();
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(); }
        }

        private string selectedData;

        public string SelectedData
        {
            get { return selectedData; }
            set { selectedData = value; OnPropertyChanged(); }
        }


        private void SetDatas()
        {
            if (Text != String.Empty && Text != null)
            {
                Datas = cache.GetValue(Text);
                DatasListView.Height = 250;
                DatasListView.ItemsSource = null;
                DatasListView.ItemsSource = Datas;

            }
            else
            {
                DatasListView.Height = 0;
                DatasListView.ItemsSource = null;
            }
        }

        private List<WikiModel> wikiModels;

        public List<WikiModel> WikiModels
        {
            get { return wikiModels; }
            set { wikiModels = value; OnPropertyChanged(); }
        }

        private WikiModel selectedWiki;

        public WikiModel SelectedWiki
        {
            get { return selectedWiki; }
            set { selectedWiki = value; }
        }

        public StackPanel MyStackPanel { get; set; }
        public ListView MyListView { get; set; }
        public ListView DatasListView { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand GetDataCommand { get; set; }
        public RelayCommand TextChangedCommand { get; set; }
        public RelayCommand SelectedDataChangedCommand { get; set; }
        public MainViewModel()
        {
            SearchCommand = new RelayCommand(async (o) =>
            {
                try
                {
                    MyStackPanel.Children.Clear();
                    MyStackPanel.Children.Add(MyListView);
                    cache.SetData(Text);
                    DatasListView.Height = 0;
                    WikiModels = await WikipediaService.GetResult(Text);
                }
                catch (Exception)
                {
                    TextBlock t = new TextBlock
                    {
                        Foreground = Brushes.Red,
                        FontSize = 30,
                        Text = $"There is not any data for - {Text} ",
                        FontWeight = FontWeights.Bold
                    };
                    MyStackPanel.Children.Clear();
                    MyStackPanel.Children.Add(t);

                }
            });
            TextChangedCommand = new RelayCommand((o) =>
            {
                SetDatas();
            });
            SelectedDataChangedCommand = new RelayCommand((o) =>
            {
                var data = DatasListView.SelectedItem as string;
                if (data != null)
                {
                    Text = data;
                    DatasListView.Height = 0;
                    DatasListView.ItemsSource = null;
                }
            });

            GetDataCommand = new RelayCommand((o) =>
            {
                var wikimodel = o as WikiModel;
                MyStackPanel.Children.Clear();
                TextBox t = new TextBox();
                t.Height = 330;
                t.Margin = new Thickness(20, 0, 20, 0);
                t.Width = 750;
                t.FontSize = 19;
                t.IsReadOnly = true;
                t.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                t.BorderThickness = new Thickness(0, 0, 0, 0);
                t.TextWrapping = TextWrapping.Wrap;
                t.FontWeight = FontWeight.FromOpenTypeWeight(20);
                t.Text = wikimodel.Title + "\n" + wikimodel.Content;
                MyStackPanel.Children.Add(t);
                FileService.WriteToFile(wikimodel.PageId);

            });
        }
    }
}
