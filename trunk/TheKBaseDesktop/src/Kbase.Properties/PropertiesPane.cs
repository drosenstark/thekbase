/*
This file is part of TheKBase Desktop
A Multi-Hierarchical  Information Manager
Copyright (C) 2004-2006 Daniel Rosenstark

TheKBase Desktop is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
("GPL") version 2 as published by the Free Software Foundation.
See the file LICENSE.TXT for the full text of the GNU GPL, or see
http://www.gnu.org/licenses/gpl.txt

For using TheKBase Desktop with software that can not be combined with 
the GNU GPL or any other queries, please contact Daniel Rosenstark 
(license@thekbase.com).
*/
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Kbase.SnippetTreeView;
using Kbase.Model;
using Kbase.MainFrm;


namespace Kbase.Properties
{
	/// <summary>
	/// Summary description for PropertiesPane.
	/// </summary>
	public class PropertiesPane : System.Windows.Forms.Panel
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.Container components = null;

        ParentPane.ParentPane parentPane;
        LocationPane2.LocationPane locationPane;
        TabControl locationPanes;
        TabPage locationPanesPageLocations;
        TabPage locationPanesPageParents;

		SnippetTitleBox snippetTitle;
        SnippetDateBox snippetDate;
        Label instructions;
		Panel topPanel;

