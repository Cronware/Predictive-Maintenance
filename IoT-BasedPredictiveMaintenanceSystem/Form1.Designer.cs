namespace IoT_BasedPredictiveMaintenanceSystem
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
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonStartMonitoring = new System.Windows.Forms.Button();
            this.dataGridViewSensors = new System.Windows.Forms.DataGridView();
            this.cartesianChartSensors = new LiveCharts.WinForms.CartesianChart();
            this.labelStatus = new System.Windows.Forms.Label();
            this.timerData = new System.Windows.Forms.Timer(this.components);
            this.buttonViewReports = new System.Windows.Forms.Button();
            this.buttonOpenDashboard = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSensors)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(12, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(97, 39);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonStartMonitoring
            // 
            this.buttonStartMonitoring.Location = new System.Drawing.Point(115, 12);
            this.buttonStartMonitoring.Name = "buttonStartMonitoring";
            this.buttonStartMonitoring.Size = new System.Drawing.Size(97, 39);
            this.buttonStartMonitoring.TabIndex = 1;
            this.buttonStartMonitoring.Text = "Start Monitoring";
            this.buttonStartMonitoring.UseVisualStyleBackColor = true;
            this.buttonStartMonitoring.Click += new System.EventHandler(this.buttonStartMonitoring_Click);
            // 
            // dataGridViewSensors
            // 
            this.dataGridViewSensors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSensors.Location = new System.Drawing.Point(12, 57);
            this.dataGridViewSensors.Name = "dataGridViewSensors";
            this.dataGridViewSensors.Size = new System.Drawing.Size(333, 381);
            this.dataGridViewSensors.TabIndex = 2;
            // 
            // cartesianChartSensors
            // 
            this.cartesianChartSensors.Location = new System.Drawing.Point(351, 57);
            this.cartesianChartSensors.Name = "cartesianChartSensors";
            this.cartesianChartSensors.Size = new System.Drawing.Size(437, 381);
            this.cartesianChartSensors.TabIndex = 3;
            this.cartesianChartSensors.Text = "cartesianChart1";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(662, 20);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(13, 20);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "/";
            // 
            // timerData
            // 
            this.timerData.Interval = 1000;
            this.timerData.Tick += new System.EventHandler(this.timerData_Tick);
            // 
            // buttonViewReports
            // 
            this.buttonViewReports.Location = new System.Drawing.Point(218, 12);
            this.buttonViewReports.Name = "buttonViewReports";
            this.buttonViewReports.Size = new System.Drawing.Size(97, 39);
            this.buttonViewReports.TabIndex = 5;
            this.buttonViewReports.Text = "View Reports";
            this.buttonViewReports.UseVisualStyleBackColor = true;
            this.buttonViewReports.Click += new System.EventHandler(this.buttonViewReports_Click);
            // 
            // buttonOpenDashboard
            // 
            this.buttonOpenDashboard.Location = new System.Drawing.Point(321, 12);
            this.buttonOpenDashboard.Name = "buttonOpenDashboard";
            this.buttonOpenDashboard.Size = new System.Drawing.Size(97, 39);
            this.buttonOpenDashboard.TabIndex = 6;
            this.buttonOpenDashboard.Text = "Open Dashboard";
            this.buttonOpenDashboard.UseVisualStyleBackColor = true;
            this.buttonOpenDashboard.Click += new System.EventHandler(this.buttonOpenDashboard_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonOpenDashboard);
            this.Controls.Add(this.buttonViewReports);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.cartesianChartSensors);
            this.Controls.Add(this.dataGridViewSensors);
            this.Controls.Add(this.buttonStartMonitoring);
            this.Controls.Add(this.buttonConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Predictive Maintenance";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSensors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonStartMonitoring;
        private System.Windows.Forms.DataGridView dataGridViewSensors;
        private LiveCharts.WinForms.CartesianChart cartesianChartSensors;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Timer timerData;
        private System.Windows.Forms.Button buttonViewReports;
        private System.Windows.Forms.Button buttonOpenDashboard;
    }
}

