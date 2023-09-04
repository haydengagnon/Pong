using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongGame
{
    public partial class Form1 : Form
    {
        private int ballX = PongProperties.SCREEN_WIDTH / 2;
        private int ballY = PongProperties.SCREEN_HEIGHT / 2;
        private int ballXVelocity = -5;
        private int ballYVelocity = 0;
        private int p1PaddleVelocity = 0;
        private int p2PaddleVelocity = 0;
        private int player1PaddleY = PongProperties.SCREEN_HEIGHT / 2 - PongProperties.PADDLE_HEIGHT / 2;
        private int player2PaddleY = PongProperties.SCREEN_HEIGHT / 2 - PongProperties.PADDLE_HEIGHT / 2;
        private int player1Score = 0;
        private int player2Score = 0;


        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            this.ClientSize = new Size(PongProperties.SCREEN_WIDTH, PongProperties.SCREEN_HEIGHT);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 10;
            gameTimer.Tick += new EventHandler(GameTimer_Tick);
            gameTimer.Start();
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Draw paddles
            e.Graphics.FillRectangle(Brushes.Green, 0, player1PaddleY, PongProperties.PADDLE_WIDTH, PongProperties.PADDLE_HEIGHT);
            e.Graphics.FillRectangle(Brushes.Red, PongProperties.SCREEN_WIDTH - PongProperties.PADDLE_WIDTH, player2PaddleY, PongProperties.PADDLE_WIDTH, PongProperties.PADDLE_HEIGHT);

            // Draw ball
            e.Graphics.FillEllipse(Brushes.White, ballX - PongProperties.BALL_RADIUS, ballY - PongProperties.BALL_RADIUS, PongProperties.BALL_RADIUS * 2, PongProperties.BALL_RADIUS * 2);

            // Draw scores
            e.Graphics.DrawString(player1Score.ToString(), Font, Brushes.White, PongProperties.SCREEN_WIDTH / 2 - 50, 10);
            e.Graphics.DrawString(player2Score.ToString(), Font, Brushes.White, PongProperties.SCREEN_WIDTH / 2 + 30, 10);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                FormHome gohome = new FormHome();
                gohome.Show();
                this.Close();
            }

            // Move player paddles
            if (e.KeyCode == Keys.W && player1PaddleY > 0)
            {
                p1PaddleVelocity -= PongProperties.PADDLE_MOVE_AMOUNT;
            }
            else if (e.KeyCode == Keys.S && player1PaddleY < PongProperties.SCREEN_HEIGHT - PongProperties.PADDLE_HEIGHT)
            {
                p1PaddleVelocity += PongProperties.PADDLE_MOVE_AMOUNT;
            }
            else if (e.KeyCode == Keys.Up && player2PaddleY > 0)
            {
                p2PaddleVelocity -= PongProperties.PADDLE_MOVE_AMOUNT;
            }
            else if (e.KeyCode == Keys.Down && player2PaddleY < PongProperties.SCREEN_HEIGHT - PongProperties.PADDLE_HEIGHT)
            {
                p2PaddleVelocity += PongProperties.PADDLE_MOVE_AMOUNT;
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // Update ball position
            ballX += ballXVelocity;
            ballY += ballYVelocity;

            // Update Paddle Position
            player1PaddleY += p1PaddleVelocity;
            player2PaddleY += p2PaddleVelocity;
            if (p1PaddleVelocity > 0)
            {
                p1PaddleVelocity--;
            }
            else if (p1PaddleVelocity < 0)
            {
                p1PaddleVelocity++;
            }

            if (p2PaddleVelocity > 0)
            {
                p2PaddleVelocity--;
            }
            else if (p2PaddleVelocity < 0)
            {
                p2PaddleVelocity++;
            }

            // Check if paddle hits top or bottom of screen
            if(player1PaddleY <= 0 || player1PaddleY + PongProperties.PADDLE_HEIGHT >= PongProperties.SCREEN_HEIGHT)
            {
                p1PaddleVelocity = -p1PaddleVelocity;
            }

            if(player2PaddleY <= 0 || player2PaddleY + PongProperties.PADDLE_HEIGHT >= PongProperties.SCREEN_HEIGHT)
            {
                p2PaddleVelocity = -p2PaddleVelocity;
            }

            // Check if ball hits top or bottom of screen
            if (ballY - PongProperties.BALL_RADIUS <= 0 || ballY + PongProperties.BALL_RADIUS >= PongProperties.SCREEN_HEIGHT)
            {
                ballYVelocity = -ballYVelocity;
            }

            // Check if ball hits player paddles
            if (ballX - PongProperties.BALL_RADIUS <= PongProperties.PADDLE_WIDTH && ballY >= player1PaddleY && ballY <= player1PaddleY + PongProperties.PADDLE_HEIGHT)
            {
                ballXVelocity = -ballXVelocity;
                if (ballXVelocity < 10)
                {
                    ballXVelocity++;
                }
                if (ballY <= player1PaddleY + PongProperties.PADDLE_HEIGHT && ballY > player1PaddleY + PongProperties.PADDLE_HEIGHT / 2) 
                {
                    ballYVelocity++;
                }
                else if (ballY >= player1PaddleY && ballY < player1PaddleY + PongProperties.PADDLE_HEIGHT /2)
                {
                    ballYVelocity--;
                }
            }
            else if (ballX + PongProperties.BALL_RADIUS >= PongProperties.SCREEN_WIDTH - PongProperties.PADDLE_WIDTH && ballY >= player2PaddleY && ballY <= player2PaddleY + PongProperties.PADDLE_HEIGHT)
            {
                ballXVelocity = -ballXVelocity;
                if (ballXVelocity > -10)
                {
                    ballXVelocity--;
                }
                if (ballY <= player2PaddleY + PongProperties.PADDLE_HEIGHT && ballY > player2PaddleY + PongProperties.PADDLE_HEIGHT / 2) 
                {
                    ballYVelocity++;
                }
                else if (ballY >= player2PaddleY && ballY < player2PaddleY + PongProperties.PADDLE_HEIGHT /2)
                {
                    ballYVelocity--;
                }
            }

            // Check if ball goes out of bounds
            if (ballX - PongProperties.BALL_RADIUS <= 0)
            {
                player2Score++;
                ResetBall();
            }
            else if (ballX + PongProperties.BALL_RADIUS >= PongProperties.SCREEN_WIDTH)
            {
                player1Score++;
                ResetBall();
            }

            // Redraw form
            this.Invalidate();
            if (player1Score == 5)
            {
                Player1Win winner1 = new Player1Win();
                winner1.Show();
                player1Score++;
                this.Close();
            }
            if (player2Score == 5)
            {
                Player2Win winner2 = new Player2Win();
                winner2.Show();
                player2Score++;
                this.Close();
            }
        }

        private void ResetBall()
        {
            ballX = PongProperties.SCREEN_WIDTH / 2;
            ballY = PongProperties.SCREEN_HEIGHT / 2;
            ballXVelocity = -5;
            ballYVelocity = 0;
            player1PaddleY = PongProperties.SCREEN_HEIGHT / 2 - PongProperties.PADDLE_HEIGHT / 2;
            player2PaddleY = PongProperties.SCREEN_HEIGHT / 2 - PongProperties.PADDLE_HEIGHT / 2;
        }
    }
}