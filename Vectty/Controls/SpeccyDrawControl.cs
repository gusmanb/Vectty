using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vectty.ZXClasses;
using System.IO;
using System.Drawing.Imaging;

namespace Vectty
{
    public partial class SpeccyDrawControl : UserControl
    {
        public event EventHandler HistoryChanged;
        public event EventHandler PolyToolChanged;
        public ZXAttribute ActiveAttribute { get; private set; } = new ZXAttribute();
        public SpeccyDrawControlTool Tool { get; set; }
        public SpeccyDrawControlMode Mode { get; set; }
        SpeccyDrawControlBGMode bgMode;
        public SpeccyDrawControlBGMode BackgroundMode { get { return bgMode; } set { bgMode = value; this.Invalidate(); } }
        public bool Grid { get; set; } = true;
        public bool CanUndo { get { return undo.Count > 0; } }
        public bool CanRedo { get { return redo.Count > 0; } }
        int scale = 2;
        public new int Scale { get { return scale; } set { scale = value; SpeccyDrawControl_Resize(this, EventArgs.Empty); } }
        public bool PolyTool { get; private set; }
        Image bg;
        Image bgCheckered;
        public new Image BackgroundImage
        {
            get { return bg; }
            set
            {
                if (value.Size.Width != 256 || value.Size.Height != 192)
                    throw new BadImageFormatException("Image must be 256x192 pixels.");

                if (bg != null)
                    bg.Dispose();

                bg = value.Clone() as Image;
                CreateCheckered();
                Invalidate();
            }
        }

        Bitmap pixels;
        ZXChar[,] chars = new ZXChar[32, 24];
        Point startPoint;

        Point arcA;
        Point arcB;
        Point arcC;

        Panel arcAMarker;
        Panel arcBMarker;

        byte arcPhase = 0;

        List<SCAction> actions = new List<SCAction>();

        List<SCState> undo = new List<SCState>();
        List<SCState> redo = new List<SCState>();

        bool toolEnabled = false;

        public SpeccyDrawControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();

            Cleanup();

            Tool = SpeccyDrawControlTool.Arc;
        }

        public bool Undo()
        {
            if (undo.Count > 0)
            {
                CreateRedo();

                var state = undo[0];
                undo.RemoveAt(0);
                actions.Clear();
                actions.AddRange(state.actions);
                pixels.Dispose();
                pixels = state.pixels.Clone() as Bitmap;
                drawBox.Image = pixels;
                for (int x = 0; x < 32; x++)
                    for (int y = 0; y < 24; y++)
                        chars[x, y] = state.chars[x, y].Clone();
                Invalidate();

                if (HistoryChanged != null)
                    HistoryChanged(this, EventArgs.Empty);

                return true;
            }

            return false;
        }
        public bool Redo()
        {
            if (redo.Count > 0)
            {
                CreateUndo(false);

                var state = redo[0];
                redo.RemoveAt(0);
                actions.Clear();
                actions.AddRange(state.actions);
                pixels.Dispose();
                pixels = state.pixels.Clone() as Bitmap;
                drawBox.Image = pixels;
                for (int x = 0; x < 32; x++)
                    for (int y = 0; y < 24; y++)
                        chars[x, y] = state.chars[x, y].Clone();
                Invalidate();

                if (HistoryChanged != null)
                    HistoryChanged(this, EventArgs.Empty);

                return true;
            }

            return false;
        }
        public void Cleanup()
        {
            for (int x = 0; x < 32; x++)
                for (int y = 0; y < 24; y++)
                    chars[x, y] = new ZXChar(x, y);

            if (pixels != null)
                pixels.Dispose();

            pixels = new Bitmap(256, 192, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(pixels);
            g.Clear(Color.Transparent);
            g.Dispose();
            drawBox.Image = pixels;

            foreach (var state in undo)
                state.Dispose();

            foreach (var state in redo)
                state.Dispose();

            undo.Clear();
            redo.Clear();

            actions.Clear();

            Invalidate();
        }
        public void FillPaper()
        {
            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 24; y++)
                {
                    chars[x, y].Attribute.Paper = ActiveAttribute.Paper;
                    chars[x, y].Attribute.Bright = ActiveAttribute.Bright;
                }
            }

