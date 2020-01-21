using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Timers;

namespace Pomodore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private readonly Timer _timer = new Timer();
        private readonly Timer _visibilityTimer = new Timer();
        private const double _opacityChangeStep = 0.03;
        private TimeProvider _timeProvider = new TimeProvider();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += new RoutedEventHandler(Window_Loaded);
            StartTimer();

        }

        private void StartTimer()
        {
            _timer.AutoReset = true;
            _timer.Interval = 10000;
            _timer.Elapsed += ElapsedTime;
            _timer.Enabled = true;

            _visibilityTimer.AutoReset = true;
            _visibilityTimer.Interval = 30;
            _visibilityTimer.Elapsed += ElapsedVisibilityTime;
            _visibilityTimer.Enabled = true;
            
        }

        private void ElapsedVisibilityTime(object sender, EventArgs e)
        {
            if (_expectedOpacity > OpacityV)
            {
                OpacityV = OpacityV + _opacityChangeStep > _expectedOpacity ? _expectedOpacity : OpacityV + _opacityChangeStep;
            }
            else if (_expectedOpacity < OpacityV)
            {
                OpacityV = OpacityV - _opacityChangeStep < _expectedOpacity ? _expectedOpacity : OpacityV - _opacityChangeStep;
            }
        }
        private DateTime _lastClearTime = DateTime.MinValue;
        private void ElapsedTime(object sender, EventArgs e)
        {
            if (_timeProvider.IsPomodoreTime(_lastClearTime))
            {
                ShowPomodore();
            }
            else
            {
                HidePomodore();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            HidePomodore();
        }

        private void ShowPomodore()
        {
            _expectedOpacity = 1;
        }
        private void HidePomodore()
        {
            _lastClearTime = DateTime.Now;
            _expectedOpacity = 0;
        }

        private double _expectedOpacity;
        private double _opacityV;
        public double OpacityV
        {
            get => _opacityV;
            set
            {
                _opacityV = value;
                OnPropertyRaised("OpacityV");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
