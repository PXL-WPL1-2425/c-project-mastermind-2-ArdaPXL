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
        private List<string> code;
        private int attemptsLeft = 10;
        private int score = 100;

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
            PopulateComboBoxes();
        }
        private void PopulateComboBoxes()
        {
            List<string> colorOptions = new List<string> { "Red", "Yellow", "Orange", "White", "Green", "Blue" };

            ComboBox1.ItemsSource = colorOptions;
            ComboBox2.ItemsSource = colorOptions;
            ComboBox3.ItemsSource = colorOptions;
            ComboBox4.ItemsSource = colorOptions;
        }

        private void StartNewGame()
        {
            // Generate a new code
            Random rand = new Random();
            code = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                code.Add(colors[rand.Next(colors.Count)]);
            }

            attemptsLeft = 10;
            score = 100;

            // Update UI
            ScoreLabel.Content = $"Score: {score}";
            AttemptsLabel.Content = $"Attempts Left: {attemptsLeft}";
            ListBoxHistory.Items.Clear();

            MessageBox.Show("New game started! Try to guess the code.");
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            // Get selected colors
            List<string> selectedColors = new List<string>
            {
                ComboBox1.SelectedItem?.ToString(),
                ComboBox2.SelectedItem?.ToString(),
                ComboBox3.SelectedItem?.ToString(),
                ComboBox4.SelectedItem?.ToString()
            };

            if (selectedColors.Contains(null))
            {
                MessageBox.Show("Please select all colors before checking.");
                return;
            }

            // Evaluate the attempt
            List<string> feedback = new List<string>();
            for (int i = 0; i < selectedColors.Count; i++)
            {
                if (selectedColors[i] == code[i])
                {
                    feedback.Add("Red"); // Correct color and position
                }
                else if (code.Contains(selectedColors[i]))
                {
                    feedback.Add("White"); // Correct color, wrong position
                    score -= 1; // Deduct 1 point
                }
                else
                {
                    feedback.Add("None"); // Incorrect color
                    score -= 2; // Deduct 2 points
                }
            }

            // Update UI
            ScoreLabel.Content = $"Score: {score}";
            AttemptsLabel.Content = $"Attempts Left: {attemptsLeft}";
            ListBoxHistory.Items.Add($"Attempt: {string.Join(", ", selectedColors)} | Feedback: {string.Join(", ", feedback)}");

            // Check for game end
            attemptsLeft--;
            if (selectedColors.SequenceEqual(code))
            {
                MessageBox.Show($"You guessed the code! Final Score: {score}");
                AskToPlayAgain();
            }
            else if (attemptsLeft == 0)
            {
                MessageBox.Show($"Game over! The code was: {string.Join(", ", code)}");
                AskToPlayAgain();
            }
        }

        private void AskToPlayAgain()
        {
            if (MessageBox.Show("Do you want to play again?", "Game Over", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                StartNewGame();
            }
            else
            {
                Close();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                // Update visual feedback
                string selectedColor = comboBox.SelectedItem?.ToString();
                Brush colorBrush = selectedColor switch
                {
                    "Red" => Brushes.Red,
                    "Yellow" => Brushes.Yellow,
                    "Orange" => Brushes.Orange,
                    "White" => Brushes.White,
                    "Green" => Brushes.Green,
                    "Blue" => Brushes.Blue,
                    _ => Brushes.Transparent
                };

                // Set the matching ellipse background
                if (comboBox == ComboBox1) FeedbackEllipse1.Fill = colorBrush;
                if (comboBox == ComboBox2) FeedbackEllipse2.Fill = colorBrush;
                if (comboBox == ComboBox3) FeedbackEllipse3.Fill = colorBrush;
                if (comboBox == ComboBox4) FeedbackEllipse4.Fill = colorBrush;
            }
        }
    }
}