using System.Windows;

namespace OneTestProject
{
    public partial class CustomDialogBox : Window
    {
        public CustomDialogBox(string txt)
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

        private void btn_cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
