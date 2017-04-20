using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OneTestProject
{
    public partial class ExaminationScreen : Window
    {
        private Classes.Test _test;
        private List<Classes.Question> _customAnswers;

        private DateTime _start;
        private DispatcherTimer _timer;



        public ExaminationScreen(Classes.Test test)
        {
            InitializeComponent();

            Title = test.Name;

            _test = (Classes.Test)Clone(test);
            _customAnswers = (List<Classes.Question>)Clone(test.Questions);

            UncheckedAnswers(_customAnswers);

            GenerateHeader(test.Name);
            GenerateRadioButton(test.Questions.Count);
        }




        private void UncheckedAnswers(List<Classes.Question> question)
        {
            for (int i = 0; i < question.Count; i++)
            {
                for (int j = 0; j < question[i].Answers.Count; j++)
                {
                    question[i].Answers[j].isCorrect = false;
                }
            }
        }



        public static object Clone(object inobject)
        {
            BinaryFormatter BF = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            BF.Serialize(memStream, inobject);
            memStream.Flush();
            memStream.Position = 0;
            return BF.Deserialize(memStream);
        }




        private void GenerateHeader(string name)
        {
            Theme.Content = name;

            _start = DateTime.Now;
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(on_Tick);
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        private void on_Tick(object sender, EventArgs e)
        {
            Time.Content = string.Format("{0:mm}{1}{0:ss}", (DateTime.Now - _start), ((DateTime.Now - _start).Seconds % 2 == 0) ? " " : ":");
        }





        private void GenerateRadioButton(int count)
        {
            for (int i = 0; i < count; i++)
            {
                RadioButton rb = new RadioButton { Content = i + 1 };
                rb.FontSize = 22;
                rb.FontFamily = Time.FontFamily;
                rb.Checked += RadioButton_Checked;
                RadioBtn.Children.Add(rb);
            }
        }




        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            question.Text = _customAnswers[int.Parse((sender as RadioButton).Content.ToString()) - 1].Text;
            answers.ItemsSource = _customAnswers[int.Parse((sender as RadioButton).Content.ToString()) - 1].Answers;
        }




        private void RadioButton_Checked_Done(object sender, RoutedEventArgs e)
        {
            RadioButton_Checked(sender, e);
            answers.IsEnabled = false;

            List<Classes.Answer> anw = _test.Questions[int.Parse((sender as RadioButton).Content.ToString()) - 1].Answers;
            string number = "";
            for (int i = 0; i < anw.Count; i++)
            {
                if (anw[i].isCorrect)
                    number += ", " + (i + 1);
            }
            correctAnswers.Content = (number == "") ? Properties.Resources.HaveNoCorrectAnswers : string.Format(Properties.Resources.CorrectAnswers, number.Remove(0, 2));
        }





        private void btn_exit(object sender, RoutedEventArgs e)
        {
            Close();
        }




        private void btn_finish(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            Finish.Visibility = Visibility.Hidden;
            Exit.Visibility = Visibility.Visible;
            Result.Visibility = Visibility.Visible;

            for (int i = 0; i < RadioBtn.Children.Count; i++)
            {
                (RadioBtn.Children[i] as RadioButton).Checked += RadioButton_Checked_Done;
                (RadioBtn.Children[i] as RadioButton).IsChecked = false;
            }
            question.Text = null;
            answers.ItemsSource = null;
            MarkQuestion();
        }





        private void MarkQuestion()
        {
            bool status;
            for (int i = 0; i < _customAnswers.Count; i++)
            {
                status = true;
                for (int j = 0; j < _customAnswers[i].Answers.Count; j++)
                {
                    if (_customAnswers[i].Answers[j].isCorrect != _test.Questions[i].Answers[j].isCorrect)
                    {
                        status = false;
                    }
                }
                if (status)
                {
                    (RadioBtn.Children[i] as RadioButton).Background = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    (RadioBtn.Children[i] as RadioButton).Background = new SolidColorBrush(Colors.Red);

                }
            }
        }
    }
}
