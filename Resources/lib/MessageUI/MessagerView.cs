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
    public partial class MessageBox : System.Windows.Forms.Control
	{
		#region Private variables
		// Variables used for drawing
		private Color m_selBackColor = SystemColors.Highlight;
		private Color m_selForeColor = SystemColors.HighlightText;
		private Color m_separatorColor = SystemColors.ActiveBorder;
		private Color m_transparentColor = Color.Magenta;
		private string m_line1Property = null;
		private string m_line2Property = null;
		private string m_itemImageProperty = null;
		private string m_dataMember = null;
		private bool m_useTransparent = false;
		//ContactInfo c_data = new ContactInfo();

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
		private Image m_itemImage = null;			
		private Font m_fontLine1;					
		private Font m_fontLine2;					
		private Pen m_penSep;						
		private SolidBrush m_brushText;			
		private SolidBrush m_brushSelText;			
		private SolidBrush m_brushSelBack;			
		private ImageAttributes m_imageAttributes;	
		#endregion

		#region Constants
		private const int TEXT_PADDING_X = 4;
		private const int IMAGE_PADDING_X = 4;
		private const int IMAGE_PADDING_Y = 4;
		#endregion
		#region Properties
		private new Font Font
		{
			get { return null; }
		}
		public Font FontLine1
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return m_fontLine1; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (value != m_fontLine1)
				{
					m_fontLine1 = value;
					OnFontChanged(EventArgs.Empty);
				}
			}
		}
		public int ScrollBarWidth
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return m_scrollBarWidth; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (value != m_scrollBarWidth)
				{
					m_scrollBarWidth = value;
					OnScrollBarWidthChanged(EventArgs.Empty);
				}
			}
		}
		public Font FontLine2
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return m_fontLine2; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (value != m_fontLine2)
				{
					m_fontLine2 = value;

					OnFontChanged(EventArgs.Empty);
				}
			}
		}
		public Image ItemImage
		{
			get
			{
				return m_itemImage;
			}
			set
			{
				if (value != m_itemImage)
				{
					m_itemImage = value;
					OnItemImageChanged(EventArgs.Empty);
				}
			}
		}
		public Color SelectedBackColor
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return m_selBackColor; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (value != m_selBackColor)
				{
					m_selBackColor = value;
					OnSelectedBackColorChanged(EventArgs.Empty);
				}
			}
		}
		public Color SelectedForeColor
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return m_selForeColor; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (value != m_selForeColor)
				{
					m_selForeColor = value;
					OnSelectedForeColorChanged(EventArgs.Empty);
				}
			}
		}
		public Color SeparatorColor
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return m_separatorColor; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (value != m_separatorColor)
				{
					m_separatorColor = value;
					OnSeparatorColorChanged(EventArgs.Empty);
				}
			}
		}
		public Color TransparentColor
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return m_transparentColor; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (value != m_transparentColor)
				{
					m_transparentColor = value;
					OnTransparentColorChanged(EventArgs.Empty);
				}
			}
		}
		public bool UseTransparentColor
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return m_useTransparent; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (value != m_useTransparent)
				{
					m_useTransparent = value;
					OnUseTransparentColorChanged(EventArgs.Empty);
				}
			}
		}
		public int SelectedIndex
		{
			get
			{
				return m_selItem;
			}
			set
			{
				if (m_list == null)
					return;

				if (value >= 0 && value < m_list.Count && value != m_selItem)
				{
					m_selItem = value;
					if (SelectedIndexChanged != null)
						SelectedIndexChanged(this, EventArgs.Empty);
					Invalidate();
				}
			}
		}
		public object SelectedItem
		{
			get
			{
				if (m_selItem == -1)
					return null;

				if (m_list == null)
					return null;

				return m_list[m_selItem];

			}
		}

		public string selectedItemInfo
		{
			get
			{
				if (m_selItem == -1)
					return null;

				if (m_list == null)
					return null;

				return GetStringProperty(m_list[m_selItem], "PicturePath");
			}
		}

		public Image selectedItemImage
		{
			get
			{
				if (m_selItem == -1)
					return null;

				if (m_list == null)
					return null;

				return GetImageProperty(m_list[m_selItem], "Picture");
			}
		}
		public string Line1DisplayMember
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return this.m_line1Property; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				this.m_line1Property = value;
				Invalidate();
			}
		}
		public string Line2DisplayMember
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return this.m_line2Property; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				this.m_line2Property = value;
				Invalidate();
			}
		}
		public string ItemImageDisplayMember
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return this.m_itemImageProperty; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				this.m_itemImageProperty = value;
				Invalidate();
			}
		}
		public ArrayList Input
		{
			get
			{
				if (m_input == null)
				{
					m_input = new ArrayList();
				}
				m_list = (IList)m_input;
				Invalidate();
				return m_input;
			}
			set
			{
				Invalidate();			
			}
		}
		bool ShouldSerializeInput()
		{
			return (m_list != null);
		}
		bool ShouldSerializeList()
		{
			return (m_dataSource != null);
		}
		public Object DataSource
		{
			get
			{
				return m_dataSource;
			}
			set
			{
				m_dataSource = value;
				m_list = GetInnerDataSource(m_dataSource as IList);
				if (m_list != null)
				{
					IBindingList theBindingList = this.m_list as IBindingList;
					if (theBindingList != null)
					{
						theBindingList.ListChanged += new ListChangedEventHandler(OnListChanged);
					}
				}
				Invalidate();
			}
		}
		public string DataMember
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return this.m_dataMember; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				this.m_dataMember = value;
				Invalidate();
			}
		}
		#endregion
		#region Property Change Methods
		protected virtual void OnFontChanged(System.EventArgs e)
		{
			Invalidate();
			if (FontChanged != null)
				FontChanged(this, EventArgs.Empty);
		}
		protected virtual void OnForeColorChanged(System.EventArgs e)
		{
			CreateGdiObjects();
			Invalidate();
			if (ForeColorChanged != null)
				ForeColorChanged(this, EventArgs.Empty);
		}
		protected virtual void OnBackColorChanged(System.EventArgs e)
		{
			CreateGdiObjects();
			Invalidate();
			if (BackColorChanged != null)
				BackColorChanged(this, EventArgs.Empty);
		}
		protected void OnSelectedBackColorChanged(EventArgs e)
		{
			if (SelectedBackColorChanged != null)
				SelectedBackColorChanged(this, EventArgs.Empty);
			CreateGdiObjects();
			Invalidate();
		}
		protected void OnSelectedForeColorChanged(EventArgs e)
		{
			if (SelectedForeColorChanged != null)
				SelectedForeColorChanged(this, EventArgs.Empty);
			CreateGdiObjects();
			Invalidate();
		}
		protected void OnSeparatorColorChanged(EventArgs e)
		{
			if (SeparatorColorChanged != null)
				SeparatorColorChanged(this, EventArgs.Empty);
			CreateGdiObjects();
			Invalidate();
		}
		protected void OnTransparentColorChanged(EventArgs e)
		{
			m_imageAttributes.SetColorKey(m_transparentColor, m_transparentColor);

			if (TransparentColorChanged != null)
				TransparentColorChanged(this, EventArgs.Empty);
			Invalidate();
		}
		protected void OnUseTransparentColorChanged(EventArgs e)
		{
			if (UseTransparentColorChanged != null)
				UseTransparentColorChanged(this, EventArgs.Empty);
			Invalidate();
		}
		protected virtual void OnItemImageChanged(System.EventArgs e)
		{
			Invalidate();
			if (ItemImageChanged != null)
				ItemImageChanged(this, EventArgs.Empty);
		}
		protected virtual void OnImagesChanged(System.EventArgs e)
		{
			Invalidate();
			if (ImagesChanged != null)
				ImagesChanged(this, EventArgs.Empty);
		}
		protected virtual void OnScrollBarWidthChanged(System.EventArgs e)
		{
			Invalidate();
			if (ScrollBarWidthChanged != null)
				ScrollBarWidthChanged(this, EventArgs.Empty);
		}
		#endregion

		#region Events
		public event EventHandler SelectedIndexChanged;
		public event ListPaintEventHandler PaintBackground;
		public event ListPaintEventHandler PaintItem;
		public event EventHandler Action;
		public event EventHandler ItemImageChanged;
		public event EventHandler ImagesChanged;
		public event EventHandler SelectedBackColorChanged;
		public event EventHandler TransparentColorChanged;
		public event EventHandler UseTransparentColorChanged;
		public event EventHandler SelectedForeColorChanged;
		public event EventHandler SeparatorColorChanged;
		public event EventHandler ScrollBarWidthChanged;
		public event EventHandler FontChanged;
		public event EventHandler ForeColorChanged;
		public event EventHandler BackColorChanged;
