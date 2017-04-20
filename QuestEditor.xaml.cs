using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;


namespace OneTestProject
{
    public partial class QuestEditor : Window
    {
        private List<Classes.Test> _tests = new List<Classes.Test>();



        public QuestEditor()
        {
            InitializeComponent();

            UploadDatase();

            testList.ItemsSource = _tests;
        }


        private void testList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
            {
                questionList.ItemsSource = ((sender as ListBox).SelectedItem as Classes.Test).Questions;
            }
            else
            {
                questionList.ItemsSource = null;
            }
        }


        private void questionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
            {
                answersList.ItemsSource = ((sender as ListBox).SelectedItem as Classes.Question).Answers;
            }
            else
            {
                answersList.ItemsSource = null;
            }
        }




        private void btn_createTest(object sender, RoutedEventArgs e)
        {
            TextEditor window = new TextEditor();
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
            }
            if (window.DialogResult == true)
            {
                _tests.Add(new Classes.Test(window.Text.Text));
                testList.Items.Refresh();
            }
        }


        private void btn_editTest(object sender, RoutedEventArgs e)
        {
            if (testList.SelectedIndex >= 0)
            {
                TextEditor window = new TextEditor(_tests[testList.SelectedIndex].Name);
                window.Owner = this;
                if (window.ShowDialog() == true)
                {
                }
                if (window.DialogResult == true)
                {
                    _tests[testList.SelectedIndex].Name = window.Text.Text;
                    testList.Items.Refresh();
                }
            }
            else
            {
                CustomMessageBox window = new CustomMessageBox(Properties.Resources.SelectedItemError);
                if (window.ShowDialog() == true)
                {
                }
            }
        }


        private void btn_deleteTest(object sender, RoutedEventArgs e)
        {
            if (testList.SelectedIndex >= 0)
            {
                _tests.Remove((Classes.Test)testList.SelectedItem);
                testList.Items.Refresh();
            }
            else
            {
                CustomMessageBox window = new CustomMessageBox(Properties.Resources.SelectedItemError);
                if (window.ShowDialog() == true)
                {
                }
            }
        }




        private void btn_createQuestion(object sender, RoutedEventArgs e)
        {
            if (testList.SelectedIndex >= 0)
            {
                TextEditor window = new TextEditor();
                window.Owner = this;
                if (window.ShowDialog() == true)
                {
                }
                if (window.DialogResult == true)
                {
                    _tests[testList.SelectedIndex].Questions.Add(new Classes.Question(window.Text.Text));
                    questionList.Items.Refresh();
                }
            }
            else
            {
                CustomMessageBox window = new CustomMessageBox(Properties.Resources.SelectedItemError);
                if (window.ShowDialog() == true)
                {
                }
            }
        }


        private void btn_editQuestion(object sender, RoutedEventArgs e)
        {
            if (testList.SelectedIndex >= 0 & questionList.SelectedIndex >= 0)
            {
                TextEditor window = new TextEditor(_tests[testList.SelectedIndex].Questions[questionList.SelectedIndex].Text);
                window.Owner = this;
                if (window.ShowDialog() == true)
                {
                }
                if (window.DialogResult == true)
                {
                    _tests[testList.SelectedIndex].Questions[questionList.SelectedIndex].Text = window.Text.Text;
                    questionList.Items.Refresh();
                }
            }
            else
            {
                CustomMessageBox window = new CustomMessageBox(Properties.Resources.SelectedItemError);
                if (window.ShowDialog() == true)
                {
                }
            }
        }


        private void btn_deleteQuestion(object sender, RoutedEventArgs e)
        {
            if (testList.SelectedIndex >= 0 & questionList.SelectedIndex >= 0)
            {
                _tests[testList.SelectedIndex].Questions.Remove((Classes.Question)questionList.SelectedItem);
                questionList.Items.Refresh();
            }
            else
            {
                CustomMessageBox window = new CustomMessageBox(Properties.Resources.SelectedItemError);
                if (window.ShowDialog() == true)
                {
                }
            }
        }





        private void btn_createAnswer(object sender, RoutedEventArgs e)
        {
            if (testList.SelectedIndex >= 0 & questionList.SelectedIndex >= 0)
            {
                TextEditor window = new TextEditor();
                window.Owner = this;
                if (window.ShowDialog() == true)
                {
                }
                if (window.DialogResult == true)
                {
                    _tests[testList.SelectedIndex].Questions[questionList.SelectedIndex].Answers.Add(new Classes.Answer(window.Text.Text));
                    answersList.Items.Refresh();
                }
            }
            else
            {
                CustomMessageBox window = new CustomMessageBox(Properties.Resources.SelectedItemError);
                if (window.ShowDialog() == true)
                {
                }
            }
        }


        private void btn_editAnswer(object sender, RoutedEventArgs e)
        {
            if (testList.SelectedIndex >= 0 & questionList.SelectedIndex >= 0 & answersList.SelectedIndex >= 0)
            {
                TextEditor window = new TextEditor(_tests[testList.SelectedIndex].Questions[questionList.SelectedIndex].Answers[answersList.SelectedIndex].Text);
                window.Owner = this;
                if (window.ShowDialog() == true)
                {
                }
                if (window.DialogResult == true)
                {
                    _tests[testList.SelectedIndex].Questions[questionList.SelectedIndex].Answers[answersList.SelectedIndex].Text = window.Text.Text;
                    answersList.Items.Refresh();
                }
            }
            else
            {
                CustomMessageBox window = new CustomMessageBox(Properties.Resources.SelectedItemError);
                if (window.ShowDialog() == true)
                {
                }
            }
        }


        private void btn_deleteAnswer(object sender, RoutedEventArgs e)
        {
            if (testList.SelectedIndex >= 0 & questionList.SelectedIndex >= 0 & answersList.SelectedIndex >= 0)
            {
                _tests[testList.SelectedIndex].Questions[questionList.SelectedIndex].Answers.Remove((Classes.Answer)answersList.SelectedItem);
                answersList.Items.Refresh();
            }
            else
            {
                CustomMessageBox window = new CustomMessageBox(Properties.Resources.SelectedItemError);
                if (window.ShowDialog() == true)
                {
                }
            }
        }




        private void UploadDatase()
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            Directory.CreateDirectory("Tests");
            string[] fileList = Directory.GetFiles(Environment.CurrentDirectory + @"\Tests", @"*.dat");

            if (fileList.Length != 0)
            {
                for (int i = 0; i < fileList.Length; i++)
                {
                    using (Stream fStream = File.OpenRead(fileList[i]))
                    {
                        try
                        {
                            _tests.Add((Classes.Test)binFormat.Deserialize(fStream));
                        }
                        catch
                        {
                            CustomMessageBox window = new CustomMessageBox(string.Format(Properties.Resources.DamagedTestFile, fileList[i]));
                            if (window.ShowDialog() == true)
                            {
                            }
                        }
                    }
                }
            }
        }




        private void btn_save(object sender, RoutedEventArgs e)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            Directory.Delete("Tests", true);
            Directory.CreateDirectory("Tests");

            for (int i = 0; i < _tests.Count; i++)
            {
                using (Stream fStream = new FileStream(Environment.CurrentDirectory + @"\Tests\" + _tests[i].Name + @".dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, _tests[i]);
                }
            }
        }




        private void btn_close(object sender, RoutedEventArgs e)
        {
            CustomDialogBox window = new CustomDialogBox(Properties.Resources.ExitMessage);
            if (window.ShowDialog() == true)
            {
                Close();
            }
        }
    }
}
