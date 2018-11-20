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

namespace lebedev
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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

        public MainWindow()
        {
            InitializeComponent();
            DbInteraction db = new DbInteraction();
            DbInteraction.getListFlights(listFlight);
            DbInteraction.getListTickets(listTicket);
            listviewKab.ItemsSource = listTicket;
            listviewFly.ItemsSource = listFlight;
        }

        private void Button_Fly_Add(object sender, RoutedEventArgs e)
        {
            // with validate
            bool isValidated = true;
            if (!Char.IsDigit(tbInCount.Text, 0))
            {
                tbInCount.BorderThickness = new Thickness(2);
                tbInCount.BorderBrush = Brushes.Red;
                isValidated = false;
            }
            else
            {
                tbInCount.BorderBrush = Brushes.Green;
                if (listviewFly.SelectedItem != null)
                {
                    var item = (Flight)listviewFly.SelectedItem;
                    if (item.countfree >= Convert.ToInt32(tbInCount.Text))
                    {
                        DbInteraction.addNewTicket(item.number.ToString(), tbInF.Text, tbInI.Text, tbInO.Text, tbInCount.Text);
                        DbInteraction.decCount(item.number, Convert.ToInt32(tbInCount.Text));
                        listFlight.Clear();
                        DbInteraction.getListFlights(listFlight);
                        listviewFly.ItemsSource = new List<Flight>();
                        listviewFly.ItemsSource = listFlight;
                        DbInteraction.getListTickets(listTicket);
                        listviewKab.ItemsSource = listFindTicket;
                        listviewKab.ItemsSource = listTicket;
                    }
                }
            }
                if (!isValidated)
                {
                    MessageBox.Show("Поле заполнено некорректно!", "Error");
                }            
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

        private void Button_Fly_Add_Search(object sender, RoutedEventArgs e)
        {
            if(tbCabF.Text.Replace(" ","")!="" && tbCabI.Text.Replace(" ","")!="" && tbCabO.Text.Replace(" ", "") != "")
            {
                listFindTicket.Clear();
                foreach (var item in listTicket)
                {
                    if(item.F== tbCabF.Text.Replace(" ", "") && item.I== tbCabI.Text.Replace(" ", "") && item.O == tbCabO.Text.Replace(" ", ""))
                    {
                        listFindTicket.Add(item);
                    }
                }
                listviewKab.ItemsSource = listTicket;
                listviewKab.ItemsSource = listFindTicket;
            }
        }

        private void Button_Fly_Add_Clear(object sender, RoutedEventArgs e)
        {
            listFindTicket.Clear();
            foreach (var item in listTicket)
            {
                listFindTicket.Add(item);
            }
            tbCabF.Clear();
            tbCabI.Clear();
            tbCabO.Clear();
            listviewKab.ItemsSource = listTicket;
        }

        private void Button_Fly_Add_Delete(object sender, RoutedEventArgs e)
        {
            var item = listviewKab.SelectedItem;
            if (item != null)
            {
                var delButton = (Ticket)item;
                DbInteraction.delTicket(delButton.number, delButton.F, delButton.I, delButton.O, delButton.count);
                DbInteraction.getListTickets(listTicket);
                listviewKab.ItemsSource = new List<Ticket>();
                listviewKab.ItemsSource = listTicket;
            }
        }

        private void Button_Fly_Refresh(object sender, RoutedEventArgs e)
        {
            DbInteraction.getListFlights(listFlight);
            listviewFly.ItemsSource = new List<Flight>();
            listviewFly.ItemsSource = listFlight;

            tbFindAvia.Text = FindAvia;
            tbFindFrom.Text = FindFrom;
            tbFindTo.Text = FindTo;
            tbFindNum.Text = FindNum;
            tbFindType.Text = FindType;

            tbInF.Text = InF;
            tbInI.Text = InI;
            tbInO.Text = InO;
            tbInCount.Text = InCount;
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

        private void tbInF_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbInF.Text == InF)
                tbInF.Text = "";
        }

        private void tbInI_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbInI.Text == InI)
                tbInI.Text = "";
        }

        private void tbInO_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbInO.Text == InO)
                tbInO.Text = "";
        }

        private void tbInCount_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbInCount.Text == InCount)
                tbInCount.Text = "";
        }

        private void tbCabF_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbCabF.Text == CabF)
                tbCabF.Text = "";
        }

        private void tbCabI_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbCabI.Text == CabI)
                tbCabI.Text = "";
        }

        private void tbCabO_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbCabO.Text == CabO)
                tbCabO.Text = "";
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sliderGreen != null && sliderBlue != null && sliderRed.Value != 1)
            {
                TimetableGridUser.Background = new SolidColorBrush(Color.FromRgb((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value));
            }
            else if (sliderGreen != null && sliderBlue != null && sliderRed.Value == 1)
            {
                TimetableGridUser.Background = new SolidColorBrush(Color.FromRgb((byte)130, (byte)118, (byte)240));
                sliderRed.Value = 130;
                sliderGreen.Value = 118;
                sliderBlue.Value = 240;
            }
        }        
    }
}
