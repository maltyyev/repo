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
    static public class ValueModel
    {
        public static double Pressure;
        public static double Speed;
        public static double Temperature;

        public static bool IsItDrilled = false;
        public static bool IsItDrillingNow = false;
        public static bool IsItWorking = false;
        public static bool Fire = false;

        public static double FireTime = 0;
        public static bool Leakage;
        public static bool IsMiningBegin;
    }
    public partial class MainWindow : Window
    {
        public System.Threading.Thread TimerThread;
        public MainWindow( )
        {
            InitializeComponent();
            TimerThread = new System.Threading.Thread( Timer );
            TimerThread.Start();
        }

        private void OnStart_Click( object sender, RoutedEventArgs e )
        {
            ChangeWorkingStatus();
            if(ValueModel.IsItWorking)
            {
                OnStart.Content = "Стоп";
            }
            else if(!ValueModel.IsItWorking)
            {
                OnStart.Content = "Старт";
            }
        }

        private void Exit_Click( object sender, RoutedEventArgs e )
        {
            Application.Current.Shutdown();
        }





        public void Conrtoller( )
        {
            Fireing();
            Drilling();
        }

        static public void Fireing( )
        {
            if(ValueModel.Fire)
            {
                ValueModel.FireTime -= 1;

                if(ValueModel.FireTime <= 0)
                {
                    ValueModel.Fire = false;
                    ValueModel.FireTime = 0;
                }
            }
        }

        public void Drilling( )
        {
            MessageBox.Show(Convert.ToString(DrillingBar.Dispatcher.CheckAccess()));//темп, для 
            if(DrillingBar.Dispatcher.CheckAccess())        //проверяет, есть ли доступ к этому элементу из фонового потока.
                                                            // Да, ошибок не вылетает, но... Не жадничай же ты главный поток.
                                                            // Может быть поможет  System.Threading.Thread.Sleep( 0 );  
                                                            //в главном потоке... только вот где это всунуть, хм.
                                                            //  https://msdn.microsoft.com/ru-ru/library/system.windows.threading.dispatcher.begininvoke(v=vs.110).aspx 
                                                            //вот вроде как даже солюшн, но мне пора спать, а с тебя запиленный метод к утру
            {
                if(ValueModel.IsItDrillingNow)
                {
                    if(!ValueModel.IsItDrilled)
                    {
                        if(DrillingBar.Value < 95)
                        {
                            DrillingBar.Value += 5;
                        }
                        else if(DrillingBar.Value >= 95)
                        {
                            DrillingBar.Value = 100;
                        }
                        else if(DrillingBar.Value == 100)
                        {
                            DrillingBar.Value = 0;
                            ValueModel.IsItDrilled = true;
                            DrillingButton.IsEnabled = false;
                        }
                    }
                }
            }
        }
        public void Timer( int i )
        {
            while(true)
            {
                if(ValueModel.IsItWorking)
                {
                    System.Threading.Thread.Sleep( i );
                    Conrtoller();
                }
            }
        }

        public void Timer( )
        {
            while(true)
            {
                if(ValueModel.IsItWorking)
                {
                    System.Threading.Thread.Sleep( 1000 );
                    Conrtoller();
                }
            }
        }

        public void ChangeWorkingStatus( )
        {
            if(ValueModel.IsItWorking)
            {
                ValueModel.IsItWorking = false;
            }
            else if(!ValueModel.IsItWorking)
            {
                ValueModel.IsItWorking = true;
            }

        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            if(!ValueModel.IsItDrillingNow)
            {
                ValueModel.IsItDrillingNow = true;
            }
            else if(ValueModel.IsItDrillingNow)
            {
                ValueModel.IsItDrillingNow = false;
            }
        }
    }
}