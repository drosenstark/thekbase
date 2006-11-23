using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Kbase.MainFrm;
using Kbase.Model.Search;

namespace Kbase.Search
{
	public class SearchForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.Button buttonCancel;
		private SearchFormPart searchFormPart;
		private SearchFormPart searchFormPart1;
		private SearchFormPart searchFormPart2;
		private System.Windows.Forms.Button buttonReset;
		/// <summary>
		/// Required designer variable.
		/// </summary>

        private System.ComponentModel.Container components = null;

		public SearchForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.searchFormPart.FirstLine = true;
			this.searchFormPart1.FirstLine = false;
			this.searchFormPart2.FirstLine = false;
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
			this.buttonSearch = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.searchFormPart = new SearchFormPart();
			this.searchFormPart1 = new SearchFormPart();
			this.searchFormPart2 = new SearchFormPart();
			this.buttonReset = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(488, 88);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.TabIndex = 4;
			this.buttonSearch.Text = "Search";
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			this.AcceptButton = buttonSearch;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(408, 88);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// searchFormPart
			// 
			this.searchFormPart.Location = new System.Drawing.Point(8, 8);
			this.searchFormPart.Name = "searchFormPart";
			this.searchFormPart.Size = new System.Drawing.Size(652, 24);
			this.searchFormPart.TabIndex = 0;
			// 
			// searchFormPart1
			// 
			this.searchFormPart1.Location = new System.Drawing.Point(8, 32);
			this.searchFormPart1.Name = "searchFormPart1";
			this.searchFormPart1.Size = new System.Drawing.Size(652, 24);
			this.searchFormPart1.TabIndex = 0;
			// 
			// searchFormPart2
			// 
			this.searchFormPart2.Location = new System.Drawing.Point(8, 56);
			this.searchFormPart2.Name = "searchFormPart2";
			this.searchFormPart2.Size = new System.Drawing.Size(652, 24);
			this.searchFormPart2.TabIndex = 0;
			// 
			// buttonReset
			// 
			this.buttonReset.Location = new System.Drawing.Point(8, 88);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.TabIndex = 5;
			this.buttonReset.Text = "Reset";
			this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
			// 
			// SearchForm
			// 
			this.AutoScaleDimensions = new SizeF(5, 13);
			this.ClientSize = new System.Drawing.Size(670, 120);
			this.ControlBox = false;
			this.Controls.Add(this.buttonReset);
			this.Controls.Add(this.searchFormPart);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.searchFormPart1);
			this.Controls.Add(this.searchFormPart2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "SearchForm";
			ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Search For Snippets";
			this.Load += new System.EventHandler(this.SearchForm_Load);
			this.ResumeLayout(false);

		}

		private void comboBoxConcat_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.Visible = false;
			} 
			catch (Exception e2) 
			{
				MainForm.ShowError(e2);
			}

		}

		private void buttonSearch_Click(object sender, System.EventArgs e)
		{
			try 
			{
                List<SearchCriterion> criteria = new List<SearchCriterion>();
				criteria.Add(searchFormPart.GetSearchCriterion());
				criteria.Add(searchFormPart1.GetSearchCriterion());
				criteria.Add(searchFormPart2.GetSearchCriterion());
                Universe.Instance.ModelGateway.Search(criteria);
                this.Visible = false;
            } 
			catch (Exception e2) 
			{
				MainForm.ShowError(e2);
			}

		}

		private void SearchForm_Load(object sender, System.EventArgs e)
		{
		
		}

		private void buttonReset_Click(object sender, System.EventArgs e)
		{
			try 
			{
				searchFormPart.Reset();
				searchFormPart1.Reset();
				searchFormPart2.Reset();
			} 
			catch (Exception e2) 
			{
				MainForm.ShowError(e2);
			}

		
		}
	}
}
