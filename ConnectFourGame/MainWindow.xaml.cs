/*
 * Copyright (c) 2015 Ivo Nunes, Samuel Rodrigues, Sérgio Lança
 *
 * This software is licensed under the GNU General Public License
 * (version 3 or later). See the COPYING file in this distribution.
 *
 * You should have received a copy of the GNU Library General Public
 * License along with this software; if not, write to the
 * Free Software Foundation, Inc., 59 Temple Place - Suite 330,
 * Boston, MA 02111-1307, USA.
 *
 * Authored by: Ivo Nunes <ivonunes@me.com>
 *              Samuel Rodrigues <samuel.f.f.rodrigues@gmail.com>
 *              Sérgio Lança <sergio.lanca@my.istec.pt>
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConnectFourGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int CurrentPlayer = 0;
        PieceState[,] BoardState = new PieceState[7, 6];

        public MainWindow()
        {
            InitializeComponent();
            this.DrawBoard();
        }

        private void DrawBoard()
        {
            this.BoardGrid.Children.Clear();

            for (int c = 0; c < 7; c++)
            {
                for (int r = 0; r < 6; r++)
                {
                    Piece p = new Piece();
                    p.SetState(this.BoardState[c, r]);
                    p.MouseLeftButtonUp += this.Piece_OnClick;
                    p.MouseEnter += this.Piece_Enter;
                    p.MouseLeave += this.Piece_Leave;
                    this.BoardGrid.Children.Add(p);
                    Grid.SetRow(p, r);
                    Grid.SetColumn(p, c);
                }
            }
        }

        private void Piece_Leave(object sender, MouseEventArgs e)
        {
            Piece p = (Piece)sender;

            if (this.BoardState[Grid.GetColumn(p), Grid.GetRow(p)] == PieceState.EMPTY)
                p.SetState(PieceState.EMPTY);
        }

        private void Piece_Enter(object sender, MouseEventArgs e)
        {
            Piece p = (Piece)sender;

            if (this.BoardState[Grid.GetColumn(p), Grid.GetRow(p)] == PieceState.EMPTY)
                p.SetState((this.CurrentPlayer > 0) ? PieceState.PLAYER_2 : PieceState.PLAYER_1);
        }

        private void CheckGame()
        {
            for (int c = 0; c < 7; c++)
            {
                for (int r = 0; r < 6; r++)
                {
                    if (this.BoardState[c, r] != PieceState.EMPTY)
                    {
                        // check left
                        if (c >= 3)
                        {
                            if (this.BoardState[c - 1, r] == this.BoardState[c, r] &&
                                this.BoardState[c - 2, r] == this.BoardState[c, r] &&
                                this.BoardState[c - 3, r] == this.BoardState[c, r])
                            {
                                this.WonGame();
                                return;
                            }
                        }

                        // check right
                        if (c < 4)
                        {
                            if (this.BoardState[c + 1, r] == this.BoardState[c, r] &&
                                this.BoardState[c + 2, r] == this.BoardState[c, r] &&
                                this.BoardState[c + 3, r] == this.BoardState[c, r])
                            {
                                this.WonGame();
                                return;
                            }
                        }

                        // check up
                        if (r >= 3)
                        {
                            if (this.BoardState[c, r - 1] == this.BoardState[c, r] &&
                                this.BoardState[c, r - 2] == this.BoardState[c, r] &&
                                this.BoardState[c, r - 3] == this.BoardState[c, r])
                            {
                                this.WonGame();
                                return;
                            }
                        }

                        // check down
                        if (r < 3)
                        {
                            if (this.BoardState[c, r + 1] == this.BoardState[c, r] &&
                                this.BoardState[c, r + 2] == this.BoardState[c, r] &&
                                this.BoardState[c, r + 3] == this.BoardState[c, r])
                            {
                                this.WonGame();
                                return;
                            }
                        }

                        //check diagR
                        if (c >= 3 && r < 3)
                        {
                            if (this.BoardState[c - 1, r + 1] == this.BoardState[c, r] &&
                                this.BoardState[c - 2, r + 2] == this.BoardState[c, r] &&
                                this.BoardState[c - 3, r + 3] == this.BoardState[c, r])
                            {
                                this.WonGame();
                                return;
                            }
                        }
                        //check diagL
                        if (c <= 3 && r < 3)
                        {
                            if (this.BoardState[c + 1, r + 1] == this.BoardState[c, r] &&
                                this.BoardState[c + 2, r + 2] == this.BoardState[c, r] &&
                                this.BoardState[c + 3, r + 3] == this.BoardState[c, r])
                            {
                                this.WonGame();
                                return;
                            }
                        }
                    }


                }


            }
            //check draw
            Boolean reset = true;
            for (int c = 0; c < 7; c++)
            {
                for (int r = 0; r < 6; r++)
                {
                    if (this.BoardState[c, r] == PieceState.EMPTY)
                        reset = false;
                }
            }
            if (reset)
            {
                MessageBox.Show("Draw");
                ResetGame();
            }
        }
            

        private void WonGame()
        {
            MessageBox.Show("Player " + ((this.CurrentPlayer > 0) ? "1 (red)" : "2 (green)") + " won.");
            this.ResetGame();
        }

        private void ResetGame()
        {
            this.CurrentPlayer = 0;
            this.BoardState = new PieceState[7, 6];
            this.DrawBoard();
        }

        private void Piece_OnClick(object sender, MouseButtonEventArgs e)
        {
            Piece p = (Piece)sender;

            for(int i = 5; i >= 0; i--) {
                if (this.BoardState[Grid.GetColumn(p), i] == PieceState.EMPTY) {
                    this.BoardState[Grid.GetColumn(p), i] = (this.CurrentPlayer > 0) ? PieceState.PLAYER_2 : PieceState.PLAYER_1;
                    this.CurrentPlayer = (this.CurrentPlayer > 0) ? 0 : 1;
                    break;
                }
            }

            this.DrawBoard();
            this.CheckGame();
        }
    }
}
