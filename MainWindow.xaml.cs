using System.Windows;

//Roman Korpechyn, 2016

namespace OneTestProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void btn_Test(object sender, RoutedEventArgs e)
        {
            SelectionScreen window = new SelectionScreen();
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
            }
        }


        private void btm_QuestEd(object sender, RoutedEventArgs e)
        {
            QuestEditor window = new QuestEditor();
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
            }
        }
    }
}
