using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongGame
{
    public partial class MegaPong : Form
    {
        private const int SCREEN_WIDTH = 900;
        private const int SCREEN_HEIGHT = 900;
        private const int BALL_RADIUS = 10;
        private const int PADDLE_WIDTH = 20;
        private const int PADDLE_HEIGHT = 80;
        private const int PADDLE_MOVE_AMOUNT = 20;
        private int ballX = SCREEN_WIDTH / 2;
        private int ballY = SCREEN_HEIGHT / 2;
        private int ballXVelocity = -5;
        private int ballYVelocity = 0;
        private int player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        private int player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;        
        private int player3PaddleX = SCREEN_WIDTH / 2 - PADDLE_HEIGHT / 2;
        private int player4PaddleX = SCREEN_WIDTH / 2 - PADDLE_HEIGHT / 2;
        private int player1Lives = 4;
        private int player2Lives = 4;
        private int player3Lives = 4;
        private int player4Lives = 4;

        public MegaPong()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.Paint += new PaintEventHandler(MegaPong_Paint);
            this.KeyDown += new KeyEventHandler(MegaPong_KeyDown);
            System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 10;
            gameTimer.Tick += new EventHandler(GameTimer_Tick);
            gameTimer.Start();
        }

        private void MegaPong_Paint(object sender, PaintEventArgs e)
        {
            // Draw paddles
            e.Graphics.FillRectangle(Brushes.Green, 0, player1PaddleY, PADDLE_WIDTH, PADDLE_HEIGHT);
            e.Graphics.FillRectangle(Brushes.Red, SCREEN_WIDTH - PADDLE_WIDTH, player2PaddleY, PADDLE_WIDTH, PADDLE_HEIGHT);
            e.Graphics.FillRectangle(Brushes.Yellow, player3PaddleX, 0, PADDLE_HEIGHT, PADDLE_WIDTH);
            e.Graphics.FillRectangle(Brushes.Blue, player4PaddleX, SCREEN_HEIGHT - PADDLE_WIDTH, PADDLE_HEIGHT, PADDLE_WIDTH);

            // Draw ball
            e.Graphics.FillEllipse(Brushes.White, ballX - BALL_RADIUS, ballY - BALL_RADIUS, BALL_RADIUS * 2, BALL_RADIUS * 2);

            // Draw scores
            e.Graphics.DrawString(player1Lives.ToString(), Font, Brushes.White, SCREEN_WIDTH / 2 - 50, 10);
            e.Graphics.DrawString(player2Lives.ToString(), Font, Brushes.White, SCREEN_WIDTH / 2 + 30, 10);
            e.Graphics.DrawString(player3Lives.ToString(), Font, Brushes.White, SCREEN_WIDTH / 3 + 110, 10);
            e.Graphics.DrawString(player4Lives.ToString(), Font, Brushes.White, SCREEN_WIDTH / 3 - 130, 10);
        }

        private void MegaPong_KeyDown(object sender, KeyEventArgs e)
        {
            // Move player paddles
            if (e.KeyCode == Keys.W && player1PaddleY > 0)
            {
                player1PaddleY -= PADDLE_MOVE_AMOUNT;
            }
            else if (e.KeyCode == Keys.S && player1PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT)
            {
                player1PaddleY += PADDLE_MOVE_AMOUNT;
            }
            
            if (e.KeyCode == Keys.Escape)
            {
                FormHome gohome = new FormHome();
                gohome.Show();
                this.Close();
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // Update ball position
            ballX += ballXVelocity;
            ballY += ballYVelocity;

            //Computer paddle movement
            if (player2PaddleY > ballY && player2PaddleY > 0)
            {
                player2PaddleY -= 5;
            }
            else if (player2PaddleY < ballY - PADDLE_HEIGHT / 2 && player2PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT)
            {
                player2PaddleY += 5;
            }

            if (player3PaddleX > ballX && player3PaddleX > 0)
            {
                player3PaddleX -= 5;
            }
            else if (player3PaddleX < ballX - PADDLE_HEIGHT / 2 && player3PaddleX < SCREEN_WIDTH - PADDLE_HEIGHT)
            {
                player3PaddleX += 5;
            }

            if (player4PaddleX > ballX && player4PaddleX > 0)
            {
                player4PaddleX -= 5;
            }
            else if (player4PaddleX < ballX - PADDLE_HEIGHT / 2 && player4PaddleX < SCREEN_WIDTH - PADDLE_HEIGHT)
            {
                player4PaddleX += 5;
            }

            // Check if ball hits player paddles
            if (ballX - BALL_RADIUS <= PADDLE_WIDTH && ballY >= player1PaddleY && ballY <= player1PaddleY + PADDLE_HEIGHT)
            {
                ballXVelocity = -ballXVelocity;

                if (ballY < player1PaddleY + PADDLE_HEIGHT && ballY > player1PaddleY + PADDLE_HEIGHT / 2) 
                {
                    ballYVelocity++;
                }
                else if (ballY > player1PaddleY && ballY < player1PaddleY + PADDLE_HEIGHT /2)
                {
                    ballYVelocity--;
                }
            }
            else if (ballX + BALL_RADIUS >= SCREEN_WIDTH - PADDLE_WIDTH && ballY >= player2PaddleY && ballY <= player2PaddleY + PADDLE_HEIGHT)
            {
                ballXVelocity = -ballXVelocity;
                
                if (ballY < player2PaddleY + PADDLE_HEIGHT && ballY > player2PaddleY + PADDLE_HEIGHT / 2) 
                {
                    ballYVelocity++;
                }
                else if (ballY > player2PaddleY && ballY < player2PaddleY + PADDLE_HEIGHT / 2)
                {
                    ballYVelocity--;
                }
            }
            else if (ballY - BALL_RADIUS <= PADDLE_WIDTH && ballX >= player3PaddleX && ballX <= player3PaddleX + PADDLE_HEIGHT)
            {
                ballYVelocity = -ballYVelocity;

                if (ballX < player3PaddleX + PADDLE_HEIGHT && ballX > player3PaddleX + PADDLE_HEIGHT / 2)
                {
                    ballXVelocity++;
                }
                else if (ballX > player3PaddleX && ballX < player3PaddleX + PADDLE_HEIGHT / 2)
                {
                    ballXVelocity--;
                }
            }
            else if (ballY + BALL_RADIUS >= SCREEN_HEIGHT - PADDLE_WIDTH && ballX >= player4PaddleX && ballX <= player4PaddleX + PADDLE_HEIGHT)
            {
                ballYVelocity = -ballYVelocity;

                if (ballX < player4PaddleX + PADDLE_HEIGHT && ballX > player4PaddleX + PADDLE_HEIGHT / 2)
                {
                    ballXVelocity++;
                }
                else if (ballX > player4PaddleX && ballX < player4PaddleX + PADDLE_HEIGHT / 2)
                {
                    ballXVelocity--;
                }
            }


            // Check if ball goes out of bounds
            if (ballX - BALL_RADIUS <= 0)
            {
                player2Lives--;
                ResetBall();
            }
            else if (ballX + BALL_RADIUS >= SCREEN_WIDTH)
            {
                player1Lives--;
                ResetBall();
            }
            else if (ballY + BALL_RADIUS <= 0)
            {
                player3Lives--;
                ResetBall();
            }
            else if (ballY + BALL_RADIUS >= SCREEN_HEIGHT)
            {
                player4Lives--;
                ResetBall();
            }

            // Redraw form
            this.Invalidate();
            if (player1Lives == 5)
            {
                Player1Win winner1 = new Player1Win();
                winner1.Show();
                player1Lives++;
                this.Close();
            }
            if (player2Lives == 5)
            {
                Player2Win winner2 = new Player2Win();
                winner2.Show();
                player2Lives++;
                this.Close();
            }
        }

        private void ResetBall()
        {
            ballX = SCREEN_WIDTH / 2;
            ballY = SCREEN_HEIGHT / 2;
            ballXVelocity = -5;
            ballYVelocity = 0;
            player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
            player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
            player3PaddleX = SCREEN_WIDTH / 2 - PADDLE_HEIGHT / 2;
            player4PaddleX = SCREEN_WIDTH / 2 - PADDLE_HEIGHT / 2; 
        }
    }
}