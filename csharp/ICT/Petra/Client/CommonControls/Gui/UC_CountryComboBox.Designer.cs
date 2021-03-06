//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       christiank
//
// Copyright 2004-2010 by OM International
//
// This file is part of OpenPetra.org.
//
// OpenPetra.org is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// OpenPetra.org is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with OpenPetra.org.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.Windows.Forms;
using System.Drawing.Printing;
using Ict.Common.Controls;
using Ict.Petra.Client.CommonControls;

namespace Ict.Petra.Client.CommonControls
{
    partial class TUC_CountryComboBox
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbCountry = new TCmbLabelled();
            this.SuspendLayout();

            //
            // cmbCountry
            //
            this.cmbCountry.Anchor = System.Windows.Forms.AnchorStyles.Top |
                                     System.Windows.Forms.AnchorStyles.Left |
                                     System.Windows.Forms.AnchorStyles.Right;
            this.cmbCountry.BackColor = System.Drawing.SystemColors.Control;
            this.cmbCountry.ColumnWidthCol1 = 50;
            this.cmbCountry.ColumnWidthCol2 = 200;
            this.cmbCountry.ColumnWidthCol3 = 0;
            this.cmbCountry.ColumnWidthCol4 = 0;
            this.cmbCountry.ComboBoxWidth = 50;
            this.cmbCountry.DisplayInColumn1 = "p_country_code_c";
            this.cmbCountry.DisplayInColumn2 = "p_country_name_c";
            this.cmbCountry.DisplayInColumn3 = null;
            this.cmbCountry.DisplayInColumn4 = null;
            this.cmbCountry.DisplayMember = "";
            this.cmbCountry.Font = new System.Drawing.Font("Verdana",
                8.25f,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                (Byte)0);
            this.cmbCountry.ImageColumn = 0;
            this.cmbCountry.Images = null;
            this.cmbCountry.LabelDisplaysColumn = "p_country_name_c";
            this.cmbCountry.Location = new System.Drawing.Point(0, 0);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(388, 21);
            this.cmbCountry.TabIndex = 0;
            this.cmbCountry.ValueMember = "";

            //
            // TUC_CountryComboBox
            //
            this.Controls.Add(this.cmbCountry);
            this.Name = "TUC_CountryComboBox";
            this.Size = new System.Drawing.Size(388, 22);
            this.ResumeLayout(false);
        }

        private TCmbLabelled cmbCountry;
    }
}