// auto generated with nant generateWinforms from IncomeExpenseStmt.yaml
//
// DO NOT edit manually, DO NOT edit with the designer
//
//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       auto generated
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Mono.Unix;
using Ict.Petra.Shared;
using Ict.Petra.Shared.MReporting;
using System.Resources;
using System.Collections.Specialized;
using Ict.Common;
using Ict.Common.Verification;
using Ict.Petra.Client.App.Core;
using Ict.Common.Controls;
using Ict.Petra.Client.CommonForms;
using Ict.Petra.Client.MReporting.Logic;

namespace Ict.Petra.Client.MReporting.Gui.MFinance
{

  /// <summary>
  /// auto generated class for report
  /// </summary>
  public partial class TFrmIncomeExpenseStmt: System.Windows.Forms.Form, IFrmReporting
  {
    private TFrmPetraReportingUtils FPetraUtilsObject;

    /// <summary>
    /// constructor
    /// </summary>
    public TFrmIncomeExpenseStmt(IntPtr AParentFormHandle) : base()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
      #region CATALOGI18N

      // this code has been inserted by GenerateI18N, all changes in this region will be overwritten by GenerateI18N
      this.tpgGeneralSettings.Text = Catalog.GetString("General Settings");
      this.tpgCostCentres.Text = Catalog.GetString("CostCentre Settings");
      this.tpgColumnSettings.Text = Catalog.GetString("Column Settings");
      this.rbtCurrencyComplete.Text = Catalog.GetString("Complete");
      this.rbtCurrencyWithoutDecimals.Text = Catalog.GetString("Without decimals");
      this.rbtCurrencyThousands.Text = Catalog.GetString("Only Thousands");
      this.rgrFormatCurrency.Text = Catalog.GetString("Format currency numbers:");
      this.tpgAdditionalSettings.Text = Catalog.GetString("Additional Settings");
      this.tbbGenerateReport.ToolTipText = Catalog.GetString("Generate the report");
      this.tbbGenerateReport.Text = Catalog.GetString("&Generate");
      this.tbbSaveSettings.Text = Catalog.GetString("&Save Settings");
      this.tbbSaveSettingsAs.Text = Catalog.GetString("Save Settings &As...");
      this.tbbLoadSettingsDialog.Text = Catalog.GetString("&Open...");
      this.mniLoadSettingsDialog.Text = Catalog.GetString("&Open...");
      this.mniLoadSettings1.Text = Catalog.GetString("RecentSettings");
      this.mniLoadSettings2.Text = Catalog.GetString("RecentSettings");
      this.mniLoadSettings3.Text = Catalog.GetString("RecentSettings");
      this.mniLoadSettings4.Text = Catalog.GetString("RecentSettings");
      this.mniLoadSettings5.Text = Catalog.GetString("RecentSettings");
      this.mniLoadSettings.Text = Catalog.GetString("&Load Settings");
      this.mniSaveSettings.Text = Catalog.GetString("&Save Settings");
      this.mniSaveSettingsAs.Text = Catalog.GetString("Save Settings &As...");
      this.mniMaintainSettings.Text = Catalog.GetString("&Maintain Settings...");
      this.mniWrapColumn.Text = Catalog.GetString("&Wrap Columns");
      this.mniGenerateReport.ToolTipText = Catalog.GetString("Generate the report");
      this.mniGenerateReport.Text = Catalog.GetString("&Generate");
      this.mniClose.ToolTipText = Catalog.GetString("Closes this window");
      this.mniClose.Text = Catalog.GetString("&Close");
      this.mniFile.Text = Catalog.GetString("&File");
      this.mniHelpPetraHelp.Text = Catalog.GetString("&Petra Help");
      this.mniHelpBugReport.Text = Catalog.GetString("Bug &Report");
      this.mniHelpAboutPetra.Text = Catalog.GetString("&About Petra");
      this.mniHelpDevelopmentTeam.Text = Catalog.GetString("&The Development Team...");
      this.mniHelp.Text = Catalog.GetString("&Help");
      this.Text = Catalog.GetString("Income Expense Statement");
      #endregion

      FPetraUtilsObject = new TFrmPetraReportingUtils(AParentFormHandle, this, stbMain);

      FPetraUtilsObject.FXMLFiles = "Finance\\\\incomeexpensestatement.xml,Finance\\\\finance.xml,common.xml";
      FPetraUtilsObject.FReportName = "Income Expense Statement";
      FPetraUtilsObject.FCurrentReport = "Income Expense Statement";
      FPetraUtilsObject.FSettingsDirectory = "Finance";

      // Hook up Event that is fired by ucoReportColumns
      // ucoReportColumns.FillColumnGridEventHandler += new TFillColumnGridEventHandler(FPetraUtilsObject.FillColumnGrid);
      FPetraUtilsObject.InitialiseData("");
      // FPetraUtilsObject.InitialiseSettingsGui(ucoReportColumns, mniLoadSettings, /*ConMnuLoadSettings*/null,
      //                                 mniSaveSettings, mniSaveSettingsAs, mniLoadSettingsDialog, mniMaintainSettings);
      this.SetAvailableFunctions();

