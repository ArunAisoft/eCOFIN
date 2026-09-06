using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure;

namespace eCOFIN.Infrastructure.Context;

public partial class BilzFinDbContext : DbContext
{
    public BilzFinDbContext()
    {
    }

    public BilzFinDbContext(DbContextOptions<BilzFinDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccCostPieceTab> AccCostPieceTabs { get; set; }

    public virtual DbSet<AccDcnoteDetail> AccDcnoteDetails { get; set; }

    public virtual DbSet<AccDcnoteMaster> AccDcnoteMasters { get; set; }

    public virtual DbSet<AccLife> AccLives { get; set; }

    public virtual DbSet<AccOperationRate> AccOperationRates { get; set; }

    public virtual DbSet<AccPaymentReceipt> AccPaymentReceipts { get; set; }

    public virtual DbSet<AccStdMhrMaster> AccStdMhrMasters { get; set; }

    public virtual DbSet<AccVatTypeMaster> AccVatTypeMasters { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AdvanceTable> AdvanceTables { get; set; }

    public virtual DbSet<Airsale> Airsales { get; set; }

    public virtual DbSet<Amend> Amends { get; set; }

    public virtual DbSet<AnnOpnReport> AnnOpnReports { get; set; }

    public virtual DbSet<AutoDatapull> AutoDatapulls { get; set; }

    public virtual DbSet<CfnAcccostcentre> CfnAcccostcentres { get; set; }

    public virtual DbSet<CfnAcccosttype> CfnAcccosttypes { get; set; }

    public virtual DbSet<CfnAcccustomer> CfnAcccustomers { get; set; }

    public virtual DbSet<CfnAccemployee> CfnAccemployees { get; set; }

    public virtual DbSet<CfnAccexptype> CfnAccexptypes { get; set; }

    public virtual DbSet<CfnAccncalender> CfnAccncalenders { get; set; }

    public virtual DbSet<CfnAccncalender2> CfnAccncalender2s { get; set; }

    public virtual DbSet<CfnAccobdetail> CfnAccobdetails { get; set; }

    public virtual DbSet<CfnAccount> CfnAccounts { get; set; }

    public virtual DbSet<CfnAccounthd> CfnAccounthds { get; set; }

    public virtual DbSet<CfnAccountinfo> CfnAccountinfos { get; set; }

    public virtual DbSet<CfnAccountlink> CfnAccountlinks { get; set; }

    public virtual DbSet<CfnAccountottolink> CfnAccountottolinks { get; set; }

    public virtual DbSet<CfnAccountsubhd> CfnAccountsubhds { get; set; }

    public virtual DbSet<CfnAccperiodlog> CfnAccperiodlogs { get; set; }

    public virtual DbSet<CfnAccproduct> CfnAccproducts { get; set; }

    public virtual DbSet<CfnAccrepcontb> CfnAccrepcontbs { get; set; }

    public virtual DbSet<CfnAccvendor> CfnAccvendors { get; set; }

    public virtual DbSet<CfnAgeingcfg> CfnAgeingcfgs { get; set; }

    public virtual DbSet<CfnAgeinghdr> CfnAgeinghdrs { get; set; }

    public virtual DbSet<CfnAgeingperiod> CfnAgeingperiods { get; set; }

    public virtual DbSet<CfnAmtInWord> CfnAmtInWords { get; set; }

    public virtual DbSet<CfnApplogcontrol> CfnApplogcontrols { get; set; }

    public virtual DbSet<CfnApplogerror> CfnApplogerrors { get; set; }

    public virtual DbSet<CfnApplogin> CfnApplogins { get; set; }

    public virtual DbSet<CfnApplogout> CfnApplogouts { get; set; }

    public virtual DbSet<CfnAutojournal> CfnAutojournals { get; set; }

    public virtual DbSet<CfnAutojrnldetail> CfnAutojrnldetails { get; set; }

    public virtual DbSet<CfnBalConfirm> CfnBalConfirms { get; set; }

    public virtual DbSet<CfnBank> CfnBanks { get; set; }

    public virtual DbSet<CfnBankCash> CfnBankCashes { get; set; }

    public virtual DbSet<CfnBankCashBook> CfnBankCashBooks { get; set; }

    public virtual DbSet<CfnBankCashOnholdno> CfnBankCashOnholdnos { get; set; }

    public virtual DbSet<CfnBankacclimit> CfnBankacclimits { get; set; }

    public virtual DbSet<CfnBankbook> CfnBankbooks { get; set; }

    public virtual DbSet<CfnBankdatum> CfnBankdata { get; set; }

    public virtual DbSet<CfnBankdeposit> CfnBankdeposits { get; set; }

    public virtual DbSet<CfnBankdocument> CfnBankdocuments { get; set; }

    public virtual DbSet<CfnBankimport> CfnBankimports { get; set; }

    public virtual DbSet<CfnBankinstrument> CfnBankinstruments { get; set; }

    public virtual DbSet<CfnBankpayment> CfnBankpayments { get; set; }

    public virtual DbSet<CfnBankreceipt> CfnBankreceipts { get; set; }

    public virtual DbSet<CfnBankreconcilliation> CfnBankreconcilliations { get; set; }

    public virtual DbSet<CfnBill> CfnBills { get; set; }

    public virtual DbSet<CfnBill100104> CfnBill100104s { get; set; }

    public virtual DbSet<CfnBillBk> CfnBillBks { get; set; }

    public virtual DbSet<CfnBilladjustment> CfnBilladjustments { get; set; }

    public virtual DbSet<CfnBillpassingaccount> CfnBillpassingaccounts { get; set; }

    public virtual DbSet<CfnBillpassingdtl> CfnBillpassingdtls { get; set; }

    public virtual DbSet<CfnBillpassinghdr> CfnBillpassinghdrs { get; set; }

    public virtual DbSet<CfnBnkddetail> CfnBnkddetails { get; set; }

    public virtual DbSet<CfnBnkdocdetail> CfnBnkdocdetails { get; set; }

    public virtual DbSet<CfnBnkpdetail> CfnBnkpdetails { get; set; }

    public virtual DbSet<CfnBnkrdetail> CfnBnkrdetails { get; set; }

    public virtual DbSet<CfnBnkretdetail> CfnBnkretdetails { get; set; }

    public virtual DbSet<CfnBnkretirement> CfnBnkretirements { get; set; }

    public virtual DbSet<CfnBpsyscatlinkdetail> CfnBpsyscatlinkdetails { get; set; }

    public virtual DbSet<CfnBptdsded> CfnBptdsdeds { get; set; }

    public virtual DbSet<CfnBptdshdr> CfnBptdshdrs { get; set; }

    public virtual DbSet<CfnBscsexport> CfnBscsexports { get; set; }

    public virtual DbSet<CfnBtlcustomerorder> CfnBtlcustomerorders { get; set; }

    public virtual DbSet<CfnBtldepositdetail> CfnBtldepositdetails { get; set; }

    public virtual DbSet<CfnBtldeposithd> CfnBtldeposithds { get; set; }

    public virtual DbSet<CfnBtlhstcustomerorder> CfnBtlhstcustomerorders { get; set; }

    public virtual DbSet<CfnBtlhstdepositdetail> CfnBtlhstdepositdetails { get; set; }

    public virtual DbSet<CfnBtlhstdeposithd> CfnBtlhstdeposithds { get; set; }

    public virtual DbSet<CfnBtlhstordrpricingdetail> CfnBtlhstordrpricingdetails { get; set; }

    public virtual DbSet<CfnBtlhstordrpricinghd> CfnBtlhstordrpricinghds { get; set; }

    public virtual DbSet<CfnBtlhstordrpricingproduct> CfnBtlhstordrpricingproducts { get; set; }

    public virtual DbSet<CfnBtlhstprodspecification> CfnBtlhstprodspecifications { get; set; }

    public virtual DbSet<CfnBtlhstterm> CfnBtlhstterms { get; set; }

    public virtual DbSet<CfnBtlordoa> CfnBtlordoas { get; set; }

    public virtual DbSet<CfnBtlordoadtl> CfnBtlordoadtls { get; set; }

    public virtual DbSet<CfnBtlordrpricingdetail> CfnBtlordrpricingdetails { get; set; }

    public virtual DbSet<CfnBtlordrpricinghd> CfnBtlordrpricinghds { get; set; }

    public virtual DbSet<CfnBtlordrpricingproduct> CfnBtlordrpricingproducts { get; set; }

    public virtual DbSet<CfnBtlordrprodspecification> CfnBtlordrprodspecifications { get; set; }

    public virtual DbSet<CfnBtlordrterm> CfnBtlordrterms { get; set; }

    public virtual DbSet<CfnBudget> CfnBudgets { get; set; }

    public virtual DbSet<CfnBudgetbreakup> CfnBudgetbreakups { get; set; }

    public virtual DbSet<CfnBudgetrevision> CfnBudgetrevisions { get; set; }

    public virtual DbSet<CfnBudgetrevisiondtl> CfnBudgetrevisiondtls { get; set; }

    public virtual DbSet<CfnBudgetsanction> CfnBudgetsanctions { get; set; }

    public virtual DbSet<CfnBudgetstack> CfnBudgetstacks { get; set; }

    public virtual DbSet<CfnBudgettransaction> CfnBudgettransactions { get; set; }

    public virtual DbSet<CfnCashbook> CfnCashbooks { get; set; }

    public virtual DbSet<CfnCashpayment> CfnCashpayments { get; set; }

    public virtual DbSet<CfnCashreceipt> CfnCashreceipts { get; set; }

    public virtual DbSet<CfnCcmail> CfnCcmails { get; set; }

    public virtual DbSet<CfnCfparameter> CfnCfparameters { get; set; }

    public virtual DbSet<CfnCfparvalue> CfnCfparvalues { get; set; }

    public virtual DbSet<CfnChequeissued> CfnChequeissueds { get; set; }

    public virtual DbSet<CfnChequeparty> CfnChequeparties { get; set; }

    public virtual DbSet<CfnChequerequest> CfnChequerequests { get; set; }

    public virtual DbSet<CfnChqrdetail> CfnChqrdetails { get; set; }

    public virtual DbSet<CfnCnsgdelivery> CfnCnsgdeliveries { get; set; }

    public virtual DbSet<CfnCompany> CfnCompanies { get; set; }

    public virtual DbSet<CfnConaccfg> CfnConaccfgs { get; set; }

    public virtual DbSet<CfnConacrepgl> CfnConacrepgls { get; set; }

    public virtual DbSet<CfnConloccfg> CfnConloccfgs { get; set; }

    public virtual DbSet<CfnConlocrepgl> CfnConlocrepgls { get; set; }

    public virtual DbSet<CfnConsignee> CfnConsignees { get; set; }

    public virtual DbSet<CfnConsolidated> CfnConsolidateds { get; set; }

    public virtual DbSet<CfnConstb> CfnConstbs { get; set; }

    public virtual DbSet<CfnContra> CfnContras { get; set; }

    public virtual DbSet<CfnContradetl> CfnContradetls { get; set; }

    public virtual DbSet<CfnCostcentre> CfnCostcentres { get; set; }

    public virtual DbSet<CfnCostdetail> CfnCostdetails { get; set; }

    public virtual DbSet<CfnCrdndetail> CfnCrdndetails { get; set; }

    public virtual DbSet<CfnCreditdetail> CfnCreditdetails { get; set; }

    public virtual DbSet<CfnCreditgenfactor> CfnCreditgenfactors { get; set; }

    public virtual DbSet<CfnCreditheader> CfnCreditheaders { get; set; }

    public virtual DbSet<CfnCreditinvcproduct> CfnCreditinvcproducts { get; set; }

    public virtual DbSet<CfnCreditinvoicereg> CfnCreditinvoiceregs { get; set; }

    public virtual DbSet<CfnCreditnote> CfnCreditnotes { get; set; }

    public virtual DbSet<CfnCshpdetail> CfnCshpdetails { get; set; }

    public virtual DbSet<CfnCshrdetail> CfnCshrdetails { get; set; }

    public virtual DbSet<CfnCurrency> CfnCurrencies { get; set; }

    public virtual DbSet<CfnCurrencydetl> CfnCurrencydetls { get; set; }

    public virtual DbSet<CfnCustomer> CfnCustomers { get; set; }

    public virtual DbSet<CfnCustomerorder> CfnCustomerorders { get; set; }

    public virtual DbSet<CfnCustomerprodlink> CfnCustomerprodlinks { get; set; }

    public virtual DbSet<CfnCustproduct> CfnCustproducts { get; set; }

    public virtual DbSet<CfnDealer> CfnDealers { get; set; }

    public virtual DbSet<CfnDebitnote> CfnDebitnotes { get; set; }

    public virtual DbSet<CfnDebndetail> CfnDebndetails { get; set; }

    public virtual DbSet<CfnDepartment> CfnDepartments { get; set; }

    public virtual DbSet<CfnDepositinfo> CfnDepositinfos { get; set; }

    public virtual DbSet<CfnEmployee> CfnEmployees { get; set; }

    public virtual DbSet<CfnEnqpricingdetail> CfnEnqpricingdetails { get; set; }

    public virtual DbSet<CfnEnqpricinghd> CfnEnqpricinghds { get; set; }

    public virtual DbSet<CfnEnqpricingproduct> CfnEnqpricingproducts { get; set; }

    public virtual DbSet<CfnEnqprodspecification> CfnEnqprodspecifications { get; set; }

    public virtual DbSet<CfnEnqterm> CfnEnqterms { get; set; }

    public virtual DbSet<CfnEnquiry> CfnEnquiries { get; set; }

    public virtual DbSet<CfnExtTemplate> CfnExtTemplates { get; set; }

    public virtual DbSet<CfnExtjvdatum> CfnExtjvdata { get; set; }

    public virtual DbSet<CfnFactorformula> CfnFactorformulas { get; set; }

    public virtual DbSet<CfnFactorformulaheader> CfnFactorformulaheaders { get; set; }

    public virtual DbSet<CfnFinancialinst> CfnFinancialinsts { get; set; }

    public virtual DbSet<CfnFinancialyear> CfnFinancialyears { get; set; }

    public virtual DbSet<CfnFinancialyear2> CfnFinancialyear2s { get; set; }

    public virtual DbSet<CfnFyrefcontrol> CfnFyrefcontrols { get; set; }

    public virtual DbSet<CfnGeneralledger> CfnGeneralledgers { get; set; }

    public virtual DbSet<CfnGeneralledger2> CfnGeneralledger2s { get; set; }

    public virtual DbSet<CfnGenfactor> CfnGenfactors { get; set; }

    public virtual DbSet<CfnGenhelp> CfnGenhelps { get; set; }

    public virtual DbSet<CfnGeography> CfnGeographies { get; set; }

    public virtual DbSet<CfnGivcdetail> CfnGivcdetails { get; set; }

    public virtual DbSet<CfnGivsrno> CfnGivsrnos { get; set; }

    public virtual DbSet<CfnGldetail> CfnGldetails { get; set; }

    public virtual DbSet<CfnGlsubledger> CfnGlsubledgers { get; set; }

    public virtual DbSet<CfnGlsubledger2> CfnGlsubledger2s { get; set; }

    public virtual DbSet<CfnGlsummaryacc> CfnGlsummaryaccs { get; set; }

    public virtual DbSet<CfnGoodsinvch> CfnGoodsinvches { get; set; }

    public virtual DbSet<CfnGoodsoutvch> CfnGoodsoutvches { get; set; }

    public virtual DbSet<CfnGovcdetail> CfnGovcdetails { get; set; }

    public virtual DbSet<CfnGovsrno> CfnGovsrnos { get; set; }

    public virtual DbSet<CfnHierarchy> CfnHierarchies { get; set; }

    public virtual DbSet<CfnHierarchylevel> CfnHierarchylevels { get; set; }

    public virtual DbSet<CfnHierarchylink> CfnHierarchylinks { get; set; }

    public virtual DbSet<CfnHstcnsgdelivery> CfnHstcnsgdeliveries { get; set; }

    public virtual DbSet<CfnHstcustomerorder> CfnHstcustomerorders { get; set; }

    public virtual DbSet<CfnHstdepositinfo> CfnHstdepositinfos { get; set; }

    public virtual DbSet<CfnHstordrproduct> CfnHstordrproducts { get; set; }

    public virtual DbSet<CfnHstordrterm> CfnHstordrterms { get; set; }

    public virtual DbSet<CfnHstpogenfactor> CfnHstpogenfactors { get; set; }

    public virtual DbSet<CfnHstporatecontract> CfnHstporatecontracts { get; set; }

    public virtual DbSet<CfnHstporatecontractdtl> CfnHstporatecontractdtls { get; set; }

    public virtual DbSet<CfnHstprodconsignee> CfnHstprodconsignees { get; set; }

    public virtual DbSet<CfnHstpurchasedetail> CfnHstpurchasedetails { get; set; }

    public virtual DbSet<CfnHstpurchaseorder> CfnHstpurchaseorders { get; set; }

    public virtual DbSet<CfnHstpurchaseterm> CfnHstpurchaseterms { get; set; }

    public virtual DbSet<CfnHstquotation> CfnHstquotations { get; set; }

    public virtual DbSet<CfnHstquotpricinghd> CfnHstquotpricinghds { get; set; }

    public virtual DbSet<CfnHstquotpricingproduct> CfnHstquotpricingproducts { get; set; }

    public virtual DbSet<CfnHstquotprodspecification> CfnHstquotprodspecifications { get; set; }

    public virtual DbSet<CfnHstquotterm> CfnHstquotterms { get; set; }

    public virtual DbSet<CfnIndtproduct> CfnIndtproducts { get; set; }

    public virtual DbSet<CfnInstallparam> CfnInstallparams { get; set; }

    public virtual DbSet<CfnInstrument> CfnInstruments { get; set; }

    public virtual DbSet<CfnInterbdetail> CfnInterbdetails { get; set; }

    public virtual DbSet<CfnInterbranch> CfnInterbranches { get; set; }

    public virtual DbSet<CfnIntracc> CfnIntraccs { get; set; }

    public virtual DbSet<CfnInvaccdetail> CfnInvaccdetails { get; set; }

    public virtual DbSet<CfnInvcprncfg> CfnInvcprncfgs { get; set; }

    public virtual DbSet<CfnInvcproduct> CfnInvcproducts { get; set; }

    public virtual DbSet<CfnInvcterm> CfnInvcterms { get; set; }

    public virtual DbSet<CfnInvoice> CfnInvoices { get; set; }

    public virtual DbSet<CfnInvoicereg> CfnInvoiceregs { get; set; }

    public virtual DbSet<CfnInvorderrelated> CfnInvorderrelateds { get; set; }

    public virtual DbSet<CfnInvreceipt> CfnInvreceipts { get; set; }

    public virtual DbSet<CfnInvregcfg> CfnInvregcfgs { get; set; }

    public virtual DbSet<CfnInvvchrhist> CfnInvvchrhists { get; set; }

    public virtual DbSet<CfnInvvchrtype> CfnInvvchrtypes { get; set; }

    public virtual DbSet<CfnJournal> CfnJournals { get; set; }

    public virtual DbSet<CfnJrnldetail> CfnJrnldetails { get; set; }

    public virtual DbSet<CfnLevel> CfnLevels { get; set; }

    public virtual DbSet<CfnLocation> CfnLocations { get; set; }

    public virtual DbSet<CfnLocnassociated> CfnLocnassociateds { get; set; }

    public virtual DbSet<CfnLocnbatch> CfnLocnbatches { get; set; }

    public virtual DbSet<CfnLocndepartment> CfnLocndepartments { get; set; }

    public virtual DbSet<CfnMemovoucher> CfnMemovouchers { get; set; }

    public virtual DbSet<CfnMemvdetail> CfnMemvdetails { get; set; }

    public virtual DbSet<CfnMrgquotpricinghd> CfnMrgquotpricinghds { get; set; }

    public virtual DbSet<CfnMrgquotpricingproduct> CfnMrgquotpricingproducts { get; set; }

    public virtual DbSet<CfnMrpbillheader> CfnMrpbillheaders { get; set; }

    public virtual DbSet<CfnMrpbillitemdetail> CfnMrpbillitemdetails { get; set; }

    public virtual DbSet<CfnMrpbillpassingaccount> CfnMrpbillpassingaccounts { get; set; }

    public virtual DbSet<CfnOatemp> CfnOatemps { get; set; }

    public virtual DbSet<CfnOldNew> CfnOldNews { get; set; }

    public virtual DbSet<CfnOrdergenfactor> CfnOrdergenfactors { get; set; }

    public virtual DbSet<CfnOrderpricingfactor> CfnOrderpricingfactors { get; set; }

    public virtual DbSet<CfnOrdertaxfactor> CfnOrdertaxfactors { get; set; }

    public virtual DbSet<CfnOrdrgenfactor> CfnOrdrgenfactors { get; set; }

    public virtual DbSet<CfnOrdrproduct> CfnOrdrproducts { get; set; }

    public virtual DbSet<CfnOrdrschproduct> CfnOrdrschproducts { get; set; }

    public virtual DbSet<CfnOrdrterm> CfnOrdrterms { get; set; }

    public virtual DbSet<CfnPackage> CfnPackages { get; set; }

    public virtual DbSet<CfnPanel> CfnPanels { get; set; }

    public virtual DbSet<CfnPanelpermission> CfnPanelpermissions { get; set; }

    public virtual DbSet<CfnPayment> CfnPayments { get; set; }

    public virtual DbSet<CfnPhysdetail> CfnPhysdetails { get; set; }

    public virtual DbSet<CfnPhystockvch> CfnPhystockvches { get; set; }

    public virtual DbSet<CfnPlaccount> CfnPlaccounts { get; set; }

    public virtual DbSet<CfnPlaccountOld> CfnPlaccountOlds { get; set; }

    public virtual DbSet<CfnPlgroup> CfnPlgroups { get; set; }

    public virtual DbSet<CfnPoenquiry> CfnPoenquiries { get; set; }

    public virtual DbSet<CfnPoenquiryprlink> CfnPoenquiryprlinks { get; set; }

    public virtual DbSet<CfnPoenquiryvendor> CfnPoenquiryvendors { get; set; }

    public virtual DbSet<CfnPogenfactor> CfnPogenfactors { get; set; }

    public virtual DbSet<CfnPoprint> CfnPoprints { get; set; }

    public virtual DbSet<CfnPoprvchrtype> CfnPoprvchrtypes { get; set; }

    public virtual DbSet<CfnPoquotation> CfnPoquotations { get; set; }

    public virtual DbSet<CfnPoquotationdtl> CfnPoquotationdtls { get; set; }

    public virtual DbSet<CfnPoquotgenfactor> CfnPoquotgenfactors { get; set; }

    public virtual DbSet<CfnPoquotterm> CfnPoquotterms { get; set; }

    public virtual DbSet<CfnPoratecontract> CfnPoratecontracts { get; set; }

    public virtual DbSet<CfnPoratecontractdtl> CfnPoratecontractdtls { get; set; }

    public virtual DbSet<CfnPostagetrack> CfnPostagetracks { get; set; }

    public virtual DbSet<CfnPricingfactor> CfnPricingfactors { get; set; }

    public virtual DbSet<CfnPrintfactor> CfnPrintfactors { get; set; }

    public virtual DbSet<CfnPrintslno> CfnPrintslnos { get; set; }

    public virtual DbSet<CfnProdconsignee> CfnProdconsignees { get; set; }

    public virtual DbSet<CfnProddelivery> CfnProddeliveries { get; set; }

    public virtual DbSet<CfnProdmergsrno> CfnProdmergsrnos { get; set; }

    public virtual DbSet<CfnProdspeclink> CfnProdspeclinks { get; set; }

    public virtual DbSet<CfnProduct> CfnProducts { get; set; }

    public virtual DbSet<CfnProductmerg> CfnProductmergs { get; set; }

    public virtual DbSet<CfnProductstock> CfnProductstocks { get; set; }

    public virtual DbSet<CfnProductstockSummary> CfnProductstockSummaries { get; set; }

    public virtual DbSet<CfnProducttransfer> CfnProducttransfers { get; set; }

    public virtual DbSet<CfnProducttransferdtl> CfnProducttransferdtls { get; set; }

    public virtual DbSet<CfnProductwarehouse> CfnProductwarehouses { get; set; }

    public virtual DbSet<CfnPurchaseRegister> CfnPurchaseRegisters { get; set; }

    public virtual DbSet<CfnPurchaseRegisterBilzuser> CfnPurchaseRegisterBilzusers { get; set; }

    public virtual DbSet<CfnPurchasedetail> CfnPurchasedetails { get; set; }

    public virtual DbSet<CfnPurchasejnl> CfnPurchasejnls { get; set; }

    public virtual DbSet<CfnPurchaseprint> CfnPurchaseprints { get; set; }

    public virtual DbSet<CfnPurchaseterm> CfnPurchaseterms { get; set; }

    public virtual DbSet<CfnPurjdetail> CfnPurjdetails { get; set; }

    public virtual DbSet<CfnQuerytask> CfnQuerytasks { get; set; }

    public virtual DbSet<CfnQuotation> CfnQuotations { get; set; }

    public virtual DbSet<CfnQuotdepdetail> CfnQuotdepdetails { get; set; }

    public virtual DbSet<CfnQuotdeposithd> CfnQuotdeposithds { get; set; }

    public virtual DbSet<CfnQuotgenfactor> CfnQuotgenfactors { get; set; }

    public virtual DbSet<CfnQuotpricingdetail> CfnQuotpricingdetails { get; set; }

    public virtual DbSet<CfnQuotpricinghd> CfnQuotpricinghds { get; set; }

    public virtual DbSet<CfnQuotpricingproduct> CfnQuotpricingproducts { get; set; }

    public virtual DbSet<CfnQuotprodspecification> CfnQuotprodspecifications { get; set; }

    public virtual DbSet<CfnQuotterm> CfnQuotterms { get; set; }

    public virtual DbSet<CfnRcgenfactor> CfnRcgenfactors { get; set; }

    public virtual DbSet<CfnRectemplate> CfnRectemplates { get; set; }

    public virtual DbSet<CfnRectempldetail> CfnRectempldetails { get; set; }

    public virtual DbSet<CfnRecurringvchr> CfnRecurringvchrs { get; set; }

    public virtual DbSet<CfnRecurringvchrdetail> CfnRecurringvchrdetails { get; set; }

    public virtual DbSet<CfnRecvchrtemplate> CfnRecvchrtemplates { get; set; }

    public virtual DbSet<CfnRecvoucher> CfnRecvouchers { get; set; }

    public virtual DbSet<CfnReferencectrl> CfnReferencectrls { get; set; }

    public virtual DbSet<CfnRepageingtemplate> CfnRepageingtemplates { get; set; }

    public virtual DbSet<CfnRepbcdaybook> CfnRepbcdaybooks { get; set; }

    public virtual DbSet<CfnRepbcdetail> CfnRepbcdetails { get; set; }

    public virtual DbSet<CfnRepcontb> CfnRepcontbs { get; set; }

    public virtual DbSet<CfnRepgeneralledger> CfnRepgeneralledgers { get; set; }

    public virtual DbSet<CfnRepgldetail> CfnRepgldetails { get; set; }

    public virtual DbSet<CfnReportcontrol> CfnReportcontrols { get; set; }

    public virtual DbSet<CfnReportcontrol1> CfnReportcontrol1s { get; set; }

    public virtual DbSet<CfnRequisition> CfnRequisitions { get; set; }

    public virtual DbSet<CfnRequisitiondtl> CfnRequisitiondtls { get; set; }

    public virtual DbSet<CfnSalescnsgdelivery> CfnSalescnsgdeliveries { get; set; }

    public virtual DbSet<CfnSalescustomer> CfnSalescustomers { get; set; }

    public virtual DbSet<CfnSalesindent> CfnSalesindents { get; set; }

    public virtual DbSet<CfnSalesprodconsignee> CfnSalesprodconsignees { get; set; }

    public virtual DbSet<CfnSalevoucher> CfnSalevouchers { get; set; }

    public virtual DbSet<CfnSalvdetail> CfnSalvdetails { get; set; }

    public virtual DbSet<CfnSandetail> CfnSandetails { get; set; }

    public virtual DbSet<CfnSerialamendment> CfnSerialamendments { get; set; }

    public virtual DbSet<CfnSerialnohistory> CfnSerialnohistories { get; set; }

    public virtual DbSet<CfnServicenote> CfnServicenotes { get; set; }

    public virtual DbSet<CfnSpecification> CfnSpecifications { get; set; }

    public virtual DbSet<CfnStocksrno> CfnStocksrnos { get; set; }

    public virtual DbSet<CfnTask> CfnTasks { get; set; }

    public virtual DbSet<CfnTask1> CfnTask1s { get; set; }

    public virtual DbSet<CfnTaxfactor> CfnTaxfactors { get; set; }

    public virtual DbSet<CfnTd> CfnTds { get; set; }

    public virtual DbSet<CfnTdsaccountslno> CfnTdsaccountslnos { get; set; }

    public virtual DbSet<CfnTdsbillpayment> CfnTdsbillpayments { get; set; }

    public virtual DbSet<CfnTdsdeduction> CfnTdsdeductions { get; set; }

    public virtual DbSet<CfnTdsdeposit> CfnTdsdeposits { get; set; }

    public virtual DbSet<CfnTempcost> CfnTempcosts { get; set; }

    public virtual DbSet<CfnTendcompprodspec> CfnTendcompprodspecs { get; set; }

    public virtual DbSet<CfnTender> CfnTenders { get; set; }

    public virtual DbSet<CfnTendercompetitor> CfnTendercompetitors { get; set; }

    public virtual DbSet<CfnTendercomppricingdetail> CfnTendercomppricingdetails { get; set; }

    public virtual DbSet<CfnTendercomppricinghd> CfnTendercomppricinghds { get; set; }

    public virtual DbSet<CfnTendercomppricingproduct> CfnTendercomppricingproducts { get; set; }

    public virtual DbSet<CfnTendercompterm> CfnTendercompterms { get; set; }

    public virtual DbSet<CfnTenderdepositdetail> CfnTenderdepositdetails { get; set; }

    public virtual DbSet<CfnTenderdeposithd> CfnTenderdeposithds { get; set; }

    public virtual DbSet<CfnTenderpricingdetail> CfnTenderpricingdetails { get; set; }

    public virtual DbSet<CfnTenderpricinghd> CfnTenderpricinghds { get; set; }

    public virtual DbSet<CfnTenderpricingproduct> CfnTenderpricingproducts { get; set; }

    public virtual DbSet<CfnTenderprodspecification> CfnTenderprodspecifications { get; set; }

    public virtual DbSet<CfnTenderterm> CfnTenderterms { get; set; }

    public virtual DbSet<CfnTerm> CfnTerms { get; set; }

    public virtual DbSet<CfnTermvalue> CfnTermvalues { get; set; }

    public virtual DbSet<CfnTesttask> CfnTesttasks { get; set; }

    public virtual DbSet<CfnTrLink> CfnTrLinks { get; set; }

    public virtual DbSet<CfnTranslation> CfnTranslations { get; set; }

    public virtual DbSet<CfnTranslationBill> CfnTranslationBills { get; set; }

    public virtual DbSet<CfnTravelsanc> CfnTravelsancs { get; set; }

    public virtual DbSet<CfnTravelvoucher> CfnTravelvouchers { get; set; }

    public virtual DbSet<CfnTrvldetail> CfnTrvldetails { get; set; }

    public virtual DbSet<CfnUser> CfnUsers { get; set; }

    public virtual DbSet<CfnUsercostcentre> CfnUsercostcentres { get; set; }

    public virtual DbSet<CfnUserpermission> CfnUserpermissions { get; set; }

    public virtual DbSet<CfnUservchr> CfnUservchrs { get; set; }

    public virtual DbSet<CfnUservchrsyscat> CfnUservchrsyscats { get; set; }

    public virtual DbSet<CfnUserwarehouse> CfnUserwarehouses { get; set; }

    public virtual DbSet<CfnVBankHeader> CfnVBankHeaders { get; set; }

    public virtual DbSet<CfnVBankbook> CfnVBankbooks { get; set; }

    public virtual DbSet<CfnVBankpayment> CfnVBankpayments { get; set; }

    public virtual DbSet<CfnVBnkVendor> CfnVBnkVendors { get; set; }

    public virtual DbSet<CfnVCashHeader> CfnVCashHeaders { get; set; }

    public virtual DbSet<CfnVCashbook> CfnVCashbooks { get; set; }

    public virtual DbSet<CfnVCashpayment> CfnVCashpayments { get; set; }

    public virtual DbSet<CfnVCashreceipt> CfnVCashreceipts { get; set; }

    public virtual DbSet<CfnVCfparcode> CfnVCfparcodes { get; set; }

    public virtual DbSet<CfnVChequeparty> CfnVChequeparties { get; set; }

    public virtual DbSet<CfnVContra> CfnVContras { get; set; }

    public virtual DbSet<CfnVCosttype> CfnVCosttypes { get; set; }

    public virtual DbSet<CfnVCreditnote> CfnVCreditnotes { get; set; }

    public virtual DbSet<CfnVCreditoroutstanding> CfnVCreditoroutstandings { get; set; }

    public virtual DbSet<CfnVDailytransaction> CfnVDailytransactions { get; set; }

    public virtual DbSet<CfnVDebitnote> CfnVDebitnotes { get; set; }

    public virtual DbSet<CfnVExchange> CfnVExchanges { get; set; }

    public virtual DbSet<CfnVExpensetype> CfnVExpensetypes { get; set; }

    public virtual DbSet<CfnVGinjin> CfnVGinjins { get; set; }

    public virtual DbSet<CfnVInvoice> CfnVInvoices { get; set; }

    public virtual DbSet<CfnVJournalregister> CfnVJournalregisters { get; set; }

    public virtual DbSet<CfnVPlaccount> CfnVPlaccounts { get; set; }

    public virtual DbSet<CfnVPlaccountmem> CfnVPlaccountmems { get; set; }

    public virtual DbSet<CfnVProductstock> CfnVProductstocks { get; set; }

    public virtual DbSet<CfnVSalesregister> CfnVSalesregisters { get; set; }

    public virtual DbSet<CfnVStockRegister> CfnVStockRegisters { get; set; }

    public virtual DbSet<CfnVSubcode> CfnVSubcodes { get; set; }

    public virtual DbSet<CfnVSubcodeslink> CfnVSubcodeslinks { get; set; }

    public virtual DbSet<CfnVSubcodlnk> CfnVSubcodlnks { get; set; }

    public virtual DbSet<CfnVSubledger> CfnVSubledgers { get; set; }

    public virtual DbSet<CfnVTdsregister> CfnVTdsregisters { get; set; }

    public virtual DbSet<CfnVTransaction> CfnVTransactions { get; set; }

    public virtual DbSet<CfnVTravelregister> CfnVTravelregisters { get; set; }

    public virtual DbSet<CfnVTrialbalance> CfnVTrialbalances { get; set; }

    public virtual DbSet<CfnVchrcontrol> CfnVchrcontrols { get; set; }

    public virtual DbSet<CfnVchrgroup> CfnVchrgroups { get; set; }

    public virtual DbSet<CfnVchrtype> CfnVchrtypes { get; set; }

    public virtual DbSet<CfnVendor> CfnVendors { get; set; }

    public virtual DbSet<CfnVendorratecontract> CfnVendorratecontracts { get; set; }

    public virtual DbSet<CfnView> CfnViews { get; set; }

    public virtual DbSet<CfnVoucheraccount> CfnVoucheraccounts { get; set; }

    public virtual DbSet<CfnVoucherdetail> CfnVoucherdetails { get; set; }

    public virtual DbSet<CfnVoucherheader> CfnVoucherheaders { get; set; }

    public virtual DbSet<CfnVoucherjvlnk> CfnVoucherjvlnks { get; set; }

    public virtual DbSet<CfnVoucherobject> CfnVoucherobjects { get; set; }

    public virtual DbSet<CfnVouchersysdatum> CfnVouchersysdata { get; set; }

    public virtual DbSet<CfnWarehouse> CfnWarehouses { get; set; }

    public virtual DbSet<CfnWhstocktypelink> CfnWhstocktypelinks { get; set; }

    public virtual DbSet<CodeEntryAccount> CodeEntryAccounts { get; set; }

    public virtual DbSet<CodeType> CodeTypes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerOld> CustomerOlds { get; set; }

    public virtual DbSet<EinvDetail> EinvDetails { get; set; }

    public virtual DbSet<EinvMain> EinvMains { get; set; }

    public virtual DbSet<GinDetail> GinDetails { get; set; }

    public virtual DbSet<GinMaster> GinMasters { get; set; }

    public virtual DbSet<GinPriceDomestic> GinPriceDomestics { get; set; }

    public virtual DbSet<GinPriceImport> GinPriceImports { get; set; }

    public virtual DbSet<Glsubledger> Glsubledgers { get; set; }

    public virtual DbSet<Glsummaryacc> Glsummaryaccs { get; set; }

    public virtual DbSet<ImportFinCreditNote> ImportFinCreditNotes { get; set; }

    public virtual DbSet<ImportFinDebitNote> ImportFinDebitNotes { get; set; }

    public virtual DbSet<ImportFinGin> ImportFinGins { get; set; }

    public virtual DbSet<ImportFinInvoiceDomestic> ImportFinInvoiceDomestics { get; set; }

    public virtual DbSet<ImportFinInvoiceExport> ImportFinInvoiceExports { get; set; }

    public virtual DbSet<ImportFinJin> ImportFinJins { get; set; }

    public virtual DbSet<InvDetail> InvDetails { get; set; }

    public virtual DbSet<InvMain> InvMains { get; set; }

    public virtual DbSet<MasterTd> MasterTds { get; set; }

    public virtual DbSet<MisDinvoiceEmpCustItm> MisDinvoiceEmpCustItms { get; set; }

    public virtual DbSet<MisEinvoice> MisEinvoices { get; set; }

    public virtual DbSet<MisEinvoiceEmpCustItm> MisEinvoiceEmpCustItms { get; set; }

    public virtual DbSet<MisInvoice> MisInvoices { get; set; }

    public virtual DbSet<MisInvoiceEmpCustItm> MisInvoiceEmpCustItms { get; set; }

    public virtual DbSet<MisOrderEmpCustItm> MisOrderEmpCustItms { get; set; }

    public virtual DbSet<MisPeriodEmpCustItm> MisPeriodEmpCustItms { get; set; }

    public virtual DbSet<MisTargetEmpCustItm> MisTargetEmpCustItms { get; set; }

    public virtual DbSet<MisTotalEmpCustItm> MisTotalEmpCustItms { get; set; }

    public virtual DbSet<ObiCodeEntry> ObiCodeEntries { get; set; }

    public virtual DbSet<Personel> Personels { get; set; }

    public virtual DbSet<StoAnnexDetail> StoAnnexDetails { get; set; }

    public virtual DbSet<StoAnnexMaster> StoAnnexMasters { get; set; }

    public virtual DbSet<SupplierDetail> SupplierDetails { get; set; }

    public virtual DbSet<UtPayment> UtPayments { get; set; }

    public virtual DbSet<UvPayment> UvPayments { get; set; }

    public virtual DbSet<Vendcode> Vendcodes { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-MRP\\SQLEXPRESS;Database=BilzFinDB;User Id=sa;Password=home@38;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<AccCostPieceTab>(entity =>
        {
            entity.Property(e => e.RcNo).IsFixedLength();
            entity.Property(e => e.SubType).IsFixedLength();
        });

        modelBuilder.Entity<AccDcnoteDetail>(entity =>
        {
            entity.Property(e => e.ItemCancel)
                .HasDefaultValue("N")
                .IsFixedLength();

            entity.HasOne(d => d.DcnoteNoNavigation).WithMany(p => p.AccDcnoteDetails).HasConstraintName("PK_Acc_DCNote_Master_FK_Acc_DCCNote_Detail");
        });

        modelBuilder.Entity<AccDcnoteMaster>(entity =>
        {
            entity.Property(e => e.ClientName).HasDefaultValueSql("(host_name())");
            entity.Property(e => e.CurrencyCode).HasDefaultValue("CUR/00001");
            entity.Property(e => e.CurrencyRate).HasDefaultValueSql("((1))");
            entity.Property(e => e.DcnoteCancel)
                .HasDefaultValue("N")
                .IsFixedLength();
            entity.Property(e => e.DcnoteDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MacAddress).HasDefaultValueSql("([dbo].[Get_MacAddress]())");
        });

        modelBuilder.Entity<AccLife>(entity =>
        {
            entity.Property(e => e.ActNo).IsFixedLength();
            entity.Property(e => e.CcNo).IsFixedLength();
            entity.Property(e => e.LifeDate).IsFixedLength();
        });

        modelBuilder.Entity<AccPaymentReceipt>(entity =>
        {
            entity.Property(e => e.Advance)
                .HasDefaultValue("N")
                .IsFixedLength();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<AccVatTypeMaster>(entity =>
        {
            entity.HasKey(e => e.SlNo).HasFillFactor(90);

            entity.Property(e => e.SlNo).ValueGeneratedNever();
            entity.Property(e => e.Active).IsFixedLength();
        });

        modelBuilder.Entity<AdvanceTable>(entity =>
        {
            entity.Property(e => e.AdvNo).IsFixedLength();
        });

        modelBuilder.Entity<Airsale>(entity =>
        {
            entity.ToView("airsales");
        });

        modelBuilder.Entity<Amend>(entity =>
        {
            entity.Property(e => e.OrderDispatchFrom)
                .HasDefaultValue("U1")
                .IsFixedLength();
        });

        modelBuilder.Entity<AnnOpnReport>(entity =>
        {
            entity.Property(e => e.Status).IsFixedLength();
        });

        modelBuilder.Entity<AutoDatapull>(entity =>
        {
            entity.Property(e => e.PullActive).IsFixedLength();
            entity.Property(e => e.TableName).HasComputedColumnSql("([From_TableName])", false);
        });

        modelBuilder.Entity<CfnAcccostcentre>(entity =>
        {
            entity.HasKey(e => new { e.Accountcode, e.Costcentrecode })
                .HasName("PKY_ACCCOSTCENTRE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnAcccosttype>(entity =>
        {
            entity.HasKey(e => new { e.Accountcode, e.Costtype })
                .HasName("PKY_ACCCOSTTYPE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnAcccustomer>(entity =>
        {
            entity.HasKey(e => new { e.Accountcode, e.Customercode })
                .HasName("PKY_ACCCUSTOMER")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnAccemployee>(entity =>
        {
            entity.HasKey(e => new { e.Accountcode, e.Employeecode })
                .HasName("PKY_ACCEMPLOYEE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnAccexptype>(entity =>
        {
            entity.HasKey(e => new { e.Accountcode, e.Expensetype })
                .HasName("PKY_ACCEXPTYPE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnAccncalender>(entity =>
        {
            entity.HasKey(e => e.Accperiod)
                .HasName("PKY_ACCNCALENDER")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnAccncalender2>(entity =>
        {
            entity.HasKey(e => e.Accperiod)
                .HasName("PKY_ACCNCALENDER2")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnAccobdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_ACCOBDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Billorpayment).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnAccount>(entity =>
        {
            entity.HasKey(e => e.Accountcode)
                .HasName("PKY_ACCOUNT")
                .HasFillFactor(90);

            entity.Property(e => e.Billwiseappl).IsFixedLength();
            entity.Property(e => e.Budgetappl).IsFixedLength();
            entity.Property(e => e.Controlaccount).IsFixedLength();
            entity.Property(e => e.Costappl).IsFixedLength();
            entity.Property(e => e.Costtypeappl).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
            entity.Property(e => e.Employeeappl).IsFixedLength();
            entity.Property(e => e.Expenseappl).IsFixedLength();
            entity.Property(e => e.Productappl).IsFixedLength();
            entity.Property(e => e.Stockappl).IsFixedLength();
            entity.Property(e => e.Subaccount).IsFixedLength();
            entity.Property(e => e.Subledgerappl).IsFixedLength();
        });

        modelBuilder.Entity<CfnAccounthd>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.Accountcode })
                .HasName("PKY_ACCOUNTHD")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnAccountinfo>(entity =>
        {
            entity.Property(e => e.Cmanupdflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnAccountottolink>(entity =>
        {
            entity.Property(e => e.Accountcode).IsFixedLength();
            entity.Property(e => e.CtrlAccperiod).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLocationcode).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
            entity.Property(e => e.CtrlOnholdno).IsFixedLength();
            entity.Property(e => e.CtrlPrevrefr).IsFixedLength();
            entity.Property(e => e.CtrlStatus).IsFixedLength();
            entity.Property(e => e.CtrlTrglocationcode).IsFixedLength();
            entity.Property(e => e.CtrlUsername).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Objectstatus).IsFixedLength();
            entity.Property(e => e.ProdLevytype).IsFixedLength();
            entity.Property(e => e.ProdPrefix).IsFixedLength();
            entity.Property(e => e.ProdPrefixtype).IsFixedLength();
            entity.Property(e => e.VchrType).IsFixedLength();
        });

        modelBuilder.Entity<CfnAccountsubhd>(entity =>
        {
            entity.HasKey(e => new { e.Subaccountcode, e.Accountcode, e.Accperiod })
                .HasName("PKY_ACCSUBHD")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnAccperiodlog>(entity =>
        {
            entity.Property(e => e.Accperiod).IsFixedLength();
            entity.Property(e => e.CurrState).IsFixedLength();
            entity.Property(e => e.PrevStatus).IsFixedLength();
            entity.Property(e => e.Reason).IsFixedLength();
            entity.Property(e => e.Username).IsFixedLength();
        });

        modelBuilder.Entity<CfnAccproduct>(entity =>
        {
            entity.HasKey(e => new { e.Accountcode, e.Productcode })
                .HasName("PKY_ACCPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnAccrepcontb>(entity =>
        {
            entity.HasKey(e => e.Accountcode)
                .HasName("PKY_ACCREPCONTB")
                .HasFillFactor(90);

            entity.Property(e => e.CbDbcr1).IsFixedLength();
            entity.Property(e => e.CbDbcr10).IsFixedLength();
            entity.Property(e => e.CbDbcr11).IsFixedLength();
            entity.Property(e => e.CbDbcr12).IsFixedLength();
            entity.Property(e => e.CbDbcr2).IsFixedLength();
            entity.Property(e => e.CbDbcr3).IsFixedLength();
            entity.Property(e => e.CbDbcr4).IsFixedLength();
            entity.Property(e => e.CbDbcr5).IsFixedLength();
            entity.Property(e => e.CbDbcr6).IsFixedLength();
            entity.Property(e => e.CbDbcr7).IsFixedLength();
            entity.Property(e => e.CbDbcr8).IsFixedLength();
            entity.Property(e => e.CbDbcr9).IsFixedLength();
            entity.Property(e => e.TotDbcr).IsFixedLength();
        });

        modelBuilder.Entity<CfnAccvendor>(entity =>
        {
            entity.HasKey(e => new { e.Accountcode, e.Vendorcode })
                .HasName("PKY_ACCVENDOR")
                .IsClustered(false)
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnAgeingcfg>(entity =>
        {
            entity.HasKey(e => e.Slno)
                .HasName("PKY_AGEINGCFG")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnAgeinghdr>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_AGEINGHDR")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnAmtInWord>(entity =>
        {
            entity.HasKey(e => e.Value)
                .HasName("SYS_C00406")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnApplogcontrol>(entity =>
        {
            entity.HasKey(e => new { e.Sourcelocation, e.Batchnumber, e.Targetlocation })
                .HasName("PKY_APPLOGCONTROL")
                .HasFillFactor(90);

            entity.Property(e => e.Executionstatus).IsFixedLength();
        });

        modelBuilder.Entity<CfnApplogerror>(entity =>
        {
            entity.HasKey(e => new { e.Sourcelocation, e.Batchnumber, e.Applicationloginid, e.Logsequence })
                .HasName("PKY_APPLOGERROR")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnApplogin>(entity =>
        {
            entity.HasKey(e => new { e.Sourcelocation, e.Applicationloginid, e.Logsequence, e.Logtype, e.Targetlocation })
                .HasName("PKY_APPLOGIN")
                .HasFillFactor(90);

            entity.Property(e => e.Logtype).IsFixedLength();
            entity.Property(e => e.Logexecutionflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnApplogout>(entity =>
        {
            entity.HasKey(e => new { e.Sourcelocation, e.Applicationloginid, e.Logsequence, e.Logtype, e.Targetlocation })
                .HasName("PKY_APPLOGOUT")
                .HasFillFactor(90);

            entity.Property(e => e.Logtype).IsFixedLength();
            entity.Property(e => e.Logexecutionflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnAutojournal>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_AUTOJOURNAL")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnAutojrnldetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_AUTOJRNLDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBank>(entity =>
        {
            entity.HasKey(e => e.Bankcode)
                .HasName("PKY_BANK")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBankCash>(entity =>
        {
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.RcptPmt).IsFixedLength();
        });

        modelBuilder.Entity<CfnBankacclimit>(entity =>
        {
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBankbook>(entity =>
        {
            entity.HasKey(e => e.Bankcontrolaccount)
                .HasName("PKY_BANKBOOK")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBankdatum>(entity =>
        {
            entity.HasKey(e => e.Serialnumber)
                .HasName("PKY_CFN_BANKDATA")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Payorreceiptflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBankdeposit>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_BANKDEPOSIT")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnBankdocument>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_BANKDOCUMENT")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnBankinstrument>(entity =>
        {
            entity.HasKey(e => new { e.Accountcode, e.Instrumentbookno })
                .HasName("PKY_BANKINSTRUMENT")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBankpayment>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_BANKPAYMENT")
                .HasFillFactor(90);

            entity.Property(e => e.Chqauthorize).IsFixedLength();
            entity.Property(e => e.Chqgenerate).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnBankreceipt>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_BANKRECEIPT")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnBankreconcilliation>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("pky_bankreconcil")
                .IsClustered(false)
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Payorreceiptflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBill>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BILLNEW")
                .HasFillFactor(90);

            entity.HasIndex(e => e.VchrRefnumber, "ind_vchrrefnumber").HasFillFactor(90);

            entity.HasIndex(e => e.Accountcode, "ndx_account").HasFillFactor(90);

            entity.HasIndex(e => e.Subaccountcode, "ndx_subcode").HasFillFactor(90);

            entity.Property(e => e.Bankdocumentno).IsFixedLength();
            entity.Property(e => e.Billno).IsFixedLength();
            entity.Property(e => e.Costcentrecode).IsFixedLength();
            entity.Property(e => e.Costtype).IsFixedLength();
            entity.Property(e => e.CtrlAccperiod).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLocationcode).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlTrglocationcode).IsFixedLength();
            entity.Property(e => e.CtrlUsername).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Employeecode).IsFixedLength();
            entity.Property(e => e.Expensetype).IsFixedLength();
            entity.Property(e => e.Lcnumber).IsFixedLength();
            entity.Property(e => e.LstCategory).IsFixedLength();
            entity.Property(e => e.Ponumber).IsFixedLength();
            entity.Property(e => e.Productcode).IsFixedLength();
            entity.Property(e => e.Segcode2).IsFixedLength();
            entity.Property(e => e.VchrCategory).IsFixedLength();
            entity.Property(e => e.VchrRefnumber).IsFixedLength();
            entity.Property(e => e.VchrSyscategory).IsFixedLength();
            entity.Property(e => e.VchrType).IsFixedLength();
        });

        modelBuilder.Entity<CfnBill100104>(entity =>
        {
            entity.HasIndex(e => new { e.CtrlOnholdno, e.CtrlSequenceno }, "CFN_BILL_x")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.Accountcode).IsFixedLength();
            entity.Property(e => e.Bankdocumentno).IsFixedLength();
            entity.Property(e => e.Billno).IsFixedLength();
            entity.Property(e => e.Costcentrecode).IsFixedLength();
            entity.Property(e => e.Costtype).IsFixedLength();
            entity.Property(e => e.CtrlAccperiod).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLocationcode).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlOnholdno).IsFixedLength();
            entity.Property(e => e.CtrlStatus).IsFixedLength();
            entity.Property(e => e.CtrlTrglocationcode).IsFixedLength();
            entity.Property(e => e.CtrlUsername).IsFixedLength();
            entity.Property(e => e.Currencycode).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Employeecode).IsFixedLength();
            entity.Property(e => e.Expensetype).IsFixedLength();
            entity.Property(e => e.Lcnumber).IsFixedLength();
            entity.Property(e => e.LstCategory).IsFixedLength();
            entity.Property(e => e.Ponumber).IsFixedLength();
            entity.Property(e => e.Productcode).IsFixedLength();
            entity.Property(e => e.Segcode2).IsFixedLength();
            entity.Property(e => e.Subaccountcode).IsFixedLength();
            entity.Property(e => e.Tdscertificateno).IsFixedLength();
            entity.Property(e => e.Tdscode).IsFixedLength();
            entity.Property(e => e.VchrCategory).IsFixedLength();
            entity.Property(e => e.VchrNarration).IsFixedLength();
            entity.Property(e => e.VchrNumber).IsFixedLength();
            entity.Property(e => e.VchrRefnumber).IsFixedLength();
            entity.Property(e => e.VchrSyscategory).IsFixedLength();
            entity.Property(e => e.VchrType).IsFixedLength();
        });

        modelBuilder.Entity<CfnBillBk>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BILL")
                .HasFillFactor(90);

            entity.Property(e => e.Bankdocumentno).IsFixedLength();
            entity.Property(e => e.Billno).IsFixedLength();
            entity.Property(e => e.Costcentrecode).IsFixedLength();
            entity.Property(e => e.Costtype).IsFixedLength();
            entity.Property(e => e.CtrlAccperiod).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLocationcode).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlTrglocationcode).IsFixedLength();
            entity.Property(e => e.CtrlUsername).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Employeecode).IsFixedLength();
            entity.Property(e => e.Expensetype).IsFixedLength();
            entity.Property(e => e.Lcnumber).IsFixedLength();
            entity.Property(e => e.LstCategory).IsFixedLength();
            entity.Property(e => e.Ponumber).IsFixedLength();
            entity.Property(e => e.Productcode).IsFixedLength();
            entity.Property(e => e.Segcode2).IsFixedLength();
            entity.Property(e => e.VchrCategory).IsFixedLength();
            entity.Property(e => e.VchrRefnumber).IsFixedLength();
            entity.Property(e => e.VchrSyscategory).IsFixedLength();
            entity.Property(e => e.VchrType).IsFixedLength();
        });

        modelBuilder.Entity<CfnBilladjustment>(entity =>
        {
            entity.HasKey(e => e.Billserialno)
                .HasName("PKY_BILLADJUSTMENTS")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBillpassingaccount>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BILLPASSACNT")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBillpassingdtl>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("FKY_BILLPASSINGDTL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBillpassinghdr>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_BILLPASSINGHDR")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnBnkddetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BNKDDETAIL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBnkdocdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Billreference })
                .HasName("PKY_BNKDOCDETAIL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBnkpdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BNKPDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBnkrdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BNKRDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBnkretdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Bankdocumentno })
                .HasName("PKY_BNKRETDT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBnkretirement>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_BNKRETIREMENT")
                .HasFillFactor(90);

            entity.Property(e => e.Banktype).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnBpsyscatlinkdetail>(entity =>
        {
            entity.HasKey(e => new { e.CrVchrNumber, e.BpVchrNumber, e.CrSequenceno, e.BpSequenceno })
                .HasName("PKY_BPSYSCATEGORYLINK")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBptdsded>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BPTDSDED")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBptdshdr>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BPTDSHDR")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBscsexport>(entity =>
        {
            entity.HasKey(e => e.Accountcode)
                .HasName("CFN_BSCSEXPORTs")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBtlcustomerorder>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_BTLCUST")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Orderbilling).IsFixedLength();
        });

        modelBuilder.Entity<CfnBtldepositdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.DepositCtrlno, e.Amendmentnumber })
                .HasName("PKY_BTLDEPDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.RequiredFor).IsFixedLength();
        });

        modelBuilder.Entity<CfnBtldeposithd>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber })
                .HasName("PKY_BTLDEPOSITHD")
                .HasFillFactor(90);

            entity.Property(e => e.DepositType).IsFixedLength();
            entity.Property(e => e.PayableType).IsFixedLength();
            entity.Property(e => e.RequiredFor).IsFixedLength();
        });

        modelBuilder.Entity<CfnBtlhstcustomerorder>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber })
                .HasName("PKY_BTLHSTCUST")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Orderbilling).IsFixedLength();
        });

        modelBuilder.Entity<CfnBtlhstdepositdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.DepositCtrlno, e.Amendmentnumber })
                .HasName("PKY_BTLHSTDEPOSITDTL")
                .HasFillFactor(90);

            entity.Property(e => e.RequiredFor).IsFixedLength();
        });

        modelBuilder.Entity<CfnBtlhstdeposithd>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber })
                .HasName("PKY_BTLHSTDEPOSITHD")
                .HasFillFactor(90);

            entity.Property(e => e.DepositType).IsFixedLength();
            entity.Property(e => e.PayableType).IsFixedLength();
            entity.Property(e => e.RequiredFor).IsFixedLength();
        });

        modelBuilder.Entity<CfnBtlhstordrpricingdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Prodid })
                .HasName("PKY_BTLHSTPRICINGDTL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBtlhstordrpricinghd>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Amendmentnumber })
                .HasName("PKY_HSTPRICING")
                .HasFillFactor(90);

            entity.Property(e => e.Consigneeappl).IsFixedLength();
            entity.Property(e => e.Prodappl).IsFixedLength();
            entity.Property(e => e.Scheduletype).IsFixedLength();
            entity.Property(e => e.Termappl).IsFixedLength();
        });

        modelBuilder.Entity<CfnBtlhstordrpricingproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid, e.Amendmentnumber })
                .HasName("PKY_BTLHSTORDRPRICINGPRODUCT")
                .HasFillFactor(90);

            entity.Property(e => e.Extracted).IsFixedLength();
            entity.Property(e => e.Selected).IsFixedLength();
        });

        modelBuilder.Entity<CfnBtlhstprodspecification>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid, e.Specificationcode, e.Amendmentnumber })
                .HasName("PKY_BTLHSTPRODSPECIFICATION")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBtlhstterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber, e.Termcode, e.Termcodeseq })
                .HasName("PKY_BTLHSTTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBtlordoa>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_BTLORDOA")
                .HasFillFactor(90);

