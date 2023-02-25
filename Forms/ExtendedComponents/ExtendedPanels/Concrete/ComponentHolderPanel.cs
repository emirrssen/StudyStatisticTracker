using Forms.ExtendedComponents.ExtendedPanels.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public class ComponentHolderPanel : BasePanel
    {
        public ComponentHolderPanel(Point point, Size size, int tabIndex, string name, Color color) 
            : base(point, size, tabIndex, name)
        {
            BackColor = color;
        }
    }
}
