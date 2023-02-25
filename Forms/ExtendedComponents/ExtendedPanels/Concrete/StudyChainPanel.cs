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
    public class StudyChainPanel : BasePanel
    {
        public int Day { get; }
        public int Month { get; }

        public StudyChainPanel(Point point, Size size, int tabIndex, string name, int day, int month) 
            : base(point, size, tabIndex, name)
        {
            Day = day;
            Month = month;
        }

        public void CheckAsStudied()
        {
            BackColor = Color.Green;
        }

    }
}
