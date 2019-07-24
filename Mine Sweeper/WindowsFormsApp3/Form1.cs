using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        int height = 10;
        int width = 10;
        Button[,] board;
        int[,] numboard;
        int[,] flagboard;
        int firstpress = 0;

        int time = 0;
        int minute = 0;

        int Flag = 0;

        int restart = 0;

        Image Bomb = Properties.Resources.MineSweeper_BombDone;
        Image RedBomb = Properties.Resources.MineSweeper_PressedBombDone;
        Image WrongFlag = Properties.Resources.MineSweeper_FalseFlagDone;
        Image flag = Properties.Resources.MineSweeper;
        Image SmileyFace = Properties.Resources.MineSweeper_SmileyFaceDone;
        Image SadFace = Properties.Resources.MineSweeper_SadFaceDone;
        Image NotFlag = Properties.Resources.NotAFlag;

        public Form1()
        {
            InitializeComponent();
        }
        void press(int Yshura, int Xshura)
        {
            if (numboard[Yshura, Xshura] <= 8 && numboard[Yshura, Xshura] >= 1)
            {
                if (flagboard[Yshura, Xshura] == 0)
                {
                    board[Yshura, Xshura].Text = numboard[Yshura, Xshura].ToString();
                    board[Yshura, Xshura].BackColor = Color.FromArgb(190, 190, 190);
                    board[Yshura, Xshura].FlatStyle = FlatStyle.Flat;
                    board[Yshura, Xshura].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);

                    #region color1
                    board[Yshura, Xshura].BackColor = Color.FromArgb(190, 190, 190);
                    if (numboard[Yshura, Xshura] == 1)
                        board[Yshura, Xshura].ForeColor = Color.Blue;
                    else if (numboard[Yshura, Xshura] == 2)
                        board[Yshura, Xshura].ForeColor = Color.Green;
                    else if (numboard[Yshura, Xshura] == 3)
                        board[Yshura, Xshura].ForeColor = Color.Red;
                    else if (numboard[Yshura, Xshura] == 4)
                        board[Yshura, Xshura].ForeColor = Color.DarkBlue;
                    else if (numboard[Yshura, Xshura] == 5)
                        board[Yshura, Xshura].ForeColor = Color.Brown;
                    else if (numboard[Yshura, Xshura] == 6)
                        board[Yshura, Xshura].ForeColor = Color.FromArgb(20, 140, 160);
                    else if (numboard[Yshura, Xshura] == 7)
                        board[Yshura, Xshura].ForeColor = Color.Black;
                    else if (numboard[Yshura, Xshura] == 8)
                        board[Yshura, Xshura].ForeColor = Color.Gray;
                    #endregion

                    numboard[Yshura, Xshura] = -numboard[Yshura, Xshura];
                }
            }
            if (numboard[Yshura, Xshura] == 0)
            {
                if (flagboard[Yshura, Xshura] == 0)
                {
                    board[Yshura, Xshura].BackColor = Color.FromArgb(190, 190, 190);
                    board[Yshura, Xshura].FlatStyle = FlatStyle.Flat;
                    board[Yshura, Xshura].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);

                    numboard[Yshura, Xshura] = -9;

                    #region חלק עם רקורסיה
                    if (Yshura != height - 1 && Xshura != width - 1)
                    {
                        press(Yshura + 1, Xshura + 1);
                    }
                    if (Yshura != 0 && Xshura != 0)
                    {
                        press(Yshura - 1, Xshura - 1);
                    }
                    if (Yshura != height - 1 && Xshura != 0)
                    {
                        press(Yshura + 1, Xshura - 1);
                    }
                    if (Yshura != 0 && Xshura != width - 1)
                    {
                        press(Yshura - 1, Xshura + 1);
                    }
                    if (Yshura != height - 1)
                    {
                        press(Yshura + 1, Xshura);
                    }
                    if (Yshura != 0)
                    {
                        press(Yshura - 1, Xshura);
                    }
                    if (Xshura != 0)
                    {
                        press(Yshura, Xshura - 1);
                    }
                    if (Xshura != width - 1)
                    {
                        press(Yshura, Xshura + 1);
                    }
                    #endregion
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (restart == 1)
            {
                for (int shura = 0; shura < height; shura++)
                {
                    for (int amuda = 0; amuda < width; amuda++)
                    {
                        panel1.Controls.Remove(board[shura, amuda]);
                        board[shura, amuda].Dispose();
                    }
                }
                button1.BackgroundImage = SmileyFace;
                time = 0;
                minute = 0;
                textBox6.Text = " 00 : 00";
                timer1.Stop();
                firstpress = 0;
            }
            else
            {
                restart = 1;
            }
            timer1.Interval = 1000;
            try
            {
                height = int.Parse(textBox1.Text);
            }
            catch { };
            try
            { 
                width = int.Parse(textBox2.Text);
            }
            catch { };
            board = new Button[height, width];
            numboard = new int[height, width];
            flagboard = new int[height, width];
            int btnsize = 30;
            int i = 0;
            for (int shura = 0; shura < height; shura++)
                for (int amuda = 0; amuda < width; amuda++)
                {
                    board[shura, amuda] = new Button();
                    board[shura, amuda].Size = new Size(btnsize, btnsize);
                    board[shura, amuda].Location = new Point(amuda * btnsize, shura * btnsize);
                    board[shura, amuda].Tag = i.ToString();
                    board[shura, amuda].MouseDown += Form1_MouseDown;
                    board[shura, amuda].Font = new Font("Arial", 14, FontStyle.Bold);
                    board[shura, amuda].BackColor = Color.LightGray;
                    panel1.Controls.Add(board[shura, amuda]);

                    i++;

                }
            panel1.Width = btnsize * board.GetLength(1) + 5;
            panel1.Height = btnsize * board.GetLength(0) + 5;
            this.Height = btnsize * board.GetLength(0) + 115;

            if (btnsize * board.GetLength(1) + 35 >= 288)
            {
                this.Width = btnsize * board.GetLength(1) + 35;
                panel1.Location = new Point(9 , 70);
            }
            else
            {
                this.Width = 288;
                panel1.Location = new Point((288 - btnsize * board.GetLength(1)) / 2 - 8, 70);
            }
            int HowManyFlags = 0, bombs = 0;
            for (int shura = 0; shura < height; shura++)
            {
                for (int amuda = 0; amuda < width; amuda++)
                {
                    if (flagboard[shura, amuda] == 1)
                    {
                        HowManyFlags++;
                    }
                    if (numboard[shura, amuda] == -10)
                    {
                        bombs++;
                    }
                }
            }
            if (bombs - HowManyFlags >= 100)
                textBox4.Text = "  " + (bombs - HowManyFlags).ToString();
            else if (bombs - HowManyFlags >= 10)
                textBox4.Text = "  0" + (bombs - HowManyFlags).ToString();
            else
                textBox4.Text = "  00" + (bombs - HowManyFlags).ToString();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Start();
            string n = (((Button)(sender)).Tag).ToString();
            int place = int.Parse(n);

            int Yplace = place / width;
            int Xplace = place % width;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (firstpress == 0)
                {
                    Random rnd = new Random();
                    for (int HowManyBombs = height * width / 5; HowManyBombs > 0; HowManyBombs--)
                    {
                        int boii = rnd.Next(0, height * width);
                        if (numboard[boii / width, boii % width] == 0 &&
                            (boii/ width != Yplace && boii % width != Xplace))
                        {
                            numboard[boii / width, boii % width] = -10;
                        }
                        else
                        {
                            boii = rnd.Next(0, height * width);
                            HowManyBombs++;
                        }
                    }
                    for (int shura = 0; shura < height; shura++)
                        for (int amuda = 0; amuda < width; amuda++)
                        {
                            int bomb = 0;

                            #region check around
                            if (numboard[shura, amuda] == 0)
                            {
                                if (shura != 0 && amuda != 0 && numboard[shura - 1, amuda - 1] == -10)
                                {
                                    bomb++;
                                }
                                if (shura != height - 1 && amuda != width - 1 && numboard[shura + 1, amuda + 1] == -10)
                                {
                                    bomb++;
                                }
                                if (shura != height - 1 && amuda != 0 && numboard[shura + 1, amuda - 1] == -10)
                                {
                                    bomb++;
                                }
                                if (shura != 0 && amuda != width - 1 && numboard[shura - 1, amuda + 1] == -10)
                                {
                                    bomb++;
                                }
                                if (shura != height - 1 && numboard[shura + 1, amuda] == -10)
                                {
                                    bomb++;
                                }
                                if (shura != 0 && numboard[shura - 1, amuda] == -10)
                                {
                                    bomb++;
                                }
                                if (amuda != width - 1 && numboard[shura, amuda + 1] == -10)
                                {
                                    bomb++;
                                }
                                if (amuda != 0 && numboard[shura, amuda - 1] == -10)
                                {
                                    bomb++;
                                }
                                numboard[shura, amuda] = bomb;
                            }

                            #endregion
                        }


                    firstpress = 1;
                }

                if (Flag == 0)
                {
                    if (numboard[Yplace, Xplace] == -10 && flagboard[Yplace,Xplace] == 0)
                    {
                        for (int shura = 0; shura < height; shura++)
                            for (int amuda = 0; amuda < width; amuda++)
                            {
                                if (numboard[shura, amuda] == -10)
                                {
                                    if (flagboard[shura, amuda] == 0)
                                    {
                                        board[shura, amuda].BackgroundImage = Bomb;
                                        board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                        board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                        board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                    }
                                }
                                if (numboard[shura, amuda] >= 1 && numboard[shura, amuda] <= 8)
                                {
                                    if (flagboard[shura, amuda] == 0)
                                    {
                                        board[shura, amuda].Text = numboard[shura, amuda].ToString();
                                        board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                        board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                        board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);

                                        #region color1
                                        if (numboard[shura, amuda] == 1)
                                            board[shura, amuda].ForeColor = Color.Blue;
                                        else if (numboard[shura, amuda] == 2)
                                            board[shura, amuda].ForeColor = Color.Green;
                                        else if (numboard[shura, amuda] == 3)
                                            board[shura, amuda].ForeColor = Color.Red;
                                        else if (numboard[shura, amuda] == 4)
                                            board[shura, amuda].ForeColor = Color.DarkBlue;
                                        else if (numboard[shura, amuda] == 5)
                                            board[shura, amuda].ForeColor = Color.Brown;
                                        else if (numboard[shura, amuda] == 6)
                                            board[shura, amuda].ForeColor = Color.FromArgb(20, 140, 160);
                                        else if (numboard[shura, amuda] == 7)
                                            board[shura, amuda].ForeColor = Color.Black;
                                        else if (numboard[shura, amuda] == 8)
                                            board[shura, amuda].ForeColor = Color.Gray;
                                        #endregion
                                    }
                                    else
                                    {
                                        board[shura, amuda].BackgroundImage = WrongFlag;
                                        board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                    }
                                }
                                if (numboard[shura, amuda] == 0)
                                {
                                    board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                }
                            }
                        board[Yplace, Xplace].BackgroundImage = RedBomb;
                        board[Yplace, Xplace].BackgroundImageLayout = ImageLayout.Stretch;

                        button1.BackgroundImage = SadFace;
                        timer1.Stop();
                    }
                    if (numboard[Yplace, Xplace] >= -8 && numboard[Yplace, Xplace] <= -1)
                    {
                        int around = 0;
                        #region around
                        if (Yplace != height - 1 && Xplace != width - 1 && flagboard[Yplace + 1, Xplace + 1] == 1)
                            around++;
                        if (Yplace != 0 && Xplace != width - 1 && flagboard[Yplace - 1, Xplace + 1] == 1)
                            around++;
                        if (Yplace != height - 1 && Xplace != 0 && flagboard[Yplace + 1, Xplace - 1] == 1)
                            around++;
                        if (Yplace != 0 && Xplace != 0 && flagboard[Yplace - 1, Xplace - 1] == 1)
                            around++;
                        if (Yplace != height - 1 && flagboard[Yplace + 1, Xplace] == 1)
                            around++;
                        if (Yplace != 0 && flagboard[Yplace - 1, Xplace] == 1)
                            around++;
                        if (Xplace != width - 1 && flagboard[Yplace, Xplace + 1] == 1)
                            around++;
                        if (Xplace != 0 && flagboard[Yplace, Xplace - 1] == 1)
                            around++;
                        #endregion

                        if (numboard[Yplace, Xplace] == -around)
                        {
                            if (Yplace != height - 1 && Xplace != width - 1 && flagboard[Yplace + 1, Xplace + 1] == 0)
                            {
                                if (numboard[Yplace + 1, Xplace + 1] == -10)
                                {
                                    for (int shura = 0; shura < height; shura++)
                                        for (int amuda = 0; amuda < width; amuda++)
                                        {
                                            if (numboard[shura, amuda] == -10)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].BackgroundImage = Bomb;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                                }
                                            }
                                            if (numboard[shura, amuda] >= 1 && numboard[shura, amuda] <= 8)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].Text = numboard[shura, amuda].ToString();
                                                    board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);

                                                    #region color1
                                                    if (numboard[shura, amuda] == 1)
                                                        board[shura, amuda].ForeColor = Color.Blue;
                                                    else if (numboard[shura, amuda] == 2)
                                                        board[shura, amuda].ForeColor = Color.Green;
                                                    else if (numboard[shura, amuda] == 3)
                                                        board[shura, amuda].ForeColor = Color.Red;
                                                    else if (numboard[shura, amuda] == 4)
                                                        board[shura, amuda].ForeColor = Color.DarkBlue;
                                                    else if (numboard[shura, amuda] == 5)
                                                        board[shura, amuda].ForeColor = Color.Brown;
                                                    else if (numboard[shura, amuda] == 6)
                                                        board[shura, amuda].ForeColor = Color.FromArgb(20, 140, 160);
                                                    else if (numboard[shura, amuda] == 7)
                                                        board[shura, amuda].ForeColor = Color.Black;
                                                    else if (numboard[shura, amuda] == 8)
                                                        board[shura, amuda].ForeColor = Color.Gray;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    board[shura, amuda].BackgroundImage = WrongFlag;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                }
                                            }
                                            if (numboard[shura, amuda] == 0)
                                            {
                                                board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                            }
                                        }
                                }
                                press(Yplace + 1, Xplace + 1);
                            }
                            if (Yplace != 0 && Xplace != width - 1 && flagboard[Yplace - 1, Xplace + 1] == 0)
                            {
                                if (numboard[Yplace - 1, Xplace + 1] == -10)
                                {
                                    for (int shura = 0; shura < height; shura++)
                                        for (int amuda = 0; amuda < width; amuda++)
                                        {
                                            if (numboard[shura, amuda] == -10)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].BackgroundImage = Bomb;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                                }
                                            }
                                            if (numboard[shura, amuda] >= 1 && numboard[shura, amuda] <= 8)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].Text = numboard[shura, amuda].ToString();
                                                    board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);

                                                    #region color1
                                                    if (numboard[shura, amuda] == 1)
                                                        board[shura, amuda].ForeColor = Color.Blue;
                                                    else if (numboard[shura, amuda] == 2)
                                                        board[shura, amuda].ForeColor = Color.Green;
                                                    else if (numboard[shura, amuda] == 3)
                                                        board[shura, amuda].ForeColor = Color.Red;
                                                    else if (numboard[shura, amuda] == 4)
                                                        board[shura, amuda].ForeColor = Color.DarkBlue;
                                                    else if (numboard[shura, amuda] == 5)
                                                        board[shura, amuda].ForeColor = Color.Brown;
                                                    else if (numboard[shura, amuda] == 6)
                                                        board[shura, amuda].ForeColor = Color.FromArgb(20, 140, 160);
                                                    else if (numboard[shura, amuda] == 7)
                                                        board[shura, amuda].ForeColor = Color.Black;
                                                    else if (numboard[shura, amuda] == 8)
                                                        board[shura, amuda].ForeColor = Color.Gray;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    board[shura, amuda].BackgroundImage = WrongFlag;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                }
                                            }
                                            if (numboard[shura, amuda] == 0)
                                            {
                                                board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                            }
                                        }
                                }
                                press(Yplace - 1, Xplace + 1);
                            }
                            if (Yplace != height - 1 && Xplace != 0 && flagboard[Yplace + 1, Xplace - 1] == 0)
                            {
                                if (numboard[Yplace + 1, Xplace - 1] == -10)
                                {
                                    for (int shura = 0; shura < height; shura++)
                                        for (int amuda = 0; amuda < width; amuda++)
                                        {
                                            if (numboard[shura, amuda] == -10)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].BackgroundImage = Bomb;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                                }
                                            }
                                            if (numboard[shura, amuda] >= 1 && numboard[shura, amuda] <= 8)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].Text = numboard[shura, amuda].ToString();
                                                    board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);

                                                    #region color1
                                                    if (numboard[shura, amuda] == 1)
                                                        board[shura, amuda].ForeColor = Color.Blue;
                                                    else if (numboard[shura, amuda] == 2)
                                                        board[shura, amuda].ForeColor = Color.Green;
                                                    else if (numboard[shura, amuda] == 3)
                                                        board[shura, amuda].ForeColor = Color.Red;
                                                    else if (numboard[shura, amuda] == 4)
                                                        board[shura, amuda].ForeColor = Color.DarkBlue;
                                                    else if (numboard[shura, amuda] == 5)
                                                        board[shura, amuda].ForeColor = Color.Brown;
                                                    else if (numboard[shura, amuda] == 6)
                                                        board[shura, amuda].ForeColor = Color.FromArgb(20, 140, 160);
                                                    else if (numboard[shura, amuda] == 7)
                                                        board[shura, amuda].ForeColor = Color.Black;
                                                    else if (numboard[shura, amuda] == 8)
                                                        board[shura, amuda].ForeColor = Color.Gray;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    board[shura, amuda].BackgroundImage = WrongFlag;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                }
                                            }
                                            if (numboard[shura, amuda] == 0)
                                            {
                                                board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                            }
                                        }
                                }
                                press(Yplace + 1, Xplace - 1);
                            }
                            if (Yplace != 0 && Xplace != 0 && flagboard[Yplace - 1, Xplace - 1] == 0)
                            {
                                if (numboard[Yplace - 1, Xplace - 1] == -10)
                                {
                                    for (int shura = 0; shura < height; shura++)
                                        for (int amuda = 0; amuda < width; amuda++)
                                        {
                                            if (numboard[shura, amuda] == -10)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].BackgroundImage = Bomb;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                                }
                                            }
                                            if (numboard[shura, amuda] >= 1 && numboard[shura, amuda] <= 8)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].Text = numboard[shura, amuda].ToString();
                                                    board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);

                                                    #region color1
                                                    if (numboard[shura, amuda] == 1)
                                                        board[shura, amuda].ForeColor = Color.Blue;
                                                    else if (numboard[shura, amuda] == 2)
                                                        board[shura, amuda].ForeColor = Color.Green;
                                                    else if (numboard[shura, amuda] == 3)
                                                        board[shura, amuda].ForeColor = Color.Red;
                                                    else if (numboard[shura, amuda] == 4)
                                                        board[shura, amuda].ForeColor = Color.DarkBlue;
                                                    else if (numboard[shura, amuda] == 5)
                                                        board[shura, amuda].ForeColor = Color.Brown;
                                                    else if (numboard[shura, amuda] == 6)
                                                        board[shura, amuda].ForeColor = Color.FromArgb(20, 140, 160);
                                                    else if (numboard[shura, amuda] == 7)
                                                        board[shura, amuda].ForeColor = Color.Black;
                                                    else if (numboard[shura, amuda] == 8)
                                                        board[shura, amuda].ForeColor = Color.Gray;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    board[shura, amuda].BackgroundImage = WrongFlag;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                }
                                            }
                                            if (numboard[shura, amuda] == 0)
                                            {
                                                board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                            }
                                        }
                                }
                                press(Yplace - 1, Xplace - 1);
                            }
                            if (Yplace != height - 1 && flagboard[Yplace + 1, Xplace] == 0)
                            {
                                if (numboard[Yplace + 1, Xplace] == -10)
                                {
                                    for (int shura = 0; shura < height; shura++)
                                        for (int amuda = 0; amuda < width; amuda++)
                                        {
                                            if (numboard[shura, amuda] == -10)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].BackgroundImage = Bomb;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                                }
                                            }
                                            if (numboard[shura, amuda] >= 1 && numboard[shura, amuda] <= 8)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].Text = numboard[shura, amuda].ToString();
                                                    board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);

                                                    #region color1
                                                    if (numboard[shura, amuda] == 1)
                                                        board[shura, amuda].ForeColor = Color.Blue;
                                                    else if (numboard[shura, amuda] == 2)
                                                        board[shura, amuda].ForeColor = Color.Green;
                                                    else if (numboard[shura, amuda] == 3)
                                                        board[shura, amuda].ForeColor = Color.Red;
                                                    else if (numboard[shura, amuda] == 4)
                                                        board[shura, amuda].ForeColor = Color.DarkBlue;
                                                    else if (numboard[shura, amuda] == 5)
                                                        board[shura, amuda].ForeColor = Color.Brown;
                                                    else if (numboard[shura, amuda] == 6)
                                                        board[shura, amuda].ForeColor = Color.FromArgb(20, 140, 160);
                                                    else if (numboard[shura, amuda] == 7)
                                                        board[shura, amuda].ForeColor = Color.Black;
                                                    else if (numboard[shura, amuda] == 8)
                                                        board[shura, amuda].ForeColor = Color.Gray;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    board[shura, amuda].BackgroundImage = WrongFlag;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                }
                                            }
                                            if (numboard[shura, amuda] == 0)
                                            {
                                                board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                            }
                                        }
                                }
                                press(Yplace + 1, Xplace);
                            }
                            if (Yplace != 0 && flagboard[Yplace - 1, Xplace] == 0)
                            {
                                if (numboard[Yplace - 1, Xplace] == -10)
                                {
                                    for (int shura = 0; shura < height; shura++)
                                        for (int amuda = 0; amuda < width; amuda++)
                                        {
                                            if (numboard[shura, amuda] == -10)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].BackgroundImage = Bomb;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                                }
                                            }
                                            if (numboard[shura, amuda] >= 1 && numboard[shura, amuda] <= 8)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].Text = numboard[shura, amuda].ToString();
                                                    board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);

                                                    #region color1
                                                    if (numboard[shura, amuda] == 1)
                                                        board[shura, amuda].ForeColor = Color.Blue;
                                                    else if (numboard[shura, amuda] == 2)
                                                        board[shura, amuda].ForeColor = Color.Green;
                                                    else if (numboard[shura, amuda] == 3)
                                                        board[shura, amuda].ForeColor = Color.Red;
                                                    else if (numboard[shura, amuda] == 4)
                                                        board[shura, amuda].ForeColor = Color.DarkBlue;
                                                    else if (numboard[shura, amuda] == 5)
                                                        board[shura, amuda].ForeColor = Color.Brown;
                                                    else if (numboard[shura, amuda] == 6)
                                                        board[shura, amuda].ForeColor = Color.FromArgb(20, 140, 160);
                                                    else if (numboard[shura, amuda] == 7)
                                                        board[shura, amuda].ForeColor = Color.Black;
                                                    else if (numboard[shura, amuda] == 8)
                                                        board[shura, amuda].ForeColor = Color.Gray;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    board[shura, amuda].BackgroundImage = WrongFlag;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                }
                                            }
                                            if (numboard[shura, amuda] == 0)
                                            {
                                                board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                            }
                                        }
                                }
                                press(Yplace - 1, Xplace);
                            }
                            if (Xplace != width - 1 && flagboard[Yplace, Xplace + 1] == 0)
                            {
                                if (numboard[Yplace, Xplace + 1] == -10)
                                {
                                    for (int shura = 0; shura < height; shura++)
                                        for (int amuda = 0; amuda < width; amuda++)
                                        {
                                            if (numboard[shura, amuda] == -10)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].BackgroundImage = Bomb;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                                }
                                            }
                                            if (numboard[shura, amuda] >= 1 && numboard[shura, amuda] <= 8)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].Text = numboard[shura, amuda].ToString();
                                                    board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);

                                                    #region color1
                                                    if (numboard[shura, amuda] == 1)
                                                        board[shura, amuda].ForeColor = Color.Blue;
                                                    else if (numboard[shura, amuda] == 2)
                                                        board[shura, amuda].ForeColor = Color.Green;
                                                    else if (numboard[shura, amuda] == 3)
                                                        board[shura, amuda].ForeColor = Color.Red;
                                                    else if (numboard[shura, amuda] == 4)
                                                        board[shura, amuda].ForeColor = Color.DarkBlue;
                                                    else if (numboard[shura, amuda] == 5)
                                                        board[shura, amuda].ForeColor = Color.Brown;
                                                    else if (numboard[shura, amuda] == 6)
                                                        board[shura, amuda].ForeColor = Color.FromArgb(20, 140, 160);
                                                    else if (numboard[shura, amuda] == 7)
                                                        board[shura, amuda].ForeColor = Color.Black;
                                                    else if (numboard[shura, amuda] == 8)
                                                        board[shura, amuda].ForeColor = Color.Gray;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    board[shura, amuda].BackgroundImage = WrongFlag;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                }
                                            }
                                            if (numboard[shura, amuda] == 0)
                                            {
                                                board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                            }
                                        }
                                }
                                press(Yplace, Xplace + 1);
                            }
                            if (Xplace != 0 && flagboard[Yplace, Xplace - 1] == 0)
                            {
                                if (numboard[Yplace, Xplace - 1] == -10)
                                {
                                    for (int shura = 0; shura < height; shura++)
                                        for (int amuda = 0; amuda < width; amuda++)
                                        {
                                            if (numboard[shura, amuda] == -10)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].BackgroundImage = Bomb;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                                }
                                            }
                                            if (numboard[shura, amuda] >= 1 && numboard[shura, amuda] <= 8)
                                            {
                                                if (flagboard[shura, amuda] == 0)
                                                {
                                                    board[shura, amuda].Text = numboard[shura, amuda].ToString();
                                                    board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                    board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                    board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);

                                                    #region color1
                                                    if (numboard[shura, amuda] == 1)
                                                        board[shura, amuda].ForeColor = Color.Blue;
                                                    else if (numboard[shura, amuda] == 2)
                                                        board[shura, amuda].ForeColor = Color.Green;
                                                    else if (numboard[shura, amuda] == 3)
                                                        board[shura, amuda].ForeColor = Color.Red;
                                                    else if (numboard[shura, amuda] == 4)
                                                        board[shura, amuda].ForeColor = Color.DarkBlue;
                                                    else if (numboard[shura, amuda] == 5)
                                                        board[shura, amuda].ForeColor = Color.Brown;
                                                    else if (numboard[shura, amuda] == 6)
                                                        board[shura, amuda].ForeColor = Color.FromArgb(20, 140, 160);
                                                    else if (numboard[shura, amuda] == 7)
                                                        board[shura, amuda].ForeColor = Color.Black;
                                                    else if (numboard[shura, amuda] == 8)
                                                        board[shura, amuda].ForeColor = Color.Gray;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    board[shura, amuda].BackgroundImage = WrongFlag;
                                                    board[shura, amuda].BackgroundImageLayout = ImageLayout.Stretch;
                                                }
                                            }
                                            if (numboard[shura, amuda] == 0)
                                            {
                                                board[shura, amuda].BackColor = Color.FromArgb(190, 190, 190);
                                                board[shura, amuda].FlatStyle = FlatStyle.Flat;
                                                board[shura, amuda].FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                                            }
                                        }
                                }
                                press(Yplace, Xplace - 1);
                            }
                        }
                    }
                    press(Yplace, Xplace);

                    #region win
                    int win = 0;
                    for (int shura = 0; shura < height; shura++)
                        for (int amuda = 0; amuda < width; amuda++)
                        {
                            if (numboard[shura, amuda] >= -10 && numboard[shura, amuda] <= -1)
                            {
                                win++;
                            }
                        }
                    if (win == height * width)
                    {
                        MessageBox.Show("You Won");
                    }
                    #endregion
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int HowManyFlags = 0, bombs = 0;
                for (int shura = 0; shura < height; shura++)
                {
                    for (int amuda = 0; amuda < width; amuda++)
                    {
                        if (flagboard[shura, amuda] == 1)
                        {
                            HowManyFlags++;
                        }
                        if (numboard[shura, amuda] == -10)
                        {
                            bombs++;
                        }
                    }
                }
                if (((numboard[Yplace, Xplace] >= 0 && numboard[Yplace, Xplace] <= 8) || numboard[Yplace, Xplace] == -10)
                    && flagboard[Yplace, Xplace] == 0)
                {
                    if (bombs - HowManyFlags > 0)
                    {
                        board[Yplace, Xplace].BackgroundImage = flag;
                        board[Yplace, Xplace].BackgroundImageLayout = ImageLayout.Stretch;

                        flagboard[Yplace, Xplace] = 1;
                    }
                }
                else if (flagboard[Yplace, Xplace] == 1)
                {
                    board[Yplace, Xplace].BackgroundImage = null;
                    flagboard[Yplace, Xplace] = 0;
                }

                HowManyFlags = 0;
                bombs = 0;
                for (int shura = 0; shura < height; shura++)
                {
                    for (int amuda = 0; amuda < width; amuda++)
                    {
                        if (flagboard[shura, amuda] == 1)
                        {
                            HowManyFlags++;
                        }
                        if (numboard[shura, amuda] == -10)
                        {
                            bombs++;
                        }
                    }
                }
                if (bombs - HowManyFlags >=  100)
                {
                    textBox4.Text = "  " + (bombs - HowManyFlags).ToString();
                }
                else if (bombs - HowManyFlags >= 10)
                {
                    textBox4.Text = "  0" + (bombs - HowManyFlags).ToString();
                }
                else
                {
                    textBox4.Text = "  00" + (bombs - HowManyFlags).ToString();
                }
            }
            else
            {
                if (((numboard[Yplace, Xplace] >= 0 && numboard[Yplace, Xplace] <= 8) || numboard[Yplace, Xplace] == -10)
                    && flagboard[Yplace, Xplace] == 0)
                {
                    board[Yplace, Xplace].BackgroundImage = NotFlag;
                    board[Yplace, Xplace].BackgroundImageLayout = ImageLayout.Stretch;

                    flagboard[Yplace, Xplace] = 1;
                }
                else if (flagboard[Yplace, Xplace] == 1)
                {
                    board[Yplace, Xplace].BackgroundImage = null;
                    flagboard[Yplace, Xplace] = 0;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            if (time < 60)
            {
                if (minute >= 10)
                {
                    if (time >= 10)
                    {
                        textBox6.Text = " " + minute.ToString() + " : " + time.ToString();
                    }
                    else
                    {
                        textBox6.Text = " " + minute.ToString() + " : 0" + time.ToString();
                    }
                }
                else
                {
                    if (time >= 10)
                    {
                        textBox6.Text = " 0" + minute.ToString() + " : " + time.ToString();
                    }
                    else
                    {
                        textBox6.Text = " 0" + minute.ToString() + " : 0" + time.ToString();
                    }
                }
            }
            else
            {
                time = 0;
                minute++;
                if (minute >= 10)
                    textBox6.Text = " " + minute.ToString() + " : 0" + time.ToString();
                else
                    textBox6.Text = " 0" + minute.ToString() + " : 0" + time.ToString();

            }
        }
    }
}
