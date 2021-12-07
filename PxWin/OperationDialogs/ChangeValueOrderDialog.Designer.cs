namespace PCAxis.Desktop.OperationDialogs
{
    partial class ChangeValueOrderDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeValueOrderDialog));
            this.lbFromOrder = new System.Windows.Forms.ListBox();
            this.lbToOrder = new System.Windows.Forms.ListBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSortAscending = new System.Windows.Forms.Button();
            this.btnSortDescending = new System.Windows.Forms.Button();
            this.btnMoveRest = new System.Windows.Forms.Button();
            this.btnToOrder = new System.Windows.Forms.Button();
            this.btnFromOrder = new System.Windows.Forms.Button();
            this.lblFromOrder = new System.Windows.Forms.Label();
            this.lblToOrder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbFromOrder
            // 
            this.lbFromOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbFromOrder.FormattingEnabled = true;
            this.lbFromOrder.Location = new System.Drawing.Point(12, 27);
            this.lbFromOrder.Name = "lbFromOrder";
            this.lbFromOrder.Size = new System.Drawing.Size(188, 197);
            this.lbFromOrder.TabIndex = 0;
            // 
            // lbToOrder
            // 
            this.lbToOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbToOrder.FormattingEnabled = true;
            this.lbToOrder.Location = new System.Drawing.Point(247, 27);
            this.lbToOrder.Name = "lbToOrder";
            this.lbToOrder.Size = new System.Drawing.Size(188, 197);
            this.lbToOrder.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(441, 26);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(105, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(441, 55);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSortAscending
            // 
            this.btnSortAscending.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSortAscending.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSortAscending.Location = new System.Drawing.Point(441, 144);
            this.btnSortAscending.Name = "btnSortAscending";
            this.btnSortAscending.Size = new System.Drawing.Size(105, 23);
            this.btnSortAscending.TabIndex = 4;
            this.btnSortAscending.Text = "Sort ascending";
            this.btnSortAscending.UseVisualStyleBackColor = false;
            this.btnSortAscending.Click += new System.EventHandler(this.btnSortAscending_Click);
            // 
            // btnSortDescending
            // 
            this.btnSortDescending.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSortDescending.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSortDescending.Location = new System.Drawing.Point(441, 173);
            this.btnSortDescending.Name = "btnSortDescending";
            this.btnSortDescending.Size = new System.Drawing.Size(105, 23);
            this.btnSortDescending.TabIndex = 5;
            this.btnSortDescending.Text = "Sort descending";
            this.btnSortDescending.UseVisualStyleBackColor = false;
            this.btnSortDescending.Click += new System.EventHandler(this.btnSortDescending_Click);
            // 
            // btnMoveRest
            // 
            this.btnMoveRest.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnMoveRest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveRest.Location = new System.Drawing.Point(441, 202);
            this.btnMoveRest.Name = "btnMoveRest";
            this.btnMoveRest.Size = new System.Drawing.Size(105, 23);
            this.btnMoveRest.TabIndex = 6;
            this.btnMoveRest.Text = "Move rest";
            this.btnMoveRest.UseVisualStyleBackColor = false;
            this.btnMoveRest.Click += new System.EventHandler(this.btnMoveRest_Click);
            // 
            // btnToOrder
            // 
            this.btnToOrder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnToOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToOrder.Location = new System.Drawing.Point(206, 66);
            this.btnToOrder.Name = "btnToOrder";
            this.btnToOrder.Size = new System.Drawing.Size(35, 23);
            this.btnToOrder.TabIndex = 7;
            this.btnToOrder.Text = ">>";
            this.btnToOrder.UseVisualStyleBackColor = false;
            this.btnToOrder.Click += new System.EventHandler(this.btnToOrder_Click);
            // 
            // btnFromOrder
            // 
            this.btnFromOrder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnFromOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFromOrder.Location = new System.Drawing.Point(206, 114);
            this.btnFromOrder.Name = "btnFromOrder";
            this.btnFromOrder.Size = new System.Drawing.Size(35, 23);
            this.btnFromOrder.TabIndex = 8;
            this.btnFromOrder.Text = "<<";
            this.btnFromOrder.UseVisualStyleBackColor = false;
            this.btnFromOrder.Click += new System.EventHandler(this.btnFromOrder_Click);
            // 
            // lblFromOrder
            // 
            this.lblFromOrder.AutoSize = true;
            this.lblFromOrder.Location = new System.Drawing.Point(9, 8);
            this.lblFromOrder.Name = "lblFromOrder";
            this.lblFromOrder.Size = new System.Drawing.Size(57, 13);
            this.lblFromOrder.TabIndex = 9;
            this.lblFromOrder.Text = "From order";
            // 
            // lblToOrder
            // 
            this.lblToOrder.AutoSize = true;
            this.lblToOrder.Location = new System.Drawing.Point(244, 8);
            this.lblToOrder.Name = "lblToOrder";
            this.lblToOrder.Size = new System.Drawing.Size(47, 13);
            this.lblToOrder.TabIndex = 10;
            this.lblToOrder.Text = "To order";
            // 
            // ChangeValueOrderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 237);
            this.Controls.Add(this.lblToOrder);
            this.Controls.Add(this.lblFromOrder);
            this.Controls.Add(this.btnFromOrder);
            this.Controls.Add(this.btnToOrder);
            this.Controls.Add(this.btnMoveRest);
            this.Controls.Add(this.btnSortDescending);
            this.Controls.Add(this.btnSortAscending);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lbToOrder);
            this.Controls.Add(this.lbFromOrder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(573, 275);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(573, 275);
            this.Name = "ChangeValueOrderDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChangeValueOrderDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbFromOrder;
        private System.Windows.Forms.ListBox lbToOrder;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSortAscending;
        private System.Windows.Forms.Button btnSortDescending;
        private System.Windows.Forms.Button btnMoveRest;
        private System.Windows.Forms.Button btnToOrder;
        private System.Windows.Forms.Button btnFromOrder;
        private System.Windows.Forms.Label lblFromOrder;
        private System.Windows.Forms.Label lblToOrder;
    }
}