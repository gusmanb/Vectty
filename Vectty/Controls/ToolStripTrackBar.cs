using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Vectty.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ContextMenuStrip | 
        ToolStripItemDesignerAvailability.MenuStrip | 
        ToolStripItemDesignerAvailability.ToolStrip | 
        ToolStripItemDesignerAvailability.StatusStrip)]
    public class ToolStripTrackBar : ToolStripControlHost
    {
        public ToolStripTrackBar()
            : base(CreateControlInstance())
        {
        }
        /// <summary>
        /// Create a strongly typed property called TrackBar - handy to prevent casting everywhere.
        /// </summary>
        public TrackBar TrackBar
        {
            get
            {
                return Control as TrackBar;
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(0)]
        public int MinValue 
        { 
            get 
            {
                return TrackBar.Minimum;
            } 
            
            set 
            {
                TrackBar.Minimum = value;
            } 
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(10)]
        public int MaxValue
        {
            get
            {
                return TrackBar.Maximum;
            }

            set
            {
                TrackBar.Maximum = value;
            }
        }

        /// <summary>
        /// Create the actual control, note this is static so it can be called from the
        /// constructor.
        ///
        /// </summary>
        /// <returns></returns>
        private static Control CreateControlInstance()
        {
            TrackBar t = new TrackBar();
            t.AutoSize = false;
            t.Height = 16;
            // Add other initialization code here.
            return t;
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(10)]
        public int Value
        {
            get { return TrackBar.Value; }
            set { TrackBar.Value = value; }
        }
        /// <summary>
        /// Attach to events we want to re-wrap
        /// </summary>
        /// <param name="control"></param>
        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            TrackBar trackBar = control as TrackBar;
            trackBar.ValueChanged += new EventHandler(trackBar_ValueChanged);
        }

        protected override void OnHostedControlResize(EventArgs e)
        {
            base.OnHostedControlResize(e);
        }


        public override Size GetPreferredSize(Size constrainingSize)
        {
            return base.GetPreferredSize(constrainingSize);
        }

        protected override void OnDisplayStyleChanged(EventArgs e)
        {
            base.OnDisplayStyleChanged(e);
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEvent)
        {
            base.OnGiveFeedback(giveFeedbackEvent);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            if(e.AffectedControl != null)
                Console.WriteLine(e.AffectedControl.Name);

            if (e.AffectedProperty == "Orientation")
            {
                TrackBar.Orientation = this.Parent.Orientation;
            }
        }

        /// <summary>
        /// Detach from events.
        /// </summary>
        /// <param name="control"></param>
        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            TrackBar trackBar = control as TrackBar;
            trackBar.ValueChanged -= new EventHandler(trackBar_ValueChanged);
        }
        /// <summary>
        /// Routing for event
        /// TrackBar.ValueChanged -> ToolStripTrackBar.ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void trackBar_ValueChanged(object sender, EventArgs e)
        {
            // when the trackbar value changes, fire an event.
            if (this.ValueChanged != null)
            {
                ValueChanged(sender, e);
            }
        }
        // add an event that is subscribable from the designer.
        public event EventHandler ValueChanged;
        // set other defaults that are interesting
        protected override Size DefaultSize
        {
            get
            {
                return new Size(200, 16);
            }
        }

        
    }
}
