using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace BankAccountInterest1
{
    class Program
    {
        class ValidateResult
        {
            public string ValidMessage;
            public bool IsValid;
        }
        
        static void Main(string[] args)
        {
            Decimal d1=0;// = Convert.ToDecimal("100.00");

            //Console.WriteLine(d1);

            AccountInfo a = new AccountInfo();
            InterestRates I = new InterestRates();
            
            Console.WriteLine("Welcome to AwesomeGIC Bank! What would you like to do?\n[I]nput transactions \n[D]efine interest rules\n[P]rint statement \n[Q]uit \n");
            string Opt = Console.ReadLine();
            OptionMadebyUser(Opt,a,d1,I);
            
        }
        public static bool IsValidDate(string value, string[] dateFormats)
        {
            string[] formats = { "yyyyMMdd" };
            DateTime tempDate;
            bool validDate = DateTime.TryParseExact(value, formats, System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.None, out tempDate);
            if (validDate)
                return true;
            else
                return false;
        }
        private static ValidateResult ValidateInputTrans(string dt,string ActType, Decimal Amount)
        {
            ValidateResult vr2 = new ValidateResult();
            string[] DtFormat = { "YYYYMMdd" };

            bool isValidinput = false;
            string Alert=null;
            if (IsValidDate(dt, DtFormat))
            {
                isValidinput= true;
                //Do something
            }
            else
            {
                isValidinput= false;
                Alert = "Date Fomrat should be in YYYYMMDD";
                        //Do something else
                    }
            var list = new string[] { "W", "w", "D", "d"};

            if (list.Contains(ActType))
            {
                isValidinput = true;
            }
            else
            {
                isValidinput = false;
                Alert += " Type should be 'W','w','D','d'";
                
            }
            if(Amount>0)
            {
               
                if (decimal.Round(Amount, 2) == Amount)
                {
                    isValidinput = true;
                }
                else
                {
                    Alert += " Decimals in the amount are allowed up to 2 decimal places only";
                }
                
            }
            else
            {
                isValidinput = false;
                Alert += " Amount must be greater than zero  ";
            }
            

            vr2.IsValid = isValidinput;
            vr2.ValidMessage = Alert;
            return vr2;
            

        }
        private static void InputTransactions(AccountInfo a, Decimal d1)
        {
            string userinput = Console.ReadLine();
            a.dt = userinput.Substring(0, 8);
            a.AccounttNum = userinput.Substring(9, 5);
            a.TransType = userinput.Substring(15, 1);
            string s1 = userinput.Substring(17, 6);
            if (Decimal.TryParse(s1, out d1))
            {
                a.Amount = d1;
                Console.WriteLine("Account:" + a.AccounttNum);
                Console.WriteLine("Date     | Txn Id      | Type | Amount |");

                DataSet ds = a.GetAccountTransactions(a.AccounttNum);
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {

                    Console.Write(Convert.ToDateTime(row["Date"]).ToString("yyyyMMdd") + "     | ");
                    Console.Write(row["Trans_ID"].ToString() + "     | ");
                    Console.Write(row["TransType"].ToString() + "     | ");
                    Console.Write(row["Amount"].ToString() + "     | ");
                    Console.WriteLine();
                }
            }

            ValidateResult vr1 = new ValidateResult();
            vr1 = ValidateInputTrans(a.dt, a.TransType, a.Amount);
            if (vr1.IsValid)
            {
                if (a.TransCount(a.AccounttNum) > 0)
                {
                    Decimal Balance = a.CheckBalance(a.AccounttNum, a.Amount);
                    if (Balance <= 0)
                    {
                        vr1.ValidMessage = "balance go below 0";
                    }
                    else
                    {
                        a.AddTrans(a);
                    }
                }
                else
                {
                    var list = new string[] { "W", "w" };
                    if (list.Contains(a.TransType))
                    {
                        vr1.ValidMessage = "This is first transaction your an account should not be a withdrawal";
                    }
                    else
                    {
                        a.AddTrans(a);
                    }
                }

            }
            else
            {
                Console.Write(vr1.ValidMessage);
            }
        }
        private static void OptionMadebyUser(string opt, AccountInfo a, Decimal d1,InterestRates I)
        {
            switch (opt)
            {
                case "I":
                    Console.WriteLine("Please enter transaction details in <Date>|<Account>|<Type>|<Amount> format: (or enter blank to go back to main menu):");
                    InputTransactions(a, d1);
                    Console.WriteLine("Is there anything else you'd like to do?\n[I]nput transactions[D]efine interest rules\n[P]rint statement \n[Q]uit \n");
                    string Opt = Console.ReadLine();
                    OptionMadebyUser(Opt, a, d1,I);
                    break;
                case "D":
                    string[] DtFormat2 = { "YYYYMMdd" };
                    Console.WriteLine("Please enter interest rules details in <Date>|<RuleId>|<Rate in %> format \n (or enter blank to go back to main menu):");
                    string userinputD = Console.ReadLine();
                    I.interestDate = userinputD.Substring(0, 8);
                    I.RuleID = userinputD.Substring(9, 6);
                    string s = userinputD.Substring(16, 4);
                    if (Decimal.TryParse(s, out d1))
                    {
                        I.Rate = d1;
                    }
                    if ((I.Rate > 0) && (I.Rate < 100))
                    {
                        if (IsValidDate(I.interestDate, DtFormat2))
                        {
                            I.AddInterestRate(I);
                        }
                        else
                        {
                            Console.WriteLine("Date Fomrat should be in YYYYMMDD");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Interest out of range. Should be <100 and >0");

                    }
                    Console.WriteLine("Interest rules: ");


                    Console.WriteLine("Date | RuleId | Rate(%) |");
                    DataSet ds4 = I.GetInterestRules();
                    DataTable dt2 = ds4.Tables[0];

                    foreach (DataRow row in dt2.Rows)
                    {
                        Console.Write(Convert.ToDateTime(row["InterestDate"]).ToString("yyyyMMdd") + "     | ");
                        Console.Write(row["RuleID"].ToString() + "     | ");
                        Console.Write(row["Rate"].ToString() + "     | ");
                        Console.WriteLine();

                    }
                    Console.WriteLine("Is there anything else you'd like to do?\n[I]nput transactions[D]efine interest rules\n[P]rint statement \n[Q]uit \n");
                    Opt = Console.ReadLine();
                    OptionMadebyUser(Opt, a, d1, I);
                    break;
                case "P":
                    Console.WriteLine("Please enter account and month to generate the statement <Account>|<Month> \n (or enter blank to go back to main menu):");
                    string userinputP = Console.ReadLine();
                    a.AccounttNum = userinputP.Substring(0, 5);
                    string TransMonth = userinputP.Substring(6, 2);
                    Console.WriteLine("Account: " + a.AccounttNum);
                    Console.WriteLine("Date | Txn Id | Type | Amount | Balance |");
                    DataSet ds = a.GenerateMonthlyReportforAccount(a.AccounttNum, TransMonth);
                    DataTable dt3 = ds.Tables[0];
                    
                        foreach (DataRow row in dt3.Rows)
                    {
                        Console.Write(Convert.ToDateTime(row["TrnDate"]).ToString("yyyyMMdd") + "     | ");
                       
                        Console.Write(row["Trans_ID"].ToString() + "     | ");
                        Console.Write(row["TransType"].ToString() + "     | ");
                        Console.Write(row["Amount"].ToString() + "     | ");
                        Console.Write(row["BalanceAmount"].ToString() + "     | ");
                        Console.WriteLine();

                    }
                    Console.WriteLine("Is there anything else you'd like to do?\n[I]nput transactions[D]efine interest rules\n[P]rint statement \n[Q]uit \n");
                    Opt = Console.ReadLine();
                    OptionMadebyUser(Opt, a, d1, I);
                    break;
                case "Q":
                    Console.WriteLine("Thank you for banking with AwesomeGIC Bank. \n Have a nice day!");
                    break;
                default:

                    string userinputDef = Console.ReadLine();
                    break;
            }
        }
    }


}
