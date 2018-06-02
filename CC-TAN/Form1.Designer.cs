namespace CC_TAN
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timerOfS = new System.Windows.Forms.Timer(this.components);
            this.timerSqSpeed = new System.Windows.Forms.Timer(this.components);
            this.ability1timer = new System.Windows.Forms.Timer(this.components);
            this.timerRun = new System.Windows.Forms.Timer(this.components);
            this.ability2timer = new System.Windows.Forms.Timer(this.components);
            this.ability3timer = new System.Windows.Forms.Timer(this.components);
            this.ability4timer = new System.Windows.Forms.Timer(this.components);
            this.ability5timer = new System.Windows.Forms.Timer(this.components);
            this.cursortimer = new System.Windows.Forms.Timer(this.components);
            this.ability6timer = new System.Windows.Forms.Timer(this.components);
            this.ability7timer = new System.Windows.Forms.Timer(this.components);
            this.timerStart = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.timerEnd = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timerOfS
            // 
            this.timerOfS.Enabled = true;
            this.timerOfS.Interval = 1000;
            this.timerOfS.Tick += new System.EventHandler(this.timerOfS_Tick);
            // 
            // timerSqSpeed
            // 
            this.timerSqSpeed.Enabled = true;
            this.timerSqSpeed.Tick += new System.EventHandler(this.SqSpeed_Tick);
            // 
            // ability1timer
            // 
            this.ability1timer.Tick += new System.EventHandler(this.ability1timer_Tick);
            // 
            // timerRun
            // 
            this.timerRun.Enabled = true;
            this.timerRun.Interval = 1000;
            this.timerRun.Tick += new System.EventHandler(this.timerRun_Tick);
            // 
            // ability2timer
            // 
            this.ability2timer.Tick += new System.EventHandler(this.ability2timer_Tick);
            // 
            // ability3timer
            // 
            this.ability3timer.Tick += new System.EventHandler(this.ability3timer_Tick);
            // 
            // ability4timer
            // 
            this.ability4timer.Tick += new System.EventHandler(this.ability4timer_Tick);
            // 
            // ability5timer
            // 
            this.ability5timer.Tick += new System.EventHandler(this.ability5timer_Tick);
            // 
            // cursortimer
            // 
            this.cursortimer.Enabled = true;
            this.cursortimer.Interval = 10;
            this.cursortimer.Tick += new System.EventHandler(this.cursortimer_Tick);
            // 
            // ability6timer
            // 
            this.ability6timer.Tick += new System.EventHandler(this.ability6timer_Tick);
            // 
            // ability7timer
            // 
            this.ability7timer.Tick += new System.EventHandler(this.ability7timer_Tick);
            // 
            // timerStart
            // 
            this.timerStart.Enabled = true;
            this.timerStart.Tick += new System.EventHandler(this.timerStart_Tick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(384, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(482, 112);
            this.label1.TabIndex = 0;
            this.label1.Text = "CC-TAN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerEnd
            // 
            this.timerEnd.Tick += new System.EventHandler(this.timerEnd_Tick);
           
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe Print", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(216, 463);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(813, 112);
            this.label3.TabIndex = 2;
            this.label3.Text = "GAME OVER";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1274, 1229);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "CC-TAN";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseWheel);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timerOfS;
        private System.Windows.Forms.Timer timerSqSpeed;
        private System.Windows.Forms.Timer ability1timer;
        private System.Windows.Forms.Timer timerRun;
        private System.Windows.Forms.Timer ability2timer;
        private System.Windows.Forms.Timer ability3timer;
        private System.Windows.Forms.Timer ability4timer;
        private System.Windows.Forms.Timer ability5timer;
        private System.Windows.Forms.Timer cursortimer;
        private System.Windows.Forms.Timer ability6timer;
        private System.Windows.Forms.Timer ability7timer;
        private System.Windows.Forms.Timer timerStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerEnd;
        private System.Windows.Forms.Label label3;
    }
}

