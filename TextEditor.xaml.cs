using System.Windows;

namespace OneTestProject
{
    public partial class TextEditor : Window
    {
        public TextEditor(string text)
        {
            InitializeComponent();
            Text.Text = text;
            Text.Focus();
            Title = Properties.Resources.TextEditor;
        }

        public TextEditor():this("")
        {
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
