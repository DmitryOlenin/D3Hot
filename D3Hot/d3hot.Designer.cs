namespace D3Hot
{
    partial class d3hot
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.nud_tmr1 = new System.Windows.Forms.NumericUpDown();
            this.nud_tmr2 = new System.Windows.Forms.NumericUpDown();
            this.nud_tmr3 = new System.Windows.Forms.NumericUpDown();
            this.nud_tmr4 = new System.Windows.Forms.NumericUpDown();
            this.cb_start = new System.Windows.Forms.CheckBox();
            this.cb_prog = new System.Windows.Forms.ComboBox();
            this.lb_area = new System.Windows.Forms.Label();
            this.lb_about = new System.Windows.Forms.Label();
            this.lb_trig1 = new System.Windows.Forms.Label();
            this.lb_trig2 = new System.Windows.Forms.Label();
            this.lb_trig3 = new System.Windows.Forms.Label();
            this.lb_trig4 = new System.Windows.Forms.Label();
            this.lb_tmr1_sec = new System.Windows.Forms.Label();
            this.lb_tmr2_sec = new System.Windows.Forms.Label();
            this.lb_tmr3_sec = new System.Windows.Forms.Label();
            this.lb_tmr4_sec = new System.Windows.Forms.Label();
            this.cb_trig_tmr1 = new System.Windows.Forms.ComboBox();
            this.cb_trig_tmr2 = new System.Windows.Forms.ComboBox();
            this.cb_trig_tmr3 = new System.Windows.Forms.ComboBox();
            this.cb_trig_tmr4 = new System.Windows.Forms.ComboBox();
            this.cb_key4 = new System.Windows.Forms.ComboBox();
            this.cb_key3 = new System.Windows.Forms.ComboBox();
            this.cb_key2 = new System.Windows.Forms.ComboBox();
            this.cb_key1 = new System.Windows.Forms.ComboBox();
            this.lb_key4 = new System.Windows.Forms.Label();
            this.lb_key3 = new System.Windows.Forms.Label();
            this.lb_key2 = new System.Windows.Forms.Label();
            this.lb_key1 = new System.Windows.Forms.Label();
            this.tt_start = new System.Windows.Forms.ToolTip(this.components);
            this.lb_stop = new System.Windows.Forms.Label();
            this.cb_pause = new System.Windows.Forms.ComboBox();
            this.mouseKeyEventProvider1 = new MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider();
            this.lb_auth = new System.Windows.Forms.Label();
            this.lb_lang = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr4)).BeginInit();
            this.SuspendLayout();
            // 
            // nud_tmr1
            // 
            this.nud_tmr1.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_tmr1.Location = new System.Drawing.Point(15, 113);
            this.nud_tmr1.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.nud_tmr1.Name = "nud_tmr1";
            this.nud_tmr1.Size = new System.Drawing.Size(59, 20);
            this.nud_tmr1.TabIndex = 1;
            // 
            // nud_tmr2
            // 
            this.nud_tmr2.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_tmr2.Location = new System.Drawing.Point(90, 113);
            this.nud_tmr2.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.nud_tmr2.Name = "nud_tmr2";
            this.nud_tmr2.Size = new System.Drawing.Size(59, 20);
            this.nud_tmr2.TabIndex = 3;
            // 
            // nud_tmr3
            // 
            this.nud_tmr3.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_tmr3.Location = new System.Drawing.Point(166, 113);
            this.nud_tmr3.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.nud_tmr3.Name = "nud_tmr3";
            this.nud_tmr3.Size = new System.Drawing.Size(59, 20);
            this.nud_tmr3.TabIndex = 5;
            // 
            // nud_tmr4
            // 
            this.nud_tmr4.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_tmr4.Location = new System.Drawing.Point(240, 113);
            this.nud_tmr4.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.nud_tmr4.Name = "nud_tmr4";
            this.nud_tmr4.Size = new System.Drawing.Size(59, 20);
            this.nud_tmr4.TabIndex = 7;
            // 
            // cb_start
            // 
            this.cb_start.Appearance = System.Windows.Forms.Appearance.Button;
            this.cb_start.Location = new System.Drawing.Point(15, 181);
            this.cb_start.Name = "cb_start";
            this.cb_start.Size = new System.Drawing.Size(97, 57);
            this.cb_start.TabIndex = 16;
            this.cb_start.Text = "Start";
            this.cb_start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_start.UseVisualStyleBackColor = true;
            this.cb_start.CheckedChanged += new System.EventHandler(this.cb_start_CheckedChanged);
            // 
            // cb_prog
            // 
            this.cb_prog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_prog.FormattingEnabled = true;
            this.cb_prog.Items.AddRange(new object[] {
            "All",
            "Diablo 3"});
            this.cb_prog.Location = new System.Drawing.Point(224, 178);
            this.cb_prog.Name = "cb_prog";
            this.cb_prog.Size = new System.Drawing.Size(71, 21);
            this.cb_prog.TabIndex = 17;
            this.cb_prog.SelectedIndexChanged += new System.EventHandler(this.cb_prog_SelectedIndexChanged);
            // 
            // lb_area
            // 
            this.lb_area.AutoSize = true;
            this.lb_area.Location = new System.Drawing.Point(118, 181);
            this.lb_area.Name = "lb_area";
            this.lb_area.Size = new System.Drawing.Size(100, 13);
            this.lb_area.TabIndex = 18;
            this.lb_area.Text = "Область действия";
            // 
            // lb_about
            // 
            this.lb_about.AutoSize = true;
            this.lb_about.Location = new System.Drawing.Point(12, 156);
            this.lb_about.Name = "lb_about";
            this.lb_about.Size = new System.Drawing.Size(217, 13);
            this.lb_about.TabIndex = 19;
            this.lb_about.Text = "Задержка в миллисекундах (1s = 1000ms)";
            // 
            // lb_trig1
            // 
            this.lb_trig1.AutoSize = true;
            this.lb_trig1.Location = new System.Drawing.Point(12, 39);
            this.lb_trig1.Name = "lb_trig1";
            this.lb_trig1.Size = new System.Drawing.Size(57, 13);
            this.lb_trig1.TabIndex = 20;
            this.lb_trig1.Text = "Триггер 1";
            // 
            // lb_trig2
            // 
            this.lb_trig2.AutoSize = true;
            this.lb_trig2.Location = new System.Drawing.Point(87, 39);
            this.lb_trig2.Name = "lb_trig2";
            this.lb_trig2.Size = new System.Drawing.Size(57, 13);
            this.lb_trig2.TabIndex = 21;
            this.lb_trig2.Text = "Триггер 2";
            // 
            // lb_trig3
            // 
            this.lb_trig3.AutoSize = true;
            this.lb_trig3.Location = new System.Drawing.Point(162, 39);
            this.lb_trig3.Name = "lb_trig3";
            this.lb_trig3.Size = new System.Drawing.Size(57, 13);
            this.lb_trig3.TabIndex = 22;
            this.lb_trig3.Text = "Триггер 3";
            // 
            // lb_trig4
            // 
            this.lb_trig4.AutoSize = true;
            this.lb_trig4.Location = new System.Drawing.Point(237, 39);
            this.lb_trig4.Name = "lb_trig4";
            this.lb_trig4.Size = new System.Drawing.Size(57, 13);
            this.lb_trig4.TabIndex = 23;
            this.lb_trig4.Text = "Триггер 4";
            // 
            // lb_tmr1_sec
            // 
            this.lb_tmr1_sec.AutoSize = true;
            this.lb_tmr1_sec.Location = new System.Drawing.Point(12, 136);
            this.lb_tmr1_sec.Name = "lb_tmr1_sec";
            this.lb_tmr1_sec.Size = new System.Drawing.Size(58, 13);
            this.lb_tmr1_sec.TabIndex = 26;
            this.lb_tmr1_sec.Text = "Пауза..мс";
            this.lb_tmr1_sec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_tmr2_sec
            // 
            this.lb_tmr2_sec.AutoSize = true;
            this.lb_tmr2_sec.Location = new System.Drawing.Point(87, 136);
            this.lb_tmr2_sec.Name = "lb_tmr2_sec";
            this.lb_tmr2_sec.Size = new System.Drawing.Size(58, 13);
            this.lb_tmr2_sec.TabIndex = 27;
            this.lb_tmr2_sec.Text = "Пауза..мс";
            this.lb_tmr2_sec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_tmr3_sec
            // 
            this.lb_tmr3_sec.AutoSize = true;
            this.lb_tmr3_sec.Location = new System.Drawing.Point(163, 136);
            this.lb_tmr3_sec.Name = "lb_tmr3_sec";
            this.lb_tmr3_sec.Size = new System.Drawing.Size(58, 13);
            this.lb_tmr3_sec.TabIndex = 28;
            this.lb_tmr3_sec.Text = "Пауза..мс";
            this.lb_tmr3_sec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_tmr4_sec
            // 
            this.lb_tmr4_sec.AutoSize = true;
            this.lb_tmr4_sec.Location = new System.Drawing.Point(237, 136);
            this.lb_tmr4_sec.Name = "lb_tmr4_sec";
            this.lb_tmr4_sec.Size = new System.Drawing.Size(58, 13);
            this.lb_tmr4_sec.TabIndex = 29;
            this.lb_tmr4_sec.Text = "Пауза..мс";
            this.lb_tmr4_sec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_trig_tmr1
            // 
            this.cb_trig_tmr1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_trig_tmr1.FormattingEnabled = true;
            this.cb_trig_tmr1.Items.AddRange(new object[] {
            "",
            "Shift",
            "Scroll L",
            "Caps L",
            "Num L"});
            this.cb_trig_tmr1.Location = new System.Drawing.Point(15, 15);
            this.cb_trig_tmr1.Name = "cb_trig_tmr1";
            this.cb_trig_tmr1.Size = new System.Drawing.Size(58, 21);
            this.cb_trig_tmr1.TabIndex = 31;
            // 
            // cb_trig_tmr2
            // 
            this.cb_trig_tmr2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_trig_tmr2.FormattingEnabled = true;
            this.cb_trig_tmr2.Items.AddRange(new object[] {
            "",
            "Shift",
            "Scroll L",
            "Caps L",
            "Num L"});
            this.cb_trig_tmr2.Location = new System.Drawing.Point(90, 15);
            this.cb_trig_tmr2.Name = "cb_trig_tmr2";
            this.cb_trig_tmr2.Size = new System.Drawing.Size(58, 21);
            this.cb_trig_tmr2.TabIndex = 32;
            // 
            // cb_trig_tmr3
            // 
            this.cb_trig_tmr3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_trig_tmr3.FormattingEnabled = true;
            this.cb_trig_tmr3.Items.AddRange(new object[] {
            "",
            "Shift",
            "Scroll L",
            "Caps L",
            "Num L"});
            this.cb_trig_tmr3.Location = new System.Drawing.Point(165, 15);
            this.cb_trig_tmr3.Name = "cb_trig_tmr3";
            this.cb_trig_tmr3.Size = new System.Drawing.Size(58, 21);
            this.cb_trig_tmr3.TabIndex = 33;
            // 
            // cb_trig_tmr4
            // 
            this.cb_trig_tmr4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_trig_tmr4.FormattingEnabled = true;
            this.cb_trig_tmr4.Items.AddRange(new object[] {
            "",
            "Shift",
            "Scroll L",
            "Caps L",
            "Num L"});
            this.cb_trig_tmr4.Location = new System.Drawing.Point(240, 15);
            this.cb_trig_tmr4.Name = "cb_trig_tmr4";
            this.cb_trig_tmr4.Size = new System.Drawing.Size(58, 21);
            this.cb_trig_tmr4.TabIndex = 34;
            // 
            // cb_key4
            // 
            this.cb_key4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_key4.FormattingEnabled = true;
            this.cb_key4.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "Q",
            "W",
            "E",
            "R",
            "A",
            "S",
            "D",
            "F",
            "Z",
            "X",
            "C",
            "V"});
            this.cb_key4.Location = new System.Drawing.Point(240, 63);
            this.cb_key4.Name = "cb_key4";
            this.cb_key4.Size = new System.Drawing.Size(58, 21);
            this.cb_key4.TabIndex = 44;
            // 
            // cb_key3
            // 
            this.cb_key3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_key3.FormattingEnabled = true;
            this.cb_key3.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "Q",
            "W",
            "E",
            "R",
            "A",
            "S",
            "D",
            "F",
            "Z",
            "X",
            "C",
            "V"});
            this.cb_key3.Location = new System.Drawing.Point(165, 63);
            this.cb_key3.Name = "cb_key3";
            this.cb_key3.Size = new System.Drawing.Size(58, 21);
            this.cb_key3.TabIndex = 43;
            // 
            // cb_key2
            // 
            this.cb_key2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_key2.FormattingEnabled = true;
            this.cb_key2.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "Q",
            "W",
            "E",
            "R",
            "A",
            "S",
            "D",
            "F",
            "Z",
            "X",
            "C",
            "V"});
            this.cb_key2.Location = new System.Drawing.Point(90, 63);
            this.cb_key2.Name = "cb_key2";
            this.cb_key2.Size = new System.Drawing.Size(58, 21);
            this.cb_key2.TabIndex = 42;
            // 
            // cb_key1
            // 
            this.cb_key1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_key1.FormattingEnabled = true;
            this.cb_key1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "Q",
            "W",
            "E",
            "R",
            "A",
            "S",
            "D",
            "F",
            "Z",
            "X",
            "C",
            "V"});
            this.cb_key1.Location = new System.Drawing.Point(15, 63);
            this.cb_key1.Name = "cb_key1";
            this.cb_key1.Size = new System.Drawing.Size(58, 21);
            this.cb_key1.TabIndex = 41;
            // 
            // lb_key4
            // 
            this.lb_key4.AutoSize = true;
            this.lb_key4.Location = new System.Drawing.Point(237, 87);
            this.lb_key4.Name = "lb_key4";
            this.lb_key4.Size = new System.Drawing.Size(61, 13);
            this.lb_key4.TabIndex = 39;
            this.lb_key4.Text = "Клавиша 4";
            // 
            // lb_key3
            // 
            this.lb_key3.AutoSize = true;
            this.lb_key3.Location = new System.Drawing.Point(162, 87);
            this.lb_key3.Name = "lb_key3";
            this.lb_key3.Size = new System.Drawing.Size(61, 13);
            this.lb_key3.TabIndex = 38;
            this.lb_key3.Text = "Клавиша 3";
            // 
            // lb_key2
            // 
            this.lb_key2.AutoSize = true;
            this.lb_key2.Location = new System.Drawing.Point(87, 87);
            this.lb_key2.Name = "lb_key2";
            this.lb_key2.Size = new System.Drawing.Size(61, 13);
            this.lb_key2.TabIndex = 37;
            this.lb_key2.Text = "Клавиша 2";
            // 
            // lb_key1
            // 
            this.lb_key1.AutoSize = true;
            this.lb_key1.Location = new System.Drawing.Point(12, 87);
            this.lb_key1.Name = "lb_key1";
            this.lb_key1.Size = new System.Drawing.Size(61, 13);
            this.lb_key1.TabIndex = 36;
            this.lb_key1.Text = "Клавиша 1";
            // 
            // lb_stop
            // 
            this.lb_stop.AutoSize = true;
            this.lb_stop.Location = new System.Drawing.Point(118, 208);
            this.lb_stop.Name = "lb_stop";
            this.lb_stop.Size = new System.Drawing.Size(80, 13);
            this.lb_stop.TabIndex = 46;
            this.lb_stop.Text = "Приостановка";
            // 
            // cb_pause
            // 
            this.cb_pause.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_pause.FormattingEnabled = true;
            this.cb_pause.Items.AddRange(new object[] {
            "",
            "T",
            "Enter",
            "T / Enter"});
            this.cb_pause.Location = new System.Drawing.Point(223, 205);
            this.cb_pause.Name = "cb_pause";
            this.cb_pause.Size = new System.Drawing.Size(71, 21);
            this.cb_pause.TabIndex = 47;
            this.cb_pause.SelectedIndexChanged += new System.EventHandler(this.cb_pause_SelectedIndexChanged);
            // 
            // mouseKeyEventProvider1
            // 
            this.mouseKeyEventProvider1.Enabled = true;
            this.mouseKeyEventProvider1.HookType = MouseKeyboardActivityMonitor.Controls.HookType.Global;
            this.mouseKeyEventProvider1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mouseKeyEventProvider1_KeyDown);
            // 
            // lb_auth
            // 
            this.lb_auth.AutoSize = true;
            this.lb_auth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_auth.Location = new System.Drawing.Point(204, 236);
            this.lb_auth.Name = "lb_auth";
            this.lb_auth.Size = new System.Drawing.Size(105, 13);
            this.lb_auth.TabIndex = 48;
            this.lb_auth.Text = "Автор: Dmitry Olenin";
            this.lb_auth.Click += new System.EventHandler(this.lb_auth_Click);
            this.lb_auth.MouseLeave += new System.EventHandler(this.lb_auth_MouseLeave);
            this.lb_auth.MouseHover += new System.EventHandler(this.lb_auth_MouseHover);
            // 
            // lb_lang
            // 
            this.lb_lang.AutoSize = true;
            this.lb_lang.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_lang.Location = new System.Drawing.Point(121, 235);
            this.lb_lang.Name = "lb_lang";
            this.lb_lang.Size = new System.Drawing.Size(26, 13);
            this.lb_lang.TabIndex = 49;
            this.lb_lang.Text = "Eng";
            this.lb_lang.Click += new System.EventHandler(this.lb_lang_Click);
            // 
            // d3hot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 250);
            this.Controls.Add(this.lb_lang);
            this.Controls.Add(this.lb_auth);
            this.Controls.Add(this.cb_pause);
            this.Controls.Add(this.lb_stop);
            this.Controls.Add(this.cb_key4);
            this.Controls.Add(this.cb_key3);
            this.Controls.Add(this.cb_key2);
            this.Controls.Add(this.cb_key1);
            this.Controls.Add(this.lb_key4);
            this.Controls.Add(this.lb_key3);
            this.Controls.Add(this.lb_key2);
            this.Controls.Add(this.lb_key1);
            this.Controls.Add(this.cb_trig_tmr4);
            this.Controls.Add(this.cb_trig_tmr3);
            this.Controls.Add(this.cb_trig_tmr2);
            this.Controls.Add(this.cb_trig_tmr1);
            this.Controls.Add(this.lb_tmr4_sec);
            this.Controls.Add(this.lb_tmr3_sec);
            this.Controls.Add(this.lb_tmr2_sec);
            this.Controls.Add(this.lb_tmr1_sec);
            this.Controls.Add(this.lb_trig4);
            this.Controls.Add(this.lb_trig3);
            this.Controls.Add(this.lb_trig2);
            this.Controls.Add(this.lb_trig1);
            this.Controls.Add(this.lb_about);
            this.Controls.Add(this.lb_area);
            this.Controls.Add(this.cb_prog);
            this.Controls.Add(this.cb_start);
            this.Controls.Add(this.nud_tmr4);
            this.Controls.Add(this.nud_tmr3);
            this.Controls.Add(this.nud_tmr2);
            this.Controls.Add(this.nud_tmr1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "d3hot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diablo 3 Hotkeys";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.d3hot_FormClosing);
            this.Load += new System.EventHandler(this.d3hot_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nud_tmr1;
        private System.Windows.Forms.NumericUpDown nud_tmr2;
        private System.Windows.Forms.NumericUpDown nud_tmr3;
        private System.Windows.Forms.NumericUpDown nud_tmr4;
        private System.Windows.Forms.CheckBox cb_start;
        private System.Windows.Forms.ComboBox cb_prog;
        private System.Windows.Forms.Label lb_area;
        private System.Windows.Forms.Label lb_about;
        private System.Windows.Forms.Label lb_trig1;
        private System.Windows.Forms.Label lb_trig2;
        private System.Windows.Forms.Label lb_trig3;
        private System.Windows.Forms.Label lb_trig4;
        private System.Windows.Forms.Label lb_tmr1_sec;
        private System.Windows.Forms.Label lb_tmr2_sec;
        private System.Windows.Forms.Label lb_tmr3_sec;
        private System.Windows.Forms.Label lb_tmr4_sec;
        private System.Windows.Forms.ComboBox cb_trig_tmr1;
        private System.Windows.Forms.ComboBox cb_trig_tmr2;
        private System.Windows.Forms.ComboBox cb_trig_tmr3;
        private System.Windows.Forms.ComboBox cb_trig_tmr4;
        private System.Windows.Forms.ComboBox cb_key4;
        private System.Windows.Forms.ComboBox cb_key3;
        private System.Windows.Forms.ComboBox cb_key2;
        private System.Windows.Forms.ComboBox cb_key1;
        private System.Windows.Forms.Label lb_key4;
        private System.Windows.Forms.Label lb_key3;
        private System.Windows.Forms.Label lb_key2;
        private System.Windows.Forms.Label lb_key1;
        private System.Windows.Forms.ToolTip tt_start;
        private System.Windows.Forms.Label lb_stop;
        private System.Windows.Forms.ComboBox cb_pause;
        private MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider mouseKeyEventProvider1;
        private System.Windows.Forms.Label lb_auth;
        private System.Windows.Forms.Label lb_lang;
    }
}

