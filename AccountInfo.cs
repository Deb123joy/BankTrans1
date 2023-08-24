using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace BankAccountInterest1
{
    class AccountInfo
    {
        private string date1;
        private string ActNum;
        private string TransType1;
        private Decimal Amount1;
        private string TransID1;
        public string dt
        {
            get { return date1; }
            set { date1 = value; }

        }

        public string AccounttNum
        {
            get { return ActNum; }
            set { ActNum = value; }
        }
        public string TransType
        {
            get { return TransType1; }
            set { TransType1 = value; }
        }
        public Decimal Amount
        {
            get { return Amount1; }
            set { Amount1 = value; }
        }
        public string TransID
        {
            get { return TransID1; }
            set { TransID1 = value; }
        }
        public static SqlConnection getconnectionstring()
        {
            string conStr = ConfigurationManager.ConnectionStrings["connstr"].ToString();
            SqlConnection con = new SqlConnection(conStr);


            return con;


        }

        public void AddTrans(AccountInfo A)
        {
            SqlConnection con1 = getconnectionstring();
            con1.Open();
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddTrans";
                cmd.Connection = con1;
                string TransxnID = "_" + DateTime.Now.ToString("yyyyMMdd");
                cmd.Parameters.AddWithValue("@TransID", TransxnID);
                cmd.Parameters.AddWithValue("@AccountID", A.ActNum.ToString());
                cmd.Parameters.AddWithValue("@TrnDate", A.dt);
                cmd.Parameters.AddWithValue("@TransType", A.TransType.ToString());
                
                cmd.Parameters.AddWithValue("@Amount", Convert.ToDecimal(A.Amount));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con1.Close();
            }
        }
        public int TransCount(string AccounttNum)
        {
            SqlConnection con1 = getconnectionstring();
            con1.Open();
           
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "check_firtsTrans";
                cmd.Connection = con1;
                cmd.Parameters.AddWithValue("@AcctNum", AccounttNum.ToString());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    //if (!dr.IsDBNull(dr.GetOrdinal("TransCount")))
                    while (dr.Read())
                    {

                        int Transcount = dr.GetInt32(0);
                        return Transcount;
                    }
                }
               return 99999;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con1.Close();
            }


        }
        public Decimal CheckBalance(string AccounttNum, Decimal WithdrawAmt)
        {
            SqlConnection con1 = getconnectionstring();
            con1.Open();
            Decimal BalAfterwithdrawal=0;
            Decimal bal=0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "check_Balance";
                cmd.Connection = con1;
                cmd.Parameters.AddWithValue("@AcctNum", AccounttNum.ToString());
                cmd.Parameters.AddWithValue("@WithdrawAmount", WithdrawAmt);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr["Bal"] != DBNull.Value)
                            BalAfterwithdrawal = Convert.ToDecimal(dr["Bal"].ToString());
                    }
                }

                return BalAfterwithdrawal;
                


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con1.Close();
            }


        }
        public DataSet GetAccountTransactions(string AccounttNum)
        {
            SqlConnection con1 = getconnectionstring();
            con1.Open();
             try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAccountTransactions";
                cmd.Connection = con1;
                cmd.Parameters.AddWithValue("@AcctNum", AccounttNum.ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                return dataset;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               // dr.Close();
                con1.Close();
            }


        }

        public DataSet GenerateMonthlyReportforAccount(string AccounttNum, string TransMonth)
        {
            SqlConnection con1 = getconnectionstring();
            con1.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GenerateMonthlyReportforAccount";
                cmd.Connection = con1;
                cmd.Parameters.AddWithValue("@AcctNum", AccounttNum.ToString());
                cmd.Parameters.AddWithValue("@TransMonth", TransMonth);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                return dataset;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con1.Close();
            }


        }

    }
}

