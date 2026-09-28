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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            rbPropia = new RadioButton();
            panel1 = new Panel();
            numDuracion = new NumericUpDown();
            numBpm = new NumericUpDown();
            txtArtista = new TextBox();
            txtTitulo = new TextBox();
            rbList = new RadioButton();
            rbLinkedList = new RadioButton();
            panel2 = new Panel();
            btnPurgar = new Button();
            btnOrdenarBpm = new Button();
            btnInvertir = new Button();
            btnAvanzar = new Button();
            btnReproducirSiguiente = new Button();
            btnEncolarFinal = new Button();
            label7 = new Label();
            panel3 = new Panel();
            lblEstadisticas = new Label();
            dgvCola = new DataGridView();
            lblNowPlaying = new Label();
            label9 = new Label();
            label8 = new Label();
            panel4 = new Panel();
            btnBenchmark = new Button();
            label11 = new Label();
            txtResultadosBenchmark = new TextBox();
            label10 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDuracion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBpm).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCola).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 24);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 0;
            label1.Text = "Titulo :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(259, 24);
            label2.Name = "label2";
            label2.Size = new Size(47, 15);
            label2.TabIndex = 1;
            label2.Text = "Artista :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(532, 24);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 2;
            label3.Text = "BPM :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(637, 24);
            label4.Name = "label4";
            label4.Size = new Size(80, 15);
            label4.TabIndex = 3;
            label4.Text = "Duracion( s ) :";
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
            label6.Location = new Point(3, 50);
            label6.Name = "label6";
            label6.Size = new Size(117, 15);
            label6.TabIndex = 5;
            label6.Text = "Modo de estructura :";
            // 
            // rbPropia
            // 
            rbPropia.AutoSize = true;
            rbPropia.Location = new Point(126, 48);
            rbPropia.Name = "rbPropia";
            rbPropia.Size = new Size(171, 19);
            rbPropia.TabIndex = 9;
            rbPropia.TabStop = true;
            rbPropia.Text = "Lista Simple Propia (Nodos)";
            rbPropia.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(numDuracion);
            panel1.Controls.Add(numBpm);
            panel1.Controls.Add(txtArtista);
            panel1.Controls.Add(txtTitulo);
            panel1.Controls.Add(rbList);
            panel1.Controls.Add(rbLinkedList);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(rbPropia);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(801, 71);
            panel1.TabIndex = 10;
            // 
            // numDuracion
            // 
            numDuracion.Location = new Point(723, 22);
            numDuracion.Name = "numDuracion";
            numDuracion.Size = new Size(55, 23);
            numDuracion.TabIndex = 15;
            // 
            // numBpm
            // 
            numBpm.Location = new Point(576, 21);
            numBpm.Name = "numBpm";
            numBpm.Size = new Size(55, 23);
            numBpm.TabIndex = 14;
            // 
            // txtArtista
            // 
            txtArtista.Location = new Point(312, 20);
            txtArtista.Name = "txtArtista";
            txtArtista.Size = new Size(200, 23);
            txtArtista.TabIndex = 13;
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(53, 21);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(200, 23);
            txtTitulo.TabIndex = 12;
            // 
            // rbList
            // 
            rbList.AutoSize = true;
            rbList.Location = new Point(438, 48);
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
            rbLinkedList.Location = new Point(303, 48);
            rbLinkedList.Name = "rbLinkedList";
            rbLinkedList.Size = new Size(129, 19);
            rbLinkedList.TabIndex = 10;
            rbLinkedList.TabStop = true;
            rbLinkedList.Text = ".NET LinkedList<T>";
            rbLinkedList.UseVisualStyleBackColor = true;
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
            panel2.Location = new Point(0, 73);
            panel2.Name = "panel2";
            panel2.Size = new Size(207, 198);
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
            btnEncolarFinal.Click += btnEncolarFinal_Click;
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
            panel3.Controls.Add(lblEstadisticas);
            panel3.Controls.Add(dgvCola);
            panel3.Controls.Add(lblNowPlaying);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(label8);
            panel3.Location = new Point(213, 73);
            panel3.Name = "panel3";
            panel3.Size = new Size(588, 198);
            panel3.TabIndex = 12;
            // 
            // lblEstadisticas
            // 
            lblEstadisticas.AutoSize = true;
            lblEstadisticas.Location = new Point(3, 168);
            lblEstadisticas.Name = "lblEstadisticas";
            lblEstadisticas.Size = new Size(77, 15);
            lblEstadisticas.TabIndex = 18;
            lblEstadisticas.Text = "Total en cola:";
            // 
            // dgvCola
            // 
            dgvCola.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCola.Location = new Point(0, 39);
            dgvCola.Name = "dgvCola";
            dgvCola.Size = new Size(588, 117);
            dgvCola.TabIndex = 17;
            // 
            // lblNowPlaying
            // 
            lblNowPlaying.AutoSize = true;
            lblNowPlaying.Location = new Point(90, 21);
            lblNowPlaying.Name = "lblNowPlaying";
            lblNowPlaying.Size = new Size(353, 15);
            lblNowPlaying.TabIndex = 15;
            lblNowPlaying.Text = "Estado Actual: ▶ Reproduciendo: \"Strobe - deadmau5 (128 BPM)\"";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(3, 21);
            label9.Name = "label9";
            label9.Size = new Size(83, 15);
            label9.TabIndex = 14;
            label9.Text = "Estado actual :";
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
            panel4.Controls.Add(btnBenchmark);
            panel4.Controls.Add(label11);
            panel4.Controls.Add(txtResultadosBenchmark);
            panel4.Controls.Add(label10);
            panel4.Location = new Point(0, 277);
            panel4.Name = "panel4";
            panel4.Size = new Size(801, 161);
            panel4.TabIndex = 13;
            // 
            // btnBenchmark
            // 
            btnBenchmark.Location = new Point(275, 19);
            btnBenchmark.Name = "btnBenchmark";
            btnBenchmark.Size = new Size(195, 23);
            btnBenchmark.TabIndex = 22;
            btnBenchmark.Text = "🚀 Iniciar Prueba de Rendimiento";
            btnBenchmark.UseVisualStyleBackColor = true;
            btnBenchmark.Click += btnBenchmark_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(9, 23);
            label11.Name = "label11";
            label11.Size = new Size(260, 15);
            label11.TabIndex = 21;
            label11.Text = " Cantidad de pistas para test de estrés: [ 25,000 ] ";
            // 
            // txtResultadosBenchmark
            // 
            txtResultadosBenchmark.Location = new Point(12, 41);
            txtResultadosBenchmark.Multiline = true;
            txtResultadosBenchmark.Name = "txtResultadosBenchmark";
            txtResultadosBenchmark.ReadOnly = true;
            txtResultadosBenchmark.Size = new Size(766, 106);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "SoundCore Engine v1.0 - DJ Set Controller [TecNM Monclova]";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDuracion).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBpm).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCola).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
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
        private Label label9;
        private NumericUpDown numBpm;
        private TextBox txtArtista;
        private NumericUpDown numDuracion;
        private TextBox txtTitulo;
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
        private Label label11;
        private Button btnBenchmark;
    }
}
