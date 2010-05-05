﻿/*************************************************************************
 *
 * DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
 *
 * @Authors:
 *       timop
 *
 * Copyright 2004-2010 by OM International
 *
 * This file is part of OpenPetra.org.
 *
 * OpenPetra.org is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * OpenPetra.org is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with OpenPetra.org.  If not, see <http://www.gnu.org/licenses/>.
 *
 ************************************************************************/
using System;
using System.Xml;
using System.Data;
using System.Windows.Forms;
using System.Collections.Specialized;
using Mono.Unix;
using Ict.Common;
using Ict.Common.IO;
using Ict.Petra.Shared.MPartner;
using Ict.Petra.Shared.MPartner.Partner.Data;
using Ict.Petra.Client.App.Gui;
using Ict.Petra.Client.App.Core.RemoteObjects;

namespace Ict.Petra.Client.MPartner.Gui
{
    public partial class TFrmPartnerImport
    {
        /// <summary>
        /// todoComment
        /// </summary>
        public bool SaveChanges()
        {
            // TODO
            return false;
        }

        /// <summary>
        /// todoComment
        /// </summary>
        public void FileSave(System.Object sender, EventArgs e)
        {
            SaveChanges();
        }

        XmlNode FCurrentPartnerNode = null;
        private void OpenFile(System.Object sender, EventArgs e)
        {
            if (!FPetraUtilsObject.IsEnabled("actStartImport"))
            {
                MessageBox.Show(Catalog.GetString("Please cancel the current import before selecting a different file"));
                return;
            }

            string filename;

            XmlDocument doc = TImportExportDialogs.ImportWithDialog(Catalog.GetString("Select the file for importing partners"), out filename);

            if (doc != null)
            {
                txtFilename.Text = filename;

                XmlNode root = doc.FirstChild.NextSibling;
                FCurrentPartnerNode = root.FirstChild;
            }
        }

        private void StartImport(Object sender, EventArgs e)
        {
            // TODO check for import settings, which partners to skip etc

            DataTable ValuePairs = new DataTable();

            ValuePairs.Columns.Add(new DataColumn("Attribute", typeof(string)));
            ValuePairs.Columns.Add(new DataColumn("Value", typeof(string)));

            if (FCurrentPartnerNode != null)
            {
                foreach (XmlAttribute attr in FCurrentPartnerNode.Attributes)
                {
                    DataRow valuePair = ValuePairs.NewRow();
                    valuePair["Attribute"] = attr.Name;
                    valuePair["Value"] = attr.Value;
                    ValuePairs.Rows.Add(valuePair);
                }
            }

            grdParsedValues.Columns.Clear();
            grdParsedValues.AddTextColumn(Catalog.GetString("Attribute"), ValuePairs.Columns["Attribute"], 150);
            grdParsedValues.AddTextColumn(Catalog.GetString("Value"), ValuePairs.Columns["Value"], 150);
            ValuePairs.DefaultView.AllowNew = false;
            grdParsedValues.DataSource = new DevAge.ComponentModel.BoundDataView(ValuePairs.DefaultView);

            this.FPetraUtilsObject.EnableAction("actStartImport", false);

            // TODO: get SimplePartnerFindTDS with all matching persons and families (family name, city)

            PartnerFindTDS result = TRemote.MPartner.Partner.WebConnectors.FindPartners("", "Pok", "", new StringCollection());

            grdMatchingRecords.Columns.Clear();
            grdMatchingRecords.AddTextColumn(Catalog.GetString("Class"), result.SearchResult.ColumnPartnerClass, 50);
            grdMatchingRecords.AddTextColumn(Catalog.GetString("Name"), result.SearchResult.ColumnPartnerShortName, 200);
            grdMatchingRecords.AddTextColumn(Catalog.GetString("Address"), result.SearchResult.ColumnAddress3, 200);
            grdMatchingRecords.AddTextColumn(Catalog.GetString("City"), result.SearchResult.ColumnCity, 150);
            result.SearchResult.DefaultView.AllowNew = false;
            grdMatchingRecords.DataSource = new DevAge.ComponentModel.BoundDataView(result.SearchResult.DefaultView);
        }

        private void CancelImport(Object sender, EventArgs e)
        {
            // TODO
        }
    }
}