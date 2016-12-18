using DeskMate.Properties;
using Recipe_form.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recipe_form
{
    public partial class Recipie : Form
    {
        public int generated_meal = -1;
        public int generated_desert = -1;
        public int food_type = 1; //1 = meal, 2 = desert
        public struct meal
        {
            public string name, link;
            public Image img;
        }
        meal[] meals = new meal[9];
        //public int meals_len = 6;
        public int meals_len = -1;

        public struct desert
        {
            public string name, link;
            public Image img;
        }
        desert[] deserts = new desert[9];
        public int deserts_len = 5;
        void init()
        {
            get_desert.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);

            view_past_recipes.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            view_past_recipes.FlatAppearance.MouseOverBackColor = Color.Transparent;
            view_past_recipes.FlatAppearance.MouseDownBackColor = Color.Transparent;

            GraphicsPath gp = new GraphicsPath();
            //gp.AddEllipse(new Rectangle(0, 0, 160, 160));
            gp.AddEllipse(food_picture.DisplayRectangle);
            food_picture.Region = new Region(gp);

            ///MEALS
            meals[0].name = "Hot Spinach Cheesecake";
            meals[0].img = Resources.cheesecake;
            meals[0].link = "http://www.food.com/recipe/hot-spinach-cheesecake-54065";

            meals[1].name = "Fritatta";
            meals[1].img = Resources.frittata;
            meals[1].link = "http://www.foodnetwork.com/recipes/alton-brown/frittata-recipe.html5";

            meals[2].name = "Chicken Quesadilla";
            meals[2].img = Resources.quesadilla;
            meals[2].link = "http://allrecipes.com/recipe/21659/chicken-quesadillas/";

            meals[3].name = "Halloumi aubergine burgers";
            meals[3].img = Resources.burgers;
            meals[3].link = "http://www.bbcgoodfood.com/recipes/2196638/halloumi-aubergine-burgers-with-harissa-relish";

            meals[4].name = "Chicken & chorizo paella";
            meals[4].img = Resources.paella;
            meals[4].link = "http://www.jamieoliver.com/recipes/rice-recipes/chicken-chorizo-paella/";

            meals[5].name = "Cajun Seafood Pasta";
            meals[5].img = Resources.cajun;
            meals[5].link = "http://www.jamieoliver.com/recipes/rice-recipes/chicken-chorizo-paella/";
            ///MEALS

            ///DESERTS
            deserts[0].name = "Mille feuilles";
            deserts[0].img = Resources.feuilles;
            deserts[0].link = "http://www.marmiton.org/recettes/recette_mille-feuilles_33004.aspx";

            deserts[1].name = "Cherry clafoutis";
            deserts[1].img = Resources.clafoutis;
            deserts[1].link = "http://www.jamieoliver.com/recipes/fruit-recipes/cherry-clafoutis/";

            deserts[2].name = "Chocolate Lava Cakes";
            deserts[2].img = Resources.lavacake;
            deserts[2].link = "http://www.foodnetwork.com/recipes/chocolate-lava-cakes.html";

            deserts[3].name = "Tarte Tatin";
            deserts[3].img = Resources.tatin;
            deserts[3].link = "http://www.bbcgoodfood.com/recipes/tarte-tatin";

            deserts[4].name = "Southern Red Velvet Cake";
            deserts[4].img = Resources.redvelvet;
            deserts[4].link = "http://www.foodnetwork.com/recipes/southern-red-velvet-cake-recipe.html";
            ///DESERTS
        }
        public Recipie()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            init();
            if (Settings.Default.first_run == true)
            {
                food_type = 1;
                Random r = new Random();
                int rInt = r.Next(0, meals_len);
                generated_meal = rInt;
                food_picture.Image = meals[rInt].img;
                food_text.Text = meals[rInt].name;
                Settings.Default.first_run = false;
                Settings.Default.rInt = rInt;
                Settings.Default.Save();
            }
            else
            {
                generated_meal = Settings.Default.rInt;
                food_picture.Image = meals[Settings.Default.rInt].img;
                food_text.Text = meals[Settings.Default.rInt].name;
            }

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("../../Champagne & Limousines.ttf");
            food_text.Font = new Font(pfc.Families[0], 16, FontStyle.Regular);
        }

        private void get_desert_Click(object sender, EventArgs e)
        {
            food_type = 2;
            Random r = new Random();
            int rInt = r.Next(0, deserts_len);
            generated_desert = rInt;
            food_picture.Image = deserts[rInt].img;
            food_text.Text = deserts[rInt].name;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (food_type == 1)
            {
                for (int i = generated_meal; i < meals_len; i++)
                    meals[i] = meals[i + 1];
                meals_len--;

                //try except
                Random r = new Random();
                int rInt = r.Next(0, meals_len);
                generated_meal = Settings.Default.rInt;
                food_picture.Image = meals[Settings.Default.rInt].img;
                food_text.Text = meals[Settings.Default.rInt].name;
                food_picture.Image = meals[rInt].img;
                food_text.Text = meals[rInt].name;
            }

            else if (food_type == 2)
            {
                for (int i = generated_desert; i < deserts_len; i++)
                    deserts[i] = deserts[i + 1];
                deserts_len--;

                //try except
                Random r = new Random();
                int rInt = r.Next(0, deserts_len);
                generated_desert = rInt;
                food_picture.Image = deserts[rInt].img;
                food_text.Text = deserts[rInt].name;
            }
        }

        private void like_Click(object sender, EventArgs e)
        {
            if (food_type == 1)
            {
                bool OK = true;
                try
                {
                    StreamReader sr = new StreamReader("liked_meals.txt");
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                        if (line == meals[generated_meal].name)
                        {
                            OK = false;
                            break;
                        }
                    sr.Close();
                }

                catch (FileNotFoundException)
                {
                    StreamWriter sw = new StreamWriter("liked_meals.txt");
                    sw.Close();
                }


                if (OK == true)
                {
                    StreamWriter sw = new StreamWriter("liked_meals.txt", true);
                    sw.WriteLine(String.Format("{0}\n", meals[generated_meal].name));
                    sw.Close();
                }

                }

            else
            {
                bool OK = true;
                try
                {
                    StreamReader sr = new StreamReader("liked_meals.txt");
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                        if (line == deserts[generated_desert].name)
                        {
                            OK = false;
                            break;
                        }
                    sr.Close();
                }

                catch (FileNotFoundException)
                {
                    StreamWriter sw = new StreamWriter("liked_meals.txt");
                    sw.Close();
                }


                if (OK == true)
                {
                    StreamWriter sw = new StreamWriter("liked_meals.txt", true);
                    sw.WriteLine(String.Format("{0}\n", deserts[generated_desert].name));
                    sw.Close();
                }
                
            }
            
            
        }

    }
}
