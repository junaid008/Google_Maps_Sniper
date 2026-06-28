using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GMapApp;

public partial class Form1 : Form
{
    private GMapOverlay markersOverlay = new GMapOverlay("markers");
    private bool isOffline = false;

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        // Initialize Map
        gMapControl1.MapProvider = GoogleSatelliteMapProvider.Instance;
        GMaps.Instance.Mode = AccessMode.ServerAndCache;
        gMapControl1.Position = new PointLatLng(-34.097, 18.504); // Default to somewhere
        gMapControl1.MinZoom = 1;
        gMapControl1.MaxZoom = 20;
        gMapControl1.Zoom = 10;
        gMapControl1.ShowCenter = false;

        gMapControl1.Overlays.Add(markersOverlay);

        LoadAnnotations();
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        string address = txtSearch.Text;
        if (string.IsNullOrEmpty(address)) return;

        GeoCoderStatusCode status = gMapControl1.SetPositionByKeywords(address);
        if (status != GeoCoderStatusCode.OK)
        {
            MessageBox.Show("Unable to find location: " + status.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            gMapControl1.Zoom = 15;
        }
    }

    private void btnOnlineOffline_Click(object sender, EventArgs e)
    {
        isOffline = !isOffline;
        if (isOffline)
        {
            GMaps.Instance.Mode = AccessMode.CacheOnly;
            btnOnlineOffline.Text = "Go Online";
            MessageBox.Show("Map is now in Offline mode (Cache only).", "Mode Changed");
        }
        else
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            btnOnlineOffline.Text = "Go Offline";
            MessageBox.Show("Map is now in Online mode (Server and Cache).", "Mode Changed");
        }
    }

    private void btnDownload_Click(object sender, EventArgs e)
    {
        RectLatLng area = gMapControl1.SelectedArea;
        if (area.IsEmpty)
        {
            MessageBox.Show("Please select an area on the map first (Hold ALT + Drag Mouse).", "No Area Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using (Form downloadForm = new Form())
        {
            downloadForm.Text = "Download Area";
            downloadForm.Size = new Size(300, 200);
            downloadForm.StartPosition = FormStartPosition.CenterParent;

            Label lbl = new Label() { Text = "Max Zoom Level (1-20):", Left = 10, Top = 20, Width = 150 };
            NumericUpDown zoomInput = new NumericUpDown() { Left = 160, Top = 18, Width = 50, Minimum = 1, Maximum = 20, Value = 15 };
            Button btnStart = new Button() { Text = "Start Download", Left = 10, Top = 60, Width = 100 };

            btnStart.Click += (s, ev) =>
            {
                int maxZoom = (int)zoomInput.Value;
                downloadForm.Close();
                StartBulkDownload(area, maxZoom);
            };

            downloadForm.Controls.Add(lbl);
            downloadForm.Controls.Add(zoomInput);
            downloadForm.Controls.Add(btnStart);
            downloadForm.ShowDialog();
        }
    }

    private void StartBulkDownload(RectLatLng area, int maxZoom)
    {
        try
        {
            for (int i = (int)gMapControl1.Zoom; i <= maxZoom; i++)
            {
                TilePrefetcher obj = new TilePrefetcher();
                obj.ShowCompleteMessage = true;
                obj.Start(area, i, gMapControl1.MapProvider, 100, 0);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error during download: " + ex.Message);
        }
    }

    private enum AnnotationMode { None, Pin, Circle, Text }
    private AnnotationMode currentMode = AnnotationMode.None;

    private void btnPin_Click(object sender, EventArgs e)
    {
        currentMode = AnnotationMode.Pin;
        gMapControl1.Cursor = Cursors.Cross;
    }

    private void btnCircle_Click(object sender, EventArgs e)
    {
        currentMode = AnnotationMode.Circle;
        gMapControl1.Cursor = Cursors.Cross;
    }

    private void btnText_Click(object sender, EventArgs e)
    {
        currentMode = AnnotationMode.Text;
        gMapControl1.Cursor = Cursors.Cross;
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveAnnotations();
    }

    private void SaveAnnotations()
    {
        var data = new List<AnnotationData>();
        foreach (var marker in markersOverlay.Markers)
        {
            if (marker is GMarkerGoogle gm)
            {
                data.Add(new AnnotationData { Type = "Pin", Lat = marker.Position.Lat, Lng = marker.Position.Lng });
            }
            else if (marker is GMapCustomMarkerCircle cm)
            {
                data.Add(new AnnotationData { Type = "Circle", Lat = marker.Position.Lat, Lng = marker.Position.Lng, Color = cm.MarkerColor.Name, Text = cm.MarkerText });
            }
            else if (marker is GMapCustomMarkerText tm)
            {
                data.Add(new AnnotationData { Type = "Text", Lat = marker.Position.Lat, Lng = marker.Position.Lng, Text = tm.MarkerText });
            }
        }

        string json = JsonSerializer.Serialize(data);
        File.WriteAllText("annotations.json", json);
    }

    private void LoadAnnotations()
    {
        if (File.Exists("annotations.json"))
        {
            try
            {
                string json = File.ReadAllText("annotations.json");
                var data = JsonSerializer.Deserialize<List<AnnotationData>>(json);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        PointLatLng point = new PointLatLng(item.Lat, item.Lng);
                        switch (item.Type)
                        {
                            case "Pin":
                                markersOverlay.Markers.Add(new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin));
                                break;
                            case "Circle":
                                Color color = Color.FromName(item.Color ?? "Red");
                                markersOverlay.Markers.Add(new GMapCustomMarkerCircle(point, color, item.Text ?? ""));
                                break;
                            case "Text":
                                markersOverlay.Markers.Add(new GMapCustomMarkerText(point, item.Text ?? ""));
                                break;
                        }
                    }
                }
            }
            catch { }
        }
    }

    private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && currentMode != AnnotationMode.None)
        {
            PointLatLng point = gMapControl1.FromLocalToLatLng(e.X, e.Y);

            switch (currentMode)
            {
                case AnnotationMode.Pin:
                    AddPin(point);
                    break;
                case AnnotationMode.Circle:
                    AddCircle(point);
                    break;
                case AnnotationMode.Text:
                    AddText(point);
                    break;
            }

            currentMode = AnnotationMode.None;
            gMapControl1.Cursor = Cursors.Default;
        }
    }

    private void AddPin(PointLatLng point)
    {
        GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);
        markersOverlay.Markers.Add(marker);
    }

    private void AddCircle(PointLatLng point)
    {
        // Simple dialog to ask for color and text
        using (Form inputForm = new Form())
        {
            inputForm.Text = "Circle Annotation";
            inputForm.Size = new Size(300, 250);
            inputForm.StartPosition = FormStartPosition.CenterParent;

            Label lblColor = new Label() { Text = "Color (Red/Yellow):", Left = 10, Top = 20, Width = 150 };
            ComboBox comboColor = new ComboBox() { Left = 160, Top = 18, Width = 100 };
            comboColor.Items.AddRange(new string[] { "Red", "Yellow" });
            comboColor.SelectedIndex = 0;

            Label lblText = new Label() { Text = "Inner Text:", Left = 10, Top = 60, Width = 150 };
            TextBox txtInner = new TextBox() { Left = 160, Top = 58, Width = 100 };

            Button btnOk = new Button() { Text = "OK", Left = 10, Top = 120, Width = 100 };
            btnOk.Click += (s, ev) => { inputForm.DialogResult = DialogResult.OK; inputForm.Close(); };

            inputForm.Controls.AddRange(new Control[] { lblColor, comboColor, lblText, txtInner, btnOk });

            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                Color color = comboColor.Text == "Red" ? Color.Red : Color.Yellow;
                string text = txtInner.Text;

                // Draw circle using a polygon (approximation) or a custom marker
                // For simplicity, we use a custom marker that draws a circle and text
                GMapMarker circleMarker = new GMapCustomMarkerCircle(point, color, text);
                markersOverlay.Markers.Add(circleMarker);
            }
        }
    }

    private void AddText(PointLatLng point)
    {
        using (Form inputForm = new Form())
        {
            inputForm.Text = "Text Label";
            inputForm.Size = new Size(300, 150);
            inputForm.StartPosition = FormStartPosition.CenterParent;

            Label lblText = new Label() { Text = "Text:", Left = 10, Top = 20, Width = 50 };
            TextBox txtInner = new TextBox() { Left = 70, Top = 18, Width = 180 };

            Button btnOk = new Button() { Text = "OK", Left = 10, Top = 60, Width = 100 };
            btnOk.Click += (s, ev) => { inputForm.DialogResult = DialogResult.OK; inputForm.Close(); };

            inputForm.Controls.AddRange(new Control[] { lblText, txtInner, btnOk });

            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                GMapMarker textMarker = new GMapCustomMarkerText(point, txtInner.Text);
                markersOverlay.Markers.Add(textMarker);
            }
        }
    }
}

