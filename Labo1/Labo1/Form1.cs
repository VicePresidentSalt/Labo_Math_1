//PROGRAMME DE MATH REMIS A JONATHAN LORTIE
//PAR MATHIEU DUMOULIN , DAREN-KEN ST-LAURENT ET FRANCIS COTE
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
        //DECLARATION DES DONNES GLOBALES
        private List<List<float>> tab = new List<List<float>>();
        private const int NBECHANTILLON = 20;
        private int[] choisis = new int[NBECHANTILLON];
        private string FileName = "labo1.txt";
        private choix Choix = 0;
        private int indicecourant = 0;
        private Random random = new Random();

        //fonction qui remplie les textbox a l'aide des donnes dans le tableau de choix
        private void fillTB(int indice)
        {
            if (indice >= NBECHANTILLON)
            {
                indicecourant = NBECHANTILLON - 1;
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
        //enumeration des choix d'echantillonage possible
        private enum choix { aleatoire, systematique, strate };
        public Form1()
        {
            InitializeComponent();
        }
        #region "Lecture de Fichier"
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
                for (int i = 0; i < 7; ++i)
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
        //a l'ouverture on lit le fichier
        private void Form1_Load(object sender, EventArgs e)
        {
            ReadTXT_Files();
        }

        //quitte l'application
        private void BTN_QUITTER_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //lorsque on clique sur un boutton on change la valeur du choix pour effectuer le bon échantillonnage
        private void RB_ALEATOIRE_CheckedChanged(object sender, EventArgs e)
        {
            BTN_REGEN.Enabled = true;
            
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
        //vide les textbox
        private void clearTB()
        {
            activerBTN(false);
            TB_A.Clear();
            TB_B.Clear();
            TB_C.Clear();
            TB_D.Clear();
            TB_E.Clear();
            TB_F.Clear();
            LBL_Nombre.Text = "";

        }
        //met a jour les boutton de l'application
        private void updateControls()
        {
            activerBTN(false, true);
            indicecourant = 0;
            fillTB(indicecourant);
            updatelabel();
        }

        //regenaire dependament de quelle bouton de selection qui est coché
        private void BTN_REGEN_Click(object sender, EventArgs e)
        {
            //change le texte du bouton
            BTN_REGEN.Text = "Regénérer";
            //vide les textbox deja remplis
            clearTB();
            switch(Choix)
            {
                    //SI LE BOUTTON ALEATOIRE EST SELECTIONNER
                case choix.aleatoire:
                    {
                        //traitement
                        Numero1();
                        //met a jour les boutton de l'application
                        updateControls();
                        //fin
                        break;
                    }
                case choix.systematique:
                    {
                        //traitement
                        Numero2();
                        //met a jour les boutton de l'application
                        updateControls();
                        //fin
                        break;
                    }
                case choix.strate:
                    {
                        //traitement
                        Numero3();
                        //met a jour les boutton de l'application
                        updateControls();
                        //fin
                        break;
                    }
            }
        }
        //fonction qui fais avancer dans le tableau de 1
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
        //fonction qui fais reculer dans le tableau pour revenir en arriere de 1
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
        //Cette methode change les chiffre et l'indice dans le tableau pour indique a quelle position nous somme dans les enregistrements
        private void updatelabel()
        {
            LBL_Nombre.Text = (indicecourant + 1).ToString() + " # " + (choisis[indicecourant]+1).ToString();
        }
        private void Numero1()
        {
            //tant que le programme n'as pas choisis assez de personne dans la liste des donnees
            for (int i = 0; i < NBECHANTILLON; i++)
            {
                bool dejatirer = false;
                int randomnumber = random.Next(0, tab.Count);
                //pour chaque elements dans le tableau de position choisis on verifier si le nombre aleatoire a deja ete tirer
                foreach (int element in choisis)
                {
                    //si le chiffre est deja tirer
                    if (element == randomnumber && dejatirer == false)
                    {
                        //on ne veut pas utiliser ce chiffre puisqu'il est dedans deja
                        dejatirer = true;
                        //arret de la boucle
                        break;
                    }
                }
                // si le chiffre n'as pas ete tirer
                if (!dejatirer)
                {
                    //ajout de la position dans le tableau des position choisis
                    choisis[i] = randomnumber;
                }
                else //sinon on retire un nouveau chiffre
                {
                    //on veut boucler un tour de plus puisque le nombre est deja tirer
                    i--;
                }

            }
        }
        private void Numero2()
        {
            //calcul du bond
            int bond = tab.Count / NBECHANTILLON;
            //nouveau chiffre 'Random'
            int choix = random.Next(0,tab.Count);
            
            //declaration du compteur
            int compteur = 1;
            //on met le choix directement car il est le premier au hasard les autre suivront le bond
            choisis[0] = choix;

            //tant que le compteur n'est pas à la taille de l'échantillon
            while(compteur < NBECHANTILLON)
            {
                //Si le résultat du bon est plus grand que le tableau
                if(choix + bond >= tab.Count)
                {
                    //ajout du bon au choix(choix est la position parmis les elements) on revien au debut du tableau (0) + reste
                    choix = (choix += bond) % tab.Count;
                    //Ajout de la position de l'element piger dans le tableau
                    choisis[compteur] = choix;
                    //Addition du compteur
                    compteur++;
                }
                    //sinon le resultat est plus petit que le nombre d'elements dans le tableau (on fais seulement incrementer)
                else
                {
                    //ajout du bon au choix(choix est la position parmis les elements)
                    choix += bond;
                    //Ajout de la position de l'element piger dans le tableau
                    choisis[compteur] = choix;
                    //Addition du compteur
                    compteur++;
                }
            }
        }
        private void Numero3()
        {
            // Créé un tableau qui va contenir tout les hommes
            List<List<float>> tabHomme = new List<List<float>>();
            // Créé un tableau qui va contenir toutes les femmes
            List<List<float>> tabFemme = new List<List<float>>();
            int compteurFemme = 0;
            int compteurHomme = 0;
            // Ajout des personnes dans les deux tableau (séparé par sexe)
            for (int i = 0; i < tab.Count; ++i)
            {
                if (tab[i][0] == 1) // Si c'est un homme
                {
                    // On l'ajoute dans le tableau d'hommes
                    tabHomme.Add(tab[i]);
                    // On incrémente le nombre d'hommes
                    ++compteurHomme;
                }
                if (tab[i][0] == 2) // Si c'est une femme
                {
                    // On l'ajoute dans le tableau de femmes
                    tabFemme.Add(tab[i]);
                    // On incrémente le nombre de femmes
                    ++compteurFemme;
                }
            }
            // Sélection au hasard dans les deux strates en ajoutant les personnes dans la listeTemp (Liste finale)
            int compteur = 0;
            for (int i = 0; i < compteurHomme * 20 / tab.Count; ++i)
            {
                // Créé un nombre aléatoire de 0 à le nombre d'éléments dans le tableau d'hommes
                int randomNum = random.Next(0, tabHomme.Count);
                // Insère la position de la personne choisie aléatoirement dans un tableau 
                choisis[compteur] = Convert.ToInt32(tabHomme[randomNum][6]);
                compteur++;
                tabHomme.RemoveAt(randomNum);
            }
            for (int x = 0; x <= compteurFemme * 20 / tab.Count; ++x)
            {
                // Créé un nombre aléatoire de 0 à le nombre d'éléments dans le tableau de femmes
                int randomNum = random.Next(0, tabFemme.Count);
                // Insère la position de la personne choisie aléatoirement dans un tableau 
                choisis[compteur] = Convert.ToInt32(tabFemme[randomNum][6]);
                compteur++;
                tabFemme.RemoveAt(randomNum);
            }
        }
    }
}
