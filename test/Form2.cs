using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listdata d1 = new listdata();
            d1.Names.Add("Tom");
            d1.Names.Add("Juni");

            dataTreeListView1.DataMember = "Names";
            dataTreeListView1.DataSource = d1;

            //dataTreeListView1.AddObject(d1);
        }
    }

    public class listdata
    {
        public List<string> Names = new List<string>();
        //public string Name { get; set; }
        //public int Age { get; set; }
    }
}
