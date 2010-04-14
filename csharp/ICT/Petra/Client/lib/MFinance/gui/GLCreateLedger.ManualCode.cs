﻿/*************************************************************************
 *
 * DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
 *
 * @Authors:
 *       timop
 *
 * Copyright 2004-2009 by OM International
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
using System.Windows.Forms;
using Mono.Unix;
using Ict.Common.Verification;
using Ict.Petra.Client.App.Core.RemoteObjects;
using Ict.Petra.Shared.Interfaces.MFinance.GL.WebConnectors;

namespace Ict.Petra.Client.MFinance.Gui.Setup
{
    public partial class TFrmGLCreateLedger
    {
        private void InitializeManualCode()
        {
            // TODO: should we use an assistant window?
            // TODO: retention period
            // TODO: setup of gift system, AP, etc

            nudLedgerNumber.Maximum = 9999;
            nudLedgerNumber.Minimum = 1;
            nudLedgerNumber.Value = 99;
            dtpCalendarStartDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            nudNumberOfFwdPostingPeriods.Value = 8;
            nudNumberOfPeriods.Value = 12;
            nudCurrentPeriod.Value = DateTime.Now.Month;
            cmbBaseCurrency.SetSelectedString("EUR");
            cmbIntlCurrency.SetSelectedString("USD");
            cmbCountryCode.SetSelectedString("DE");
        }

        private void CreateLedger(System.Object sender, EventArgs e)
        {
            TVerificationResultCollection VerificationResult;

            if (!TRemote.MFinance.GL.WebConnectors.CreateNewLedger(
                    Convert.ToInt32(nudLedgerNumber.Value),
                    txtLedgerName.Text,
                    cmbCountryCode.GetSelectedString(),
                    cmbBaseCurrency.GetSelectedString(),
                    cmbIntlCurrency.GetSelectedString(),
                    dtpCalendarStartDate.Value,
                    Convert.ToInt32(nudNumberOfPeriods.Value),
                    Convert.ToInt32(nudCurrentPeriod.Value),
                    Convert.ToInt32(nudNumberOfFwdPostingPeriods.Value),
                    out VerificationResult))
            {
                if (VerificationResult != null)
                {
                    MessageBox.Show(
                        VerificationResult.GetVerificationResult(0).ResultText,
                        Catalog.GetString("Problem: No Ledger has been created"));
                }
                else
                {
                    MessageBox.Show(Catalog.GetString("Problem: No Ledger has been created"),
                        Catalog.GetString("Error"));
                }
            }
            else
            {
                MessageBox.Show(String.Format(Catalog.GetString(
                            "The ledger {0} ({1}) has been created successfully. Please assign permissions to the users in System Manager."),
                        txtLedgerName.Text,
                        nudLedgerNumber.Value),
                    Catalog.GetString("Success"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}