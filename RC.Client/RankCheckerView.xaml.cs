namespace RC.Client
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for RankCheckerView.xaml
    /// </summary>
    public partial class RankCheckerView : Window
    {
        public RankCheckerView()
        {
            this.DataContext = new RankCheckerViewModel();
            InitializeComponent();
        }
    }
}
