namespace LoadTestProgram
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
            this.btnStart = new System.Windows.Forms.Button();
            this.lNoOfVirtualUser = new System.Windows.Forms.Label();
            this.tbNoOfVirtualUser = new System.Windows.Forms.TextBox();
            this.lUserPerIteration = new System.Windows.Forms.Label();
            this.tbUserPerIteration = new System.Windows.Forms.TextBox();
            this.lbStartAfter = new System.Windows.Forms.Label();
            this.lbSec = new System.Windows.Forms.Label();
            this.tbDeferStartTime = new System.Windows.Forms.TextBox();
            this.cbRepeatTask = new System.Windows.Forms.CheckBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.tbLiveMessageBoard = new System.Windows.Forms.TextBox();
            this.lbUserTaskRatio = new System.Windows.Forms.Label();
            this.tbUserTaskRatio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbThreadSleepTime = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(140, 368);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(181, 43);
            this.btnStart.TabIndex = 31;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lNoOfVirtualUser
            // 
            this.lNoOfVirtualUser.AutoSize = true;
            this.lNoOfVirtualUser.Location = new System.Drawing.Point(26, 28);
            this.lNoOfVirtualUser.Name = "lNoOfVirtualUser";
            this.lNoOfVirtualUser.Size = new System.Drawing.Size(142, 20);
            this.lNoOfVirtualUser.TabIndex = 1;
            this.lNoOfVirtualUser.Text = "No. of Virtual User:";
            // 
            // tbNoOfVirtualUser
            // 
            this.tbNoOfVirtualUser.Location = new System.Drawing.Point(195, 28);
            this.tbNoOfVirtualUser.Name = "tbNoOfVirtualUser";
            this.tbNoOfVirtualUser.Size = new System.Drawing.Size(69, 26);
            this.tbNoOfVirtualUser.TabIndex = 2;
            // 
            // lUserPerIteration
            // 
            this.lUserPerIteration.AutoSize = true;
            this.lUserPerIteration.Location = new System.Drawing.Point(26, 62);
            this.lUserPerIteration.Name = "lUserPerIteration";
            this.lUserPerIteration.Size = new System.Drawing.Size(137, 20);
            this.lUserPerIteration.TabIndex = 3;
            this.lUserPerIteration.Text = "User per Iteration:";
            // 
            // tbUserPerIteration
            // 
            this.tbUserPerIteration.Location = new System.Drawing.Point(195, 62);
            this.tbUserPerIteration.Name = "tbUserPerIteration";
            this.tbUserPerIteration.Size = new System.Drawing.Size(69, 26);
            this.tbUserPerIteration.TabIndex = 5;
            // 
            // lbStartAfter
            // 
            this.lbStartAfter.AutoSize = true;
            this.lbStartAfter.Location = new System.Drawing.Point(283, 67);
            this.lbStartAfter.Name = "lbStartAfter";
            this.lbStartAfter.Size = new System.Drawing.Size(85, 20);
            this.lbStartAfter.TabIndex = 11;
            this.lbStartAfter.Text = "Start after ";
            // 
            // lbSec
            // 
            this.lbSec.AutoSize = true;
            this.lbSec.Location = new System.Drawing.Point(436, 68);
            this.lbSec.Name = "lbSec";
            this.lbSec.Size = new System.Drawing.Size(59, 20);
            this.lbSec.TabIndex = 12;
            this.lbSec.Text = "Sec.(s)";
            // 
            // tbDeferStartTime
            // 
            this.tbDeferStartTime.Location = new System.Drawing.Point(364, 63);
            this.tbDeferStartTime.Name = "tbDeferStartTime";
            this.tbDeferStartTime.Size = new System.Drawing.Size(73, 26);
            this.tbDeferStartTime.TabIndex = 13;
            // 
            // cbRepeatTask
            // 
            this.cbRepeatTask.AutoSize = true;
            this.cbRepeatTask.Location = new System.Drawing.Point(30, 98);
            this.cbRepeatTask.Name = "cbRepeatTask";
            this.cbRepeatTask.Size = new System.Drawing.Size(126, 24);
            this.cbRepeatTask.TabIndex = 23;
            this.cbRepeatTask.Text = "Repeat Task";
            this.cbRepeatTask.UseVisualStyleBackColor = true;
            this.cbRepeatTask.CheckedChanged += new System.EventHandler(this.cbRepeatTask_CheckedChanged);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(327, 368);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(181, 43);
            this.btnStop.TabIndex = 32;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tbLiveMessageBoard
            // 
            this.tbLiveMessageBoard.Location = new System.Drawing.Point(30, 173);
            this.tbLiveMessageBoard.Multiline = true;
            this.tbLiveMessageBoard.Name = "tbLiveMessageBoard";
            this.tbLiveMessageBoard.ReadOnly = true;
            this.tbLiveMessageBoard.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLiveMessageBoard.Size = new System.Drawing.Size(598, 176);
            this.tbLiveMessageBoard.TabIndex = 30;
            // 
            // lbUserTaskRatio
            // 
            this.lbUserTaskRatio.Location = new System.Drawing.Point(26, 131);
            this.lbUserTaskRatio.Name = "lbUserTaskRatio";
            this.lbUserTaskRatio.Size = new System.Drawing.Size(257, 40);
            this.lbUserTaskRatio.TabIndex = 29;
            this.lbUserTaskRatio.Text = "No. of user  for each iteration task (e.g. 10/20/30 )";
            // 
            // tbUserTaskRatio
            // 
            this.tbUserTaskRatio.Location = new System.Drawing.Point(289, 131);
            this.tbUserTaskRatio.Name = "tbUserTaskRatio";
            this.tbUserTaskRatio.Size = new System.Drawing.Size(339, 26);
            this.tbUserTaskRatio.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(284, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 20);
            this.label1.TabIndex = 33;
            this.label1.Text = "Thread sleep time of each task (sec):";
            // 
            // tbThreadSleepTime
            // 
            this.tbThreadSleepTime.Enabled = false;
            this.tbThreadSleepTime.Location = new System.Drawing.Point(565, 96);
            this.tbThreadSleepTime.Name = "tbThreadSleepTime";
            this.tbThreadSleepTime.Size = new System.Drawing.Size(62, 26);
            this.tbThreadSleepTime.TabIndex = 27;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 430);
            this.Controls.Add(this.tbThreadSleepTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbUserTaskRatio);
            this.Controls.Add(this.lbUserTaskRatio);
            this.Controls.Add(this.tbLiveMessageBoard);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.cbRepeatTask);
            this.Controls.Add(this.tbDeferStartTime);
            this.Controls.Add(this.lbSec);
            this.Controls.Add(this.lbStartAfter);
            this.Controls.Add(this.tbUserPerIteration);
            this.Controls.Add(this.lUserPerIteration);
            this.Controls.Add(this.tbNoOfVirtualUser);
            this.Controls.Add(this.lNoOfVirtualUser);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Load Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lNoOfVirtualUser;
        private System.Windows.Forms.TextBox tbNoOfVirtualUser;
        private System.Windows.Forms.Label lUserPerIteration;
        private System.Windows.Forms.TextBox tbUserPerIteration;
        private System.Windows.Forms.Label lbStartAfter;
        private System.Windows.Forms.Label lbSec;
        private System.Windows.Forms.TextBox tbDeferStartTime;
        private System.Windows.Forms.CheckBox cbRepeatTask;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox tbLiveMessageBoard;
        private System.Windows.Forms.Label lbUserTaskRatio;
        private System.Windows.Forms.TextBox tbUserTaskRatio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbThreadSleepTime;
    }
}

