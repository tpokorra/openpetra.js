function Init() {

// year
ComboboxInitValues(
                    "serverMFinance.asmx/TGiftTransactionWebConnector_GetAvailableGiftYears",
                    {
                    // TODO ledgernumber not static
                    'ALedgerNumber': '43'
                    },
                    false, // AWithEmptyOption
                    "YearNumber",
                    "YearDate",
                    "#year",
                    2);
// period
// TODO: needs to depend on the year

// costcentre
ComboboxInitValues(
                    "serverMFinance.asmx/TFinanceCacheableWebConnector_GetCacheableTable2",
                    {
                    'ACacheableTable': 'CostCentreList',
                    'AHashCode': '',
                    // TODO ledgernumber not static
                    'ALedgerNumber': '43'
                    },
                    false, // AWithEmptyOption
                    "a_cost_centre_code_c",
                    "a_cost_centre_code_c",
                    "#costcentre");

// bank account
// "serverMFinance.asmx/TFinanceCacheableWebConnector_GetCacheableTable2"
// 'ACacheableTable': 'AccountList'
// TODO: only "BankAccountFlag":true

// Method of Payment: 
// see https://github.com/tpokorra/openpetra.js/blob/master/csharp/ICT/Petra/Client/MFinance/logic/FinanceControls.cs#L572
// serverMFinance.asmx/TFinanceCacheableWebConnector_GetCacheableTable
// MethodOfPaymentList
// but: the demodatabase does not return any values!

// Currency Code:
// see https://github.com/tpokorra/openpetra.js/blob/master/csharp/ICT/Petra/Client/MFinance/Gui/Gift/UC_GiftBatches.yaml#L73
// and https://github.com/tpokorra/openpetra.js/blob/master/csharp/ICT/Petra/Client/CommonControls/Gui/cmbAutoPopulatedComboBox.cs#L531
// serverMPartner.asmx/TPartnerCacheableWebConnector_GetCacheableTable
// CurrencyCodeList

}
