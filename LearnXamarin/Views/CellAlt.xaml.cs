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
        public int CellValue 
        {
            get
            {
                var cellValue = (int)GetValue(CellValueProperty);
                return cellValue;
            }
            set
            {
                SetValue(CellValueProperty, value);
            }
        }

        public static readonly BindableProperty CellValueProperty = BindableProperty.Create(
            propertyName: nameof(CellValue),
            declaringType: typeof(Cell),
            returnType: typeof(int),
            defaultValue: 0,
            //propertyChanged: CellValueChanged,
            defaultBindingMode: BindingMode.OneWay);

        private static void CellValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CellAlt).CellValue = (int)newValue;
        }

        public CellAlt()
        {
            InitializeComponent();
        }
    }
}