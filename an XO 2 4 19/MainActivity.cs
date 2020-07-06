using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Graphics;
using Android.Views;

namespace an_XO_2_4_19
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public const int N = 3;
        private Char player = 'p';
        private Button[,] arr;
        private Button restart;
        private int turnCount = 0;
        private LinearLayout linearLayoutBoard;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            this.linearLayoutBoard = (LinearLayout)this.FindViewById(Resource.Id.linearLayoutBoard);
            this.arr = new Button[N, N];
            this.restart = (Button)this.FindViewById(Resource.Id.restart);
            restart.Click += Restart_Click;
            BuildButtons();
            DrawBoard();
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            CleanButtons();
            turnCount = 0;
        }

        public void BuildButtons()
        {
            Point ScreenSize = new Point();
            this.WindowManager.DefaultDisplay.GetSize(ScreenSize);
            int buttonWidth = ScreenSize.X / N;
            ViewGroup.LayoutParams layoutParams = new ViewGroup.LayoutParams(buttonWidth, buttonWidth);
            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < N; col++)
                {
                    arr[row, col] = new Button(this);
                    arr[row, col].LayoutParameters = layoutParams;
                    arr[row, col].SetTextColor(Color.Beige);
                    arr[row, col].Text = "";
                    arr[row, col].SetBackgroundResource(Resource.Drawable.btnBackground);
                    arr[row, col].Click += Click;
                    
                    arr[row, col].TextSize = buttonWidth / 2;
                }
            }
        }
        public void DrawBoard()
        {
            ViewGroup.LayoutParams layoutParamsLine = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            for (int row = 0; row < N; row++)
            {
                LinearLayout linearLayoutLine = new LinearLayout(this);
                linearLayoutLine.LayoutParameters = layoutParamsLine;
                linearLayoutLine.Orientation = Orientation.Horizontal;
                for (int col = 0; col < N; col++)
                {
                    linearLayoutLine.AddView(arr[row, col]);
                }
                this.linearLayoutBoard.AddView(linearLayoutLine);
            }
        }
        public char PlayerTurn()
        {            
            if (turnCount % 2 == 0)
            {
                player = 'X';
                turnCount++;
                return 'X';
                
            }
            player = 'O';
            turnCount++;
            return 'O';
        }
        private void Click(object sender, EventArgs e)
        {
            if (Win())
            {
                return;
            }
            Button button = (Button)sender;
            if (button.Text == "")
            {
                button.Text = PlayerTurn().ToString();
            }
            else
            {
                Toast.MakeText(this, "kus ohto al abuk ya kazban", ToastLength.Long).Show();
            }
            
            if (Win())
            {
                Toast.MakeText(this, player.ToString() + " is the winner!", ToastLength.Long).Show();
                return;
            }

        }
        public void CleanButtons()
        {
            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < N; col++)
                {
                    arr[row, col].Text = "";
                }
            }
        }
        public bool SecondDiagonal()
        {           
            for (int i = 0; i < N; i++)
            {
                if (arr[i, (N -1)- i].Text != player.ToString())
                {
                    return false;
                }
            }
            return true;
        }
        public bool MainDiagonal()
        {
            for (int i = 0; i < N; i++)
            {
                if (arr[i, i].Text != player.ToString())
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckRow(int row)
        {
            for (int j = 0; j < N; j++)
            {
                if (arr[row, j].Text != player.ToString())
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckRows()
        {
            for (int row = 0; row < N; row++)
            {
                if (CheckRow(row) == true)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckCols()
        {
            for (int col= 0; col < N; col++)
            {
                if (CheckCol(col) == true)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckCol(int col)
        {
            for (int i = 0; i < N; i++)
            {
                if (arr[i, col].Text != player.ToString())
                {
                    return false;
                }
            }
            return true;
        }
        public bool Win()
        {
            if (CheckRows() || CheckCols() || MainDiagonal() || SecondDiagonal())
            {
                return true;
            }
            return false;
        }
    }
}