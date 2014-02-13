using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labo1
{
    public partial class Form1 : Form
    {
        private List<List<float>> tab = new List<List<float>>();
        private string FileName = "labo1.txt";
        public Form1()
        {
            InitializeComponent();
            ReadTXT_Files();
        }
        #region "OpenFile button"
        private void FB_Open_Click(object sender, EventArgs e)
        {
            ReadTXT_Files();
        }

        private void ReadTXT_Files()
        {
                ReadCoordinates(FileName);
        }

        private void ReadCoordinates(string url)
        {
            tab.Clear();
            string[] lines = System.IO.File.ReadAllLines(url);
            foreach (string line in lines)
            {
                List<float> temp = new List<float>();
                string[] tokens = line.Split(';');
                for(int i = 0; i < 6; ++i)
                {
                    temp.Add(float.Parse(tokens[i]));
                }
                tab.Add(temp);
            }
            this.Refresh();
        }

        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
