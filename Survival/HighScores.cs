using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;

namespace PongGame
{
    public partial class HighScores : Form
    {
        private const int SCREEN_WIDTH = 1000;
        private const int SCREEN_HEIGHT = 600;
        string topScoresQuery = "SELECT score FROM scores ORDER BY score DESC LIMIT 5";
        SQLiteConnection sqlite_conn;

        public HighScores()
        {
            InitializeComponent();
            QueryTopScores(topScoresQuery);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            Label gameCenter = new Label();
            gameCenter.Text = "HIGH SCORES";
            gameCenter.ForeColor = Color.White;
            gameCenter.BackColor = Color.Black;
            gameCenter.Font = new Font(gameCenter.Font.FontFamily, 48);
            gameCenter.Size = new Size(SCREEN_WIDTH, 100);
            gameCenter.TextAlign = ContentAlignment.MiddleCenter;
            gameCenter.BorderStyle = BorderStyle.None;
            this.Controls.Add(gameCenter);

            Label listScores = new Label();
            listScores.Text = Convert.ToString(topScoresQuery);
            listScores.ForeColor = Color.White;
            listScores.BackColor = Color.Black;
            listScores.Font = new Font(gameCenter.Font.FontFamily, 28);
            listScores.Size = new Size(SCREEN_WIDTH / 2, 100);
            listScores.Location = new Point(250, 200);
            listScores.BorderStyle = BorderStyle.None;
            this.Controls.Add(listScores);
        }

        void OrderData(SQLiteConnection conn)
        {
            sqlite_conn = new SQLiteConnection("Data Source=Survival/ScoreList.db");
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sqlite_conn);
            sqlite_cmd.CommandText = "SELECT score FROM scores ORDER BY score DESC LIMIT 5";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }

        void QueryTopScores(string txtQuery)
        {
            sqlite_conn = new SQLiteConnection("Data Source=Survival/ScoreList.db");
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sqlite_conn);
            sqlite_cmd.CommandText = txtQuery;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }

        DataTable ExecuteReadQuery(string query)
        {
            DataTable entries = new DataTable();

            using (SQLiteConnection db = new SQLiteConnection("Data Source=Survival/ScoreList.db"))
            {
                SQLiteCommand selectCommand = new SQLiteCommand(query, db);
                try
                {
                    db.Open();
                    SQLiteDataReader reader = selectCommand.ExecuteReader();

                    if (reader.HasRows)
                        for (int i = 0; i < reader.FieldCount; i++)
                            entries.Columns.Add(new DataColumn(reader.GetName(i)));

                    int j = 0;
                    while (reader.Read())
                    {
                        DataRow row = entries.NewRow();
                        entries.Rows.Add(row);

                        for (int i = 0; i < reader.FieldCount; i++)
                            entries.Rows[j][i] = (reader.GetValue(i));

                        j++;
                    }

                    db.Close();
                }
                catch (SQLiteException e)
                {
                    //OnSQLiteError(new SQLiteErrorEventArgs(e));
                    db.Close();
                }
                return entries;
            }
        }
    }
}