using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mastermind_02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int remainingAttempts = 10;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (remainingAttempts <= 0)
            {
                MessageBox.Show($"Game Over! The correct code was: {string.Join(", ", _code)}");
                EndGame();
                return;
            }

            // Logic for checking the attempt
            remainingAttempts--;
            LabelRemainingAttempts.Content = $"Attempts Left: {remainingAttempts}";

            if (remainingAttempts == 0)
            {
                MessageBox.Show($"Game Over! The correct code was: {string.Join(", ", _code)}");
                EndGame();
            }
        }
    }
}