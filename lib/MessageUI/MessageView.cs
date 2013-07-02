using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Resources;


namespace Cloudsdale.lib.MessageUI
{
    public partial class MessageView : System.Windows.Forms.Control
    {
        private Color m_selBackColor = SystemColors.Highlight;
        private Color m_selForeColor = SystemColors.HighlightText;
        private Color m_separatorColor = SystemColors.ActiveBorder;
        private Color m_transparentColor = Color.Magenta;
        private string m_line1Property = null;
        private string m_line2Property = null;
        private string m_itemImageProperty = null;
        private string m_dataMember = null;
        private bool m_useTransparent = false;

        private Object m_dataSource;		
        private IList m_list;				
        private int m_topItem;					
        private int m_visibleCount;				
        private int m_itemHeight;			
        private int m_textHeightLine1;			
        private int m_textHeightLine2;			
        private int m_selItem = -1;				
        public VScrollBar m_scrollBar;			
        private int m_scrollValue;
        private int m_scrollBarWidth;

        private ArrayList m_input;
        private Bitmap m_bmp;						
        private Image m_itemImage = null;			// The icon to draw for each item
        private Font m_fontLine1;					// The font used for drawing the text on line 1
        private Font m_fontLine2;					// The font used for drawing the text on line 2
        private Pen m_penSep;						// Pen used to draw the separator line
        private SolidBrush m_brushText;				// Brush for the unselected text
        private SolidBrush m_brushSelText;			// Brush for the selected text
        private SolidBrush m_brushSelBack;			// Brush for the selected item background
        private ImageAttributes m_imageAttributes;	// Image attributes we use for drawing transparent images

        //constants:
        private const int TEXT_PADDING_X = 4;
        private const int IMAGE_PADDING_X = 4;
        private const int IMAGE_PADDING_Y = 4;

        private new Font font
        {
            get { return m_fontLine1; }
            set
            {
                if (value != m_fontLine1)
                {
                    m_fontLine1 = value;
                    OnFontChanged(EventArgs.Empty);
                }
            }
        }
        public int ScollBarWidth
        {
            get { return m_scrollBarWidth; }
            set
            {
                if (value != m_scrollBarWidth)
                {
                    m_scrollBarWidth = value;
                    OnScrollBarWidthChanged(EventArgs.Empty);
                }
            }
        }
        public Font fontLine2
        {
            get { return m_fontLine2; }
            set
            {
                if (value != m_fontLine2)
                {
                }
            }
        }
    }
}
