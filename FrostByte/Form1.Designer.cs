namespace FrostByte
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.tmIntro = new System.Windows.Forms.Timer(this.components);
			this.lbContinue = new System.Windows.Forms.Label();
			this.tmGame = new System.Windows.Forms.Timer(this.components);
			this.tmPause = new System.Windows.Forms.Timer(this.components);
			this.tmEnd = new System.Windows.Forms.Timer(this.components);
			this.lbHeart = new System.Windows.Forms.Label();
			this.lbFriend = new System.Windows.Forms.Label();
			this.lbBullet = new System.Windows.Forms.Label();
			this.lbRepeat = new System.Windows.Forms.Label();
			this.lbPause = new System.Windows.Forms.Label();
			this.lbLevels = new System.Windows.Forms.Label();
			this.pbBullet = new System.Windows.Forms.PictureBox();
			this.pbHeart = new System.Windows.Forms.PictureBox();
			this.pbFriend = new System.Windows.Forms.PictureBox();
			this.pbIntro = new System.Windows.Forms.PictureBox();
			this.lbWinLose = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbBullet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbHeart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbFriend)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbIntro)).BeginInit();
			this.SuspendLayout();
			// 
			// tmIntro
			// 
			this.tmIntro.Tick += new System.EventHandler(this.tmIntro_Tick);
			// 
			// lbContinue
			// 
			this.lbContinue.AutoSize = true;
			this.lbContinue.Font = new System.Drawing.Font("Maiandra GD", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbContinue.ForeColor = System.Drawing.Color.Cyan;
			this.lbContinue.Location = new System.Drawing.Point(105, 413);
			this.lbContinue.Name = "lbContinue";
			this.lbContinue.Size = new System.Drawing.Size(666, 77);
			this.lbContinue.TabIndex = 1;
			this.lbContinue.Text = "Press Enter to continue";
			// 
			// tmGame
			// 
			this.tmGame.Tick += new System.EventHandler(this.tmGame_Tick);
			// 
			// tmPause
			// 
			this.tmPause.Tick += new System.EventHandler(this.tmPause_Tick);
			// 
			// tmEnd
			// 
			this.tmEnd.Tick += new System.EventHandler(this.tmEnd_Tick);
			// 
			// lbHeart
			// 
			this.lbHeart.AutoSize = true;
			this.lbHeart.Font = new System.Drawing.Font("Maiandra GD", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbHeart.ForeColor = System.Drawing.Color.Red;
			this.lbHeart.Location = new System.Drawing.Point(815, 15);
			this.lbHeart.Name = "lbHeart";
			this.lbHeart.Size = new System.Drawing.Size(33, 42);
			this.lbHeart.TabIndex = 5;
			this.lbHeart.Text = "?";
			// 
			// lbFriend
			// 
			this.lbFriend.AutoSize = true;
			this.lbFriend.Font = new System.Drawing.Font("Maiandra GD", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbFriend.ForeColor = System.Drawing.Color.Yellow;
			this.lbFriend.Location = new System.Drawing.Point(815, 62);
			this.lbFriend.Name = "lbFriend";
			this.lbFriend.Size = new System.Drawing.Size(33, 42);
			this.lbFriend.TabIndex = 6;
			this.lbFriend.Text = "?";
			// 
			// lbBullet
			// 
			this.lbBullet.AutoSize = true;
			this.lbBullet.Font = new System.Drawing.Font("Maiandra GD", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbBullet.ForeColor = System.Drawing.Color.Cyan;
			this.lbBullet.Location = new System.Drawing.Point(815, 109);
			this.lbBullet.Name = "lbBullet";
			this.lbBullet.Size = new System.Drawing.Size(33, 42);
			this.lbBullet.TabIndex = 7;
			this.lbBullet.Text = "?";
			// 
			// lbRepeat
			// 
			this.lbRepeat.AutoSize = true;
			this.lbRepeat.Font = new System.Drawing.Font("Maiandra GD", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbRepeat.ForeColor = System.Drawing.Color.Cyan;
			this.lbRepeat.Location = new System.Drawing.Point(144, 331);
			this.lbRepeat.Name = "lbRepeat";
			this.lbRepeat.Size = new System.Drawing.Size(601, 77);
			this.lbRepeat.TabIndex = 8;
			this.lbRepeat.Text = "Press Enter to repeat";
			// 
			// lbPause
			// 
			this.lbPause.AutoSize = true;
			this.lbPause.Font = new System.Drawing.Font("Maiandra GD", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbPause.ForeColor = System.Drawing.Color.White;
			this.lbPause.Location = new System.Drawing.Point(297, 244);
			this.lbPause.Name = "lbPause";
			this.lbPause.Size = new System.Drawing.Size(281, 115);
			this.lbPause.TabIndex = 9;
			this.lbPause.Text = "Pause";
			// 
			// lbLevels
			// 
			this.lbLevels.AutoSize = true;
			this.lbLevels.Font = new System.Drawing.Font("Maiandra GD", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbLevels.ForeColor = System.Drawing.Color.White;
			this.lbLevels.Location = new System.Drawing.Point(775, 540);
			this.lbLevels.Name = "lbLevels";
			this.lbLevels.Size = new System.Drawing.Size(33, 42);
			this.lbLevels.TabIndex = 10;
			this.lbLevels.Text = "?";
			// 
			// pbBullet
			// 
			this.pbBullet.Image = global::FrostByte.Properties.Resources.Bullet;
			this.pbBullet.Location = new System.Drawing.Point(771, 115);
			this.pbBullet.Name = "pbBullet";
			this.pbBullet.Size = new System.Drawing.Size(30, 30);
			this.pbBullet.TabIndex = 4;
			this.pbBullet.TabStop = false;
			// 
			// pbHeart
			// 
			this.pbHeart.Image = global::FrostByte.Properties.Resources.Heart;
			this.pbHeart.Location = new System.Drawing.Point(771, 21);
			this.pbHeart.Name = "pbHeart";
			this.pbHeart.Size = new System.Drawing.Size(30, 30);
			this.pbHeart.TabIndex = 3;
			this.pbHeart.TabStop = false;
			// 
			// pbFriend
			// 
			this.pbFriend.Image = global::FrostByte.Properties.Resources.Friend;
			this.pbFriend.Location = new System.Drawing.Point(771, 67);
			this.pbFriend.Name = "pbFriend";
			this.pbFriend.Size = new System.Drawing.Size(30, 30);
			this.pbFriend.TabIndex = 2;
			this.pbFriend.TabStop = false;
			// 
			// pbIntro
			// 
			this.pbIntro.Image = global::FrostByte.Properties.Resources.Intro;
			this.pbIntro.Location = new System.Drawing.Point(295, 130);
			this.pbIntro.Name = "pbIntro";
			this.pbIntro.Size = new System.Drawing.Size(279, 216);
			this.pbIntro.TabIndex = 0;
			this.pbIntro.TabStop = false;
			// 
			// lbWinLose
			// 
			this.lbWinLose.AutoSize = true;
			this.lbWinLose.Font = new System.Drawing.Font("Maiandra GD", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbWinLose.ForeColor = System.Drawing.Color.Red;
			this.lbWinLose.Location = new System.Drawing.Point(290, 195);
			this.lbWinLose.Name = "lbWinLose";
			this.lbWinLose.Size = new System.Drawing.Size(77, 77);
			this.lbWinLose.TabIndex = 11;
			this.lbWinLose.Text = "?!";
			// 
			// Form1
			// 
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(864, 601);
			this.Controls.Add(this.lbWinLose);
			this.Controls.Add(this.lbLevels);
			this.Controls.Add(this.lbPause);
			this.Controls.Add(this.lbRepeat);
			this.Controls.Add(this.lbBullet);
			this.Controls.Add(this.lbFriend);
			this.Controls.Add(this.lbHeart);
			this.Controls.Add(this.pbBullet);
			this.Controls.Add(this.pbHeart);
			this.Controls.Add(this.pbFriend);
			this.Controls.Add(this.lbContinue);
			this.Controls.Add(this.pbIntro);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pbBullet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbHeart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbFriend)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbIntro)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbIntro;
        private System.Windows.Forms.PictureBox pbFriend;
        private System.Windows.Forms.PictureBox pbHeart;
        private System.Windows.Forms.PictureBox pbBullet;

        private System.Windows.Forms.Label lbContinue;
        private System.Windows.Forms.Label lbHeart;
        private System.Windows.Forms.Label lbFriend;
        private System.Windows.Forms.Label lbBullet;
        private System.Windows.Forms.Label lbRepeat;
        private System.Windows.Forms.Label lbPause;
        private System.Windows.Forms.Label lbLevels;
		private System.Windows.Forms.Label lbWinLose;

		private System.Windows.Forms.Timer tmIntro;
        private System.Windows.Forms.Timer tmGame;
        private System.Windows.Forms.Timer tmPause;
        private System.Windows.Forms.Timer tmEnd;
	}
}
