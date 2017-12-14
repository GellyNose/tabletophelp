using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tableTopHelp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterPage : ContentPage
    {

        Random d20 = new Random();
        public int randomD20 = 0;
           

        public CharacterPage(App.Champion champion)
        {
            InitializeComponent();
            // Formatted strings for dual-color text in labels
            var displayAP = new FormattedString();
            var displayMove = new FormattedString();            

            
            // Set Title to Champion details
            this.Title = champion.name + ":" + " " + champion.race + " " + champion.guardian + " Lvl " + champion.level;
            
            // Initialize userInput to avoid null
            userInput.Text = "";
            // set display AP formatted string to desired presentation
            displayAP.Spans.Add(new Span { Text = "AP: " });
            displayAP.Spans.Add(new Span { Text = (champion.actionPointsModifier + champion.ACTION_POINTS).ToString(),
                                           FontAttributes = FontAttributes.Bold, ForegroundColor = Color.DarkGreen });
            aPlabel.FormattedText = displayAP;
            // set display move formatted string to desired presentation
            displayMove.Spans.Add(new Span { Text = "Move: " });
            displayMove.Spans.Add(new Span { Text = (champion.moveModifier + champion.MOVE_DISTANCE).ToString(),
                                           FontAttributes = FontAttributes.Bold, ForegroundColor = Color.DarkGreen });
            moveLabel.FormattedText = displayMove;
            
            

            
            // set image source to champion profile image
            image.Source = champion.profileImage;
            // image double tap listener
            var imageTapListener = new TapGestureRecognizer();
            imageTapListener.NumberOfTapsRequired = 2;
            imageTapListener.Tapped += (s, e) =>
            {
                if (!userInput.Text.Equals(""))
                {
                    DisplayAlert("Change Profile Picture", "Attempting to Change Picture", "OK");
                    image.Source = userInput.Text;
                    champion.profileImage = userInput.Text;
                    
                }
                else
                {
                    DisplayAlert("Error", "Must paste an image URL ending in .jpg, .png, or .gif.", "OK");
                }


            };
            image.GestureRecognizers.Add(imageTapListener);

            // initButton double tap listener and d20 logic
            var initTapListener = new TapGestureRecognizer();
            initTapListener.NumberOfTapsRequired = 2;
            initTapListener.Tapped += (s, e) =>
            {
                randomD20 = d20.Next(20) + 1;
                champion.initiative = randomD20 + champion.initiativeModifier;
                initButton.Text = "Roll Initiative: " + champion.initiative;


            };
            initButton.GestureRecognizers.Add(initTapListener);
           

        }

        

        public CharacterPage()
        {
            InitializeComponent();



           
            
        }

        
        

        

    }
}