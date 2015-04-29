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
    /// Interaction logic for Piece.xaml
    /// </summary>
    public partial class Piece : UserControl
    {
        public Piece()
        {
            InitializeComponent();
        }

        public void SetState(PieceState state)
        {
            switch (state)
            {
                case PieceState.EMPTY:
                    this.CenterPiece.Fill = Brushes.White;
                    break;
                case PieceState.PLAYER_1:
                    this.CenterPiece.Fill = Brushes.Red;
                    break;
                case PieceState.PLAYER_2:
                    this.CenterPiece.Fill = Brushes.Green;
                    break;
                default:
                    throw new Exception("Unknown piece state");
            }
        }
    }
}
