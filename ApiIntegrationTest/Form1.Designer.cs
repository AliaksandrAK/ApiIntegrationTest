namespace ApiIntegrationTest
{
    partial class MainForm
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
            this.btnTest = new System.Windows.Forms.Button();
            this.resultView = new System.Windows.Forms.TreeView();
            this.GetAllWo = new System.Windows.Forms.Button();
            this.WoFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(12, 50);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(110, 24);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // resultView
            // 
            this.resultView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultView.Location = new System.Drawing.Point(12, 102);
            this.resultView.Name = "resultView";
            this.resultView.Size = new System.Drawing.Size(724, 261);
            this.resultView.TabIndex = 1;
            // 
            // GetAllWo
            // 
            this.GetAllWo.Location = new System.Drawing.Point(152, 50);
            this.GetAllWo.Name = "GetAllWo";
            this.GetAllWo.Size = new System.Drawing.Size(121, 23);
            this.GetAllWo.TabIndex = 2;
            this.GetAllWo.Text = "Get ALL WO";
            this.GetAllWo.UseVisualStyleBackColor = true;
            this.GetAllWo.Click += new System.EventHandler(this.GetAllWo_Click);
            // 
            // WoFind
            // 
            this.WoFind.Location = new System.Drawing.Point(383, 54);
            this.WoFind.Name = "WoFind";
            this.WoFind.Size = new System.Drawing.Size(100, 22);
            this.WoFind.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(302, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "FindWO";
            // 
            // btnAdTest
            // 
            this.btnAdTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdTest.Location = new System.Drawing.Point(604, 50);
            this.btnAdTest.Name = "btnAdTest";
            this.btnAdTest.Size = new System.Drawing.Size(128, 23);
            this.btnAdTest.TabIndex = 5;
            this.btnAdTest.Text = "Additional Tests";
            this.btnAdTest.UseVisualStyleBackColor = true;
            this.btnAdTest.Click += new System.EventHandler(this.btnAdTest_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 375);
            this.Controls.Add(this.btnAdTest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WoFind);
            this.Controls.Add(this.GetAllWo);
            this.Controls.Add(this.resultView);
            this.Controls.Add(this.btnTest);
            this.Name = "MainForm";
            this.Text = "Api Integration Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TreeView resultView;
        private System.Windows.Forms.Button GetAllWo;
        private System.Windows.Forms.TextBox WoFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdTest;
    }
}

