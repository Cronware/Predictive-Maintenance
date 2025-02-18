namespace IoT_BasedPredictiveMaintenanceSystem
{
    partial class DashboardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardForm));
            this.labelTotalReadings = new System.Windows.Forms.Label();
            this.labelFailuresDetected = new System.Windows.Forms.Label();
            this.labelLastAnomaly = new System.Windows.Forms.Label();
            this.buttonRefreshStats = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTotalReadings
            // 
            this.labelTotalReadings.AutoSize = true;
            this.labelTotalReadings.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalReadings.Location = new System.Drawing.Point(43, 9);
            this.labelTotalReadings.Name = "labelTotalReadings";
            this.labelTotalReadings.Size = new System.Drawing.Size(205, 31);
            this.labelTotalReadings.TabIndex = 0;
            this.labelTotalReadings.Text = "Total Readings:";
            // 
            // labelFailuresDetected
            // 
            this.labelFailuresDetected.AutoSize = true;
            this.labelFailuresDetected.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFailuresDetected.Location = new System.Drawing.Point(12, 56);
            this.labelFailuresDetected.Name = "labelFailuresDetected";
            this.labelFailuresDetected.Size = new System.Drawing.Size(236, 31);
            this.labelFailuresDetected.TabIndex = 1;
            this.labelFailuresDetected.Text = "Failures Detected:";
            // 
            // labelLastAnomaly
            // 
            this.labelLastAnomaly.AutoSize = true;
            this.labelLastAnomaly.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastAnomaly.Location = new System.Drawing.Point(62, 107);
            this.labelLastAnomaly.Name = "labelLastAnomaly";
            this.labelLastAnomaly.Size = new System.Drawing.Size(186, 31);
            this.labelLastAnomaly.TabIndex = 2;
            this.labelLastAnomaly.Text = "Last Anomaly:";
            // 
            // buttonRefreshStats
            // 
            this.buttonRefreshStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefreshStats.Location = new System.Drawing.Point(12, 149);
            this.buttonRefreshStats.Name = "buttonRefreshStats";
            this.buttonRefreshStats.Size = new System.Drawing.Size(506, 54);
            this.buttonRefreshStats.TabIndex = 3;
            this.buttonRefreshStats.Text = "Refresh Statistics";
            this.buttonRefreshStats.UseVisualStyleBackColor = true;
            this.buttonRefreshStats.Click += new System.EventHandler(this.buttonRefreshStats_Click);
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 207);
            this.Controls.Add(this.buttonRefreshStats);
            this.Controls.Add(this.labelLastAnomaly);
            this.Controls.Add(this.labelFailuresDetected);
            this.Controls.Add(this.labelTotalReadings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DashboardForm";
            this.Text = "Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTotalReadings;
        private System.Windows.Forms.Label labelFailuresDetected;
        private System.Windows.Forms.Label labelLastAnomaly;
        private System.Windows.Forms.Button buttonRefreshStats;
    }
}