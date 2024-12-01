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
        private List<string> colors = new List<string> { "Red", "Yellow", "Orange", "White", "Green", "Blue" };
        private List<string> _code;
        private List<string> selectedColors;
        private List<string> history = new List<string>();
        private int remainingAttempts = 10; 
        private int score = 100;
        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }
        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (remainingAttempts <= 0)
            {
                MessageBox.Show($"Game Over! de juiste code was: {string.Join(", ", _code)}");
                EndGame();
                return;
            }

            remainingAttempts--;
            LabelRemainingAttempts.Content = $"Attempts Left: {remainingAttempts}";

            if (remainingAttempts == 0)
            {
                MessageBox.Show($"Game Over! de juiste code was: {string.Join(", ", _code)}");
                EndGame();
            }
        }
        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (remainingAttempts <= 0) return;

            List<string> feedback = GetFeedback(selectedColors);
            string attemptSummary = $"Attempt: {string.Join(", ", selectedColors)} | Feedback: {string.Join(", ", feedback)}";
            history.Add(attemptSummary);
            ListBoxHistory.Items.Add(attemptSummary); 

        
        }
        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (remainingAttempts <= 0) return;

            int penalty = 0;
            List<string> feedback = GetFeedback(selectedColors);

            foreach (var feedbackItem in feedback)
            {
                if (feedbackItem == "White") penalty += 1; 
                else if (feedbackItem == "Red") penalty += 0; 
                else penalty += 2;
            }

            score -= penalty;
            LabelScore.Content = $"Score: {score}";
        }
        private void EndGame()
        {
            var result = MessageBox.Show("wil je opniew spelen?", "Game Over", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                InitializeGame();
            else
            {
                this.Close(); 
            }
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("wil je dit spel beindiging?", "Exit", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true; // Prevent closure
            }
        }
    }
}