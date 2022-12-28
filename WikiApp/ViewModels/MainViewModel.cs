using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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


        public RelayCommand SearchCommand { get; set; }
        public RelayCommand GetDataCommand { get; set; }
        public MainViewModel()
        {
            SearchCommand = new RelayCommand(async (o) =>
            {
                WikiModels = await WikipediaService.GetResult(Text);
            });
            GetDataCommand = new RelayCommand((o) =>
            {
                if (selectedWiki == null)
                {
                    MessageBox.Show("Select Item . Then click title");
                }
                else MessageBox.Show(SelectedWiki.Content);
            });
        }
    }
}
