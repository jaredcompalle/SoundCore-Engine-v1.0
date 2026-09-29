namespace SoundCore_Engine_v1._0
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label5 = new Label();
            label6 = new Label();
            rbOwn = new RadioButton();
            panel1 = new Panel();
            label3 = new Label();
            numBpm = new NumericUpDown();
            rbList = new RadioButton();
            rbLinkedList = new RadioButton();
            btnPause = new Button();
            panel2 = new Panel();
            btnPurge = new Button();
            btnSortBpm = new Button();
            btnInvest = new Button();
            btnAdvance = new Button();
            btnPlayNext = new Button();
            btnInFinalStrain = new Button();
            label7 = new Label();
            panel3 = new Panel();
            label2 = new Label();
            tbVolume = new TrackBar();
            lblTime = new Label();
            btnStop = new Button();
            tbPosition = new TrackBar();
            lblStadistics = new Label();
            dgvTail = new DataGridView();
            lblNowPlaying = new Label();
            label8 = new Label();
            panel4 = new Panel();
            label1 = new Label();
            numBenchMark = new NumericUpDown();
            btnBenchmark = new Button();
            txtResultsBenchmark = new TextBox();
            label10 = new Label();
            timerPlayer = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numBpm).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbPosition).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTail).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numBenchMark).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(0, 0);
            label5.Name = "label5";
            label5.Size = new Size(221, 15);
            label5.TabIndex = 4;
            label5.Text = "[ PANEL SUPERIOR: REGISTRO DE PISTA ]";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(5, 25);
            label6.Name = "label6";
            label6.Size = new Size(117, 15);
            label6.TabIndex = 5;
            label6.Text = "Modo de estructura :";
            // 
            // rbOwn
            // 
            rbOwn.AutoSize = true;
            rbOwn.Location = new Point(128, 23);
            rbOwn.Name = "rbOwn";
            rbOwn.Size = new Size(171, 19);
            rbOwn.TabIndex = 9;
            rbOwn.TabStop = true;
            rbOwn.Text = "Lista Simple Propia (Nodos)";
            rbOwn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(label3);
            panel1.Controls.Add(numBpm);
            panel1.Controls.Add(rbList);
            panel1.Controls.Add(rbLinkedList);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(rbOwn);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(927, 48);
            panel1.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(540, 23);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 13;
            label3.Text = "BPM:";
            // 
            // numBpm
            // 
            numBpm.Location = new Point(581, 17);
            numBpm.Name = "numBpm";
            numBpm.Size = new Size(54, 23);
            numBpm.TabIndex = 12;
            // 
            // rbList
            // 
            rbList.AutoSize = true;
            rbList.Location = new Point(440, 23);
            rbList.Name = "rbList";
            rbList.Size = new Size(94, 19);
            rbList.TabIndex = 11;
            rbList.TabStop = true;
            rbList.Text = ".NET List<T>";
            rbList.UseVisualStyleBackColor = true;
            // 
            // rbLinkedList
            // 
            rbLinkedList.AutoSize = true;
            rbLinkedList.Location = new Point(305, 23);
            rbLinkedList.Name = "rbLinkedList";
            rbLinkedList.Size = new Size(129, 19);
            rbLinkedList.TabIndex = 10;
            rbLinkedList.TabStop = true;
            rbLinkedList.Text = ".NET LinkedList<T>";
            rbLinkedList.UseVisualStyleBackColor = true;
            // 
            // btnPause
            // 
            btnPause.Location = new Point(16, 91);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(88, 23);
            btnPause.TabIndex = 12;
            btnPause.Text = "Play / Pausa";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnPurge);
            panel2.Controls.Add(btnSortBpm);
            panel2.Controls.Add(btnInvest);
            panel2.Controls.Add(btnAdvance);
            panel2.Controls.Add(btnPlayNext);
            panel2.Controls.Add(btnInFinalStrain);
            panel2.Controls.Add(label7);
            panel2.Location = new Point(0, 54);
            panel2.Name = "panel2";
            panel2.Size = new Size(207, 306);
            panel2.TabIndex = 11;
            // 
            // btnPurge
            // 
            btnPurge.Location = new Point(9, 246);
            btnPurge.Name = "btnPurge";
            btnPurge.Size = new Size(195, 36);
            btnPurge.TabIndex = 18;
            btnPurge.Text = "Purgar duplicados";
            btnPurge.UseVisualStyleBackColor = true;
            btnPurge.Click += btnPurge_Click;
            // 
            // btnSortBpm
            // 
            btnSortBpm.Location = new Point(9, 204);
            btnSortBpm.Name = "btnSortBpm";
            btnSortBpm.Size = new Size(195, 36);
            btnSortBpm.TabIndex = 17;
            btnSortBpm.Text = "Ordenar por curva Bpm";
            btnSortBpm.UseVisualStyleBackColor = true;
            btnSortBpm.Click += btnSortBpm_Click;
            // 
            // btnInvest
            // 
            btnInvest.Location = new Point(9, 162);
            btnInvest.Name = "btnInvest";
            btnInvest.Size = new Size(195, 36);
            btnInvest.TabIndex = 16;
            btnInvest.Text = "Invertir lista (in place)";
            btnInvest.UseVisualStyleBackColor = true;
            btnInvest.Click += btnInvest_Click;
            // 
            // btnAdvance
            // 
            btnAdvance.Location = new Point(9, 120);
            btnAdvance.Name = "btnAdvance";
            btnAdvance.Size = new Size(195, 36);
            btnAdvance.TabIndex = 15;
            btnAdvance.Text = "Avanzar pista";
            btnAdvance.UseVisualStyleBackColor = true;
            btnAdvance.Click += btnAdvance_Click;
            // 
            // btnPlayNext
            // 
            btnPlayNext.Location = new Point(9, 74);
            btnPlayNext.Name = "btnPlayNext";
            btnPlayNext.Size = new Size(195, 36);
            btnPlayNext.TabIndex = 14;
            btnPlayNext.Text = "Reproducir siguiente";
            btnPlayNext.UseVisualStyleBackColor = true;
            btnPlayNext.Click += btnPlayNext_Click;
            // 
            // btnInFinalStrain
            // 
            btnInFinalStrain.Location = new Point(9, 25);
            btnInFinalStrain.Name = "btnInFinalStrain";
            btnInFinalStrain.Size = new Size(195, 36);
            btnInFinalStrain.TabIndex = 13;
            btnInFinalStrain.Text = "Encolar al final";
            btnInFinalStrain.UseVisualStyleBackColor = true;
            btnInFinalStrain.Click += btnAddMusic_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(0, -3);
            label7.Name = "label7";
            label7.Size = new Size(129, 15);
            label7.TabIndex = 12;
            label7.Text = "[ ACCIONES DE COLA ]";
            // 
            // panel3
            // 
            panel3.Controls.Add(label2);
            panel3.Controls.Add(tbVolume);
            panel3.Controls.Add(lblTime);
            panel3.Controls.Add(btnStop);
            panel3.Controls.Add(btnPause);
            panel3.Controls.Add(tbPosition);
            panel3.Controls.Add(lblStadistics);
            panel3.Controls.Add(dgvTail);
            panel3.Controls.Add(lblNowPlaying);
            panel3.Controls.Add(label8);
            panel3.Location = new Point(213, 54);
            panel3.Name = "panel3";
            panel3.Size = new Size(686, 306);
            panel3.TabIndex = 12;
            // 
            // label2
            // 
            label2.Location = new Point(305, 95);
            label2.Name = "label2";
            label2.Size = new Size(21, 19);
            label2.TabIndex = 25;
            label2.Text = "🔊";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbVolume
            // 
            tbVolume.AutoSize = false;
            tbVolume.Location = new Point(332, 91);
            tbVolume.Margin = new Padding(3, 8, 3, 0);
            tbVolume.Maximum = 100;
            tbVolume.Name = "tbVolume";
            tbVolume.Size = new Size(159, 23);
            tbVolume.TabIndex = 24;
            tbVolume.TickStyle = TickStyle.None;
            tbVolume.Value = 80;
            tbVolume.Scroll += tbVolume_Scroll;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(204, 95);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(72, 15);
            lblTime.TabIndex = 23;
            lblTime.Text = "00:00 / 00:00";
            // 
            // btnStop
            // 
            btnStop.Location = new Point(110, 91);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(88, 23);
            btnStop.TabIndex = 20;
            btnStop.Text = "Detener";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // tbPosition
            // 
            tbPosition.AutoSize = false;
            tbPosition.LargeChange = 50;
            tbPosition.Location = new Point(9, 53);
            tbPosition.Maximum = 1000;
            tbPosition.Name = "tbPosition";
            tbPosition.Size = new Size(641, 32);
            tbPosition.TabIndex = 19;
            tbPosition.TickStyle = TickStyle.None;
            tbPosition.MouseDown += tbPosition_MouseDown;
            tbPosition.MouseUp += tbPosition_MouseUp;
            // 
            // lblStadistics
            // 
            lblStadistics.AutoSize = true;
            lblStadistics.Location = new Point(16, 282);
            lblStadistics.Name = "lblStadistics";
            lblStadistics.Size = new Size(77, 15);
            lblStadistics.TabIndex = 18;
            lblStadistics.Text = "Total en cola:";
            // 
            // dgvTail
            // 
            dgvTail.AllowUserToAddRows = false;
            dgvTail.AllowUserToDeleteRows = false;
            dgvTail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTail.Location = new Point(16, 120);
            dgvTail.Name = "dgvTail";
            dgvTail.ReadOnly = true;
            dgvTail.RowHeadersVisible = false;
            dgvTail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTail.Size = new Size(650, 159);
            dgvTail.TabIndex = 17;
            // 
            // lblNowPlaying
            // 
            lblNowPlaying.AutoSize = true;
            lblNowPlaying.Location = new Point(16, 25);
            lblNowPlaying.Name = "lblNowPlaying";
            lblNowPlaying.Size = new Size(305, 15);
            lblNowPlaying.TabIndex = 15;
            lblNowPlaying.Text = "Sin reproducción — agrega música y pulsa Avanzar Pista";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(0, -3);
            label8.Name = "label8";
            label8.Size = new Size(200, 15);
            label8.TabIndex = 13;
            label8.Text = "[ PLAYLIST VISUAL / COLA EN VIVO ]";
            // 
            // panel4
            // 
            panel4.Controls.Add(label1);
            panel4.Controls.Add(numBenchMark);
            panel4.Controls.Add(btnBenchmark);
            panel4.Controls.Add(txtResultsBenchmark);
            panel4.Controls.Add(label10);
            panel4.Location = new Point(0, 366);
            panel4.Name = "panel4";
            panel4.Size = new Size(899, 203);
            panel4.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 23);
            label1.Name = "label1";
            label1.Size = new Size(137, 15);
            label1.TabIndex = 26;
            label1.Text = "Pistas para test de estrés:";
            // 
            // numBenchMark
            // 
            numBenchMark.Location = new Point(152, 19);
            numBenchMark.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            numBenchMark.Name = "numBenchMark";
            numBenchMark.Size = new Size(58, 23);
            numBenchMark.TabIndex = 25;
            numBenchMark.Value = new decimal(new int[] { 2500, 0, 0, 0 });
            // 
            // btnBenchmark
            // 
            btnBenchmark.Location = new Point(213, 19);
            btnBenchmark.Name = "btnBenchmark";
            btnBenchmark.Size = new Size(195, 23);
            btnBenchmark.TabIndex = 22;
            btnBenchmark.Text = "🚀 Iniciar Prueba de Rendimiento";
            btnBenchmark.UseVisualStyleBackColor = true;
            btnBenchmark.Click += btnBenchmark_Click;
            // 
            // txtResultsBenchmark
            // 
            txtResultsBenchmark.Location = new Point(12, 41);
            txtResultsBenchmark.Multiline = true;
            txtResultsBenchmark.Name = "txtResultsBenchmark";
            txtResultsBenchmark.ReadOnly = true;
            txtResultsBenchmark.Size = new Size(867, 149);
            txtResultsBenchmark.TabIndex = 20;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(0, 0);
            label10.Name = "label10";
            label10.Size = new Size(228, 15);
            label10.TabIndex = 19;
            label10.Text = "[ PANEL DE BENCHMARK Y TELEMETRÍA ]";
            // 
            // timerPlayer
            // 
            timerPlayer.Enabled = true;
            timerPlayer.Interval = 250;
            timerPlayer.Tick += timerPlayer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(898, 571);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "SoundCore Engine v1.0 - DJ Set Controller [TecNM Monclova]";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numBpm).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbPosition).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTail).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numBenchMark).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label label5;
        private Label label6;
        private RadioButton rbOwn;
        private Panel panel1;
        private RadioButton rbList;
        private RadioButton rbLinkedList;
        private Panel panel2;
        private Label label7;
        private Panel panel3;
        private Label label8;
        private DataGridView dgvTail;
        private Label lblNowPlaying;
        private Button btnPlayNext;
        private Button btnInFinalStrain;
        private Button btnSortBpm;
        private Button btnInvest;
        private Button btnAdvance;
        private Button btnPurge;
        private Label lblStadistics;
        private Panel panel4;
        private Label label10;
        private TextBox txtResultsBenchmark;
        private Button btnBenchmark;
        private Button btnPause;
        private Button btnStop;
        private TrackBar tbPosition;
        private TrackBar tbVolume;
        private Label lblTime;
        private System.Windows.Forms.Timer timerPlayer;
        private Label label1;
        private NumericUpDown numBenchMark;
        private Label label2;
        private Label label3;
        private NumericUpDown numBpm;
    }
}
