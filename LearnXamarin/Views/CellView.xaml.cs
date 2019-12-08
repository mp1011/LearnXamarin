
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

        public string DebugText()
        {
            string textStr;
            var text = TheLabel.Text;
            if (text == null)
                textStr = "NULL";
            else if (string.IsNullOrEmpty(text))
                textStr = "EMPTY";
            else if (string.IsNullOrWhiteSpace(text))
                textStr = "WHITESPACE";
            else
                textStr = text;

            return $"Cell Text={textStr} Color={TheLabel.TextColor} Value={GridCell.Value}";
        }

        public CellView()
        {
            InitializeComponent();
            TheLabel.PropertyChanged += TheLabel_PropertyChanged;
        }

        private void TheLabel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(TheLabel.Text))
            {
                System.Console.WriteLine($"Set Label Text to {TheLabel.Text ?? "NULL"} ");
            }
        }

        protected override void OnBindingContextChanged()
        {
            //this should not be neccessary, but the label text doesn't refresh on its own
            TheLabel.Text = GridCell.Value.ToString(); 
        }

        public async Task MoveToDestination()
        {
            var translation = CalculateMotionTranslation();
            await this.TranslateTo(translation.X, translation.Y, 100); //todo, use a resource

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