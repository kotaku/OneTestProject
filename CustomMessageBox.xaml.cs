using System.Windows;

namespace OneTestProject
{
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string txt)
        {
            InitializeComponent();
            Title = Properties.Resources.Warning;
            text.Text = txt;
            System.Media.SystemSounds.Exclamation.Play();          
        }

        private void btn_ok(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