		public PropertiesPane()
		{
			Init();
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

		private void Init()
		{
			this.SuspendLayout();

			instructions = new Label();
			instructions.Text = "Drag properties here";
			instructions.ForeColor = Color.BlueViolet;
			instructions.Width = this.ClientSize.Width;
			
			// 
			// snippetTitle
			// 
			this.snippetTitle = new SnippetTitleBox();
			this.snippetTitle.Location = new System.Drawing.Point(0, 0);
			this.snippetTitle.Width = this.ClientSize.Width;
			this.snippetTitle.Height = instructions.Height;
			this.snippetTitle.Multiline = false;
			this.snippetTitle.Name = "Snippet Title";
			this.snippetTitle.TabIndex = 0;
			this.snippetTitle.ReadOnly = false;

            // 
            // snippetDate
            // 
            this.snippetDate = new SnippetDateBox();
            this.snippetDate.Location = new System.Drawing.Point(0, 0);
            this.snippetDate.Width = this.ClientSize.Width;
            this.snippetDate.Height = instructions.Height;
            this.snippetDate.AutoSize = true;
            //this.snippetDate.Dock = DockStyle.Right;
            this.snippetDate.Text = new DateTime().ToString();
            this.snippetDate.BorderStyle = BorderStyle.Fixed3D;

			// Top Panel
			topPanel = new Panel();
			topPanel.Dock = DockStyle.Top;
            topPanel.Controls.Add(this.snippetDate);
			topPanel.Controls.Add(this.snippetTitle);
			topPanel.Controls.Add(instructions);


            // 
            // nodeInfo
            // 
            this.parentPane = new ParentPane.ParentPane();
            this.parentPane.Location = new System.Drawing.Point(0, 15);
            this.parentPane.Dock = DockStyle.Fill;
            this.parentPane.AutoSize = true;

            this.locationPane = new LocationPane2.LocationPane();
            this.locationPane.Location = new System.Drawing.Point(0, 15);
            this.locationPane.Dock = DockStyle.Fill;
            this.locationPane.AutoSize = true;


            // Tab control
            this.locationPanes = new TabControl();
            this.locationPanes.Location = new System.Drawing.Point(0, 15);
            this.locationPanes.Dock = DockStyle.Fill;
            this.locationPanes.AutoSize = true;

            locationPanes.SelectedIndexChanged += new EventHandler(locationPanes_SelectedIndexChanged);

            locationPanesPageLocations = new TabPage("Parents");
            locationPanesPageLocations.Controls.Add(parentPane);
            locationPanesPageParents = new TabPage("Locations");
            locationPanesPageParents.Controls.Add(locationPane);
            locationPanes.TabPages.Add(locationPanesPageParents);
            locationPanes.TabPages.Add(locationPanesPageLocations);


			
			// 
			// PropertiesPane
			// 
			this.AllowDrop = true;
			this.AutoScroll = true;
			this.VScroll = true;
			this.Controls.Add(locationPanes);
			this.Controls.Add(topPanel);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Name = "PropertiesPane";
			this.Text = "Properties";
			this.ResumeLayout(false);
		}

        void locationPanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locationPanes.SelectedIndex == 0)
                parentPane.Focus();
            else
                locationPane.Focus();
        }



		protected override void OnDragEnter(DragEventArgs e)
		{
			try 
			{
				base.OnDragEnter (e);
				e.Effect = DragDropEffects.All;	
			} 
			catch (Exception e2) 
			{
				MainForm.ShowError(e2);
			}

		}	
		protected override void OnDragDrop(DragEventArgs e)
		{
			try 
			{	
				base.OnDragDrop(e);
				Kbase.Serialization.SerializableUniverse draggedData = (Kbase.Serialization.SerializableUniverse)e.Data.GetData("Kbase.SerializableUniverse");
				// if the drag has come from another KBase, jump out
				if (draggedData != null && draggedData.GetHashCode() != Universe.Instance.snippetPane.hashCodeOfNodesBeingDragged) 
				{
					return;
				} 

				ArrayList selectedNodesCopy = new ArrayList(Universe.Instance.snippetPane.SelectedNodes);
				foreach (SnippetTNode node in selectedNodesCopy) 
				{
					Snippet snippet = node.Snippet;
					RegisterProperty(snippet);
				}
				LayoutProperties();

			} 
			catch (Exception e2) 
			{
				MainForm.ShowError(e2);
			}
		}

		ArrayList propertyCombos = new ArrayList();
		List<Snippet> selectedSnippets = null;

		public void RegisterProperty(Snippet property) 
		{
			PropertyComboBox box = new PropertyComboBox(property);
            if (this.selectedSnippets != null)
                box.Edit(this.selectedSnippets);
			topPanel.Controls.Add(box);
			this.propertyCombos.Add(box);
			box.MouseDown += new MouseEventHandler(box_MouseDown);
			box.label1.MouseDown += new MouseEventHandler(box_MouseDown);
		}

		public void DeregisterProperty(PropertyComboBox box) 
		{
			topPanel.Controls.Remove(box);
			this.propertyCombos.Remove(box);
			LayoutProperties();
		}

        public void OnAfterRestoreProperties() {
            LayoutProperties();        
        }


		void LayoutProperties() 
		{
			SuspendLayout();

			int y = 0;
			int tabIndex = 0;
			topPanel.Width = ClientRectangle.Width;
			topPanel.TabIndex = tabIndex++;

            if (selectedSnippets != null && selectedSnippets.Count == 1)
            {
                snippetTitle.Location = new System.Drawing.Point(0, y);
                y += snippetTitle.Height;
                // at some point I'll redo this, just leaving space to the right
                // and in between
                snippetTitle.Width = topPanel.ClientRectangle.Width - snippetDate.Width - 5;
                snippetTitle.TabIndex = tabIndex++;
                snippetTitle.Visible = true;

                snippetDate.Location = new System.Drawing.Point(snippetTitle.Width + 3, y - snippetTitle.Height);
                snippetDate.Visible = true;
                
            }
            else
            {
                snippetTitle.Visible = false;
                snippetDate.Visible = false;
            }

			foreach (PropertyComboBox box in propertyCombos) 
			{
				box.Location = new System.Drawing.Point(0, y);
				y += box.Height;
				box.Width = topPanel.ClientRectangle.Width;
				box.TabIndex = tabIndex++;
			} 

			// if there are no properties show the instructions
			if (propertyCombos.Count == 0) 
			{
				instructions.Visible = true;
				instructions.Top = y;
				y += instructions.Height;
				instructions.Width = topPanel.ClientSize.Width;
			} 
			else
				instructions.Visible = false;
			
			topPanel.Height = y;
			locationPanes.Width = ClientRectangle.Width;
            locationPanes.Location = new System.Drawing.Point(0, y);
            locationPanes.Refresh();
            locationPanes.TabIndex = tabIndex++;
			ResumeLayout(true);

		}

		public void EditNone() 
		{
            if (this.selectedSnippets != null)
                this.selectedSnippets.Clear();
            snippetTitle.Edit(null);
            snippetDate.Edit(null);
            parentPane.Clear();
            locationPane.Clear();
            LayoutProperties();
            this.Enabled = false;
		
		}

		public void Edit(SnippetInstance instance) 
		{
			if (instance == null) 
			{
				EditNone();
				return;
			}
			this.Enabled = true;
            selectedSnippets = new List<Snippet>(1);
            selectedSnippets.Add(instance.Snippet);
			foreach (PropertyComboBox box in propertyCombos) 
			{
				box.Edit(selectedSnippets);
			}

			snippetTitle.Edit(instance.Snippet);
            snippetDate.Edit(instance.Snippet);

            parentPane.Edit(instance);


            locationPane.Edit(instance);

			LayoutProperties();
		}

		public void Edit(List<Snippet> selectedSnippets) 
		{
			if (selectedSnippets == null)
				return;
            this.selectedSnippets = selectedSnippets;
            this.Enabled = true;
			foreach (PropertyComboBox box in propertyCombos) 
			{
				box.Edit(selectedSnippets);
			}
			Text = "- Multiple Selection ("+ selectedSnippets.Count +" Snippets) -\n";
			foreach (Snippet snippet in selectedSnippets) 
			{
                //Text += node.Text + " (" + node.Location + ")\n";
			}
            parentPane.Clear();
            locationPane.Clear();
            LayoutProperties();
		}

		public bool IsEditingSnippet(Snippet snippet) 
		{
			bool retVal = false;
			if (this.selectedSnippets != null)
				return (this.selectedSnippets.Contains(snippet));
			return retVal;
		}



		protected override void OnResize(EventArgs e)
		{
			try 
			{
				base.OnResize (e);
				LayoutProperties();
			} 
			catch (Exception e2) 
			{
				MainForm.ShowError(e2);
			}

		}


		private void box_MouseDown(object sender, MouseEventArgs e)
		{
			try 
			{
				if (e.Clicks == 2) 
				{
					PropertyComboBox box = null;
					if (sender is PropertyComboBox) 
						box = (PropertyComboBox)sender;
					else if (sender is Label)
						box = (PropertyComboBox)((Label)sender).Parent;
				
					if (box != null) 
					{
						if (MessageBox.Show(this,"Are you sure you want to remove the property " + box + "?",MainForm.DialogCaption,MessageBoxButtons.YesNo) == DialogResult.Yes)
							DeregisterProperty(box);
					}			
				}
			} 
			catch (Exception e2) 
			{
				MainForm.ShowError(e2);
			}

		}

        internal void Reset()
        {
            ArrayList copy = new ArrayList(propertyCombos);
            foreach (PropertyComboBox box in copy) {
                DeregisterProperty(box);
            }
        }

        internal void BeginEdit()
        {
            if (selectedSnippets.Count == 1) {
                snippetTitle.SelectAll();
                snippetTitle.Focus();
            
            }
        }

        internal List<Snippet> getProperties()
        {
            List<Snippet> retVal = new List<Snippet>();
            foreach (PropertyComboBox combo in propertyCombos) {
                retVal.Add(combo.getPropertySnippet());
            }
            return retVal;
        }
    }
}
