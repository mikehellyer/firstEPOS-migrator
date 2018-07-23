using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using csvfile;


namespace CreateSQLDatabase
{
    public partial class frmMain : Form
    {

        // Initialise connection. Dont have it point to any table initially before we create the database
        private string ConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;" +
                                          "Initial Catalog = master;" +
                                          "Integrated Security = true";
        private string AccessConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;data source=";

        
        // SQL stuff
        private SqlDataReader reader = null;
        private SqlConnection conn = null;
        private SqlCommand cmd = null;
        private string sql = null;
        
        // Access database stuff
        private OleDbDataReader accessReader = null;
        private OleDbConnection accessConn = null;
        private OleDbCommand accessCmd = null;
        private DataTable accessTable;
        private DataTable accessTableCols;
        private string access = null;
        private DataTable Accessdt;
        private DataTable deptsdt;
        private DataTable itemsdt;

         
        
        
        private string filename = "C:\\Home\\Projects\\Visual Studio 2017\\Original Epos Data File\\data.mdb";
        
        
        public frmMain()
        {
            InitializeComponent();
            
            conn = new SqlConnection(ConnectionString);

            lblSelectedDatabase.Text = filename;
            AccessConnectionString += filename;
            accessConn = new OleDbConnection(AccessConnectionString);
            loadAccessDatabase();
        }

