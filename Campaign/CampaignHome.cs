using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PongGame
{
    public partial class CampaignHome : Form
    {
        private const int SCREEN_WIDTH = 1000;
        private const int SCREEN_HEIGHT = 600;
        private bool unlockedTwo = false;
        private bool unlockedThree = false;
        private bool unlockedFour = false;
        private bool unlockedFive = false;
        private string line;

        public CampaignHome()
        {
            try
            {
                StreamReader sr = new StreamReader("Unlocked.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    if (line == "1")
                    {
                        unlockedTwo = true;
                    }
                    if (line == "2")
                    {
                        unlockedThree = true;
                    }
                    if (line == "3")
                    {
                        unlockedFour = true;
                    }
                    if (line == "4")
                    {
                        unlockedFive = true;
                    }
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Campaign_KeyDown);

            Label campaign = new Label();
            campaign.Text = "Select a Level";
            campaign.ForeColor = Color.White;
            campaign.BackColor = Color.Black;
            campaign.Font = new Font(campaign.Font.FontFamily, 48);
            campaign.Size = new Size(SCREEN_WIDTH, 100);
            campaign.TextAlign = ContentAlignment.MiddleCenter;
            campaign.BorderStyle = BorderStyle.None;
            this.Controls.Add(campaign);

            Label controls = new Label();
            controls.Text = "up = W\ndown = S";
            controls.ForeColor = Color.Yellow;
            controls.Font = new Font(controls.Font.FontFamily, 18);
            controls.Size = new Size(150, 75);
            controls.Location = new Point(SCREEN_WIDTH / 2 - 50, 500);
            controls.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(controls);

            Button levelOne = new Button();
            levelOne.Text = "1";
            levelOne.ForeColor = Color.Black;
            levelOne.BackColor = Color.Green;
            levelOne.Font = new Font(levelOne.Font.FontFamily, 24);
            levelOne.Size = new Size(75, 75);
            levelOne.Location = new Point(100, 150);
            levelOne.TextAlign = ContentAlignment.MiddleCenter;
            levelOne.Click += new EventHandler(OnButtonClickedLevelOne);
            this.Controls.Add(levelOne);

            Button levelTwo = new Button();
            levelTwo.Text = "2";
            levelTwo.ForeColor = Color.Black;
            if(unlockedTwo == true)
            {
                levelTwo.BackColor = Color.Green;
                levelTwo.Click += new EventHandler(OnButtonClickedLevelTwo);
            }
            else
            {
                levelTwo.BackColor = Color.Red;
            }
            levelTwo.Font = new Font(levelTwo.Font.FontFamily, 24);
            levelTwo.Size = new Size(75, 75);
            levelTwo.Location = new Point(200, 150);
            levelTwo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(levelTwo);

            Button levelThree = new Button();
            levelThree.Text = "3";
            levelThree.ForeColor = Color.Black;
            if(unlockedThree == true)
            {
                levelThree.BackColor = Color.Green;
                levelThree.Click += new EventHandler(OnButtonClickedLevelThree);
            }
            else
            {
                levelThree.BackColor = Color.Red;
            }
            levelThree.Font = new Font(levelThree.Font.FontFamily, 24);
            levelThree.Size = new Size(75, 75);
            levelThree.Location = new Point(300, 150);
            levelThree.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(levelThree);

            Button levelFour = new Button();
            levelFour.Text = "4";
            levelFour.ForeColor = Color.Black;
            if(unlockedFour == true)
            {
                levelFour.BackColor = Color.Green;
                levelFour.Click += new EventHandler(OnButtonClickedLevelFour);
            }
            else
            {
                levelFour.BackColor = Color.Red;
            }
            levelFour.Font = new Font(levelThree.Font.FontFamily, 24);
            levelFour.Size = new Size(75, 75);
            levelFour.Location = new Point(400, 150);
            levelFour.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(levelFour);

            Button levelFive = new Button();
            levelFive.Text = "5";
            levelFive.ForeColor = Color.Black;
            if(unlockedFive == true)
            {
                levelFive.BackColor = Color.Green;
                levelFive.Click += new EventHandler(OnButtonClickedLevelFive);
            }
            else
            {
                levelFive.BackColor = Color.Red;
            }
            levelFive.Font = new Font(levelThree.Font.FontFamily, 24);
            levelFive.Size = new Size(75, 75);
            levelFive.Location = new Point(500, 150);
            levelFive.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(levelFive);
        }

        private void OnButtonClickedLevelOne(object sender, EventArgs e)
        {
            LevelOne playL1 = new LevelOne();
            playL1.Show();
            this.Close();
        }

        private void OnButtonClickedLevelTwo(object sender, EventArgs e)
        {
            LevelTwo playL2 = new LevelTwo();
            playL2.Show();
            this.Close();
        }

        private void OnButtonClickedLevelThree(object sender, EventArgs e)
        {
            LevelThree playL3 = new LevelThree();
            playL3.Show();
            this.Close();
        }

        private void OnButtonClickedLevelFour(Object sender, EventArgs e)
        {
            LevelFour playL4 = new LevelFour();
            playL4.Show();
            this.Close();
        }

        private void OnButtonClickedLevelFive(Object sender, EventArgs e)
        {
            LevelFive playL5 = new LevelFive();
            playL5.Show();
            this.Close();
        }

        private void Campaign_KeyDown(Object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                FormHome gohome = new FormHome();
                gohome.Show();
                this.Close();
            }
        }
    }
}
