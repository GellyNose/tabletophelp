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
        public CharacterPage(App.Champion champion)
        {
            InitializeComponent();
            
            this.Title = champion.name + ":" + " " + champion.race + " " + champion.guardian + " Lvl " + champion.level;
            userInput.Text = "";
            image.Source = champion.profileImage;
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.NumberOfTapsRequired = 2;
            tapGestureRecognizer.Tapped += (s, e) => {
                DisplayAlert("Change Profile Picture", "Profile Picture Changed!", "OK");
                if (!userInput.Text.Equals(""))
                {
                    champion.profileImage = userInput.Text;
                    image.Source = champion.profileImage;
                    
                }
                else
                {
                    champion.profileImage = "noavatar.png";
                    image.Source = champion.profileImage;
                    
                }
                

                
                
            };
            image.GestureRecognizers.Add(tapGestureRecognizer);
        }

        public CharacterPage()
        {
            InitializeComponent();

            

            var imageTap = new TapGestureRecognizer();
            imageTap.NumberOfTapsRequired = 2;
            imageTap.Tapped += (s, e) => {
                DisplayAlert("Tapped", "Tapped Image", "OK");
                image.Source = userInput.Text;
            };
            image.GestureRecognizers.Add(imageTap);
        }

        

    }
    
}