        private void ExecuteSQLStmt(string sql)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                MessageBox.Show(ae.Message.ToString());
            }
        }

        private void btnCreateDB_Click(object sender, EventArgs e)
        {
            // Create a connection
            //conn = new SqlConnection(ConnectionString);
            // Open the connection
            if (conn.State != ConnectionState.Open)
                conn.Open();
            string sql = "CREATE DATABASE ePOSData ON PRIMARY"
                       + "(Name=ePOS_data, filename = 'C:\\mysql\\ePOS_data.mdf', size=3,"
                       + "maxsize=30, filegrowth=30MB)log on"
                       + "(name=ePOSData_log, filename='C:\\mysql\\ePOSData_log.ldf',size=3,"
                       + "maxsize=30,filegrowth=30MB)";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                MessageBox.Show(ae.Message.ToString());
            }


        }


        private void updateProgress(String progress)
        {
            listProgress.Items.Add(progress);
            listProgress.Refresh();
        
        }
        
        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            
            int tableCount = 0;
            // Open the connection
            if ((conn != null) && (conn.State == ConnectionState.Open))
                conn.Close();
            ConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;" +
                               "Initial Catalog = ePOSData;" +
                               "Integrated Security = SSPI";
            
            conn.ConnectionString = ConnectionString;
            conn.Open();

            //Create the table structure


            try //to create the tables !
            {
                //Create Departs_tbl table
                sql = "CREATE TABLE tbl_Departments" +
                    "(DepartmentCode NVARCHAR(30) CONSTRAINT PKeyDepartmentCode PRIMARY KEY," +
                    "DepartmentID INTEGER IDENTITY(1,1) NOT NULL," +
                    "ButtonColourId INTEGER," +
                    "PositionId INTEGER," +
                    "ButtonTextId INTEGER)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_Departments Created");

                //Create tbl_Items table
                sql = "CREATE TABLE tbl_Items" +
                      "(StockRef NVARCHAR(50) CONSTRAINT PKeyStockRef PRIMARY KEY," +
                      "Description NVARCHAR(50)," +
                      "[Stock Qty] INTEGER," +
                      "CostPrice DECIMAL(19,4)," +
                      "RetailPrice DECIMAL(19,4)," +
                      "VATPaid DECIMAL(19,4)," +
                      "TaxApplicable BIT," +
                      "VATRate FLOAT," +
                      "SellByValue BIT," +
                      "SupplierCode NVARCHAR(10)," +
                      "SupplierName NVARCHAR(20)," +
                    //CONSTRAINT FKeyDeptCode FOREIGN KEY REFERENCES tbl_Departments(DepartmentCode)
                      "DeptCode NVARCHAR (30) CONSTRAINT FKeyDeptCode FOREIGN KEY REFERENCES tbl_Departments(DepartmentCode)," +
                      "AgeRestriction INTEGER," +
                      "MinimumStockLevel INTEGER," +
                      "MinimumOrderQty INTEGER," +
                      "CatalogueCode NVARCHAR(20)," +
                      "FirstRecvd DATETIME," +
                      "LastRecvd DATETIME," +
                      "BatchDelete BIT," +
                      "TaxCode INTEGER," +
                      "ButtonPicture BIT," +
                      "UnitOfMeasurement INTEGER," +
                      "UseIcon BIT," +
                      "IconLocation NVARCHAR(100)," +
                      "PacketSize NVARCHAR(12)," +
                      "Favorite BIT," +
                      "MarkUpPercent INTEGER," +
                      "UseMarkup BIT," +
                      "OverideRetailPrice DECIMAL(19,4)," +
                      "UseOveride BIT," +
                      "LogicalDelete BIT)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                //temporary turn off constraint checking
                sql = "ALTER TABLE tbl_Items NOCHECK CONSTRAINT ALL";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_Items Created");


                //Create tbl_Loyalty table
                sql = "CREATE TABLE tbl_Loyalty" +
                      "(CustID INTEGER IDENTITY(1,1) CONSTRAINT PKeyCustID PRIMARY KEY," +
                      "CustName NVARCHAR(25),"+
                      "CustAddress TEXT,"+
                      "CustPostCode NVARCHAR(9),"+
                      "CustTel NVARCHAR(15), "+
                      "CustOpenDate DATETIME,"+
                      "CustSalesFromReset DECIMAL(19,4),"+
                      "CustPointsAvailable INTEGER,"+
                      "CustLoyaltyPercent FLOAT,"+ 
                      "CustCashValue DECIMAL(19,4),"+
                      "RptCustomerOrder BIT,"+
                      "RptDeliveryNote BIT,"+
                      "RptStatement BIT)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_Loyalty Created");


                // Create tblInvoices
                sql = "CREATE TABLE tbl_Invoices" +
                      "(SaleNo INTEGER IDENTITY(1,1) CONSTRAINT PKeySaleNo PRIMARY KEY," +
                      "InvoiceNo INTEGER," +
                      "Date DATETIME," +
                      "GoodsSold DECIMAL (19,4)," +
                      "TaxPaid DECIMAL (19,4)," +
                      "SaleTotal DECIMAL (19,4)," +
                      "SaleType NVARCHAR(20)," +
                      "LastRefund DATETIME," +
                      "LastRefundAmount DECIMAL (19,4)," +
                      "CustBalance DECIMAL (19,4)," +
                      "CustID INTEGER," +
                      "Time DATETIME," +
                      "PoS INTEGER," +
                      "AmountTendered DECIMAL (19,4)," +
                      "SaleCompleted BIT," +
                      "TotalSaleDiscount DECIMAL (19,4))";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_Invoices Created");
                
                
                //Create tbl_SalesLines table
                sql = "CREATE TABLE tbl_SalesLines" +
                      "(OrderNo INTEGER IDENTITY(1,1) CONSTRAINT PKeyOrderNo PRIMARY KEY," +
                      "SaleNo INTEGER CONSTRAINT FKeySaleNo FOREIGN KEY REFERENCES tbl_Invoices(SaleNo)," +
                      "StockRef NVARCHAR(50),"+
                      "Description NVARCHAR(50),"+
                      "OrderQty FLOAT,"+
                      "RetailPrice DECIMAL (19,4),"+
                      "[Ext Price] DECIMAL (19,4),"+
                      "VATPaid DECIMAL (19,4),"+
                      "ItemsReturned INTEGER,"+
                      "RefundAmount DECIMAL(19,4),"+
                      "RefundDate DATETIME,"+
                      "DeptCode NVARCHAR(50),"+
                      "TaxApplicable BIT,"+
                      "SellByValue BIT,"+
                      "TaxCode INTEGER,"+
                      "CostPrice DECIMAL (19,4),"+
                      "CatalogueCode NVARCHAR(20),"+
                      "MarkUp INTEGER,"+
                      "AltBarcode NVARCHAR(60))";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                //temporary turn off constraint checking
                sql = "ALTER TABLE tbl_SalesLines NOCHECK CONSTRAINT ALL";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("SalesLine_tbl Created");

                // Create tblAddHocReturns
                sql = "CREATE TABLE tbl_AddHocReturns" +
                      "(ReturnNo INTEGER IDENTITY(1,1) CONSTRAINT PKeyReturnNo PRIMARY KEY," +
                      "Date DATETIME," +
                      "GoodsSold DECIMAL (19,4)," +
                      "TaxPaid DECIMAL (19,4)," +
                      "SaleTotal DECIMAL (19,4)," +
                      "PoS INTEGER," +
                      "Completed BIT)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_AddHocReturns Created");
                
                
                
                // Create tblAddHocRefundsDetails
                //the LineNo field has square brackets becasue it is a reserved name in SQL
                sql = "CREATE TABLE tbl_AddHocRefundsDetails" +
                      "([LineNo] INTEGER IDENTITY(1,1) CONSTRAINT PKeyLineNo PRIMARY KEY," +
                      "ReturnNo INTEGER CONSTRAINT FKeyReturnNo FOREIGN KEY REFERENCES tbl_AddHocReturns(ReturnNo)," +
                      "StockRef NVARCHAR(50)," +
                      "Description NVARCHAR(50)," +
                      "OrderQty FLOAT," +
                      "RetailPrice DECIMAL (19,4)," +
                      "[Ext Price] DECIMAL (19,4)," +
                      "VATPaid DECIMAL (19,4)," +
                      "DeptCode DECIMAL (19,4)," +
                      "TaxApplicable BIT," +
                      "SellByValue BIT," +
                      "TaxCode INTEGER," +
                      "CurrentStockLevel INTEGER)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_AddHocRefundsDetails Created");

                

                // Create tblAdditionalBarcodes
                sql = "CREATE TABLE tbl_AdditionalBarcodes" +
                      "(AdditionalStockRef NVARCHAR(50) CONSTRAINT PKeyAdditionalStockRef PRIMARY KEY," +
                      "StockRef NVARCHAR(50) CONSTRAINT FKeyStockRef FOREIGN KEY REFERENCES tbl_Items(StockRef)," +
                      "CostPrice DECIMAL (19,4),"+
                      "RetailPrice DECIMAL (19,4),"+
                      "VATPaid DECIMAL (19,4),"+
                      "PacketSize DECIMAL (19,4),"+
                      "MassDelete BIT)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_AdditionalBarcodes Created");


                // Create tblBackGroundColours
                sql = "CREATE TABLE tbl_BackGroundColours" +
                      "(ColourId INTEGER IDENTITY(1,1) CONSTRAINT PKeyColourId PRIMARY KEY," +
                      "Colour NVARCHAR(50),"+
                      "ColourCode INTEGER)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_BackGroundColours Created");

                // Create tblBarcodes
                sql = "CREATE TABLE tbl_Barcodes" +
                      "(StockRef NVARCHAR (50) CONSTRAINT PKeyBCStockRef PRIMARY KEY," +
                      "Description NVARCHAR(40),"+
                      "RetailPrice DECIMAL (19,4),"+
                      "LabelCount INTEGER)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_Barcodes Created");



                // Create tblCurrencyConversion
                sql = "CREATE TABLE tbl_CurrencyConversion" +
                      "(CurrencyConversion DECIMAL (19,4) CONSTRAINT PKeyCurrencyConversion PRIMARY KEY," +
                      "CurrencyType NVARCHAR(50))";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_CurrencyConversion Created");



                // Create tblDefaultScreen
                sql = "CREATE TABLE tbl_DefaultScreen" +
                      "(ScreenId INTEGER IDENTITY(1,1) CONSTRAINT PKeyScreenId PRIMARY KEY," +
                      "Screen NVARCHAR(50))";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_DefaultScreen Created");


                // Create tblInvoicePayments
                sql = "CREATE TABLE tbl_InvoicePayments" +
                      "(PaymentId INTEGER IDENTITY(1,1) CONSTRAINT PKeyPaymentId PRIMARY KEY," +
                      "SaleNo INTEGER CONSTRAINT FKeyInvoiceSaleNo FOREIGN KEY REFERENCES tbl_Invoices(SaleNo)," +
                      "Method NVARCHAR(50),"+
                      "Type NVARCHAR(50),"+
                      "Amount DECIMAL (19,4),"+
                      "PaymentDate DATETIME,"+
                      "CashChangeDue DECIMAL (19,4))";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_InvoicePayments Created");

                


                // Create tblLayerProtection
                sql = "CREATE TABLE tbl_LayerProtection" +
                      "(FunctionID INTEGER CONSTRAINT PKeyFunctionID PRIMARY KEY," +
                      "OpenCashDraw BIT,"+
                      "ManagementReports BIT,"+
                      "DeleteStock BIT,"+
                      "ModifyStock BIT,"+
                      "ViewStock BIT,"+
                      "StockDelivery BIT,"+
                      "ModifyAccount BIT,"+
                      "StockControlOnly BIT)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_LayerProtection Created");


                // Create tblPayments
                sql = "CREATE TABLE tbl_Payments" +
                      "([fIELD id] INTEGER IDENTITY(1,1) CONSTRAINT PKeyfIELDid PRIMARY KEY," +
                      "FieldName NVARCHAR(50))";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_Payments Created");

                // Create tblSerialNumbers
                sql = "CREATE TABLE tbl_SerialNumbers" +
                      "(SerialNoID INTEGER IDENTITY(1,1) CONSTRAINT PKeySerialNoID PRIMARY KEY," +
                      "SerialNumber NVARCHAR(30),"+
                      "Description NVARCHAR(20),"+
                      "DeliveryDate DATETIME,"+
                      "InvoiceNo NVARCHAR(30),"+
                      "Supplier NVARCHAR(50))";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_SerialNumbers Created");


                // Create tblSetUp
                sql = "CREATE TABLE tbl_SetUp" +
                      "([Business Name] NVARCHAR(14),"+
                      "[Business Address] TEXT,"+
                      "Tel NVARCHAR(50),"+
                      "[VAT Number] NVARCHAR(50),"+
                      "[End Message] TEXT,"+
                      "Password NVARCHAR(10),"+
                      "[License No] NVARCHAR(10),"+
                      "[File Path] NVARCHAR(50),"+
                      "PoS INTEGER,"+
                      "PromptPrintReceipt INTEGER,"+
                      "SetPrinterDefault INTEGER,"+
                      "DefaultToZeroRated BIT,"+
                      "AlwaysUseDefaultPrinter BIT,"+
                      "DefaultCustomerReport BIT,"+
                      "DefaultFileCopyReport BIT,"+
                      "DefaultAdditionalOrderCopy BIT,"+
                      "DefaultDeliveryNote BIT,"+
                      "DefaultFullStatement BIT,"+
                      "UseCommPort1 BIT,"+
                      "UseCommPort2 BIT,"+
                      "UseCommPort3 BIT,"+
                      "TaxRate1 FLOAT,"+
                      "TaxRate2 FLOAT,"+
                      "TaxRate1Alias NVARCHAR(50),"+
                      "TaxRate2Alias NVARCHAR(50),"+
                      "OpenTillDrawCashOnly BIT,"+
                      "CurrencyAbreviation NVARCHAR(10),"+
                      "DefaultToFastFood INTEGER,"+
                      "UseCCTV BIT,"+
                      "SetButtonColourToCategory BIT,"+
                      "ButtonColourId INTEGER,"+
                      "NeverPrintLayAwayReports BIT,"+
                      "SalesScreenInFavoriteMode BIT,"+
                      "AutoBackup BIT,"+
                      "AutoBackupLocation NVARCHAR(250),"+
                      "DefaultMarkupPercent INTEGER,"+
                      "UseOveridePrice BIT,"+
                      "AlwaysUseMarkUp BIT,"+
                      "ShowChangeDuePage BIT)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_SetUp Created");

                // Create tblSuppliers
                sql = "CREATE TABLE tbl_Suppliers" +
                      "(SupplierCode NVARCHAR(10),"+
                      "SupplieName NVARCHAR(15),"+
                      "SupplierID INTEGER IDENTITY(1,1) CONSTRAINT PKeySupplierID PRIMARY KEY)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_Suppliers Created");


                // Create tblTaxDetails
                sql = "CREATE TABLE tbl_TaxDetails" +
                      "(TaxId INTEGER IDENTITY(1,1) CONSTRAINT PKeyTaxId PRIMARY KEY," +
                      "TaxCode NVARCHAR(50),"+
                      "TaxRate FLOAT)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_TaxDetails Created");


                // Create tblTextColours
                sql = "CREATE TABLE tbl_TextColours" +
                      "(ColourId INTEGER IDENTITY(1,1) CONSTRAINT PKeyTextColourId PRIMARY KEY," +
                      "Colour NVARCHAR(50),"+
                      "ColourCode INTEGER)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_TextColours Created");

                // Create tblToken
                sql = "CREATE TABLE tbl_Token" +
                      "(TokenID INTEGER IDENTITY(1,1) CONSTRAINT PKeyTokenID PRIMARY KEY)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_Token Created");



                // Create tblZReset
                sql = "CREATE TABLE tbl_ZReset" +
                      "(ZResetDate DATETIME,"+
                      "SalesFromReset DECIMAL (19,4),"+
                      "TaxFromReset DECIMAL (19,4),"+
                      "Solo DECIMAL (19,4),"+
                      "SoloQty INTEGER,"+
                      "Amex DECIMAL (19,4),"+
                      "AmexQty INTEGER,"+
                      "Cash DECIMAL (19,4),"+
                      "Cheque DECIMAL (19,4),"+
                      "ChequeQty INTEGER,"+
                      "Switch DECIMAL (19,4),"+
                      "SwitchQty INTEGER,"+
                      "MasterCard DECIMAL (19,4),"+
                      "MasterCardQty INTEGER,"+
                      "Visa DECIMAL (19,4),"+
                      "VisaQty INTEGER,"+
                      "ZTotal DECIMAL (19,4),"+
                      "Discrepencies DECIMAL (19,4),"+
                      "Refunds DECIMAL (19,4),"+
                      "ZeroRatedSales DECIMAL (19,4),"+
                      "GoodsSold DECIMAL (19,4),"+
                      "TempSalesFromReset DECIMAL (19,4),"+
                      "Discount DECIMAL (19,4),"+
                      "PaidOut DECIMAL (19,4),"+
                      "TillOpened INTEGER,"+
                      "TaxRate1Sales DECIMAL (19,4),"+
                      "TaxRate2Sales DECIMAL (19,4),"+
                      "TaxRate3Sales DECIMAL (19,4),"+
                      "TaxPaidRate1 DECIMAL (19,4),"+
                      "TaxPaidRate2 DECIMAl (19,4),"+
                      "TaxPaidRate3 DECIMAL (19,4),"+
                      "GoodsSoldTax1 DECIMAL (19,4),"+
                      "GoodsSoldTax2 DECIMAL (19,4),"+
                      "GoodsSoldTax3 DECIMAL (19,4))";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                updateProgress("tbl_ZReset Created");


                


            }
            
            
            //any problems with creating the tables then atch display exception
            catch (SqlException ae)
            {
                MessageBox.Show(ae.Message.ToString());
            }


            

            
            
            try //to create the table entries
            {
                //execute create table
                //cmd.ExecuteNonQuery();
                // Adding records the table
                //check here to see if firstEpos database has been selected
                if (lblSelectedDatabase.Text != "No Database Selected")
                {


                    DataSet Mainds = new DataSet();
                    
                    
                    //loop through relevant table and insert fields into SQL table.
                    foreach (string item in listBox1.Items)
                    {
                     
                        
                        switch (item)
                        {

                            
                            
                            // Copy the department details to SQL
                            case "tblDepartments":                        
                                //read in items to the table and iterate and insert.
                                string commandStr = "Select * FROM tblDepartments";
                                deptsdt = fillAccessDataTable(commandStr);
                                deptsdt = CleanDataTable(deptsdt);

                                //turn on identity insert to allow us to insert auto incement field
                                identityInsert("on");
                                
                                //for each row in the table write to SQL table
                                foreach (DataRow row in deptsdt.Rows)
                                {
                                    sql = "INSERT INTO tbl_Departments" +
                                    "(DepartmentCode," +
                                    "DepartmentID," +
                                    "ButtonColourId," +
                                    "PositionId," +
                                    "ButtonTextId)" +
                                    " VALUES('" + row["DepartmentCode"].ToString().TrimEnd()+"'," + 
                                    row["DepartmentID"].ToString().TrimEnd() + "," +
                                    row["ButtonColourId"] + "," +
                                    row["PositionId"] + "," +
                                    row["ButtonTextId"]+")";
                                    //MessageBox.Show(sql);
                                    cmd = new SqlCommand(sql, conn);
                                    cmd.ExecuteNonQuery();
                                }
                                //remember to turn off indentity insert to allow autoincrementer.
                                identityInsert("off");

                                break;
                            
                            
                            
                            //Copy the tbl_Items details to SQL
                            case "Spec_tbl":
                                //Load in items from the access table.
                                commandStr = "Select * FROM spec_tbl";
                                itemsdt = fillAccessDataTable(commandStr);
                                //lets clean datatable of possible nulls
                                itemsdt = CleanDataTable(itemsdt);

                                //Create an SQL Dataset with a data adapter and a relevant command builder 
                                sql = "SELECT * FROM tbl_Items";
                                SqlDataAdapter SQLda = new SqlDataAdapter();
                                SQLda.SelectCommand = new SqlCommand(sql, conn);
                                DataSet SQLds = new DataSet();
                                SQLda.Fill(SQLds, "tbl_Items");
                                SqlCommandBuilder cb = new SqlCommandBuilder(SQLda);
                                //Get a data table from the dataset
                                DataTable SQLdt = SQLds.Tables["tbl_Items"];
                                //Create a blank row
                                DataRow SQLnewRow = SQLdt.NewRow();
                                
                                


                                //Insert into SQL
                                //turn on identity insert to allow us to insert auto incement field
                                identityInsert("on");
                                int rowcount = 0;
                                //for each row in the table write to SQL table

                                foreach (DataRow Accessrow in itemsdt.Rows)
                                {

                                    SQLnewRow = SQLdt.NewRow();

                                    SQLnewRow["StockRef"] = Accessrow["StockRef"];
                                    SQLnewRow["Description"] = Accessrow["Description"];
                                    SQLnewRow["Stock Qty"] = Accessrow["Stock Qty"];
                                    SQLnewRow["CostPrice"] = Accessrow["CostPrice"];
                                    SQLnewRow["RetailPrice"] = Accessrow["RetailPrice"];
                                    SQLnewRow["VATPaid"] = Accessrow["VATPaid"];
                                    SQLnewRow["TaxApplicable"] = returnBool(Accessrow["TaxApplicable"].ToString());
                                    SQLnewRow["VATRate"] = Accessrow["VATRate"];
                                    SQLnewRow["SellByValue"] = returnBool(Accessrow["SellByValue"].ToString());
                                    SQLnewRow["SupplierCode"] = Accessrow["SupplierCode"];
                                    SQLnewRow["SupplierName"] = Accessrow["SupplierName"];
                                    SQLnewRow["DeptCode"] = Accessrow["DeptCode"];
                                    SQLnewRow["AgeRestriction"] = Accessrow["AgeRestriction"];
                                    SQLnewRow["MinimumStockLevel"] = Accessrow["MinimumStockLevel"];
                                    SQLnewRow["MinimumOrderQty"] = Accessrow["MinimumOrderQty"];
                                    SQLnewRow["CatalogueCode"] = Accessrow["CatalogueCode"];
                                    SQLnewRow["FirstRecvd"] = Accessrow["FirstRecvd"];
                                    SQLnewRow["LastRecvd"] = Accessrow["LastRecvd"];
                                    SQLnewRow["BatchDelete"] = returnBool(Accessrow["BatchDelete"].ToString());
                                    SQLnewRow["TaxCode"] = Accessrow["TaxCode"];
                                    SQLnewRow["ButtonPicture"] = returnBool(Accessrow["ButtonPicture"].ToString());
                                    SQLnewRow["UnitOfMeasurement"] = Accessrow["UnitOfMeasurement"];
                                    SQLnewRow["UseIcon"] = returnBool(Accessrow["UseIcon"].ToString());
                                    SQLnewRow["IconLocation"] = Accessrow["IconLocation"];
                                    SQLnewRow["PacketSize"] = Accessrow["PacketSize"];
                                    SQLnewRow["Favorite"] = returnBool(Accessrow["Favorite"].ToString());
                                    SQLnewRow["MarkUpPercent"] = Accessrow["MarkUpPercent"];
                                    SQLnewRow["Usemarkup"] = returnBool(Accessrow["UseMarkup"].ToString());
                                    SQLnewRow["OverideRetailPrice"] = Accessrow["OverideRetailPrice"];
                                    SQLnewRow["UseOveride"] = returnBool(Accessrow["UseOveride"].ToString());
                                    SQLnewRow["LogicalDelete"] = returnBool(Accessrow["LogicalDelete"].ToString());

                                    
                                    SQLdt.Rows.Add(SQLnewRow);

                                    rowcount += 1;
                                    
                                    refreshlabel3(rowcount.ToString());


                                }
                                //remember to turn off indentity insert to allow autoincrementer.
                                identityInsert("off");
                                SQLda.Update(SQLds, "tbl_Items");
                                break;

                            //Copy the tbl_Loyalty details to SQL
                            case "tbl_Loyalty":
                                //Load in items from the access table.
                                commandStr = "Select * FROM tbl_Loyalty";
                                Accessdt = fillAccessDataTable(commandStr);
                                //lets clean datatable of possible nulls
                                Accessdt = CleanDataTable(Accessdt);

                                //Create an SQL Dataset with a data adapter and a relevant command builder 
                                sql = "SELECT * FROM tbl_Loyalty";
                                SQLda = new SqlDataAdapter();
                                SQLda.SelectCommand = new SqlCommand(sql, conn);
                                SQLds = new DataSet();
                                SQLda.Fill(SQLds, "tbl_Loyalty");
                                cb = new SqlCommandBuilder(SQLda);
                                //Get a data table from the dataset
                                SQLdt = SQLds.Tables["tbl_Loyalty"];
                                //Create a blank row
                                SQLnewRow = SQLdt.NewRow();

                                //Insert into SQL
                                //turn on identity insert to allow us to insert auto incement field
                                identityInsert("on");
                                rowcount = 0;
                                //for each row in the table write to SQL table

                                foreach (DataRow Accessrow in Accessdt.Rows)
                                {

                                    SQLnewRow = SQLdt.NewRow();

                                    


                                    SQLnewRow["CustID"] = Accessrow["CustID"];
                                    SQLnewRow["CustName"] = Accessrow["CustName"];
                                    SQLnewRow["CustAddress"] = Accessrow["CustAddress"];
                                    SQLnewRow["CustPostCode"] = Accessrow["CustPostCode"];
                                    SQLnewRow["CustTel"] = Accessrow["CustTel"];
                                    SQLnewRow["CustOpenDate"] = Accessrow["CustOpenDate"];
                                    SQLnewRow["CustSalesFromReset"] = (Accessrow["CustSalesFromReset"]);
                                    SQLnewRow["CustPointsAvailable"] = Accessrow["CustPointsAvailable"];
                                    SQLnewRow["CustLoyaltyPercent"] = returnBool(Accessrow["CustLoyaltyPercent"].ToString());
                                    SQLnewRow["CustCashValue"] = Accessrow["CustCashValue"];
                                    SQLnewRow["RptCustomerOrder"] = returnBool(Accessrow["RptCustomerOrder"].ToString());
                                    SQLnewRow["RptDeliveryNote"] = returnBool(Accessrow["RptDeliveryNote"].ToString());
                                    SQLnewRow["RptStatement"] = Accessrow["RptStatement"];

                                    
                                    SQLdt.Rows.Add(SQLnewRow);

                                    rowcount += 1;
                                    refreshlabel3(rowcount.ToString());
                                    
                                    

                                }
                                //remember to turn off indentity insert to allow autoincrementer.
                                identityInsert("off");
                                SQLda.Update(SQLds, "tbl_Loyalty");
                                break;


                            //Copy the tblInvoices details to SQL
                            case "tblInvoices":
                                //Load in items from the access table.
                                commandStr = "Select * FROM tblInvoices";
                                Accessdt = fillAccessDataTable(commandStr);
                                //lets clean datatable of possible nulls
                                Accessdt = CleanDataTable(Accessdt);

                                //Create an SQL Dataset with a data adapter and a relevant command builder 
                                sql = "SELECT * FROM tbl_Invoices";
                                SQLda = new SqlDataAdapter();
                                SQLda.SelectCommand = new SqlCommand(sql, conn);
                                //SQLds = new DataSet();
                                SQLda.Fill(Mainds, "tbl_Invoices");
                                cb = new SqlCommandBuilder(SQLda);
                                //Get a data table from the dataset
                                SQLdt = Mainds.Tables["tbl_Invoices"];
                                //Create a blank row
                                SQLnewRow = SQLdt.NewRow();

                                //Insert into SQL
                                //turn on identity insert to allow us to insert auto incement field
                                identityInsert("on");
                                rowcount = 0;
                                //for each row in the table write to SQL table

                                foreach (DataRow Accessrow in Accessdt.Rows)
                                {

                                    SQLnewRow = SQLdt.NewRow();




                                    SQLnewRow["SaleNo"] = Accessrow["SaleNo"];
                                    SQLnewRow["InvoiceNo"] = Accessrow["InvoiceNo"];
                                    SQLnewRow["Date"] = Accessrow["Date"];
                                    SQLnewRow["GoodsSold"] = Accessrow["GoodsSold"];
                                    SQLnewRow["TaxPaid"] = Accessrow["Taxpaid"];
                                    SQLnewRow["SaleTotal"] = Accessrow["SaleTotal"];
                                    SQLnewRow["SaleType"] = (Accessrow["SaleType"]);
                                    SQLnewRow["LastRefund"] = Accessrow["LastRefund"];
                                    SQLnewRow["LastRefundAmount"] = Accessrow["LastRefundAmount"];
                                    SQLnewRow["CustBalance"] = Accessrow["CustBalance"];
                                    SQLnewRow["CustID"] = Accessrow["CustID"];
                                    SQLnewRow["Time"] = Accessrow["Time"];
                                    SQLnewRow["Pos"] = Accessrow["Pos"];
                                    SQLnewRow["AmountTendered"] = Accessrow["AmountTendered"];
                                    SQLnewRow["SaleCompleted"] = returnBool(Accessrow["SaleCompleted"].ToString());
                                    SQLnewRow["TotalSaleDiscount"] = Accessrow["TotalSaleDiscount"];
                                    

                                    SQLdt.Rows.Add(SQLnewRow);

                                    rowcount += 1;
                                    refreshlabel3(rowcount.ToString());



                                }
                                //remember to turn off indentity insert to allow autoincrementer.
                                identityInsert("off");
                                SQLda.Update(Mainds, "tbl_Invoices");
                                break;
                            
                            
                          

                            //Copy the QuoteDetails to SQL
                            case "QuoteDetails_tbl":
                                //Load in items from the access table.
                                commandStr = "Select * FROM QuoteDetails_tbl";
                                Accessdt = fillAccessDataTable(commandStr);
                                //lets clean datatable of possible nulls
                                Accessdt = CleanDataTable(Accessdt);

                                //Create an SQL Dataset with a data adapter and a relevant command builder 
                                
                                sql = "SELECT * FROM tbl_SalesLines";
                                SQLda = new SqlDataAdapter();
                                SQLda.SelectCommand = new SqlCommand(sql, conn);
                                //SQLds = new DataSet();
                                SQLda.Fill(Mainds, "tbl_SalesLines");
                                cb = new SqlCommandBuilder(SQLda);
                                //Get a data table from the dataset
                                SQLdt = Mainds.Tables["tbl_SalesLines"];
                                //Create a blank row
                                SQLnewRow = SQLdt.NewRow();

                                //Insert into SQL
                                //turn on identity insert to allow us to insert auto incement field
                                identityInsert("on");
                                rowcount = 0;
                                //for each row in the table write to SQL table

                                foreach (DataRow Accessrow in Accessdt.Rows)
                                {

                                    SQLnewRow = SQLdt.NewRow();
                                    SQLnewRow["OrderNo"] = Accessrow["OrderNo"];
                                    SQLnewRow["SaleNo"] = Accessrow["SaleNo"];
                                    
                                    SQLnewRow["StockRef"] = Accessrow["StockRef"];
                                    SQLnewRow["Description"] = Accessrow["Description"];
                                    SQLnewRow["OrderQty"] = Accessrow["OrderQty"];
                                    SQLnewRow["RetailPrice"] = Accessrow["RetailPrice"];
                                    SQLnewRow["Ext Price"] = (Accessrow["Ext Price"]);
                                    SQLnewRow["VATPaid"] = Accessrow["VATPaid"];
                                    SQLnewRow["ItemsReturned"] = Accessrow["ItemsReturned"];
                                    SQLnewRow["RefundAmount"] = Accessrow["RefundAmount"];
                                    SQLnewRow["RefundDate"] = Accessrow["RefundDate"];
                                    SQLnewRow["DeptCode"] = Accessrow["DeptCode"];

                                    SQLnewRow["TaxApplicable"] = returnBool(Accessrow["TaxApplicable"].ToString());
                                    SQLnewRow["SellByValue"] = returnBool(Accessrow["SellByValue"].ToString());
                                    SQLnewRow["TaxCode"] = Accessrow["TaxCode"];
                                    SQLnewRow["CostPrice"] = Accessrow["CostPrice"];
                                    SQLnewRow["CatalogueCode"] = Accessrow["CatalogueCode"];
                                    SQLnewRow["MarkUp"] = Accessrow["MarkUp"];
                                    SQLnewRow["AltBarcode"] = Accessrow["AltBarcode"];


                                    SQLdt.Rows.Add(SQLnewRow);

                                    rowcount += 1;
                                    refreshlabel3(rowcount.ToString());
                                    


                                }
                                //remember to turn off indentity insert to allow autoincrementer.
                                
                                SQLda.Update(Mainds, "tbl_SalesLines");
                                MessageBox.Show(rowcount.ToString());
                                identityInsert("off");
                                sql = "ALTER TABLE tbl_SalesLines CHECK CONSTRAINT ALL";
                                SQLda = new SqlDataAdapter();
                                SQLda.SelectCommand = new SqlCommand(sql, conn);
                                
                                break;

                            
                            default:
                                break;

                        }
                        tableCount += 1;
                    }

                }
    
            }
            //any problems then 
            catch (SqlException ae)
            {
                MessageBox.Show(ae.Message.ToString());
            }


            // turn on constraints for tables here
            sql = "ALTER TABLE tbl_SalesLines CHECK CONSTRAINT ALL";
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            sql = "ALTER TABLE tbl_Items CHECK CONSTRAINT ALL";
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

        }

        private void refreshlabel3(string rowcount)
        {
            label3.Text = rowcount;
            label3.Refresh();
        }


        private void btnDropTable_Click(object sender, EventArgs e)
        {
            dropTable("tbl_Departments");

            dropTable("tbl_AdditionalBarcodes");
            dropTable("tbl_Items");
            dropTable("tbl_Loyalty");
            dropTable("tbl_SalesLines");
            dropTable("tbl_AddHocRefundsDetails");
            dropTable("tbl_AddHocReturns");
            
            dropTable("tbl_BackGroundColours");
            dropTable("tbl_Barcodes");
            dropTable("tbl_CurrencyConversion");
            dropTable("tbl_DefaultScreen");
            dropTable("tbl_InvoicePayments");
            dropTable("tbl_Invoices");
            dropTable("tbl_LayerProtection");
            dropTable("tbl_Payments");
            dropTable("tbl_SerialNumbers");
            dropTable("tbl_SetUp");
            dropTable("tbl_Suppliers");
            dropTable("tbl_TaxDetails");
            dropTable("tbl_TextColours");
            dropTable("tbl_Token");
            dropTable("tbl_ZReset");
        

        }

        private void btnViewData_Click(object sender, EventArgs e)
        {
            /// Open the connection
            if ((conn != null) && (conn.State == ConnectionState.Open))
                conn.Close();
            ConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;" +
                               "Initial Catalog = ePOSData;" +
                               "Integrated Security = SSPI";
            conn.ConnectionString = ConnectionString;
            conn.Open();
            // Create a data adapter
            SqlDataAdapter da = new SqlDataAdapter
            ("SELECT * FROM tbl_Items", conn);
            // Create DataSet, fill it and view in data grid
            DataSet ds = new DataSet("myTable");
            da.Fill(ds, "items_tbl");
            dataGrid1.DataSource = ds.Tables["items_tbl"].DefaultView;
        }

 

        // Called when you are done with the applicaton
        // Or from Close button
        private void AppExit()
        {
            if (reader != null)
                reader.Close();
            if ((conn != null) && (conn.State == ConnectionState.Open))
                conn.Close();
            if ((accessConn != null) && (accessConn.State == ConnectionState.Open))
                accessConn.Close();

            

            Application.Exit();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            AppExit();
        }

        private void btnDropDB_Click(object sender, EventArgs e)
        {
            string sql = "DROP DATABASE ePOSData";
            ExecuteSQLStmt(sql); 
        }

        private void btnSelectFirstEpos_Click(object sender, EventArgs e)
        {
            filename = SelectFile(@"C:\");
            lblSelectedDatabase.Text = filename;
            AccessConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=" + filename;
            accessConn = new OleDbConnection(AccessConnectionString);
            loadAccessDatabase();

        }
        
        private string SelectFile(string initialDirectory)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
                   "First Epos files (*.mdb)|*.mdb|All Files (*.*)|*.*";
            dialog.InitialDirectory = initialDirectory;
            dialog.Title = "Select a First Epos File";
            return Path.GetFullPath((dialog.ShowDialog() == DialogResult.OK)
                ? dialog.FileName : "No File Selected");
        }

        private void loadAccessDatabase()
        {
            listBox1.Items.Clear();

            // Execute the query.
            try
            {
                //Open the Connection
                accessConn.Open();
                object[] objArrRestrict;
                //select just TABLE in the Object array of restrictions.
                //Remove TABLE and insert Null to see tables, views, and other objects.
                objArrRestrict = new object[] { null, null, null, "TABLE" };
                accessTable = accessConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, objArrRestrict);

                // Display the table name from each row in the schema
                foreach (DataRow row in accessTable.Rows)
                {
                    listBox1.Items.Add(row["TABLE_NAME"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally
            {
                //Explicitly closing the connection without waiting for Garbage Collection
                accessConn.Close();
            }

            //Label for Database Name
            label1.Text = "Tables in " + accessConn.Database.ToString() + " Database";

            // To select the First Item in the List Box1
            if (listBox1.Items.Count > 0)  listBox1.SetSelected(0, true);


        
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            //Select a table name.
            string selTbl = listBox1.SelectedItem.ToString();
            try
            {
                accessConn.Open();
                object[] objArrRestrict;
                objArrRestrict = new object[] { null, null, selTbl, null };
                //
                accessTableCols = accessConn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, objArrRestrict);
                //List the schema info for the selected table
                foreach (DataRow row in accessTableCols.Rows)
                {
                    listBox2.Items.Add(row["COLUMN_NAME"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally
            {
                accessConn.Close();
            }

            //Label for Column Names
            label2.Text = "Column Names in " + selTbl + " Table";
        }

        private DataTable fillAccessDataTable(string commandStr )
        {
            OleDbCommand accessCmd = new OleDbCommand(commandStr, accessConn);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = accessCmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        private void identityInsert(string status)
        {
            if (status == "on")
            {
                sql = "SET IDENTITY_INSERT tbl_Departments ON";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            else
            if (status == "off")
            {
                sql = "SET IDENTITY_INSERT tbl_Departments OFF";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        private void dropTable(String tableName)
        {
            string sql = "DROP TABLE " + tableName;
            ExecuteSQLStmt(sql);
     
        }

        private int returnBool(string str)
        {
            if (str == "True") return 1;
            else return 0;
            
        }

        private DateTime returnDate()
        {
           DateTime date = new DateTime (2011,12,1);

            //MessageBox.Show(date.ToString());

            return date;    
        }

        /// In the case of null values in a data table, this method
        /// will turn all nulls into zeros instead.
        /// </summary>
        public static DataTable CleanDataTable(DataTable dt)
        {
            for (int a = 0; a < dt.Rows.Count; a++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Rows[a][i] == DBNull.Value)
                    {
                        Type type = dt.Columns[i].DataType;
                        if (type == typeof(int) || type == typeof(float) || type == typeof(double))
                        {
                            dt.Columns[i].ReadOnly = false;
                            dt.Rows[a][i] = 0.0F;
                        }
                    }
                }
            }

            return dt;
        }

        private void btnCsv_Click(object sender, EventArgs e)
        {

            using (CsvFileWriter writer = new CsvFileWriter("WriteTest.csv"))
            {

                foreach (DataRow itemRow in itemsdt.Rows)
                {
                    CsvRow row = new CsvRow();
                    for (int j = 0;j<31; j++)
                        row.Add(String.Format(itemRow[j].ToString()));
                    writer.WriteRow(row);

                }

               

            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
