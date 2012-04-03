//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       wolfgangb
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Ict.Common;
using Ict.Common.Controls;
using Ict.Common.Data;
using Ict.Common.Remoting.Client;
using Ict.Common.Remoting.Shared;
using Ict.Common.Verification;
using Ict.Petra.Client.App.Core;
using Ict.Petra.Client.App.Core.RemoteObjects;
using Ict.Petra.Shared.Interfaces.MPartner.Partner.UIConnectors;
using Ict.Petra.Shared.Interfaces.MPartner.Partner;
using Ict.Petra.Shared.MPartner.Mailroom.Data;
using Ict.Petra.Shared.MPartner.Partner.Data;
using Ict.Petra.Shared.Interfaces.MPartner.Mailing.WebConnectors;
using Ict.Petra.Shared.MPartner;
using Ict.Petra.Client.App.Gui;
using Ict.Petra.Client.CommonForms;

namespace Ict.Petra.Client.MPartner.Gui.Extracts
{
    public partial class TFrmExtractMaster
    {
        /// <summary>holds the DataSet that contains most data that is used on the screen</summary>
        private ExtractTDS FMainDS = null;

        #region Public Methods

        /// <summary>
        /// Verify and if necessary update partner data in an extract
        /// </summary>
        public static void VerifyAndUpdateExtract(System.Windows.Forms.Form AForm, int AExtractId)
        {
            ExtractTDSMExtractTable ExtractTable;
            bool ChangesMade;

            // retrieve contents of extract from server
            ExtractTable = TRemote.MPartner.Partner.WebConnectors.GetExtractRowsWithPartnerData(AExtractId);

            VerifyAndUpdateExtract(AForm, ref ExtractTable, out ChangesMade);

            foreach (DataRow InspectDR in ExtractTable.Rows)
            {
                InspectDR.EndEdit();
            }

            TSubmitChangesResult SubmissionResult;
            TVerificationResultCollection VerificationResult;

            MExtractTable SubmitDT = new MExtractTable();

            if (ExtractTable.GetChangesTyped() != null)
            {
                SubmitDT.Merge(ExtractTable.GetChangesTyped());
            }

            if ((SubmitDT.Rows.Count == 0)
                || !ChangesMade)
            {
                MessageBox.Show(Catalog.GetString("Extract was already up to date"),
                    Catalog.GetString("Verify and Update Extract"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // return if no changes were made
                return;
            }

            // Submit changes to the PETRAServer
            SubmissionResult = TRemote.MPartner.Partner.WebConnectors.SaveExtract
                                   (AExtractId, ref SubmitDT, out VerificationResult);

            if (SubmissionResult == TSubmitChangesResult.scrError)
            {
                MessageBox.Show(Catalog.GetString("Verify and Update of Extract failed"),
                    Catalog.GetString("Verify and Update Extract"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(Catalog.GetString("Verification and Update of Extract was successful"),
                    Catalog.GetString("Verify and Update Extract"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Verify and if necessary update partner data in an extract
        /// </summary>
        public static void VerifyAndUpdateExtract(System.Windows.Forms.Form AForm,
            ref ExtractTDSMExtractTable AExtractTable,
            out bool AChangesMade)
        {
            bool AddressExists;
            bool AddressNeitherCurrentNorMailing;
            bool ReplaceAddress;
            bool ReplaceAddressYesToAll = false;
            bool ReplaceAddressNoToAll = false;
            string CountryName;
            string EmailAddress;
            PLocationTable LocationTable;
            PLocationRow LocationRow;
            PPartnerLocationTable PartnerLocationTable;
            TFrmExtendedMessageBox MsgBox = new TFrmExtendedMessageBox(AForm);

            TFrmExtendedMessageBox.TResult MsgBoxResult;
            bool DontShowPartnerRemovePartnerKeyNonExistent = false;
            bool DontShowReplaceAddress = false;
            bool DontShowPartnerRemoveNoAddress = false;


            // initialize output parameter
            AChangesMade = false;

            // build a collection of objects to be deleted before actually deleting them (as otherwise
            // indexes may not be valid any longer)
            List <ExtractTDSMExtractRow>RowsToDelete = new List <ExtractTDSMExtractRow>();

            // prepare mouse cursor so user knows something is happening
            AForm.Cursor = Cursors.WaitCursor;

            // look at every single extract row
            foreach (ExtractTDSMExtractRow Row in AExtractTable.Rows)
            {
                // initialize for this row
                ReplaceAddress = false;

                // check if the partner record still exists, otherwise remove from extract
                if (!TRemote.MPartner.Partner.ServerLookups.VerifyPartner(Row.PartnerKey))
                {
                    if (!DontShowPartnerRemovePartnerKeyNonExistent)
                    {
                        MsgBox.ShowDialog(String.Format(Catalog.GetString("The following partner record does not exist any longer and " +
                                    "will therefore be removed from this extract: \n\r\n\r" +
                                    "{0} ({1})"), Row.PartnerShortName, Row.PartnerKey),
                            Catalog.GetString("Verify and Update Extract"),
                            Catalog.GetString("Don't show this message again"),
                            TFrmExtendedMessageBox.TButtons.embbOK,
                            TFrmExtendedMessageBox.TIcon.embiInformation);
                        MsgBoxResult = MsgBox.GetResult(out DontShowPartnerRemovePartnerKeyNonExistent);
                    }

                    RowsToDelete.Add(Row);
                }
                else
                {
                    AddressExists = TRemote.MPartner.Partner.ServerLookups.VerifyPartnerAtLocation
                                        (Row.PartnerKey, new TLocationPK(Row.SiteKey, Row.LocationKey), out AddressNeitherCurrentNorMailing);

                    if (!AddressExists)
                    {
                        if (!DontShowReplaceAddress)
                        {
                            MsgBox.ShowDialog(String.Format(Catalog.GetString("Address for {0} ({1}) in this extract no longer exists and " +
                                        "will therefore be replaced with a current address"),
                                    Row.PartnerShortName, Row.PartnerKey),
                                Catalog.GetString("Verify and Update Extract"),
                                Catalog.GetString("Don't show this message again"),
                                TFrmExtendedMessageBox.TButtons.embbOK,
                                TFrmExtendedMessageBox.TIcon.embiInformation);
                            MsgBoxResult = MsgBox.GetResult(out DontShowReplaceAddress);
                        }

                        ReplaceAddress = true;
                    }
                    else if (AddressNeitherCurrentNorMailing)
                    {
                        if (!ReplaceAddressYesToAll
                            && !ReplaceAddressNoToAll)
                        {
                            MsgBoxResult =
                                MsgBox.ShowDialog(String.Format(Catalog.GetString("Address for {0} ({1}) in this extract is not current. " +
                                            "Do you want to update it with a current address if there is one?"),
                                        Row.PartnerShortName, Row.PartnerKey),
                                    Catalog.GetString("Verify and Update Extract"),
                                    "",
                                    TFrmExtendedMessageBox.TButtons.embbYesYesToAllNoNoToAll,
                                    TFrmExtendedMessageBox.TIcon.embiQuestion);

                            if (MsgBoxResult == TFrmExtendedMessageBox.TResult.embrYesToAll)
                            {
                                ReplaceAddressYesToAll = true;
                            }
                            else if (MsgBoxResult == TFrmExtendedMessageBox.TResult.embrYes)
                            {
                                ReplaceAddress = true;
                            }
                            else if (MsgBoxResult == TFrmExtendedMessageBox.TResult.embrNoToAll)
                            {
                                ReplaceAddressNoToAll = true;
                            }
                        }

                        // need to set the flag each time we come through here.
                        if (ReplaceAddressYesToAll)
                        {
                            ReplaceAddress = true;
                        }
                    }

                    if (ReplaceAddress)
                    {
                        if (!TRemote.MPartner.Mailing.WebConnectors.GetBestAddress(Row.PartnerKey,
                                out LocationTable, out PartnerLocationTable, out CountryName, out EmailAddress))
                        {
                            // in this case there is no address at all for this partner (should not really happen)
                            if (!DontShowPartnerRemoveNoAddress)
                            {
                                MsgBox.ShowDialog(String.Format(Catalog.GetString("No address could be found for {0} ({1}). " +
                                            "Therefore the partner record will be removed from this extract"),
                                        Row.PartnerShortName, Row.PartnerKey),
                                    Catalog.GetString("Verify and Update Extract"),
                                    Catalog.GetString("Don't show this message again"),
                                    TFrmExtendedMessageBox.TButtons.embbOK,
                                    TFrmExtendedMessageBox.TIcon.embiInformation);
                                MsgBoxResult = MsgBox.GetResult(out DontShowPartnerRemoveNoAddress);
                            }

                            RowsToDelete.Add(Row);
                        }
                        else
                        {
                            if (LocationTable.Rows.Count > 0)
                            {
                                LocationRow = (PLocationRow)LocationTable.Rows[0];

                                /* it could be that GetBestAddress still returns a non-current address if
                                 * there is no better one */
                                if ((Row.SiteKey != LocationRow.SiteKey)
                                    || (Row.LocationKey != LocationRow.LocationKey))
                                {
                                    AChangesMade = true;

                                    Row.SiteKey = LocationRow.SiteKey;
                                    Row.LocationKey = LocationRow.LocationKey;
                                }
                            }
                        }
                    }
                }
            }

            // now delete the actual rows
            foreach (ExtractTDSMExtractRow Row in RowsToDelete)
            {
                AChangesMade = true;
                Row.Delete();
            }

            // prepare mouse cursor so user knows something is happening
            AForm.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Loads Partner Types Data from Petra Server into FMainDS.
        /// </summary>
        /// <returns>true if successful, otherwise false.</returns>
        public Boolean LoadData()
        {
            Boolean ReturnValue = true;

            return ReturnValue;
        }

        /// <summary>
        /// save the changes on the screen (code is copied from auto-generated code)
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return ucoExtractMasterList.SaveChanges();
        }

        /// <summary>
        /// react to menu item / save button
        /// </summary>
        public void FileSave(System.Object sender, EventArgs e)
        {
            SaveChanges();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///
        /// </summary>
        private void InitializeManualCode()
        {
        }

        /// <summary>
        ///
        /// </summary>
        private void ShowDataManual()
        {
        }

        /// <summary>
        /// Open screen to maintain contents of an extract
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaintainExtract(System.Object sender, EventArgs e)
        {
            ucoExtractMasterList.MaintainExtract(sender, e);
        }

        /// <summary>
        /// Verify and if necessary update partner data in an extract
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerifyAndUpdateExtract(System.Object sender, EventArgs e)
        {
            ucoExtractMasterList.VerifyAndUpdateExtract(sender, e);
        }

        #endregion
    }
}