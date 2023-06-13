using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YourNameSpace
{
    public class RoundedForm : Component
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(int nR, int nL, int nT, int nB, int RoundWidth, int RoundHeight);
        private Control control = new();
        private int RRadius = 35;

        public Control TargetControl
        {
            get { return control; }
            set
            {
                control = value; control.SizeChanged += (sender, eventArgs) => control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, RRadius, RRadius));

            }
        }
        public int FormRadius
        {
            get { return RRadius; }
            set
            {
                RRadius = value;
                if (control != null)
                {
                    control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, RRadius, RRadius));
                }
            }

        }
    }
}