public class AnnotationData
{
    public string? Type { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
    public string? Color { get; set; }
    public string? Text { get; set; }
}

public class GMapCustomMarkerCircle : GMapMarker
{
    public Color MarkerColor { get; }
    public string MarkerText { get; }
    private int radius = 40;

    public GMapCustomMarkerCircle(PointLatLng p, Color c, string t) : base(p)
    {
        MarkerColor = c;
        MarkerText = t;
        Size = new Size(radius, radius);
        Offset = new Point(-radius / 2, -radius / 2);
    }

    public override void OnRender(Graphics g)
    {
        using (Pen pen = new Pen(MarkerColor, 2))
        {
            g.DrawEllipse(pen, LocalPosition.X, LocalPosition.Y, radius, radius);
        }
        if (!string.IsNullOrEmpty(MarkerText))
        {
            using (Font font = new Font("Arial", 8))
            using (Brush brush = new SolidBrush(MarkerColor))
            {
                SizeF size = g.MeasureString(MarkerText, font);
                g.DrawString(MarkerText, font, brush, LocalPosition.X + (radius - size.Width) / 2, LocalPosition.Y + (radius - size.Height) / 2);
            }
        }
    }
}

public class GMapCustomMarkerText : GMapMarker
{
    public string MarkerText { get; }

    public GMapCustomMarkerText(PointLatLng p, string t) : base(p)
    {
        MarkerText = t;
    }

    public override void OnRender(Graphics g)
    {
        if (!string.IsNullOrEmpty(MarkerText))
        {
            using (Font font = new Font("Arial", 10, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.DrawString(MarkerText, font, brush, LocalPosition.X, LocalPosition.Y);
            }
        }
    }
}
