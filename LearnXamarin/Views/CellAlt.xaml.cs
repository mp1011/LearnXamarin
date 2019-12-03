using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CellAlt : ContentView
    {
        public int CellValue { get;  set; }

        public static readonly BindableProperty CellValueProperty = BindableProperty.Create(nameof(CellValue), typeof(int), typeof(Cell),0);

        public CellAlt()
        {
            InitializeComponent();
        }
    }
}