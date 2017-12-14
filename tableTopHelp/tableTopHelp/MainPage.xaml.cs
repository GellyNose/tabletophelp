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
        //Champion List Object: array of "Champion" objects
        public ChampionList championsList = new ChampionList();
        public Boolean createIsSelected = false,
                        deleteIsSelected = false;
        // Example champion
        public App.Champion kelgen = new App.Champion("Kelgen");
        public App.Champion brumal = new App.Champion("Brumal");
        public App.Champion winter = new App.Champion("Winter Star");
        public App.Champion mufasa = new App.Champion("Mufasa");
        public App.Champion magda = new App.Champion("Magda");
        public Label[] championLabels = new Label[5];
        public static int MAX_CHAMPION_COUNT = 5;
        public static double OPACITY_LOW_VALUE = 0.25;
        public static double OPACITY_HIGH_VALUE = 1.0;
        
        
        public MainPage()
        {       
                       
            InitializeComponent();    


           
            // Set BindingContext for this page to access buttons and labels from XAML
            BindingContext = this;
            
           
            // Actions for "Create" button clicked
            if (championsList.numChampions == MAX_CHAMPION_COUNT)
            {
                // grey-out chracter create button
                createChampButt.Opacity = OPACITY_LOW_VALUE;
                createChampButt.Text = "List is Full";
            }//end if
            
            
             // create character actions
            createChampButt.Clicked += OnButtonCreateClicked;
             // create mode, check select buttons               
           

          


            // Actions for "Delete" button clicked
            deleteChampButt.Clicked += OnButtonDeleteClicked;
            championLabels[0] = championOne;
            championLabels[1] = championTwo;
            championLabels[2] = championThree;
            championLabels[3] = championFour;
            championLabels[4] = championFive;

            for(int i = 0; i< championLabels.Length; i++)
            {
                championLabels[i].Text = championsList.championArray[i].name + "\rLVL - " + championsList.championArray[i].level.ToString();
            }
            // Set label text to champion names
            championOne = championLabels[0];
            championTwo = championLabels[1];
            championThree = championLabels[2];
            championFour = championLabels[3];
            championFive = championLabels[4];

            
            // Actions for "Select" button clicks, 1-5
            selectChampOne.Clicked += OnButtonOneClicked;
            selectChampTwo.Clicked += OnButtonTwoClicked;




        } // end MainPage

        //Create
        async void OnButtonCreateClicked(object sender, EventArgs e)
        {


            if (championsList.numChampions >= MAX_CHAMPION_COUNT)
            {
                await DisplayAlert("Error", "You've reached the maximum number of characters. Please delete a character to continue.", "OK");
            }//end if


            if (!input.Text.Equals("") && input.Text.Length <= 25)
            {
                championsList.addChampion(input.Text);
                input.Text = "";
            }  // End if (!input.Text.Equals(""))  
            else if (input.Text.Equals(""))
            {
                await DisplayAlert("Error", "You must type a new champion name.", "OK");
            }// End else if(input.Text.Equals(""))
            else if (input.Text.Length >= 25)
            {
                await DisplayAlert("Error", "Your champion name must be 25 characters or less. You are " + (input.Text.Length - 25) + " characters over.", "OK");
            }// End else if(input.Text.Equals(""))


            if (championsList.numChampions >= MAX_CHAMPION_COUNT)
            {
                createChampButt.Opacity = OPACITY_LOW_VALUE;
                createChampButt.Text = "List is Full";
            }
            else
            {
                createChampButt.Opacity = OPACITY_HIGH_VALUE;
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
                    // "if champion isnt found, AND text at this index equals input, AND input isn't basic constructor name"
                    if (!deleteChampionFound && input.Text.Equals(championsList.championArray[i].name) &&
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
                    await DisplayAlert("Error", "You must type a champion name to be deleted.", "Ok.");
                    // escape for loop
                    i = championsList.length + 1;
                    input.Text = "";
                }
            }
            // "if champion is not found, AND text does not equal blank:
            if (!deleteChampionFound && !input.Text.Equals(""))
            {
                await DisplayAlert("Error", "There is no champion named " + input.Text + ".", "OK");
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
                createChampButt.Opacity = OPACITY_HIGH_VALUE;
                input.Text = "";
            }
            else
            {
                if (championsList.numChampions >= MAX_CHAMPION_COUNT)
                {
                    createChampButt.Opacity = OPACITY_LOW_VALUE;
                    createChampButt.Text = "List is Full";
                    input.Text = "";
                }
                else
                {
                    createChampButt.Opacity = OPACITY_HIGH_VALUE;
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
            if (!championsList.championArray[0].name.Equals("Create New Character"))
            {
                // STUB - load champion
                CharacterPage charSheet = new CharacterPage(championsList.championArray[0]);
                await Navigation.PushAsync(charSheet);
                // MainPage = new tableTopHelp.MainPage();               
                championsList.isSelected[0] = true;
                image1.Source = championsList.championArray[0].profileImage;
            }
        }

        async void OnButtonTwoClicked(object sender, EventArgs e)
        {
            // Check for create
            if (!championsList.championArray[1].name.Equals("Create New Character"))
            {
                // STUB - load champion
                CharacterPage charSheet = new CharacterPage(championsList.championArray[1]);
                await Navigation.PushAsync(charSheet);
                // MainPage = new tableTopHelp.MainPage();               
                championsList.isSelected[1] = true;
                image2.Source = championsList.championArray[1].profileImage;
            }
        }
        async void OnButtonThreeClicked(object sender, EventArgs e)
        {
            // Check for create
            if (!championsList.championArray[2].name.Equals("Create New Character"))
            {
                // STUB - load champion
                CharacterPage charSheet = new CharacterPage(championsList.championArray[2]);
                await Navigation.PushAsync(charSheet);
                // MainPage = new tableTopHelp.MainPage();               
                championsList.isSelected[2] = true;
                image3.Source = championsList.championArray[2].profileImage;
            }
        }

        async void OnButtonFourClicked(object sender, EventArgs e)
        {
            // Check for create
            if (!championsList.championArray[3].name.Equals("Create New Character"))
            {
                // STUB - load champion
                CharacterPage charSheet = new CharacterPage(championsList.championArray[3]);
                await Navigation.PushAsync(charSheet);
                // MainPage = new tableTopHelp.MainPage();               
                championsList.isSelected[3] = true;
                image4.Source = championsList.championArray[3].profileImage;
            }
        }

        async void OnButtonFiveClicked(object sender, EventArgs e)
        {
            // Check for create
            if (!championsList.championArray[4].name.Equals("Create New Character"))
            {
                // STUB - load champion
                CharacterPage charSheet = new CharacterPage(championsList.championArray[4]);
                await Navigation.PushAsync(charSheet);
                // MainPage = new tableTopHelp.MainPage();               
                championsList.isSelected[4] = true;
                image5.Source = championsList.championArray[4].profileImage;
            }
        }



        public class ChampionList
        {
            public App.Champion[] championArray;
            // Temporary Champion for sorting
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

        
  



    }

    
}
