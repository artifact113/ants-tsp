namespace AntsTSP
{
    partial class FInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FInput));
            this._gbOutput = new System.Windows.Forms.GroupBox();
            this._lblTime = new System.Windows.Forms.Label();
            this._gbAvrg = new System.Windows.Forms.GroupBox();
            this._tbAVRIter = new System.Windows.Forms.TextBox();
            this._tbAVRGlob = new System.Windows.Forms.TextBox();
            this._lblAVRIter = new System.Windows.Forms.Label();
            this._lblAVRGlob = new System.Windows.Forms.Label();
            this._gbBest = new System.Windows.Forms.GroupBox();
            this._tbBestIter = new System.Windows.Forms.TextBox();
            this._tbBestGlob = new System.Windows.Forms.TextBox();
            this._lblBestIter = new System.Windows.Forms.Label();
            this._lblBestGlob = new System.Windows.Forms.Label();
            this._lblCityCount = new System.Windows.Forms.Label();
            this._gbConfig = new System.Windows.Forms.GroupBox();
            this._lblMinTourLength = new System.Windows.Forms.Label();
            this._lblOptTourLength = new System.Windows.Forms.Label();
            this._tbMinTour = new System.Windows.Forms.TextBox();
            this._tbOptTour = new System.Windows.Forms.TextBox();
            this._btnStart = new System.Windows.Forms.Button();
            this._tbQ = new System.Windows.Forms.TextBox();
            this._lblQ = new System.Windows.Forms.Label();
            this._tbTau = new System.Windows.Forms.TextBox();
            this._lblTau = new System.Windows.Forms.Label();
            this._tbRho = new System.Windows.Forms.TextBox();
            this._lblRho = new System.Windows.Forms.Label();
            this._tbBeta = new System.Windows.Forms.TextBox();
            this._lblBeta = new System.Windows.Forms.Label();
            this._tbAlpha = new System.Windows.Forms.TextBox();
            this._lblAlpha = new System.Windows.Forms.Label();
            this._tbIterationCount = new System.Windows.Forms.TextBox();
            this._lblCountIterations = new System.Windows.Forms.Label();
            this._tbAntCount = new System.Windows.Forms.TextBox();
            this._lblAntCount = new System.Windows.Forms.Label();
            this._ttInputTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._smiTSPLadenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tODOErgsSpeichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._lblIterCount = new System.Windows.Forms.Label();
            this._gbOutput.SuspendLayout();
            this._gbAvrg.SuspendLayout();
            this._gbBest.SuspendLayout();
            this._gbConfig.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _gbOutput
            // 
            this._gbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._gbOutput.Controls.Add(this._lblIterCount);
            this._gbOutput.Controls.Add(this._lblTime);
            this._gbOutput.Controls.Add(this._gbAvrg);
            this._gbOutput.Controls.Add(this._gbBest);
            this._gbOutput.Controls.Add(this._lblCityCount);
            this._gbOutput.Location = new System.Drawing.Point(12, 37);
            this._gbOutput.Name = "_gbOutput";
            this._gbOutput.Size = new System.Drawing.Size(510, 145);
            this._gbOutput.TabIndex = 0;
            this._gbOutput.TabStop = false;
            this._gbOutput.Text = "Ausgabe";
            // 
            // _lblTime
            // 
            this._lblTime.AutoSize = true;
            this._lblTime.Location = new System.Drawing.Point(6, 116);
            this._lblTime.Name = "_lblTime";
            this._lblTime.Size = new System.Drawing.Size(30, 15);
            this._lblTime.TabIndex = 1;
            this._lblTime.Text = "Zeit:";
            // 
            // _gbAvrg
            // 
            this._gbAvrg.Controls.Add(this._tbAVRIter);
            this._gbAvrg.Controls.Add(this._tbAVRGlob);
            this._gbAvrg.Controls.Add(this._lblAVRIter);
            this._gbAvrg.Controls.Add(this._lblAVRGlob);
            this._gbAvrg.Location = new System.Drawing.Point(264, 16);
            this._gbAvrg.Name = "_gbAvrg";
            this._gbAvrg.Size = new System.Drawing.Size(240, 87);
            this._gbAvrg.TabIndex = 0;
            this._gbAvrg.TabStop = false;
            this._gbAvrg.Text = "Durschnitt";
            // 
            // _tbAVRIter
            // 
            this._tbAVRIter.Location = new System.Drawing.Point(115, 47);
            this._tbAVRIter.Name = "_tbAVRIter";
            this._tbAVRIter.ReadOnly = true;
            this._tbAVRIter.Size = new System.Drawing.Size(98, 20);
            this._tbAVRIter.TabIndex = 6;
            // 
            // _tbAVRGlob
            // 
            this._tbAVRGlob.Location = new System.Drawing.Point(115, 20);
            this._tbAVRGlob.Name = "_tbAVRGlob";
            this._tbAVRGlob.ReadOnly = true;
            this._tbAVRGlob.Size = new System.Drawing.Size(98, 20);
            this._tbAVRGlob.TabIndex = 7;
            // 
            // _lblAVRIter
            // 
            this._lblAVRIter.AutoSize = true;
            this._lblAVRIter.Location = new System.Drawing.Point(27, 50);
            this._lblAVRIter.Name = "_lblAVRIter";
            this._lblAVRIter.Size = new System.Drawing.Size(45, 15);
            this._lblAVRIter.TabIndex = 5;
            this._lblAVRIter.Text = "iterativ:";
            // 
            // _lblAVRGlob
            // 
            this._lblAVRGlob.AutoSize = true;
            this._lblAVRGlob.Location = new System.Drawing.Point(30, 23);
            this._lblAVRGlob.Name = "_lblAVRGlob";
            this._lblAVRGlob.Size = new System.Drawing.Size(44, 15);
            this._lblAVRGlob.TabIndex = 4;
            this._lblAVRGlob.Text = "global:";
            // 
            // _gbBest
            // 
            this._gbBest.Controls.Add(this._tbBestIter);
            this._gbBest.Controls.Add(this._tbBestGlob);
            this._gbBest.Controls.Add(this._lblBestIter);
            this._gbBest.Controls.Add(this._lblBestGlob);
            this._gbBest.Location = new System.Drawing.Point(6, 16);
            this._gbBest.Name = "_gbBest";
            this._gbBest.Size = new System.Drawing.Size(243, 87);
            this._gbBest.TabIndex = 0;
            this._gbBest.TabStop = false;
            this._gbBest.Text = "Beste Tour";
            // 
            // _tbBestIter
            // 
            this._tbBestIter.Location = new System.Drawing.Point(111, 47);
            this._tbBestIter.Name = "_tbBestIter";
            this._tbBestIter.ReadOnly = true;
            this._tbBestIter.Size = new System.Drawing.Size(98, 20);
            this._tbBestIter.TabIndex = 2;
            // 
            // _tbBestGlob
            // 
            this._tbBestGlob.Location = new System.Drawing.Point(111, 20);
            this._tbBestGlob.Name = "_tbBestGlob";
            this._tbBestGlob.ReadOnly = true;
            this._tbBestGlob.Size = new System.Drawing.Size(98, 20);
            this._tbBestGlob.TabIndex = 3;
            // 
            // _lblBestIter
            // 
            this._lblBestIter.AutoSize = true;
            this._lblBestIter.Location = new System.Drawing.Point(23, 50);
            this._lblBestIter.Name = "_lblBestIter";
            this._lblBestIter.Size = new System.Drawing.Size(45, 15);
            this._lblBestIter.TabIndex = 1;
            this._lblBestIter.Text = "iterativ:";
            // 
            // _lblBestGlob
            // 
            this._lblBestGlob.AutoSize = true;
            this._lblBestGlob.Location = new System.Drawing.Point(26, 23);
            this._lblBestGlob.Name = "_lblBestGlob";
            this._lblBestGlob.Size = new System.Drawing.Size(44, 15);
            this._lblBestGlob.TabIndex = 0;
            this._lblBestGlob.Text = "global:";
            // 
            // _lblCityCount
            // 
            this._lblCityCount.AutoSize = true;
            this._lblCityCount.Location = new System.Drawing.Point(376, 116);
            this._lblCityCount.Name = "_lblCityCount";
            this._lblCityCount.Size = new System.Drawing.Size(45, 15);
            this._lblCityCount.TabIndex = 4;
            this._lblCityCount.Text = "Städte:";
            // 
            // _gbConfig
            // 
            this._gbConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._gbConfig.Controls.Add(this._lblMinTourLength);
            this._gbConfig.Controls.Add(this._lblOptTourLength);
            this._gbConfig.Controls.Add(this._tbMinTour);
            this._gbConfig.Controls.Add(this._tbOptTour);
            this._gbConfig.Controls.Add(this._btnStart);
            this._gbConfig.Controls.Add(this._tbQ);
            this._gbConfig.Controls.Add(this._lblQ);
            this._gbConfig.Controls.Add(this._tbTau);
            this._gbConfig.Controls.Add(this._lblTau);
            this._gbConfig.Controls.Add(this._tbRho);
            this._gbConfig.Controls.Add(this._lblRho);
            this._gbConfig.Controls.Add(this._tbBeta);
            this._gbConfig.Controls.Add(this._lblBeta);
            this._gbConfig.Controls.Add(this._tbAlpha);
            this._gbConfig.Controls.Add(this._lblAlpha);
            this._gbConfig.Controls.Add(this._tbIterationCount);
            this._gbConfig.Controls.Add(this._lblCountIterations);
            this._gbConfig.Controls.Add(this._tbAntCount);
            this._gbConfig.Controls.Add(this._lblAntCount);
            this._gbConfig.Location = new System.Drawing.Point(12, 197);
            this._gbConfig.Name = "_gbConfig";
            this._gbConfig.Size = new System.Drawing.Size(510, 159);
            this._gbConfig.TabIndex = 0;
            this._gbConfig.TabStop = false;
            this._gbConfig.Text = "Konfiguration";
            // 
            // _lblMinTourLength
            // 
            this._lblMinTourLength.AutoSize = true;
            this._lblMinTourLength.Location = new System.Drawing.Point(1, 78);
            this._lblMinTourLength.Name = "_lblMinTourLength";
            this._lblMinTourLength.Size = new System.Drawing.Size(87, 15);
            this._lblMinTourLength.TabIndex = 22;
            this._lblMinTourLength.Text = "Minimale Tour";
            this._lblMinTourLength.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _lblOptTourLength
            // 
            this._lblOptTourLength.AutoSize = true;
            this._lblOptTourLength.Location = new System.Drawing.Point(3, 104);
            this._lblOptTourLength.Name = "_lblOptTourLength";
            this._lblOptTourLength.Size = new System.Drawing.Size(85, 15);
            this._lblOptTourLength.TabIndex = 21;
            this._lblOptTourLength.Text = "Optimale Tour";
            this._lblOptTourLength.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _tbMinTour
            // 
            this._tbMinTour.Location = new System.Drawing.Point(117, 75);
            this._tbMinTour.Name = "_tbMinTour";
            this._tbMinTour.Size = new System.Drawing.Size(98, 20);
            this._tbMinTour.TabIndex = 5;
            this._tbMinTour.Text = "1";
            this._tbMinTour.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // _tbOptTour
            // 
            this._tbOptTour.Location = new System.Drawing.Point(117, 101);
            this._tbOptTour.Name = "_tbOptTour";
            this._tbOptTour.Size = new System.Drawing.Size(98, 20);
            this._tbOptTour.TabIndex = 6;
            this._tbOptTour.Text = "1";
            this._tbOptTour.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // _btnStart
            // 
            this._btnStart.Location = new System.Drawing.Point(117, 131);
            this._btnStart.Name = "_btnStart";
            this._btnStart.Size = new System.Drawing.Size(98, 23);
            this._btnStart.TabIndex = 18;
            this._btnStart.Text = "Start";
            this._btnStart.UseVisualStyleBackColor = true;
            this._btnStart.Click += new System.EventHandler(this._btnStart_Click);
            // 
            // _tbQ
            // 
            this._tbQ.Location = new System.Drawing.Point(379, 133);
            this._tbQ.Name = "_tbQ";
            this._tbQ.Size = new System.Drawing.Size(98, 20);
            this._tbQ.TabIndex = 11;
            this._ttInputTooltip.SetToolTip(this._tbQ, "heuristischer Parameter für Pheromon-Update (Q > 0)");
            this._tbQ.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // _lblQ
            // 
            this._lblQ.AutoSize = true;
            this._lblQ.Location = new System.Drawing.Point(326, 136);
            this._lblQ.Name = "_lblQ";
            this._lblQ.Size = new System.Drawing.Size(16, 15);
            this._lblQ.TabIndex = 16;
            this._lblQ.Text = "Q";
            this._ttInputTooltip.SetToolTip(this._lblQ, "heuristischer Parameter für Pheromon-Update (Q > 0)");
            // 
            // _tbTau
            // 
            this._tbTau.Location = new System.Drawing.Point(379, 104);
            this._tbTau.Name = "_tbTau";
            this._tbTau.Size = new System.Drawing.Size(98, 20);
            this._tbTau.TabIndex = 10;
            this._ttInputTooltip.SetToolTip(this._tbTau, "initiale Pheromon-Werte (Tau0 > 0)");
            this._tbTau.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // _lblTau
            // 
            this._lblTau.AutoSize = true;
            this._lblTau.Location = new System.Drawing.Point(309, 107);
            this._lblTau.Name = "_lblTau";
            this._lblTau.Size = new System.Drawing.Size(35, 15);
            this._lblTau.TabIndex = 14;
            this._lblTau.Text = "Tau0";
            this._ttInputTooltip.SetToolTip(this._lblTau, "initiale Pheromon-Werte (Tau0 > 0)");
            // 
            // _tbRho
            // 
            this._tbRho.Location = new System.Drawing.Point(379, 78);
            this._tbRho.Name = "_tbRho";
            this._tbRho.Size = new System.Drawing.Size(98, 20);
            this._tbRho.TabIndex = 9;
            this._ttInputTooltip.SetToolTip(this._tbRho, "Verdunstungsfaktor (0 < Rho <= 1)");
            this._tbRho.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // _lblRho
            // 
            this._lblRho.AutoSize = true;
            this._lblRho.Location = new System.Drawing.Point(314, 81);
            this._lblRho.Name = "_lblRho";
            this._lblRho.Size = new System.Drawing.Size(30, 15);
            this._lblRho.TabIndex = 12;
            this._lblRho.Text = "Rho";
            this._ttInputTooltip.SetToolTip(this._lblRho, "Verdunstungsfaktor (0 < Rho <= 1)");
            // 
            // _tbBeta
            // 
            this._tbBeta.Location = new System.Drawing.Point(379, 52);
            this._tbBeta.Name = "_tbBeta";
            this._tbBeta.Size = new System.Drawing.Size(98, 20);
            this._tbBeta.TabIndex = 8;
            this._tbBeta.Text = "1";
            this._ttInputTooltip.SetToolTip(this._tbBeta, "heuristischer Parameter für die lokale Information (Beta > 0)");
            this._tbBeta.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // _lblBeta
            // 
            this._lblBeta.AutoSize = true;
            this._lblBeta.Location = new System.Drawing.Point(312, 55);
            this._lblBeta.Name = "_lblBeta";
            this._lblBeta.Size = new System.Drawing.Size(32, 15);
            this._lblBeta.TabIndex = 10;
            this._lblBeta.Text = "Beta";
            this._ttInputTooltip.SetToolTip(this._lblBeta, "heuristischer Parameter für die lokale Information (Beta > 0)");
            // 
            // _tbAlpha
            // 
            this._tbAlpha.Location = new System.Drawing.Point(379, 23);
            this._tbAlpha.Name = "_tbAlpha";
            this._tbAlpha.Size = new System.Drawing.Size(98, 20);
            this._tbAlpha.TabIndex = 7;
            this._tbAlpha.Text = "1";
            this._ttInputTooltip.SetToolTip(this._tbAlpha, "Pheromon Parameter (0 <= Alpha <= 0)");
            this._tbAlpha.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // _lblAlpha
            // 
            this._lblAlpha.AutoSize = true;
            this._lblAlpha.Location = new System.Drawing.Point(307, 26);
            this._lblAlpha.Name = "_lblAlpha";
            this._lblAlpha.Size = new System.Drawing.Size(38, 15);
            this._lblAlpha.TabIndex = 8;
            this._lblAlpha.Text = "Alpha";
            this._ttInputTooltip.SetToolTip(this._lblAlpha, "Pheromon Parameter (0 <= Alpha <= 0)");
            // 
            // _tbIterationCount
            // 
            this._tbIterationCount.Location = new System.Drawing.Point(117, 49);
            this._tbIterationCount.Name = "_tbIterationCount";
            this._tbIterationCount.Size = new System.Drawing.Size(98, 20);
            this._tbIterationCount.TabIndex = 4;
            this._tbIterationCount.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // _lblCountIterations
            // 
            this._lblCountIterations.AutoSize = true;
            this._lblCountIterations.Location = new System.Drawing.Point(23, 52);
            this._lblCountIterations.Name = "_lblCountIterations";
            this._lblCountIterations.Size = new System.Drawing.Size(65, 15);
            this._lblCountIterations.TabIndex = 6;
            this._lblCountIterations.Text = "Iterationen";
            // 
            // _tbAntCount
            // 
            this._tbAntCount.Location = new System.Drawing.Point(117, 23);
            this._tbAntCount.Name = "_tbAntCount";
            this._tbAntCount.Size = new System.Drawing.Size(98, 20);
            this._tbAntCount.TabIndex = 3;
            this._tbAntCount.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // _lblAntCount
            // 
            this._lblAntCount.AutoSize = true;
            this._lblAntCount.Location = new System.Drawing.Point(33, 26);
            this._lblAntCount.Name = "_lblAntCount";
            this._lblAntCount.Size = new System.Drawing.Size(55, 15);
            this._lblAntCount.TabIndex = 0;
            this._lblAntCount.Text = "Ameisen";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(534, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._smiTSPLadenToolStripMenuItem,
            this.tODOErgsSpeichernToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // _smiTSPLadenToolStripMenuItem
            // 
            this._smiTSPLadenToolStripMenuItem.Name = "_smiTSPLadenToolStripMenuItem";
            this._smiTSPLadenToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this._smiTSPLadenToolStripMenuItem.Text = "... TSP laden";
            this._smiTSPLadenToolStripMenuItem.Click += new System.EventHandler(this._smiTSPLadenToolStripMenuItem_Click);
            // 
            // tODOErgsSpeichernToolStripMenuItem
            // 
            this.tODOErgsSpeichernToolStripMenuItem.Name = "tODOErgsSpeichernToolStripMenuItem";
            this.tODOErgsSpeichernToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.tODOErgsSpeichernToolStripMenuItem.Text = "... Resultat speichern";
            this.tODOErgsSpeichernToolStripMenuItem.Click += new System.EventHandler(this.tODOErgsSpeichernToolStripMenuItem_Click);
            // 
            // _lblIterCount
            // 
            this._lblIterCount.AutoSize = true;
            this._lblIterCount.Location = new System.Drawing.Point(285, 116);
            this._lblIterCount.Name = "_lblIterCount";
            this._lblIterCount.Size = new System.Drawing.Size(51, 15);
            this._lblIterCount.TabIndex = 6;
            this._lblIterCount.Text = "Iteration";
            // 
            // FInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 368);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this._gbConfig);
            this.Controls.Add(this._gbOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FInput";
            this.Text = "Eingabefenster";
            this._gbOutput.ResumeLayout(false);
            this._gbOutput.PerformLayout();
            this._gbAvrg.ResumeLayout(false);
            this._gbAvrg.PerformLayout();
            this._gbBest.ResumeLayout(false);
            this._gbBest.PerformLayout();
            this._gbConfig.ResumeLayout(false);
            this._gbConfig.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _gbOutput;
        private System.Windows.Forms.GroupBox _gbConfig;
        private System.Windows.Forms.Label _lblTime;
        private System.Windows.Forms.GroupBox _gbAvrg;
        private System.Windows.Forms.TextBox _tbAVRIter;
        private System.Windows.Forms.TextBox _tbAVRGlob;
        private System.Windows.Forms.Label _lblAVRIter;
        private System.Windows.Forms.Label _lblAVRGlob;
        private System.Windows.Forms.GroupBox _gbBest;
        private System.Windows.Forms.TextBox _tbBestIter;
        private System.Windows.Forms.TextBox _tbBestGlob;
        private System.Windows.Forms.Label _lblBestIter;
        private System.Windows.Forms.Label _lblBestGlob;
        private System.Windows.Forms.Button _btnStart;
        private System.Windows.Forms.TextBox _tbQ;
        private System.Windows.Forms.Label _lblQ;
        private System.Windows.Forms.TextBox _tbTau;
        private System.Windows.Forms.Label _lblTau;
        private System.Windows.Forms.TextBox _tbRho;
        private System.Windows.Forms.Label _lblRho;
        private System.Windows.Forms.TextBox _tbBeta;
        private System.Windows.Forms.Label _lblBeta;
        private System.Windows.Forms.TextBox _tbAlpha;
        private System.Windows.Forms.Label _lblAlpha;
        private System.Windows.Forms.TextBox _tbIterationCount;
        private System.Windows.Forms.Label _lblCountIterations;
        private System.Windows.Forms.Label _lblCityCount;
        private System.Windows.Forms.TextBox _tbAntCount;
        private System.Windows.Forms.Label _lblAntCount;
        private System.Windows.Forms.ToolTip _ttInputTooltip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _smiTSPLadenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tODOErgsSpeichernToolStripMenuItem;
        private System.Windows.Forms.Label _lblMinTourLength;
        private System.Windows.Forms.Label _lblOptTourLength;
        private System.Windows.Forms.TextBox _tbMinTour;
        private System.Windows.Forms.TextBox _tbOptTour;
        private System.Windows.Forms.Label _lblIterCount;
    }
}