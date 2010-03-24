/*************************************************************************
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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Data;
using Ict.Tools.CodeGeneration;
using Ict.Tools.DBXML;
using Ict.Common.IO;
using Ict.Common;

namespace Ict.Tools.CodeGeneration.DataStore
{
    public class codeGenerationDatasetAccess
    {
        private static void AddTableToDataset(TTable ASqltable,
            string tabletype,
            string variablename,
            ProcessTemplate snippetDataset,
            ProcessTemplate ASnippetSubmitChanges)
        {
            ProcessTemplate tempSnippet;

            if (ASqltable != null)
            {
                string SequenceFields = "";

                foreach (TTableField tablefield in ASqltable.grpTableField.List)
                {
                    // is there a field filled by a sequence?
                    // yes: get the next value of that sequence and assign to row
                    if (tablefield.strSequence.Length > 0)
                    {
                        SequenceFields = ", \"" + tablefield.strSequence + "\", \"" + tablefield.strName + "\"";

                        // assume only one sequence per table
                        break;
                    }
                }

                tempSnippet = snippetDataset.GetSnippet("SUBMITCHANGES");
                tempSnippet.SetCodelet("ORIGTABLENAME", TTable.NiceTableName(ASqltable.strName));
                tempSnippet.SetCodelet("TABLETYPENAME", tabletype);
                tempSnippet.SetCodelet("TABLEVARIABLENAME", variablename);
                tempSnippet.SetCodelet("SQLOPERATION", "eDelete");
                tempSnippet.SetCodelet("SEQUENCENAMEANDFIELD", SequenceFields);
                ASnippetSubmitChanges.InsertSnippetPrepend("SUBMITCHANGESDELETE", tempSnippet);

                tempSnippet = snippetDataset.GetSnippet("SUBMITCHANGES");
                tempSnippet.SetCodelet("ORIGTABLENAME", TTable.NiceTableName(ASqltable.strName));
                tempSnippet.SetCodelet("TABLETYPENAME", tabletype);
                tempSnippet.SetCodelet("TABLEVARIABLENAME", variablename);
                tempSnippet.SetCodelet("SQLOPERATION", "eInsert | TTypedDataAccess.eSubmitChangesOperations.eUpdate");
                tempSnippet.SetCodelet("SEQUENCENAMEANDFIELD", SequenceFields);
                ASnippetSubmitChanges.InsertSnippet("SUBMITCHANGESINSERT", tempSnippet);

                ASnippetSubmitChanges.AddToCodelet("SUBMITCHANGESUPDATE", "");
            }
        }

        public static void CreateTypedDataSets(String AInputXmlfile,
            String AOutputPath,
            String ANameSpace,
            TDataDefinitionStore store,
            string[] groups,
            string AFilename)
        {
            Console.WriteLine("processing dataset " + ANameSpace);

            TAppSettingsManager opts = new TAppSettingsManager(false);
            string templateDir = opts.GetValue("TemplateDir", true);
            ProcessTemplate Template = new ProcessTemplate(templateDir + Path.DirectorySeparatorChar +
                "ORM" + Path.DirectorySeparatorChar +
                "DataSetAccess.cs");

            // load default header with license and copyright
            StreamReader sr = new StreamReader(templateDir + Path.DirectorySeparatorChar + "EmptyFileComment.txt");
            string fileheader = sr.ReadToEnd();
            sr.Close();
            fileheader = fileheader.Replace(">>>> Put your full name or just a shortname here <<<<", "auto generated");
            Template.SetCodelet("GPLFILEHEADER", fileheader);

            Template.SetCodelet("NAMESPACE", ANameSpace);

            // if no dataset is defined yet in the xml file, the following variables can be empty
            Template.AddToCodelet("USINGNAMESPACES", "");
            Template.AddToCodelet("CONTENTDATASETSANDTABLESANDROWS", "");

            Template.AddToCodelet("USINGNAMESPACES", "using " + ANameSpace.Replace(".Server.", ".Shared.") + ";" + Environment.NewLine, false);

            TXMLParser parserDataSet = new TXMLParser(AInputXmlfile, false);
            XmlDocument myDoc = parserDataSet.GetDocument();
            XmlNode startNode = myDoc.DocumentElement;

            if (startNode.Name.ToLower() == "petradatasets")
            {
                XmlNode cur = TXMLParser.NextNotBlank(startNode.FirstChild);

                while ((cur != null) && (cur.Name.ToLower() == "importunit"))
                {
                    Template.AddToCodelet("USINGNAMESPACES", "using " + TXMLParser.GetAttribute(cur, "name") + ";" + Environment.NewLine, false);
                    Template.AddToCodelet("USINGNAMESPACES", "using " + TXMLParser.GetAttribute(cur, "name").Replace(".Shared.",
                            ".Server.") + ".Access;" + Environment.NewLine, false);
                    cur = TXMLParser.GetNextEntity(cur);
                }

                while ((cur != null) && (cur.Name.ToLower() == "dataset"))
                {
                    ProcessTemplate snippetDataset = Template.GetSnippet("TYPEDDATASET");
                    string datasetname = TXMLParser.GetAttribute(cur, "name");
                    snippetDataset.SetCodelet("DATASETNAME", datasetname);

                    ProcessTemplate snippetSubmitChanges = snippetDataset.GetSnippet("SUBMITCHANGESFUNCTION");
                    snippetSubmitChanges.AddToCodelet("DATASETNAME", datasetname);

                    SortedList <string, TDataSetTable>tables = new SortedList <string, TDataSetTable>();

                    XmlNode curChild = cur.FirstChild;

                    while (curChild != null)
                    {
                        if (curChild.Name.ToLower() == "table")
                        {
                            string tabletype = TTable.NiceTableName(TXMLParser.GetAttribute(curChild, "sqltable"));
                            string variablename = (TXMLParser.HasAttribute(curChild, "name") ?
                                                   TXMLParser.GetAttribute(curChild, "name") :
                                                   tabletype);

                            TDataSetTable table = new TDataSetTable(
                                TXMLParser.GetAttribute(curChild, "sqltable"),
                                tabletype,
                                variablename,
                                store.GetTable(tabletype));
                            XmlNode tableNodes = curChild.FirstChild;

                            tables.Add(table.tableorig, table);

                            AddTableToDataset(store.GetTable(table.tableorig), tabletype, variablename,
                                snippetDataset,
                                snippetSubmitChanges);
                        }

                        curChild = curChild.NextSibling;
                    }

                    // there is one codelet for the dataset name.
                    // only add the full submitchanges function if there are any table to submit
                    if (snippetSubmitChanges.FCodelets.Count > 1)
                    {
                        snippetDataset.InsertSnippet("SUBMITCHANGESFUNCTION", snippetSubmitChanges);
                    }
                    else
                    {
                        snippetDataset.AddToCodelet("SUBMITCHANGESFUNCTION", "");
                    }

                    Template.InsertSnippet("CONTENTDATASETSANDTABLESANDROWS", snippetDataset);

                    cur = TXMLParser.GetNextEntity(cur);
                }
            }

            Template.FinishWriting(AOutputPath + Path.DirectorySeparatorChar + AFilename + ".cs", ".cs", true);
        }
    }
}