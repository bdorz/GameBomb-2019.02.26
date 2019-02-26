using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Game
{
    public partial class Form1 //裝置地雷的CS
    {
      
        //顯示所有地雷
        public void DiscoverAllBomb()
        {
            for (int x = 0; x < Horizontal; x++)
            {
                for (int y = 0; y < Vertical; y++)
                {
                    if (SquareInformation[x, y] == -1)
                        SquareGame[x, y].Text = "●";
                }
            }
        }

        //設置地雷
        public void SetBomb(int clickedI, int clickedJ)
        {

            int count, x, y, rangeX, rangeY;

            //隨機放置地雷
            Random random = new Random();
            for (count = 0; count < Bomb; count++)
            {
                x = random.Next(Horizontal);
                y = random.Next(Vertical);
                if (SquareInformation[x, y] == -1 || ((x == clickedI) && (y == clickedJ)))
                {
                    count--;
                    continue;
                }
                else
                    SquareInformation[x, y] = -1;
            }


            //計算周圍地雷數量
            for (x = 0; x < Horizontal; x++)
            {
                for (y = 0; y < Vertical; y++)
                {
                    count = 0;
                    if (SquareInformation[x, y] == -1) continue;
                    for (rangeX = -1; rangeX < 2; rangeX++)
                    {
                        for (rangeY = -1; rangeY < 2; rangeY++)
                        {
                            if (x + rangeX > -1 && x + rangeX < Horizontal && y + rangeY > -1 && y + rangeY < Vertical)
                            {
                                if (SquareInformation[x + rangeX, y + rangeY] == -1)
                                {
                                    count++;
                                }
                            }
                        }
                    }
                    SquareInformation[x, y] = count;
                }
            }

        }

        //重新開始游戲
        public void ReStartGame()
        {
            count = 0;
            isLost = false;
            isStart = true;
            for (int x = 0; x < Horizontal; x++)
            {
                for (int y = 0; y < Vertical; y++)
                {
                    SquareInformation[x, y] = 0;                                     //初始無地雷
                    isClicked[x, y] = false;                                         //初始不點擊
                    isLabeled[x, y] = false;                                         //初始不標記
                    SquareGame[x, y].BackColor = Color.LightSkyBlue;                 //淺藍背景色
                    SquareGame[x, y].Text = " ";                                     //按鈕的文本
                }
            }
        }
    }

}