      uco_GeneralSettings.InitialiseData(FPetraUtilsObject);
      uco_CostCentreSettings.InitialiseData(FPetraUtilsObject);
      uco_FinanceColumns.InitialiseData(FPetraUtilsObject);

      uco_GeneralSettings.PetraUtilsObject = FPetraUtilsObject;
      uco_GeneralSettings.InitUserControl();
      uco_CostCentreSettings.PetraUtilsObject = FPetraUtilsObject;
      uco_CostCentreSettings.InitUserControl();
      uco_FinanceColumns.PetraUtilsObject = FPetraUtilsObject;
      uco_FinanceColumns.InitUserControl();
      uco_FinanceColumns.SetVisibility(true, false);

      FPetraUtilsObject.LoadDefaultSettings();
    }

    private void TFrmPetra_Activated(object sender, EventArgs e)
    {
        FPetraUtilsObject.TFrmPetra_Activated(sender, e);
    }

    private void TFrmPetra_Load(object sender, EventArgs e)
    {
        FPetraUtilsObject.TFrmPetra_Load(sender, e);
    }

    private void TFrmPetra_Closing(object sender, CancelEventArgs e)
    {
        FPetraUtilsObject.TFrmPetra_Closing(sender, e);
    }

    private void Form_KeyDown(object sender, KeyEventArgs e)
    {
        FPetraUtilsObject.Form_KeyDown(sender, e);
    }

    private void TFrmPetra_Closed(object sender, EventArgs e)
    {
    }
#region Parameter/Settings Handling
    /**
       Reads the selected values from the controls, and stores them into the parameter system of FCalculator

    */
    public void ReadControls(TRptCalculator ACalc, TReportActionEnum AReportAction)
    {
      ACalc.SetMaxDisplayColumns(FPetraUtilsObject.FMaxDisplayColumns);

      uco_GeneralSettings.ReadControls(ACalc, AReportAction);
      uco_CostCentreSettings.ReadControls(ACalc, AReportAction);
      uco_FinanceColumns.ReadControls(ACalc, AReportAction);
      if (rbtCurrencyComplete.Checked)
      {
          ACalc.AddParameter("param_currency_format", "CurrencyComplete");
      }
      if (rbtCurrencyWithoutDecimals.Checked)
      {
          ACalc.AddParameter("param_currency_format", "CurrencyWithoutDecimals");
      }
      if (rbtCurrencyThousands.Checked)
      {
          ACalc.AddParameter("param_currency_format", "CurrencyThousands");
      }
      ReadControlsManual(ACalc, AReportAction);

    }

    /**
       Sets the selected values in the controls, using the parameters loaded from a file

    */
    public void SetControls(TParameterList AParameters)
    {

      uco_GeneralSettings.SetControls(AParameters);
      uco_CostCentreSettings.SetControls(AParameters);
      uco_FinanceColumns.SetControls(AParameters);
      rbtCurrencyComplete.Checked = AParameters.Get("param_currency_format").ToString() == "CurrencyComplete";
      rbtCurrencyWithoutDecimals.Checked = AParameters.Get("param_currency_format").ToString() == "CurrencyWithoutDecimals";
      rbtCurrencyThousands.Checked = AParameters.Get("param_currency_format").ToString() == "CurrencyThousands";
      SetControlsManual(AParameters);
    }
#endregion

#region Column Functions and Calculations
    /**
       This will add functions to the list of available functions

    */
    public void SetAvailableFunctions()
    {
      //ArrayList availableFunctions = FPetraUtilsObject.InitAvailableFunctions();

      FPetraUtilsObject.AddAvailableFunction(new TFinanceColumnFunction("Actual Selected Year", 0));
      FPetraUtilsObject.AddAvailableFunction(new TFinanceColumnFunction("Actual Previous Year", 0));
      FPetraUtilsObject.AddAvailableFunction(new TFinanceColumnFunction("Actual End of Year", false, true));
      FPetraUtilsObject.AddAvailableFunction(new TFinanceColumnFunction("Variance", 2));
      FPetraUtilsObject.AddAvailableFunction(new TFinanceColumnFunction("Variance %", 2));
      FPetraUtilsObject.AddAvailableFunction(new TFinanceColumnFunction("Account %", 1));
      FPetraUtilsObject.AddAvailableFunction(new TFinanceColumnFunction("Account % INC/EXP", 1));
      FPetraUtilsObject.AddAvailableFunction(new TFinanceColumnFunction("Budget Previous Year", 0));
      FPetraUtilsObject.AddAvailableFunction(new TFinanceColumnFunction("Budget Selected Year", 0));
      FPetraUtilsObject.AddAvailableFunction(new TFinanceColumnFunction("Budget Whole Selected Year", false, true));
      FPetraUtilsObject.AddAvailableFunction(new TFinanceColumnFunction("Budget Next Year", 0));

      uco_GeneralSettings.SetAvailableFunctions(FPetraUtilsObject.GetAvailableFunctions());
      uco_CostCentreSettings.SetAvailableFunctions(FPetraUtilsObject.GetAvailableFunctions());
      uco_FinanceColumns.SetAvailableFunctions(FPetraUtilsObject.GetAvailableFunctions());

    }
#endregion

#region Implement interface functions

