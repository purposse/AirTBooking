using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lebedev
{
    public partial class AddForm : Form
    {
        AdminWindow ad;
        Flight f;

    public AddForm(AdminWindow add, Flight fly)
        {
            InitializeComponent();
            ad = add;
            f = fly;
            if (f != null)
            {
                textBox1.Text = f.avia;
                textBox2.Text = f.from;
                textBox3.Text = f.to;
                textBox4.Text = f.dayfrom;
                textBox5.Text = f.dayto;
                textBox6.Text = f.timefrom;
                textBox7.Text = f.timeto;
                textBox8.Text = f.flyclass;
                button1.Text = "Изменить";
                this.Text = "Изменение рейса";
            }
            else
            {
                button1.Text = "Добавить";
                this.Text = "Добавление рейса";
            }
        }

        private void AddForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Flight l = new Flight
            {
                avia = textBox1.Text,
                number = Math.Abs((textBox1.Text + textBox2.Text + textBox3.Text).GetHashCode())%250,
                from = textBox2.Text,
                to = textBox3.Text,
                dayfrom = textBox4.Text,
                dayto = textBox5.Text,
                timefrom = textBox6.Text,
                timeto = textBox7.Text,
                flyclass = textBox8.Text,
                countfree = 100
            };
            if (f != null)
            {
                l.number = f.number;
                DbInteraction.replaceFlight(l.avia, l.number, l.from, l.to, l.dayfrom, l.dayto, l.timefrom,
                    l.timeto, l.flyclass, l.countfree);
            }
            else
                DbInteraction.addNewFlight(l.avia, l.number, l.from, l.to, l.dayfrom, l.dayto, l.timefrom,
                    l.timeto, l.flyclass, l.countfree);
            ad.Button_Fly_Refresh(null, null);
            ad.Focus();
            this.Close();
        }
    }
}
