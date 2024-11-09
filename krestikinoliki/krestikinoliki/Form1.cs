using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace krestikinoliki
{
    public partial class Form1 : Form
    {
        private bool isXTurn = true;
        private int turnCount = 0;
        private Button[] buttons;

        public Form1()
        {
            InitializeComponent();
   
            buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            labelTitle.Text = "Крестики-Нолики";
            labelTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            labelTitle.ForeColor = Color.DarkSlateBlue;
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;

            labelTurn.Font = new Font("Arial", 12, FontStyle.Bold);
            labelTurn.ForeColor = Color.DarkGreen;
            labelTurn.TextAlign = ContentAlignment.MiddleCenter;
            labelResult.Font = new Font("Arial", 12, FontStyle.Bold);
            labelResult.ForeColor = Color.DarkGreen;
            labelResult.TextAlign = ContentAlignment.MiddleCenter;

            foreach (Button button in buttons)
            {
                button.Text = "";
                button.Font = new Font("Arial", 20, FontStyle.Bold);
                button.BackColor = Color.LightBlue;
                button.ForeColor = Color.DarkBlue;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = Color.DarkBlue;
                button.Size = new Size(80, 80);
            }

            resetButton.Text = "Сброс";
            resetButton.Font = new Font("Arial", 12, FontStyle.Bold);
            resetButton.BackColor = Color.LightCoral;
            resetButton.ForeColor = Color.White;
            resetButton.FlatStyle = FlatStyle.Flat;
            resetButton.FlatAppearance.BorderSize = 1;
            resetButton.FlatAppearance.BorderColor = Color.DarkRed;
            resetButton.Size = new Size(100, 40);

            labelResult.Text = "";
            labelTurn.Visible = true;
            UpdateTurnLabel();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Text == "")
            {
                button.Text = isXTurn ? "X" : "O";
                turnCount++;
                isXTurn = !isXTurn;
                UpdateTurnLabel();

                if (CheckForWinner())
                {
                    DisableButtons();
                    labelResult.Text = $"{button.Text} выиграл!";
                    labelTurn.Visible = false;
                }
                else if (turnCount == 9)
                {
                    labelResult.Text = "Ничья!";
                    labelTurn.Visible = false;
                }
            }
        }

        private void UpdateTurnLabel()
        {
            labelTurn.Text = isXTurn ? "Ход: X" : "Ход: O";
        }

        private bool CheckForWinner()
        {
            int[,] winningCombinations = new int[,]
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
                {0, 4, 8}, {2, 4, 6}
            };

            for (int i = 0; i < winningCombinations.GetLength(0); i++)
            {
                if (buttons[winningCombinations[i, 0]].Text == buttons[winningCombinations[i, 1]].Text &&
                    buttons[winningCombinations[i, 1]].Text == buttons[winningCombinations[i, 2]].Text &&
                    buttons[winningCombinations[i, 0]].Text != "")
                {
                    return true;
                }
            }
            return false;
        }

        private void DisableButtons()
        {
            foreach (Button button in buttons)
            {
                button.Enabled = false;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Text = "";
                button.Enabled = true;
            }
            isXTurn = true;
            turnCount = 0;
            labelResult.Text = "";
            labelTurn.Visible = true;
            UpdateTurnLabel();
        }
    }
}
