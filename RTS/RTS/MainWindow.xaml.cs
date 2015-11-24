using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        public delegate void Setter(ProgressBar pBar);

        Setter changer;

        public double drillingBarValue;

        public System.Threading.Thread TimerThread;
        public MainWindow()
        {
            InitializeComponent();

            changer = new Setter(ChangeProgressBarValue);

            TimerThread = new System.Threading.Thread(Timer);
            TimerThread.IsBackground = true;
            TimerThread.Start();
        }

        private void OnStart_Click(object sender, RoutedEventArgs e)
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void Conrtoller()
        {
            Fireing();
            Drilling();
        }

        static public void Fireing()
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

        public void ChangeProgressBarValue(ProgressBar pBar)
        {
            if (pBar.Value == 100)
            {
                pBar.Value = 0;
                ValueModel.IsItDrilled = true;
                DrillingButton.IsEnabled = false;
            }
            else if (pBar.Value < 95)
                pBar.Value += 5;
            else if (pBar.Value >= 95)
                pBar.Value = 100;
        }

        public void Drilling()
        {
            if (ValueModel.IsItDrillingNow)
            {
                if (!ValueModel.IsItDrilled)
                {
                    Dispatcher.Invoke(changer, DrillingBar);
                }
            }
        }

        public void Timer(int i)
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

        public void Timer()
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

        public void ChangeWorkingStatus()
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

        private void Button_Click(object sender, RoutedEventArgs e)
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