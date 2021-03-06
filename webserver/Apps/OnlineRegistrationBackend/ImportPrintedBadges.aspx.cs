﻿//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       timop
//
// Copyright 2004-2011 by OM International
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
using System.Collections.Generic;
using System.Data;
using System.IO;
using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Ext.Net;
using Ict.Common;
using Ict.Common.IO;
using Ict.Common.Verification;
using PetraWebService;
using Ict.Petra.Shared;
using Ict.Petra.Server.MConference.Applications;

namespace Ict.Petra.WebServer.MConference
{
    public partial class TImportPrintedBadgesUI : System.Web.UI.Page
    {
        protected Ext.Net.TextArea RegistrationsKeys;
        protected Ext.Net.FormPanel PrintedBadgesForm;

        protected void Page_Load(object sender, EventArgs e)
        {
            // check for valid user
            TOpenPetraOrg myServer = new TOpenPetraOrg();

            if (!myServer.IsUserLoggedIn())
            {
                this.Response.Redirect("Default.aspx");
                return;
            }
        }

        protected void ImportPrintedBadges(Object sender, DirectEventArgs e)
        {
            Dictionary <string, string>values = JSON.Deserialize <Dictionary <string, string>>(e.ExtraParams["Values"]);
            string EnteredValues = values["RegistrationsKeys"].ToString().Trim();

            TImportPrintedBadges.ImportPrintedBadges(
                EnteredValues,
                TAppSettingsManager.GetInt64("ConferenceTool.EventPartnerKey"),
                TAppSettingsManager.GetValue("ConferenceTool.EventCode"),
                -1);
        }
    }
}