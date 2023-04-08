namespace obs_client
{
    partial class obs_client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(obs_client));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.listViewObs = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastModify = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 18);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(153, 43);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(190, 18);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(153, 43);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // listViewObs
            // 
            this.listViewObs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.size,
            this.lastModify});
            this.listViewObs.HideSelection = false;
            this.listViewObs.Location = new System.Drawing.Point(2, 115);
            this.listViewObs.Name = "listViewObs";
            this.listViewObs.Scrollable = false;
            this.listViewObs.Size = new System.Drawing.Size(1443, 745);
            this.listViewObs.TabIndex = 4;
            this.listViewObs.UseCompatibleStateImageBehavior = false;
            this.listViewObs.View = System.Windows.Forms.View.Details;
            this.listViewObs.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listViewObs_ColumnWidthChanging);
            // 
            // name
            // 
            this.name.Text = "文件名称";
            this.name.Width = 600;
            // 
            // size
            // 
            this.size.Text = "文件大小（单位：M）";
            this.size.Width = 200;
            // 
            // lastModify
            // 
            this.lastModify.Text = "最后修改时间";
            this.lastModify.Width = 200;
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelResult.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelResult.Location = new System.Drawing.Point(134, 78);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(106, 24);
            this.labelResult.TabIndex = 3;
            this.labelResult.Text = "暂无结果";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "执行结果：";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(368, 18);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(153, 43);
            this.btnDownload.TabIndex = 6;
            this.btnDownload.Text = "下载";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(546, 18);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(153, 43);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // obs_client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1445, 860);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.listViewObs);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnRefresh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "obs_client";
            this.Text = "OBS客户端";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ListView listViewObs;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader size;
        private System.Windows.Forms.ColumnHeader lastModify;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnDelete;
    }
}