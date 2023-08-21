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
    class InterestRates
    {
        private string interestDate1;
        private string RuleID1;
        private Decimal Rate1;
        public string interestDate
        {
            get { return interestDate1; }
            set { interestDate1 = value; }

        }
        public string RuleID
        {
            get { return RuleID1; }
            set { RuleID1 = value; }

        }
        public Decimal Rate
        {
            get { return Rate1; }
            set { Rate1 = value; }
        }
        public static SqlConnection getconnectionstring()
        {
            string conStr = ConfigurationManager.ConnectionStrings["connstr"].ToString();
            SqlConnection con = new SqlConnection(conStr);


            return con;


        }

        public void AddInterestRate(InterestRates I)
        {
            SqlConnection con1 = getconnectionstring();
            con1.Open();
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddINterestRule";
                cmd.Connection = con1;

                cmd.Parameters.AddWithValue("@IntDate", I.interestDate);
                cmd.Parameters.AddWithValue("@RuleID", I.RuleID);
                //float amountavailable = float.Parse(A.Amount.ToString().Trim());
                // decimal amountavailable = Convert.ToDecimal(A.Amount.ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat);
                cmd.Parameters.AddWithValue("@Rate", Convert.ToDecimal(I.Rate));
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
        public DataSet GetInterestRules()
        {
            SqlConnection con1 = getconnectionstring();
            con1.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetInterestRules";
                cmd.Connection = con1;
                //cmd.Parameters.AddWithValue("@AcctNum", AccounttNum.ToString());

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
