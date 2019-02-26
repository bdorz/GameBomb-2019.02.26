using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Game
{
    public partial class Form1 : Form  //主要視窗構造cs
    {
        int count = 0;                                                  //翻開的格字數量統計
        static int Horizontal = 18, Vertical = 18, Bomb = 20;           //框架長寬格數 炸彈數量調整
        bool isStart = true;                                            //是否開始點擊方塊
        bool isLost = false;                                            //是否輸掉了本次的遊戲
        bool[,] isClicked = new bool[Horizontal, Vertical];             //記錄方塊是否被點擊
        bool[,] isLabeled = new bool[Horizontal, Vertical];             //記錄方塊是否被標記
        int[,] SquareInformation = new int[Horizontal, Vertical];       //記錄方塊內的資訊 0為沒翻開 -1為炸彈
        Button[,] SquareGame = new Button[Horizontal, Vertical];        //按鈕的實例方塊

        //視窗構造
        public Form1()                          
        {
            InitializeComponent();              //初始化組件
            InitializeGame();                   //初始化游戲
        }

        //初始化游戲
        private void InitializeGame()           
        {
            this.Text = "踩地雷";                                                  //標題
            this.Width = 35 + Horizontal * 30;                                     //窗寬
            this.Height = 60 + Vertical * 30;                                      //窗高
            this.MaximizeBox = false;                                              //不可最大化
            this.StartPosition = FormStartPosition.CenterScreen;                   //啟動位置
            this.FormBorderStyle = FormBorderStyle.FixedSingle;                    //不可改大小
           
            //按鈕控件的添加
            for (int x = 0; x < Horizontal; x++)
            {
                for (int y = 0; y < Vertical; y++)
                {
                    SquareInformation[x, y] = 0;                                     //初始無地雷
                    isLabeled[x, y] = false;
                    isClicked[x, y] = false;                                         //初始不點擊      
                    SquareGame[x, y] = new Button();
                    SquareGame[x, y].BackColor = Color.LightSkyBlue;                 //淺藍背景                  
                    SquareGame[x, y].Width = 30;                                     //按鈕的寬度
                    SquareGame[x, y].Height = 30;                                    //按鈕的高度
                    SquareGame[x, y].Top = x * 30 + 10;                              //按鈕的位置
                    SquareGame[x, y].Left = y * 30 + 10;
                    SquareGame[x, y].Tag = x * Horizontal + y + 1;                   //按鈕的標簽
                    SquareGame[x, y].Text = " ";                                     //按鈕的文本
                    SquareGame[x, y].Click += new EventHandler(OnClickedGameBomb);   //左鍵點擊方法
                    SquareGame[x, y].MouseUp += new MouseEventHandler(OnRightClicked); //右鍵標記方法
                    this.Controls.Add(SquareGame[x, y]);                             //讀取到視窗
                }
            }
        }
    }
}
