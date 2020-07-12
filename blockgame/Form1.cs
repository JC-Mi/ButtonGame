using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace blockgame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        const int N = 2;//按鈕的行列
        Button[,] buttons = new Button[N,N];//按鈕的陣列


        private void Form1_Load(object sender, EventArgs e)
        {
            //產生所有按鈕
            GenerateAllButtons();
        }
        private void btnBegin_Click(object sender, EventArgs e)
        {
            //打亂順序
            Shuffle();
        }

        
        //生成所有按鈕
        void GenerateAllButtons()
        {
            int x0 = 100, y0 = 10, w = 45, d = 50;

            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    int num = r * N + c;
                    Button btn = new Button();
                    btn.Text = (num + 1).ToString();
                    btn.Top = y0 + r * d;
                    btn.Left = x0 + c * d;
                    btn.Width = w;
                    btn.Height = w;
                    btn.Visible = true;
                    btn.Tag = r * N + c;//這個數據用來表示他所在的行列位置


                    btn.Click += new EventHandler(Btn_Click);//按鈕事件
                    buttons[r, c] = btn;//把產生按鈕放入陣列
                    this.Controls.Add(btn);//把按鈕放到介面中     

                }
            }
            buttons[N - 1, N - 1].Visible = false; //最後一個按鈕不顯示
        }


        //打亂順序
        void Shuffle()
        {
            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                int a = rnd.Next(N);
                int b = rnd.Next(N);
                int c = rnd.Next(N);
                int d = rnd.Next(N);
                Swap(buttons[a, b], buttons[c, d]);
            }

        }

        void Swap(Button btn1,Button btn2)
        {
            string t = btn1.Text;
            btn1.Text = btn2.Text;
            btn2.Text = t;

            bool v = btn1.Visible;
            btn1.Visible = btn2.Visible;
            btn2.Visible = v;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;//當前點中按鈕
            Button blank = FindHiddenButton();//空白按鈕

            //判斷是否相鄰，如果是則交換
            
            if (IsNeighbor(btn, blank)){
                Swap(btn, blank);
                blank.Focus();
            }
                

            
            //判斷是否完成
            if (ResultIsOk())
            {
                MessageBox.Show("OK");
            }
            
            
        }



        //找隱藏按鈕
        Button FindHiddenButton()
        {
            for (int r = 0;r < N;r++){
                for(int c = 0; c < N; c++)
                {
                    if (!buttons[r,c].Visible)
                    {
                        return buttons[r,c];
                    }
                }
            }

            return null;
        }



        bool IsNeighbor(Button btn1,Button btn2)
        {
            int a = (int)btn1.Tag;
            int b = (int)btn2.Tag;

            int r1 = a / N;
            int c1 = a % N;
            int r2 = b / N;
            int c2 = b % N;

            if (r1==r2&&(c1-c2==1||c2-c1==1)
                || c1 == c2 && (r1 - r2 == 1 || r2 - r1 == 1))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        bool ResultIsOk()
        {
            
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    if (buttons[r, c].Text != (r * N + c + 1).ToString())
                    {
                        return false;
                    }

                    
                }
            }

            return true;

        }

    }
}
