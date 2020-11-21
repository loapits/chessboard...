using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;


namespace Chessboard
{
    public partial class Form1 : Form
    {
        readonly Graphics Graph;
        readonly private NpgsqlConnection connect;
        /*private string LETTERS = "abcdefgh";
        private string DIGITS = "12345678";*/
        private string LETTERS;
        private string DIGITS;
        private string sql;

        public Form1()
        {
            connect = new NpgsqlConnection("Server=127.0.0.1;Port=5432;" +
                      "User Id=postgres;Password=postgres;Database=postgres;");

            InitializeComponent();
            Graph = CreateGraphics();
            GetChessCells();
        }

        private void GetChessCells()
        {
            connect.Open();
            try
            {
                sql = "SELECT letters, digits FROM chessCells WHERE id = 1";
                NpgsqlCommand comand = new NpgsqlCommand(sql, connect);
                NpgsqlDataReader result = comand.ExecuteReader();

                while (result.Read())
                {
                    LETTERS = result[0].ToString();
                    DIGITS = result[1].ToString();
                }
                connect.Close();
            }
            catch (Exception e)
            {
                connect.Close();
                MessageBox.Show("Error: " + e.Message);
            }
        }

        // Inserting selected points and the result into the database
        private async void InsertPoints(string point1, string point2, string result)
        {
            try
            {
                connect.Open();

                sql = "INSERT INTO points (point1, point2, result) VALUES (@point1, @point2, @result)";
                NpgsqlCommand comand = new NpgsqlCommand(sql, connect);

                // Parameters of the request
                comand.Parameters.AddWithValue("point1", point1);
                comand.Parameters.AddWithValue("point2", point2);
                comand.Parameters.AddWithValue("result", result);

                await comand.ExecuteNonQueryAsync();
                connect.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
        }

        // Checking of the point
        bool Check(int mx, int my, int nx, int ny)
        {
            if (mx < 0 || mx > 7) return false;
            if (my < 0 || my > 7) return false;
            if (nx < 0 || nx > 7) return false;
            if (ny < 0 || ny > 7) return false;
            return (Math.Abs(mx - nx) == 2 && Math.Abs(my - ny) == 1) || 
                (Math.Abs(mx - nx) == 1 && Math.Abs(my - ny) == 2);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                int nx, ny, mx, my;
                string str1 = textBox1.Text;
                string str2 = textBox2.Text;
                int[] dx = { -2, -2, -1, -1, 1, 1, 2, 2 };
                int[] dy = { -1, 1, -2, 2, -2, 2, -1, 1 };

                nx = str1[0] - 'a';
                ny = str1[1] - '1';
                mx = str2[0] - 'a';
                my = str2[1] - '1';

                if (Check(mx, my, nx, ny))
                {
                    textBox3.Text = "1";
                }
                else
                {
                    textBox3.Text = "NO";
                }

                for (int i = 0; i < 8; ++i)
                {
                    if (Check(mx + dx[i], my + dy[i], nx, ny))
                    {
                        textBox3.Text = "2";
                    }
                }

                InsertPoints(textBox1.Text, textBox2.Text, textBox3.Text);
                textBox1.Text = "";
                textBox2.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Выберите точки");
            }
        }

        // Drawing the chessboard
        private void DrawGrid(object sender, PaintEventArgs e)
        {
            int i = 0;
            // Size of the cell
            int side = 35;

            for (int k = 0; k < 64; k++)
            {
                // Color of the cell
                Brush color = (k / 8 + k % 8) % 2 != 0 ? Brushes.White : Brushes.Black;

                // Drawing board
                e.Graphics.FillRectangle(color, (k / 8) * side, (k % 8) * side, side, side);

                // Drawing border
                Rectangle rect = new Rectangle(0, 0, side * 8, side * 8);
                e.Graphics.DrawRectangle(new Pen(Color.Black), rect);
            }
                
            // Location letters and digits
            for (int n = 35; n < side * 8 + side; n += side)
            {
                Graph.DrawString(LETTERS[i].ToString(), new Font("Arial", 12), new SolidBrush(Color.Black), n, 5);
                Graph.DrawString(DIGITS[i].ToString(), new Font("Arial", 12), new SolidBrush(Color.Black), 5, n);
                i++;
            }
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Text = LocationToCell(e.Location).ToString();
        }

        // Selecting the cell and add in the text box
        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = LocationToCell(e.Location).ToString();
            }
            else
            {
                textBox2.Text = LocationToCell(e.Location).ToString();
            }
        }

        // Сonverting of the point to coordinates the cell
        string LocationToCell(Point location)
        {
            // A dictionary that contains coordinates where axis X
            Dictionary<int, string> letterList = new Dictionary<int, string> 
            { 
                [1] = "a", [2] = "b", [3] = "c", [4] = "d", [5] = "e", [6] = "f", [7] = "g", [8] = "h" 
            };
            // Size of the cell where horizontal
            int w = 35;
            // Size of the cell where vertical
            int h = 35;
            // Coordinate where axis X, getting from the dictionary
            string xLet = letterList[1 + location.X / w];

            // Coordinate where axis Y
            int yLet = 1 + location.Y / h;

            // The returning string then is a point on the chessboard
            return xLet + yLet;
        }
    }
}
