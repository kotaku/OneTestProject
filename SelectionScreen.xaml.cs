using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Input;
using System.Windows.Controls;


namespace OneTestProject
{
    public partial class SelectionScreen : Window
    {
        private List<Classes.Test> _tests = new List<Classes.Test>();




        public SelectionScreen()
        {
            InitializeComponent();

            UploadDatase();
            TestsCheck();
            testList.ItemsSource = _tests;
        }




        private void testList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
            {
                qstNumber.Text = ((sender as ListBox).SelectedItem as Classes.Test).Questions.Count.ToString();
            }
        }




        private void TestsCheck()
        {
            for (int i = 0; i < _tests.Count; i++)
            {
                if (!TestCheck(_tests[i].Questions))
                {
                    CustomMessageBox window = new CustomMessageBox(string.Format(Properties.Resources.TestCheckFail, _tests[i].Name));
                    if (window.ShowDialog() == true)
                    {
                    }
                    _tests.RemoveAt(i);
                    i--;
                }
            }
        }




        private bool TestCheck(List<Classes.Question> qsts)
        {
            bool result = true;
            if (qsts.Count > 0)
            {
                for (int i = 0; i < qsts.Count; i++)
                {
                    if (qsts[i].Answers.Count <= 0)
                    {
                        i = qsts.Count;
                        result = false;
                    }
                }
            }
            else
            {
                result = false;
            }
            return result;
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




        private void btn_Start(object sender, RoutedEventArgs e)
        {
            if (testList.SelectedIndex >= 0 && int.Parse(qstNumber.Text) <= _tests[testList.SelectedIndex].Questions.Count)
            {
                ExaminationScreen window = new ExaminationScreen(RandomazeQuestion(_tests[testList.SelectedIndex], int.Parse(qstNumber.Text)));
                window.Owner = this;
                if (window.ShowDialog() == true)
                {
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


        private Classes.Test RandomazeQuestion(Classes.Test selectedTest, int numberOfQst)
        {
            Classes.Test result = new Classes.Test();
            result.Name = selectedTest.Name;

            Random rnd = new Random();
            int[] oldCounts = new int[0];
            int currentCount;

            for (int i = 0; i < numberOfQst; i++)
            {
                currentCount = rnd.Next(0, numberOfQst - 1);
                for (int j = 0; j < oldCounts.Length; j++)
                {
                    if (currentCount == oldCounts[j])
                    {
                        j = -1;
                        currentCount = rnd.Next(0, numberOfQst);
                    }
                }

                Array.Resize(ref oldCounts, oldCounts.Length + 1);
                oldCounts[oldCounts.Length - 1] = currentCount;

                result.Questions.Add(selectedTest.Questions[currentCount]);
            }

            return result;
        }



        private void btn_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }




        private void qstNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = ("0123456789".IndexOf(e.Text) < 0) && (e.Text != " ");
        }

        private void qstNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.V || e.Key == Key.Insert)
            {
                e.Handled = true;
            }

        }
    }
}
