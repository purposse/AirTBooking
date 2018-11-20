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
using System.Windows.Shapes;

namespace lebedev
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        List<Flight> listFlight = new List<Flight>();
        List<Flight> listFindFlight = new List<Flight>();
        List<Ticket> listTicket = new List<Ticket>();
        List<Ticket> listFindTicket = new List<Ticket>();
        private const string FindAvia = "Авиакомпания";
        private const string FindFrom = "Откуда";
        private const string FindTo = "Куда";
        private const string FindNum = "Номер полёта";
        private const string FindType = "Класс";

        private const string InF = "Фамилия";
        private const string InI = "Имя";
        private const string InO = "Отчество";
        private const string InCount = "Кол-во билетов";

        private const string CabF = "Фамилия";
        private const string CabI = "Имя";
        private const string CabO = "Отчество";

        public AdminWindow()
        {
            InitializeComponent();
            
            DbInteraction db = new DbInteraction();
            DbInteraction.getListFlights(listFlight);
            DbInteraction.getListTickets(listTicket);
            listviewFly.ItemsSource = listFlight;
        }

        private void Button_Fly_Search(object sender, RoutedEventArgs e)
        {
            string av = tbFindAvia.Text.Replace(" ", "");
            string fr = tbFindFrom.Text.Replace(" ", "");
            string tto = tbFindTo.Text.Replace(" ", "");
            string ttypee = tbFindType.Text.Replace(" ", "");
            listFindFlight.Clear();
            listviewFly.ItemsSource = listFlight;
            foreach (var item in listFlight)
            {
                if (item.avia.Contains(av) || av == "")
                    if (item.from.Contains(fr) || fr == "")
                        if (item.to.Contains(tto) || tto == "")
                            if (item.flyclass.Contains(ttypee) || ttypee == "")
                            {
                                if (tbFindNum.Text.Replace(" ", "") == "")
                                {
                                    listFindFlight.Add(item);
                                }
                                else
                                {
                                    int numm = Convert.ToInt32(tbFindNum.Text.Replace(" ", ""));
                                    if (item.number == numm)
                                    {
                                        listFindFlight.Add(item);
                                    }
                                }
                            }
            }

            listviewFly.ItemsSource = listFindFlight;
        }

        private void Button_Fly_Clear(object sender, RoutedEventArgs e)
        {
            tbFindNum.Clear();
            tbFindAvia.Clear();
            tbFindFrom.Clear();
            tbFindTo.Clear();
            tbFindType.Clear();
            listviewFly.ItemsSource = listFlight;
        }

        public void Button_Fly_Refresh(object sender, RoutedEventArgs e)
        {
            DbInteraction.getListFlights(listFlight);
            listviewFly.ItemsSource = new List<Flight>();
            listviewFly.ItemsSource = listFlight;

            tbFindAvia.Text = FindAvia;
            tbFindFrom.Text = FindFrom;
            tbFindTo.Text = FindTo;
            tbFindNum.Text = FindNum;
            tbFindType.Text = FindType;
        }

        private void tbFindAvia_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbFindAvia.Text == FindAvia)
                tbFindAvia.Text = "";
        }

        private void tbFindFrom_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbFindFrom.Text == FindFrom)
                tbFindFrom.Text = "";
        }

        private void tbFindTo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbFindTo.Text == FindTo)
                tbFindTo.Text = "";
        }

        private void tbFindNum_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbFindNum.Text == FindNum)
                tbFindNum.Text = "";
        }

        private void tbFindType_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbFindType.Text == FindType)
                tbFindType.Text = "";
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sliderGreen != null && sliderBlue != null && sliderRed.Value != 1)
            {
                TimetableGrid.Background = new SolidColorBrush(Color.FromRgb((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value));
                
            }
            else if (sliderGreen != null && sliderBlue != null && sliderRed.Value == 1)
            {
                TimetableGrid.Background = new SolidColorBrush(Color.FromRgb((byte)130, (byte)118, (byte)240));
                sliderRed.Value =130;
                sliderGreen.Value =118;
                sliderBlue.Value =240;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e) //add
        {
            AddForm addWin = new AddForm(this, null);
            addWin.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e) //edit
        {
            if (listviewFly.SelectedItem != null)
            {
                AddForm addwin = new AddForm(this, (listviewFly.SelectedItem as Flight));
                addwin.Show();
            }
        }
        private void button3_Click(object sender, RoutedEventArgs e) //delete
        {
            if (listviewFly.SelectedItem != null)
            {
                DbInteraction.delFlight((listviewFly.SelectedItem as Flight).number);
                Button_Fly_Refresh(null, null);
            }
        }
    }
}
