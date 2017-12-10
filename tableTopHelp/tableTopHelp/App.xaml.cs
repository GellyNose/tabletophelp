using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
// Trinidad
namespace tableTopHelp
{
    
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public class Champion
        {
            // Administrative values
            public String name { get; set; }
            public String race { get; set; }
            public int level { get; set; }
            public String role { get; set; }
            public String guardian { get; set; }

            // Health values
            public int maxHP { get; set; }
            public int currentHP { get; set; }
            public double currentHPPercent { get; set; }
            
            public int healthSkillModifier { get; set; }
            public int bonusHPFromModifers { get; set; }
            public int healthModifiersSpent { get; set; }
            public static int BASE_HP { get; }

            // Magicka values
            public int maxMagicka { get; set; }
            public int currentMagicka { get; set; }
            public double currentMagickaPercent { get; set; }
            public int magickaSkillModifier { get; set; }
            public int bonusMagickaFromModifiers { get; set; }
           
            public int magickaModifiersSpent { get; set; }
            public int restMagickaAmount { get; set; }
            public static int BASE_MAGICKA { get; }

            // Stamina values
            public int maxStamina { get; set; }
            public int currentStamina { get; set; }
            public double currentStaminaPercent { get; set; }
            public int staminaSkillModifier { get; set; }
            public int staminaModifiersSpent { get; set; }
            public int bonusStaminaFromModifiers { get; set; }
            public static int WEIGHT_MULTIPLIER { get; }
            public int maxWeight { get; set; }
            public int currentWeight { get; set; }
            public int restStaminaAmount { get; set; }
            public static int MOVE_DISTANCE { get; }
            public int moveModifier { get; set; }
            public int initiativeModifier { get; set; }
            public int initiative { get; set; }
            public static int ACTION_POINTS { get; }
            public int actionPointsModifier { get; set; }
            public static int BASE_STAMINA { get; }
            
            // Defense values
            public int armorSetRating { get; set; }
            public double armorSetProtection { get; set; }
            public int shieldArmorRating { get; set; }
            public double shieldProtection { get; set; }
            public double shieldMagicResistance { get; set; }
            public double magicResistance { get; set; }
            public double electricResistance { get; set; }
            public double fireResistance { get; set; }
            public double frostResistance { get; set; }

            // Total number of attribute modifier points spent between HP, STAM, and MAG
            public int totalModifiersSpent { get; set; }



            // Total number of HP, STAM, and MAG points you can spend
            public int attributeModifiersAvailable { get; set; }
            public static int MAX_LEVEL { get; }
            public static int MAX_MODIFIER { get; }
            public static int ATTRIBUTE_POINTS_PER_LEVEL { get; }

            public int talentsSpent { get; set; }
            public int talentsAvailable { get; set; }
            
            // skill check modifiers
            public int strengthModifier { get; set; }
            public int intelligenceModifier { get; set; }
            public int wisdomModifier { get; set; }
            public int dexterityModifier { get; set; }
            public int charismaModifier { get; set; }

            public Champion()
            {
                name = "Create New Character";
            }// public Champion()

            public Champion(String inputName)
            {
                name = inputName;
                level = 1;
                guardian = "Warrior";
                race = "Nord";

            }

            public void spendHealthPoint()
            {
                // if point is available
                if (attributeModifiersAvailable > 0)
                {
                    // remove that point
                    attributeModifiersAvailable -= 1;
                    // add the point to health modifer spent
                    healthModifiersSpent++;
                    //check that skill modifier is not beyond max, assign skill modifier
                    if (healthSkillModifier < MAX_MODIFIER)
                    {
                        // 1 Point, 0 modifier
                        if (healthModifiersSpent == 1)
                        {
                            healthSkillModifier = 0;
                        }
                        // 2 Points, +1 modifier
                        else if (healthModifiersSpent > 1 && healthModifiersSpent < 4)
                        {
                            healthSkillModifier = 1;
                        }
                        // 4 Points, +2 Modifier
                        else if (healthModifiersSpent > 3 && healthModifiersSpent < 6)
                        {
                            healthSkillModifier = 2;
                        }
                        // 6 Points, +3 Modifier
                        else if (healthModifiersSpent > 5 && healthModifiersSpent < 8)
                        {
                            healthSkillModifier = 3;
                        }
                        // 8 Points, +4 Modifier
                        else if (healthModifiersSpent > 7 && healthModifiersSpent < 10)
                        {
                            healthSkillModifier = 4;
                        }
                        // 10 Points, +5 Modifier
                        else if (healthModifiersSpent > 9 && healthModifiersSpent < 12)
                        {
                            healthSkillModifier = 5;
                        }
                        // 12 Points, +1 Modifier
                        else if (healthModifiersSpent > 11 && healthModifiersSpent < 14)
                        {
                            healthSkillModifier = 6;
                        }
                        // 14 Points, +7 Modifier
                        else if (healthModifiersSpent > 13 && healthModifiersSpent < 16)
                        {
                            healthSkillModifier = 7;
                        }
                        // 16 Points, +8 Modifier
                        else if (healthModifiersSpent > 15 && healthModifiersSpent < 18)
                        {
                            healthSkillModifier = 8;
                        }
                        // 18 Points, +9 Modifier
                        else if (healthModifiersSpent > 17 && healthModifiersSpent < 20)
                        {
                            healthSkillModifier = 9;
                        }
                        // 20 Points, +10 Modifier, MAXED
                        else if (healthModifiersSpent > 19)
                        {
                            healthSkillModifier = 10;
                        }

                    }// if
                    
                    // calculate bonus HP from modifier points
                    bonusHPFromModifers = healthModifiersSpent * ATTRIBUTE_POINTS_PER_LEVEL;
                    // reset maxHP
                    maxHP = BASE_HP + bonusHPFromModifers;
                    currentHP = maxHP;
                    currentHPPercent = 100.00;
                }

            }// public void spendHealthPoint
            public void spendMagickaPoint()
            {
                // if point is available
                if (attributeModifiersAvailable > 0)
                {
                    // remove that point
                    attributeModifiersAvailable -= 1;
                    // add the point to magicka modifer spent
                    magickaModifiersSpent++;
                    //check that skill modifier is not beyond max, assign skill modifier
                    if (magickaSkillModifier < MAX_MODIFIER)
                    {
                        // 1 Point, 0 modifier
                        if (magickaModifiersSpent == 1)
                        {
                            magickaSkillModifier = 0;
                        }
                        // 2 Points, +1 modifier
                        else if (magickaModifiersSpent > 1 && magickaModifiersSpent < 4)
                        {
                            magickaSkillModifier = 1;
                        }
                        // 4 Points, +2 Modifier
                        else if (magickaModifiersSpent > 3 && magickaModifiersSpent < 6)
                        {
                            magickaSkillModifier = 2;
                        }
                        // 6 Points, +3 Modifier
                        else if (magickaModifiersSpent > 5 && magickaModifiersSpent < 8)
                        {
                            magickaSkillModifier = 3;
                        }
                        // 8 Points, +4 Modifier
                        else if (magickaModifiersSpent > 7 && magickaModifiersSpent < 10)
                        {
                            magickaSkillModifier = 4;
                        }
                        // 10 Points, +5 Modifier
                        else if (magickaModifiersSpent > 9 && magickaModifiersSpent < 12)
                        {
                            magickaSkillModifier = 5;
                        }
                        // 12 Points, +1 Modifier
                        else if (magickaModifiersSpent > 11 && magickaModifiersSpent < 14)
                        {
                            magickaSkillModifier = 6;
                        }
                        // 14 Points, +7 Modifier
                        else if (magickaModifiersSpent > 13 && magickaModifiersSpent < 16)
                        {
                            magickaSkillModifier = 7;
                        }
                        // 16 Points, +8 Modifier
                        else if (magickaModifiersSpent > 15 && magickaModifiersSpent < 18)
                        {
                            magickaSkillModifier = 8;
                        }
                        // 18 Points, +9 Modifier
                        else if (magickaModifiersSpent > 17 && magickaModifiersSpent < 20)
                        {
                            magickaSkillModifier = 9;
                        }
                        // 20 Points, +10 Modifier, MAXED
                        else if (magickaModifiersSpent > 19)
                        {
                            magickaSkillModifier = 10;
                        }

                    }// if

                    // calculate bonus Magicka from modifier points
                    bonusMagickaFromModifiers = magickaModifiersSpent * ATTRIBUTE_POINTS_PER_LEVEL;
                    // reset max Magicka and current Magicka
                    maxMagicka = BASE_MAGICKA + bonusMagickaFromModifiers;
                    currentMagicka = maxMagicka;
                    currentMagickaPercent = 100.00;
                }

            }// public void spendMagickaPoint
            public void spendStaminaPoint()
            {
                // if point is available
                if (attributeModifiersAvailable > 0)
                {
                    // remove that point
                    attributeModifiersAvailable -= 1;
                    // add the point to stamina modifer spent
                    staminaModifiersSpent++;
                    //check that skill modifier is not beyond max, assign skill modifier
                    if (staminaSkillModifier < MAX_MODIFIER)
                    {
                        // 1 Point, 0 modifier
                        if (staminaModifiersSpent == 1)
                        {
                            staminaSkillModifier = 0;
                        }
                        // 2 Points, +1 modifier
                        else if (staminaModifiersSpent > 1 && staminaModifiersSpent < 4)
                        {
                            staminaSkillModifier = 1;
                        }
                        // 4 Points, +2 Modifier
                        else if (staminaModifiersSpent > 3 && staminaModifiersSpent < 6)
                        {
                            staminaSkillModifier = 2;
                        }
                        // 6 Points, +3 Modifier
                        else if (staminaModifiersSpent > 5 && staminaModifiersSpent < 8)
                        {
                            staminaSkillModifier = 3;
                        }
                        // 8 Points, +4 Modifier
                        else if (staminaModifiersSpent > 7 && staminaModifiersSpent < 10)
                        {
                            staminaSkillModifier = 4;
                        }
                        // 10 Points, +5 Modifier
                        else if (staminaModifiersSpent > 9 && staminaModifiersSpent < 12)
                        {
                            staminaSkillModifier = 5;
                        }
                        // 12 Points, +1 Modifier
                        else if (staminaModifiersSpent > 11 && staminaModifiersSpent < 14)
                        {
                            staminaSkillModifier = 6;
                        }
                        // 14 Points, +7 Modifier
                        else if (staminaModifiersSpent > 13 && staminaModifiersSpent < 16)
                        {
                            staminaSkillModifier = 7;
                        }
                        // 16 Points, +8 Modifier
                        else if (staminaModifiersSpent > 15 && staminaModifiersSpent < 18)
                        {
                            staminaSkillModifier = 8;
                        }
                        // 18 Points, +9 Modifier
                        else if (staminaModifiersSpent > 17 && staminaModifiersSpent < 20)
                        {
                            staminaSkillModifier = 9;
                        }
                        // 20 Points, +10 Modifier, MAXED
                        else if (staminaModifiersSpent > 19)
                        {
                            staminaSkillModifier = 10;
                        }

                    }// if

                    // calculate bonus Magicka from modifier points
                    bonusStaminaFromModifiers = staminaModifiersSpent * ATTRIBUTE_POINTS_PER_LEVEL;
                    // reset max Magicka and current Magicka
                    maxStamina = BASE_STAMINA + bonusStaminaFromModifiers;
                    currentStamina = maxStamina;
                    currentStaminaPercent = 100.00;
                }

            }// public void spendStaminaPoint

        } // public class Champion

        
    }
}