            entity.Property(e => e.Advancecollection).IsFixedLength();
            entity.Property(e => e.Advformat).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
            entity.Property(e => e.Customeracc).IsFixedLength();
            entity.Property(e => e.Internalacc).IsFixedLength();
            entity.Property(e => e.Negotiate).IsFixedLength();
            entity.Property(e => e.Stform).IsFixedLength();
        });

        modelBuilder.Entity<CfnBtlordoadtl>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Locationcode })
                .HasName("PKY_BTLOADTL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBtlordrpricingdetail>(entity =>
        {
            entity.HasKey(e => new { e.Scheduleid, e.Prodid, e.CtrlOnholdno })
                .HasName("PKY_BTLORDRPRICINGDETAIL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBtlordrpricinghd>(entity =>
        {
            entity.HasKey(e => new { e.Scheduleid, e.CtrlOnholdno })
                .HasName("PKY_BTLORDRPRICINGHD")
                .HasFillFactor(90);

            entity.Property(e => e.Consigneeappl).IsFixedLength();
            entity.Property(e => e.Prodappl).IsFixedLength();
            entity.Property(e => e.Scheduletype).IsFixedLength();
            entity.Property(e => e.Termappl).IsFixedLength();
        });

        modelBuilder.Entity<CfnBtlordrpricingproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid })
                .HasName("PKY_BTLORDRPRICINGPRODUCT")
                .HasFillFactor(90);

            entity.Property(e => e.Extracted).IsFixedLength();
            entity.Property(e => e.Selected).IsFixedLength();
        });

        modelBuilder.Entity<CfnBtlordrprodspecification>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid, e.Specificationcode })
                .HasName("PKY_BTLORDRPRODSPECIFICATION")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBtlordrterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Termcode, e.Termcodeseq })
                .HasName("PKY_BTLORDRTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBudget>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_BUDGET")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBudgetbreakup>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BUDGETBREAKUP")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBudgetrevision>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BUDGETREVISION")
                .HasFillFactor(90);

            entity.Property(e => e.Depletedenchan).IsFixedLength();
        });

        modelBuilder.Entity<CfnBudgetrevisiondtl>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlHdrsequenceno, e.CtrlSequenceno, e.Accperiod })
                .HasName("PKY_BUDGETREVISIONDTL")
                .HasFillFactor(90);

            entity.Property(e => e.Edflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnBudgetsanction>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_BUDGETSANCTION")
                .HasFillFactor(90);

            entity.Property(e => e.Bscsflag).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnBudgetstack>(entity =>
        {
            entity.HasKey(e => e.CtrlSequenceno)
                .HasName("PKY_BUDGETSTACK")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnBudgettransaction>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnCashbook>(entity =>
        {
            entity.HasKey(e => e.Cashcontrolaccount)
                .HasName("PKY_CASHBOOK")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnCashpayment>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_CASHPAYMENT")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnCashreceipt>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_CASHRECEIPT")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnCcmail>(entity =>
        {
            entity.HasKey(e => new { e.Locationcode, e.Userid })
                .HasName("PKY_CCMAIL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnCfparameter>(entity =>
        {
            entity.HasKey(e => e.Parametergroup)
                .HasName("PKY_CFPARAMETER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnCfparvalue>(entity =>
        {
            entity.HasKey(e => new { e.Parametergroup, e.Parametercode })
                .HasName("PKY_CFPARVALUE")
                .HasFillFactor(90);

            entity.Property(e => e.Activestatus).IsFixedLength();
        });

        modelBuilder.Entity<CfnChequeparty>(entity =>
        {
            entity.ToView("cfn_chequeparty");
        });

        modelBuilder.Entity<CfnChequerequest>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_CHEQUEREQUEST")
                .HasFillFactor(90);

            entity.Property(e => e.Accclearance).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnChqrdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_CHQRDETAIL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnCnsgdelivery>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber, e.Productcode, e.Consigneecode, e.Deliverydate })
                .HasName("PKY_CNSGDELIVERY")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnCompany>(entity =>
        {
            entity.HasKey(e => e.Companycode)
                .HasName("PKY_COMPANAY")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnConaccfg>(entity =>
        {
            entity.HasKey(e => e.Accperiod)
                .HasName("PKY_CONACCFG")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnConacrepgl>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.Accountcode })
                .HasName("PKY_CONAC")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnConloccfg>(entity =>
        {
            entity.HasKey(e => e.Locationcode)
                .HasName("PKY_CONLOCCFG")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnConlocrepgl>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.CtrlLocationcode, e.Accountcode })
                .HasName("PKY_CONLOCREPGL")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnConsignee>(entity =>
        {
            entity.HasKey(e => new { e.Customercode, e.Consigneecode })
                .HasName("PKY_CONSIGNEE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnConstb>(entity =>
        {
            entity.HasKey(e => new { e.Locationname, e.Accperiod, e.Accountcode })
                .HasName("PKY_CONSTB")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnContra>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_CONTRA")
                .HasFillFactor(90);

            entity.Property(e => e.Chqauthorize).IsFixedLength();
            entity.Property(e => e.Chqgenerate).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnContradetl>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_CONTRADETL")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnCostcentre>(entity =>
        {
            entity.HasKey(e => e.Costcentrecode)
                .HasName("PKY_COSTCENTRE")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnCostdetail>(entity =>
        {
            entity.HasKey(e => new
            {
                e.CtrlOnholdno,
                e.CtrlSequenceno,
                e.CostSequenceno
            });

            entity.Property(e => e.ChkFlag).IsFixedLength();
            entity.Property(e => e.CtrlStatus).IsFixedLength();
        });

        modelBuilder.Entity<CfnCrdndetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_CRDNDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnCreditdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_CRDNDETAIL1")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnCreditgenfactor>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_CRNGENFACTOR1")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Fixedorvariable).IsFixedLength();
            entity.Property(e => e.Levelno).IsFixedLength();
            entity.Property(e => e.Percentageorvalue).IsFixedLength();
        });

        modelBuilder.Entity<CfnCreditheader>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_CREDITHEADER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnCreditinvcproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productid, e.Scheduleid })
                .HasName("PKY_INVCPRODUCT_03")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnCreditinvoicereg>(entity =>
        {
            entity.HasKey(e => new { e.Invoiceno, e.Sequenceno })
                .HasName("PKY_INVNOREG")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnCreditnote>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_CREDITNOTE")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnCshpdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_CSHPDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnCshrdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_CSHRDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnCurrency>(entity =>
        {
            entity.HasKey(e => e.Currencycode)
                .HasName("PKY_CURRENCY")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
            entity.Property(e => e.Enabled).IsFixedLength();
            entity.Property(e => e.Objectstatus).IsFixedLength();
        });

        modelBuilder.Entity<CfnCurrencydetl>(entity =>
        {
            entity.HasKey(e => new { e.Currencycode, e.Effectivefrom, e.Effectiveto })
                .HasName("pky_currencydetl")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnCustomer>(entity =>
        {
            entity.HasKey(e => e.Customercode)
                .HasName("PKY_CUSTOMER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnCustomerorder>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber })
                .HasName("PKY_CUSTOMERORDER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Orderbilling).IsFixedLength();
        });

        modelBuilder.Entity<CfnCustomerprodlink>(entity =>
        {
            entity.HasKey(e => new { e.Customercode, e.Productcode, e.Customerproductcode })
                .HasName("PKY_CUSTOMERPRODLINK")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnCustproduct>(entity =>
        {
            entity.HasKey(e => new { e.Customercode, e.Productcode })
                .HasName("PKY_CUSTPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnDealer>(entity =>
        {
            entity.HasKey(e => e.Dealercode)
                .HasName("PKY_DEALER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnDebitnote>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_DEBITNOTE")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnDebndetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_DEBNDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnDepartment>(entity =>
        {
            entity.HasKey(e => e.Departmentcode)
                .HasName("PKY_DEPARTMENT")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnDepositinfo>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Depositctrlno, e.Depositamendmentno })
                .HasName("PKY_DEPOSIT01")
                .HasFillFactor(90);

            entity.Property(e => e.Deposittype).IsFixedLength();
            entity.Property(e => e.Payabletype).IsFixedLength();
            entity.Property(e => e.Requiredfor).IsFixedLength();
        });

        modelBuilder.Entity<CfnEmployee>(entity =>
        {
            entity.HasKey(e => e.Employeecode)
                .HasName("PKY_EMPLOYEE")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnEnqpricingdetail>(entity =>
        {
            entity.HasKey(e => new { e.Scheduleid, e.Prodid, e.CtrlOnholdno })
                .HasName("PKY_ENQPRICINGDETAIL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnEnqpricinghd>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid })
                .HasName("PKY_PRICINGHD")
                .HasFillFactor(90);

            entity.Property(e => e.Prodappl).IsFixedLength();
            entity.Property(e => e.Scheduletype).IsFixedLength();
            entity.Property(e => e.Selectind).IsFixedLength();
            entity.Property(e => e.Termappl).IsFixedLength();
        });

        modelBuilder.Entity<CfnEnqpricingproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid })
                .HasName("PKY_ENQPRICINGPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnEnqprodspecification>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid, e.Specificationcode })
                .HasName("PKY_ENQPRODSPECIFICATION")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnEnqterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Termcode, e.Termcodeseq })
                .HasName("PKY_ENQTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnEnquiry>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_ENQUIRY")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnExtjvdatum>(entity =>
        {
            entity.HasKey(e => new { e.CtrlVchrno, e.CtrlSequenceno })
                .HasName("PKY_EXTJVDATA")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Updateflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnFactorformula>(entity =>
        {
            entity.HasKey(e => new { e.Pricingfactorcode, e.Formulaid, e.Sequence })
                .HasName("PKY_FACTORFORMULA")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnFactorformulaheader>(entity =>
        {
            entity.HasKey(e => new { e.Pricingfactorcode, e.Formulaid })
                .HasName("PKY_FACTORFORMULAHEADER")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnFinancialinst>(entity =>
        {
            entity.HasKey(e => e.Financialinstcode)
                .HasName("PKY_FINANCIALINST")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnFinancialyear>(entity =>
        {
            entity.HasKey(e => e.Financialyear)
                .HasName("PKY_FINANCIALYEAR")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnFinancialyear2>(entity =>
        {
            entity.HasKey(e => e.Financialyear)
                .HasName("PKY_FINANCIALYEAR2")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnFyrefcontrol>(entity =>
        {
            entity.HasKey(e => e.Financialyear)
                .HasName("PKY_FYREFCONTROL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnGeneralledger>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.Accountcode })
                .HasName("PKY_GENERALLEDGER")
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.Accperiod, e.Accountcode }, "fky_generalleger")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.Onholdcbdbcr).IsFixedLength();
            entity.Property(e => e.Onholdobdbcr).IsFixedLength();
            entity.Property(e => e.Postedcbdbcr).IsFixedLength();
            entity.Property(e => e.Postedobdbcr).IsFixedLength();
        });

        modelBuilder.Entity<CfnGeneralledger2>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.Accountcode })
                .HasName("PKY_GENERALLEDGER2")
                .HasFillFactor(90);

            entity.Property(e => e.Onholdcbdbcr).IsFixedLength();
            entity.Property(e => e.Onholdobdbcr).IsFixedLength();
            entity.Property(e => e.Postedcbdbcr).IsFixedLength();
            entity.Property(e => e.Postedobdbcr).IsFixedLength();
        });

        modelBuilder.Entity<CfnGenfactor>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_GENFACTOR")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Fixedorvariable).IsFixedLength();
            entity.Property(e => e.Levelno).IsFixedLength();
            entity.Property(e => e.Percentageorvalue).IsFixedLength();
        });

        modelBuilder.Entity<CfnGenhelp>(entity =>
        {
            entity.HasKey(e => e.Helpid)
                .HasName("PKY_GENHELP")
                .HasFillFactor(90);

            entity.Property(e => e.Regflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnGeography>(entity =>
        {
            entity.HasKey(e => e.Geographycode)
                .HasName("PKY_GEOGRAPHY")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnGivcdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_GIVCDTL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnGivsrno>(entity =>
        {
            entity.HasKey(e => new { e.Productcode, e.Serialno, e.CtrlOnholdno })
                .HasName("PK_PROD")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnGldetail>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.Accountcode, e.CtrlOnholdno, e.CtrlSequenceno, e.VchrType })
                .HasName("PKY_GLDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnGlsubledger>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.Accountcode, e.Subaccountcode })
                .HasName("PKY_GLSUBLEDGER")
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.Accperiod, e.Accountcode, e.Subaccountcode }, "fky_glsubledger")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.Onholdcbdbcr).IsFixedLength();
            entity.Property(e => e.Onholdobdbcr).IsFixedLength();
            entity.Property(e => e.Postedcbdbcr).IsFixedLength();
            entity.Property(e => e.Postedobdbcr).IsFixedLength();
        });

        modelBuilder.Entity<CfnGlsubledger2>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.Accountcode, e.Subaccountcode })
                .HasName("PKY_GLSUBLEDGER2")
                .HasFillFactor(90);

            entity.Property(e => e.Onholdcbdbcr).IsFixedLength();
            entity.Property(e => e.Onholdobdbcr).IsFixedLength();
            entity.Property(e => e.Postedcbdbcr).IsFixedLength();
            entity.Property(e => e.Postedobdbcr).IsFixedLength();
        });

        modelBuilder.Entity<CfnGlsummaryacc>(entity =>
        {
            entity.HasKey(e => new { e.Accountcode, e.Vchrtype })
                .HasName("PK_ACCVCH")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnGoodsinvch>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_GOODINVCH")
                .HasFillFactor(90);

            entity.Property(e => e.Billpassing).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnGoodsoutvch>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_GOODSOUTVCH")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnGovcdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productcode })
                .HasName("PKY_GOVCDETAIL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnGovsrno>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productcode, e.Serialno })
                .HasName("PK_GOVSRNO")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnHierarchy>(entity =>
        {
            entity.HasKey(e => e.Hierarchyid)
                .HasName("PKY_HIERARCHY")
                .HasFillFactor(90);

            entity.Property(e => e.Created).IsFixedLength();
        });

        modelBuilder.Entity<CfnHierarchylevel>(entity =>
        {
            entity.HasKey(e => new { e.Hierarchyid, e.Levelno })
                .HasName("PKY_HIERARCHYLEVEL")
                .HasFillFactor(90);

            entity.Property(e => e.Dependency).IsFixedLength();
        });

        modelBuilder.Entity<CfnHierarchylink>(entity =>
        {
            entity.HasKey(e => new { e.Hierarchyid, e.Viewid })
                .HasName("PKY_HIERARCHYLINK")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnHstcnsgdelivery>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber, e.Productcode, e.Consigneecode, e.Deliverydate })
                .HasName("PKY_HSTCNSGDELIVERY")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnHstcustomerorder>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber })
                .HasName("PKY_HSTCUSTOMERORDER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Orderbilling).IsFixedLength();
        });

        modelBuilder.Entity<CfnHstdepositinfo>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Depositctrlno, e.Depositamendmentno })
                .HasName("PKY_HSTDEPOSITINFO")
                .HasFillFactor(90);

            entity.Property(e => e.Deposittype).IsFixedLength();
            entity.Property(e => e.Payabletype).IsFixedLength();
            entity.Property(e => e.Requiredfor).IsFixedLength();
        });

        modelBuilder.Entity<CfnHstordrproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber, e.Productcode })
                .HasName("PKY_HSTORDRPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnHstordrterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber, e.Termcode })
                .HasName("PKY_HSTORDRTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnHstpogenfactor>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno, e.Amendmentnumber })
                .HasName("PKY_HSTPOGENFACTOR")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Fixedorvariable).IsFixedLength();
            entity.Property(e => e.Levelno).IsFixedLength();
            entity.Property(e => e.Percentageorvalue).IsFixedLength();
        });

        modelBuilder.Entity<CfnHstporatecontract>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber })
                .HasName("PKY_PORATECONTRACT")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnHstporatecontractdtl>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productid, e.Amendmentnumber })
                .HasName("PKY_DTL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnHstprodconsignee>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber, e.Productcode, e.Consigneecode })
                .HasName("PKY_HSTPRODCONSIGNEE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnHstpurchasedetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno, e.Amendmentnumber })
                .HasName("PKY_HSTPODTL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnHstpurchaseorder>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber })
                .HasName("PKY_HSTPOHDR")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Qtyvariance).IsFixedLength();
            entity.Property(e => e.Ratevariance).IsFixedLength();
        });

        modelBuilder.Entity<CfnHstpurchaseterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Termcode, e.Termcodeseq, e.Amendmentnumber })
                .HasName("PKY_HSTPOTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnHstquotation>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber })
                .HasName("PKY_HSTQUOTATION")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Enquirybase).IsFixedLength();
        });

        modelBuilder.Entity<CfnHstquotpricinghd>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Amendmentnumber })
                .HasName("PKY_HSTQUOTPRICINGHD")
                .HasFillFactor(90);

            entity.Property(e => e.Prodappl).IsFixedLength();
            entity.Property(e => e.Scheduletype).IsFixedLength();
            entity.Property(e => e.Termappl).IsFixedLength();
        });

        modelBuilder.Entity<CfnHstquotpricingproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid, e.Amendmentnumber })
                .HasName("PKY_HSTQUOTPRICINGPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnHstquotprodspecification>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid, e.Specificationcode, e.Amendmentnumber })
                .HasName("PKY_HSTQUOTPRODSPECIFICATION")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnHstquotterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Termcode, e.Termcodeseq, e.Amendmentnumber })
                .HasName("PKY_HSTQUOTTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnIndtproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productcode })
                .HasName("PKY_INDTPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnInstallparam>(entity =>
        {
            entity.HasKey(e => new { e.Companycode, e.Locationcode })
                .HasName("PKY_INSTALLPARAM")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
            entity.Property(e => e.Editinvoicedate).IsFixedLength();
            entity.Property(e => e.Netoffbp).IsFixedLength();
            entity.Property(e => e.Netoffbr).IsFixedLength();
            entity.Property(e => e.Netoffcn).IsFixedLength();
            entity.Property(e => e.Netoffcp).IsFixedLength();
            entity.Property(e => e.Netoffcr).IsFixedLength();
            entity.Property(e => e.Netoffdn).IsFixedLength();
            entity.Property(e => e.Roundofaccount).IsFixedLength();
            entity.Property(e => e.TdsprefixAccountcode).IsFixedLength();
        });

        modelBuilder.Entity<CfnInstrument>(entity =>
        {
            entity.ToView("Cfn_Instrument");

            entity.Property(e => e.BillCtrlonholdno).IsFixedLength();
            entity.Property(e => e.GinJinNo).IsFixedLength();
        });

        modelBuilder.Entity<CfnInterbdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Accountcode })
                .HasName("PKY_INTERBDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnInterbranch>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_INTERBRANCH")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnIntracc>(entity =>
        {
            entity.Property(e => e.Cmanupdflag).IsFixedLength();
            entity.Property(e => e.Famupdflag).IsFixedLength();
            entity.Property(e => e.Lcupdflag).IsFixedLength();
            entity.Property(e => e.Payupdflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnInvaccdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_INVDET")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnInvcprncfg>(entity =>
        {
            entity.HasKey(e => e.Pricingfactorcode)
                .HasName("PKY_FACTORCODE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnInvcproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productid, e.Scheduleid })
                .HasName("PKY_INVCPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnInvcterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Termcode, e.Termcodeseq })
                .HasName("PKY_INVCTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnInvoice>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_INVOICE")
                .HasFillFactor(90);

            entity.Property(e => e.ConsigneeFlag).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Invoicecancelflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnInvoicereg>(entity =>
        {
            entity.HasKey(e => new { e.Invoiceno, e.Sequenceno })
                .HasName("PK_INVOICEREG")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnInvorderrelated>(entity =>
        {
            entity.HasKey(e => e.Serialno)
                .HasName("PKY_INTERMEDIATE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnInvreceipt>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_INVRECEIPT")
                .HasFillFactor(90);

            entity.Property(e => e.Advanceorinvoice).IsFixedLength();
            entity.Property(e => e.Chequeorcash).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnInvregcfg>(entity =>
        {
            entity.HasKey(e => e.Pricingfactorcode)
                .HasName("PKY_PRICINGFACTORCODE")
                .HasFillFactor(90);

            entity.Property(e => e.Pricingfactorcode).IsFixedLength();
            entity.Property(e => e.Columnname).IsFixedLength();
        });

        modelBuilder.Entity<CfnInvvchrhist>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_INVVCHRHIST")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnInvvchrtype>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_INVVCHRTYPE")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnJournal>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_JOURNAL")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnJrnldetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_JRNLDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnLevel>(entity =>
        {
            entity.HasKey(e => e.Levelnumber)
                .HasName("PKY_LEVEL")
                .HasFillFactor(90);

            entity.Property(e => e.Levelnumber).IsFixedLength();
        });

        modelBuilder.Entity<CfnLocation>(entity =>
        {
            entity.HasKey(e => e.Locationcode)
                .HasName("PKY_LOCATION")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
            entity.Property(e => e.TdsRemitance).IsFixedLength();
        });

        modelBuilder.Entity<CfnLocnassociated>(entity =>
        {
            entity.HasKey(e => new { e.Locationcode, e.Associatedlocation })
                .HasName("PKY_LOCNASSOCIATED")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnLocnbatch>(entity =>
        {
            entity.HasKey(e => new { e.Locationcode, e.Accperiod, e.Batchserialno })
                .HasName("PKY_LOCNBATCH")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnLocndepartment>(entity =>
        {
            entity.HasKey(e => new { e.Locationcode, e.Departmentcode })
                .HasName("PKY_LOCNDEPARTMENT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnMemovoucher>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_MEMOVOUCHER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnMemvdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_MEMVDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnMrgquotpricinghd>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid })
                .HasName("PKY_MRGQUOTPRICINGHD")
                .HasFillFactor(90);

            entity.Property(e => e.Prodappl).IsFixedLength();
            entity.Property(e => e.Scheduletype).IsFixedLength();
            entity.Property(e => e.Termappl).IsFixedLength();
        });

        modelBuilder.Entity<CfnMrgquotpricingproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid })
                .HasName("PKY_MRGQUOTPRICINGPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnMrpbillheader>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_MRPBILLHDR")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnMrpbillitemdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BILLPASSINGDTL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnMrpbillpassingaccount>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_BILLPASSINGACC")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnOldNew>(entity =>
        {
            entity.Property(e => e.NewCost).IsFixedLength();
            entity.Property(e => e.OldCost).IsFixedLength();
        });

        modelBuilder.Entity<CfnOrdergenfactor>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_ORDERGENFACTOR")
                .HasFillFactor(90);

            entity.Property(e => e.Factorlevelno).IsFixedLength();
            entity.Property(e => e.Percentageorvalue).IsFixedLength();
        });

        modelBuilder.Entity<CfnOrderpricingfactor>(entity =>
        {
            entity.HasKey(e => e.Pricingfactorcode)
                .HasName("PKY_ORDERPRICINGFACTOR")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnOrdertaxfactor>(entity =>
        {
            entity.HasKey(e => new { e.Factorlevelcode, e.Factorcode })
                .HasName("PKY_ORDERTAXFACTOR")
                .HasFillFactor(90);

            entity.Property(e => e.Factorlevelno).IsFixedLength();
            entity.Property(e => e.Percentageorvalue).IsFixedLength();
        });

        modelBuilder.Entity<CfnOrdrgenfactor>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_ORDRGENFACTOR")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Fixedorvariable).IsFixedLength();
            entity.Property(e => e.Levelno).IsFixedLength();
            entity.Property(e => e.Percentageorvalue).IsFixedLength();
        });

        modelBuilder.Entity<CfnOrdrproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber, e.Productcode })
                .HasName("PKY_ORDRPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnOrdrschproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productid })
                .HasName("PKY_ORDRSCHPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnOrdrterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber, e.Termcode })
                .HasName("PKY_ORDRTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPackage>(entity =>
        {
            entity.HasKey(e => e.Packagetype)
                .HasName("PKY_PACKAGE")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnPanel>(entity =>
        {
            entity.HasKey(e => new { e.Taskid, e.Panelid })
                .HasName("PKY_PANEL")
                .HasFillFactor(90);

            entity.Property(e => e.Logflag).IsFixedLength();
            entity.Property(e => e.Nextlevel).IsFixedLength();
            entity.Property(e => e.Panelorrefr).IsFixedLength();
        });

        modelBuilder.Entity<CfnPanelpermission>(entity =>
        {
            entity.HasKey(e => new { e.Username, e.Levelnumber, e.Taskid, e.Panelid })
                .HasName("PKY_PANELPERMISSION")
                .HasFillFactor(90);

            entity.Property(e => e.Levelnumber).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnPayment>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_PAYMENTS")
                .HasFillFactor(90);

            entity.HasIndex(e => e.Paymentamountbalance, "ndx_paymentbalance").HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnPhysdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productcode })
                .HasName("PKY_PHYSDETAIL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPhystockvch>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_PHYSTOCKVCH")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnPlaccount>(entity =>
        {
            entity.HasKey(e => new { e.Accountcode, e.Groupcode })
                .HasName("PKY_PLACCOUNT")
                .HasFillFactor(90);

            entity.Property(e => e.Cbdbcr).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnPlaccountOld>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.Groupcode, e.Schdcode, e.Accountcode })
                .HasName("pky_placcount_old")
                .IsClustered(false)
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPlgroup>(entity =>
        {
            entity.HasKey(e => e.Groupcode)
                .HasName("PKY_PLGROUP")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnPoenquiry>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_POENQUIRY")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnPoenquiryprlink>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Reqonholdno, e.Productid })
                .HasName("PKY_ENQUIRYPR")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPoenquiryvendor>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_ENQVENDOR")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPogenfactor>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_POGENFACTOR")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Fixedorvariable).IsFixedLength();
            entity.Property(e => e.Levelno).IsFixedLength();
            entity.Property(e => e.Percentageorvalue).IsFixedLength();
        });

        modelBuilder.Entity<CfnPoprvchrtype>(entity =>
        {
            entity.HasKey(e => new { e.Povchrtype, e.Prvchrtype })
                .HasName("PKY_POPRVCHRTYPE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPoquotation>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_POQUOT")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Enquirybase).IsFixedLength();
        });

        modelBuilder.Entity<CfnPoquotationdtl>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_POQUOTATIONDTL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPoquotgenfactor>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_POQUOTGEN")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Fixedorvariable).IsFixedLength();
            entity.Property(e => e.Levelno).IsFixedLength();
            entity.Property(e => e.Percentageorvalue).IsFixedLength();
        });

        modelBuilder.Entity<CfnPoquotterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Termcode, e.Termcodeseq })
                .HasName("PKY_POQTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPoratecontract>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_PORATE")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnPoratecontractdtl>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productid })
                .HasName("PKY_PORATEDTL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPostagetrack>(entity =>
        {
            entity.HasKey(e => e.Ponumber)
                .HasName("PKY_POSTAGE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPricingfactor>(entity =>
        {
            entity.HasKey(e => new { e.Pricingfactorcode, e.CtrlOnholdno })
                .HasName("PKY_PRICINGFACTOR")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Levelno).IsFixedLength();
        });

        modelBuilder.Entity<CfnPrintfactor>(entity =>
        {
            entity.Property(e => e.Factor).IsFixedLength();
        });

        modelBuilder.Entity<CfnProdconsignee>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Amendmentnumber, e.Productcode, e.Consigneecode })
                .HasName("PKY_PRODCONSIGNEE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnProddelivery>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productcode, e.Deliverydate })
                .HasName("PKY_PRODDELIVERY")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnProdmergsrno>(entity =>
        {
            entity.HasKey(e => new { e.Productcode, e.Serialno, e.CtrlOnholdno })
                .HasName("PK_PRODMERGSRNO")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnProdspeclink>(entity =>
        {
            entity.HasKey(e => new { e.Specificationcode, e.Productcode })
                .HasName("PKY_PRODSPECLINK")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnProduct>(entity =>
        {
            entity.HasKey(e => e.Productcode)
                .HasName("PKY_PRODUCT")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
            entity.Property(e => e.Productabc).IsFixedLength();
            entity.Property(e => e.Quotebasedratecontract).IsFixedLength();
            entity.Property(e => e.Serialnoappl).IsFixedLength();
            entity.Property(e => e.Stock).IsFixedLength();
        });

        modelBuilder.Entity<CfnProductmerg>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_PRODUCTMERG")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnProductstock>(entity =>
        {
            entity.HasKey(e => new { e.Productcode, e.Stocktype, e.Warehousecode, e.Accperiod })
                .HasName("pky_productstock")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnProductstockSummary>(entity =>
        {
            entity.HasKey(e => new { e.Productcode, e.Stocktype, e.Warehousecode, e.Accperiod })
                .HasName("PKY_PRODUCTSTOCK_SUMMARY")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnProducttransfer>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productcode })
                .HasName("PKY_PRODTRANSFER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlTrglogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnProducttransferdtl>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productcode })
                .HasName("PKY_PRTRDT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnProductwarehouse>(entity =>
        {
            entity.HasKey(e => new { e.Productcode, e.Warehousecode })
                .HasName("PK_PRODUCTWARECODE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPurchaseRegister>(entity =>
        {
            entity.Property(e => e.IsSelected).IsFixedLength();
        });

        modelBuilder.Entity<CfnPurchaseRegisterBilzuser>(entity =>
        {
            entity.Property(e => e.IsSelected)
                .HasDefaultValue("N")
                .IsFixedLength();
        });

        modelBuilder.Entity<CfnPurchasedetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_PODTL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPurchasejnl>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_PURCHASEJNL")
                .HasFillFactor(90);

            entity.Property(e => e.Billpassingappl).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnPurchaseprint>(entity =>
        {
            entity.HasKey(e => new { e.VchrNumber, e.Sequenceno })
                .HasName("PKY_PURCHASEPRINT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPurchaseterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Termcode, e.Termcodeseq })
                .HasName("PKY_POTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnPurjdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno, e.Accountcode })
                .HasName("pky_purjdetail")
                .IsClustered(false)
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnQuerytask>(entity =>
        {
            entity.HasKey(e => e.Taskfullname)
                .HasName("PKY_QUERYTASKS")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnQuotation>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_QUOTATION")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Enquirybase).IsFixedLength();
        });

        modelBuilder.Entity<CfnQuotdepdetail>(entity =>
        {
            entity.Property(e => e.RequiredFor).IsFixedLength();
        });

        modelBuilder.Entity<CfnQuotdeposithd>(entity =>
        {
            entity.Property(e => e.DepositType).IsFixedLength();
            entity.Property(e => e.PayableType).IsFixedLength();
        });

        modelBuilder.Entity<CfnQuotgenfactor>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_QUOTGENFACTOR")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Fixedorvariable).IsFixedLength();
            entity.Property(e => e.Levelno).IsFixedLength();
            entity.Property(e => e.Percentageorvalue).IsFixedLength();
        });

        modelBuilder.Entity<CfnQuotpricinghd>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid })
                .HasName("PKY_QUOTPRICINGHD")
                .HasFillFactor(90);

            entity.Property(e => e.Prodappl).IsFixedLength();
            entity.Property(e => e.Scheduletype).IsFixedLength();
            entity.Property(e => e.Termappl).IsFixedLength();
        });

        modelBuilder.Entity<CfnQuotpricingproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid })
                .HasName("PKY_QUOTPRICINGPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnQuotprodspecification>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid, e.Specificationcode })
                .HasName("PKY_QUOTPRODSPECIFICATION")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnQuotterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Termcode, e.Termcodeseq })
                .HasName("PKY_QUOTTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnRcgenfactor>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_RCGENFACTOR")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Fixedorvariable).IsFixedLength();
            entity.Property(e => e.Levelno).IsFixedLength();
            entity.Property(e => e.Percentageorvalue).IsFixedLength();
        });

        modelBuilder.Entity<CfnRectemplate>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_RECTEMPLATE")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnRectempldetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_RECTEMPLDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnRecurringvchr>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_RECURRINGVCHR")
                .HasFillFactor(90);

            entity.Property(e => e.Automatedapplflag).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnRecurringvchrdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_RECURRINGVCHRDETAIL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnRecvchrtemplate>(entity =>
        {
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Fixedorvariable).IsFixedLength();
            entity.Property(e => e.Obsoleteflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnRecvoucher>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlAccperiod })
                .HasName("PKY_RECVOUCHER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnReferencectrl>(entity =>
        {
            entity.HasKey(e => e.Referencetype)
                .HasName("PKY_REFERENCECTRL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnRepageingtemplate>(entity =>
        {
            entity.HasKey(e => e.Srrecno)
                .HasName("PYY_REPAGE")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnRepbcdaybook>(entity =>
        {
            entity.HasKey(e => new { e.VchrDate, e.BankCashAc })
                .HasName("PK_REPBCDAYBOOK")
                .HasFillFactor(90);

            entity.Property(e => e.BankCashType).IsFixedLength();
            entity.Property(e => e.CbDbcrflag).IsFixedLength();
            entity.Property(e => e.ObDbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnRepbcdetail>(entity =>
        {
            entity.HasKey(e => new { e.Bankaccount, e.BankSequenceno })
                .HasName("PKY_REPBCDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnRepcontb>(entity =>
        {
            entity.HasKey(e => e.Accountcode)
                .HasName("PKY_REPCONTB")
                .HasFillFactor(90);

            entity.Property(e => e.CbDbcr1).IsFixedLength();
            entity.Property(e => e.CbDbcr10).IsFixedLength();
            entity.Property(e => e.CbDbcr11).IsFixedLength();
            entity.Property(e => e.CbDbcr12).IsFixedLength();
            entity.Property(e => e.CbDbcr13).IsFixedLength();
            entity.Property(e => e.CbDbcr14).IsFixedLength();
            entity.Property(e => e.CbDbcr15).IsFixedLength();
            entity.Property(e => e.CbDbcr16).IsFixedLength();
            entity.Property(e => e.CbDbcr17).IsFixedLength();
            entity.Property(e => e.CbDbcr18).IsFixedLength();
            entity.Property(e => e.CbDbcr19).IsFixedLength();
            entity.Property(e => e.CbDbcr2).IsFixedLength();
            entity.Property(e => e.CbDbcr20).IsFixedLength();
            entity.Property(e => e.CbDbcr3).IsFixedLength();
            entity.Property(e => e.CbDbcr4).IsFixedLength();
            entity.Property(e => e.CbDbcr5).IsFixedLength();
            entity.Property(e => e.CbDbcr6).IsFixedLength();
            entity.Property(e => e.CbDbcr7).IsFixedLength();
            entity.Property(e => e.CbDbcr8).IsFixedLength();
            entity.Property(e => e.CbDbcr9).IsFixedLength();
            entity.Property(e => e.TotDbcr).IsFixedLength();
        });

        modelBuilder.Entity<CfnRepgeneralledger>(entity =>
        {
            entity.HasKey(e => new { e.Accountcode, e.Accperiod })
                .HasName("PKY_REPGENERALLEDGER")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnRepgldetail>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.Accountcode, e.CtrlOnholdno, e.CtrlSequenceno, e.VchrType })
                .HasName("PKY_REPGLDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnReportcontrol>(entity =>
        {
            entity.HasKey(e => e.Taskid)
                .HasName("PKY_REPORTCONTROL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnReportcontrol1>(entity =>
        {
            entity.Property(e => e.Exclude).IsFixedLength();
            entity.Property(e => e.Reportlongdesc).IsFixedLength();
            entity.Property(e => e.Reportobject).IsFixedLength();
            entity.Property(e => e.Reporttitle).IsFixedLength();
            entity.Property(e => e.Taskid).IsFixedLength();
        });

        modelBuilder.Entity<CfnRequisition>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_REQ01")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Quotebasedratecontract).IsFixedLength();
        });

        modelBuilder.Entity<CfnRequisitiondtl>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productid })
                .HasName("PKY_REQ02")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnSalescnsgdelivery>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Productcode, e.Salescustomercode, e.Deliverydate })
                .HasName("PKY_SALESCNSGDELIVERY")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnSalescustomer>(entity =>
        {
            entity.HasKey(e => e.Salescustomercode)
                .HasName("PKY_SALESCUSTOMER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnSalesindent>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_SALESINDENT")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnSalesprodconsignee>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid, e.Salescustomercode })
                .HasName("PKY_SALESPRODCONSIGNEE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnSalevoucher>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_SALEVOUCHER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnSalvdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_SALVDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnSandetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_SANDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Percentageorvalue).IsFixedLength();
        });

        modelBuilder.Entity<CfnSerialamendment>(entity =>
        {
            entity.HasKey(e => e.Slno)
                .HasName("PKY_AMENDSERIALNO")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnSerialnohistory>(entity =>
        {
            entity.HasKey(e => new { e.Serialno, e.CtrlOnholdno })
                .HasName("PKY_SERIALNOHISTORY")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnServicenote>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_SERVICENOTE")
                .HasFillFactor(90);

            entity.Property(e => e.Billpassing).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnSpecification>(entity =>
        {
            entity.HasKey(e => e.Specificationcode)
                .HasName("PKY_SPECIFICATION")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnStocksrno>(entity =>
        {
            entity.HasKey(e => new { e.Productcode, e.Serialno })
                .HasName("PRMKSERIALNO")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnTask>(entity =>
        {
            entity.HasKey(e => e.Taskid)
                .HasName("PKY_TASK")
                .HasFillFactor(90);

            entity.Property(e => e.Logflag).IsFixedLength();
            entity.Property(e => e.Nextlevel).IsFixedLength();
            entity.Property(e => e.Taskorrefr).IsFixedLength();
        });

        modelBuilder.Entity<CfnTask1>(entity =>
        {
            entity.Property(e => e.Logflag).IsFixedLength();
            entity.Property(e => e.Nextlevel).IsFixedLength();
            entity.Property(e => e.Objectstatus).IsFixedLength();
            entity.Property(e => e.Taskdescription).IsFixedLength();
            entity.Property(e => e.Taskfullname).IsFixedLength();
            entity.Property(e => e.Taskheader).IsFixedLength();
            entity.Property(e => e.Taskid).IsFixedLength();
            entity.Property(e => e.Taskinterfacerefr).IsFixedLength();
            entity.Property(e => e.Taskorrefr).IsFixedLength();
            entity.Property(e => e.Taskpicture).IsFixedLength();
            entity.Property(e => e.Taskprimarykey).IsFixedLength();
            entity.Property(e => e.Taskshortname).IsFixedLength();
            entity.Property(e => e.Tasktype).IsFixedLength();
            entity.Property(e => e.Vouchergroup).IsFixedLength();
        });

        modelBuilder.Entity<CfnTaxfactor>(entity =>
        {
            entity.HasKey(e => new { e.Pricingfactorcode, e.Factorlevelcode, e.CtrlOnholdno })
                .HasName("PKY_PF")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
            entity.Property(e => e.Fixedorvariable).IsFixedLength();
            entity.Property(e => e.Percentageorvalue).IsFixedLength();
        });

        modelBuilder.Entity<CfnTd>(entity =>
        {
            entity.HasKey(e => e.Tdscode)
                .HasName("PKY_TDS")
                .HasFillFactor(90);

            entity.Property(e => e.CodeType).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnTdsaccountslno>(entity =>
        {
            entity.HasKey(e => e.Accountcode)
                .HasName("CFN_TDSACCOUTNSLNO")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnTdsbillpayment>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_TDS_BILLPAYMENT")
                .HasFillFactor(90);

            entity.Property(e => e.BillpaymentFlag).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnTdsdeduction>(entity =>
        {
            entity.HasKey(e => new { e.Vendorcode, e.Tdscode, e.CtrlSequenceno, e.CtrlAccperiod })
                .HasName("PKY_TDSDEDUCTION")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnTdsdeposit>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.Tdscode })
                .HasName("PK_A")
                .IsClustered(false)
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnTempcost>(entity =>
        {
            entity.Property(e => e.NCostcode).IsFixedLength();
            entity.Property(e => e.NCostdesc).IsFixedLength();
        });

        modelBuilder.Entity<CfnTendcompprodspec>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid, e.Specificationcode })
                .HasName("PKY_TENDCOMPPRODSPEC")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnTender>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_TENDER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Despatchmode).IsFixedLength();
            entity.Property(e => e.Paymentmode).IsFixedLength();
            entity.Property(e => e.PerfGuarantee).IsFixedLength();
            entity.Property(e => e.PerfType).IsFixedLength();
            entity.Property(e => e.PriceBasis).IsFixedLength();
            entity.Property(e => e.ScrutinyType).IsFixedLength();
            entity.Property(e => e.SecurityDeposit).IsFixedLength();
            entity.Property(e => e.SecurityType).IsFixedLength();
            entity.Property(e => e.Tendercategory).IsFixedLength();
        });

        modelBuilder.Entity<CfnTendercompetitor>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_TENDERCOMPETITOR")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnTendercomppricingdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Prodid })
                .HasName("PKY_TENDERCOMPRICINGDETAIL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnTendercomppricinghd>(entity =>
        {
            entity.HasKey(e => new { e.Scheduleid, e.CtrlOnholdno })
                .HasName("PKY_TENDERCOMPPRICINGHD")
                .HasFillFactor(90);

            entity.Property(e => e.Prodappl).IsFixedLength();
            entity.Property(e => e.Scheduletype).IsFixedLength();
            entity.Property(e => e.Termappl).IsFixedLength();
        });

        modelBuilder.Entity<CfnTendercomppricingproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid })
                .HasName("PKY_TENDERCOMPPRICINGPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnTendercompterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Termcode, e.Termcodeseq })
                .HasName("PKY_TENDERCOMPTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnTenderdepositdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.DepositCtrlno })
                .HasName("PKY_TENDERDEPDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.RequiredFor).IsFixedLength();
        });

        modelBuilder.Entity<CfnTenderdeposithd>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_TENDERDEPOSITHD")
                .HasFillFactor(90);

            entity.Property(e => e.DepositType).IsFixedLength();
            entity.Property(e => e.PayableType).IsFixedLength();
            entity.Property(e => e.RequiredFor).IsFixedLength();
        });

        modelBuilder.Entity<CfnTenderpricingdetail>(entity =>
        {
            entity.HasKey(e => new { e.Scheduleid, e.Prodid, e.CtrlOnholdno })
                .HasName("PKY_TENDERPRICINGDETAIL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnTenderpricinghd>(entity =>
        {
            entity.HasKey(e => new { e.Scheduleid, e.CtrlOnholdno })
                .HasName("PKY_TENDERPRICINGHD")
                .HasFillFactor(90);

            entity.Property(e => e.Prodappl).IsFixedLength();
            entity.Property(e => e.Scheduletype).IsFixedLength();
            entity.Property(e => e.Termappl).IsFixedLength();
        });

        modelBuilder.Entity<CfnTenderpricingproduct>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid })
                .HasName("PKY_TENDERPRICINGPRODUCT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnTenderprodspecification>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Scheduleid, e.Productid, e.Specificationcode })
                .HasName("PKY_TENDERPRODSPECIFICATION")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnTenderterm>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.Termcode, e.Termcodeseq })
                .HasName("PKY_TENDERTERM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnTerm>(entity =>
        {
            entity.HasKey(e => e.Termcode)
                .HasName("PKY_TERM")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnTermvalue>(entity =>
        {
            entity.HasKey(e => new { e.Termcode, e.Termcodeseq })
                .HasName("PKY_TERMVALUE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnTesttask>(entity =>
        {
            entity.Property(e => e.Logflag).IsFixedLength();
            entity.Property(e => e.Nextlevel).IsFixedLength();
            entity.Property(e => e.Taskorrefr).IsFixedLength();
        });

        modelBuilder.Entity<CfnTrLink>(entity =>
        {
            entity.HasKey(e => e.Accountcode)
                .HasName("pky_tr_link")
                .HasFillFactor(90);

            entity.Property(e => e.TrnsType).IsFixedLength();
        });

        modelBuilder.Entity<CfnTranslation>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.Accountcode, e.FlctAccount })
                .HasName("pky_translation")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnTranslationBill>(entity =>
        {
            entity.HasKey(e => new { e.Accperiod, e.VchrNumber, e.JvCtrlOnholdno })
                .HasName("pky_translation_bill")
                .HasFillFactor(90);

            entity.Property(e => e.ApArType).IsFixedLength();
        });

        modelBuilder.Entity<CfnTravelsanc>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_TRAVELSANC")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.Settlementflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnTravelvoucher>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_TRAVELVOUCHER")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnTrvldetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno })
                .HasName("PKY_TRVLDETAIL")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnUser>(entity =>
        {
            entity.HasKey(e => e.Username)
                .HasName("PK_USERNAME")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnUsercostcentre>(entity =>
        {
            entity.HasKey(e => new { e.Username, e.Costcentrecode })
                .HasName("PKY_USERCOSTCENTRE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnUserpermission>(entity =>
        {
            entity.HasKey(e => new { e.Username, e.Taskid })
                .HasName("PKY_USERPERMISSION")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
            entity.Property(e => e.Levelnumber).IsFixedLength();
        });

        modelBuilder.Entity<CfnUservchr>(entity =>
        {
            entity.HasKey(e => new { e.Username, e.Vouchergroup, e.Vouchertype })
                .HasName("PKY_USERVCHR")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnUservchrsyscat>(entity =>
        {
            entity.HasKey(e => new { e.Username, e.Parametergroup, e.Parametercode })
                .HasName("PKY_USERVCHRSYSCAT")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnUserwarehouse>(entity =>
        {
            entity.HasKey(e => new { e.Username, e.Warehousecode })
                .HasName("PK_USERWARE")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnVBankHeader>(entity =>
        {
            entity.ToView("cfn_v_bank_header");
        });

        modelBuilder.Entity<CfnVBankbook>(entity =>
        {
            entity.ToView("cfn_v_bankbook");

            entity.Property(e => e.Dbcrindication).IsFixedLength();
        });

        modelBuilder.Entity<CfnVBankpayment>(entity =>
        {
            entity.ToView("cfn_v_bankpayment");

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Cancelflag).IsFixedLength();
            entity.Property(e => e.Chqauthorize).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Logextract).IsFixedLength();
            entity.Property(e => e.Logextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnVBankreceipt>(entity =>
        {
            entity.ToView("cfn_v_bankreceipt");

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Cancelflag).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Logextract).IsFixedLength();
            entity.Property(e => e.Logextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnVBnkVendor>(entity =>
        {
            entity.ToView("cfn_v_bnk_vendor");
        });

        modelBuilder.Entity<CfnVCashHeader>(entity =>
        {
            entity.ToView("cfn_v_cash_header");
        });

        modelBuilder.Entity<CfnVCashbook>(entity =>
        {
            entity.ToView("cfn_v_cashbook");

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnVCashpayment>(entity =>
        {
            entity.ToView("cfn_v_cashpayment");

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Cancelflag).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Logextract).IsFixedLength();
            entity.Property(e => e.Logextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnVCashreceipt>(entity =>
        {
            entity.ToView("cfn_v_cashreceipt");

            entity.Property(e => e.Cancelflag).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Logextract).IsFixedLength();
            entity.Property(e => e.Logextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnVCfparcode>(entity =>
        {
            entity.ToView("cfn_v_cfparcode");
        });

        modelBuilder.Entity<CfnVChequeparty>(entity =>
        {
            entity.ToView("cfn_v_chequeparty");
        });

        modelBuilder.Entity<CfnVContra>(entity =>
        {
            entity.ToView("cfn_v_contra");

            entity.Property(e => e.Cancelflag).IsFixedLength();
            entity.Property(e => e.Chqauthorize).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Logextract).IsFixedLength();
            entity.Property(e => e.Logextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnVCosttype>(entity =>
        {
            entity.ToView("cfn_v_costtype");
        });

        modelBuilder.Entity<CfnVCreditnote>(entity =>
        {
            entity.ToView("cfn_v_creditnote");

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Cancelflag).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Logextract).IsFixedLength();
            entity.Property(e => e.Logextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnVCreditoroutstanding>(entity =>
        {
            entity.ToView("cfn_v_creditoroutstanding");

            entity.Property(e => e.Accountcode).IsFixedLength();
            entity.Property(e => e.Accperiod).IsFixedLength();
            entity.Property(e => e.Billno).IsFixedLength();
            entity.Property(e => e.Onholdno).IsFixedLength();
            entity.Property(e => e.Subcode).IsFixedLength();
            entity.Property(e => e.Vchrcategory).IsFixedLength();
            entity.Property(e => e.Vchrtype).IsFixedLength();
        });

        modelBuilder.Entity<CfnVDailytransaction>(entity =>
        {
            entity.ToView("cfn_v_dailytransaction");
        });

        modelBuilder.Entity<CfnVDebitnote>(entity =>
        {
            entity.ToView("cfn_v_debitnote");

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnVExchange>(entity =>
        {
            entity.ToView("cfn_v_exchange");

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnVExpensetype>(entity =>
        {
            entity.ToView("cfn_v_expensetype");
        });

        modelBuilder.Entity<CfnVGinjin>(entity =>
        {
            entity.ToView("cfn_v_ginjin");

            entity.Property(e => e.GinJinNumber).IsFixedLength();
        });

        modelBuilder.Entity<CfnVInvoice>(entity =>
        {
            entity.ToView("cfn_v_invoice");

            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnVJournalregister>(entity =>
        {
            entity.ToView("cfn_v_journalregister");

            entity.Property(e => e.Cancelflag).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Logextract).IsFixedLength();
            entity.Property(e => e.Logextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnVPlaccount>(entity =>
        {
            entity.ToView("cfn_v_placcount");
        });

        modelBuilder.Entity<CfnVPlaccountmem>(entity =>
        {
            entity.ToView("cfn_v_placcountmem");
        });

        modelBuilder.Entity<CfnVProductstock>(entity =>
        {
            entity.ToView("cfn_v_productstock");

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnVSalesregister>(entity =>
        {
            entity.ToView("cfn_v_salesregister");

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Cancelflag).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Loagextract).IsFixedLength();
            entity.Property(e => e.Logextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnVStockRegister>(entity =>
        {
            entity.ToView("cfn_v_stock_register");
        });

        modelBuilder.Entity<CfnVSubcode>(entity =>
        {
            entity.ToView("cfn_v_subcodes");
        });

        modelBuilder.Entity<CfnVSubcodeslink>(entity =>
        {
            entity.ToView("cfn_v_subcodeslink");
        });

        modelBuilder.Entity<CfnVSubcodlnk>(entity =>
        {
            entity.ToView("cfn_v_subcodlnk");
        });

        modelBuilder.Entity<CfnVSubledger>(entity =>
        {
            entity.ToView("cfn_v_subledger");

            entity.Property(e => e.CbFlag).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.ObFlag).IsFixedLength();
        });

        modelBuilder.Entity<CfnVTdsregister>(entity =>
        {
            entity.ToView("cfn_v_tdsregister");

            entity.Property(e => e.Accountcode).IsFixedLength();
            entity.Property(e => e.Accperiod).IsFixedLength();
            entity.Property(e => e.Billno).IsFixedLength();
            entity.Property(e => e.CtrlStatus).IsFixedLength();
            entity.Property(e => e.Subaccountcode).IsFixedLength();
            entity.Property(e => e.Tdscode).IsFixedLength();
            entity.Property(e => e.VchrNumber).IsFixedLength();
            entity.Property(e => e.VchrType).IsFixedLength();
        });

        modelBuilder.Entity<CfnVTransaction>(entity =>
        {
            entity.ToView("cfn_v_transaction");
        });

        modelBuilder.Entity<CfnVTravelregister>(entity =>
        {
            entity.ToView("cfn_v_travelregister");

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Cancelflag).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Logextract).IsFixedLength();
            entity.Property(e => e.Logextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnVTrialbalance>(entity =>
        {
            entity.ToView("cfn_v_trialbalance");
        });

        modelBuilder.Entity<CfnVchrcontrol>(entity =>
        {
            entity.HasKey(e => new { e.Vouchergroup, e.Vouchertype, e.Accperiod })
                .HasName("PKY_VCHRCONTROL")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnVchrgroup>(entity =>
        {
            entity.HasKey(e => e.Vouchergroup)
                .HasName("PKY_VCHRGROUP")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnVchrtype>(entity =>
        {
            entity.HasKey(e => new { e.Vouchergroup, e.Vouchertype })
                .HasName("PKY_VCHRTYPE")
                .HasFillFactor(90);

            entity.Property(e => e.Activestatus).IsFixedLength();
        });

        modelBuilder.Entity<CfnVendor>(entity =>
        {
            entity.HasKey(e => e.Vendorcode)
                .HasName("PKY_VENDOR")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnVendorratecontract>(entity =>
        {
            entity.HasKey(e => new { e.Vendorcode, e.Rcfromperiod, e.Rctoperiod })
                .HasName("FKY_VENDORRCCUM")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnView>(entity =>
        {
            entity.HasKey(e => e.Viewid)
                .HasName("PKY_VIEW")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnVoucheraccount>(entity =>
        {
            entity.HasKey(e => new { e.Accountcode, e.Vouchergroup, e.Vouchertype })
                .HasName("PKY_VOUCHERACCOUNTS")
                .HasFillFactor(90);

            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.Status).IsFixedLength();
        });

        modelBuilder.Entity<CfnVoucherdetail>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdno, e.CtrlSequenceno, e.CostSequenceno })
                .HasName("ZXVZXCVZ")
                .HasFillFactor(90);

            entity.Property(e => e.Automated).IsFixedLength();
            entity.Property(e => e.Dbcrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnVoucherheader>(entity =>
        {
            entity.HasKey(e => e.CtrlOnholdno)
                .HasName("PKY_HEADERVCHR")
                .HasFillFactor(90);

            entity.Property(e => e.Billpassingappl).IsFixedLength();
            entity.Property(e => e.Chqauthorize).IsFixedLength();
            entity.Property(e => e.Chqgenerate).IsFixedLength();
            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
        });

        modelBuilder.Entity<CfnVoucherjvlnk>(entity =>
        {
            entity.HasKey(e => new { e.CtrlOnholdvoucherno, e.CtrlOnholdautomatedjv })
                .HasName("PKY_VOUCHERJVLNK")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnVoucherobject>(entity =>
        {
            entity.HasKey(e => e.Taskid)
                .HasName("PKY_TASKID")
                .HasFillFactor(90);

            entity.Property(e => e.Taskid).IsFixedLength();
            entity.Property(e => e.Dataobject).IsFixedLength();
            entity.Property(e => e.Detailobject).IsFixedLength();
        });

        modelBuilder.Entity<CfnVouchersysdatum>(entity =>
        {
            entity.HasKey(e => new { e.Code, e.Vouchertype, e.Vouchergroup })
                .HasName("PKY_DATA")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CfnWarehouse>(entity =>
        {
            entity.HasKey(e => e.Warehousecode)
                .HasName("PKY_WAREHOUSECODE")
                .HasFillFactor(90);

            entity.Property(e => e.CtrlCancelflag).IsFixedLength();
            entity.Property(e => e.CtrlLogextract).IsFixedLength();
            entity.Property(e => e.CtrlLogextracttype).IsFixedLength();
            entity.Property(e => e.CtrlNextrefrflag).IsFixedLength();
        });

        modelBuilder.Entity<CfnWhstocktypelink>(entity =>
        {
            entity.HasKey(e => new { e.Warehousecode, e.Stocktype })
                .HasName("PKY_WHSTOCKTYPELINK")
                .HasFillFactor(90);
        });

        modelBuilder.Entity<CodeEntryAccount>(entity =>
        {
            entity.ToTable("Code_Entry_Account", tb => tb.HasTrigger("Code_Entry_Account_UpdateLOG"));

            entity.Property(e => e.AccActive).IsFixedLength();
        });

        modelBuilder.Entity<CodeType>(entity =>
        {
            entity.ToTable("Code_Type", tb => tb.HasTrigger("Code_Type_UpdateLOG"));

            entity.Property(e => e.Code).IsFixedLength();
            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Active).IsFixedLength();
            entity.Property(e => e.AllowMultiplePoToInvoice).IsFixedLength();
            entity.Property(e => e.AnnexureInvoiceAttach).IsFixedLength();
            entity.Property(e => e.AttachInvoiceReport).IsFixedLength();
            entity.Property(e => e.AutoMailOs).IsFixedLength();
            entity.Property(e => e.Cform).IsFixedLength();
            entity.Property(e => e.ClubOrders)
                .HasDefaultValue("Y")
                .IsFixedLength();
            entity.Property(e => e.CustUpdate).IsFixedLength();
            entity.Property(e => e.EmailInvCopy).IsFixedLength();
            entity.Property(e => e.EndCustomerRequired).IsFixedLength();
            entity.Property(e => e.EorinoRequired).IsFixedLength();
            entity.Property(e => e.InvMultiPo).IsFixedLength();
            entity.Property(e => e.LoiAllowed).IsFixedLength();
            entity.Property(e => e.PermanentCustomer).IsFixedLength();
            entity.Property(e => e.RexApply).IsFixedLength();
            entity.Property(e => e.Sez).IsFixedLength();
            entity.Property(e => e.SignatureRequired).IsFixedLength();
        });

        modelBuilder.Entity<CustomerOld>(entity =>
        {
            entity.Property(e => e.Active).IsFixedLength();
            entity.Property(e => e.AllowMultiplePoToInvoice).IsFixedLength();
            entity.Property(e => e.AutoMailOs).IsFixedLength();
            entity.Property(e => e.Cform).IsFixedLength();
            entity.Property(e => e.CustUpdate).IsFixedLength();
            entity.Property(e => e.EmailInvCopy).IsFixedLength();
            entity.Property(e => e.InvMultiPo)
                .HasDefaultValue("Y")
                .IsFixedLength();
            entity.Property(e => e.LoiAllowed).IsFixedLength();
            entity.Property(e => e.PermanentCustomer).IsFixedLength();
            entity.Property(e => e.RexApply)
                .HasDefaultValue("N")
                .IsFixedLength();
            entity.Property(e => e.Sez).IsFixedLength();
        });

        modelBuilder.Entity<EinvDetail>(entity =>
        {
            entity.ToTable("einv_detail", tb => tb.HasTrigger("EInv_Detail_UpdateLOG"));

            entity.Property(e => e.InvDActive).IsFixedLength();
        });

        modelBuilder.Entity<EinvMain>(entity =>
        {
            entity.ToTable("einv_main", tb => tb.HasTrigger("EInv_Main_UpdateLOG"));

            entity.Property(e => e.CompanyCode).HasDefaultValue("COM/00001");
            entity.Property(e => e.CustRec).IsFixedLength();
            entity.Property(e => e.DisplayDiscount).IsFixedLength();
            entity.Property(e => e.IndigenousInv).IsFixedLength();
            entity.Property(e => e.InvActive).IsFixedLength();
            entity.Property(e => e.InvMultiPo)
                .HasDefaultValue("Y")
                .IsFixedLength();
            entity.Property(e => e.IsPromoOrder).IsFixedLength();
        });

        modelBuilder.Entity<GinDetail>(entity =>
        {
            entity.Property(e => e.ArticleType).IsFixedLength();
            entity.Property(e => e.StockTo).IsFixedLength();
        });

        modelBuilder.Entity<GinMaster>(entity =>
        {
            entity.Property(e => e.Check).IsFixedLength();
            entity.Property(e => e.GinApp).IsFixedLength();
            entity.Property(e => e.GinAuto).IsFixedLength();
            entity.Property(e => e.StoresApprove).IsFixedLength();
        });

        modelBuilder.Entity<GinPriceDomestic>(entity =>
        {
            entity.ToTable("Gin_Price_Domestic", tb => tb.HasTrigger("GIN_Price_Domestic_UpdateLOG"));
        });

        modelBuilder.Entity<GinPriceImport>(entity =>
        {
            entity.ToTable("Gin_Price_Import", tb => tb.HasTrigger("GIN_Price_Import_UpdateLOG"));
        });

        modelBuilder.Entity<ImportFinInvoiceDomestic>(entity =>
        {
            entity.Property(e => e.IndigenousInv).IsFixedLength();
        });

        modelBuilder.Entity<ImportFinInvoiceExport>(entity =>
        {
            entity.Property(e => e.IndigenousInv).IsFixedLength();
        });

        modelBuilder.Entity<InvDetail>(entity =>
        {
            entity.Property(e => e.InvDActive).IsFixedLength();
        });

        modelBuilder.Entity<InvMain>(entity =>
        {
            entity.ToTable("Inv_main", tb => tb.HasTrigger("Inv_Main_UpdateLOG"));

            entity.Property(e => e.CustRec).IsFixedLength();
            entity.Property(e => e.DisplayDiscount).IsFixedLength();
            entity.Property(e => e.IndigenousInv).IsFixedLength();
            entity.Property(e => e.InvActive).IsFixedLength();
            entity.Property(e => e.InvMultiPo)
                .HasDefaultValue("Y")
                .IsFixedLength();
            entity.Property(e => e.IsPromoOrder).IsFixedLength();
        });

        modelBuilder.Entity<MasterTd>(entity =>
        {
            entity.Property(e => e.TdsActive).IsFixedLength();
        });

        modelBuilder.Entity<MisDinvoiceEmpCustItm>(entity =>
        {
            entity.ToView("MIS_DInvoice_EmpCustItm");
        });

        modelBuilder.Entity<MisEinvoice>(entity =>
        {
            entity.ToView("MIS_Einvoice");
        });

        modelBuilder.Entity<MisEinvoiceEmpCustItm>(entity =>
        {
            entity.ToView("MIS_EInvoice_EmpCustItm");
        });

        modelBuilder.Entity<MisInvoice>(entity =>
        {
            entity.ToView("MIS_Invoice");
        });

        modelBuilder.Entity<MisInvoiceEmpCustItm>(entity =>
        {
            entity.ToView("MIS_Invoice_EmpCustItm");
        });

        modelBuilder.Entity<MisOrderEmpCustItm>(entity =>
        {
            entity.ToView("MIS_Order_EmpCustItm");
        });

        modelBuilder.Entity<MisPeriodEmpCustItm>(entity =>
        {
            entity.ToView("MIS_Period_EmpCustItm");
        });

        modelBuilder.Entity<MisTargetEmpCustItm>(entity =>
        {
            entity.ToView("MIS_Target_EmpCustItm");
        });

        modelBuilder.Entity<MisTotalEmpCustItm>(entity =>
        {
            entity.ToView("MIS_Total_EmpCustItm");
        });

        modelBuilder.Entity<ObiCodeEntry>(entity =>
        {
            entity.Property(e => e.Active).IsFixedLength();
            entity.Property(e => e.ForMachine).IsFixedLength();
            entity.Property(e => e.SubContractItem).IsFixedLength();
            entity.Property(e => e.UpdateCode).IsFixedLength();
        });

        modelBuilder.Entity<Personel>(entity =>
        {
            entity.Property(e => e.Caste).IsFixedLength();
            entity.Property(e => e.CompanyCode).HasDefaultValue("COM/00001");
            entity.Property(e => e.CompensatoryLeaveAllowed)
                .HasDefaultValue("Y")
                .IsFixedLength();
            entity.Property(e => e.EmpActive).IsFixedLength();
            entity.Property(e => e.FrequentTraveller).IsFixedLength();
            entity.Property(e => e.Hposition).IsFixedLength();
            entity.Property(e => e.HrRights).IsFixedLength();
            entity.Property(e => e.Insurenceno).IsFixedLength();
            entity.Property(e => e.ItaxNo).IsFixedLength();
            entity.Property(e => e.Nationality).IsFixedLength();
            entity.Property(e => e.Passportno).IsFixedLength();
            entity.Property(e => e.Paymode).IsFixedLength();
            entity.Property(e => e.Religion).IsFixedLength();
        });

        modelBuilder.Entity<StoAnnexDetail>(entity =>
        {
            entity.Property(e => e.AnnSuffix).IsFixedLength();
            entity.Property(e => e.InsApprove).IsFixedLength();
            entity.Property(e => e.JinApprove).IsFixedLength();
            entity.Property(e => e.WOpn).IsFixedLength();
        });

        modelBuilder.Entity<StoAnnexMaster>(entity =>
        {
            entity.ToTable("Sto_Annex_Master", tb => tb.HasTrigger("Sto_Annex_Master_UpdateLOG"));

            entity.Property(e => e.AnnReturn).IsFixedLength();
        });

        modelBuilder.Entity<SupplierDetail>(entity =>
        {
            entity.Property(e => e.MandatotySupplierItemDetails).IsFixedLength();
            entity.Property(e => e.SkipStoreValuation).IsFixedLength();
            entity.Property(e => e.StdPrice).IsFixedLength();
            entity.Property(e => e.SupType).IsFixedLength();
            entity.Property(e => e.SuppActive).IsFixedLength();
            entity.Property(e => e.SuppAutoPo).IsFixedLength();
        });

        modelBuilder.Entity<UtPayment>(entity =>
        {
            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.VchrRefnumber).IsFixedLength();
        });

        modelBuilder.Entity<UvPayment>(entity =>
        {
            entity.ToView("UV_PAYMENTS");

            entity.Property(e => e.Dbcrflag).IsFixedLength();
            entity.Property(e => e.VchrRefnumber).IsFixedLength();
        });

        modelBuilder.Entity<Vendcode>(entity =>
        {
            entity.ToTable("vendcode", tb => tb.HasTrigger("VendCode_UpdateLOG"));

            entity.Property(e => e.Active)
                .HasDefaultValue("Y")
                .IsFixedLength();
            entity.Property(e => e.Authorised).IsFixedLength();
            entity.Property(e => e.CurrencyCode).HasDefaultValue("CUR/00001");
            entity.Property(e => e.PriceTypeCode).HasDefaultValue("VPT/00000");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
