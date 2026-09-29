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
            rbPropia = new RadioButton();
            panel1 = new Panel();
            rbList = new RadioButton();
            rbLinkedList = new RadioButton();
            btnPausa = new Button();
            panel2 = new Panel();
            btnPurgar = new Button();
            btnOrdenarBpm = new Button();
            btnInvertir = new Button();
            btnAvanzar = new Button();
            btnReproducirSiguiente = new Button();
            btnEncolarFinal = new Button();
            label7 = new Label();
            panel3 = new Panel();
            label2 = new Label();
            tbVolumen = new TrackBar();
            lblTiempo = new Label();
            btnDetener = new Button();
            tbPosicion = new TrackBar();
            lblEstadisticas = new Label();
            dgvCola = new DataGridView();
            lblNowPlaying = new Label();
            label8 = new Label();
            panel4 = new Panel();
            label1 = new Label();
            numBenchMark = new NumericUpDown();
            btnBenchmark = new Button();
            txtResultadosBenchmark = new TextBox();
            label10 = new Label();
            timerReproductor = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbVolumen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbPosicion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCola).BeginInit();
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
            // rbPropia
            // 
            rbPropia.AutoSize = true;
            rbPropia.Location = new Point(128, 23);
            rbPropia.Name = "rbPropia";
            rbPropia.Size = new Size(171, 19);
            rbPropia.TabIndex = 9;
            rbPropia.TabStop = true;
            rbPropia.Text = "Lista Simple Propia (Nodos)";
            rbPropia.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(rbList);
            panel1.Controls.Add(rbLinkedList);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(rbPropia);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(927, 48);
            panel1.TabIndex = 10;
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
            // btnPausa
            // 
            btnPausa.Location = new Point(16, 91);
            btnPausa.Name = "btnPausa";
            btnPausa.Size = new Size(88, 23);
            btnPausa.TabIndex = 12;
            btnPausa.Text = "Play / Pausa";
            btnPausa.UseVisualStyleBackColor = true;
            btnPausa.Click += btnPausa_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnPurgar);
            panel2.Controls.Add(btnOrdenarBpm);
            panel2.Controls.Add(btnInvertir);
            panel2.Controls.Add(btnAvanzar);
            panel2.Controls.Add(btnReproducirSiguiente);
            panel2.Controls.Add(btnEncolarFinal);
            panel2.Controls.Add(label7);
            panel2.Location = new Point(0, 54);
            panel2.Name = "panel2";
            panel2.Size = new Size(207, 306);
            panel2.TabIndex = 11;
            // 
            // btnPurgar
            // 
            btnPurgar.Location = new Point(9, 164);
            btnPurgar.Name = "btnPurgar";
            btnPurgar.Size = new Size(195, 23);
            btnPurgar.TabIndex = 18;
            btnPurgar.Text = "Purgar duplicados";
            btnPurgar.UseVisualStyleBackColor = true;
            btnPurgar.Click += btnPurgar_Click;
            // 
            // btnOrdenarBpm
            // 
            btnOrdenarBpm.Location = new Point(9, 133);
            btnOrdenarBpm.Name = "btnOrdenarBpm";
            btnOrdenarBpm.Size = new Size(195, 23);
            btnOrdenarBpm.TabIndex = 17;
            btnOrdenarBpm.Text = "Ordenar por curva Bpm";
            btnOrdenarBpm.UseVisualStyleBackColor = true;
            btnOrdenarBpm.Click += btnOrdenarBpm_Click;
            // 
            // btnInvertir
            // 
            btnInvertir.Location = new Point(9, 104);
            btnInvertir.Name = "btnInvertir";
            btnInvertir.Size = new Size(195, 23);
            btnInvertir.TabIndex = 16;
            btnInvertir.Text = "Invertir lista (in place)";
            btnInvertir.UseVisualStyleBackColor = true;
            btnInvertir.Click += btnInvertir_Click;
            // 
            // btnAvanzar
            // 
            btnAvanzar.Location = new Point(9, 75);
            btnAvanzar.Name = "btnAvanzar";
            btnAvanzar.Size = new Size(195, 23);
            btnAvanzar.TabIndex = 15;
            btnAvanzar.Text = "Avanzar pista";
            btnAvanzar.UseVisualStyleBackColor = true;
            btnAvanzar.Click += btnAvanzar_Click;
            // 
            // btnReproducirSiguiente
            // 
            btnReproducirSiguiente.Location = new Point(9, 46);
            btnReproducirSiguiente.Name = "btnReproducirSiguiente";
            btnReproducirSiguiente.Size = new Size(195, 23);
            btnReproducirSiguiente.TabIndex = 14;
            btnReproducirSiguiente.Text = "Reproducir siguiente";
            btnReproducirSiguiente.UseVisualStyleBackColor = true;
            btnReproducirSiguiente.Click += btnReproducirSiguiente_Click;
            // 
            // btnEncolarFinal
            // 
            btnEncolarFinal.Location = new Point(9, 17);
            btnEncolarFinal.Name = "btnEncolarFinal";
            btnEncolarFinal.Size = new Size(195, 23);
            btnEncolarFinal.TabIndex = 13;
            btnEncolarFinal.Text = "Encolar al final";
            btnEncolarFinal.UseVisualStyleBackColor = true;
            btnEncolarFinal.Click += btnAgregarMusica_Click;
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
            panel3.Controls.Add(tbVolumen);
            panel3.Controls.Add(lblTiempo);
            panel3.Controls.Add(btnDetener);
            panel3.Controls.Add(btnPausa);
            panel3.Controls.Add(tbPosicion);
            panel3.Controls.Add(lblEstadisticas);
            panel3.Controls.Add(dgvCola);
            panel3.Controls.Add(lblNowPlaying);
            panel3.Controls.Add(label8);
            panel3.Location = new Point(213, 54);
            panel3.Name = "panel3";
            panel3.Size = new Size(704, 306);
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
            // tbVolumen
            // 
            tbVolumen.AutoSize = false;
            tbVolumen.Location = new Point(332, 91);
            tbVolumen.Margin = new Padding(3, 8, 3, 0);
            tbVolumen.Maximum = 100;
            tbVolumen.Name = "tbVolumen";
            tbVolumen.Size = new Size(159, 23);
            tbVolumen.TabIndex = 24;
            tbVolumen.TickStyle = TickStyle.None;
            tbVolumen.Value = 80;
            // 
            // lblTiempo
            // 
            lblTiempo.AutoSize = true;
            lblTiempo.Location = new Point(204, 95);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(72, 15);
            lblTiempo.TabIndex = 23;
            lblTiempo.Text = "00:00 / 00:00";
            // 
            // btnDetener
            // 
            btnDetener.Location = new Point(110, 91);
            btnDetener.Name = "btnDetener";
            btnDetener.Size = new Size(88, 23);
            btnDetener.TabIndex = 20;
            btnDetener.Text = "Detener";
            btnDetener.UseVisualStyleBackColor = true;
            btnDetener.Click += btnDetener_Click;
            // 
            // tbPosicion
            // 
            tbPosicion.AutoSize = false;
            tbPosicion.LargeChange = 50;
            tbPosicion.Location = new Point(9, 53);
            tbPosicion.Maximum = 1000;
            tbPosicion.Name = "tbPosicion";
            tbPosicion.Size = new Size(641, 32);
            tbPosicion.TabIndex = 19;
            tbPosicion.TickStyle = TickStyle.None;
            tbPosicion.MouseDown += tbPosicion_MouseUp;
            tbPosicion.MouseUp += tbPosicion_MouseUp;
            // 
            // lblEstadisticas
            // 
            lblEstadisticas.AutoSize = true;
            lblEstadisticas.Location = new Point(9, 282);
            lblEstadisticas.Name = "lblEstadisticas";
            lblEstadisticas.Size = new Size(77, 15);
            lblEstadisticas.TabIndex = 18;
            lblEstadisticas.Text = "Total en cola:";
            // 
            // dgvCola
            // 
            dgvCola.AllowUserToAddRows = false;
            dgvCola.AllowUserToDeleteRows = false;
            dgvCola.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCola.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCola.Location = new Point(0, 120);
            dgvCola.Name = "dgvCola";
            dgvCola.ReadOnly = true;
            dgvCola.RowHeadersVisible = false;
            dgvCola.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCola.Size = new Size(650, 159);
            dgvCola.TabIndex = 17;
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
            panel4.Controls.Add(txtResultadosBenchmark);
            panel4.Controls.Add(label10);
            panel4.Location = new Point(0, 366);
            panel4.Name = "panel4";
            panel4.Size = new Size(927, 143);
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
            numBenchMark.Name = "numBenchMark";
            numBenchMark.Size = new Size(58, 23);
            numBenchMark.TabIndex = 25;
            // 
            // btnBenchmark
            // 
            btnBenchmark.Location = new Point(213, 19);
            btnBenchmark.Name = "btnBenchmark";
            btnBenchmark.Size = new Size(195, 23);
            btnBenchmark.TabIndex = 22;
            btnBenchmark.Text = "🚀 Iniciar Prueba de Rendimiento";
            btnBenchmark.UseVisualStyleBackColor = true;
            // 
            // txtResultadosBenchmark
            // 
            txtResultadosBenchmark.Location = new Point(12, 41);
            txtResultadosBenchmark.Multiline = true;
            txtResultadosBenchmark.Name = "txtResultadosBenchmark";
            txtResultadosBenchmark.ReadOnly = true;
            txtResultadosBenchmark.Size = new Size(887, 106);
            txtResultadosBenchmark.TabIndex = 20;
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
            // timerReproductor
            // 
            timerReproductor.Interval = 250;
            timerReproductor.Tick += timerReproductor_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(929, 510);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "SoundCore Engine v1.0 - DJ Set Controller [TecNM Monclova]";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbVolumen).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbPosicion).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCola).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numBenchMark).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label label5;
        private Label label6;
        private RadioButton rbPropia;
        private Panel panel1;
        private RadioButton rbList;
        private RadioButton rbLinkedList;
        private Panel panel2;
        private Label label7;
        private Panel panel3;
        private Label label8;
        private DataGridView dgvCola;
        private Label lblNowPlaying;
        private Button btnReproducirSiguiente;
        private Button btnEncolarFinal;
        private Button btnOrdenarBpm;
        private Button btnInvertir;
        private Button btnAvanzar;
        private Button btnPurgar;
        private Label lblEstadisticas;
        private Panel panel4;
        private Label label10;
        private TextBox txtResultadosBenchmark;
        private Button btnBenchmark;
        private Button btnPausa;
        private Button btnDetener;
        private TrackBar tbPosicion;
        private TrackBar tbVolumen;
        private Label lblTiempo;
        private System.Windows.Forms.Timer timerReproductor;
        private Label label1;
        private NumericUpDown numBenchMark;
        private Label label2;
    }
}
