using System.Windows;
using System.Windows.Controls;

namespace CandyControls
{
    public class CandyMenu : Menu
    {
        public CandyMenu()
        {
            this.SetResourceReference(CandyMenu.StyleProperty, nameof(CandyMenu));
        }
    }

    public class CandyMenuItem : MenuItem
    {
        public CandyMenuItem()
        {
            this.SetResourceReference(CandyMenuItem.StyleProperty, nameof(CandyMenuItem));
        }
    }
}
