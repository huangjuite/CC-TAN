using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC_TAN
{
    public partial class Form1 : Form
    {
        float[] dashValues = { 2,2,2,2 };
        Point pt;
        int score = 0;
        int ctx;
        int cty;
        int[] r= new int[3];//for ability 5 & 6 & 7        
        bool s = false;//for ability 5
        bool drawC = false;
        Bitmap scr;
        Bitmap BallPicture;
        Bitmap STBPicture;
        Bitmap elephant;
        int timeS = 0;
        int[] timeI = new int[8];
        int wheelDelta;

        //balance
        int abilityInteval = 15;
        int ballspeed = 10;
        int ballInterval = 500;
        int ballcount=0;
        int SquareCount = 8;
        int squareInterval = 1000;
        int squareSpeed = 20;
        int level=5;
        int circleCount = 6;
        


        public class Ball
        {
            public PointF pos;
            public PointF dir;
            public bool visable;

            public Ball()
            {
                pos = new PointF();
                dir = new PointF();
                visable = false;
            }
            public void reset()
            {
                pos = new PointF();
                dir = new PointF();
                visable = false;
            }
            public void InRange(int pw, int ph)
            {
                if (pos.X >= pw + 100 || pos.X <= -pw-100 || pos.Y >= ph+100 || pos.Y <= -ph-100)
                {
                    reset();
                }
            }

            public void Bounce(float tx,float ty)
            {
                PointF ndir = new PointF();            
                double angle;
                //reflect matrices
                angle = 2 * Math.Atan((ty - pos.Y)/(tx - pos.X));
                ndir.X = (float)(Math.Cos(angle) * dir.X + Math.Sin(angle) * dir.Y);
                ndir.Y = (float)(Math.Sin(angle) * dir.X - Math.Cos(angle) * dir.Y);
                
                dir.X = -ndir.X;
                dir.Y = -ndir.Y;
            }
        }
        public class objC
        {
            public PointF pos;
            public RectangleF size;
            public System.Drawing.Pen PforC;
            public Color CforC;
            public bool visible;
            public bool ability;
            public Label text;
            public int count;
            public float radius;
            public objC()
            {
                ability = false;
                size = new RectangleF();
                pos = new PointF();
                radius = 25;
                count = 0;
                text = new Label();
                text.BackColor = Color.Black;
                text.Width = 25;
                visible = false;
            }
            public void reset()
            {
                text.Visible = false;
                size = new Rectangle();
                count = 0;
                ability = false;
                visible = false;
            }
            public void initial(int ctx,int cty,int type,int c)
            {
                count = c;
                ability = false;
                size = new RectangleF(pos.X - radius, pos.Y - radius, radius * 2, radius * 2);
                visible = true;
                switch (type)
                {
                    case 0:
                        CforC = System.Drawing.Color.Yellow;
                        break;
                    case 1:
                        CforC = System.Drawing.Color.Orange;
                        break;
                    case 2:
                        CforC = System.Drawing.Color.Pink;
                        break;
                    case 3:
                        CforC = System.Drawing.Color.Green;
                        break;
                    case 4:
                        CforC = System.Drawing.Color.Brown;
                        break;
                    case 5:
                        CforC = System.Drawing.Color.Aqua;
                        break;
                    case 6:
                        CforC = System.Drawing.Color.Honeydew;
                        break;

                }
                PforC = new System.Drawing.Pen(CforC, 2);
                text.Visible = true;
                text.ForeColor = CforC;
                text.Location = new Point((int)pos.X + ctx-8, (int)pos.Y + cty-5);
            }
            public void update()
            {
                if (count <= 0)
                {
                    ability = true;
                }
                else text.Text = Convert.ToString(count);
            }
        }
        public class objS
        {
            public RectangleF rec = new RectangleF();
            public PointF pos;
            public PointF dir;
            public System.Drawing.Pen PforS;
            public Color CforS;
            public bool visible;
            public Label text;
            public int count;
            public int length;
            public int total;
            public objS()
            {
                rec = new RectangleF();
                dir = new PointF();
                pos = new PointF();
                text = new Label();
                text.BackColor = Color.Black;
                text.Width = 25;
                length = 50;
                CforS = Color.Orange;
                PforS = new System.Drawing.Pen(CforS, 2);
                count = 0;
                total = 0;
                visible = false;
            }
            public void reset()
            {
                text.Visible = false;
                rec = new RectangleF();
                dir = new PointF();
                pos = new PointF();
                total = 0;
                count = 0;
                length = 50;
                visible = false;
            }
            public void initial(int num,int ctx,int cty)
            {
                count = num;
                total = num;
                CforS = System.Drawing.Color.FromArgb(255, 0, 255);
                PforS = new System.Drawing.Pen(CforS, 2);
                text.Text = Convert.ToString(count);
                text.Location = new Point((int)pos.X+ctx-5,(int)pos.Y+cty-5);
                text.ForeColor = CforS;
                visible = true;
                text.Visible = true;
                Random rnd = new Random();
                switch (rnd.Next(1, 5))
                {
                    case 1:
                        pos.X = rnd.Next(-ctx, ctx);
                        pos.Y = cty+length/2;
                        break;
                    case 2:
                        pos.X = rnd.Next(-ctx, ctx);
                        pos.Y = -cty-length/2;
                        break;
                    case 3:
                        pos.X = ctx+length/2;
                        pos.Y = rnd.Next(-cty, cty);
                        break;
                    case 4:
                        pos.X = -ctx-length/2;
                        pos.Y = rnd.Next(-cty, cty);
                        break;
                }
            }
            public void update(int ctx,int cty)
            {
                CforS = System.Drawing.Color.FromArgb(255 * count / total, 255 * (total - count) / total, 255);
                PforS = new System.Drawing.Pen(CforS, 2);
                text.Text = Convert.ToString(count);
                text.Location = new Point((int)pos.X + ctx - 5, (int)pos.Y + cty - 5);
                text.ForeColor = CforS;
                rec = new RectangleF(pos.X - length / 2, pos.Y - length / 2, length, length);
            }

        }

        

        Ball[] ballarray = new Ball[1000];
        objC[] circlearray = new objC[7];
        objS[] squareArray = new objS[500];
        Ball BigBall = new Ball();
        Ball STButton = new Ball();



        public Form1()
        {
            ctx = this.ClientSize.Width / 2;
            cty = this.ClientSize.Height / 2;
            InitializeComponent();           
            timerStart.Interval = ballspeed;
            timerRun.Enabled = false;
            timerOfS.Enabled = false;
            timerSqSpeed.Enabled = false;
            timerEnd.Enabled = false;
            cursortimer.Interval = ballspeed;
            timer1.Interval = ballspeed;
            timer2.Interval = ballInterval;
            timerOfS.Interval = squareInterval;
            timerSqSpeed.Interval = squareSpeed;
            timerEnd.Interval = squareSpeed;

            BallPicture = new Bitmap(Properties.Resources.ball);
            elephant = new Bitmap(Properties.Resources.elephant);
            STBPicture = new Bitmap(Properties.Resources.play_button_icon_png_0);
            STButton.pos = new PointF(100, 100);
            label1.SendToBack();

            for (int i = 0; i < ballarray.Length; i++)
            {
                ballarray[i] = new Ball();
            }
            for(int i = 0; i < squareArray.Length; i++)
            {
                squareArray[i] = new objS();
                this.Controls.Add(squareArray[i].text);
                squareArray[i].text.BringToFront();
            }
            for(int i = 0; i < circlearray.Length; i++)
            {
                circlearray[i] = new objC();
                this.Controls.Add(circlearray[i].text);
                circlearray[i].text.BringToFront();
            }
            label3.BringToFront();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            ctx = this.ClientSize.Width / 2;
            cty = this.ClientSize.Height / 2;
            scr = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics dr1 = Graphics.FromImage(scr);
            dr1.TranslateTransform(ctx, cty);
            Graphics dr2 = Graphics.FromImage(scr);
            dr2.TranslateTransform(ctx, cty);
            Graphics dr3 = Graphics.FromImage(scr);
            dr3.TranslateTransform(ctx, cty);
            Graphics dr4 = Graphics.FromImage(scr);
            dr4.TranslateTransform(ctx,cty);
            Graphics dr5 = Graphics.FromImage(scr);
            dr5.TranslateTransform(ctx, cty);
            dr5.RotateTransform(angle(pt)-90);

            if (timerEnd.Enabled)
            {
                label3.Visible = true;                
            }
            else
            {
                label3.Visible = false;
            }

            if (timerStart.Enabled)
            {
                label1.Text = "CC-TAN";
            }
            else
            {
                label1.Text = score.ToString();
            }

            if (drawC)
            {
                dr5.DrawImage(elephant, -elephant.Width / 2, -elephant.Height / 2);
            }


            //畫球
            for (int i = 0; i < ballarray.Length; i++)
            {
                if (ballarray[i].visable)
                {
                    dr2.DrawImage(BallPicture, ballarray[i].pos.X - BallPicture.Width / 2, ballarray[i].pos.Y - BallPicture.Height / 2);             
                }
            }

            //畫方
            for(int i = 0; i < squareArray.Length; i++)
            {
                if (squareArray[i].visible)
                {
                    dr3.DrawRectangle(squareArray[i].PforS, Rectangle.Round(squareArray[i].rec));                   
                }
            }

            //畫圓
            for(int i = 0; i < circlearray.Length; i++)
            {
                if (circlearray[i].visible)
                {
                    dr4.DrawEllipse(circlearray[i].PforC, circlearray[i].size);
                }
            }


            if (timerStart.Enabled)
            {
                dr2.DrawImage(STBPicture, STButton.pos.X - STBPicture.Width / 2, STButton.pos.Y - STBPicture.Height / 2);
                SolidBrush brush = new SolidBrush(Color.FromArgb(128, 200, (ballcount * 10) % 255, 255 - (ballcount * 10 % 255)));
                dr1.FillEllipse(brush, (float)(STButton.pos.X- STBPicture.Width / 2+2), (float)(STButton.pos.Y- STBPicture.Height / 2+2), 
                    STBPicture.Width-5, STBPicture.Width-5);
                Pen p = new Pen(Color.FromArgb(128, 200, (ballcount * 10) % 255, 255 - (ballcount * 10 % 255)),(float)2.5);
                p.DashPattern = dashValues;
                dr1.DrawEllipse(p, -STBPicture.Width / 2, - STBPicture.Width / 2, STBPicture.Width - 5, STBPicture.Width - 5);
            }

            if (ability1timer.Enabled)
            {
                Color c = System.Drawing.Color.FromArgb((timeS % 10) * 20, (timeS % 10) * 20, 255);
                Pen p = new Pen(c, 2);
                p.DashPattern = dashValues;
                dr1.DrawLine(p, -UnitVector(pt).X * 20, -UnitVector(pt).Y * 20, UnitVector(pt).X * 20, UnitVector(pt).Y * 20);
                dr1.DrawLine(p, -UnitVector(pt).Y * 20, -UnitVector(pt).X * 20, UnitVector(pt).Y * 20, UnitVector(pt).X * 20);
            }
            if (ability2timer.Enabled)
            {
                Color c = System.Drawing.Color.FromArgb(255 - (timeS % 10) * 20, (timeS % 10) * 20, 255);
                Pen p = new Pen(c, 2);
                p.DashPattern = dashValues;
                dr1.DrawEllipse(p, new RectangleF(pt.X-70, pt.Y-70, 140, 140));
            }
            if (ability3timer.Enabled)
            {
                Color c = System.Drawing.Color.FromArgb(255 - (timeS % 10) * 20, (timeS % 10) * 20, 255);
                Pen p = new Pen(c, 2);
                dr1.DrawEllipse(p, new RectangleF(-200, -200, 400, 400));
            }
            if (ability4timer.Enabled)
            {
                Color c = System.Drawing.Color.FromArgb((timeS%10)*20, (timeS % 10) * 20, 255);
                Pen p = new Pen(c,10);
                dr1.DrawRectangle(p,new Rectangle(-ctx,-cty,ctx*2,cty*2));
            }
            if (ability5timer.Enabled)
            {
                float r = (float)(13-(timeS - timeI[4]) + 0.5) * 10+12;
                Color c = System.Drawing.Color.FromArgb(255 - (timeS % 10) * 20, (timeS % 10) * 20, 255);
                Pen p = new Pen(c, 2);
                dr1.DrawEllipse(p, new RectangleF(-r+BigBall.pos.X, -r+BigBall.pos.Y, r*2, r*2));
            }
            if (ability6timer.Enabled)
            {
                Color c = System.Drawing.Color.FromArgb(255 - r[1]%255, r[1]%255, 255);
                Pen p = new Pen(c, 3);
                dr1.DrawEllipse(p, new RectangleF(-r[1] + circlearray[5].pos.X, -r[1]+circlearray[5].pos.Y , r[1] * 2, r[1] * 2));
            }
            if (ability7timer.Enabled)
            {
                Color c = System.Drawing.Color.FromArgb(255 - r[2] % 255, r[2] % 255, 255);
                Pen p = new Pen(c, 2);
                dr1.DrawRectangle(p, new Rectangle(-r[2] , -r[2] , r[2] * 2, r[2] * 2));
            }


            e.Graphics.DrawImage(scr, 0, 0);
            dr1.Dispose(); 
            dr2.Dispose();
            dr3.Dispose();
            dr4.Dispose();
            dr5.Dispose();
            scr.Dispose();
        }



        private void HitSquare(int i,int k)
        {
            bool p = false;
            if ((ballarray[i].pos.X < squareArray[k].pos.X + squareArray[k].length / 2 && ballarray[i].pos.X > squareArray[k].pos.X - squareArray[k].length / 2))
            {
                if (Math.Abs(ballarray[i].pos.Y - squareArray[k].pos.Y) <= BallPicture.Height / 2 + squareArray[k].length / 2)
                {
                    ballarray[i].Bounce(ballarray[i].pos.X, ballarray[i].pos.Y - BallPicture.Height / 2);
                    while(Math.Abs(ballarray[i].pos.Y - squareArray[k].pos.Y) <= BallPicture.Height / 2 + squareArray[k].length / 2
                        && Math.Abs(ballarray[i].pos.X - squareArray[k].pos.X) <= BallPicture.Height / 2 + squareArray[k].length / 2)
                    {
                        ballarray[i].pos.X += ballarray[i].dir.X;
                        ballarray[i].pos.Y += ballarray[i].dir.Y;
                    }
                    squareArray[k].count -= 1;
                    p = true;             
                }
            }
            else if (ballarray[i].pos.Y < squareArray[k].pos.Y + squareArray[k].length / 2 && ballarray[i].pos.Y > squareArray[k].pos.Y - squareArray[k].length / 2)
            {
                if (Math.Abs(ballarray[i].pos.X - squareArray[k].pos.X) <= BallPicture.Height / 2 + squareArray[k].length / 2 + 3)
                {
                    ballarray[i].Bounce(ballarray[i].pos.X - BallPicture.Height / 2, ballarray[i].pos.Y);
                    while (Math.Abs(ballarray[i].pos.Y - squareArray[k].pos.Y) <= BallPicture.Height / 2 + squareArray[k].length / 2
                        && Math.Abs(ballarray[i].pos.X - squareArray[k].pos.X) <= BallPicture.Height / 2 + squareArray[k].length / 2)
                    {
                        ballarray[i].pos.X += ballarray[i].dir.X;
                        ballarray[i].pos.Y += ballarray[i].dir.Y;
                    }
                    squareArray[k].count -= 1;
                    p = true;
                }
            }
            if (distance(squareArray[k].pos.X - squareArray[k].length / 2, squareArray[k].pos.Y - squareArray[k].length / 2, ballarray[i].pos) < BallPicture.Height / 2)
            {
                ballarray[i].Bounce(squareArray[k].pos.X - squareArray[k].length / 2, squareArray[k].pos.Y - squareArray[k].length / 2);
                squareArray[k].count -= 1;
                p = true;
            }
            else if (distance(squareArray[k].pos.X + squareArray[k].length / 2, squareArray[k].pos.Y - squareArray[k].length / 2, ballarray[i].pos) < BallPicture.Height / 2)
            {
                ballarray[i].Bounce(squareArray[k].pos.X + squareArray[k].length / 2, squareArray[k].pos.Y - squareArray[k].length / 2);
                squareArray[k].count -= 1;
                p = true;
            }
            else if (distance(squareArray[k].pos.X - squareArray[k].length / 2, squareArray[k].pos.Y + squareArray[k].length / 2, ballarray[i].pos) < BallPicture.Height / 2)
            {
                ballarray[i].Bounce(squareArray[k].pos.X - squareArray[k].length / 2, squareArray[k].pos.Y + squareArray[k].length / 2);
                squareArray[k].count -= 1;
                p = true;
            }
            else if (distance(squareArray[k].pos.X + squareArray[k].length / 2, squareArray[k].pos.Y + squareArray[k].length / 2, ballarray[i].pos) < BallPicture.Height / 2)
            {
                ballarray[i].Bounce(squareArray[k].pos.X + squareArray[k].length / 2, squareArray[k].pos.Y + squareArray[k].length / 2);
                squareArray[k].count -= 1;
                p = true;
            }


            if (ability7timer.Enabled && squareArray[k].count > 1 && p)
            {
                bool t=true;
                squareArray[k].count /= 2;
                squareArray[k].pos.X -= 15 * squareArray[k].dir.X;
                squareArray[k].pos.Y -= 15 * squareArray[k].dir.Y;
                while (t)
                {
                    if (squareArray[SquareCount].visible == false)
                    {
                        Random rnd = new Random();
                        PointF v = new PointF();
                        v = UnitVector(RotateVector(squareArray[k].pos, rnd.Next(-90, 90)));
                        squareArray[SquareCount].initial(squareArray[k].count, ctx, cty);
                        squareArray[SquareCount].pos.X = v.X + squareArray[k].pos.X;
                        squareArray[SquareCount].pos.Y = v.Y + squareArray[k].pos.Y;
                        squareArray[SquareCount].dir = UnitVector(squareArray[SquareCount].pos);
                        squareArray[SquareCount].dir.X *= -(float)(0.025);
                        squareArray[SquareCount].dir.Y *= -(float)(0.025);
                        t = false;
                    }
                    else SquareCount++;
                }
                SquareCount++;
                if (SquareCount == squareArray.Length) SquareCount = 0;

            }
        }
        private void HitCircle(int i,int k)
        {
            if (distance(circlearray[k].pos, ballarray[i].pos) <= (circlearray[k].radius + BallPicture.Height / 2))
            {
                ballarray[i].Bounce(circlearray[k].pos.X, circlearray[k].pos.Y);
                circlearray[k].count--;
            }
        }
        private void HitWall(int i)
        {

            //右牆
            if (ballarray[i].pos.X > ctx - BallPicture.Height / 2)
            {
                ballarray[i].Bounce(ballarray[i].pos.X + (BallPicture.Height / 2), ballarray[i].pos.Y);
            }
            //左牆            
            if (ballarray[i].pos.X < -1 * (ctx - BallPicture.Height / 2))
            {
                ballarray[i].Bounce(ballarray[i].pos.X - (BallPicture.Height / 2), ballarray[i].pos.Y);
            }
            //下牆
            if (ballarray[i].pos.Y > cty - BallPicture.Height / 2)
            {
                ballarray[i].Bounce(ballarray[i].pos.X, ballarray[i].pos.Y + BallPicture.Height / 2);
            }
            //上牆
            if (ballarray[i].pos.Y < -1*(cty - BallPicture.Height / 2))
            {
                ballarray[i].Bounce(ballarray[i].pos.X , ballarray[i].pos.Y - BallPicture.Height / 2);
            }            
        }
        private void circleAbility(int type)
        {
            switch (type)
            {
                case 0:
                    ability1timer.Enabled = true;
                    ability1timer.Interval = timer2.Interval;
                    timeI[0] = timeS;
                    break;
                case 1:
                    ability2timer.Enabled = true;
                    ability2timer.Interval = ballspeed;
                    timeI[1] = timeS;
                    break;
                case 2:
                    ability3timer.Enabled = true;
                    ability3timer.Interval = ballspeed;
                    timeI[2] = timeS;
                    break;
                case 3:
                    ability4timer.Enabled = true;
                    ability4timer.Interval = ballspeed;
                    timeI[3] = timeS;
                    break;
                case 4:                   
                    timeI[4] = timeS;
                    r[0] = 20;
                    s = false;
                    ability5timer.Enabled = true;
                    ability5timer.Interval = ballspeed;
                    BigBall.visable = true;
                    break;
                case 5:
                    if (timer2.Interval > 30) timer2.Interval = (int)(timer2.Interval * 0.6);
                    r[1] = 10;
                    timeI[5] = timeS;
                    ability6timer.Enabled = true;
                    ability6timer.Interval = 30;
                    break;
                case 6:
                    timeI[6] = timeS;
                    r[2] = 10;
                    ability7timer.Enabled = true;
                    ability7timer.Interval = 30;
                    break;
            }
        }
        private void ResetAll()
        {
            for(int i = 0; i < squareArray.Length; i++)
            {
                squareArray[i].reset();
            }
            for(int i = 0; i < ballarray.Length; i++)
            {
                ballarray[i].reset();
            }
            for(int i = 0; i < circlearray.Length; i++)
            { 
                circlearray[i].reset();
            }
        }
        private void gameover()
        {
            timer2.Enabled = false;
            timer1.Enabled = false;
            ability1timer.Enabled = false;
            ability2timer.Enabled = false;
            ability3timer.Enabled = false;
            ability4timer.Enabled = false;
            ability5timer.Enabled = false;
            ability6timer.Enabled = false;
            ability7timer.Enabled = false;
            timerOfS.Enabled = false;
            timerSqSpeed.Enabled = false;
            timerEnd.Enabled = true;
            timeI[7] = timeS;
        }



        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            wheelDelta = e.Delta;           
        }
        private void cursortimer_Tick(object sender, EventArgs e)
        {
            pt = Cursor.Position;
            pt = this.PointToClient(pt);
            if (timerStart.Enabled)
            {
                pt.Offset(-ctx, -2*cty);
            }
            else
            {
                pt.Offset(-ctx, -cty);
            }
                

            if (pt.X == 0 && pt.Y == 0)
            {
                pt.X = 1;
                pt.Y = 1;
            }
            //this.Invalidate();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
           
            for (int i = 0; i < ballarray.Length; i++)
            {
                if (ballarray[i].visable)
                {
                    ballarray[i].pos.X += ballarray[i].dir.X;
                    ballarray[i].pos.Y += ballarray[i].dir.Y;
                }
                ballarray[i].InRange(ctx, cty);
            }


            //判斷碰撞
            for (int i = 0; i < ballarray.Length; i++)
            {
                if (ballarray[i].visable)
                {
                    if (ability4timer.Enabled)
                        HitWall(i);

                    for (int k = 0; k < circlearray.Length; k++)
                    {
                        if (circlearray[k].visible)
                        {
                            HitCircle(i,k);
                            circlearray[k].update();
                            if (circlearray[k].ability)
                            {
                                circlearray[k].reset();
                                circleAbility(k);
                            }
                        }
                    }

                    for (int k = 0; k < squareArray.Length; k++)
                    {
                        if (squareArray[k].visible)
                        {
                            HitSquare(i, k);
                            if (squareArray[k].count <= 0)
                            {
                                squareArray[k].reset();
                                score++;
                            }
                            else
                            {
                                squareArray[k].update(ctx, cty);     
                            }
                        }
                    }
                }
            }
            
            this.Invalidate();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            int t = 0;
            bool p = true;
            while (p)
            {
                if (ballarray[ballcount].visable == false)
                {
                    if (ability2timer.Enabled)
                    {
                        ballarray[ballcount].dir = UnitVector(pt);
                        ballarray[ballcount].dir = RotateVector(ballarray[ballcount].dir, -90);
                        ballarray[ballcount].dir.X *= (float)0.8;
                        ballarray[ballcount].dir.Y *= (float)0.8;
                        ballarray[ballcount].visable = true;
                        p = false;
                    }                    
                    else
                    {
                        ballarray[ballcount].dir = UnitVector(pt);
                        if (timerStart.Enabled) ballarray[ballcount].pos = new PointF(0, cty);                        
                        ballarray[ballcount].dir.X *= (float)0.8;
                        ballarray[ballcount].dir.Y *= (float)0.8;
                        ballarray[ballcount].visable = true;
                        p = false;
                    }
                }
                ballcount++;
                if (ballcount == ballarray.Length) ballcount = 0;

                t++;
                if (t == ballarray.Length) p = false;
            }
            //this.Invalidate();
        }
        private void timerOfS_Tick(object sender, EventArgs e)
        {
            if (level % 2 == 0)
            {
                bool p = true;
                while (p)
                {
                    if (squareArray[SquareCount].visible == false)
                    {
                        Random rnd = new Random();
                        squareArray[SquareCount].initial(rnd.Next() % level, ctx, cty);
                        squareArray[SquareCount].dir = UnitVector(squareArray[SquareCount].pos);
                        squareArray[SquareCount].dir.X *= -(float)(0.025);
                        squareArray[SquareCount].dir.Y *= -(float)(0.025);
                        p = false;
                    }
                    else SquareCount++;
                }
                SquareCount++;
                if (SquareCount == squareArray.Length) SquareCount = 0;

                if (SquareCount % 10 == 0)
                {
                    Random rnd = new Random();
                    circlearray[circleCount].pos = UnitVector(new Point(rnd.Next(0, 200)-100, rnd.Next(0, 200)-100));
                    circlearray[circleCount].pos.X *= 20;
                    circlearray[circleCount].pos.Y *= 20;
                    circlearray[circleCount].initial(ctx, cty, circleCount, level / 2);
                    circleCount++;
                    circleCount %= 7;
                }
            }

            //this.Invalidate();
        }
        private void SqSpeed_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < squareArray.Length; i++)
            {
                for(int k = 0; k < squareArray.Length; k++)
                {
                    if (squareArray[i].visible && squareArray[k].visible && i!=k)
                    {
                        if (distance(squareArray[i].pos, squareArray[k].pos) < squareArray[i].length/5)
                        {
                            squareArray[i].count += squareArray[k].count;
                            squareArray[i].total += squareArray[k].total;
                            squareArray[k].count = 0;
                            squareArray[k].reset();
                            squareArray[i].update(ctx, cty);

                            break;
                        }
                        else if ( !ability7timer.Enabled && distance(squareArray[i].pos, squareArray[k].pos) < squareArray[i].length)
                        {
                            PointF p = new PointF();
                            p.X = squareArray[i].pos.X - squareArray[k].pos.X;
                            p.Y = squareArray[i].pos.Y - squareArray[k].pos.Y;
                            squareArray[k].dir = UnitVector(p);
                            squareArray[k].dir.X *= (float)0.5;
                            squareArray[k].dir.Y *= (float)0.5;
                        }
                        else if (ability7timer.Enabled && distance(squareArray[i].pos, squareArray[k].pos) < squareArray[i].length +5)
                        {
                            PointF p = new PointF();
                            p.X = squareArray[i].pos.X - squareArray[k].pos.X;
                            p.Y = squareArray[i].pos.Y - squareArray[k].pos.Y;
                            squareArray[k].dir = UnitVector(p);
                            squareArray[k].dir.X *= -(float)0.5;
                            squareArray[k].dir.Y *= -(float)0.5;
                        }
                    }
                }
                if (squareArray[i].visible)
                {
                    squareArray[i].pos.X += squareArray[i].dir.X;
                    squareArray[i].pos.Y += squareArray[i].dir.Y;
                    squareArray[i].dir = UnitVector(squareArray[i].pos);
                    squareArray[i].dir.X *= -(float)(0.025);
                    squareArray[i].dir.Y *= -(float)(0.025);

                    if (distance(0, 0, squareArray[i].pos) < 50 )
                    {              
                        gameover();
                    }                   
                }
            }
            this.Invalidate();
        }
        private void timerRun_Tick(object sender, EventArgs e)
        {
            timeS++;
            if (timeS % 5 == 0) level++;
        }
        private void ability1timer_Tick(object sender, EventArgs e)
        {
            bool p = true;
            int i = 0;
            //後
            while (p)
            {
                if (ballarray[ballcount].visable == false)
                {
                    ballarray[ballcount].dir = UnitVector(pt);
                    ballarray[ballcount].dir.X *= -(float)0.8;
                    ballarray[ballcount].dir.Y *= -(float)0.8;
                    ballarray[ballcount].visable = true;
                    p = false;
                }
                ballcount++;
                if (ballcount == ballarray.Length) ballcount = 0;
                i++;
                if (i == ballarray.Length) p = false;
            }

            
            //左
            p = true;
            float t;
            i = 0;
            while (p)
            {
                if (ballarray[ballcount].visable == false)
                {
                    ballarray[ballcount].dir = UnitVector(pt);
                    ballarray[ballcount].dir.X *= (float)0.8;
                    ballarray[ballcount].dir.Y *= (float)0.8;
                    t = ballarray[ballcount].dir.X;
                    ballarray[ballcount].dir.X = -1 * ballarray[ballcount].dir.Y;
                    ballarray[ballcount].dir.Y = t;
                    ballarray[ballcount].visable = true;
                    p = false;
                }
                ballcount++;
                if (ballcount == ballarray.Length) ballcount = 0;
                i++;
                if (i == ballarray.Length) p = false;
            }


            //右
            p = true;
            i = 0;
            while (p)
            {
                if (ballarray[ballcount].visable == false)
                {
                    ballarray[ballcount].dir = UnitVector(pt);
                    ballarray[ballcount].dir.X *= (float)0.8;
                    ballarray[ballcount].dir.Y *= (float)0.8;
                    t = ballarray[ballcount].dir.X;
                    ballarray[ballcount].dir.X =  ballarray[ballcount].dir.Y;
                    ballarray[ballcount].dir.Y = -t;
                    ballarray[ballcount].visable = true;
                    p = false;
                }
                ballcount++;
                if (ballcount == ballarray.Length) ballcount = 0;
                i++;
                if (i == ballarray.Length) p = false;
            }



            if (timeS - timeI[0] == abilityInteval) ability1timer.Enabled = false;
            //this.Invalidate();
        }
        private void ability2timer_Tick(object sender, EventArgs e)
        {
            PointF t = new PointF();
            for (int i = 0; i < ballarray.Length; i++)
            {
                if (ballarray[i].visable)
                {
                    t.X = pt.X - ballarray[i].pos.X;
                    t.Y = pt.Y - ballarray[i].pos.Y;
                    t = UnitVector(t);
                    ballarray[i].dir.X += t.X * (float)(5 * (1 - 35 / distance(pt, ballarray[i].pos)));
                    ballarray[i].dir.Y += t.Y * (float)(5 * (1 - 35 / distance(pt, ballarray[i].pos)));
                    ballarray[i].dir = UnitVector(ballarray[i].dir);
                }
            }

            if (timeS - timeI[1] == abilityInteval) {
                ability2timer.Enabled = false;
            }
        }
        private void ability3timer_Tick(object sender, EventArgs e)
        {
            PointF t = new PointF();
            int n=0,s=0;
            double d = 1e9;
            for(int i = 0; i < squareArray.Length; i++)
            {
                if (d > distance(0, 0, squareArray[i].pos) && squareArray[i].visible && distance(0, 0, squareArray[i].pos) < 200)
                {
                    s++;
                    d = distance(0, 0, squareArray[i].pos);
                    n = i;
                }
            }
            if (s > 0)
            {
                for (int i = 0; i < ballarray.Length; i++)
                {
                    if (ballarray[i].visable)
                    {
                        t.X = squareArray[n].pos.X - ballarray[i].pos.X;
                        t.Y = squareArray[n].pos.Y - ballarray[i].pos.Y;
                        t = UnitVector(t);
                        t.X += ballarray[i].dir.X * 6;
                        t.Y += ballarray[i].dir.Y * 6;
                        ballarray[i].dir = UnitVector(t);
                    }
                }
            }
            if (timeS - timeI[2] == abilityInteval)
            {
                ability3timer.Enabled = false;
            }
        }
        private void ability4timer_Tick(object sender, EventArgs e)
        {
            if (timeS - timeI[3] == abilityInteval)
            {
                ability4timer.Enabled = false;
            }
        }
        private void ability5timer_Tick(object sender, EventArgs e)
        {
            PointF t = new PointF();

            if (wheelDelta > 0)
            {
                r[0] += 10;//r定義在最上面
            }
            else if (wheelDelta < 0)
            {
                r[0] -= 10;
                if (r[0] <= 20) r[0] = 20;
            }
            wheelDelta = 0;

            if (Control.MouseButtons == MouseButtons.Left || s)
            {
                BigBall.pos.X += BigBall.dir.X;
                BigBall.pos.Y += BigBall.dir.Y;               
                s = true;               
            }
            else
            {
                BigBall.pos.X = 1;
                BigBall.pos.Y = 1;
                BigBall.dir = UnitVector(pt);
                BigBall.dir.X *= (float)0.3;
                BigBall.dir.Y *= (float)0.3;
            }

            if (Control.MouseButtons == MouseButtons.Right) ability5timer.Enabled = false;


            for (int i = 0; i < ballarray.Length; i++)
            {
                if (ballarray[i].visable)
                {
                    //不能用(0,0)
                    t.X = ballarray[i].pos.X - BigBall.pos.X;
                    t.Y = ballarray[i].pos.Y - BigBall.pos.Y;
                    t = UnitVector(t);
                    ballarray[i].dir = RotateVector(t, 90);
                    ballarray[i].dir.X += -t.X * (float)(5 * (1 - r[0] / distance(BigBall.pos, ballarray[i].pos)));
                    ballarray[i].dir.Y += -t.Y * (float)(5 * (1 - r[0] / distance(BigBall.pos, ballarray[i].pos)));
                    ballarray[i].dir = UnitVector(ballarray[i].dir);
                }
            }

            BigBall.InRange(ctx, cty);
            if (timeS - timeI[4] == abilityInteval || !BigBall.visable)
            {
                s = false;
                ability5timer.Enabled = false;
            }
        }
        private void ability6timer_Tick(object sender, EventArgs e)
        {
            r[1] += 10;
            for(int i = 0; i < squareArray.Length; i++)
            {
                if (squareArray[i].visible) 
                {
                    if (Math.Abs(distance(squareArray[i].pos, circlearray[5].pos) - r[1]) < 25)
                    {
                        squareArray[i].count -= 3;
                        squareArray[i].length = 75;
                    }
                    else squareArray[i].length = 50;
                }
            }
            if (timeS - timeI[5] == 4)
            {
                ability6timer.Enabled = false;
            }
        }
        private void ability7timer_Tick(object sender, EventArgs e)
        {
            r[2] = (r[2] + 10) % (ctx+200) ;
            if (timeS - timeI[6] == abilityInteval)
            {
                ability7timer.Enabled = false;
            }
        }
        private void timerStart_Tick(object sender, EventArgs e)
        {
            PointF t = new PointF();

            if (wheelDelta > 0)
            {
                timer2.Interval -= 10;
                if (timer2.Interval <= 100) timer2.Interval = 100;
            }
            else if (wheelDelta < 0)
            {
                timer2.Interval += 20;
                if (timer2.Interval >= 1000) timer2.Interval = 1000;
            }
            wheelDelta = 0;

            for (int i = 0; i < ballarray.Length; i++)
            {
                if (distance(STButton.pos, ballarray[i].pos) <= (STBPicture.Height/2 + BallPicture.Height / 2))
                {
                    ballarray[i].Bounce(STButton.pos.X, STButton.pos.Y);
                    STButton.dir.X += (STButton.pos.X - ballarray[i].pos.X) / (float)10;
                    STButton.dir.Y += (STButton.pos.Y - ballarray[i].pos.Y) / (float)10;
                }
            }

            if (distance(0, 0, STButton.pos) > ctx * 3) STButton.pos = new Point(100, 100);
            if (distance(0, 0, STButton.dir) > 100) STButton.dir = new Point(1, 1);
            

            //右牆
            if (STButton.pos.X > ctx - STBPicture.Height / 2)
            {
                STButton.Bounce(STButton.pos.X + (STBPicture.Height / 2), STButton.pos.Y);
                STButton.pos.X += STButton.dir.X;
                STButton.pos.Y += STButton.dir.Y;
            }
            //左牆            
            if (STButton.pos.X < -1 * (ctx - STBPicture.Height / 2))
            {
                STButton.Bounce(STButton.pos.X - (STBPicture.Height / 2), STButton.pos.Y);
                STButton.pos.X += STButton.dir.X;
                STButton.pos.Y += STButton.dir.Y;
            }
            //下牆
            if (STButton.pos.Y > cty - STBPicture.Height / 2)
            {
                STButton.Bounce(STButton.pos.X, STButton.pos.Y + STBPicture.Height / 2);
                STButton.pos.X += STButton.dir.X;
                STButton.pos.Y += STButton.dir.Y;
            }
            //上牆
            if (STButton.pos.Y < -1 * (cty - STBPicture.Height / 2))
            {
                STButton.Bounce(STButton.pos.X, STButton.pos.Y - STBPicture.Height / 2);
                STButton.pos.X += STButton.dir.X;
                STButton.pos.Y += STButton.dir.Y;
            }
            

            if (Control.MouseButtons == MouseButtons.Left && distance(pt.X,pt.Y+cty, STButton.pos) < STBPicture.Height / 2)
            {
                STButton.dir = new PointF(0, 0);
                STButton.pos.X = pt.X;
                STButton.pos.Y = pt.Y + cty;  
                if (distance(0, 0, STButton.pos) <= 30)
                {
                    STButton.pos = new PointF(1, 1);
                    timerRun.Enabled = true;
                }

            }
            else if(!timerRun.Enabled)
            {
                STButton.pos.X += STButton.dir.X;
                STButton.pos.Y += STButton.dir.Y;
                STButton.dir.X *= (float)0.99;
                STButton.dir.Y *= (float)0.99;
                if (distance(0, 0, STButton.pos) <= STBPicture.Width)
                {
                    STButton.Bounce(0, 0);
                }
                if (distance(0, 0, STButton.pos) <= STBPicture.Width + 20)
                {
                    STButton.dir.X *= (float)0.95;
                    STButton.dir.Y *= (float)0.95;
                }
            }

            if (timerRun.Enabled)
            {
                timer2.Interval = 100;
                for (int i = 0; i < ballarray.Length; i++)
                {
                    if (ballarray[i].visable)
                    {
                        //不能用(0,0)
                        t.X = ballarray[i].pos.X - 1;
                        t.Y = ballarray[i].pos.Y - 1;
                        t = UnitVector(t);
                        ballarray[i].dir = RotateVector(t, 90);
                        ballarray[i].dir.X += -t.X * (float)(5 * (1 - 50 / distance(1,1, ballarray[i].pos)));
                        ballarray[i].dir.Y += -t.Y * (float)(5 * (1 - 50 / distance(1,1, ballarray[i].pos)));
                        ballarray[i].dir = UnitVector(ballarray[i].dir);
                    }
                }
            }

            if (timeS >= 1)
            {
                label1.Visible = false;
            }

            if (timeS >= 3)
            {
                Cursor.Hide();
                score = 0;
                level = 5;
                label1.Visible = true;
                timerStart.Enabled = false;
                timerOfS.Enabled = true;
                timerSqSpeed.Enabled = true;
                timer2.Interval = 500;
                STButton.pos = new PointF(100, 100);
                drawC = true;
                timeS = 0;
            }
            this.Invalidate();
        }
        private void timerEnd_Tick(object sender, EventArgs e)
        {
            if (timeS - timeI[7] >= 5)
            {
                ResetAll();
                timeS = 0;
                Cursor.Show();              
                timerRun.Enabled = false;
                timerStart.Enabled = true;
                timer1.Enabled = true;
                timer2.Enabled = true;
                timer2.Interval = ballInterval;
                drawC = false;
                timerEnd.Enabled = false;
            }
            this.Invalidate();
        }



        public PointF RotateVector(PointF v,double angle)
        {
            PointF ndir = new PointF();
            angle = angle * Math.PI / 180;
            ndir.X = (float)(Math.Cos(angle) * v.X - Math.Sin(angle) * v.Y);
            ndir.Y = (float)(Math.Sin(angle) * v.X + Math.Cos(angle) * v.Y);
            return ndir;
        }
        public PointF UnitVector(Point v)
        {
            PointF p0 = new PointF();
            (p0.X) = 10*(float)(v.X / Math.Sqrt(v.X * v.X + v.Y * v.Y));
            (p0.Y) = 10*(float)(v.Y / Math.Sqrt(v.X * v.X + v.Y * v.Y));
            return p0;
        }
        public PointF UnitVector(PointF v)
        {
            PointF p0 = new PointF();
            (p0.X) = 10 * (float)(v.X / Math.Sqrt(v.X * v.X + v.Y * v.Y));
            (p0.Y) = 10 * (float)(v.Y / Math.Sqrt(v.X * v.X + v.Y * v.Y));
            return p0;
        }
        public double distance(PointF a,PointF b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
        public double distance(float ax,float ay ,PointF b)
        {
            return Math.Sqrt(Math.Pow(ax - b.X, 2) + Math.Pow(ay - b.Y, 2));
        }
        public float angle(Point v)
        {
            float angle;
            if (v.X == 0) v.X = 1 ;
            angle = (float)(Math.Atan2(v.Y , v.X)*180/Math.PI);
            return angle;
        }

        
    }
}

