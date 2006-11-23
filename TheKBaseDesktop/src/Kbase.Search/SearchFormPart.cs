using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Kbase.Model.Search;

namespace Kbase.Search
{
    /// <summary>
    /// Note to self: in the future, don't use this UserControl
    /// crap. You're tying to things that can't be ported to 
    /// .NET Mobile. Bastards!
    /// </summary>
	public class SearchFormPart : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ComboBox boxParentOrAncestor;
		private System.Windows.Forms.ComboBox boxContainsIs;
		private System.Windows.Forms.ComboBox boxConcat;
        private System.Windows.Forms.ComboBox boxTextOrTitle;
        private System.Windows.Forms.TextBox textBoxSearchText;
        private System.Windows.Forms.LinkLabel selectLink;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SearchFormPart()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			LoadComboBoxes();

		}

		private void LoadComboBoxes() {
			LoadBox(boxParentOrAncestor,SearchTypeWhere.Ancestor);
			LoadBox(boxTextOrTitle,SearchTypeTextTitle.Text);
			LoadBox(boxContainsIs,SearchTypeIsContains.Contains);
			LoadBox(boxConcat,SearchTypeConcat.And);

		}

		bool firstLine = false;

		public bool FirstLine {
			set {
				firstLine = value;
				SetupConcat();
			}
		}

		private void SetupConcat() {
			if (firstLine) 
			{
				boxConcat.SelectedIndex = boxConcat.Items.IndexOf(SearchTypeConcat.None);
				boxConcat.Visible = false;
			} 
			else {
				boxConcat.Visible = true;
				boxConcat.Items.Remove(SearchTypeConcat.None);
				boxConcat.SelectedItem = SearchTypeConcat.And;
			}
		}

		public SearchCriterion GetSearchCriterion() {
			SearchCriterion retVal = new SearchCriterion();
            retVal.ConcatWithLast =  SearchTypeConcat.None;
            if (boxConcat.SelectedItem != null)
                retVal.ConcatWithLast = (SearchTypeConcat)SearchCriterionConverter.GetTextAsEnum(boxConcat.SelectedItem.ToString(), typeof(SearchTypeConcat));
            retVal.IsContains = (SearchTypeIsContains)SearchCriterionConverter.GetTextAsEnum(boxContainsIs.SelectedItem.ToString(), typeof(SearchTypeIsContains));
            retVal.Where = (SearchTypeWhere)SearchCriterionConverter.GetTextAsEnum(boxParentOrAncestor.SelectedItem.ToString(), typeof(SearchTypeWhere));
            retVal.TextTitle = (SearchTypeTextTitle)SearchCriterionConverter.GetTextAsEnum(boxTextOrTitle.SelectedItem.ToString(), typeof(SearchTypeTextTitle));
			retVal.Word = textBoxSearchText.Text;
			retVal.IgnoreCase = true;
			return retVal;
		}

		private void LoadBox(ComboBox box, object obj) {
			Array items = Enum.GetValues(obj.GetType());
			foreach (Object where in items) 
			{
				box.Items.Add(SearchCriterionConverter.GetEnumAsText(where));
			}
			box.SelectedIndex = 0;
		}		

		public void Reset() {
			if (!firstLine) 
				boxConcat.SelectedItem = SearchTypeConcat.And;
			boxParentOrAncestor.SelectedIndex = 0;
			boxTextOrTitle.SelectedIndex = 0;
			boxContainsIs.SelectedIndex = 0;
			textBoxSearchText.Clear();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.boxParentOrAncestor = new System.Windows.Forms.ComboBox();
			this.boxContainsIs = new System.Windows.Forms.ComboBox();
			this.boxConcat = new System.Windows.Forms.ComboBox();
			this.textBoxSearchText = new System.Windows.Forms.TextBox();
			this.boxTextOrTitle = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// boxParentOrAncestor
			// 
			this.boxParentOrAncestor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.boxParentOrAncestor.Location = new System.Drawing.Point(48, 0);
			this.boxParentOrAncestor.Name = "boxParentOrAncestor";
			this.boxParentOrAncestor.Size = new System.Drawing.Size(104, 21);
			this.boxParentOrAncestor.TabIndex = 2;
			// 
			// boxContainsIs
			// 
			this.boxContainsIs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.boxContainsIs.Location = new System.Drawing.Point(248, 0);
			this.boxContainsIs.Name = "boxContainsIs";
			this.boxContainsIs.Size = new System.Drawing.Size(72, 21);
			this.boxContainsIs.TabIndex = 4;
			// 
			// boxConcat
			// 
			this.boxConcat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.boxConcat.Location = new System.Drawing.Point(0, 0);
			this.boxConcat.Name = "boxConcat";
			this.boxConcat.Size = new System.Drawing.Size(48, 21);
			this.boxConcat.TabIndex = 1;
			// 
			// boxTextOrTitle
			// 
			this.boxTextOrTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.boxTextOrTitle.Location = new System.Drawing.Point(152, 0);
			this.boxTextOrTitle.Name = "boxTextOrTitle";
			this.boxTextOrTitle.Size = new System.Drawing.Size(96, 21);
			this.boxTextOrTitle.TabIndex = 3;

            // 
            // textBoxSearchText
            // 
            this.textBoxSearchText.Location = new System.Drawing.Point(320, 0);
            this.textBoxSearchText.Name = "textBoxSearchText";
            this.textBoxSearchText.Size = new System.Drawing.Size(232, 20);
            this.textBoxSearchText.TabIndex = 5;
            this.textBoxSearchText.Text = "";

            // 
            // selectLink
            // 
            this.selectLink = new LinkLabel();
            this.selectLink.Text = "Select Snippet";
            this.selectLink.Location = new System.Drawing.Point(562, 0);
            this.selectLink.Name = "textBoxSearchText";
            this.selectLink.Size = new System.Drawing.Size(100, 20);
            this.selectLink.Visible = true;
            this.selectLink.Click += new EventHandler(selectLink_Click);
            this.selectLink.TabIndex = 6;
            

            // 
			// SearchFormPart
			// 
			this.Controls.Add(this.boxParentOrAncestor);
			this.Controls.Add(this.boxContainsIs);
			this.Controls.Add(this.boxConcat);
			this.Controls.Add(this.textBoxSearchText);
			this.Controls.Add(this.boxTextOrTitle);
            this.Controls.Add(this.selectLink);
            this.Name = "SearchFormPart";
			this.Size = new System.Drawing.Size(652, 24);
			this.ResumeLayout(false);



		}

        void selectLink_Click(object sender, EventArgs e)
        {
            Universe.Instance.mainForm.Show();
            Universe.Instance.mainForm.Focus();
            Universe.Instance.snippetPane.SelectSnippetLinkStart(new SomeSnippetEventHandler(this.PlugId), this.Parent);
        }

        public void PlugId(Kbase.Model.Snippet snippet) {
            this.textBoxSearchText.Text = snippet.Id.ToString();
            this.boxTextOrTitle.SelectedItem = "Id";
        }
	}
}
