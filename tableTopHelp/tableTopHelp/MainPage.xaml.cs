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
       
        public ChampionList championsList = new ChampionList();
        public Boolean createIsSelected = false,
                        deleteIsSelected = false;
        // Example champion
        public App.Champion kelgen = new App.Champion("Kelgen");
        public App.Champion brumal = new App.Champion("Brumal");
        public App.Champion winter = new App.Champion("Winter Star");
        public App.Champion mufasa = new App.Champion("Mufasa");
        public App.Champion magda = new App.Champion("Magda");

        
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
           

          


            // Actions for "Delete" button clicked
            deleteChampButt.Clicked += OnButtonDeleteClicked;

            // Set label text to champion names
            championOne.Text = championsList.championArray[0].name + "\rLVL - " + championsList.championArray[0].level.ToString();
            championTwo.Text = championsList.championArray[1].name + "\rLVL - " + championsList.championArray[1].level.ToString();
            championThree.Text = championsList.championArray[2].name + "\rLVL - " + championsList.championArray[2].level.ToString();
            championFour.Text = championsList.championArray[3].name + "\rLVL - " + championsList.championArray[3].level.ToString();
            championFive.Text = championsList.championArray[4].name + "\rLVL - " + championsList.championArray[4].level.ToString();

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
                championArray = new App.Champion[] { new App.Champion(), new App.Champion(),
                                                     new App.Champion(), new App.Champion(),
                                                    new App.Champion() };
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
                // decrement number of champions
                numChampions--;
                // add blank champion to list at removal location
                championArray[index] = new App.Champion();
                // sort array, pushing blanks down
                for(int x = 0,y = 1; x < championArray.Length-1; x++, y++)
                {
                    //compare each
                    if(championArray[x].name.Equals("Create New Character") && !championArray[y].name.Equals("Create New Character"))
                    {
                        //exchange
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
            championOne.Text = championsList.championArray[0].name + "\rLVL - " + championsList.championArray[0].level.ToString();
            championTwo.Text = championsList.championArray[1].name + "\rLVL - " + championsList.championArray[1].level.ToString();
            championThree.Text = championsList.championArray[2].name + "\rLVL - " + championsList.championArray[2].level.ToString();
            championFour.Text = championsList.championArray[3].name + "\rLVL - " + championsList.championArray[3].level.ToString();
            championFive.Text = championsList.championArray[4].name + "\rLVL - " + championsList.championArray[4].level.ToString();

        }// end OnButtonCreateClicked

        // Delete
        async void OnButtonDeleteClicked(object sender, EventArgs e)
        {
            // found champion escape boolean
            var deleteChampionFound = false;
            // prompt response boolean
            var deleteResponse = false;
            // index value of found champion
            var championDeleteIndex = -1;

            if (input.Text.Equals(""))
                await DisplayAlert("Error", "You must type a champion name to be deleted.", "Ok.");
            // search championArray
            for (int i = 0; i < championsList.length; i++)
            {
                
                //try-catch for null input
                try
                {
                    // "if champion isnt found, AND text at this spoke equals input, AND input isn't basic constructor name"
                    if(!deleteChampionFound && input.Text.Equals(championsList.championArray[i].name) &&
                                           !championsList.championArray[i].name.Equals("Create New Character"))
                    {
                        // Prompt user to confirm champion delete by name, save response
                        deleteResponse = await DisplayAlert("Delete Champion", "Do you want to delete " + 
                                                  championsList.championArray[i].name + "?", "Yes", "No");
                        // set found champion escale value
                        deleteChampionFound = true;
                        // set found champion index for later reference
                        championDeleteIndex = i;
                    }
                }

                catch
                {
                    DisplayAlert("Error", "You must type a champion name to be deleted.", "Ok.");
                    // escape for loop
                    i = championsList.length + 1;
                    input.Text = "";
                }
            }
            // "if champion is not found, AND champion index isnt original number:
            if(!deleteChampionFound && !input.Text.Equals(""))
            {
                DisplayAlert("Error", "There is no champion named " + input.Text, "OK");
                input.Text = "";
            }
           
            // if user affirms deletion
            if (deleteResponse)
            {
                // prompt for awareness
                await DisplayAlert("Instructions", championsList.championArray[championDeleteIndex].name + 
                                                   " is about to be deleted.", "Ok");
                // remove champion at found index
                championsList.removeChampion(championDeleteIndex);
                // reset create button, in case it was set to "full"
                createChampButt.Text = "Create Champion";
                createChampButt.Opacity = 1.0;   
                input.Text = "";                         
            }
            else
            {
                if (championsList.numChampions >= 5)
                {
                    createChampButt.Opacity = 0.25;
                    createChampButt.Text = "List is Full";
                    input.Text = "";
                }
                else
                {
                    createChampButt.Opacity = 1;
                    createChampButt.Text = "Create Champion";
                    input.Text = "";
                }
            }
            // Set label text to champion names
            championOne.Text = championsList.championArray[0].name + "\rLVL - " + championsList.championArray[0].level.ToString();
            championTwo.Text = championsList.championArray[1].name + "\rLVL - " + championsList.championArray[1].level.ToString();
            championThree.Text = championsList.championArray[2].name + "\rLVL - " + championsList.championArray[2].level.ToString();
            championFour.Text = championsList.championArray[3].name + "\rLVL - " + championsList.championArray[3].level.ToString();
            championFive.Text = championsList.championArray[4].name + "\rLVL - " + championsList.championArray[4].level.ToString();

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
