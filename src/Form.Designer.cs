namespace BaldrickGUI
{
    partial class Form
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
        private void InitializeComponent() {
            tableLayoutPanel1 = new TableLayoutPanel();
            updateBaldrick_button = new Button();
            runBaldrick_button = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            local_dataSource_radioButton = new RadioButton();
            googleSheets_dataSource_radioButton = new RadioButton();
            flowLayoutPanel2 = new FlowLayoutPanel();
            label2 = new Label();
            dataSource_info = new Label();
            update_baldrick_info = new Label();
            run_baldrick_info = new Label();
            select_data_source_info = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.25703F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3714867F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3714867F));
            tableLayoutPanel1.Controls.Add(updateBaldrick_button, 0, 0);
            tableLayoutPanel1.Controls.Add(runBaldrick_button, 2, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Controls.Add(update_baldrick_info, 0, 1);
            tableLayoutPanel1.Controls.Add(run_baldrick_info, 2, 1);
            tableLayoutPanel1.Controls.Add(select_data_source_info, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.Size = new Size(731, 347);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // updateBaldrick_button
            // 
            updateBaldrick_button.AutoSize = true;
            updateBaldrick_button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            updateBaldrick_button.Dock = DockStyle.Fill;
            updateBaldrick_button.Font = new Font("Consolas", 15.75F, FontStyle.Bold);
            updateBaldrick_button.Location = new Point(3, 3);
            updateBaldrick_button.Name = "updateBaldrick_button";
            updateBaldrick_button.Size = new Size(237, 306);
            updateBaldrick_button.TabIndex = 0;
            updateBaldrick_button.Text = "Update Baldrick";
            updateBaldrick_button.UseVisualStyleBackColor = true;
            updateBaldrick_button.Click += updateBaldrick_button_Click;
            // 
            // runBaldrick_button
            // 
            runBaldrick_button.AutoSize = true;
            runBaldrick_button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            runBaldrick_button.Dock = DockStyle.Fill;
            runBaldrick_button.Enabled = false;
            runBaldrick_button.Font = new Font("Consolas", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            runBaldrick_button.ForeColor = SystemColors.ControlText;
            runBaldrick_button.Location = new Point(489, 3);
            runBaldrick_button.Name = "runBaldrick_button";
            runBaldrick_button.Size = new Size(239, 306);
            runBaldrick_button.TabIndex = 1;
            runBaldrick_button.Text = "Run Baldrick";
            runBaldrick_button.UseVisualStyleBackColor = true;
            runBaldrick_button.Click += runBaldrick_button_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel1, 0, 1);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel2, 0, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(246, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RightToLeft = RightToLeft.No;
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tableLayoutPanel2.Size = new Size(237, 306);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Consolas", 15.75F, FontStyle.Bold);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(231, 30);
            label1.TabIndex = 0;
            label1.Text = "Select Data Source";
            label1.TextAlign = ContentAlignment.BottomCenter;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(local_dataSource_radioButton);
            flowLayoutPanel1.Controls.Add(googleSheets_dataSource_radioButton);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(3, 33);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(231, 131);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // local_dataSource_radioButton
            // 
            local_dataSource_radioButton.AutoSize = true;
            local_dataSource_radioButton.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            local_dataSource_radioButton.Location = new Point(25, 3);
            local_dataSource_radioButton.Margin = new Padding(25, 3, 3, 3);
            local_dataSource_radioButton.Name = "local_dataSource_radioButton";
            local_dataSource_radioButton.Size = new Size(78, 26);
            local_dataSource_radioButton.TabIndex = 0;
            local_dataSource_radioButton.TabStop = true;
            local_dataSource_radioButton.Text = "Local";
            local_dataSource_radioButton.UseVisualStyleBackColor = true;
            local_dataSource_radioButton.CheckedChanged += local_dataSource_radioButton_CheckedChanged;
            // 
            // googleSheets_dataSource_radioButton
            // 
            googleSheets_dataSource_radioButton.AutoSize = true;
            googleSheets_dataSource_radioButton.Font = new Font("Consolas", 14.25F);
            googleSheets_dataSource_radioButton.Location = new Point(25, 35);
            googleSheets_dataSource_radioButton.Margin = new Padding(25, 3, 3, 3);
            googleSheets_dataSource_radioButton.Name = "googleSheets_dataSource_radioButton";
            googleSheets_dataSource_radioButton.Size = new Size(158, 26);
            googleSheets_dataSource_radioButton.TabIndex = 1;
            googleSheets_dataSource_radioButton.TabStop = true;
            googleSheets_dataSource_radioButton.Text = "Google Sheets";
            googleSheets_dataSource_radioButton.UseVisualStyleBackColor = true;
            googleSheets_dataSource_radioButton.CheckedChanged += googleSheets_dataSource_radioButton_CheckedChanged;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel2.Controls.Add(label2);
            flowLayoutPanel2.Controls.Add(dataSource_info);
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(3, 170);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(231, 133);
            flowLayoutPanel2.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 12F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label2.Location = new Point(3, 3);
            label2.Margin = new Padding(3, 3, 3, 0);
            label2.Name = "label2";
            label2.Size = new Size(216, 19);
            label2.TabIndex = 0;
            label2.Text = "Data Source Information";
            // 
            // dataSource_info
            // 
            dataSource_info.AutoSize = true;
            dataSource_info.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataSource_info.Location = new Point(3, 25);
            dataSource_info.Margin = new Padding(3, 3, 3, 0);
            dataSource_info.Name = "dataSource_info";
            dataSource_info.Size = new Size(109, 13);
            dataSource_info.TabIndex = 1;
            dataSource_info.Text = "No data selected.";
            // 
            // update_baldrick_info
            // 
            update_baldrick_info.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            update_baldrick_info.AutoSize = true;
            update_baldrick_info.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            update_baldrick_info.Location = new Point(3, 312);
            update_baldrick_info.Name = "update_baldrick_info";
            update_baldrick_info.Size = new Size(237, 35);
            update_baldrick_info.TabIndex = 3;
            update_baldrick_info.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // run_baldrick_info
            // 
            run_baldrick_info.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            run_baldrick_info.AutoSize = true;
            run_baldrick_info.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            run_baldrick_info.Location = new Point(489, 312);
            run_baldrick_info.Name = "run_baldrick_info";
            run_baldrick_info.Size = new Size(239, 35);
            run_baldrick_info.TabIndex = 4;
            run_baldrick_info.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // select_data_source_info
            // 
            select_data_source_info.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            select_data_source_info.AutoSize = true;
            select_data_source_info.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            select_data_source_info.Location = new Point(246, 312);
            select_data_source_info.Name = "select_data_source_info";
            select_data_source_info.Size = new Size(237, 35);
            select_data_source_info.TabIndex = 5;
            select_data_source_info.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(731, 347);
            Controls.Add(tableLayoutPanel1);
            MinimumSize = new Size(747, 386);
            Name = "Form";
            Text = "BaldrickGUI";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button updateBaldrick_button;
        private Button runBaldrick_button;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private RadioButton local_dataSource_radioButton;
        private RadioButton googleSheets_dataSource_radioButton;
        private Label label2;
        private Label dataSource_info;
        private Label update_baldrick_info;
        private Label run_baldrick_info;
        private Label select_data_source_info;
    }
}
