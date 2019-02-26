using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;



namespace Game
{
    partial class Form1 //左鍵與右鍵的點擊事件cs
    {
        private void OnRightClicked(object sender, MouseEventArgs e)               //右鍵的標記事件
        {
            Button thisSquare = sender as Button;
            int x = (int.Parse(thisSquare.Tag.ToString()) % Horizontal == 0 ? int.Parse(thisSquare.Tag.ToString()) / Horizontal - 1 : int.Parse(thisSquare.Tag.ToString()) / Horizontal),
                y = int.Parse(thisSquare.Tag.ToString()) - x * Horizontal - 1;
            if (e.Button == MouseButtons.Right && !isClicked[x, y])
            {
                isLabeled[x, y] = !isLabeled[x, y];
                if (isLabeled[x, y])
                    thisSquare.BackColor = Color.Red;
                else
                    thisSquare.BackColor = Color.LightSkyBlue;
            }
        }

        private void OnClickedGameBomb(object sender, EventArgs e)                 //左鍵的點擊事件
        {
            Button thisSquare = sender as Button;
            int x = (int.Parse(thisSquare.Tag.ToString()) % Horizontal == 0 ? int.Parse(thisSquare.Tag.ToString()) / Horizontal - 1 : int.Parse(thisSquare.Tag.ToString()) / Horizontal),
                y = int.Parse(thisSquare.Tag.ToString()) - x * Horizontal - 1;
            int rangeX, rangeY;

            if (isStart)                        //是否首次點擊
            {
                SetBomb(x, y);
                isStart = false;
            }
            if (isLabeled[x, y])
                return;
            if (isClicked[x, y] || isLost)         //是否已點擊過
                return;

            if (SquareInformation[x, y] == -1)               //是否踩中地雷
            {
                isLost = true;
                DiscoverAllBomb();
                MessageBox.Show("你輸了", "遊戲失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ReStartGame();
            }
            else
            {
                SquareGame[x, y].BackColor = Color.LightGreen;
                isClicked[x, y] = true;
                if (SquareInformation[x, y] > 0)           //周圍有雷，顯示雷的數量
                    thisSquare.Text = SquareInformation[x, y].ToString();
                else                            //周圍無雷，進行擴充處理
                {
                    for (rangeX = -1; rangeX < 2; rangeX++)
                    {
                        for (rangeY = -1; rangeY < 2; rangeY++)
                        {
                            if (x + rangeX > -1 && x + rangeX < Horizontal && y + rangeY > -1 && y + rangeY < Vertical)
                            {
                                if (SquareInformation[x + rangeX, y + rangeY] > -1)
                                {
                                    OnClickedGameBomb(SquareGame[x + rangeX, y + rangeY], null);
                                }
                            }
                        }
                    }
                }
            }

            count++;
            if (count == Horizontal * Vertical - Bomb)
            {
                DiscoverAllBomb();
                MessageBox.Show("你贏了", "游戲勝利", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReStartGame();
            }
        }

    }
}
