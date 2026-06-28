namespace GMapApp;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
        this.panelTop = new System.Windows.Forms.Panel();
        this.btnDownload = new System.Windows.Forms.Button();
        this.btnOnlineOffline = new System.Windows.Forms.Button();
        this.btnSearch = new System.Windows.Forms.Button();
        this.txtSearch = new System.Windows.Forms.TextBox();
        this.toolStrip1 = new System.Windows.Forms.ToolStrip();
        this.btnPin = new System.Windows.Forms.ToolStripButton();
        this.btnCircle = new System.Windows.Forms.ToolStripButton();
        this.btnText = new System.Windows.Forms.ToolStripButton();
        this.panelTop.SuspendLayout();
        this.toolStrip1.SuspendLayout();
        this.SuspendLayout();
        //
        // gMapControl1
        //
        this.gMapControl1.Bearing = 0F;
        this.gMapControl1.CanDragMap = true;
        this.gMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
        this.gMapControl1.GrayScaleMode = false;
        this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
        this.gMapControl1.LevelsKeepInMemory = 5;
        this.gMapControl1.Location = new System.Drawing.Point(0, 40);
        this.gMapControl1.MarkersEnabled = true;
        this.gMapControl1.MaxZoom = 2;
        this.gMapControl1.MinZoom = 2;
        this.gMapControl1.MouseWheelZoomEnabled = true;
        this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
        this.gMapControl1.Name = "gMapControl1";
        this.gMapControl1.NegativeMode = false;
        this.gMapControl1.PolygonsEnabled = true;
        this.gMapControl1.RetryLoadTile = 0;
        this.gMapControl1.RoutesEnabled = true;
        this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
        this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
        this.gMapControl1.ShowTileGridLines = false;
        this.gMapControl1.Size = new System.Drawing.Size(800, 410);
        this.gMapControl1.TabIndex = 0;
        this.gMapControl1.Zoom = 0D;
        this.gMapControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gMapControl1_MouseClick);
        //
        // panelTop
        //
        this.panelTop.Controls.Add(this.btnDownload);
        this.panelTop.Controls.Add(this.btnOnlineOffline);
        this.panelTop.Controls.Add(this.btnSearch);
        this.panelTop.Controls.Add(this.txtSearch);
        this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
        this.panelTop.Location = new System.Drawing.Point(0, 0);
        this.panelTop.Name = "panelTop";
        this.panelTop.Size = new System.Drawing.Size(800, 40);
        this.panelTop.TabIndex = 1;
        //
        // btnDownload
        //
        this.btnDownload.Location = new System.Drawing.Point(550, 8);
        this.btnDownload.Name = "btnDownload";
        this.btnDownload.Size = new System.Drawing.Size(120, 23);
        this.btnDownload.TabIndex = 3;
        this.btnDownload.Text = "Download Area";
        this.btnDownload.UseVisualStyleBackColor = true;
        this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
        //
        // btnOnlineOffline
        //
        this.btnOnlineOffline.Location = new System.Drawing.Point(440, 8);
        this.btnOnlineOffline.Name = "btnOnlineOffline";
        this.btnOnlineOffline.Size = new System.Drawing.Size(100, 23);
        this.btnOnlineOffline.TabIndex = 2;
        this.btnOnlineOffline.Text = "Go Offline";
        this.btnOnlineOffline.UseVisualStyleBackColor = true;
        this.btnOnlineOffline.Click += new System.EventHandler(this.btnOnlineOffline_Click);
        //
        // btnSearch
        //
        this.btnSearch.Location = new System.Drawing.Point(350, 8);
        this.btnSearch.Name = "btnSearch";
        this.btnSearch.Size = new System.Drawing.Size(75, 23);
        this.btnSearch.TabIndex = 1;
        this.btnSearch.Text = "Search";
        this.btnSearch.UseVisualStyleBackColor = true;
        this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
        //
        // txtSearch
        //
        this.txtSearch.Location = new System.Drawing.Point(12, 9);
        this.txtSearch.Name = "txtSearch";
        this.txtSearch.Size = new System.Drawing.Size(332, 23);
        this.txtSearch.TabIndex = 0;
        //
        // toolStrip1
        //
        this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
        this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.btnPin,
        this.btnCircle,
        this.btnText});
        this.toolStrip1.Location = new System.Drawing.Point(0, 40);
        this.toolStrip1.Name = "toolStrip1";
        this.toolStrip1.Size = new System.Drawing.Size(32, 410);
        this.toolStrip1.TabIndex = 2;
        this.toolStrip1.Text = "toolStrip1";
        //
        // btnPin
        //
        this.btnPin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.btnPin.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.btnPin.Name = "btnPin";
        this.btnPin.Size = new System.Drawing.Size(29, 19);
        this.btnPin.Text = "Pin";
        this.btnPin.Click += new System.EventHandler(this.btnPin_Click);
        //
        // btnCircle
        //
        this.btnCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.btnCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.btnCircle.Name = "btnCircle";
        this.btnCircle.Size = new System.Drawing.Size(29, 19);
        this.btnCircle.Text = "Circ";
        this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
        //
        // btnText
        //
        this.btnText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.btnText.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.btnText.Name = "btnText";
        this.btnText.Size = new System.Drawing.Size(29, 19);
        this.btnText.Text = "Txt";
        this.btnText.Click += new System.EventHandler(this.btnText_Click);
        //
        // Form1
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.toolStrip1);
        this.Controls.Add(this.gMapControl1);
        this.Controls.Add(this.panelTop);
        this.Name = "Form1";
        this.Text = "GMap Windows 11 App";
        this.Load += new System.EventHandler(this.Form1_Load);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
        this.panelTop.ResumeLayout(false);
        this.panelTop.PerformLayout();
        this.toolStrip1.ResumeLayout(false);
        this.toolStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private GMap.NET.WindowsForms.GMapControl gMapControl1;
    private System.Windows.Forms.Panel panelTop;
    private System.Windows.Forms.Button btnSearch;
    private System.Windows.Forms.TextBox txtSearch;
    private System.Windows.Forms.Button btnOnlineOffline;
    private System.Windows.Forms.Button btnDownload;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnPin;
    private System.Windows.Forms.ToolStripButton btnCircle;
    private System.Windows.Forms.ToolStripButton btnText;
}
