﻿namespace D3Hot
{
    partial class D3Hotkeys
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                Exit();
                if (_tmrAll != null) _tmrAll.Dispose();
                if (_tmrCdr != null) _tmrCdr.Dispose();
                if (_tmrVer != null) _tmrVer.Dispose();
                if (_tmrHover != null) _tmrHover.Dispose();
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
            this.cb_start = new System.Windows.Forms.CheckBox();
            this.cb_prog = new System.Windows.Forms.ComboBox();
            this.lb_area = new System.Windows.Forms.Label();
            this.mouseKeyEventProvider1 = new MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider();
            this.lb_auth = new System.Windows.Forms.Label();
            this.lb_lang = new System.Windows.Forms.Label();
            this.cb_prof = new System.Windows.Forms.ComboBox();
            this.lb_prof = new System.Windows.Forms.Label();
            this.cb_startstop = new System.Windows.Forms.ComboBox();
            this.lb_startstop = new System.Windows.Forms.Label();
            this.lb_tp = new System.Windows.Forms.Label();
            this.cb_tp = new System.Windows.Forms.ComboBox();
            this.lb_tpdelay = new System.Windows.Forms.Label();
            this.cb_tpdelay = new System.Windows.Forms.ComboBox();
            this.cb_proc = new System.Windows.Forms.ComboBox();
            this.lb_proc = new System.Windows.Forms.Label();
            this.lb_lang_name = new System.Windows.Forms.Label();
            this.notify_d3h = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.b_opt = new System.Windows.Forms.Button();
            this.pan_opt = new System.Windows.Forms.Panel();
            this.chb_log = new System.Windows.Forms.CheckBox();
            this.lb_nud_coold = new System.Windows.Forms.Label();
            this.nud_coold = new System.Windows.Forms.NumericUpDown();
            this.lb_coold = new System.Windows.Forms.Label();
            this.cb_returndelay = new System.Windows.Forms.ComboBox();
            this.lb_returndelay = new System.Windows.Forms.Label();
            this.lb_ver_check = new System.Windows.Forms.Label();
            this.chb_ver_check = new System.Windows.Forms.CheckBox();
            this.chb_users = new System.Windows.Forms.CheckBox();
            this.cb_map = new System.Windows.Forms.ComboBox();
            this.lb_map = new System.Windows.Forms.Label();
            this.cb_mapdelay = new System.Windows.Forms.ComboBox();
            this.lb_mapdelay = new System.Windows.Forms.Label();
            this.lb_hot_prof = new System.Windows.Forms.Label();
            this.cb_hot_prof = new System.Windows.Forms.ComboBox();
            this.lb_nud_rand = new System.Windows.Forms.Label();
            this.nud_rand = new System.Windows.Forms.NumericUpDown();
            this.lb_rand = new System.Windows.Forms.Label();
            this.chb_saveload = new System.Windows.Forms.CheckBox();
            this.chb_mpress = new System.Windows.Forms.CheckBox();
            this.chb_hold = new System.Windows.Forms.CheckBox();
            this.lb_key_delay_desc = new System.Windows.Forms.Label();
            this.nud_key_delay_ms = new System.Windows.Forms.NumericUpDown();
            this.cb_key_delay = new System.Windows.Forms.ComboBox();
            this.lb_key_delay = new System.Windows.Forms.Label();
            this.lb_key_delay_ms = new System.Windows.Forms.Label();
            this.chb_mult = new System.Windows.Forms.CheckBox();
            this.chb_tray = new System.Windows.Forms.CheckBox();
            this.tt_key = new System.Windows.Forms.ToolTip(this.components);
            this.lb_hold = new System.Windows.Forms.Label();
            this.b_save = new System.Windows.Forms.Button();
            this.b_load = new System.Windows.Forms.Button();
            this.pan_hold = new System.Windows.Forms.Panel();
            this.tb_prof_name = new System.Windows.Forms.TextBox();
            this.pan_prof_name = new System.Windows.Forms.Panel();
            this.pan_proc = new System.Windows.Forms.Panel();
            this.pan_prog = new System.Windows.Forms.Panel();
            this.lb_debug = new System.Windows.Forms.Label();
            this.lb_num = new System.Windows.Forms.Label();
            this.lb_caps = new System.Windows.Forms.Label();
            this.lb_scroll = new System.Windows.Forms.Label();
            this.mouseKeyEventProvider2 = new MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lb_help = new System.Windows.Forms.Label();
            this.gb_set = new System.Windows.Forms.GroupBox();
            this.b_trig_ok = new System.Windows.Forms.Button();
            this.lb_trig_delay_ms = new System.Windows.Forms.Label();
            this.lb_trig_time_ms = new System.Windows.Forms.Label();
            this.lb_trig_delay = new System.Windows.Forms.Label();
            this.nud_trig_delay = new System.Windows.Forms.NumericUpDown();
            this.cb_trig_once = new System.Windows.Forms.CheckBox();
            this.lb_trig_enable = new System.Windows.Forms.Label();
            this.cb_trig_enable = new System.Windows.Forms.ComboBox();
            this.lb_trig_time = new System.Windows.Forms.Label();
            this.nud_trig_time = new System.Windows.Forms.NumericUpDown();
            this.pan_press_type = new System.Windows.Forms.Panel();
            this.cb_press_type = new System.Windows.Forms.ComboBox();
            this.lb_press_type = new System.Windows.Forms.Label();
            this.pan_set = new System.Windows.Forms.Panel();
            this.cb_tmr3 = new System.Windows.Forms.ComboBox();
            this.cb_tmr4 = new System.Windows.Forms.ComboBox();
            this.cb_tmr5 = new System.Windows.Forms.ComboBox();
            this.cb_tmr6 = new System.Windows.Forms.ComboBox();
            this.cb_tmr2 = new System.Windows.Forms.ComboBox();
            this.cb_tmr1 = new System.Windows.Forms.ComboBox();
            this.cb_key3 = new System.Windows.Forms.ComboBox();
            this.cb_key4 = new System.Windows.Forms.ComboBox();
            this.cb_key2 = new System.Windows.Forms.ComboBox();
            this.cb_key1 = new System.Windows.Forms.ComboBox();
            this.lb_key4 = new System.Windows.Forms.Label();
            this.lb_key3 = new System.Windows.Forms.Label();
            this.nud_tmr5 = new System.Windows.Forms.NumericUpDown();
            this.lb_key2 = new System.Windows.Forms.Label();
            this.lb_trig5 = new System.Windows.Forms.Label();
            this.lb_key1 = new System.Windows.Forms.Label();
            this.lb_tmr5_sec = new System.Windows.Forms.Label();
            this.cb_trig_tmr4 = new System.Windows.Forms.ComboBox();
            this.cb_trig_tmr5 = new System.Windows.Forms.ComboBox();
            this.cb_trig_tmr3 = new System.Windows.Forms.ComboBox();
            this.lb_key5 = new System.Windows.Forms.Label();
            this.cb_trig_tmr2 = new System.Windows.Forms.ComboBox();
            this.cb_key5 = new System.Windows.Forms.ComboBox();
            this.lb_tmr4_sec = new System.Windows.Forms.Label();
            this.nud_tmr6 = new System.Windows.Forms.NumericUpDown();
            this.lb_tmr3_sec = new System.Windows.Forms.Label();
            this.lb_trig6 = new System.Windows.Forms.Label();
            this.lb_tmr2_sec = new System.Windows.Forms.Label();
            this.lb_tmr6_sec = new System.Windows.Forms.Label();
            this.lb_tmr1_sec = new System.Windows.Forms.Label();
            this.cb_trig_tmr6 = new System.Windows.Forms.ComboBox();
            this.lb_trig4 = new System.Windows.Forms.Label();
            this.lb_trig3 = new System.Windows.Forms.Label();
            this.lb_key6 = new System.Windows.Forms.Label();
            this.lb_trig2 = new System.Windows.Forms.Label();
            this.cb_key6 = new System.Windows.Forms.ComboBox();
            this.lb_trig1 = new System.Windows.Forms.Label();
            this.nud_tmr4 = new System.Windows.Forms.NumericUpDown();
            this.nud_tmr3 = new System.Windows.Forms.NumericUpDown();
            this.nud_tmr2 = new System.Windows.Forms.NumericUpDown();
            this.nud_tmr1 = new System.Windows.Forms.NumericUpDown();
            this.cb_trig_tmr1 = new System.Windows.Forms.ComboBox();
            this.chb_trig1 = new System.Windows.Forms.CheckBox();
            this.chb_trig2 = new System.Windows.Forms.CheckBox();
            this.chb_trig3 = new System.Windows.Forms.CheckBox();
            this.chb_trig4 = new System.Windows.Forms.CheckBox();
            this.chb_trig5 = new System.Windows.Forms.CheckBox();
            this.chb_trig6 = new System.Windows.Forms.CheckBox();
            this.pan_main = new System.Windows.Forms.Panel();
            this.contextMenuStrip1.SuspendLayout();
            this.pan_opt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_coold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_rand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_key_delay_ms)).BeginInit();
            this.pan_hold.SuspendLayout();
            this.pan_prof_name.SuspendLayout();
            this.pan_proc.SuspendLayout();
            this.pan_prog.SuspendLayout();
            this.gb_set.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_trig_delay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_trig_time)).BeginInit();
            this.pan_press_type.SuspendLayout();
            this.pan_set.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr1)).BeginInit();
            this.pan_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_start
            // 
            this.cb_start.Appearance = System.Windows.Forms.Appearance.Button;
            this.cb_start.Location = new System.Drawing.Point(12, 211);
            this.cb_start.Name = "cb_start";
            this.cb_start.Size = new System.Drawing.Size(97, 89);
            this.cb_start.TabIndex = 12;
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
            this.cb_prog.Location = new System.Drawing.Point(100, 3);
            this.cb_prog.Name = "cb_prog";
            this.cb_prog.Size = new System.Drawing.Size(100, 21);
            this.cb_prog.TabIndex = 15;
            this.cb_prog.SelectedIndexChanged += new System.EventHandler(this.cb_prog_SelectedIndexChanged);
            this.cb_prog.SelectionChangeCommitted += new System.EventHandler(this.cb_prog_SelectionChangeCommitted);
            // 
            // lb_area
            // 
            this.lb_area.AutoSize = true;
            this.lb_area.BackColor = System.Drawing.Color.Transparent;
            this.lb_area.Location = new System.Drawing.Point(-3, 6);
            this.lb_area.Name = "lb_area";
            this.lb_area.Size = new System.Drawing.Size(100, 13);
            this.lb_area.TabIndex = 18;
            this.lb_area.Text = "Область действия";
            // 
            // mouseKeyEventProvider1
            // 
            this.mouseKeyEventProvider1.Enabled = false;
            this.mouseKeyEventProvider1.HookType = MouseKeyboardActivityMonitor.Controls.HookType.Global;
            this.mouseKeyEventProvider1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseKeyEventProvider1_MouseDown);
            this.mouseKeyEventProvider1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mouseKeyEventProvider1_KeyUp);
            this.mouseKeyEventProvider1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mouseKeyEventProvider1_KeyDown);
            // 
            // lb_auth
            // 
            this.lb_auth.AutoSize = true;
            this.lb_auth.BackColor = System.Drawing.Color.Transparent;
            this.lb_auth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_auth.Location = new System.Drawing.Point(352, 299);
            this.lb_auth.Name = "lb_auth";
            this.lb_auth.Size = new System.Drawing.Size(105, 13);
            this.lb_auth.TabIndex = 17;
            this.lb_auth.Text = "Автор: Dmitry Olenin";
            this.lb_auth.Click += new System.EventHandler(this.lb_auth_Click);
            this.lb_auth.MouseLeave += new System.EventHandler(this.lb_auth_MouseLeave);
            this.lb_auth.MouseHover += new System.EventHandler(this.lb_auth_MouseHover);
            // 
            // lb_lang
            // 
            this.lb_lang.AutoSize = true;
            this.lb_lang.BackColor = System.Drawing.Color.Transparent;
            this.lb_lang.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_lang.Location = new System.Drawing.Point(408, 175);
            this.lb_lang.Name = "lb_lang";
            this.lb_lang.Size = new System.Drawing.Size(26, 13);
            this.lb_lang.TabIndex = 16;
            this.lb_lang.Text = "Eng";
            this.lb_lang.Click += new System.EventHandler(this.lb_lang_Click);
            // 
            // cb_prof
            // 
            this.cb_prof.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_prof.FormattingEnabled = true;
            this.cb_prof.Items.AddRange(new object[] {
            "",
            "Profile 1",
            "Profile 2",
            "Profile 3",
            "Profile 4",
            "Profile 5",
            "Profile 6",
            "Profile 7",
            "Profile 8",
            "Profile 9"});
            this.cb_prof.Location = new System.Drawing.Point(225, 211);
            this.cb_prof.Name = "cb_prof";
            this.cb_prof.Size = new System.Drawing.Size(100, 21);
            this.cb_prof.TabIndex = 13;
            this.cb_prof.SelectionChangeCommitted += new System.EventHandler(this.cb_prof_SelectionChangeCommitted);
            // 
            // lb_prof
            // 
            this.lb_prof.AutoSize = true;
            this.lb_prof.BackColor = System.Drawing.Color.Transparent;
            this.lb_prof.Location = new System.Drawing.Point(120, 214);
            this.lb_prof.Name = "lb_prof";
            this.lb_prof.Size = new System.Drawing.Size(53, 13);
            this.lb_prof.TabIndex = 50;
            this.lb_prof.Text = "Профиль";
            // 
            // cb_startstop
            // 
            this.cb_startstop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_startstop.FormattingEnabled = true;
            this.cb_startstop.Items.AddRange(new object[] {
            "",
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "F9",
            "F10",
            "F11"});
            this.cb_startstop.Location = new System.Drawing.Point(87, 4);
            this.cb_startstop.Name = "cb_startstop";
            this.cb_startstop.Size = new System.Drawing.Size(57, 21);
            this.cb_startstop.TabIndex = 63;
            this.cb_startstop.SelectedIndexChanged += new System.EventHandler(this.cb_startstop_SelectedIndexChanged);
            this.cb_startstop.SelectionChangeCommitted += new System.EventHandler(this.cb_op_SelectionChangeCommitted);
            // 
            // lb_startstop
            // 
            this.lb_startstop.AutoSize = true;
            this.lb_startstop.Location = new System.Drawing.Point(1, 7);
            this.lb_startstop.Name = "lb_startstop";
            this.lb_startstop.Size = new System.Drawing.Size(74, 13);
            this.lb_startstop.TabIndex = 64;
            this.lb_startstop.Text = "Хоткей старт";
            // 
            // lb_tp
            // 
            this.lb_tp.AutoSize = true;
            this.lb_tp.Location = new System.Drawing.Point(2, 61);
            this.lb_tp.Name = "lb_tp";
            this.lb_tp.Size = new System.Drawing.Size(57, 13);
            this.lb_tp.TabIndex = 66;
            this.lb_tp.Text = "Хоткей тп";
            // 
            // cb_tp
            // 
            this.cb_tp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_tp.FormattingEnabled = true;
            this.cb_tp.Items.AddRange(new object[] {
            "",
            "T",
            "Y",
            "U",
            "I",
            "O",
            "P",
            "G",
            "H",
            "J",
            "K",
            "L",
            "B",
            "N",
            "M"});
            this.cb_tp.Location = new System.Drawing.Point(87, 58);
            this.cb_tp.Name = "cb_tp";
            this.cb_tp.Size = new System.Drawing.Size(57, 21);
            this.cb_tp.TabIndex = 65;
            this.cb_tp.SelectedIndexChanged += new System.EventHandler(this.cb_tp_SelectedIndexChanged);
            this.cb_tp.SelectionChangeCommitted += new System.EventHandler(this.cb_op_SelectionChangeCommitted);
            this.cb_tp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            // 
            // lb_tpdelay
            // 
            this.lb_tpdelay.AutoSize = true;
            this.lb_tpdelay.Location = new System.Drawing.Point(1, 88);
            this.lb_tpdelay.Name = "lb_tpdelay";
            this.lb_tpdelay.Size = new System.Drawing.Size(52, 13);
            this.lb_tpdelay.TabIndex = 68;
            this.lb_tpdelay.Text = "Пауза тп";
            // 
            // cb_tpdelay
            // 
            this.cb_tpdelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_tpdelay.FormattingEnabled = true;
            this.cb_tpdelay.Items.AddRange(new object[] {
            "",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cb_tpdelay.Location = new System.Drawing.Point(87, 85);
            this.cb_tpdelay.Name = "cb_tpdelay";
            this.cb_tpdelay.Size = new System.Drawing.Size(57, 21);
            this.cb_tpdelay.TabIndex = 67;
            this.cb_tpdelay.SelectedIndexChanged += new System.EventHandler(this.cb_tpdelay_SelectedIndexChanged);
            this.cb_tpdelay.SelectionChangeCommitted += new System.EventHandler(this.cb_op_SelectionChangeCommitted);
            // 
            // cb_proc
            // 
            this.cb_proc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_proc.FormattingEnabled = true;
            this.cb_proc.Location = new System.Drawing.Point(100, 3);
            this.cb_proc.Name = "cb_proc";
            this.cb_proc.Size = new System.Drawing.Size(100, 21);
            this.cb_proc.TabIndex = 70;
            this.cb_proc.SelectionChangeCommitted += new System.EventHandler(this.cb_proc_SelectionChangeCommitted);
            this.cb_proc.Click += new System.EventHandler(this.cb_proc_Click);
            // 
            // lb_proc
            // 
            this.lb_proc.AutoSize = true;
            this.lb_proc.BackColor = System.Drawing.Color.Transparent;
            this.lb_proc.Location = new System.Drawing.Point(-3, 6);
            this.lb_proc.Name = "lb_proc";
            this.lb_proc.Size = new System.Drawing.Size(51, 13);
            this.lb_proc.TabIndex = 71;
            this.lb_proc.Text = "Процесс";
            // 
            // lb_lang_name
            // 
            this.lb_lang_name.AutoSize = true;
            this.lb_lang_name.BackColor = System.Drawing.Color.Transparent;
            this.lb_lang_name.Location = new System.Drawing.Point(341, 175);
            this.lb_lang_name.Name = "lb_lang_name";
            this.lb_lang_name.Size = new System.Drawing.Size(61, 13);
            this.lb_lang_name.TabIndex = 72;
            this.lb_lang_name.Text = "Language: ";
            // 
            // notify_d3h
            // 
            this.notify_d3h.ContextMenuStrip = this.contextMenuStrip1;
            this.notify_d3h.Text = "D3Hotkeys";
            this.notify_d3h.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // b_opt
            // 
            this.b_opt.Location = new System.Drawing.Point(344, 218);
            this.b_opt.Name = "b_opt";
            this.b_opt.Size = new System.Drawing.Size(104, 64);
            this.b_opt.TabIndex = 73;
            this.b_opt.Text = "Настройки";
            this.b_opt.UseVisualStyleBackColor = true;
            this.b_opt.Click += new System.EventHandler(this.b_opt_Click);
            // 
            // pan_opt
            // 
            this.pan_opt.BackColor = System.Drawing.Color.Transparent;
            this.pan_opt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pan_opt.Controls.Add(this.chb_log);
            this.pan_opt.Controls.Add(this.lb_nud_coold);
            this.pan_opt.Controls.Add(this.nud_coold);
            this.pan_opt.Controls.Add(this.lb_coold);
            this.pan_opt.Controls.Add(this.cb_returndelay);
            this.pan_opt.Controls.Add(this.lb_returndelay);
            this.pan_opt.Controls.Add(this.lb_ver_check);
            this.pan_opt.Controls.Add(this.chb_ver_check);
            this.pan_opt.Controls.Add(this.chb_users);
            this.pan_opt.Controls.Add(this.cb_map);
            this.pan_opt.Controls.Add(this.lb_map);
            this.pan_opt.Controls.Add(this.cb_mapdelay);
            this.pan_opt.Controls.Add(this.lb_mapdelay);
            this.pan_opt.Controls.Add(this.lb_hot_prof);
            this.pan_opt.Controls.Add(this.cb_hot_prof);
            this.pan_opt.Controls.Add(this.lb_nud_rand);
            this.pan_opt.Controls.Add(this.nud_rand);
            this.pan_opt.Controls.Add(this.lb_rand);
            this.pan_opt.Controls.Add(this.chb_saveload);
            this.pan_opt.Controls.Add(this.chb_mpress);
            this.pan_opt.Controls.Add(this.chb_hold);
            this.pan_opt.Controls.Add(this.lb_key_delay_desc);
            this.pan_opt.Controls.Add(this.nud_key_delay_ms);
            this.pan_opt.Controls.Add(this.cb_key_delay);
            this.pan_opt.Controls.Add(this.lb_key_delay);
            this.pan_opt.Controls.Add(this.lb_key_delay_ms);
            this.pan_opt.Controls.Add(this.chb_mult);
            this.pan_opt.Controls.Add(this.chb_tray);
            this.pan_opt.Controls.Add(this.lb_startstop);
            this.pan_opt.Controls.Add(this.cb_startstop);
            this.pan_opt.Controls.Add(this.cb_tp);
            this.pan_opt.Controls.Add(this.lb_tp);
            this.pan_opt.Controls.Add(this.cb_tpdelay);
            this.pan_opt.Controls.Add(this.lb_tpdelay);
            this.pan_opt.Location = new System.Drawing.Point(2, 8);
            this.pan_opt.Name = "pan_opt";
            this.pan_opt.Size = new System.Drawing.Size(455, 165);
            this.pan_opt.TabIndex = 74;
            this.pan_opt.Visible = false;
            // 
            // chb_log
            // 
            this.chb_log.AutoSize = true;
            this.chb_log.Location = new System.Drawing.Point(330, 146);
            this.chb_log.Name = "chb_log";
            this.chb_log.Size = new System.Drawing.Size(93, 17);
            this.chb_log.TabIndex = 96;
            this.chb_log.Text = "Логирование";
            this.chb_log.UseVisualStyleBackColor = true;
            this.chb_log.CheckedChanged += new System.EventHandler(this.cb_users_CheckedChanged);
            // 
            // lb_nud_coold
            // 
            this.lb_nud_coold.AutoSize = true;
            this.lb_nud_coold.Location = new System.Drawing.Point(255, 122);
            this.lb_nud_coold.Name = "lb_nud_coold";
            this.lb_nud_coold.Size = new System.Drawing.Size(58, 13);
            this.lb_nud_coold.TabIndex = 95;
            this.lb_nud_coold.Text = "Пауза..мс";
            this.lb_nud_coold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nud_coold
            // 
            this.nud_coold.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nud_coold.Location = new System.Drawing.Point(257, 102);
            this.nud_coold.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nud_coold.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nud_coold.Name = "nud_coold";
            this.nud_coold.Size = new System.Drawing.Size(63, 20);
            this.nud_coold.TabIndex = 94;
            this.nud_coold.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nud_coold.ValueChanged += new System.EventHandler(this.nud_Leave);
            this.nud_coold.Leave += new System.EventHandler(this.nud_Leave);
            // 
            // lb_coold
            // 
            this.lb_coold.AutoSize = true;
            this.lb_coold.Location = new System.Drawing.Point(145, 103);
            this.lb_coold.Name = "lb_coold";
            this.lb_coold.Size = new System.Drawing.Size(107, 13);
            this.lb_coold.TabIndex = 93;
            this.lb_coold.Text = "Задержка кулдауна";
            // 
            // cb_returndelay
            // 
            this.cb_returndelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_returndelay.FormattingEnabled = true;
            this.cb_returndelay.Items.AddRange(new object[] {
            "",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40"});
            this.cb_returndelay.Location = new System.Drawing.Point(87, 31);
            this.cb_returndelay.Name = "cb_returndelay";
            this.cb_returndelay.Size = new System.Drawing.Size(57, 21);
            this.cb_returndelay.TabIndex = 91;
            this.cb_returndelay.SelectedIndexChanged += new System.EventHandler(this.cb_returndelay_SelectedIndexChanged);
            this.cb_returndelay.SelectionChangeCommitted += new System.EventHandler(this.cb_op_SelectionChangeCommitted);
            // 
            // lb_returndelay
            // 
            this.lb_returndelay.AutoSize = true;
            this.lb_returndelay.Location = new System.Drawing.Point(1, 34);
            this.lb_returndelay.Name = "lb_returndelay";
            this.lb_returndelay.Size = new System.Drawing.Size(66, 13);
            this.lb_returndelay.TabIndex = 92;
            this.lb_returndelay.Text = "Пауза Enter";
            // 
            // lb_ver_check
            // 
            this.lb_ver_check.AutoSize = true;
            this.lb_ver_check.BackColor = System.Drawing.Color.Transparent;
            this.lb_ver_check.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_ver_check.Location = new System.Drawing.Point(345, 128);
            this.lb_ver_check.Name = "lb_ver_check";
            this.lb_ver_check.Size = new System.Drawing.Size(96, 13);
            this.lb_ver_check.TabIndex = 86;
            this.lb_ver_check.Text = "Проверка версии";
            this.lb_ver_check.Click += new System.EventHandler(this.lb_ver_check_Click);
            this.lb_ver_check.MouseLeave += new System.EventHandler(this.lb_auth_MouseLeave);
            this.lb_ver_check.MouseHover += new System.EventHandler(this.lb_auth_MouseHover);
            // 
            // chb_ver_check
            // 
            this.chb_ver_check.AutoSize = true;
            this.chb_ver_check.Location = new System.Drawing.Point(330, 128);
            this.chb_ver_check.Name = "chb_ver_check";
            this.chb_ver_check.Size = new System.Drawing.Size(15, 14);
            this.chb_ver_check.TabIndex = 90;
            this.chb_ver_check.UseVisualStyleBackColor = true;
            this.chb_ver_check.CheckedChanged += new System.EventHandler(this.cb_users_CheckedChanged);
            // 
            // chb_users
            // 
            this.chb_users.AutoSize = true;
            this.chb_users.Location = new System.Drawing.Point(330, 107);
            this.chb_users.Name = "chb_users";
            this.chb_users.Size = new System.Drawing.Size(112, 17);
            this.chb_users.TabIndex = 89;
            this.chb_users.Text = "Персонализация";
            this.chb_users.UseVisualStyleBackColor = true;
            this.chb_users.CheckedChanged += new System.EventHandler(this.cb_users_CheckedChanged);
            // 
            // cb_map
            // 
            this.cb_map.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_map.FormattingEnabled = true;
            this.cb_map.Items.AddRange(new object[] {
            "",
            "T",
            "Y",
            "U",
            "I",
            "O",
            "P",
            "G",
            "H",
            "J",
            "K",
            "L",
            "B",
            "N",
            "M"});
            this.cb_map.Location = new System.Drawing.Point(87, 112);
            this.cb_map.Name = "cb_map";
            this.cb_map.Size = new System.Drawing.Size(57, 21);
            this.cb_map.TabIndex = 85;
            this.cb_map.SelectedIndexChanged += new System.EventHandler(this.cb_map_SelectedIndexChanged);
            this.cb_map.SelectionChangeCommitted += new System.EventHandler(this.cb_op_SelectionChangeCommitted);
            this.cb_map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            // 
            // lb_map
            // 
            this.lb_map.AutoSize = true;
            this.lb_map.Location = new System.Drawing.Point(1, 115);
            this.lb_map.Name = "lb_map";
            this.lb_map.Size = new System.Drawing.Size(75, 13);
            this.lb_map.TabIndex = 86;
            this.lb_map.Text = "Хоткей карта";
            // 
            // cb_mapdelay
            // 
            this.cb_mapdelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mapdelay.FormattingEnabled = true;
            this.cb_mapdelay.Items.AddRange(new object[] {
            "",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cb_mapdelay.Location = new System.Drawing.Point(87, 139);
            this.cb_mapdelay.Name = "cb_mapdelay";
            this.cb_mapdelay.Size = new System.Drawing.Size(57, 21);
            this.cb_mapdelay.TabIndex = 87;
            this.cb_mapdelay.SelectedIndexChanged += new System.EventHandler(this.cb_mapdelay_SelectedIndexChanged);
            this.cb_mapdelay.SelectionChangeCommitted += new System.EventHandler(this.cb_mapdelay_SelectionChangeCommitted);
            // 
            // lb_mapdelay
            // 
            this.lb_mapdelay.AutoSize = true;
            this.lb_mapdelay.Location = new System.Drawing.Point(1, 142);
            this.lb_mapdelay.Name = "lb_mapdelay";
            this.lb_mapdelay.Size = new System.Drawing.Size(70, 13);
            this.lb_mapdelay.TabIndex = 88;
            this.lb_mapdelay.Text = "Пауза карта";
            // 
            // lb_hot_prof
            // 
            this.lb_hot_prof.AutoSize = true;
            this.lb_hot_prof.Location = new System.Drawing.Point(145, 142);
            this.lb_hot_prof.Name = "lb_hot_prof";
            this.lb_hot_prof.Size = new System.Drawing.Size(96, 13);
            this.lb_hot_prof.TabIndex = 84;
            this.lb_hot_prof.Text = "Хоткеи профилей";
            // 
            // cb_hot_prof
            // 
            this.cb_hot_prof.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_hot_prof.FormattingEnabled = true;
            this.cb_hot_prof.Items.AddRange(new object[] {
            "",
            "F1/F2/F3",
            "F2/F3/F4",
            "F3/F4/F5",
            "F4/F5/F6",
            "F5/F6/F7",
            "F6/F7/F8",
            "F7/F8/F9",
            "F8/F9/F10",
            "F9/F10/F11"});
            this.cb_hot_prof.Location = new System.Drawing.Point(257, 139);
            this.cb_hot_prof.Name = "cb_hot_prof";
            this.cb_hot_prof.Size = new System.Drawing.Size(63, 21);
            this.cb_hot_prof.TabIndex = 83;
            this.cb_hot_prof.SelectedIndexChanged += new System.EventHandler(this.cb_hot_prof_SelectedIndexChanged);
            this.cb_hot_prof.SelectionChangeCommitted += new System.EventHandler(this.cb_hot_prof_SelectionChangeCommitted);
            // 
            // lb_nud_rand
            // 
            this.lb_nud_rand.AutoSize = true;
            this.lb_nud_rand.Location = new System.Drawing.Point(255, 87);
            this.lb_nud_rand.Name = "lb_nud_rand";
            this.lb_nud_rand.Size = new System.Drawing.Size(58, 13);
            this.lb_nud_rand.TabIndex = 82;
            this.lb_nud_rand.Text = "Пауза..мс";
            this.lb_nud_rand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nud_rand
            // 
            this.nud_rand.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nud_rand.Location = new System.Drawing.Point(257, 67);
            this.nud_rand.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_rand.Name = "nud_rand";
            this.nud_rand.Size = new System.Drawing.Size(63, 20);
            this.nud_rand.TabIndex = 81;
            this.nud_rand.ValueChanged += new System.EventHandler(this.nud_Leave);
            this.nud_rand.Leave += new System.EventHandler(this.nud_Leave);
            // 
            // lb_rand
            // 
            this.lb_rand.AutoSize = true;
            this.lb_rand.Location = new System.Drawing.Point(145, 68);
            this.lb_rand.Name = "lb_rand";
            this.lb_rand.Size = new System.Drawing.Size(113, 13);
            this.lb_rand.TabIndex = 80;
            this.lb_rand.Text = "Случайная задержка";
            // 
            // chb_saveload
            // 
            this.chb_saveload.AutoSize = true;
            this.chb_saveload.Location = new System.Drawing.Point(330, 86);
            this.chb_saveload.Name = "chb_saveload";
            this.chb_saveload.Size = new System.Drawing.Size(80, 17);
            this.chb_saveload.TabIndex = 79;
            this.chb_saveload.Text = "Save/Load";
            this.chb_saveload.UseVisualStyleBackColor = true;
            this.chb_saveload.CheckedChanged += new System.EventHandler(this.chb_saveload_CheckedChanged);
            // 
            // chb_mpress
            // 
            this.chb_mpress.AutoSize = true;
            this.chb_mpress.Location = new System.Drawing.Point(330, 65);
            this.chb_mpress.Name = "chb_mpress";
            this.chb_mpress.Size = new System.Drawing.Size(106, 17);
            this.chb_mpress.TabIndex = 78;
            this.chb_mpress.Text = "Мультинажатие";
            this.chb_mpress.UseVisualStyleBackColor = true;
            this.chb_mpress.CheckedChanged += new System.EventHandler(this.cb_op_SelectionChangeCommitted);
            // 
            // chb_hold
            // 
            this.chb_hold.AutoSize = true;
            this.chb_hold.Checked = true;
            this.chb_hold.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_hold.Location = new System.Drawing.Point(330, 44);
            this.chb_hold.Name = "chb_hold";
            this.chb_hold.Size = new System.Drawing.Size(118, 17);
            this.chb_hold.TabIndex = 77;
            this.chb_hold.Text = "Процесс/зажатие";
            this.chb_hold.UseVisualStyleBackColor = true;
            this.chb_hold.CheckedChanged += new System.EventHandler(this.chb_hold_CheckedChanged);
            // 
            // lb_key_delay_desc
            // 
            this.lb_key_delay_desc.AutoSize = true;
            this.lb_key_delay_desc.Location = new System.Drawing.Point(255, 52);
            this.lb_key_delay_desc.Name = "lb_key_delay_desc";
            this.lb_key_delay_desc.Size = new System.Drawing.Size(58, 13);
            this.lb_key_delay_desc.TabIndex = 76;
            this.lb_key_delay_desc.Text = "Пауза..мс";
            this.lb_key_delay_desc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nud_key_delay_ms
            // 
            this.nud_key_delay_ms.Enabled = false;
            this.nud_key_delay_ms.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_key_delay_ms.Location = new System.Drawing.Point(257, 32);
            this.nud_key_delay_ms.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.nud_key_delay_ms.Name = "nud_key_delay_ms";
            this.nud_key_delay_ms.Size = new System.Drawing.Size(63, 20);
            this.nud_key_delay_ms.TabIndex = 75;
            this.nud_key_delay_ms.ValueChanged += new System.EventHandler(this.nud_Leave);
            this.nud_key_delay_ms.Leave += new System.EventHandler(this.nud_Leave);
            // 
            // cb_key_delay
            // 
            this.cb_key_delay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_key_delay.FormattingEnabled = true;
            this.cb_key_delay.Items.AddRange(new object[] {
            "",
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
            "V",
            "Space",
            "LMouse",
            "RMouse"});
            this.cb_key_delay.Location = new System.Drawing.Point(257, 4);
            this.cb_key_delay.Name = "cb_key_delay";
            this.cb_key_delay.Size = new System.Drawing.Size(63, 21);
            this.cb_key_delay.TabIndex = 71;
            this.cb_key_delay.SelectedIndexChanged += new System.EventHandler(this.cb_key_delay_SelectedIndexChanged);
            this.cb_key_delay.SelectionChangeCommitted += new System.EventHandler(this.cb_op_SelectionChangeCommitted);
            this.cb_key_delay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            // 
            // lb_key_delay
            // 
            this.lb_key_delay.AutoSize = true;
            this.lb_key_delay.Location = new System.Drawing.Point(145, 7);
            this.lb_key_delay.Name = "lb_key_delay";
            this.lb_key_delay.Size = new System.Drawing.Size(99, 13);
            this.lb_key_delay.TabIndex = 72;
            this.lb_key_delay.Text = "Клавиша с паузой";
            // 
            // lb_key_delay_ms
            // 
            this.lb_key_delay_ms.AutoSize = true;
            this.lb_key_delay_ms.Location = new System.Drawing.Point(145, 33);
            this.lb_key_delay_ms.Name = "lb_key_delay_ms";
            this.lb_key_delay_ms.Size = new System.Drawing.Size(105, 13);
            this.lb_key_delay_ms.TabIndex = 74;
            this.lb_key_delay_ms.Text = "Задержка клавиши";
            // 
            // chb_mult
            // 
            this.chb_mult.AutoSize = true;
            this.chb_mult.Location = new System.Drawing.Point(330, 2);
            this.chb_mult.Name = "chb_mult";
            this.chb_mult.Size = new System.Drawing.Size(98, 17);
            this.chb_mult.TabIndex = 70;
            this.chb_mult.Text = "Мультизапуск";
            this.chb_mult.UseVisualStyleBackColor = true;
            this.chb_mult.CheckedChanged += new System.EventHandler(this.cb_op_SelectionChangeCommitted);
            // 
            // chb_tray
            // 
            this.chb_tray.AutoSize = true;
            this.chb_tray.Location = new System.Drawing.Point(330, 23);
            this.chb_tray.Name = "chb_tray";
            this.chb_tray.Size = new System.Drawing.Size(126, 17);
            this.chb_tray.TabIndex = 69;
            this.chb_tray.Text = "Сворачивать в трей";
            this.chb_tray.UseVisualStyleBackColor = true;
            this.chb_tray.CheckedChanged += new System.EventHandler(this.cb_op_SelectionChangeCommitted);
            // 
            // lb_hold
            // 
            this.lb_hold.AutoSize = true;
            this.lb_hold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_hold.Location = new System.Drawing.Point(-3, 0);
            this.lb_hold.Name = "lb_hold";
            this.lb_hold.Size = new System.Drawing.Size(315, 13);
            this.lb_hold.TabIndex = 76;
            this.lb_hold.Text = "Выберите процесс для передачи зажатой клавиши.";
            // 
            // b_save
            // 
            this.b_save.Location = new System.Drawing.Point(225, 211);
            this.b_save.Name = "b_save";
            this.b_save.Size = new System.Drawing.Size(45, 22);
            this.b_save.TabIndex = 77;
            this.b_save.Text = "Save";
            this.b_save.UseVisualStyleBackColor = true;
            this.b_save.Visible = false;
            this.b_save.Click += new System.EventHandler(this.tsmi_save_Click);
            // 
            // b_load
            // 
            this.b_load.Location = new System.Drawing.Point(280, 211);
            this.b_load.Name = "b_load";
            this.b_load.Size = new System.Drawing.Size(45, 22);
            this.b_load.TabIndex = 78;
            this.b_load.Text = "Load";
            this.b_load.UseVisualStyleBackColor = true;
            this.b_load.Visible = false;
            this.b_load.Click += new System.EventHandler(this.tsmi_load_Click);
            // 
            // pan_hold
            // 
            this.pan_hold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pan_hold.Controls.Add(this.lb_hold);
            this.pan_hold.Location = new System.Drawing.Point(12, 180);
            this.pan_hold.Name = "pan_hold";
            this.pan_hold.Size = new System.Drawing.Size(310, 18);
            this.pan_hold.TabIndex = 79;
            this.pan_hold.Visible = false;
            // 
            // tb_prof_name
            // 
            this.tb_prof_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_prof_name.ForeColor = System.Drawing.Color.Maroon;
            this.tb_prof_name.Location = new System.Drawing.Point(0, 0);
            this.tb_prof_name.MaxLength = 25;
            this.tb_prof_name.Name = "tb_prof_name";
            this.tb_prof_name.Size = new System.Drawing.Size(200, 20);
            this.tb_prof_name.TabIndex = 80;
            this.tb_prof_name.Text = "Имя профиля";
            this.tb_prof_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_prof_name.TextChanged += new System.EventHandler(this.tb_prof_name_TextChanged);
            this.tb_prof_name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_prof_name_KeyDown);
            this.tb_prof_name.Leave += new System.EventHandler(this.tb_prof_name_Leave);
            this.tb_prof_name.MouseHover += new System.EventHandler(this.tb_prof_name_MouseHover);
            // 
            // pan_prof_name
            // 
            this.pan_prof_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pan_prof_name.Controls.Add(this.tb_prof_name);
            this.pan_prof_name.Location = new System.Drawing.Point(124, 176);
            this.pan_prof_name.Name = "pan_prof_name";
            this.pan_prof_name.Size = new System.Drawing.Size(215, 25);
            this.pan_prof_name.TabIndex = 81;
            // 
            // pan_proc
            // 
            this.pan_proc.Controls.Add(this.cb_proc);
            this.pan_proc.Controls.Add(this.lb_proc);
            this.pan_proc.Location = new System.Drawing.Point(125, 238);
            this.pan_proc.Name = "pan_proc";
            this.pan_proc.Size = new System.Drawing.Size(202, 24);
            this.pan_proc.TabIndex = 82;
            this.pan_proc.Visible = false;
            // 
            // pan_prog
            // 
            this.pan_prog.Controls.Add(this.cb_prog);
            this.pan_prog.Controls.Add(this.lb_area);
            this.pan_prog.Location = new System.Drawing.Point(125, 238);
            this.pan_prog.Name = "pan_prog";
            this.pan_prog.Size = new System.Drawing.Size(202, 24);
            this.pan_prog.TabIndex = 83;
            this.pan_prog.Visible = false;
            // 
            // lb_debug
            // 
            this.lb_debug.AutoSize = true;
            this.lb_debug.Location = new System.Drawing.Point(110, 299);
            this.lb_debug.Name = "lb_debug";
            this.lb_debug.Size = new System.Drawing.Size(37, 13);
            this.lb_debug.TabIndex = 84;
            this.lb_debug.Text = "debug";
            this.lb_debug.Visible = false;
            // 
            // lb_num
            // 
            this.lb_num.BackColor = System.Drawing.Color.Transparent;
            this.lb_num.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_num.Location = new System.Drawing.Point(343, 196);
            this.lb_num.Name = "lb_num";
            this.lb_num.Size = new System.Drawing.Size(35, 15);
            this.lb_num.TabIndex = 91;
            this.lb_num.Text = "Num";
            this.lb_num.Click += new System.EventHandler(this.lb_locks_Click);
            // 
            // lb_caps
            // 
            this.lb_caps.BackColor = System.Drawing.Color.Transparent;
            this.lb_caps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_caps.Location = new System.Drawing.Point(377, 196);
            this.lb_caps.Name = "lb_caps";
            this.lb_caps.Size = new System.Drawing.Size(35, 15);
            this.lb_caps.TabIndex = 92;
            this.lb_caps.Text = "Caps";
            this.lb_caps.Click += new System.EventHandler(this.lb_locks_Click);
            // 
            // lb_scroll
            // 
            this.lb_scroll.BackColor = System.Drawing.Color.Transparent;
            this.lb_scroll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_scroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_scroll.Location = new System.Drawing.Point(411, 196);
            this.lb_scroll.Name = "lb_scroll";
            this.lb_scroll.Size = new System.Drawing.Size(35, 15);
            this.lb_scroll.TabIndex = 93;
            this.lb_scroll.Text = "Scroll";
            this.lb_scroll.Click += new System.EventHandler(this.lb_locks_Click);
            // 
            // mouseKeyEventProvider2
            // 
            this.mouseKeyEventProvider2.Enabled = true;
            this.mouseKeyEventProvider2.HookType = MouseKeyboardActivityMonitor.Controls.HookType.Global;
            this.mouseKeyEventProvider2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mouseKeyEventProvider2_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(229, 294);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 94;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(155, 294);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 95;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lb_help
            // 
            this.lb_help.AutoSize = true;
            this.lb_help.BackColor = System.Drawing.Color.Transparent;
            this.lb_help.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_help.Location = new System.Drawing.Point(296, 299);
            this.lb_help.Name = "lb_help";
            this.lb_help.Size = new System.Drawing.Size(50, 13);
            this.lb_help.TabIndex = 96;
            this.lb_help.Text = "Помощь";
            this.lb_help.Click += new System.EventHandler(this.lb_help_Click);
            this.lb_help.MouseLeave += new System.EventHandler(this.lb_auth_MouseLeave);
            this.lb_help.MouseHover += new System.EventHandler(this.lb_auth_MouseHover);
            // 
            // gb_set
            // 
            this.gb_set.BackColor = System.Drawing.Color.Transparent;
            this.gb_set.Controls.Add(this.b_trig_ok);
            this.gb_set.Controls.Add(this.lb_trig_delay_ms);
            this.gb_set.Controls.Add(this.lb_trig_time_ms);
            this.gb_set.Controls.Add(this.lb_trig_delay);
            this.gb_set.Controls.Add(this.nud_trig_delay);
            this.gb_set.Controls.Add(this.cb_trig_once);
            this.gb_set.Controls.Add(this.lb_trig_enable);
            this.gb_set.Controls.Add(this.cb_trig_enable);
            this.gb_set.Controls.Add(this.lb_trig_time);
            this.gb_set.Controls.Add(this.nud_trig_time);
            this.gb_set.Location = new System.Drawing.Point(2, 2);
            this.gb_set.Name = "gb_set";
            this.gb_set.Size = new System.Drawing.Size(443, 116);
            this.gb_set.TabIndex = 97;
            this.gb_set.TabStop = false;
            this.gb_set.Text = "Настройка работы триггера";
            // 
            // b_trig_ok
            // 
            this.b_trig_ok.Location = new System.Drawing.Point(186, 84);
            this.b_trig_ok.Name = "b_trig_ok";
            this.b_trig_ok.Size = new System.Drawing.Size(58, 28);
            this.b_trig_ok.TabIndex = 31;
            this.b_trig_ok.Text = "OK";
            this.b_trig_ok.UseVisualStyleBackColor = true;
            this.b_trig_ok.Click += new System.EventHandler(this.b_trig_ok_Click);
            // 
            // lb_trig_delay_ms
            // 
            this.lb_trig_delay_ms.AutoSize = true;
            this.lb_trig_delay_ms.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_trig_delay_ms.Location = new System.Drawing.Point(354, 74);
            this.lb_trig_delay_ms.Name = "lb_trig_delay_ms";
            this.lb_trig_delay_ms.Size = new System.Drawing.Size(58, 13);
            this.lb_trig_delay_ms.TabIndex = 30;
            this.lb_trig_delay_ms.Text = "Пауза..мс";
            this.lb_trig_delay_ms.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_trig_time_ms
            // 
            this.lb_trig_time_ms.AutoSize = true;
            this.lb_trig_time_ms.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_trig_time_ms.Location = new System.Drawing.Point(112, 73);
            this.lb_trig_time_ms.Name = "lb_trig_time_ms";
            this.lb_trig_time_ms.Size = new System.Drawing.Size(58, 13);
            this.lb_trig_time_ms.TabIndex = 29;
            this.lb_trig_time_ms.Text = "Пауза..мс";
            this.lb_trig_time_ms.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_trig_delay
            // 
            this.lb_trig_delay.AutoSize = true;
            this.lb_trig_delay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_trig_delay.Location = new System.Drawing.Point(202, 53);
            this.lb_trig_delay.Name = "lb_trig_delay";
            this.lb_trig_delay.Size = new System.Drawing.Size(139, 13);
            this.lb_trig_delay.TabIndex = 28;
            this.lb_trig_delay.Text = "Задержка включения (мс)";
            // 
            // nud_trig_delay
            // 
            this.nud_trig_delay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nud_trig_delay.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_trig_delay.Location = new System.Drawing.Point(357, 51);
            this.nud_trig_delay.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_trig_delay.Name = "nud_trig_delay";
            this.nud_trig_delay.Size = new System.Drawing.Size(79, 20);
            this.nud_trig_delay.TabIndex = 27;
            this.nud_trig_delay.ValueChanged += new System.EventHandler(this.nud_Leave);
            this.nud_trig_delay.Leave += new System.EventHandler(this.nud_Leave);
            // 
            // cb_trig_once
            // 
            this.cb_trig_once.AutoSize = true;
            this.cb_trig_once.Location = new System.Drawing.Point(9, 22);
            this.cb_trig_once.Name = "cb_trig_once";
            this.cb_trig_once.Size = new System.Drawing.Size(139, 17);
            this.cb_trig_once.TabIndex = 26;
            this.cb_trig_once.Text = "Однократное нажатие";
            this.cb_trig_once.UseVisualStyleBackColor = true;
            this.cb_trig_once.CheckedChanged += new System.EventHandler(this.cb_trig_once_CheckedChanged);
            // 
            // lb_trig_enable
            // 
            this.lb_trig_enable.AutoSize = true;
            this.lb_trig_enable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_trig_enable.Location = new System.Drawing.Point(202, 23);
            this.lb_trig_enable.Name = "lb_trig_enable";
            this.lb_trig_enable.Size = new System.Drawing.Size(144, 13);
            this.lb_trig_enable.TabIndex = 25;
            this.lb_trig_enable.Text = "Включение после триггера";
            this.lb_trig_enable.Visible = false;
            // 
            // cb_trig_enable
            // 
            this.cb_trig_enable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_trig_enable.FormattingEnabled = true;
            this.cb_trig_enable.Items.AddRange(new object[] {
            "",
            "Триггер 1",
            "Триггер 2",
            "Триггер 3",
            "Триггер 4",
            "Триггер 5",
            "Триггер 6"});
            this.cb_trig_enable.Location = new System.Drawing.Point(357, 20);
            this.cb_trig_enable.Name = "cb_trig_enable";
            this.cb_trig_enable.Size = new System.Drawing.Size(79, 21);
            this.cb_trig_enable.TabIndex = 24;
            this.cb_trig_enable.Visible = false;
            // 
            // lb_trig_time
            // 
            this.lb_trig_time.AutoSize = true;
            this.lb_trig_time.Enabled = false;
            this.lb_trig_time.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_trig_time.Location = new System.Drawing.Point(6, 52);
            this.lb_trig_time.Name = "lb_trig_time";
            this.lb_trig_time.Size = new System.Drawing.Size(103, 13);
            this.lb_trig_time.TabIndex = 23;
            this.lb_trig_time.Text = "Время работы (мс)";
            // 
            // nud_trig_time
            // 
            this.nud_trig_time.Enabled = false;
            this.nud_trig_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nud_trig_time.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_trig_time.Location = new System.Drawing.Point(115, 50);
            this.nud_trig_time.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_trig_time.Name = "nud_trig_time";
            this.nud_trig_time.Size = new System.Drawing.Size(79, 20);
            this.nud_trig_time.TabIndex = 22;
            this.nud_trig_time.ValueChanged += new System.EventHandler(this.nud_Leave);
            this.nud_trig_time.Leave += new System.EventHandler(this.nud_Leave);
            // 
            // pan_press_type
            // 
            this.pan_press_type.Controls.Add(this.cb_press_type);
            this.pan_press_type.Controls.Add(this.lb_press_type);
            this.pan_press_type.Location = new System.Drawing.Point(125, 269);
            this.pan_press_type.Name = "pan_press_type";
            this.pan_press_type.Size = new System.Drawing.Size(202, 24);
            this.pan_press_type.TabIndex = 83;
            this.pan_press_type.Visible = false;
            // 
            // cb_press_type
            // 
            this.cb_press_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_press_type.FormattingEnabled = true;
            this.cb_press_type.Items.AddRange(new object[] {
            "Default",
            "PostMessage",
            "Keybd_event",
            "Input Simulator"});
            this.cb_press_type.Location = new System.Drawing.Point(100, 3);
            this.cb_press_type.Name = "cb_press_type";
            this.cb_press_type.Size = new System.Drawing.Size(100, 21);
            this.cb_press_type.TabIndex = 70;
            this.cb_press_type.SelectionChangeCommitted += new System.EventHandler(this.cb_press_type_SelectionChangeCommitted);
            // 
            // lb_press_type
            // 
            this.lb_press_type.AutoSize = true;
            this.lb_press_type.BackColor = System.Drawing.Color.Transparent;
            this.lb_press_type.Location = new System.Drawing.Point(-3, 6);
            this.lb_press_type.Name = "lb_press_type";
            this.lb_press_type.Size = new System.Drawing.Size(78, 13);
            this.lb_press_type.TabIndex = 71;
            this.lb_press_type.Text = "Тип прожатия";
            // 
            // pan_set
            // 
            this.pan_set.Controls.Add(this.gb_set);
            this.pan_set.Location = new System.Drawing.Point(4, 50);
            this.pan_set.Name = "pan_set";
            this.pan_set.Size = new System.Drawing.Size(450, 122);
            this.pan_set.TabIndex = 98;
            this.pan_set.Visible = false;
            // 
            // cb_tmr3
            // 
            this.cb_tmr3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cb_tmr3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_tmr3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_tmr3.ItemHeight = 14;
            this.cb_tmr3.Items.AddRange(new object[] {
            " ",
            " ",
            " ",
            " "});
            this.cb_tmr3.Location = new System.Drawing.Point(160, 111);
            this.cb_tmr3.Name = "cb_tmr3";
            this.cb_tmr3.Size = new System.Drawing.Size(68, 20);
            this.cb_tmr3.TabIndex = 71;
            this.cb_tmr3.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cb_tmr_DrawItem);
            this.cb_tmr3.SelectedIndexChanged += new System.EventHandler(this.cb_tmr_SelectedIndexChanged);
            this.cb_tmr3.SelectionChangeCommitted += new System.EventHandler(this.cb_tmr_SelectionChangeCommitted);
            // 
            // cb_tmr4
            // 
            this.cb_tmr4.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cb_tmr4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_tmr4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_tmr4.ItemHeight = 14;
            this.cb_tmr4.Items.AddRange(new object[] {
            "",
            "",
            " ",
            " "});
            this.cb_tmr4.Location = new System.Drawing.Point(234, 111);
            this.cb_tmr4.Name = "cb_tmr4";
            this.cb_tmr4.Size = new System.Drawing.Size(68, 20);
            this.cb_tmr4.TabIndex = 72;
            this.cb_tmr4.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cb_tmr_DrawItem);
            this.cb_tmr4.SelectedIndexChanged += new System.EventHandler(this.cb_tmr_SelectedIndexChanged);
            this.cb_tmr4.SelectionChangeCommitted += new System.EventHandler(this.cb_tmr_SelectionChangeCommitted);
            // 
            // cb_tmr5
            // 
            this.cb_tmr5.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cb_tmr5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_tmr5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_tmr5.ItemHeight = 14;
            this.cb_tmr5.Items.AddRange(new object[] {
            "",
            " ",
            " ",
            " "});
            this.cb_tmr5.Location = new System.Drawing.Point(309, 111);
            this.cb_tmr5.Name = "cb_tmr5";
            this.cb_tmr5.Size = new System.Drawing.Size(68, 20);
            this.cb_tmr5.TabIndex = 73;
            this.cb_tmr5.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cb_tmr_DrawItem);
            this.cb_tmr5.SelectedIndexChanged += new System.EventHandler(this.cb_tmr_SelectedIndexChanged);
            this.cb_tmr5.SelectionChangeCommitted += new System.EventHandler(this.cb_tmr_SelectionChangeCommitted);
            // 
            // cb_tmr6
            // 
            this.cb_tmr6.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cb_tmr6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_tmr6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_tmr6.ItemHeight = 14;
            this.cb_tmr6.Items.AddRange(new object[] {
            "",
            "",
            "",
            " "});
            this.cb_tmr6.Location = new System.Drawing.Point(384, 111);
            this.cb_tmr6.Name = "cb_tmr6";
            this.cb_tmr6.Size = new System.Drawing.Size(68, 20);
            this.cb_tmr6.TabIndex = 74;
            this.cb_tmr6.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cb_tmr_DrawItem);
            this.cb_tmr6.SelectedIndexChanged += new System.EventHandler(this.cb_tmr_SelectedIndexChanged);
            this.cb_tmr6.SelectionChangeCommitted += new System.EventHandler(this.cb_tmr_SelectionChangeCommitted);
            // 
            // cb_tmr2
            // 
            this.cb_tmr2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cb_tmr2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_tmr2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_tmr2.ItemHeight = 14;
            this.cb_tmr2.Items.AddRange(new object[] {
            "",
            "",
            "",
            " "});
            this.cb_tmr2.Location = new System.Drawing.Point(84, 111);
            this.cb_tmr2.Name = "cb_tmr2";
            this.cb_tmr2.Size = new System.Drawing.Size(68, 20);
            this.cb_tmr2.TabIndex = 70;
            this.cb_tmr2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cb_tmr_DrawItem);
            this.cb_tmr2.SelectedIndexChanged += new System.EventHandler(this.cb_tmr_SelectedIndexChanged);
            this.cb_tmr2.SelectionChangeCommitted += new System.EventHandler(this.cb_tmr_SelectionChangeCommitted);
            // 
            // cb_tmr1
            // 
            this.cb_tmr1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_tmr1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_tmr1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_tmr1.ItemHeight = 14;
            this.cb_tmr1.Items.AddRange(new object[] {
            "",
            "",
            "",
            " "});
            this.cb_tmr1.Location = new System.Drawing.Point(9, 111);
            this.cb_tmr1.Name = "cb_tmr1";
            this.cb_tmr1.Size = new System.Drawing.Size(68, 20);
            this.cb_tmr1.TabIndex = 69;
            this.cb_tmr1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cb_tmr_DrawItem);
            this.cb_tmr1.SelectedIndexChanged += new System.EventHandler(this.cb_tmr_SelectedIndexChanged);
            this.cb_tmr1.SelectionChangeCommitted += new System.EventHandler(this.cb_tmr_SelectionChangeCommitted);
            // 
            // cb_key3
            // 
            this.cb_key3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_key3.FormattingEnabled = true;
            this.cb_key3.Items.AddRange(new object[] {
            "",
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
            "V",
            "Space",
            "LMouse",
            "RMouse",
            "Shift+LM",
            "Shift+RM"});
            this.cb_key3.Location = new System.Drawing.Point(159, 56);
            this.cb_key3.Name = "cb_key3";
            this.cb_key3.Size = new System.Drawing.Size(58, 21);
            this.cb_key3.TabIndex = 6;
            this.cb_key3.SelectionChangeCommitted += new System.EventHandler(this.key_choose_SelectionChangeCommitted);
            this.cb_key3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            // 
            // cb_key4
            // 
            this.cb_key4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_key4.FormattingEnabled = true;
            this.cb_key4.Items.AddRange(new object[] {
            "",
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
            "V",
            "Space",
            "LMouse",
            "RMouse",
            "Shift+LM",
            "Shift+RM"});
            this.cb_key4.Location = new System.Drawing.Point(234, 56);
            this.cb_key4.Name = "cb_key4";
            this.cb_key4.Size = new System.Drawing.Size(58, 21);
            this.cb_key4.TabIndex = 7;
            this.cb_key4.SelectionChangeCommitted += new System.EventHandler(this.key_choose_SelectionChangeCommitted);
            this.cb_key4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            // 
            // cb_key2
            // 
            this.cb_key2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_key2.FormattingEnabled = true;
            this.cb_key2.Items.AddRange(new object[] {
            "",
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
            "V",
            "Space",
            "LMouse",
            "RMouse",
            "Shift+LM",
            "Shift+RM"});
            this.cb_key2.Location = new System.Drawing.Point(84, 56);
            this.cb_key2.Name = "cb_key2";
            this.cb_key2.Size = new System.Drawing.Size(58, 21);
            this.cb_key2.TabIndex = 5;
            this.cb_key2.SelectionChangeCommitted += new System.EventHandler(this.key_choose_SelectionChangeCommitted);
            this.cb_key2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            // 
            // cb_key1
            // 
            this.cb_key1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_key1.FormattingEnabled = true;
            this.cb_key1.Items.AddRange(new object[] {
            "",
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
            "V",
            "Space",
            "LMouse",
            "RMouse",
            "Shift+LM",
            "Shift+RM"});
            this.cb_key1.Location = new System.Drawing.Point(9, 56);
            this.cb_key1.Name = "cb_key1";
            this.cb_key1.Size = new System.Drawing.Size(58, 21);
            this.cb_key1.TabIndex = 4;
            this.cb_key1.SelectionChangeCommitted += new System.EventHandler(this.key_choose_SelectionChangeCommitted);
            this.cb_key1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            // 
            // lb_key4
            // 
            this.lb_key4.AutoSize = true;
            this.lb_key4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_key4.Location = new System.Drawing.Point(231, 80);
            this.lb_key4.Name = "lb_key4";
            this.lb_key4.Size = new System.Drawing.Size(61, 13);
            this.lb_key4.TabIndex = 39;
            this.lb_key4.Text = "Клавиша 4";
            // 
            // lb_key3
            // 
            this.lb_key3.AutoSize = true;
            this.lb_key3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_key3.Location = new System.Drawing.Point(156, 80);
            this.lb_key3.Name = "lb_key3";
            this.lb_key3.Size = new System.Drawing.Size(61, 13);
            this.lb_key3.TabIndex = 38;
            this.lb_key3.Text = "Клавиша 3";
            // 
            // nud_tmr5
            // 
            this.nud_tmr5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nud_tmr5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.nud_tmr5.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_tmr5.Location = new System.Drawing.Point(309, 111);
            this.nud_tmr5.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_tmr5.Name = "nud_tmr5";
            this.nud_tmr5.Size = new System.Drawing.Size(56, 20);
            this.nud_tmr5.TabIndex = 53;
            this.nud_tmr5.ValueChanged += new System.EventHandler(this.nud_Leave);
            this.nud_tmr5.Leave += new System.EventHandler(this.nud_Leave);
            // 
            // lb_key2
            // 
            this.lb_key2.AutoSize = true;
            this.lb_key2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_key2.Location = new System.Drawing.Point(81, 80);
            this.lb_key2.Name = "lb_key2";
            this.lb_key2.Size = new System.Drawing.Size(61, 13);
            this.lb_key2.TabIndex = 37;
            this.lb_key2.Text = "Клавиша 2";
            // 
            // lb_trig5
            // 
            this.lb_trig5.AutoSize = true;
            this.lb_trig5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_trig5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_trig5.Location = new System.Drawing.Point(306, 27);
            this.lb_trig5.Name = "lb_trig5";
            this.lb_trig5.Size = new System.Drawing.Size(57, 13);
            this.lb_trig5.TabIndex = 54;
            this.lb_trig5.Text = "Триггер 5";
            this.lb_trig5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lb_trig_MouseClick);
            this.lb_trig5.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // lb_key1
            // 
            this.lb_key1.AutoSize = true;
            this.lb_key1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_key1.Location = new System.Drawing.Point(6, 80);
            this.lb_key1.Name = "lb_key1";
            this.lb_key1.Size = new System.Drawing.Size(61, 13);
            this.lb_key1.TabIndex = 36;
            this.lb_key1.Text = "Клавиша 1";
            // 
            // lb_tmr5_sec
            // 
            this.lb_tmr5_sec.AutoSize = true;
            this.lb_tmr5_sec.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_tmr5_sec.Location = new System.Drawing.Point(306, 134);
            this.lb_tmr5_sec.Name = "lb_tmr5_sec";
            this.lb_tmr5_sec.Size = new System.Drawing.Size(58, 13);
            this.lb_tmr5_sec.TabIndex = 55;
            this.lb_tmr5_sec.Text = "Пауза..мс";
            this.lb_tmr5_sec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.cb_trig_tmr4.Location = new System.Drawing.Point(234, 3);
            this.cb_trig_tmr4.Name = "cb_trig_tmr4";
            this.cb_trig_tmr4.Size = new System.Drawing.Size(58, 21);
            this.cb_trig_tmr4.TabIndex = 3;
            this.cb_trig_tmr4.SelectionChangeCommitted += new System.EventHandler(this.cb_trig_SelectionChangeCommitted);
            this.cb_trig_tmr4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            this.cb_trig_tmr4.MouseLeave += new System.EventHandler(this.cb_trig_tmr_MouseLeave);
            this.cb_trig_tmr4.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // cb_trig_tmr5
            // 
            this.cb_trig_tmr5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_trig_tmr5.FormattingEnabled = true;
            this.cb_trig_tmr5.Items.AddRange(new object[] {
            "",
            "Shift",
            "Scroll L",
            "Caps L",
            "Num L"});
            this.cb_trig_tmr5.Location = new System.Drawing.Point(309, 3);
            this.cb_trig_tmr5.Name = "cb_trig_tmr5";
            this.cb_trig_tmr5.Size = new System.Drawing.Size(58, 21);
            this.cb_trig_tmr5.TabIndex = 51;
            this.cb_trig_tmr5.SelectionChangeCommitted += new System.EventHandler(this.cb_trig_SelectionChangeCommitted);
            this.cb_trig_tmr5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            this.cb_trig_tmr5.MouseLeave += new System.EventHandler(this.cb_trig_tmr_MouseLeave);
            this.cb_trig_tmr5.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
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
            this.cb_trig_tmr3.Location = new System.Drawing.Point(159, 3);
            this.cb_trig_tmr3.Name = "cb_trig_tmr3";
            this.cb_trig_tmr3.Size = new System.Drawing.Size(58, 21);
            this.cb_trig_tmr3.TabIndex = 2;
            this.cb_trig_tmr3.SelectedIndexChanged += new System.EventHandler(this.cb_trig_tmr3_SelectedIndexChanged);
            this.cb_trig_tmr3.SelectionChangeCommitted += new System.EventHandler(this.cb_trig_SelectionChangeCommitted);
            this.cb_trig_tmr3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            this.cb_trig_tmr3.MouseLeave += new System.EventHandler(this.cb_trig_tmr_MouseLeave);
            this.cb_trig_tmr3.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // lb_key5
            // 
            this.lb_key5.AutoSize = true;
            this.lb_key5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_key5.Location = new System.Drawing.Point(306, 80);
            this.lb_key5.Name = "lb_key5";
            this.lb_key5.Size = new System.Drawing.Size(61, 13);
            this.lb_key5.TabIndex = 56;
            this.lb_key5.Text = "Клавиша 5";
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
            this.cb_trig_tmr2.Location = new System.Drawing.Point(84, 3);
            this.cb_trig_tmr2.Name = "cb_trig_tmr2";
            this.cb_trig_tmr2.Size = new System.Drawing.Size(58, 21);
            this.cb_trig_tmr2.TabIndex = 1;
            this.cb_trig_tmr2.SelectionChangeCommitted += new System.EventHandler(this.cb_trig_SelectionChangeCommitted);
            this.cb_trig_tmr2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            this.cb_trig_tmr2.MouseLeave += new System.EventHandler(this.cb_trig_tmr_MouseLeave);
            this.cb_trig_tmr2.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // cb_key5
            // 
            this.cb_key5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_key5.FormattingEnabled = true;
            this.cb_key5.Items.AddRange(new object[] {
            "",
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
            "V",
            "Space",
            "LMouse",
            "RMouse",
            "Shift+LM",
            "Shift+RM"});
            this.cb_key5.Location = new System.Drawing.Point(309, 56);
            this.cb_key5.Name = "cb_key5";
            this.cb_key5.Size = new System.Drawing.Size(58, 21);
            this.cb_key5.TabIndex = 52;
            this.cb_key5.SelectionChangeCommitted += new System.EventHandler(this.key_choose_SelectionChangeCommitted);
            this.cb_key5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            // 
            // lb_tmr4_sec
            // 
            this.lb_tmr4_sec.AutoSize = true;
            this.lb_tmr4_sec.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_tmr4_sec.Location = new System.Drawing.Point(231, 134);
            this.lb_tmr4_sec.Name = "lb_tmr4_sec";
            this.lb_tmr4_sec.Size = new System.Drawing.Size(58, 13);
            this.lb_tmr4_sec.TabIndex = 29;
            this.lb_tmr4_sec.Text = "Пауза..мс";
            this.lb_tmr4_sec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nud_tmr6
            // 
            this.nud_tmr6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nud_tmr6.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_tmr6.Location = new System.Drawing.Point(384, 111);
            this.nud_tmr6.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_tmr6.Name = "nud_tmr6";
            this.nud_tmr6.Size = new System.Drawing.Size(56, 20);
            this.nud_tmr6.TabIndex = 59;
            this.nud_tmr6.ValueChanged += new System.EventHandler(this.nud_Leave);
            this.nud_tmr6.Leave += new System.EventHandler(this.nud_Leave);
            // 
            // lb_tmr3_sec
            // 
            this.lb_tmr3_sec.AutoSize = true;
            this.lb_tmr3_sec.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_tmr3_sec.Location = new System.Drawing.Point(157, 134);
            this.lb_tmr3_sec.Name = "lb_tmr3_sec";
            this.lb_tmr3_sec.Size = new System.Drawing.Size(58, 13);
            this.lb_tmr3_sec.TabIndex = 28;
            this.lb_tmr3_sec.Text = "Пауза..мс";
            this.lb_tmr3_sec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_trig6
            // 
            this.lb_trig6.AutoSize = true;
            this.lb_trig6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_trig6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_trig6.Location = new System.Drawing.Point(381, 27);
            this.lb_trig6.Name = "lb_trig6";
            this.lb_trig6.Size = new System.Drawing.Size(57, 13);
            this.lb_trig6.TabIndex = 60;
            this.lb_trig6.Text = "Триггер 6";
            this.lb_trig6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lb_trig_MouseClick);
            this.lb_trig6.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // lb_tmr2_sec
            // 
            this.lb_tmr2_sec.AutoSize = true;
            this.lb_tmr2_sec.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_tmr2_sec.Location = new System.Drawing.Point(81, 134);
            this.lb_tmr2_sec.Name = "lb_tmr2_sec";
            this.lb_tmr2_sec.Size = new System.Drawing.Size(58, 13);
            this.lb_tmr2_sec.TabIndex = 27;
            this.lb_tmr2_sec.Text = "Пауза..мс";
            this.lb_tmr2_sec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_tmr6_sec
            // 
            this.lb_tmr6_sec.AutoSize = true;
            this.lb_tmr6_sec.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_tmr6_sec.Location = new System.Drawing.Point(381, 134);
            this.lb_tmr6_sec.Name = "lb_tmr6_sec";
            this.lb_tmr6_sec.Size = new System.Drawing.Size(58, 13);
            this.lb_tmr6_sec.TabIndex = 61;
            this.lb_tmr6_sec.Text = "Пауза..мс";
            this.lb_tmr6_sec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_tmr1_sec
            // 
            this.lb_tmr1_sec.AutoSize = true;
            this.lb_tmr1_sec.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_tmr1_sec.Location = new System.Drawing.Point(6, 134);
            this.lb_tmr1_sec.Name = "lb_tmr1_sec";
            this.lb_tmr1_sec.Size = new System.Drawing.Size(58, 13);
            this.lb_tmr1_sec.TabIndex = 26;
            this.lb_tmr1_sec.Text = "Пауза..мс";
            this.lb_tmr1_sec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_trig_tmr6
            // 
            this.cb_trig_tmr6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_trig_tmr6.FormattingEnabled = true;
            this.cb_trig_tmr6.Items.AddRange(new object[] {
            "",
            "Shift",
            "Scroll L",
            "Caps L",
            "Num L"});
            this.cb_trig_tmr6.Location = new System.Drawing.Point(384, 3);
            this.cb_trig_tmr6.Name = "cb_trig_tmr6";
            this.cb_trig_tmr6.Size = new System.Drawing.Size(58, 21);
            this.cb_trig_tmr6.TabIndex = 57;
            this.cb_trig_tmr6.SelectionChangeCommitted += new System.EventHandler(this.cb_trig_SelectionChangeCommitted);
            this.cb_trig_tmr6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            this.cb_trig_tmr6.MouseLeave += new System.EventHandler(this.cb_trig_tmr_MouseLeave);
            this.cb_trig_tmr6.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // lb_trig4
            // 
            this.lb_trig4.AutoSize = true;
            this.lb_trig4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_trig4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_trig4.Location = new System.Drawing.Point(231, 27);
            this.lb_trig4.Name = "lb_trig4";
            this.lb_trig4.Size = new System.Drawing.Size(57, 13);
            this.lb_trig4.TabIndex = 23;
            this.lb_trig4.Text = "Триггер 4";
            this.lb_trig4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lb_trig_MouseClick);
            this.lb_trig4.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // lb_trig3
            // 
            this.lb_trig3.AutoSize = true;
            this.lb_trig3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_trig3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_trig3.Location = new System.Drawing.Point(156, 27);
            this.lb_trig3.Name = "lb_trig3";
            this.lb_trig3.Size = new System.Drawing.Size(57, 13);
            this.lb_trig3.TabIndex = 22;
            this.lb_trig3.Text = "Триггер 3";
            this.lb_trig3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lb_trig_MouseClick);
            this.lb_trig3.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // lb_key6
            // 
            this.lb_key6.AutoSize = true;
            this.lb_key6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_key6.Location = new System.Drawing.Point(381, 80);
            this.lb_key6.Name = "lb_key6";
            this.lb_key6.Size = new System.Drawing.Size(61, 13);
            this.lb_key6.TabIndex = 62;
            this.lb_key6.Text = "Клавиша 6";
            // 
            // lb_trig2
            // 
            this.lb_trig2.AutoSize = true;
            this.lb_trig2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_trig2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_trig2.Location = new System.Drawing.Point(81, 27);
            this.lb_trig2.Name = "lb_trig2";
            this.lb_trig2.Size = new System.Drawing.Size(57, 13);
            this.lb_trig2.TabIndex = 21;
            this.lb_trig2.Text = "Триггер 2";
            this.lb_trig2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lb_trig_MouseClick);
            this.lb_trig2.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // cb_key6
            // 
            this.cb_key6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_key6.FormattingEnabled = true;
            this.cb_key6.Items.AddRange(new object[] {
            "",
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
            "V",
            "Space",
            "LMouse",
            "RMouse",
            "Shift+LM",
            "Shift+RM"});
            this.cb_key6.Location = new System.Drawing.Point(384, 56);
            this.cb_key6.Name = "cb_key6";
            this.cb_key6.Size = new System.Drawing.Size(58, 21);
            this.cb_key6.TabIndex = 58;
            this.cb_key6.SelectionChangeCommitted += new System.EventHandler(this.key_choose_SelectionChangeCommitted);
            this.cb_key6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            // 
            // lb_trig1
            // 
            this.lb_trig1.AutoSize = true;
            this.lb_trig1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_trig1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_trig1.Location = new System.Drawing.Point(6, 27);
            this.lb_trig1.Name = "lb_trig1";
            this.lb_trig1.Size = new System.Drawing.Size(57, 13);
            this.lb_trig1.TabIndex = 20;
            this.lb_trig1.Text = "Триггер 1";
            this.lb_trig1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lb_trig_MouseClick);
            this.lb_trig1.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // nud_tmr4
            // 
            this.nud_tmr4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nud_tmr4.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_tmr4.Location = new System.Drawing.Point(234, 111);
            this.nud_tmr4.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_tmr4.Name = "nud_tmr4";
            this.nud_tmr4.Size = new System.Drawing.Size(56, 20);
            this.nud_tmr4.TabIndex = 11;
            this.nud_tmr4.ValueChanged += new System.EventHandler(this.nud_Leave);
            this.nud_tmr4.Leave += new System.EventHandler(this.nud_Leave);
            // 
            // nud_tmr3
            // 
            this.nud_tmr3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nud_tmr3.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_tmr3.Location = new System.Drawing.Point(160, 111);
            this.nud_tmr3.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_tmr3.Name = "nud_tmr3";
            this.nud_tmr3.Size = new System.Drawing.Size(56, 20);
            this.nud_tmr3.TabIndex = 10;
            this.nud_tmr3.ValueChanged += new System.EventHandler(this.nud_Leave);
            this.nud_tmr3.Leave += new System.EventHandler(this.nud_Leave);
            // 
            // nud_tmr2
            // 
            this.nud_tmr2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nud_tmr2.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_tmr2.Location = new System.Drawing.Point(84, 111);
            this.nud_tmr2.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_tmr2.Name = "nud_tmr2";
            this.nud_tmr2.Size = new System.Drawing.Size(56, 20);
            this.nud_tmr2.TabIndex = 9;
            this.nud_tmr2.ValueChanged += new System.EventHandler(this.nud_Leave);
            this.nud_tmr2.Leave += new System.EventHandler(this.nud_Leave);
            // 
            // nud_tmr1
            // 
            this.nud_tmr1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nud_tmr1.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_tmr1.Location = new System.Drawing.Point(9, 111);
            this.nud_tmr1.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_tmr1.Name = "nud_tmr1";
            this.nud_tmr1.Size = new System.Drawing.Size(56, 20);
            this.nud_tmr1.TabIndex = 8;
            this.nud_tmr1.ValueChanged += new System.EventHandler(this.nud_Leave);
            this.nud_tmr1.Leave += new System.EventHandler(this.nud_Leave);
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
            this.cb_trig_tmr1.Location = new System.Drawing.Point(9, 3);
            this.cb_trig_tmr1.Name = "cb_trig_tmr1";
            this.cb_trig_tmr1.Size = new System.Drawing.Size(58, 21);
            this.cb_trig_tmr1.TabIndex = 0;
            this.cb_trig_tmr1.SelectionChangeCommitted += new System.EventHandler(this.cb_trig_SelectionChangeCommitted);
            this.cb_trig_tmr1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_choose_MouseClick);
            this.cb_trig_tmr1.MouseLeave += new System.EventHandler(this.cb_trig_tmr_MouseLeave);
            this.cb_trig_tmr1.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // chb_trig1
            // 
            this.chb_trig1.AutoSize = true;
            this.chb_trig1.Location = new System.Drawing.Point(51, 7);
            this.chb_trig1.Name = "chb_trig1";
            this.chb_trig1.Size = new System.Drawing.Size(15, 14);
            this.chb_trig1.TabIndex = 63;
            this.chb_trig1.UseVisualStyleBackColor = true;
            this.chb_trig1.CheckedChanged += new System.EventHandler(this.chb_trig_CheckedChanged);
            this.chb_trig1.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // chb_trig2
            // 
            this.chb_trig2.AutoSize = true;
            this.chb_trig2.Location = new System.Drawing.Point(126, 7);
            this.chb_trig2.Name = "chb_trig2";
            this.chb_trig2.Size = new System.Drawing.Size(15, 14);
            this.chb_trig2.TabIndex = 64;
            this.chb_trig2.UseVisualStyleBackColor = true;
            this.chb_trig2.CheckedChanged += new System.EventHandler(this.chb_trig_CheckedChanged);
            this.chb_trig2.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // chb_trig3
            // 
            this.chb_trig3.AutoSize = true;
            this.chb_trig3.Location = new System.Drawing.Point(201, 7);
            this.chb_trig3.Name = "chb_trig3";
            this.chb_trig3.Size = new System.Drawing.Size(15, 14);
            this.chb_trig3.TabIndex = 65;
            this.chb_trig3.UseVisualStyleBackColor = true;
            this.chb_trig3.CheckedChanged += new System.EventHandler(this.chb_trig_CheckedChanged);
            this.chb_trig3.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // chb_trig4
            // 
            this.chb_trig4.AutoSize = true;
            this.chb_trig4.Location = new System.Drawing.Point(276, 7);
            this.chb_trig4.Name = "chb_trig4";
            this.chb_trig4.Size = new System.Drawing.Size(15, 14);
            this.chb_trig4.TabIndex = 66;
            this.chb_trig4.UseVisualStyleBackColor = true;
            this.chb_trig4.CheckedChanged += new System.EventHandler(this.chb_trig_CheckedChanged);
            this.chb_trig4.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // chb_trig5
            // 
            this.chb_trig5.AutoSize = true;
            this.chb_trig5.Location = new System.Drawing.Point(351, 7);
            this.chb_trig5.Name = "chb_trig5";
            this.chb_trig5.Size = new System.Drawing.Size(15, 14);
            this.chb_trig5.TabIndex = 67;
            this.chb_trig5.UseVisualStyleBackColor = true;
            this.chb_trig5.CheckedChanged += new System.EventHandler(this.chb_trig_CheckedChanged);
            this.chb_trig5.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // chb_trig6
            // 
            this.chb_trig6.AutoSize = true;
            this.chb_trig6.Location = new System.Drawing.Point(426, 7);
            this.chb_trig6.Name = "chb_trig6";
            this.chb_trig6.Size = new System.Drawing.Size(15, 14);
            this.chb_trig6.TabIndex = 68;
            this.chb_trig6.UseVisualStyleBackColor = true;
            this.chb_trig6.CheckedChanged += new System.EventHandler(this.chb_trig_CheckedChanged);
            this.chb_trig6.MouseHover += new System.EventHandler(this.cb_trig_tmr_MouseHover);
            // 
            // pan_main
            // 
            this.pan_main.BackColor = System.Drawing.Color.Transparent;
            this.pan_main.Controls.Add(this.chb_trig6);
            this.pan_main.Controls.Add(this.chb_trig5);
            this.pan_main.Controls.Add(this.chb_trig4);
            this.pan_main.Controls.Add(this.chb_trig3);
            this.pan_main.Controls.Add(this.chb_trig2);
            this.pan_main.Controls.Add(this.chb_trig1);
            this.pan_main.Controls.Add(this.cb_trig_tmr1);
            this.pan_main.Controls.Add(this.nud_tmr1);
            this.pan_main.Controls.Add(this.nud_tmr2);
            this.pan_main.Controls.Add(this.nud_tmr3);
            this.pan_main.Controls.Add(this.nud_tmr4);
            this.pan_main.Controls.Add(this.lb_trig1);
            this.pan_main.Controls.Add(this.cb_key6);
            this.pan_main.Controls.Add(this.lb_trig2);
            this.pan_main.Controls.Add(this.lb_key6);
            this.pan_main.Controls.Add(this.lb_trig3);
            this.pan_main.Controls.Add(this.lb_trig4);
            this.pan_main.Controls.Add(this.cb_trig_tmr6);
            this.pan_main.Controls.Add(this.lb_tmr1_sec);
            this.pan_main.Controls.Add(this.lb_tmr6_sec);
            this.pan_main.Controls.Add(this.lb_tmr2_sec);
            this.pan_main.Controls.Add(this.lb_trig6);
            this.pan_main.Controls.Add(this.lb_tmr3_sec);
            this.pan_main.Controls.Add(this.nud_tmr6);
            this.pan_main.Controls.Add(this.lb_tmr4_sec);
            this.pan_main.Controls.Add(this.cb_key5);
            this.pan_main.Controls.Add(this.cb_trig_tmr2);
            this.pan_main.Controls.Add(this.lb_key5);
            this.pan_main.Controls.Add(this.cb_trig_tmr3);
            this.pan_main.Controls.Add(this.cb_trig_tmr5);
            this.pan_main.Controls.Add(this.cb_trig_tmr4);
            this.pan_main.Controls.Add(this.lb_tmr5_sec);
            this.pan_main.Controls.Add(this.lb_key1);
            this.pan_main.Controls.Add(this.lb_trig5);
            this.pan_main.Controls.Add(this.lb_key2);
            this.pan_main.Controls.Add(this.nud_tmr5);
            this.pan_main.Controls.Add(this.lb_key3);
            this.pan_main.Controls.Add(this.lb_key4);
            this.pan_main.Controls.Add(this.cb_key1);
            this.pan_main.Controls.Add(this.cb_key2);
            this.pan_main.Controls.Add(this.cb_key4);
            this.pan_main.Controls.Add(this.cb_key3);
            this.pan_main.Controls.Add(this.cb_tmr1);
            this.pan_main.Controls.Add(this.cb_tmr2);
            this.pan_main.Controls.Add(this.cb_tmr6);
            this.pan_main.Controls.Add(this.cb_tmr5);
            this.pan_main.Controls.Add(this.cb_tmr4);
            this.pan_main.Controls.Add(this.cb_tmr3);
            this.pan_main.Location = new System.Drawing.Point(2, 8);
            this.pan_main.Name = "pan_main";
            this.pan_main.Size = new System.Drawing.Size(455, 165);
            this.pan_main.TabIndex = 75;
            this.pan_main.Paint += new System.Windows.Forms.PaintEventHandler(this.pan_main_Paint);
            this.pan_main.MouseClick += new System.Windows.Forms.MouseEventHandler(this.D3Hotkeys_MouseClick);
            this.pan_main.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.d3hot_MouseDoubleClick);
            // 
            // D3Hotkeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 316);
            this.Controls.Add(this.pan_press_type);
            this.Controls.Add(this.lb_help);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lb_scroll);
            this.Controls.Add(this.lb_caps);
            this.Controls.Add(this.lb_num);
            this.Controls.Add(this.lb_debug);
            this.Controls.Add(this.pan_prof_name);
            this.Controls.Add(this.pan_hold);
            this.Controls.Add(this.b_load);
            this.Controls.Add(this.b_save);
            this.Controls.Add(this.b_opt);
            this.Controls.Add(this.lb_lang_name);
            this.Controls.Add(this.lb_lang);
            this.Controls.Add(this.cb_prof);
            this.Controls.Add(this.lb_prof);
            this.Controls.Add(this.lb_auth);
            this.Controls.Add(this.cb_start);
            this.Controls.Add(this.pan_proc);
            this.Controls.Add(this.pan_prog);
            this.Controls.Add(this.pan_set);
            this.Controls.Add(this.pan_main);
            this.Controls.Add(this.pan_opt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "D3Hotkeys";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diablo 3 Hotkeys ver. 2.5";
            this.Activated += new System.EventHandler(this.d3hot_Activated);
            this.Deactivate += new System.EventHandler(this.d3hot_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.D3Hotkeys_FormClosed);
            this.Load += new System.EventHandler(this.d3hot_Load);
            this.Shown += new System.EventHandler(this.d3hot_Shown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.D3Hotkeys_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.d3hot_MouseDoubleClick);
            this.Resize += new System.EventHandler(this.d3hot_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.pan_opt.ResumeLayout(false);
            this.pan_opt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_coold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_rand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_key_delay_ms)).EndInit();
            this.pan_hold.ResumeLayout(false);
            this.pan_hold.PerformLayout();
            this.pan_prof_name.ResumeLayout(false);
            this.pan_prof_name.PerformLayout();
            this.pan_proc.ResumeLayout(false);
            this.pan_proc.PerformLayout();
            this.pan_prog.ResumeLayout(false);
            this.pan_prog.PerformLayout();
            this.gb_set.ResumeLayout(false);
            this.gb_set.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_trig_delay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_trig_time)).EndInit();
            this.pan_press_type.ResumeLayout(false);
            this.pan_press_type.PerformLayout();
            this.pan_set.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tmr1)).EndInit();
            this.pan_main.ResumeLayout(false);
            this.pan_main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cb_start;
        private System.Windows.Forms.ComboBox cb_prog;
        private System.Windows.Forms.Label lb_area;
        private MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider mouseKeyEventProvider1;
        private System.Windows.Forms.Label lb_auth;
        private System.Windows.Forms.Label lb_lang;
        private System.Windows.Forms.ComboBox cb_prof;
        private System.Windows.Forms.Label lb_prof;
        private System.Windows.Forms.ComboBox cb_startstop;
        private System.Windows.Forms.Label lb_startstop;
        private System.Windows.Forms.Label lb_tp;
        private System.Windows.Forms.ComboBox cb_tp;
        private System.Windows.Forms.Label lb_tpdelay;
        private System.Windows.Forms.ComboBox cb_tpdelay;
        private System.Windows.Forms.ComboBox cb_proc;
        private System.Windows.Forms.Label lb_proc;
        private System.Windows.Forms.Label lb_lang_name;
        private System.Windows.Forms.NotifyIcon notify_d3h;
        private System.Windows.Forms.Button b_opt;
        private System.Windows.Forms.Panel pan_opt;
        private System.Windows.Forms.CheckBox chb_tray;
        private System.Windows.Forms.CheckBox chb_mult;
        private System.Windows.Forms.NumericUpDown nud_key_delay_ms;
        private System.Windows.Forms.ComboBox cb_key_delay;
        private System.Windows.Forms.Label lb_key_delay;
        private System.Windows.Forms.Label lb_key_delay_ms;
        private System.Windows.Forms.Label lb_key_delay_desc;
        private System.Windows.Forms.ToolTip tt_key;
        private System.Windows.Forms.CheckBox chb_hold;
        private System.Windows.Forms.Label lb_hold;
        private System.Windows.Forms.CheckBox chb_mpress;
        private System.Windows.Forms.Button b_save;
        private System.Windows.Forms.Button b_load;
        private System.Windows.Forms.CheckBox chb_saveload;
        private System.Windows.Forms.Label lb_nud_rand;
        private System.Windows.Forms.NumericUpDown nud_rand;
        private System.Windows.Forms.Label lb_rand;
        private System.Windows.Forms.Panel pan_hold;
        private System.Windows.Forms.Label lb_hot_prof;
        private System.Windows.Forms.ComboBox cb_hot_prof;
        private System.Windows.Forms.TextBox tb_prof_name;
        private System.Windows.Forms.Panel pan_prof_name;
        private System.Windows.Forms.ComboBox cb_map;
        private System.Windows.Forms.Label lb_map;
        private System.Windows.Forms.ComboBox cb_mapdelay;
        private System.Windows.Forms.Label lb_mapdelay;
        private System.Windows.Forms.CheckBox chb_users;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.CheckBox chb_ver_check;
        private System.Windows.Forms.Panel pan_proc;
        private System.Windows.Forms.Panel pan_prog;
        private System.Windows.Forms.Label lb_debug;
        private System.Windows.Forms.Label lb_ver_check;
        private System.Windows.Forms.Label lb_num;
        private System.Windows.Forms.Label lb_caps;
        private System.Windows.Forms.Label lb_scroll;
        private System.Windows.Forms.ComboBox cb_returndelay;
        private System.Windows.Forms.Label lb_returndelay;
        private MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider mouseKeyEventProvider2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lb_nud_coold;
        private System.Windows.Forms.NumericUpDown nud_coold;
        private System.Windows.Forms.Label lb_coold;
        private System.Windows.Forms.Label lb_help;
        private System.Windows.Forms.GroupBox gb_set;
        private System.Windows.Forms.Label lb_trig_delay;
        private System.Windows.Forms.NumericUpDown nud_trig_delay;
        private System.Windows.Forms.CheckBox cb_trig_once;
        private System.Windows.Forms.Label lb_trig_enable;
        private System.Windows.Forms.ComboBox cb_trig_enable;
        private System.Windows.Forms.Label lb_trig_time;
        private System.Windows.Forms.NumericUpDown nud_trig_time;
        private System.Windows.Forms.Button b_trig_ok;
        private System.Windows.Forms.Label lb_trig_delay_ms;
        private System.Windows.Forms.Label lb_trig_time_ms;
        private System.Windows.Forms.CheckBox chb_log;
        private System.Windows.Forms.Panel pan_press_type;
        private System.Windows.Forms.ComboBox cb_press_type;
        private System.Windows.Forms.Label lb_press_type;
        private System.Windows.Forms.Panel pan_set;
        private System.Windows.Forms.ComboBox cb_tmr3;
        private System.Windows.Forms.ComboBox cb_tmr4;
        private System.Windows.Forms.ComboBox cb_tmr5;
        private System.Windows.Forms.ComboBox cb_tmr6;
        private System.Windows.Forms.ComboBox cb_tmr2;
        private System.Windows.Forms.ComboBox cb_tmr1;
        private System.Windows.Forms.ComboBox cb_key3;
        private System.Windows.Forms.ComboBox cb_key4;
        private System.Windows.Forms.ComboBox cb_key2;
        private System.Windows.Forms.ComboBox cb_key1;
        private System.Windows.Forms.Label lb_key4;
        private System.Windows.Forms.Label lb_key3;
        private System.Windows.Forms.NumericUpDown nud_tmr5;
        private System.Windows.Forms.Label lb_key2;
        private System.Windows.Forms.Label lb_trig5;
        private System.Windows.Forms.Label lb_key1;
        private System.Windows.Forms.Label lb_tmr5_sec;
        private System.Windows.Forms.ComboBox cb_trig_tmr4;
        private System.Windows.Forms.ComboBox cb_trig_tmr5;
        private System.Windows.Forms.ComboBox cb_trig_tmr3;
        private System.Windows.Forms.Label lb_key5;
        private System.Windows.Forms.ComboBox cb_trig_tmr2;
        private System.Windows.Forms.ComboBox cb_key5;
        private System.Windows.Forms.Label lb_tmr4_sec;
        private System.Windows.Forms.NumericUpDown nud_tmr6;
        private System.Windows.Forms.Label lb_tmr3_sec;
        private System.Windows.Forms.Label lb_trig6;
        private System.Windows.Forms.Label lb_tmr2_sec;
        private System.Windows.Forms.Label lb_tmr6_sec;
        private System.Windows.Forms.Label lb_tmr1_sec;
        private System.Windows.Forms.ComboBox cb_trig_tmr6;
        private System.Windows.Forms.Label lb_trig4;
        private System.Windows.Forms.Label lb_trig3;
        private System.Windows.Forms.Label lb_key6;
        private System.Windows.Forms.Label lb_trig2;
        private System.Windows.Forms.ComboBox cb_key6;
        private System.Windows.Forms.Label lb_trig1;
        private System.Windows.Forms.NumericUpDown nud_tmr4;
        private System.Windows.Forms.NumericUpDown nud_tmr3;
        private System.Windows.Forms.NumericUpDown nud_tmr2;
        private System.Windows.Forms.NumericUpDown nud_tmr1;
        private System.Windows.Forms.ComboBox cb_trig_tmr1;
        private System.Windows.Forms.CheckBox chb_trig1;
        private System.Windows.Forms.CheckBox chb_trig2;
        private System.Windows.Forms.CheckBox chb_trig3;
        private System.Windows.Forms.CheckBox chb_trig4;
        private System.Windows.Forms.CheckBox chb_trig5;
        private System.Windows.Forms.CheckBox chb_trig6;
        private System.Windows.Forms.Panel pan_main;
    }
}