    /// <summary>
    /// only run this code once during activation
    /// </summary>
    public void RunOnceOnActivation()
    {
    }

    /// <summary>
    /// Adds event handlers for the appropiate onChange event to call a central procedure
    /// </summary>
    /// <returns>void</returns>
    public void HookupAllControls()
    {
    }

    /// <summary>
    /// check if report window can be closed
    /// </summary>
    public bool CanClose()
    {
        return FPetraUtilsObject.CanClose();
    }

    /// <summary>
    /// access to the utility object
    /// </summary>
    public TFrmPetraUtils GetPetraUtilsObject()
    {
        return (TFrmPetraUtils)FPetraUtilsObject;
    }

    /// <summary>
    /// initialisation
    /// </summary>
    /// <param name="AReportParameter">Initialisation values needed for some reports</param>
    public void InitialiseData(String AReportParameter)
    {
        FPetraUtilsObject.InitialiseData(AReportParameter);
    }

    /// <summary>
    /// Checks / Unchecks the menu item "Wrap Columns"
    /// </summary>
    /// <param name="ACheck">True if menu item is to be checked. Otherwise false</param>
    public void CheckWrapColumnMenuItem(bool ACheck)
    {
        this.mniWrapColumn.Checked = ACheck;
    }
#endregion

    /// <summary>
    /// allow to store and load settings
    /// </summary>
    /// <param name="AEnabled">True if the store and load settings are to be enabled.</param>
    public void EnableSettings(bool AEnabled)
    {
        foreach (ToolStripItem item in mniLoadSettings.DropDownItems)
        {
            item.Enabled = AEnabled;
        }
        mniLoadSettings.Enabled = AEnabled;
        mniSaveSettings.Enabled = AEnabled;
        mniSaveSettingsAs.Enabled = AEnabled;
        mniMaintainSettings.Enabled = AEnabled;
        //tbbLoadSettings.Enabled = AEnabled;
        tbbSaveSettings.Enabled = AEnabled;
        tbbSaveSettingsAs.Enabled = AEnabled;
    }

    /// <summary>
    /// activate and deactivate toolbar buttons and menu items depending on ongoing report calculation
    /// </summary>
    /// <param name="ABusy">True if a report is generated and the close button should be disabled.</param>
    public void EnableBusy(bool ABusy)
    {
        mniClose.Enabled = !ABusy;

        if (ABusy == false)
        {
            mniGenerateReport.Text = "&Generate Report...";
            tbbGenerateReport.Text = "Generate";
            tbbGenerateReport.ToolTipText = "Generate a report and display the preview";
        }
        else
        {
            mniGenerateReport.Text = "&Cancel Report";
            tbbGenerateReport.Text = "Cancel";
            tbbGenerateReport.ToolTipText = "Cancel the calculation of the report (after cancelling it might still take a while)";
        }
    }

    /// <summary>
    /// this is used for writing the captions of the menu items and toolbar buttons for recently used report settings
    /// </summary>
    /// <returns>false if an item with that index does not exist</returns>
    /// <param name="AIndex"></param>
    /// <param name="mniItem"></param>
    /// <param name="tbbItem"></param>
    public bool GetRecentSettingsItems(int AIndex, out ToolStripItem mniItem, out ToolStripItem tbbItem)
    {
        if (AIndex < 0 || AIndex >= mniLoadSettings.DropDownItems.Count - 2)
        {
            mniItem = null;
            tbbItem = null;
            return false;
        }
        mniItem = mniLoadSettings.DropDownItems[AIndex + 2];
        // TODO
        tbbItem = null;
        return true;
    }

#region Action Handling

    /// auto generated
    protected void actClose(object sender, EventArgs e)
    {
        FPetraUtilsObject.ExecuteAction(eActionId.eClose);
    }

    /// auto generated
    protected void actGenerateReport(object sender, EventArgs e)
    {
        FPetraUtilsObject.MI_GenerateReport_Click(sender, e);
    }

    /// auto generated
    protected void actSaveSettingsAs(object sender, EventArgs e)
    {
        FPetraUtilsObject.MI_SaveSettingsAs_Click(sender, e);
    }

    /// auto generated
    protected void actSaveSettings(object sender, EventArgs e)
    {
        FPetraUtilsObject.MI_SaveSettings_Click(sender, e);
    }

    /// auto generated
    protected void actLoadSettingsDialog(object sender, EventArgs e)
    {
        FPetraUtilsObject.MI_LoadSettingsDialog_Click(sender, e);
    }

    /// auto generated
    protected void actLoadSettings(object sender, EventArgs e)
    {
        FPetraUtilsObject.MI_LoadSettings_Click(sender, e);
    }

    /// auto generated
    protected void actMaintainSettings(object sender, EventArgs e)
    {
        FPetraUtilsObject.MI_MaintainSettings_Click(sender, e);
    }

    /// auto generated
    protected void actWrapColumn(object sender, EventArgs e)
    {
        FPetraUtilsObject.MI_WrapColumn_Click(sender, e);
    }

#endregion
  }
}
