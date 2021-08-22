using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Anumals
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    using System.Windows.Threading;

    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound = 0;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
                textBlock.IsEnabled = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            TimeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                TimeTextBlock.Text = TimeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {

            List<string> animalEmoji = new List<string>()
            {
               "🐲" , "🐵" ,
               "🦄" , "👻" ,
               "🦁" , "👽" ,
               "🦒" , "🦏" ,
               "🦝" , "🐳" ,
               "🐷" , "🐰" ,
               "🐸" , "🦀" ,
               "🐮" , "🦨" ,
               "🐼" , "🦥" ,
               "🐶" , "🦐" ,
            };

            Random random = new Random();
            Random nerandom = new Random();

            int t = 0;
            while (t < 17)
            {
                int index = 0;
                int y = 0;
                foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>().OrderBy(x => nerandom.Next()))

                {

                    if (textBlock.Name != "TimeTextBlock")
                    {
                        textBlock.Visibility = Visibility.Visible;
                        textBlock.IsEnabled = true;
                        if(y == 0)
                        {
                            index = random.Next(animalEmoji.Count);
                            string nextEmoji = animalEmoji[index];
                            y++;
                        }
                        if (y < 3 && y > 0)
                        {
                            string nextEmoji = animalEmoji[index];
                            textBlock.Text = nextEmoji;
                            y++;
                        }
                        if (y == 3)
                        {
                            animalEmoji.RemoveAt(index);
                            y = 0;
                        }
                    }
                    else
                    {
                        textBlock.IsEnabled = true;
                    }
                }
                t += 100;
            }


            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
            startButton.Visibility = Visibility.Hidden;
        }

        TextBlock lastTextBlockClikcked;
        bool findingMatch = false;


        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)

            {
                textBlock.Background = Brushes.LemonChiffon;
                lastTextBlockClikcked = textBlock;
                findingMatch = true;
                lastTextBlockClikcked.IsEnabled = false;
            }
            else if (textBlock.Text == lastTextBlockClikcked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClikcked.Background = Brushes.Transparent;
                lastTextBlockClikcked.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClikcked.Background = Brushes.Transparent;
                lastTextBlockClikcked.IsEnabled = true;
                findingMatch = false;

            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            SetUpGame();
        }
    }
}
