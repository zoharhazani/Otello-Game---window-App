namespace OthelloGameUI
{
    partial class OthelloGameSetting
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
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.buttonPlayAgainstTheComputer = new System.Windows.Forms.Button();
            this.buttonPlayAgainstYourFriend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.Location = new System.Drawing.Point(62, 50);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(624, 90);
            this.buttonBoardSize.TabIndex = 2;
            this.buttonBoardSize.Text = "Board Size : 6X6 (click to increase)";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // buttonPlayAgainstTheComputer
            // 
            this.buttonPlayAgainstTheComputer.Location = new System.Drawing.Point(62, 216);
            this.buttonPlayAgainstTheComputer.Name = "buttonPlayAgainstTheComputer";
            this.buttonPlayAgainstTheComputer.Size = new System.Drawing.Size(273, 81);
            this.buttonPlayAgainstTheComputer.TabIndex = 3;
            this.buttonPlayAgainstTheComputer.Text = "Play against the computer";
            this.buttonPlayAgainstTheComputer.UseVisualStyleBackColor = true;
            this.buttonPlayAgainstTheComputer.Click += new System.EventHandler(this.buttonPlayAgainstTheComputer_Click);
            // 
            // buttonPlayAgainstYourFriend
            // 
            this.buttonPlayAgainstYourFriend.Location = new System.Drawing.Point(413, 216);
            this.buttonPlayAgainstYourFriend.Name = "buttonPlayAgainstYourFriend";
            this.buttonPlayAgainstYourFriend.Size = new System.Drawing.Size(273, 81);
            this.buttonPlayAgainstYourFriend.TabIndex = 4;
            this.buttonPlayAgainstYourFriend.Text = "Play against your friend";
            this.buttonPlayAgainstYourFriend.UseVisualStyleBackColor = true;
            this.buttonPlayAgainstYourFriend.Click += new System.EventHandler(this.buttonPlayAgainstYourFriend_Click);
            // 
            // OthelloGameSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 322);
            this.Controls.Add(this.buttonPlayAgainstYourFriend);
            this.Controls.Add(this.buttonPlayAgainstTheComputer);
            this.Controls.Add(this.buttonBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OthelloGameSetting";
            this.Text = "Othello - Game Setting";
            this.Load += new System.EventHandler(this.OthelloGameSetting_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonBoardSize;
        private System.Windows.Forms.Button buttonPlayAgainstTheComputer;
        private System.Windows.Forms.Button buttonPlayAgainstYourFriend;
    }
}