            Invalidate();
        }
        public void FillInk()
        {
            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 24; y++)
                {
                    chars[x, y].Attribute.Ink = ActiveAttribute.Ink;
                    chars[x, y].Attribute.Bright = ActiveAttribute.Bright;
                    UpdatePixels(chars[x, y]);
                }
            }

            Invalidate();
        }
        public bool LoadFile(string FileName)
        {
            try
            {
                byte[] data = File.ReadAllBytes(FileName);
                if (Encoding.ASCII.GetString(data, 0, 6) != "VCT1.0")
                    throw new FormatException();

                int len = BitConverter.ToInt32(data, 6);

                byte[] tmpData = new byte[len];
                Buffer.BlockCopy(data, 10, tmpData, 0, len);
                MemoryStream ms = new MemoryStream(tmpData);
                ms.Position = 0;
                Bitmap bmp = (Bitmap)Bitmap.FromStream(ms);
                ms.Dispose();

                List<SCAction> actions = new List<SCAction>();

                int sectionStart = 10 + len;
                len = BitConverter.ToInt32(data, sectionStart);
                sectionStart += 4;

                for (int buc = 0; buc < len; buc++)
                {
                    actions.Add(new SCAction 
                    {
                        Tool = (SpeccyDrawControlTool)data[sectionStart],
                        StartPoint = new Point(data[sectionStart + 1], data[sectionStart + 2]),
                        EndPoint = new Point(data[sectionStart + 3], data[sectionStart + 4]),
                        Distance = BitConverter.ToUInt16(data, sectionStart + 5)
                    });

                    sectionStart += 7;
                }

                len = BitConverter.ToInt32(data, sectionStart);
                sectionStart += 4;
                tmpData = new byte[len];
                Buffer.BlockCopy(data, sectionStart, tmpData, 0, len);
                tmpData = RLDecode(tmpData);

                if(tmpData.Length != 768)
                    throw new FormatException();

                sectionStart = 0;

                for (int y = 0; y < 24; y++)
                    for (int x = 0; x < 32; x++)
                        chars[x, y].Attribute.LoadFromByte(tmpData[sectionStart++]);

                if(pixels != null)
                    pixels.Dispose();

                pixels = new Bitmap(256, 192, PixelFormat.Format32bppArgb);

                var g = Graphics.FromImage(pixels);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                g.DrawImage(bmp, Point.Empty);
                g.Dispose();
                bmp.Dispose();

                drawBox.Image = pixels;

                foreach (var state in undo)
                    state.Dispose();

                foreach (var state in redo)
                    state.Dispose();

                undo.Clear();
                redo.Clear();

                this.actions = actions;

                Invalidate();

                return true;
            }
            catch { return false; }
        }
        public bool SaveFile(string FileName)
        {
            MemoryStream ms = new MemoryStream();
            pixels.Save(ms, ImageFormat.Png);
            List<byte> fileBuffer = new List<byte>();
            fileBuffer.AddRange(Encoding.ASCII.GetBytes("VCT1.0"));
            byte[] data = ms.ToArray();
            fileBuffer.AddRange(BitConverter.GetBytes(data.Length));
            fileBuffer.AddRange(data);
            fileBuffer.AddRange(BitConverter.GetBytes(actions.Count));
            for (int buc = 0; buc < actions.Count; buc++)
            {
                SCAction act = actions[buc];
                fileBuffer.Add((byte)act.Tool);
                fileBuffer.Add((byte)act.StartPoint.X);
                fileBuffer.Add((byte)act.StartPoint.Y);
                fileBuffer.Add((byte)act.EndPoint.X);
                fileBuffer.Add((byte)act.EndPoint.Y);
                fileBuffer.AddRange(BitConverter.GetBytes(act.Distance));
            }

            List<byte> attribs = new List<byte>();

            for (int y = 0; y < 24; y++)
                for (int x = 0; x < 32; x++)
                    attribs.Add(chars[x, y].Attribute.ToByte());

            data = RLEncode(attribs.ToArray());

            fileBuffer.AddRange(BitConverter.GetBytes(data.Length));
            fileBuffer.AddRange(data);

            File.WriteAllBytes(FileName, fileBuffer.ToArray());

            return true;
        }
        public bool ExportFile(string FileName, SpeccyDrawExportMode Mode)
        {
            try
            {
                List<byte> cpActions = new List<byte>();

                SCAction lastAction = null;

                bool onPolygon = false;

                for (int buc = 0; buc < actions.Count; buc++)
                {
                    SCAction currentAction = actions[buc];

                    if (buc == 0 || lastAction.Tool != currentAction.Tool || (onPolygon && lastAction.EndPoint != currentAction.StartPoint))
                    {
                        byte action;

                        if (buc < actions.Count + 1 &&
                            currentAction.Tool == SpeccyDrawControlTool.Line &&
                            actions[buc + 1].Tool == SpeccyDrawControlTool.Line &&
                            currentAction.EndPoint == actions[buc + 1].StartPoint)
                        {
                            onPolygon = true;
                            action = 250;
                            cpActions.Add(action);
                            cpActions.Add((byte)(191 - currentAction.StartPoint.Y));
                            cpActions.Add((byte)currentAction.StartPoint.X);
                        }
                        else
                        {
                            onPolygon = false;
                            action = (byte)(200 + ((byte)currentAction.Tool));
                            cpActions.Add(action);
                        }

                    }

                    lastAction = currentAction;

                    //Y y X están invertidos, de esta forma si Y es superior a 200 indica que es una nueva operación
                    //si no se repite la operación anterior para ahorrar espacio

                    switch (currentAction.Tool)
                    {
                        case SpeccyDrawControlTool.Line:

                            if (!onPolygon)
                            {
                                cpActions.Add((byte)(191 - currentAction.StartPoint.Y));
                                cpActions.Add((byte)currentAction.StartPoint.X);
                            }

                            cpActions.Add((byte)(191 - currentAction.EndPoint.Y));
                            cpActions.Add((byte)currentAction.EndPoint.X);

                            break;

                        case SpeccyDrawControlTool.Rectangle:

                            cpActions.Add((byte)(191 - currentAction.StartPoint.Y));
                            cpActions.Add((byte)currentAction.StartPoint.X);
                            cpActions.Add((byte)(191 - currentAction.EndPoint.Y));
                            cpActions.Add((byte)currentAction.EndPoint.X);

                            break;

                        case SpeccyDrawControlTool.Arc:

                            cpActions.Add((byte)(191 - currentAction.StartPoint.Y));
                            cpActions.Add((byte)currentAction.StartPoint.X);
                            cpActions.Add((byte)(191 - currentAction.EndPoint.Y));
                            cpActions.Add((byte)currentAction.EndPoint.X);
                            cpActions.Add((byte)((currentAction.Distance >> 8) & 0xFF));
                            cpActions.Add((byte)(currentAction.Distance & 0xFF));

                            break;

                        case SpeccyDrawControlTool.Circle:

                            cpActions.Add((byte)(191 - currentAction.StartPoint.Y));
                            cpActions.Add((byte)currentAction.StartPoint.X);
                            cpActions.Add((byte)currentAction.StartPoint.Distance(currentAction.EndPoint));

                            break;

                        case SpeccyDrawControlTool.Fill:

                            cpActions.Add((byte)(currentAction.StartPoint.Y));
                            cpActions.Add((byte)currentAction.StartPoint.X);

                            break;

                        case SpeccyDrawControlTool.BlockEraser:

                            cpActions.Add((byte)(currentAction.StartPoint.Y));
                            cpActions.Add((byte)currentAction.StartPoint.X);

                            break;
                    }
                }

                List<byte> attribs = new List<byte>();

                for (int y = 0; y < 24; y++)
                    for (int x = 0; x < 32; x++)
                        attribs.Add(chars[x, y].Attribute.ToByte());

                var encodedAttribs = RLEncode(attribs.ToArray());

                List<byte> finalBuffer = new List<byte>();
                
                finalBuffer.AddRange(cpActions);
                finalBuffer.Add(0xFF);
                finalBuffer.AddRange(encodedAttribs);

                StreamWriter output = File.CreateText(FileName);

                switch (Mode)
                {
                    case SpeccyDrawExportMode.SinclairBasic:

                        int line = 8999;
                        for (int buc = 0; buc < finalBuffer.Count; buc++)
                        {
                            if (buc % 32 == 0)
                            {
                                line++;
                                output.WriteLine();
                                output.Write($"{line} DATA {finalBuffer[buc]}");
                            }
                            else
                                output.Write($", {finalBuffer[buc]}");
                        }

                        break;

                    case SpeccyDrawExportMode.BorielBasic:

                        output.Write($"Dim vectImg({finalBuffer.Count - 1}) as uByte => {{ _\r\n    {finalBuffer[0]}");

                        for (int buc = 1; buc < finalBuffer.Count; buc++)
                        {
                            if (buc % 32 == 0)
                                output.Write($", _\r\n    {finalBuffer[buc]}");
                            else
                                output.Write($", {finalBuffer[buc]}");
                        }
                        output.Write(" _\r\n }");
                        break;

                    case SpeccyDrawExportMode.Assembler:

                        output.WriteLine("vectImg:");

                        for (int buc = 0; buc < finalBuffer.Count; buc++)
                            output.WriteLine($"    DEFB    {finalBuffer[buc]}");

                        break;
                }

                output.Close();

                return true;
            }
            catch { return false; }
        }

        private void CreateCheckered()
        {

            if (bgCheckered != null)
                bgCheckered.Dispose();

            Bitmap bmpCheckered = new Bitmap(bg.Width, bg.Height, bg.PixelFormat);
            Bitmap bmpbg = bg as Bitmap;

            Graphics g = Graphics.FromImage(bmpCheckered);
            g.Clear(Color.Transparent);
            g.Dispose();

            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 192; y ++)
                {
                    if((x + y) % 2 == 0)
                        bmpCheckered.SetPixel(x, y, bmpbg.GetPixel(x, y));
                }
            }

            bgCheckered = bmpCheckered;
        }

        private void CreateUndo(bool ForHistory = true)
        {
            SCState history = new SCState(pixels, chars, actions);
            undo.Insert(0, history);

            if (ForHistory)
            {
                if (undo.Count > 32)
                {
                    var itm = undo[undo.Count - 1];
                    itm.Dispose();
                    undo.RemoveAt(undo.Count - 1);
                }

                foreach (var itm in redo)
                    itm.Dispose();

                redo.Clear();

                if (HistoryChanged != null)
                    HistoryChanged(this, EventArgs.Empty);
            }
        }
        private void CreateRedo()
        {
            SCState history = new SCState(pixels, chars, actions);
            redo.Insert(0, history);
        }
        private void SpeccyDrawControl_Resize(object sender, EventArgs e)
        {
            if(this.Width != 256 * scale || this.Height != 192 * scale)
                this.Size = new Size(256 * scale, 192 * scale);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 24; y++)
                {
                    Brush b = new SolidBrush(chars[x, y].Attribute.RGBPaper);
                    e.Graphics.FillRectangle(b, chars[x, y].DoubleArea(scale));
                    b.Dispose();

                    if (bgMode == SpeccyDrawControlBGMode.Over && bg != null)
                        e.Graphics.DrawImage(bg, chars[x, y].DoubleArea(scale), chars[x, y].Area, GraphicsUnit.Pixel);
                    else if(bgMode == SpeccyDrawControlBGMode.Interlaced && bgCheckered != null)
                        e.Graphics.DrawImage(bgCheckered, chars[x, y].DoubleArea(scale), chars[x, y].Area, GraphicsUnit.Pixel);

                    if (Grid)
                        e.Graphics.DrawRectangle(Pens.Black, chars[x, y].GridArea(scale));
                }

            }

        }

        private bool DrawCircle(int centerX, int centerY, int radius, Bitmap bmp, bool UpdateAttributes = true)
        {
            int d = (5 - radius * 4) / 4;
            int x = 0;
            int y = radius;

            List<Point> finalPoints = new List<Point>();

            do
            {
                // ensure index is in range before setting (depends on your image implementation)
                // in this case we check if the pixel location is within the bounds of the image before setting the pixel
                if (centerX + x >= 0 && centerX + x <= 256 - 1 && centerY + y >= 0 && centerY + y <= 192 - 1)
                    finalPoints.Add(new Point(centerX + x, centerY + y));
                else
                    return false;

                if (centerX + x >= 0 && centerX + x <= 256 - 1 && centerY - y >= 0 && centerY - y <= 192 - 1) 
                    finalPoints.Add(new Point(centerX + x, centerY - y));
                else
                    return false;
                if (centerX - x >= 0 && centerX - x <= 256 - 1 && centerY + y >= 0 && centerY + y <= 192 - 1) 
                    finalPoints.Add(new Point(centerX - x, centerY + y));
                else
                    return false;
                if (centerX - x >= 0 && centerX - x <= 256 - 1 && centerY - y >= 0 && centerY - y <= 192 - 1) 
                    finalPoints.Add(new Point(centerX - x, centerY - y));
                else
                    return false;
                if (centerX + y >= 0 && centerX + y <= 256 - 1 && centerY + x >= 0 && centerY + x <= 192 - 1) 
                    finalPoints.Add(new Point(centerX + y, centerY + x));
                else
                    return false;
                if (centerX + y >= 0 && centerX + y <= 256 - 1 && centerY - x >= 0 && centerY - x <= 192 - 1) 
                    finalPoints.Add(new Point(centerX + y, centerY - x));
                else
                    return false;
                if (centerX - y >= 0 && centerX - y <= 256 - 1 && centerY + x >= 0 && centerY + x <= 192 - 1) 
                    finalPoints.Add(new Point(centerX - y, centerY + x));
                else
                    return false;
                if (centerX - y >= 0 && centerX - y <= 256 - 1 && centerY - x >= 0 && centerY - x <= 192 - 1) 
                    finalPoints.Add(new Point(centerX - y, centerY - x));
                else
                    return false;

                if (d < 0)
                {
                    d += 2 * x + 1;
                }
                else
                {
                    d += 2 * (x - y) + 1;
                    y--;
                }

                x++;

            } while (x <= y);

            foreach (var point in finalPoints)
                bmp.SetPixel(point.X, point.Y, GetInk(point.X, point.Y, UpdateAttributes));

            return true;
        }
        private bool DrawLine(int x0, int y0, int x1, int y1, Bitmap bmp, bool UpdateAttributes = true)
        {
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;

            List<Point> finalPoints = new List<Point>();

            for (; ; )
            {
                if (x0 >= 0 && x0 < bmp.Width && y0 >= 0 && y0 < bmp.Height)
                    finalPoints.Add(new Point(x0, y0));
                else
                    return false;

                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }

            foreach (var point in finalPoints)
                bmp.SetPixel(point.X, point.Y, GetInk(point.X, point.Y, UpdateAttributes));

            return true;
        }
        private bool DrawRect(int rx0, int ry0, int rx1, int ry1, Bitmap bmp, bool UpdateAttributes = true)
        {
            List<Point> finalPoints = new List<Point>();

            for (int buc = 0; buc < 4; buc++)
            {
                int x0 = 0;
                int y0 = 0;
                int x1 = 0;
                int y1 = 0;

                switch (buc)
                {
                    case 0:

                        x0 = rx0;
                        y0 = ry0;
                        x1 = rx1;
                        y1 = ry0;

                        break;

                    case 1:

                        x0 = rx1;
                        y0 = ry0;
                        x1 = rx1;
                        y1 = ry1;

                        break;

                    case 2:

                        x0 = rx1;
                        y0 = ry1;
                        x1 = rx0;
                        y1 = ry1;

                        break;

                    case 3:

                        x0 = rx0;
                        y0 = ry1;
                        x1 = rx0;
                        y1 = ry0;

                        break;
                }

                int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
                int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
                int err = (dx > dy ? dx : -dy) / 2, e2;

                for (; ; )
                {
                    if (x0 >= 0 && x0 < bmp.Width && y0 >= 0 && y0 < bmp.Height)
                        finalPoints.Add(new Point(x0, y0));
                    else
                        return false;

                    if (x0 == x1 && y0 == y1) break;
                    e2 = err;
                    if (e2 > -dx) { err -= dy; x0 += sx; }
                    if (e2 < dy) { err += dx; y0 += sy; }
                }
            }

            foreach (var point in finalPoints)
                bmp.SetPixel(point.X, point.Y, GetInk(point.X, point.Y, UpdateAttributes));

            return true;
        }
        private bool DrawArc(int x0, int y0, int x1, int y1, double angle, Bitmap bmp, bool UpdateAttributes = true)
        {
            if (Math.Abs(angle) >= Math.PI * 2)
                return false;

            int i, xx, yy;
            double len, dx, dy, mx, my, px, py, t, cx, cy, p0x, p0y, an;

            mx = (x0 + x1) / 2;
            my = (y0 + y1) / 2;

            dx = (x1 - x0) / 2;
            dy = (y1 - y0) / 2;

            len = System.Math.Sqrt(dx * dx + dy * dy);

            px = -dy / len;
            py = dx / len;

            if (angle == Math.PI)
                t = 0;
            else
                t = len / Math.Tan(angle / 2);

            cx = mx + px * t;
            cy = my + py * t;

            p0x = x0 - cx;
            p0y = y0 - cy;

            int steps = Math.Max((int)(Math.Abs(angle) * 1024), 360);

            int lastXX = -1;
            int lastYY = -1;

            List<Point> finalPoints = new List<Point>();

            for (i = 0; i < steps + 1; i++)
            {
                an = i * angle / (steps + 1);
                xx = (int)(Math.Round(cx + p0x * Math.Cos(an) - p0y * Math.Sin(an)));
                yy = (int)(Math.Round(cy + p0x * Math.Sin(an) + p0y * Math.Cos(an)));

                if (xx >= 0 && xx < bmp.Width && yy >= 0 && yy < bmp.Height)
                {
                    if (xx != lastXX || yy != lastYY)
                    {
                        finalPoints.Add(new Point(xx, yy));
                        lastXX = xx;
                        lastYY = yy;
                    }
                }
                else
                    return false;
            }

            foreach (var point in finalPoints)
                bmp.SetPixel(point.X, point.Y, GetInk(point.X, point.Y, UpdateAttributes));

            return true;
        }
        private void FloodFill(int X, int Y)
        {
            var tAlpha = pixels.GetPixel(X, Y).A;

            if (tAlpha != 0)
                return;

            Queue<Point> toVisit = new Queue<Point>();

            toVisit.Enqueue(new Point(X, Y));

            while (toVisit.Count > 0)
            {
                Point current = toVisit.Dequeue();

                if (current.X < 0 || current.Y < 0 || current.X >= pixels.Width || current.Y >= pixels.Height)
                    continue;

                if(tAlpha == pixels.GetPixel(current.X, current.Y).A)
                {
                    pixels.SetPixel(current.X, current.Y, GetInk(current.X, current.Y, true));
                    Point next = new Point(current.X, current.Y + 1);
                    toVisit.Enqueue(next);
                    next = new Point(current.X, current.Y - 1);
                    toVisit.Enqueue(next);
                    next = new Point(current.X + 1, current.Y);
                    toVisit.Enqueue(next);
                    next = new Point(current.X - 1, current.Y);
                    toVisit.Enqueue(next);
                }

            }
        }      
        
        private ZXChar GetCharAt(int X, int Y)
        {
            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 24; y++)
                {
                    if (chars[x, y].Area.Contains(X, Y))
                        return chars[x, y];
                }
            }

            return null;
        }
        private Color GetPaper(int X, int Y)
        {
            return GetCharAt(X, Y).Attribute?.RGBPaper ?? Color.Black;

        }
        private Color GetInk(int X, int Y, bool UpdateAttributes)
        {
            var chr = GetCharAt(X, Y);

            if (chr == null)
                return Color.Black;

            if(UpdateAttributes)
                CheckAttribute(chr);

            return chr.Attribute.RGBInk;
            
        }
        
        private void drawBox_MouseDown(object sender, MouseEventArgs e)
        {

            //Console.WriteLine("DOWN");

            if (arcPhase == 0)
                CreateUndo();

            toolEnabled = true;

            startPoint = new Point(e.X / scale, e.Y / scale);

            switch (Tool)
            {
                case SpeccyDrawControlTool.Arc:

                    if (arcPhase == 0)
                    {
                        var pt = e.Location;
                        pt.Offset(-1, -1);

                        arcAMarker = new Panel { Width = 3, Height = 3, Location = pt, BackColor = Color.BlueViolet };
                        this.Controls.Add(arcAMarker);
                        arcAMarker.BringToFront();
                    }
                    else if (arcPhase == 1)
                    {
                        var pt = e.Location;
                        pt.Offset(-1, -1);

                        arcBMarker = new Panel { Width = 3, Height = 3, Location = pt, BackColor = Color.BlueViolet };
                        this.Controls.Add(arcBMarker);
                        arcBMarker.BringToFront();
                    }
                    else if (arcPhase == 2)
                    {
                        arcC = e.Location;
                        this.Controls.Remove(arcAMarker);
                        this.Controls.Remove(arcBMarker);
                        arcAMarker.Dispose();
                        arcBMarker.Dispose();
                        arcAMarker = null;
                        arcBMarker = null;
                    }

                    break;

                case SpeccyDrawControlTool.Fill:

                    FloodFill(startPoint.X, startPoint.Y);
                    actions.Add(new SCAction { Tool = SpeccyDrawControlTool.Fill, StartPoint = startPoint });
                    Invalidate();
                    break;

                case SpeccyDrawControlTool.Brush:

                    if (Mode == SpeccyDrawControlMode.Bitmap)
                        return;

                    CheckAttribute(GetCharAt(startPoint.X, startPoint.Y));
                    break;

                case SpeccyDrawControlTool.BlockEraser:

                    var chrD = GetCharAt(startPoint.X, startPoint.Y);

                    if (DeletePixels(chrD))
                    {
                        actions.Add(new SCAction { Tool = SpeccyDrawControlTool.BlockEraser, StartPoint = new Point(chrD.X, chrD.Y) });
                        Invalidate();
                    }

                    break;

                case SpeccyDrawControlTool.Line:

                    if (actions.Count > 0 && actions.Last().EndPoint == startPoint)
                    {
                        PolyTool = true;

                        if (PolyToolChanged != null)
                            PolyToolChanged(this, EventArgs.Empty);
                    }

                    break;

            }
        }
        private void drawBox_MouseUp(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("UP");
            if (!toolEnabled)
                return;

            toolEnabled = false;

            tempBox.Visible = false;

            var endPoint = new Point(e.X / scale, e.Y / scale);
            SCAction action;

            switch (Tool)
            {
                case SpeccyDrawControlTool.Line:

                    if (DrawLine(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y, pixels))
                    {
                        action = new SCAction { EndPoint = endPoint, StartPoint = startPoint, Tool = SpeccyDrawControlTool.Line };
                        actions.Add(action);
                        Invalidate();
                    }

                    if (PolyTool)
                    {
                        PolyTool = false;

                        if (PolyToolChanged != null)
                            PolyToolChanged(this, EventArgs.Empty);
                    }

                    break;

                case SpeccyDrawControlTool.Rectangle:

                    if (DrawRect(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y, pixels))
                    {
                        action = new SCAction { EndPoint = endPoint, StartPoint = startPoint, Tool = SpeccyDrawControlTool.Rectangle };
                        actions.Add(action);
                        Invalidate();
                    }

                    break;

                case SpeccyDrawControlTool.Circle:

                    int distance = (int)startPoint.Distance(endPoint);

                    if (distance > 255)
                        return;

                    if (DrawCircle(startPoint.X, startPoint.Y, (int)startPoint.Distance(endPoint), pixels))
                    {
                        SCAction dAction = new SCAction { EndPoint = endPoint, StartPoint = startPoint, Tool = SpeccyDrawControlTool.Circle };
                        actions.Add(dAction);
                        Invalidate();
                    }

                    break;

                case SpeccyDrawControlTool.Arc:

                    if (arcPhase == 0)
                    {
                        arcA = endPoint;
                        arcPhase++;
                    }
                    else if (arcPhase == 1)
                    {
                        arcB = endPoint;
                        arcPhase++;
                    }
                    else
                    {
                        var dist = Math.Round(arcC.Distance(e.Location), 1);
                        var sign = Math.Sign((e.Location.X - arcC.X) + (e.Location.Y - arcC.Y));

                        if (DrawArc(arcA.X, arcA.Y, arcB.X, arcB.Y, ToRad(dist) * sign, pixels))
                        {
                            ushort nDist = (ushort)(dist * 10);

                            if (sign == -1)
                                nDist = (ushort)(nDist + 32768);

                            SCAction dAction = new SCAction { EndPoint = arcB, StartPoint = arcA, Distance = nDist, Tool = SpeccyDrawControlTool.Arc };
                            actions.Add(dAction);
                            Invalidate();

                        }

                        arcPhase = 0;
                    }

                    break;

            }
        }
        private void drawBox_MouseMove(object sender, MouseEventArgs e)
        {

            //Console.WriteLine("MOVE");

            if (!toolEnabled)
                return;

            if (e.Button != MouseButtons.Left)
                return;

            Point midPoint = new Point(e.X / scale, e.Y / scale);

            switch (Tool)
            {
                case SpeccyDrawControlTool.Brush:

                    if (Mode == SpeccyDrawControlMode.Bitmap)
                        return;

                    CheckAttribute(GetCharAt(midPoint.X, midPoint.Y));

                    break;

                case SpeccyDrawControlTool.BlockEraser:

                    var chrD = GetCharAt(midPoint.X, midPoint.Y);

                    if (DeletePixels(chrD))
                    {
                        actions.Add(new SCAction { Tool = SpeccyDrawControlTool.BlockEraser, StartPoint = new Point(chrD.X, chrD.Y) });
                        Invalidate();
                    }

                    break;

                case SpeccyDrawControlTool.Line:

                    tempBox.Visible = true;

                    if (tempBox.Image != null)
                        tempBox.Image.Dispose();
                    
                    tempBox.Image = (Image)drawBox.Image.Clone();

                    DrawLine(startPoint.X, startPoint.Y, midPoint.X, midPoint.Y, tempBox.Image as Bitmap, false);

                    tempBox.Invalidate();

                    break;

                case SpeccyDrawControlTool.Circle:

                    tempBox.Visible = true;

                    if (tempBox.Image != null)
                        tempBox.Image.Dispose();

                    tempBox.Image = (Image)drawBox.Image.Clone();

                    DrawCircle(startPoint.X, startPoint.Y, (int)startPoint.Distance(midPoint), tempBox.Image as Bitmap, false);

                    tempBox.Invalidate();

                    break;

                case SpeccyDrawControlTool.Rectangle:

                    tempBox.Visible = true;

                    if (tempBox.Image != null)
                        tempBox.Image.Dispose();

                    tempBox.Image = (Image)drawBox.Image.Clone();

                    DrawRect(startPoint.X, startPoint.Y, midPoint.X, midPoint.Y, tempBox.Image as Bitmap, false);

                    tempBox.Invalidate();

                    break;

                case SpeccyDrawControlTool.Arc:
                    
                    if(arcPhase == 0)
                    {
                        var pt = e.Location;
                        pt.Offset(-1, -1);

                        arcAMarker.Location = pt;
                    }
                    else if (arcPhase == 1)
                    {
                        var pt = e.Location;
                        pt.Offset(-1, -1);

                        arcBMarker.Location = pt;
                    }
                    else if (arcPhase == 2)
                    {
                        tempBox.Visible = true;

                        if (tempBox.Image != null)
                            tempBox.Image.Dispose();

                        tempBox.Image = (Image)drawBox.Image.Clone();

                        var dist = Math.Round(arcC.Distance(e.Location), 1);
                        var sign = Math.Sign((e.Location.X - arcC.X) + (e.Location.Y - arcC.Y));

                        DrawArc(arcA.X, arcA.Y, arcB.X, arcB.Y, ToRad(dist) * sign, tempBox.Image as Bitmap);

                        tempBox.Invalidate();

                        break;
                    }

                    break;
            }

        }

        private void CheckAttribute(ZXChar Chr)
        {
            if (Chr == null)
                return;

            bool updatePixels = false;

            if (Mode == SpeccyDrawControlMode.Ink || Mode == SpeccyDrawControlMode.InkPaper)
            {
                if (Chr.Attribute.Ink != ActiveAttribute.Ink || Chr.Attribute.Bright != ActiveAttribute.Bright)
                {
                    Chr.Attribute.Ink = ActiveAttribute.Ink;
                    Chr.Attribute.Bright = ActiveAttribute.Bright;
                    updatePixels = true;                    
                }
            }

            if (Mode == SpeccyDrawControlMode.Paper || Mode == SpeccyDrawControlMode.InkPaper)
            {
                if (Chr.Attribute.Paper != ActiveAttribute.Paper || Chr.Attribute.Bright != ActiveAttribute.Bright)
                {

                    if (Chr.Attribute.Bright != ActiveAttribute.Bright)
                        updatePixels = true;

                    Chr.Attribute.Paper = ActiveAttribute.Paper;
                    Chr.Attribute.Bright = ActiveAttribute.Bright;
                }
            }

            if(updatePixels)
                UpdatePixels(Chr);

            Invalidate();
        }
        private bool DeletePixels(ZXChar chrD)
        {
            if (chrD == null)
                return false;

            bool didClear = false;

            for (int y = chrD.Area.Y; y < chrD.Area.Bottom; y++)
            {
                for (int x = chrD.Area.X; x < chrD.Area.Right; x++)
                {
                    if (pixels.GetPixel(x, y).A != 0)
                    {
                        pixels.SetPixel(x, y, Color.Transparent);
                        didClear = true;
                    }
                }
            }

            return didClear;
        }
        private void UpdatePixels(ZXChar chr)
        {
            for (int y = chr.Area.Y; y < chr.Area.Bottom; y++)
            {
                for (int x = chr.Area.X; x < chr.Area.Right; x++)
                {
                    if (pixels.GetPixel(x, y).A != 0)
                        pixels.SetPixel(x, y, chr.Attribute.RGBInk);
                }
            }
        }
        
        private byte[] RLEncode(byte[] Input)
        {
            List<byte> outBuffer = new List<byte>();

            for (int buc = 0; buc < Input.Length; buc++)
            {
                byte current = Input[buc];

                if (buc + 1 < Input.Length)
                {
                    byte next = Input[buc + 1];

                    if (next == current)
                    {
                        int runLen = 0;
                        int pos = buc + 2;

                        while (pos < Input.Length && next == current && runLen < 255)
                        {
                            next = Input[pos];
                            runLen++;
                            pos++;
                        }

                        outBuffer.Add(current);
                        outBuffer.Add(current);

                        if (next == current)
                        {
                            buc = pos - 1;
                            outBuffer.Add((byte)runLen);
                        }
                        else
                        {
                            buc = pos - 2;
                            outBuffer.Add((byte)(runLen - 1));
                        }

                    }
                    else
                        outBuffer.Add(current);
                }
                else
                    outBuffer.Add(current);
            }

            return outBuffer.ToArray();
        }
        private byte[] RLDecode(byte[] Input)
        {
            int posE = 0;
            List<byte> reconstr = new List<byte>();

            byte currentE;
            byte nextE;

            while (posE < Input.Length)
            {
                currentE = Input[posE];

                if (posE + 1 < Input.Length)
                {
                    nextE = Input[posE + 1];
                    if (nextE == currentE)
                    {
                        int runlen = Input[posE + 2] + 2;

                        while (runlen > 0)
                        {
                            reconstr.Add(currentE);
                            runlen -= 1;
                        }

                        posE += 3;
                    }
                    else
                    {
                        reconstr.Add(currentE);
                        posE++;
                    }
                }
                else
                {
                    reconstr.Add(currentE);
                    posE++;
                }
            }

            return reconstr.ToArray();
        }
        
        private double ToRad(double Deg)
        {
            return (Math.PI / 180.0) * Deg;
        }

        class SCAction
        {
            public SpeccyDrawControlTool Tool { get; set; }
            public Point StartPoint { get; set; }
            public Point EndPoint { get; set; }
            public ushort Distance { get; set; }
        }

        class SCState : IDisposable
        {

            public Bitmap pixels;
            public ZXChar[,] chars = new ZXChar[32,24];
            public List<SCAction> actions = new List<SCAction>();

            public SCState(Bitmap Pixels, ZXChar[,] Chars, List<SCAction> Actions)
            {
                pixels = (Bitmap)Pixels.Clone();
                actions.AddRange(Actions);
                for (int x = 0; x < 32; x++)
                    for (int y = 0; y < 24; y++)
                        chars[x, y] = Chars[x, y].Clone();
            }

            public void Dispose()
            {
                pixels.Dispose();
                chars = null;
                actions.Clear();
            }

        }

        private void tempBox_MouseDown(object sender, MouseEventArgs e)
        {
            tempBox.Visible = false;
        }
    }

    public enum SpeccyDrawControlTool
    {
        Line,
        Rectangle,
        Circle,
        Arc,
        Fill,
        BlockEraser,
        Brush
    }

    public enum SpeccyDrawControlMode
    {
        Bitmap,
        Ink,
        Paper,
        InkPaper
    }

    public enum SpeccyDrawControlBGMode
    {
        Disabled,
        Interlaced,
        Over
    }

    public enum SpeccyDrawExportMode
    {
        SinclairBasic,
        BorielBasic,
        Assembler
    }
}
