namespace HafızaOyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer2.Tick += timer2_Tick;
            timer3.Tick += timer3_Tick;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DisableAllButtons();
        }
        public int time;
        public int progressBarValue;
        public Button firstButton, secondButton;
        public int player;
        public int player1Point, player2Point;
        private void timer1_Tick(object sender, EventArgs e)
        {
            time--;
            if (time <= 0)
            {
                timer1.Stop();
                label1.Text = "Zaman Doldu";
                progressBar1.Value = 0;
                CloseCards();

            }
            else
            {
                label1.Text = time.ToString();
                progressBar1.Value--;
            }
        }
        private void timer2_Tick(object? sender, EventArgs e)
        {
            timer2.Stop();
            firstButton.ForeColor = Color.White;
            secondButton.ForeColor = Color.White;
            ClearButtons();
            EnableButtonClick();
            switchPlayer();

        }
        private void timer3_Tick(object? sender, EventArgs e)
        {
            timer3.Stop();
            if (secondButton == null)
            {
                firstButton.ForeColor = Color.White;
                ClearButtons();
                switchPlayer();
            }
            
        }
        public void CloseCards()
        {
            for (int i = 1; i <= 40; i++)
            {
                this.Controls["button" + i].ForeColor = Color.White;
            }
        }
        public void ClearButtons()
        {
            firstButton = null;
            secondButton = null;
        }
        public void ShowCards()
        {
            for (int i = 1; i <= 40; i++)
            {
                this.Controls["button" + i].ForeColor = Color.Black;
            }
        }
        public void switchPlayer()
        {
            if (player == 1)
            {
                player = 2;
                label5.BackColor = Color.Green;
                label2.BackColor = Color.White;

            }
            else
            { 
                label2.BackColor = Color.Green;
                label5.BackColor = Color.White;
                player = 1;
            }
        }

        private void EnableAllButtons()
        {
            for (int i = 1; i <= 40; i++)
            {
                var button = this.Controls["button" + i].Enabled = true;
               
            }
        }
        private void DisableAllButtons()
        {
            for (int i = 1; i <= 40; i++)
            {
                var button = this.Controls["button" + i].Enabled = false;

            }
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            EnableAllButtons();
            StartButton.Enabled = false;
            ShowCards();
            List<char> symbolList = new List<char>()
            {
                'a', 'a',
                'b', 'b',
                'c', 'c',
                'd', 'd',
                'e', 'e',
                'f', 'f',
                'g', 'g',
                'h', 'h',
                'i', 'i',
                'j', 'j',
                'k', 'k',
                'l', 'l',
                'm', 'm',
                'n', 'n',
                'o', 'o',
                'p', 'p',
                'q', 'q',
                'r', 'r',
                's', 's',
                't', 't'
            };

            player = 1;
            label2.BackColor = Color.Green;

            player1Point = 0;
            player2Point = 0;
            label3.Text = player1Point.ToString();
            label4.Text = player2Point.ToString();

            Random rnd = new Random();

            for (int i = 1; i <= 40; i++)
            {
                int rndIndex = rnd.Next(symbolList.Count);
                this.Controls["button" + i].Text = symbolList[rndIndex].ToString();
                symbolList.RemoveAt(rndIndex);
            }
            InitializeTimer();
        }
        private void InitializeTimer()
        {
            time = 5;
            label1.Text = time.ToString();
            timer1.Interval = 1000;
            timer1.Tick -= timer1_Tick;
            timer1.Tick += timer1_Tick;
            timer1.Start();
            progressBar1.Maximum = time;
            progressBar1.Minimum = 0;
            progressBar1.Value = time;

        }

        private void DisableButtonClick()
        {
            for (int i = 1; i <= 40; i++)
            {
                var button = this.Controls["button" + i] as Button;
                if (button != null)
                {
                    button.Click -= Button_Click;
                }
            }
        }
        
        private void EnableButtonClick()
        {
            for (int i = 1; i <= 40; i++)
            {
                var button = this.Controls["button" + i] as Button;
                if (button != null)
                {
                    button.Click += Button_Click;
                }
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            
            Button btn = sender as Button;

            if (firstButton == null)
            {
                firstButton = btn;
                firstButton.ForeColor = Color.Black;
                timer3.Start();
                timer3.Interval = 5000;
                return;
            }

            secondButton = btn;
            secondButton.ForeColor = Color.Black;
            DisableButtonClick();
            timer3.Stop();

            if (secondButton.Text == firstButton.Text)
            {
                if(secondButton == firstButton)
                {
                    firstButton.ForeColor = Color.White;
                    secondButton.ForeColor = Color.White;                  
                    ClearButtons();
                    EnableButtonClick();
                }
                else
                {
                    secondButton.ForeColor = Color.Black;
                    firstButton.ForeColor = Color.Black;
                    firstButton.Enabled = false;
                    secondButton.Enabled = false;
                    ClearButtons();
                    EnableButtonClick();
                    if (player == 1)
                    {
                        player1Point++;
                        label3.Text = player1Point.ToString();
                        if (player1Point == 11)
                        {
                            MessageBox.Show("PLAYER-1 WON!!!");
                            StartButton.Text = "PLAY AGAIN";
                            StartButton.Enabled = true;
                            ClearButtons();
                            EnableAllButtons();
                            CloseCards();
                        }
                        else if (player1Point == 10 && player2Point == 10)
                        {
                            MessageBox.Show("Oyun bitti.DOSTLUK KAZANDI!!!");
                            StartButton.Text = "PLAY AGAIN";
                            StartButton.Enabled = true;
                            ClearButtons();
                            EnableAllButtons();
                            CloseCards();
                        }
                        

                    }
                    else
                    {
                        player2Point++;
                        label4.Text = player2Point.ToString();
                        if (player2Point == 11)
                        {
                            MessageBox.Show("PLAYER-2 WON!!!");
                            StartButton.Text = "PLAY AGAIN";
                            StartButton.Enabled = true;
                            ClearButtons();
                            EnableAllButtons();
                            CloseCards();
                            label5.BackColor = Color.White;
                        }
                        else if(player1Point == 10 && player2Point == 10)
                        {
                            MessageBox.Show("Oyun bitti.DOSTLUK KAZANDI!!!");
                            StartButton.Text = "PLAY AGAIN";
                            StartButton.Enabled = true;
                            ClearButtons();
                            EnableAllButtons();
                            CloseCards();
                        }
                        
                    }

                    
                }
            }  
            else
            {
                timer2.Interval = 1000;
                timer2.Start();

            }
        }
    }

}
