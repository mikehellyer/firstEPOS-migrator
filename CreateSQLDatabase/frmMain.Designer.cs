namespace CreateSQLDatabase
{
    partial class frmMain
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
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.btnCreateDB = new System.Windows.Forms.Button();
            this.btnCreateTable = new System.Windows.Forms.Button();
            this.btnViewData = new System.Windows.Forms.Button();
            this.btnDropTable = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnDropDB = new System.Windows.Forms.Button();
            this.btnSelectFirstEpos = new System.Windows.Forms.Button();
            this.lblSelectedDatabase = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listProgress = new System.Windows.Forms.ListBox();
            this.btnCsv = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid1.Location = new System.Drawing.Point(421, 106);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(504, 292);
            this.dataGrid1.TabIndex = 0;
            // 
            // btnCreateDB
            // 
            this.btnCreateDB.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnCreateDB.Location = new System.Drawing.Point(538, 22);
            this.btnCreateDB.Name = "btnCreateDB";
            this.btnCreateDB.Size = new System.Drawing.Size(88, 30);
            this.btnCreateDB.TabIndex = 1;
            this.btnCreateDB.Text = "CreateSQL DB";
            this.btnCreateDB.UseVisualStyleBackColor = true;
            this.btnCreateDB.Click += new System.EventHandler(this.btnCreateDB_Click);
            // 
            // btnCreateTable
            // 
            this.btnCreateTable.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnCreateTable.Location = new System.Drawing.Point(641, 22);
            this.btnCreateTable.Name = "btnCreateTable";
            this.btnCreateTable.Size = new System.Drawing.Size(88, 30);
            this.btnCreateTable.TabIndex = 2;
            this.btnCreateTable.Text = "Create Tables";
            this.btnCreateTable.UseVisualStyleBackColor = true;
            this.btnCreateTable.Click += new System.EventHandler(this.btnCreateTable_Click);
            // 
            // btnViewData
            // 
            this.btnViewData.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnViewData.Location = new System.Drawing.Point(735, 22);
            this.btnViewData.Name = "btnViewData";
            this.btnViewData.Size = new System.Drawing.Size(88, 30);
            this.btnViewData.TabIndex = 6;
            this.btnViewData.Text = "View Data";
            this.btnViewData.UseVisualStyleBackColor = true;
            this.btnViewData.Click += new System.EventHandler(this.btnViewData_Click);
            // 
            // btnDropTable
            // 
            this.btnDropTable.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnDropTable.Location = new System.Drawing.Point(641, 58);
            this.btnDropTable.Name = "btnDropTable";
            this.btnDropTable.Size = new System.Drawing.Size(88, 30);
            this.btnDropTable.TabIndex = 10;
            this.btnDropTable.Text = "Drop Table";
            this.btnDropTable.UseVisualStyleBackColor = true;
            this.btnDropTable.Click += new System.EventHandler(this.btnDropTable_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnQuit.Location = new System.Drawing.Point(973, 398);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 12;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnDropDB
            // 
            this.btnDropDB.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnDropDB.Location = new System.Drawing.Point(538, 57);
            this.btnDropDB.Name = "btnDropDB";
            this.btnDropDB.Size = new System.Drawing.Size(88, 30);
            this.btnDropDB.TabIndex = 13;
            this.btnDropDB.Text = "Drop DB";
            this.btnDropDB.UseVisualStyleBackColor = true;
            this.btnDropDB.Click += new System.EventHandler(this.btnDropDB_Click);
            // 
            // btnSelectFirstEpos
            // 
            this.btnSelectFirstEpos.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnSelectFirstEpos.Location = new System.Drawing.Point(12, 24);
            this.btnSelectFirstEpos.Name = "btnSelectFirstEpos";
            this.btnSelectFirstEpos.Size = new System.Drawing.Size(121, 30);
            this.btnSelectFirstEpos.TabIndex = 14;
            this.btnSelectFirstEpos.Text = "Select FirstEpos data";
            this.btnSelectFirstEpos.UseVisualStyleBackColor = true;
            this.btnSelectFirstEpos.Click += new System.EventHandler(this.btnSelectFirstEpos_Click);
            // 
            // lblSelectedDatabase
            // 
            this.lblSelectedDatabase.AutoSize = true;
            this.lblSelectedDatabase.Location = new System.Drawing.Point(139, 27);
            this.lblSelectedDatabase.Name = "lblSelectedDatabase";
            this.lblSelectedDatabase.Size = new System.Drawing.Size(115, 13);
            this.lblSelectedDatabase.TabIndex = 15;
            this.lblSelectedDatabase.Text = "No Database Selected";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 121);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(176, 277);
            this.listBox1.TabIndex = 16;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(205, 121);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(176, 277);
            this.listBox2.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.MaximumSize = new System.Drawing.Size(200, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(202, 75);
            this.label2.MaximumSize = new System.Drawing.Size(200, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(808, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "label3";
            // 
            // listProgress
            // 
            this.listProgress.FormattingEnabled = true;
            this.listProgress.Location = new System.Drawing.Point(849, 18);
            this.listProgress.Name = "listProgress";
            this.listProgress.Size = new System.Drawing.Size(199, 69);
            this.listProgress.TabIndex = 21;
            // 
            // btnCsv
            // 
            this.btnCsv.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnCsv.Location = new System.Drawing.Point(931, 94);
            this.btnCsv.Name = "btnCsv";
            this.btnCsv.Size = new System.Drawing.Size(117, 40);
            this.btnCsv.TabIndex = 22;
            this.btnCsv.Text = "Write Products to CSV";
            this.btnCsv.UseVisualStyleBackColor = true;
            this.btnCsv.Click += new System.EventHandler(this.btnCsv_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 457);
            this.Controls.Add(this.btnCsv);
            this.Controls.Add(this.listProgress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lblSelectedDatabase);
            this.Controls.Add(this.btnSelectFirstEpos);
            this.Controls.Add(this.btnDropDB);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnDropTable);
            this.Controls.Add(this.btnViewData);
            this.Controls.Add(this.btnCreateTable);
            this.Controls.Add(this.btnCreateDB);
            this.Controls.Add(this.dataGrid1);
            this.ForeColor = System.Drawing.Color.MediumBlue;
            this.Name = "frmMain";
            this.Text = "Create SQL Database";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid1;
        private System.Windows.Forms.Button btnCreateDB;
        private System.Windows.Forms.Button btnCreateTable;
        private System.Windows.Forms.Button btnViewData;
        private System.Windows.Forms.Button btnDropTable;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnDropDB;
        private System.Windows.Forms.Button btnSelectFirstEpos;
        private System.Windows.Forms.Label lblSelectedDatabase;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listProgress;
        private System.Windows.Forms.Button btnCsv;
    }
}

