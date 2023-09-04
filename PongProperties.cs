using System;

namespace PongGame {

    public class PongProperties {

        public const int SCREEN_WIDTH = 1000;
        public const int SCREEN_HEIGHT = 600;
        public const int BALL_RADIUS = 10;
        public const int PADDLE_WIDTH = 20;
        public const int PADDLE_HEIGHT = 80;
        public const int PADDLE_MOVE_AMOUNT = 8;
        public int ballX = SCREEN_WIDTH / 2;
        public int ballY = SCREEN_HEIGHT / 2;
        public int ballXVelocity = -5;
        public int ballYVelocity = 0;
        public int p1PaddleVelocity = 0;
        public int p2PaddleVelocity = 0;
        public int player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        public int player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        public int player1Score = 0;
        public int player2Score = 0;
        
        public void drawBoard() {

        }

        public void reset() {
            ballX = SCREEN_WIDTH / 2;
            ballY = SCREEN_HEIGHT / 2;
            ballXVelocity = -5;
            ballYVelocity = 0;
            player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
            player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        }
    }
}