namespace CT_Vaja1
{
    partial class Form1
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
            this.imageBox = new Emgu.CV.UI.ImageBox();
            this.btn_uploadImg = new System.Windows.Forms.Button();
            this.btn_applyDifferentLut = new System.Windows.Forms.Button();
            this.btn_lzwCompress = new System.Windows.Forms.Button();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.btn_compress = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Location = new System.Drawing.Point(12, 61);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(933, 585);
            this.imageBox.TabIndex = 2;
            this.imageBox.TabStop = false;
            // 
            // btn_uploadImg
            // 
            this.btn_uploadImg.Location = new System.Drawing.Point(166, 14);
            this.btn_uploadImg.Name = "btn_uploadImg";
            this.btn_uploadImg.Size = new System.Drawing.Size(162, 43);
            this.btn_uploadImg.TabIndex = 2;
            this.btn_uploadImg.Text = "Upload IMG";
            this.btn_uploadImg.UseVisualStyleBackColor = true;
            this.btn_uploadImg.Click += new System.EventHandler(this.btn_uploadImg_Click);
            // 
            // btn_applyDifferentLut
            // 
            this.btn_applyDifferentLut.Location = new System.Drawing.Point(334, 14);
            this.btn_applyDifferentLut.Name = "btn_applyDifferentLut";
            this.btn_applyDifferentLut.Size = new System.Drawing.Size(162, 43);
            this.btn_applyDifferentLut.TabIndex = 3;
            this.btn_applyDifferentLut.Text = "Apply LUT";
            this.btn_applyDifferentLut.UseVisualStyleBackColor = true;
            this.btn_applyDifferentLut.Click += new System.EventHandler(this.btn_uploadLut_Click);
            // 
            // btn_lzwCompress
            // 
            this.btn_lzwCompress.Location = new System.Drawing.Point(772, 14);
            this.btn_lzwCompress.Name = "btn_lzwCompress";
            this.btn_lzwCompress.Size = new System.Drawing.Size(162, 43);
            this.btn_lzwCompress.TabIndex = 5;
            this.btn_lzwCompress.Text = "LZW Compress";
            this.btn_lzwCompress.UseVisualStyleBackColor = true;
            this.btn_lzwCompress.Click += new System.EventHandler(this.btn_lzwCompress_Click);
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(13, 34);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(49, 20);
            this.txtWidth.TabIndex = 0;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(68, 34);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(49, 20);
            this.txtHeight.TabIndex = 1;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(13, 13);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(35, 13);
            this.lblWidth.TabIndex = 8;
            this.lblWidth.Text = "Width";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(70, 14);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(38, 13);
            this.lblHeight.TabIndex = 9;
            this.lblHeight.Text = "Height";
            // 
            // btn_compress
            // 
            this.btn_compress.Location = new System.Drawing.Point(604, 14);
            this.btn_compress.Name = "btn_compress";
            this.btn_compress.Size = new System.Drawing.Size(162, 43);
            this.btn_compress.TabIndex = 4;
            this.btn_compress.Text = "Tree Compression";
            this.btn_compress.UseVisualStyleBackColor = true;
            this.btn_compress.Click += new System.EventHandler(this.btn_compress_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 658);
            this.Controls.Add(this.btn_compress);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.btn_lzwCompress);
            this.Controls.Add(this.btn_applyDifferentLut);
            this.Controls.Add(this.btn_uploadImg);
            this.Controls.Add(this.imageBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox;
        private System.Windows.Forms.Button btn_uploadImg;
        private System.Windows.Forms.Button btn_applyDifferentLut;
        private System.Windows.Forms.Button btn_lzwCompress;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Button btn_compress;
    }
}

