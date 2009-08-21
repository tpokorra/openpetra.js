﻿/*
 * Contains classes for data verifications that are needed both on Server and
 * Client side.
 *
 * @Comment None of the data verifications in here must access the database
 *   since the Client doesn't have access to the database!
 *
 * @Comment NOTES on C# conversion: We dropped the 'Main' sub-namespace and put
 *   the 'SetColumnErrorText' function into a new Class 'Data'.
 *
 * @Author  christiank
 *
 */
using System;
using System.Data;
using System.Collections;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Ict.Common.Verification
{
    /// <summary>
    /// a verification error can either be critical or non critical
    /// </summary>
    public enum TResultSeverity
    {
        /// <summary>
        /// the verification failed
        /// </summary>
        Resv_Critical,

        /// <summary>
        /// verification warning
        /// </summary>
        Resv_Noncritical
    };

    /// <summary>
    /// A TVerificationResult object stores information about failed data
    /// verification and is passed (serialised) from the Server to the Client.
    /// It is made to be stored in the TVerificationResultCollection.
    /// </summary>
    [Serializable]
    public class TVerificationResult
    {
        /// <summary>DB Field or other context that describes where the data verification failed (use '[ODBC ...]' instead to signal a database error (such as a failed call to a stored procedure)</summary>
        public String FResultContext;

        /// <summary>Verification failure explanation</summary>
        public String FResultText;

        /// <summary>Verification failure caption</summary>
        public String FResultTextCaption;

        /// <summary>Error code if verification failure</summary>
        public String FResultCode;

        /// <summary>Signals whether the verification failure prevented saving of data (critical) or the verification result is only for information purposes (noncritical).</summary>
        public TResultSeverity FResultSeverity;

        /// <summary>
        /// We need this constructor so that inherited Classes can get by not having a default constructor...
        /// </summary>
        protected internal TVerificationResult()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="AResultContext">context where this verification happens (e.g. DB field name)</param>
        /// <param name="AResultText">Verification failure explanation</param>
        /// <param name="AResultSeverity">is this an error or just a warning</param>
        public TVerificationResult(String AResultContext, String AResultText, TResultSeverity AResultSeverity)
        {
            FResultContext = AResultContext;
            FResultText = AResultText;
            FResultSeverity = AResultSeverity;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="AResultContext">context where this verification happens (e.g. DB field name)</param>
        /// <param name="AResultText">Verification failure explanation</param>
        /// <param name="AResultCaption">caption for message box</param>
        /// <param name="AResultCode">a result code to identify error messages</param>
        /// <param name="AResultSeverity">is this an error or just a warning</param>
        public TVerificationResult(String AResultContext,
            String AResultText,
            String AResultCaption,
            String AResultCode,
            TResultSeverity AResultSeverity)
        {
            FResultContext = AResultContext;
            FResultText = AResultText;
            FResultTextCaption = AResultCaption;
            FResultCode = AResultCode;
            FResultSeverity = AResultSeverity;
        }

        /// <summary>
        /// get the context
        /// </summary>
        /// <returns></returns>
        public object ResultContext()
        {
            return FResultContext;
        }

        /// <summary>
        /// get the explanation for the verification failure
        /// </summary>
        /// <returns></returns>
        public String ResultText()
        {
            return FResultText;
        }
    }


    /// <summary>
    /// A TScreenVerificationResult object stores information about failed data
    /// verification in a Form or UserControl on the Client side.
    /// It is made to be stored in the TVerificationResultCollection.
    /// </summary>
    public class TScreenVerificationResult : TVerificationResult
    {
        /// <summary>
        /// the context for the verification; here of type System.Object
        /// </summary>
        public new System.Object FResultContext;

        /// <summary>
        /// specify the column where the error verification happened
        /// </summary>
        public DataColumn FResultColumn;

        /// <summary>
        /// specify the control that failed the verification
        /// </summary>
        public Control FResultControl;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="AResultContext">context of verification</param>
        /// <param name="AResultColumn">which column failed</param>
        /// <param name="AResultText">description and error message for the user</param>
        /// <param name="AResultControl">which control is involved</param>
        /// <param name="AResultSeverity">is this serious, or just a warning</param>
        public TScreenVerificationResult(object AResultContext,
            DataColumn AResultColumn,
            String AResultText,
            Control AResultControl,
            TResultSeverity AResultSeverity)
        {
            FResultContext = AResultContext;
            FResultColumn = AResultColumn;
            FResultText = AResultText;
            FResultSeverity = AResultSeverity;
            FResultControl = AResultControl;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="AResultContext">context of verification</param>
        /// <param name="AResultColumn">which column failed</param>
        /// <param name="AResultText">description and error message for the user</param>
        /// <param name="AResultCaption">caption for error message box</param>
        /// <param name="AResultCode">error code to identify the error message</param>
        /// <param name="AResultControl">which control is involved</param>
        /// <param name="AResultSeverity">is this serious, or just a warning</param>
        public TScreenVerificationResult(object AResultContext,
            DataColumn AResultColumn,
            String AResultText,
            String AResultCaption,
            String AResultCode,
            Control AResultControl,
            TResultSeverity AResultSeverity)
        {
            FResultContext = AResultContext;
            FResultColumn = AResultColumn;
            FResultText = AResultText;
            FResultTextCaption = AResultCaption;
            FResultCode = AResultCode;
            FResultControl = AResultControl;
            FResultSeverity = AResultSeverity;
        }

        /// <summary>
        /// the context of the verification failure
        /// </summary>
        /// <returns></returns>
        public new object ResultContext()
        {
            return FResultContext;
        }

        /// <summary>
        /// get the explanation for the verification failure
        /// </summary>
        /// <returns></returns>
        public new String ResultText()
        {
            return FResultText;
        }
    }


    /// <summary>
    /// A TVerificationResult object stores any number of TVerificationResult objects.
    /// With this strongly-typed Collection it is for instance possible to perform
    /// several data verification steps on the Server and pass the results back to
    /// the Client in one object.
    ///
    /// NOTES on C# conversion:
    /// (1) The 'Item' method overloads have been renamed to 'FindBy' method
    /// overloads (couldn't do that as in Delphi.NET!);
    /// (2) The 'VerificationResultInfo' Indexed Property that we had in .NET has
    /// now become the Default Indexed Property of this Class because C# doesn't
    /// allow named Indexed Properties!
    /// </summary>
    [Serializable]
    public class TVerificationResultCollection : CollectionBase
    {
        /// <summary>
        /// constructor
        /// </summary>
        public TVerificationResultCollection()
        {
        }

        /// <summary>
        /// access the elements of the verification collection
        /// </summary>
        public TVerificationResult this[int index]
        {
            get
            {
                return GetVerificationResult(index);
            }

            set
            {
                SetVerificationResult(index, value);
            }
        }

        /// <summary>
        /// add a new verification object
        /// </summary>
        /// <param name="value">the verification object to be added</param>
        /// <returns></returns>
        public int Add(TVerificationResult value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// merge another verification collection into the current collection
        /// </summary>
        /// <param name="value">collection to be merged</param>
        public void AddCollection(TVerificationResultCollection value)
        {
            for (int Counter = 0; Counter <= value.Count - 1; Counter += 1)
            {
                List.Add(value.GetVerificationResult(Counter));
            }
        }

        /// <summary>
        /// check if there is any verification that shows a critical error
        /// </summary>
        /// <returns></returns>
        public bool HasCriticalError()
        {
            foreach (TVerificationResult v in List)
            {
                if (v.FResultSeverity == TResultSeverity.Resv_Critical)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// generate the text for a message box showing all verification errors
        /// </summary>
        /// <param name="AErrorMessages">will have the list of error messages</param>
        /// <param name="AFirstErrorControl">for focusing the first control that caused verification failure</param>
        /// <param name="AFirstErrorContext">context of the first error</param>
        public void BuildScreenVerificationResultList(out String AErrorMessages, out Control AFirstErrorControl, out object AFirstErrorContext)
        {
            TScreenVerificationResult si;

            AFirstErrorControl = null;
            AErrorMessages = "";
            AFirstErrorContext = null;

            for (int Counter = 0; Counter <= Count - 1; Counter += 1)
            {
                si = (TScreenVerificationResult)(List[Counter]);
                AErrorMessages = AErrorMessages + si.FResultText + Environment.NewLine + Environment.NewLine;

                if (AFirstErrorControl == null)
                {
                    AFirstErrorControl = si.FResultControl;
                    AFirstErrorContext = si.FResultContext;
                }
            }
        }

        /// <summary>
        /// generate the text for a message box showing all verification errors of a given context
        /// </summary>
        /// <param name="AResultContext">only show errors of the given context</param>
        /// <param name="AErrorMessages">will have the list of error messages</param>
        /// <param name="AFirstErrorControl">for focusing the first control that caused verification failure</param>
        public void BuildScreenVerificationResultList(object AResultContext, out String AErrorMessages, out Control AFirstErrorControl)
        {
            TScreenVerificationResult si;

            AFirstErrorControl = null;
            AErrorMessages = "";

            for (int Counter = 0; Counter <= Count - 1; Counter += 1)
            {
                si = (TScreenVerificationResult)(List[Counter]);

                if (si.ResultContext() == AResultContext)
                {
                    AErrorMessages = AErrorMessages + si.FResultText + Environment.NewLine + Environment.NewLine;

                    if (AFirstErrorControl == null)
                    {
                        AFirstErrorControl = si.FResultControl;
                    }
                }
            }
        }

        /// <summary>
        /// check if the Verification list contains this value already
        /// </summary>
        /// <param name="value">check list for this value</param>
        /// <returns>true if the value is already there</returns>
        public bool Contains(TVerificationResult value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// check if the verification list already contains a message from the given context
        /// </summary>
        /// <param name="AResultContext">check list for this value</param>
        /// <returns>true if the value is already there</returns>
        /// <returns>true if an error from this context is already there</returns>
        public bool Contains(object AResultContext)
        {
            TVerificationResult si;
            Boolean Found = false;

            for (int Counter = 0; Counter <= Count - 1; Counter += 1)
            {
                si = (TVerificationResult)(List[Counter]);

                if (si.ResultContext() == AResultContext)
                {
                    Found = true;
                    break;
                }
            }

            return Found;
        }

        /// <summary>
        /// check if there is an error for this data column already
        /// </summary>
        /// <param name="AResultColumn">the column to check for</param>
        /// <returns>true if such an error already is part of the list</returns>
        public bool Contains(DataColumn AResultColumn)
        {
            TScreenVerificationResult si;
            Boolean Found = false;

            for (int Counter = 0; Counter <= Count - 1; Counter += 1)
            {
                si = (TScreenVerificationResult)(List[Counter]);

                if (si.FResultColumn == AResultColumn)
                {
                    Found = true;
                    break;
                }
            }

            return Found;
        }

        /// <summary>
        /// access result by index
        /// </summary>
        /// <param name="Index">which result should be returned</param>
        /// <returns>the selected result</returns>
        public TVerificationResult GetVerificationResult(int Index)
        {
            return (TVerificationResult)List[Index];
        }

        /// <summary>
        /// assign a specified verification result in the list
        /// </summary>
        /// <param name="Index">index to change the verification result</param>
        /// <param name="Value">the new value</param>
        public void SetVerificationResult(int Index, TVerificationResult Value)
        {
            List[Index] = Value;
        }

        /// <summary>
        /// find the index of the given value
        /// </summary>
        /// <param name="value">value to look for</param>
        /// <returns>index of the value</returns>
        public int IndexOf(TVerificationResult value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// insert a new value at the given position
        /// </summary>
        /// <param name="index">position to insert after</param>
        /// <param name="value">value to add</param>
        public void Insert(int index, TVerificationResult value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// find result by column
        /// </summary>
        /// <param name="AResultColumn">column to look for</param>
        /// <returns>the first result for that column</returns>
        public TScreenVerificationResult FindBy(DataColumn AResultColumn)
        {
            TScreenVerificationResult ReturnValue;
            TScreenVerificationResult si;

            ReturnValue = null;

            for (int Counter = 0; Counter <= Count - 1; Counter += 1)
            {
                si = (TScreenVerificationResult)(List[Counter]);

                if (si.FResultColumn == AResultColumn)
                {
                    ReturnValue = si;
                    break;
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// find result by context
        /// </summary>
        /// <param name="AResultContext">context to look for</param>
        /// <returns>the first result for that context</returns>
        public TVerificationResult FindBy(object AResultContext)
        {
            TVerificationResult ReturnValue;
            TVerificationResult si;

            ReturnValue = null;

            for (int Counter = 0; Counter <= Count - 1; Counter += 1)
            {
                si = (TVerificationResult)(List[Counter]);

                if (si.ResultContext() == AResultContext)
                {
                    ReturnValue = si;
                    break;
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// find result by index
        /// </summary>
        /// <param name="index">index to identify the result</param>
        /// <returns>the result</returns>
        public TVerificationResult FindBy(int index)
        {
            return (TVerificationResult)(List[index]);
        }

        /// <summary>
        /// Type checking events
        /// </summary>
        /// <returns>void</returns>
        private new void OnInsert(int index, object value)
        {
            VerifyType(value);
        }

        private new void OnSet(int index, object oldValue, object newValue)
        {
            VerifyType(newValue);
        }

        private new void OnValidate(object value)
        {
            VerifyType(value);
        }

        /// <summary>
        /// print the results in the collection to the System Console
        /// </summary>
        public void PrintItems()
        {
            const String fmt = "ResultField: {0}, ResultText: {1}, ResultSeverity: {2}";
            TVerificationResult si;

            for (int i = 0; i <= Count - 1; i += 1)
            {
                si = (TVerificationResult)(List[i]);
                System.Console.WriteLine(String.Format(
                        fmt,
                        new object[] { si.FResultContext, si.FResultText,
                                       si.FResultSeverity }
                        ));
            }
        }

        /// <summary>
        /// remove a result from the list
        /// </summary>
        /// <param name="value">value to delete</param>
        public void Remove(TVerificationResult value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// remove a result from the list, specified by the column
        /// </summary>
        /// <param name="AResultColumn">the column identifying the result</param>
        public void Remove(DataColumn AResultColumn)
        {
            TScreenVerificationResult si;

            for (int Counter = 0; Counter <= Count - 1; Counter += 1)
            {
                si = (TScreenVerificationResult)(List[Counter]);

                if (si.FResultColumn == AResultColumn)
                {
                    List.Remove(si);
                    break;
                }
            }
        }

        /// <summary>
        /// remove a result identified by the column name
        /// </summary>
        /// <param name="AResultColumnName">column name</param>
        public void Remove(String AResultColumnName)
        {
            TScreenVerificationResult si;

            try
            {
                for (int Counter = 0; Counter <= Count - 1; Counter += 1)
                {
                    si = (TScreenVerificationResult)(List[Counter]);

                    if (si.FResultColumn.ColumnName == AResultColumnName)
                    {
                        List.Remove(si);
                        break;
                    }
                }
            }
            catch (Exception Exp)
            {
                MessageBox.Show("Exception occured in TVerificationResultCollection.Remove(AResultColumnName): " +
                    Exp.ToString());
            }
        }

        /// <summary>
        /// remove a result identified by the context
        /// </summary>
        /// <param name="AResultContext">the context</param>
        public void Remove(object AResultContext)
        {
            TScreenVerificationResult si;
            ArrayList siarray = new ArrayList();

            for (int Counter = 0; Counter <= Count - 1; Counter += 1)
            {
                si = (TScreenVerificationResult)(List[Counter]);

                // MessageBox.Show(si.FResultColumn.ToString + ' is found at array count ' + Counter.ToString);
                if (si.FResultContext == AResultContext)
                {
                    // MessageBox.Show('Length(siarray): ' + Convert.ToInt16(Length(siarray)).ToString);
                    siarray.Add(si);

                    // MessageBox.Show('Added ' + si.FResultColumn.ToString + ' at array count ' + Counter.ToString);
                }
            }

            for (int Counter2 = 0; Counter2 <= siarray.Count - 1; Counter2 += 1)
            {
                // MessageBox.Show('List.Remove of ' + siarray[Counter2].FResultColumn.ToString + ' at array count ' + Counter2.ToString);
                List.Remove((TScreenVerificationResult)siarray[Counter2]);
            }
        }

        private void VerifyType(object value)
        {
            if (!(value is TVerificationResult))
            {
                throw new ArgumentException("Invalid Type");
            }
        }
    }

    /// <summary>
    /// todoComment
    /// </summary>
    public class Data
    {
        /// <summary>
        /// todoComment
        /// </summary>
        /// <param name="e"></param>
        /// <param name="AVerificationResultEntry"></param>
        /// <param name="AControlName"></param>
        /// <param name="AResetValue"></param>
        public static void SetColumnErrorText(DataColumnChangeEventArgs e,
            TVerificationResult AVerificationResultEntry,
            String AControlName,
            Boolean AResetValue)
        {
            object PreviousProposedValue = e.ProposedValue;

            e.Row.SetColumnError(e.Column,
                AVerificationResultEntry.FResultText + "//[[" + AControlName + "]]");

            /*
             * SetColumnError automatically resets the value of the DataColumn.
             * Therefore we need to re-assign it again if we don't want to reset the value.
             */
            if (!AResetValue)
            {
                MessageBox.Show("SetColumnErrorText: Before resetting the value: " + e.ProposedValue.ToString());
                e.ProposedValue = PreviousProposedValue;
                MessageBox.Show("SetColumnErrorText: After resetting the value: " + e.ProposedValue.ToString());
            }
        }
    }
}