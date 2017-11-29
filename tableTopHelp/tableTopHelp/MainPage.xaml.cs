using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tableTopHelp
{
    public partial class MainPage : ContentPage
    {
        // create champion list, get/set (name,level,race,selected), CREATE SHIT HERE, JORDAN!!
        public ChampionList championsList = new ChampionList();
        public Boolean createIsSelected = false,
                        deleteIsSelected = false;
        
        // This is the MOTHER FUCKING CONSTRUCTOR FOR MAINPAGE, NOT A RUN METHOD.... JORDAN... 3 HOURS LOST FOREVER!!!
        public MainPage()
        {
            InitializeComponent();
            // Set BindingContext for this page to access buttons and labels from XAML
            BindingContext = this;
            

            // Actions for "Create" button clicked
            if (championsList.numChampions == 5)
            {
                // grey-out chracter create button
                createChampButt.Opacity = 0.25;                
            }//end if
            
            
                // create character actions
            createChampButt.Clicked += OnButtonCreateClicked;
                // create mode, check select buttons               
           

            //if (createChampButt.BackgroundColor.Equals(Color.Green))
            //{
           //     championsList.selectChampion(championsList.numChampions - 1);
             //   DisplayAlert("Create Character", "What is the character's name?", "Submit");
           // }


            // Actions for "Delete" button clicked
            deleteChampButt.Clicked += OnButtonDeleteClicked;

            // Set label text to champion names
            championOne.Text = championsList.names[0];
            championTwo.Text = championsList.names[1];
            championThree.Text = championsList.names[2];
            championFour.Text = championsList.names[3];
            championFive.Text = championsList.names[4];

            // Actions for "Select" button clicks, 1-5
            selectChampOne.Clicked += OnButtonOneClicked;
          

           
        } // end MainPage
        public class ChampionList
        {
            public String[] names { get; set; }
            public int length { get; set; }
            public int[] levels { get; set; }
            public String[] races { get; set; }
            public Boolean[] isSelected { get; set; }
            public int numChampions { get; set; }

            public ChampionList()
            {
                names = new string[] { "Brumal Orc-Killer",
                                       "Magda the Confessor",
                                       "Kelgen IronShield",
                                       "Mufasa Dingred",
                                       ""};
                length = names.Length;
                levels = new int[] {0,0,0,0,0};
                races = new String[] { "","","","",""};
                isSelected = new Boolean[] { false, false, false, false, false };
                numChampions = 0;
                
                for(int i = 0; i < length; i++)
                {
                    if (names[i] != "")
                        numChampions++;
                }

            }//End constructor

            public void addChampion(String name, int level, String race)
            {
                // check if full
                if (length < 5)
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (names[i].Equals(""))
                        {
                            names[i] = name;
                            levels[i] = level;
                            races[i] = race;
                            i = length + 1;
                        }//End if
                    }//End for
                }//end if
                
            }//end addChampion

            public void selectChampion(int index)
            {
                for(int i = 0; i<length; i++)
                {
                    if (i != index)
                    {
                        isSelected[i] = false;
                    }//end if
                   
                    else if (i == index)
                    {
                        isSelected[i] = true;
                    }//end else if
                }//end for             
            }// end selectChampion
        }// end ChampionList

        
  
        //Create
        async void OnButtonCreateClicked(object sender, EventArgs e)
        {
            // check for grey-out
            if (championsList.numChampions >= 5)
            {
                DisplayAlert("Error", "You've reached the maximum number of characters. Please delete a character to continue.", "OK");
            }//end if
            else
            {
                var createResponse = await DisplayAlert("Create Character", "Do you want to create a new character?", "Yes", "No");

                if (createResponse)
                {
                    createChampButt.BackgroundColor = Color.Green;
                    createIsSelected = true;
                }//end if
            }//end else
            
        }// end OnButtonCreateClicked

        // Delete
        async void OnButtonDeleteClicked(object sender, EventArgs e)
        {
            var deleteResponse = await DisplayAlert("Delete Champion", "Are you sure?", "Yes", "No");

            if (deleteResponse)
            {
                deleteChampButt.BackgroundColor = Color.Red;
                deleteIsSelected = true;
                DisplayAlert("Instructions", "Select a Champion to Delete", "Ok");

            }

        }
        async void OnButtonOneClicked(object sender, EventArgs e)
        {
            // Check for create
            if (!createIsSelected && !deleteIsSelected)
            {
                // STUB - load champion
                championOne.Text = "Selected";
                championsList.isSelected[0] = true;
            }

            else if (createIsSelected)
            {                
                
            }

            else if (deleteChampButt.BackgroundColor.Equals(Color.Red))
            {
                championOne.Text = "DELETED";
                deleteChampButt.BackgroundColor = Color.Default;
            }
            
            
        }

        

       
    }

    
}