#endregion

		#region Methods
		public MessageBox()
		{
			Line1DisplayMember = "Name";
			Line2DisplayMember = "PhoneNumber";
			ItemImageDisplayMember = "Picture";

			string platform = "Pocket PC"; //GetCurrentPlatform();
			if (platform == "Pocket PC")
			{
				m_fontLine1 = new Font("Tahoma", 8.0F, FontStyle.Regular);
				m_fontLine2 = new Font("Tahoma", 8.0F, FontStyle.Regular);
				m_scrollBarWidth = 14;
				this.Size = new Size(224, 200);
			}
			else if (platform == "Smartphone")
			{
				m_fontLine1 = new Font("Nina", 10.0F, FontStyle.Regular);
				m_fontLine2 = new Font("Nina", 10.0F, FontStyle.Regular);
				m_scrollBarWidth = 8;
				this.Size = new Size(160, 136);
			}
			else
			{
				m_fontLine1 = new Font("Arial", 10.0F, FontStyle.Regular);
				m_fontLine2 = new Font("Arial", 10.0F, FontStyle.Regular);
				m_scrollBarWidth = 14;
				this.Size = new Size(224, 200);
			}
			CreateGdiObjects();
			m_scrollBar = new VScrollBar();
			m_scrollBar.ValueChanged += new System.EventHandler(this.scrollBar_ValueChanged);
			this.Controls.Add(m_scrollBar);
		}
		private IList GetInnerDataSource(object item)
		{
			try
			{
				if (item is DataSet)
				{
					if (m_dataMember != null)
						return ((IListSource)((DataSet)item).Tables[m_dataMember]).GetList();
					else
						return ((IListSource)((DataSet)item).Tables[0]).GetList();
				}
				else if (item is IListSource)
					return ((IListSource)item).GetList();
				else
					return item as IList;
			}
			catch
			{
				return null;
			}
		}
		public void Clear()
		{
			m_list = null;
			m_selItem = -1;
			RecalcScrollBar();
			Invalidate();
		}
		private void ResetScrollPos()
		{
			m_scrollValue = 0;
			m_scrollBar.Value = 0;
			m_selItem = -1;
			Invalidate();
		}
		private string GetStringProperty(object o, string property)
		{
			try
			{
				PropertyInfo objProp = o.GetType().GetProperty(property);
				if (objProp == null)
					return null;
				return (objProp.GetValue(o, null)).ToString();
			}
			catch
			{
				return null;
			}
		}
		private Image GetImageProperty(object o, string property)
		{
			try
			{
				PropertyInfo objProp = o.GetType().GetProperty(property);
				if (objProp == null)
					return null;
				return (objProp.GetValue(o, null)) as Image;
			}
			catch
			{
				return null;
			}
		}
		#endregion

		#region Scroll Bar Event Handler and Helpers
		private void scrollBar_ValueChanged(object sender, System.EventArgs e)
		{
			m_scrollValue = m_scrollBar.Value;
			Invalidate();
		}
		private void RecalcScrollBar()
		{
			if (m_list == null)
			{
				m_scrollBar.Enabled = false;
				m_scrollBar.Maximum = 0;
			}
			else
			{
				// update scrollbar 
				m_scrollBar.Minimum = m_list.Count;
				m_scrollBar.LargeChange = Math.Max(m_visibleCount - 1, 1);
				m_scrollBar.Visible = (m_visibleCount < m_list.Count);
			}
		}
		#endregion

		#region Drawing Methods and Helpers
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			CreateMemoryBitmap();
			RecalcItems(e.Graphics);
			Graphics g = Graphics.FromImage(m_bmp);
			OnPaintBackground(new ListPaintEventArgs(g));
			if (m_list != null)
				DrawItems(g);
			Rectangle rc = new Rectangle(0, 0, this.Width - m_scrollBarWidth, this.Height - 1);
			g.DrawRectangle(new Pen(Color.Black), rc);
			e.Graphics.DrawImage(m_bmp, 0, 0);
		}
		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}
		protected virtual void OnPaintBackground(ListPaintEventArgs e)
		{
			e.Graphics.Clear(this.BackColor);
			if (PaintBackground != null)
				PaintBackground(this, e);
		}
		protected virtual void OnPaintItem(ListPaintEventArgs e)
		{

			Graphics g = e.Graphics;
			Rectangle destRect = new Rectangle();	
			Rectangle srcRect = new Rectangle();	
			Rectangle textRect = new Rectangle();	
			int lineIndent = 0;						
			Image imageToDraw = null;
			string line1Text;
			string line2Text;
			if (e.Item == null)
				goto DrawSeparator;
			line1Text = GetStringProperty(e.Item, this.m_line1Property);
			if (line1Text == null)
				line1Text = e.Item.ToString();

			line2Text = GetStringProperty(e.Item, this.m_line2Property);
			if (line2Text == null)
				line2Text = e.Item.ToString();

			imageToDraw = GetImageProperty(e.Item, this.m_itemImageProperty);
			if (imageToDraw == null)

				imageToDraw = m_itemImage;
			if (imageToDraw != null)
			{
				srcRect.X = 0;
				srcRect.Y = 0;
				srcRect.Width = imageToDraw.Width;
				srcRect.Height = imageToDraw.Height;

				destRect.X = e.ClipRectangle.X + IMAGE_PADDING_X;
				destRect.Y = e.ClipRectangle.Y + IMAGE_PADDING_Y;
				destRect.Height = (imageToDraw.Height > m_itemHeight - IMAGE_PADDING_Y) ? m_itemHeight - (IMAGE_PADDING_Y * 2) : imageToDraw.Height;
				destRect.Width = destRect.Height;
				lineIndent = IMAGE_PADDING_X + imageToDraw.Width + TEXT_PADDING_X;	
			}
			else
			{
				lineIndent = TEXT_PADDING_X;
			}

			// Calculate the text rectangle
			textRect.X = e.ClipRectangle.X + lineIndent;
			textRect.Width = e.ClipRectangle.Width - TEXT_PADDING_X - textRect.X;	
			textRect.Y = e.ClipRectangle.Y + 2;
			textRect.Height = this.m_textHeightLine1;

			if (e.Selected)
				g.FillRectangle(m_brushSelBack, e.ClipRectangle);

			// Draw the icon
			if (imageToDraw != null)
			{
				if (m_useTransparent)

					g.DrawImage(imageToDraw, destRect, srcRect.Y, srcRect.Y, srcRect.Height, srcRect.Height,
						GraphicsUnit.Pixel, m_imageAttributes);
				else
					g.DrawImage(imageToDraw, destRect, srcRect, GraphicsUnit.Pixel);
			}

			// Draw the text in a bounding rect to force it to truncate if too long
			g.DrawString(line1Text, m_fontLine1, e.Selected ? m_brushSelText : m_brushText, textRect);

			// Draw the second line
			textRect.Y += m_textHeightLine1 + 3;
			textRect.Height = this.m_textHeightLine2;
			g.DrawString(line2Text, m_fontLine2, e.Selected ? m_brushSelText : m_brushText, textRect);

		DrawSeparator:
			// Draw the separator line
			g.DrawLine(m_penSep, e.ClipRectangle.X, e.ClipRectangle.Y + e.ClipRectangle.Height,
				e.ClipRectangle.X + e.ClipRectangle.Width, e.ClipRectangle.Y + e.ClipRectangle.Height);

			// Let other people know it's time for them to draw
			if (PaintItem != null)
				PaintItem(this, e);
		}


		// Draw all the items.
		private void DrawItems(Graphics g)
		{
			ListPaintEventArgs ListPaintEventArgs = new ListPaintEventArgs(g);

			Rectangle rc = new Rectangle(0, 0, this.Width - m_scrollBarWidth, this.Height - 1);

			// draw items that are visible
			int curItem = 0;
			for (int i = 0; (i < m_visibleCount); i++)
			{
				curItem = i + m_topItem;
				if (curItem < m_list.Count)
				{
					ListPaintEventArgs.ClipRectangle = new Rectangle(rc.X,
						rc.Y + (i * m_itemHeight),
						rc.Width,
						this.m_itemHeight);			
					ListPaintEventArgs.Item = m_list[curItem];
					ListPaintEventArgs.Selected = (m_selItem == curItem);

					OnPaintItem(ListPaintEventArgs);
				}
			}
		}
		// TODO: Get rid of this method by moving the rest of the items into the assorted
		// properties.
		private void RecalcItems(Graphics g)
		{
			// The text heights for a single line of text is the height of the font.
			m_textHeightLine1 = g.MeasureString("W", this.m_fontLine1).ToSize().Height;
			m_textHeightLine2 = g.MeasureString("W", this.m_fontLine2).ToSize().Height;

			// The height for an individual item is two lines plus some padding
			m_itemHeight = m_textHeightLine1 + m_textHeightLine2 + 5;

			m_visibleCount = this.Height / m_itemHeight;

			// Set the top item to draw to the current scroll position
			m_topItem = m_scrollValue;
		}

		// Creates all the objects we need for drawing
		private void CreateGdiObjects()
		{
			m_brushText = new SolidBrush(this.ForeColor);
			m_brushSelText = new SolidBrush(this.m_selForeColor);
			m_brushSelBack = new SolidBrush(this.m_selBackColor);
			m_penSep = new Pen(this.m_separatorColor);
			m_imageAttributes = new ImageAttributes();
		}
		private void CreateMemoryBitmap()
		{
			if (m_bmp == null || m_bmp.Width != this.Width || m_bmp.Height != this.Height)
			{
				m_bmp = new Bitmap(this.Width - m_scrollBarWidth, this.Height);

				// TODO: Figure out why this is here.
				m_scrollBar.Left = this.Width - m_scrollBarWidth;
				m_scrollBar.Top = 0;
				m_scrollBar.Width = m_scrollBarWidth;
				m_scrollBar.Height = this.Height;
			}
		}
		#endregion

		#region Mouse/Pointer Methods and Helpers
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (m_list == null)
				return;

			// determine what item was selected
			m_selItem = HitTest(e.X, e.Y);
			Invalidate();
			Update();
		}

		private int HitTest(int x, int y)
		{
			// loop through visible items
			int itemPos = 0;
			int curItem = 0;

			for (int i = 0; i < m_visibleCount; i++)
			{
				// went past list, no items are in hit area
				curItem = i + m_topItem;
				if (curItem >= m_list.Count)
					return -1;

				// found item in hit area					
				if (y > itemPos && y < (itemPos + m_itemHeight))
					return curItem;

				// move to next item
				itemPos += m_itemHeight;

				// moved outside of list control, no items are in hit area
				if (itemPos > this.Height)
					return -1;
			}

			// did not find any items in hit area
			return -1;
		}
		#endregion

		#region Keypress Methods and Helpers
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (m_list == null)
			{
				base.OnKeyDown(e);
				return;
			}

			switch (e.KeyCode)
			{
				case Keys.Up:
					if (this.m_selItem <= 0)
						return;
					m_selItem--;
					if (m_selItem < this.m_topItem)
						m_scrollBar.Value = m_selItem;
					Invalidate();

					if (SelectedIndexChanged != null)
						SelectedIndexChanged(this, EventArgs.Empty);
					break;
				case Keys.Down:
					if (this.m_selItem >= this.m_list.Count - 1)
						return;

					m_selItem++;
					if (m_selItem > (this.m_topItem + this.m_visibleCount - 1))
						m_scrollBar.Value = m_selItem;
					Invalidate();

					if (SelectedIndexChanged != null)
						SelectedIndexChanged(this, EventArgs.Empty);
					break;
				case Keys.Enter:
					OnAction(EventArgs.Empty);
					break;
			}
			base.OnKeyDown(e);
		}
		public virtual void OnAction(EventArgs e)
		{
			if (Action != null)
				Action(this, EventArgs.Empty);
		}
		#endregion

		#region IBindingList Event Handlers
		private void OnListChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
				case ListChangedType.ItemAdded:
				case ListChangedType.ItemChanged:
					if ((e.NewIndex >= m_topItem) && (e.NewIndex < m_topItem + m_visibleCount))
						Invalidate();

					RecalcScrollBar();
					break;
				case ListChangedType.ItemDeleted:
					if (m_selItem >= m_list.Count)
						m_selItem = m_list.Count - 1;

					RecalcScrollBar();

					if ((e.NewIndex >= m_topItem) && (e.NewIndex < m_topItem + m_visibleCount))
						Invalidate();

					break;
				case ListChangedType.Reset:
					Invalidate();
					return;
				case ListChangedType.PropertyDescriptorAdded:
				case ListChangedType.PropertyDescriptorChanged:
				case ListChangedType.PropertyDescriptorDeleted:
					return;
			}
		}
		#endregion

		#region Internal Class for Drawing Events

		public class ListPaintEventArgs
		{
			private Graphics m_graphics;
			private Rectangle m_ClipRectangle;
			private object m_itemToDraw;
			private bool m_selected;

			public ListPaintEventArgs(Graphics g)
			{
				m_itemToDraw = null;
				m_graphics = g;
				m_ClipRectangle = new Rectangle();
				m_selected = false;
			}
			public object Item
			{
				get { return m_itemToDraw; }
				set { m_itemToDraw = value; }
			}
			public Graphics Graphics
			{
				get { return m_graphics; }
			}
			public Rectangle ClipRectangle
			{
				get { return m_ClipRectangle; }
				set { m_ClipRectangle = value; }
			}
			public bool Selected
			{
				get { return m_selected; }
				set { m_selected = value; }
			}
		}
		#endregion

		#region Delegates for the namespace
		public delegate void ListPaintEventHandler(object sender, ListPaintEventArgs e);
		#endregion
	}
	public class Message
	{
		private string m_name;
		private string m_time;
		private System.Drawing.Image m_picture;
		private string m_picturePath;

		public Message()
		{ }

		public Message(string name, string currentTime)
		{
			Name = name;
			CurrentTime = currentTime;
			Picture = null;
			PicturePath = "";
		}
		public Message(string name, string phoneNumber, Image picture, string path)
		{
			Name = name;
			CurrentTime = phoneNumber;
			Picture = picture;
			PicturePath = path;
		}

		public Message(string name, string phoneNumber, Image picture)
		{
			Name = name;
			CurrentTime = phoneNumber;
			Picture = picture;
			PicturePath = "";
		}

		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}

		public string CurrentTime
		{
			get { return m_time; }
			set { m_time = value; }
		}

		public System.Drawing.Image Picture
		{
			get { return m_picture; }
			set { m_picture = value; }
		}

		public string PicturePath
		{
			get { return m_picturePath; }
			set { m_picturePath = value; }
		}
	}
}
