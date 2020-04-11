using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vectty
{
    public partial class ActionList : Form
    {
        public event EventHandler<ActionEventArgs> CopyActions;
        public event EventHandler PasteActions;
        public event EventHandler<ActionEventArgs> ShiftUpActions;
        public event EventHandler<ActionEventArgs> ShiftDownActions;
        public event EventHandler<ActionEventArgs> DeleteActions;

        public event EventHandler<ActionEventArgs> HorizontalMirrorActions;
        public event EventHandler<ActionEventArgs> VerticalMirrorActions;
        public event EventHandler<ActionEventArgs> AbsoulteHorizontalMirrorActions;
        public event EventHandler<ActionEventArgs> AbsoulteVerticalMirrorActions;

        public ActionList()
        {
            InitializeComponent();
        }

        public void UpdateList(IEnumerable<SCAction> Actions)
        {
            lstActions.BeginUpdate();
            lstActions.Items.Clear();
            ListViewItem lItem = null;

            foreach (var item in Actions)
            {
                lItem = new ListViewItem(item.Tool.ToString(), (int)item.Tool);
                lItem.Tag = item;
                lItem.EnsureVisible();
                lstActions.Items.Add(lItem);
            }

            if (lItem != null)
            {
                lItem.EnsureVisible();
                lItem.Selected = true;
            }

            lstActions.EndUpdate();
        }

        private void lstActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAction.Text = "---";
            lblSX.Text = "---";
            lblSY.Text = "---";
            lblEX.Text = "---";
            lblEY.Text = "---";
            lblDegRad.Text = "---";

            if (lstActions.SelectedItems.Count == 1)
            {
                var act = lstActions.SelectedItems[0].Tag as SCAction;
                lblAction.Text = act.Tool.ToString();
                lblSX.Text = act.StartPoint.X.ToString();
                lblSY.Text = act.StartPoint.Y.ToString();

                if (act.Tool == SpeccyDrawControlTool.Line ||
                    act.Tool == SpeccyDrawControlTool.Rectangle ||
                    act.Tool == SpeccyDrawControlTool.Arc)
                {
                    lblEX.Text = act.EndPoint.X.ToString();
                    lblEY.Text = act.EndPoint.Y.ToString();
                }

                if (act.Tool == SpeccyDrawControlTool.Arc)
                {

                    float val = act.Distance;
                    if (val > 32768)
                    {
                        val -= 32768;
                        val *= -1;
                    }

                    val /= 10.0f;

                    lblDegRad.Text = Math.Round(val, 1).ToString();

                }

                if (act.Tool == SpeccyDrawControlTool.Circle)
                    lblDegRad.Text = ((int)act.StartPoint.Distance(act.EndPoint)).ToString();

            }
            else if (lstActions.SelectedItems.Count > 1)
            {
                var query = from item in lstActions.SelectedItems.Cast<ListViewItem>()
                            let idx = lstActions.Items.IndexOf(item)
                            orderby idx
                            select idx;

                int first = query.First();
                int last = query.Last();

                for (int buc = first; buc <= last; buc++)
                    lstActions.Items[buc].Selected = true;
            }

        }

        private void btCopy_Click(object sender, EventArgs e)
        {
            if (CopyActions != null && lstActions.SelectedItems.Count > 0)
            {
                var query = from item in lstActions.SelectedItems.Cast<ListViewItem>()
                            let idx = lstActions.Items.IndexOf(item)
                            orderby idx
                            select idx;

                int first = query.First();

                CopyActions(this, new ActionEventArgs { Index = first, Count = lstActions.SelectedItems.Count });
            }

        }

        private void btPaste_Click(object sender, EventArgs e)
        {
            if (PasteActions != null)
                PasteActions(this, EventArgs.Empty);
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (DeleteActions != null && lstActions.SelectedItems.Count > 0)
            {
                var query = from item in lstActions.SelectedItems.Cast<ListViewItem>()
                            let idx = lstActions.Items.IndexOf(item)
                            orderby idx
                            select idx;

                int first = query.First();

                DeleteActions(this, new ActionEventArgs { Index = first, Count = lstActions.SelectedItems.Count });
            }
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            if (ShiftUpActions != null && lstActions.SelectedItems.Count > 0)
            {
                var query = from item in lstActions.SelectedItems.Cast<ListViewItem>()
                            let idx = lstActions.Items.IndexOf(item)
                            orderby idx
                            select idx;

                int first = query.First();

                ShiftUpActions(this, new ActionEventArgs { Index = first, Count = lstActions.SelectedItems.Count });
            }
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            if (ShiftDownActions != null && lstActions.SelectedItems.Count > 0)
            {
                var query = from item in lstActions.SelectedItems.Cast<ListViewItem>()
                            let idx = lstActions.Items.IndexOf(item)
                            orderby idx
                            select idx;

                int first = query.First();

                ShiftDownActions(this, new ActionEventArgs { Index = first, Count = lstActions.SelectedItems.Count });
            }
        }

        private void btHMirror_Click(object sender, EventArgs e)
        {
            if (HorizontalMirrorActions != null && lstActions.SelectedItems.Count > 0)
            {
                var query = from item in lstActions.SelectedItems.Cast<ListViewItem>()
                            let idx = lstActions.Items.IndexOf(item)
                            orderby idx
                            select idx;

                int first = query.First();

                HorizontalMirrorActions(this, new ActionEventArgs { Index = first, Count = lstActions.SelectedItems.Count });
            }
        }

        private void btVMirror_Click(object sender, EventArgs e)
        {
            if (VerticalMirrorActions != null && lstActions.SelectedItems.Count > 0)
            {
                var query = from item in lstActions.SelectedItems.Cast<ListViewItem>()
                            let idx = lstActions.Items.IndexOf(item)
                            orderby idx
                            select idx;

                int first = query.First();

                VerticalMirrorActions(this, new ActionEventArgs { Index = first, Count = lstActions.SelectedItems.Count });
            }
        }

        private void btAHMirror_Click(object sender, EventArgs e)
        {
            if (HorizontalMirrorActions != null && lstActions.SelectedItems.Count > 0)
            {
                var query = from item in lstActions.SelectedItems.Cast<ListViewItem>()
                            let idx = lstActions.Items.IndexOf(item)
                            orderby idx
                            select idx;

                int first = query.First();

                AbsoulteHorizontalMirrorActions(this, new ActionEventArgs { Index = first, Count = lstActions.SelectedItems.Count });
            }
        }

        private void btAVMirror_Click(object sender, EventArgs e)
        {
            if (VerticalMirrorActions != null && lstActions.SelectedItems.Count > 0)
            {
                var query = from item in lstActions.SelectedItems.Cast<ListViewItem>()
                            let idx = lstActions.Items.IndexOf(item)
                            orderby idx
                            select idx;

                int first = query.First();

                AbsoulteVerticalMirrorActions(this, new ActionEventArgs { Index = first, Count = lstActions.SelectedItems.Count });
            }
        }
    }

    public class ActionEventArgs : EventArgs
    {
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
