using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.ExtendedComponents.ExtendedPanels.Abstract
{
    public class BasePanel : Panel
    {
        public BasePanel(Point point, Size size, int tabIndex, string name)
        {
            Location = point;
            Size = size;
            TabIndex = tabIndex;
            Name = name;
        }
    }
}
