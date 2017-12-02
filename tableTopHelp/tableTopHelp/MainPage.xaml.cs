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
        // Example champion
        public App.Champion kelgen = new App.Champion("Kelgen");
        public App.Champion brumal = new App.Champion("Brumal");
        public App.Champion winter = new App.Champion("Winter Star");
        public App.Champion mufasa = new App.Champion("Mufasa");
        public App.Champion magda = new App.Champion("Magda");

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
                createChampButt.Text = "List is Full";
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
            championOne.Text = championsList.championArray[0].name;
            championTwo.Text = championsList.championArray[1].name;
            championThree.Text = championsList.championArray[2].name;
            championFour.Text = championsList.championArray[3].name;
            championFive.Text = championsList.championArray[4].name;

            // Actions for "Select" button clicks, 1-5
            selectChampOne.Clicked += OnButtonOneClicked;
          

           
        } // end MainPage
        public class ChampionList
        {
            public App.Champion[] championArray;
            public App.Champion temporaryChampion;
            
            public int length { get; set; }
            public int[] levels { get; set; }
            public String[] races { get; set; }
            public Boolean[] isSelected { get; set; }
            public int numChampions { get; set; }
            
            public ChampionList()
            {
                championArray = new App.Champion[] { new App.Champion("Create New Character"), new App.Champion("Create New Character"),
                                                     new App.Champion("Create New Character"), new App.Champion("Create New Character"),
                                                    new App.Champion("Create New Character") };
                temporaryChampion = new App.Champion();
           
                length = championArray.Length;                
                isSelected = new Boolean[] { false, false, false, false, false };
                numChampions = 0;
                
                for(int i = 0; i < length; i++)
                {
                    if (championArray[i].name != "Create New Character")
                        numChampions++;
                }

            }//End constructor

            public void addChampion(String newChampion)
            {
                // check if full
                if (numChampions < 5)
                {
                    championArray[numChampions] = new App.Champion(newChampion);
                    numChampions++;
                }//end if
                
            }//end addChampion


            public void removeChampion(int index)
            {
                numChampions--;
                championArray[index] = new App.Champion("Create New Character");
                for(int x = 0,y = 1; x < championArray.Length-1; x++, y++)
                {
                    //compare each
                    if(championArray[x].name.Equals("Create New Character") && !championArray[y].name.Equals("Create New Character"))
                    {
                        temporaryChampion = championArray[y];
                        championArray[y] = championArray[x];
                        championArray[x] = temporaryChampion;
                        temporaryChampion = new App.Champion();
                    }
                }

                // Set label text to champion names
            



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
            

            if (championsList.numChampions >= 5)
            {
                await DisplayAlert("Error", "You've reached the maximum number of characters. Please delete a character to continue.", "OK");
            }//end if

            try
            {
                if(!input.Text.Equals(""))
                { 
                    championsList.addChampion(input.Text);
                    input.Text = "";                   
                }
            }

            catch
            {
                await DisplayAlert("Error", "You must type a new champion name.", "OK");
            }


            if (championsList.numChampions >= 5)
            {
                createChampButt.Opacity = 0.25;
                createChampButt.Text = "List is Full";
            }
            else
            {
                createChampButt.Opacity = 1;
                createChampButt.Text = "Create Champion";
            }

            // Set label text to champion names
            championOne.Text = championsList.championArray[0].name;
            championTwo.Text = championsList.championArray[1].name;
            championThree.Text = championsList.championArray[2].name;
            championFour.Text = championsList.championArray[3].name;
            championFive.Text = championsList.championArray[4].name;

        }// end OnButtonCreateClicked

        // Delete
        async void OnButtonDeleteClicked(object sender, EventArgs e)
        {
            var deleteChampionFound = false;
            var deleteResponse = false;
            var championDeleteIndex = -1;

            

            for(int i = 0; i < championsList.length; i++)
            {
                try
                {
                    if(!deleteChampionFound && input.Text.Equals(championsList.championArray[i].name) &&
                                           !championsList.championArray[i].name.Equals("Create New Character"))
                    {
                        deleteResponse = await DisplayAlert("Delete Champion", "Do you want to delete " + 
                                                  championsList.championArray[i].name + "?", "Yes", "No");
                        deleteChampionFound = true;
                        championDeleteIndex = i;
                    }
                }

                catch
                {
                    DisplayAlert("Error", "You must type a champion name to be deleted.", "Ok.");
                    i = championsList.length + 1;
                    input.Text = null;
                }
            }

            if(!deleteChampionFound && championDeleteIndex != -1)
            {
                DisplayAlert("Error", "There is no champion named " + input.Text, "OK");
                input.Text = null;
            }
           

            if (deleteResponse)
            {
                await DisplayAlert("Instructions", championsList.championArray[championDeleteIndex].name + 
                                                   " is about to be deleted.", "Ok");
                championsList.removeChampion(championDeleteIndex);
                createChampButt.Text = "Create Champion";
                createChampButt.Opacity = 1.0;   
                input.Text = null;                         
                

            }
            // Set label text to champion names
            championOne.Text = championsList.championArray[0].name;
            championTwo.Text = championsList.championArray[1].name;
            championThree.Text = championsList.championArray[2].name;
            championFour.Text = championsList.championArray[3].name;
            championFive.Text = championsList.championArray[4].name;

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
