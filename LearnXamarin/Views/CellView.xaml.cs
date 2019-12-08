
using LearnXamarin.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CellView : ContentView
    {
        public GridCell GridCell => BindingContext as GridCell;

        public bool NeedsToMove => GridCell != null && !GridCell.TargetGridPosition.Equals(GridCell.OriginalGridPosition);

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
            propertyChanged: CellValueChanged,
            defaultBindingMode: BindingMode.TwoWay);

        private static void CellValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
          //  (bindable as Cell).CellValue = (int)newValue;
        }

        public CellView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            GridCell.PropertyChanged += GridCell_PropertyChanged;
        }

        private void GridCell_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (GridCell != null && e.PropertyName == nameof(GridCell.TargetGridPosition))
            {
                int index = FooIndex++;
                System.Console.WriteLine($"{index} Target Changed to {GridCell.TargetGridPosition} from {GridCell.OriginalGridPosition}, NeedsToMove={NeedsToMove}");

                if (NeedsToMove)
                    MoveToDestination();
            }
        }

        public static int FooIndex=0;
        public async void MoveToDestination()
        {
            int index = FooIndex++;
            var sw = new Stopwatch();
            sw.Start();

            System.Console.WriteLine($"{index} MoveToDestination Begin {this.TranslationX} {this.TranslationY}");
          

            var translation = CalculateMotionTranslation();
            await this.TranslateTo(translation.X, translation.Y, 100); //todo, use a resource

            sw.Stop();

            System.Console.WriteLine();
            System.Console.WriteLine($"{index} MoveToDestination End {this.TranslationX} {this.TranslationY} {sw.ElapsedMilliseconds}ms");
            System.Console.WriteLine();

            GridCell.OriginalGridPosition = GridCell.TargetGridPosition;
            TranslationX = 0;
            TranslationY = 0;
        }

        private Point CalculateMotionTranslation()
        {
            var gridHeight = Height;
            var gridWidth = Width;

            var deltaY = (GridCell.TargetGridPosition.Y - GridCell.OriginalGridPosition.Y) * gridHeight;
            var deltaX = (GridCell.TargetGridPosition.X - GridCell.OriginalGridPosition.X) * gridWidth;
            return new Point(deltaX, deltaY);
        }
    }
}