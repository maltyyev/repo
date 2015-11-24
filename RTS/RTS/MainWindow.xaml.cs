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
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RTS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    ///  
    static public class ValueModel
    {
        public static double Pressure;
        public static double Speed;
        public static double Temperature;

        public static bool IsItWork;

        public static bool Fire = false;
        public static bool Leakage;
        public static bool IsMiningBegin;
    }
    public partial class MainWindow : Window
    {
        public MainWindow( )
        {
            InitializeComponent();
            ValueModel.IsItWork = false;
            NewThread.Start();
        }

        private void OnStart_Click( object sender, RoutedEventArgs e )
        {
            ChangeStatus();
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            Application.Current.Shutdown();
        }




        static public void Timer( int i )
        {
            while(true)
            {

                if(ValueModel.IsItWork)
                {
                    System.Threading.Thread.Sleep( i );
                }
            }
        }

        static public void Timer( )
        {
            while(true)
            {

                if(ValueModel.IsItWork)
                {
                    System.Threading.Thread.Sleep( 2000 );
                }
            }
        }

        public void ChangeStatus( )
        {
            if(ValueModel.IsItWork)
            {
                ValueModel.IsItWork = false;
            }
            else if(!ValueModel.IsItWork)
            {
                ValueModel.IsItWork = true;
            }

        }

        public System.Threading.Thread NewThread =
    new System.Threading.Thread( Timer );
    }
}
