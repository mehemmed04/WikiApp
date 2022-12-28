using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WikiApp.Commands;
using WikiApp.Models;
using WikiApp.Services;

namespace WikiApp.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value;OnPropertyChanged(); }
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
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand GetDataCommand { get; set; }
        public MainViewModel()
        {
            SearchCommand = new RelayCommand(async (o) =>
            {
                MyStackPanel.Children.Clear();
                MyStackPanel.Children.Add(MyListView);
                WikiModels = await WikipediaService.GetResult(Text);

            });
            GetDataCommand = new RelayCommand((o) =>
            {
                var wikimodel = o as WikiModel;
                MyStackPanel.Children.Clear();
                TextBox t = new TextBox();
                t.Height = 330;
                t.Margin =  new Thickness(20,0,20,0);
                t.Width = 750;
                t.FontSize = 19;
                t.IsReadOnly = true;
                t.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                t.BorderThickness = new Thickness(0,0,0,0);
                t.TextWrapping = TextWrapping.Wrap;
                t.FontWeight = FontWeight.FromOpenTypeWeight(20);
                t.Text = wikimodel.Content ;
                MyStackPanel.Children.Add(t);
            });
        }
    }
}
