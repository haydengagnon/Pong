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
        private int player1Lives = 0;
        private int player2Lives = 0;

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
            else if (e.KeyCode == Keys.Up && player2PaddleY > 0)
            {
                player2PaddleY -= PADDLE_MOVE_AMOUNT;
            }
            else if (e.KeyCode == Keys.Down && player2PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT)
            {
                player2PaddleY += PADDLE_MOVE_AMOUNT;
            }
            else if (e.KeyCode == Keys.O && player3PaddleX > 0)
            {
                player3PaddleX -= PADDLE_MOVE_AMOUNT;
            }
            else if (e.KeyCode == Keys.P && player3PaddleX < SCREEN_WIDTH - PADDLE_HEIGHT)
            {
                player3PaddleX += PADDLE_MOVE_AMOUNT;
            }
            else if (e.KeyCode == Keys.N && player4PaddleX > 0)
            {
                player4PaddleX -= PADDLE_MOVE_AMOUNT;
            }
            else if (e.KeyCode == Keys.M && player4PaddleX < SCREEN_WIDTH - PADDLE_HEIGHT)
            {
                player4PaddleX += PADDLE_MOVE_AMOUNT;
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

            // Check if ball hits top or bottom of screen
            if (ballY - BALL_RADIUS <= 0 || ballY + BALL_RADIUS >= SCREEN_HEIGHT)
            {
                ballYVelocity = -ballYVelocity;
            }

            // Check if ball hits player paddles
            if (ballX - BALL_RADIUS <= PADDLE_WIDTH && ballY >= player1PaddleY && ballY <= player1PaddleY + PADDLE_HEIGHT)
            {
                ballXVelocity = -ballXVelocity;
                if (ballXVelocity < 10)
                {
                    ballXVelocity++;
                }
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
                if (ballXVelocity > -10)
                {
                    ballXVelocity--;
                }
                if (ballY < player2PaddleY + PADDLE_HEIGHT && ballY > player2PaddleY + PADDLE_HEIGHT / 2) 
                {
                    ballYVelocity++;
                }
                else if (ballY > player2PaddleY && ballY < player2PaddleY + PADDLE_HEIGHT /2)
                {
                    ballYVelocity--;
                }
            }

            // Check if ball goes out of bounds
            if (ballX - BALL_RADIUS <= 0)
            {
                player2Lives++;
                ResetBall();
            }
            else if (ballX + BALL_RADIUS >= SCREEN_WIDTH)
            {
                player1Lives++;
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