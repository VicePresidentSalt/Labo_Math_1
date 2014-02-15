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
        private const int NBECHANTILLON = 20;
        private int[] choisis = new int[NBECHANTILLON];
        private string FileName = "labo1.txt";
        private choix Choix = 0;
        private int indicecourant = 0;
        private int nbHomme = 0;
        private int nbFemme = 0;
        private Random random = new Random();

        private void fillTB(int indice)
        {
            if (indice >= NBECHANTILLON)
            {
                indicecourant = 19;
                indice = indicecourant;
            }
            else if (indice < 0)
            {
                indicecourant = 0;
                indice = indicecourant;
            }
            TB_A.Text = tab[choisis[indice]][0].ToString();
            TB_B.Text = tab[choisis[indice]][1].ToString();
            TB_C.Text = tab[choisis[indice]][2].ToString();
            TB_D.Text = tab[choisis[indice]][3].ToString();
            TB_E.Text = tab[choisis[indice]][4].ToString();
            TB_F.Text = tab[choisis[indice]][5].ToString();
        }

        private enum choix { aleatoire, systematique, strate };
        public Form1()
        {
            InitializeComponent();
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
                for (int i = 0; i < 6; ++i)
                {
                    temp.Add(float.Parse(tokens[i]));
                }
                tab.Add(temp);
            }
            this.Refresh();
        }

        #endregion
        private void activerBTN(bool status)
        {
            BTN_BACK.Enabled = status;
            BTN_NEXT.Enabled = status;
        }
        private void activerBTN(bool status1,bool status2)
        {
            BTN_BACK.Enabled = status1;
            BTN_NEXT.Enabled = status2;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ReadTXT_Files();
        }

        private void BTN_QUITTER_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RB_ALEATOIRE_CheckedChanged(object sender, EventArgs e)
        {
            BTN_REGEN.Enabled = true;
            clearTB();
            if (RB_ALEATOIRE.Checked == true)
            {
                Choix = choix.aleatoire;
            }
            else if(RB_SYSTEMATIQUE.Checked == true)
            {
                Choix = choix.systematique;
            }
            else if(RB_STRATE.Checked == true)
            {
                Choix = choix.strate;
            }
        }
        private void clearTB()
        {
            activerBTN(false);
            TB_A.Clear();
            TB_B.Clear();
            TB_C.Clear();
            TB_D.Clear();
            TB_E.Clear();
            TB_F.Clear();

        }

        private void BTN_REGEN_Click(object sender, EventArgs e)
        {
            BTN_REGEN.Text = "Regénérer";
            switch(Choix)
            {
                case choix.aleatoire:
                    {
                        for (int i = 0; i < NBECHANTILLON; i++)
                        {
                            bool dejatirer = false;
                            int randomnumber = random.Next(0, 120);
                            foreach(int element in choisis)
                            {
                                if(element == randomnumber && dejatirer == false)
                                {
                                    dejatirer = true;
                                    break;
                                }
                            }
                            if(!dejatirer)
                            {
                                choisis[i] = randomnumber;
                            }
                            else
                            {
                                i--;
                            }
                            
                        }
                        activerBTN(false,true);
                        indicecourant = 0;
                        
                        fillTB(indicecourant);
                        updatelabel();
                        break;
                    }
                case choix.systematique:
                    {

                        break;
                    }
                case choix.strate:
                    {
                        
                        break;
                    }
            }
        }

        private void BTN_NEXT_Click(object sender, EventArgs e)
        {
            indicecourant++;
            updatelabel();
            if (indicecourant == 19)
                BTN_NEXT.Enabled = false;
            else
                activerBTN(true);

            fillTB(indicecourant);
        }

        private void BTN_BACK_Click(object sender, EventArgs e)
        {
            indicecourant--;
            updatelabel();
            if (indicecourant == 0)
                BTN_BACK.Enabled = false;
            else
                activerBTN(true);
            fillTB(indicecourant);
        }
        private void updatelabel()
        {
            LBL_Nombre.Text = (indicecourant + 1).ToString();
        }
    }
}
