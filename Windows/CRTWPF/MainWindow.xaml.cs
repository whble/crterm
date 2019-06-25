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

namespace CRTWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool CursorOn = true;
        bool TextChanged = true;
        public System.Timers.Timer CursorBlink = new System.Timers.Timer();
        public Run TextCursor = new Run(" ");

        public MainWindow()
        {
            CursorBlink.Elapsed += CursorBlink_Elapsed;
            CursorBlink.Interval = 500;
            CursorBlink.Enabled = true;

            InitializeComponent();
        }

        private void CursorBlink_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CursorOn = !CursorOn;
            Action updateInvoke = UpdateCursor;
            TextCursor.Dispatcher.Invoke(updateInvoke);

            if (TextChanged)
            {
                Action ut = UpdateText;
                MainText.Dispatcher.Invoke(ut);
            }
        }

        public void UpdateCursor()
        {
            if (CursorOn)
            {
                TextCursor.Background = this.Foreground;
                TextCursor.Foreground = this.Background;
            }
            //TextCursor.TextDecorations = TextDecorations.Underline;
            else
            {
                TextCursor.Foreground = this.Foreground;
                TextCursor.Background = this.Background;
            }
        }

        public void UpdateText()
        {
            DateTime st = DateTime.Now;

            TextBlock tb = new TextBlock();
            if (MainText.Children.Count != 25)
            {
                MainText.Children.Clear();
                for (int i = 0; i < 25; i++)
                {
                    MainText.Children.Add(new TextBlock());
                }
            }

            tb = MainText.Children[0] as TextBlock;
            tb.Inlines.Clear();
            for (int i = 1; i < 9; i++)
            {
                tb.Inlines.Add(new Run("         " + i.ToString()));
            }

            tb = MainText.Children[1] as TextBlock;
            tb.Inlines.Clear();
            for (int i = 1; i < 9; i++)
            {
                tb.Inlines.Add(new Run("1234567890"));
            }

            for (int i = 2; i < 25; i++)
            {
                tb = MainText.Children[i] as TextBlock;
                tb.Inlines.Clear();
                tb.Inlines.Add(new Run(i.ToString("d2")));
            }

            TimeSpan et = DateTime.Now - st;
            tb = MainText.Children[24] as TextBlock;
            TextPointer p = tb.ContentEnd;
            p.InsertTextInRun(" ");
            p.InsertTextInRun(et.TotalMilliseconds.ToString());

        }
    }
}

