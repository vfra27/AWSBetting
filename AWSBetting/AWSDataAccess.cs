using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.RDS;
using System.Data.SqlClient;

namespace AWSBetting
{
    public class AWSDataAccess
    {
        private static SqlConnectionStringBuilder Initialize()
        {
            //DataSet ds = new DataSet();

            SqlConnectionStringBuilder dbConString = new SqlConnectionStringBuilder();
            //dbConString.UserID = "vfra87";
            //dbConString.Password = "12345678";
            //dbConString.DataSource = "betting.ctmdahjqzmcq.us-east-1.rds.amazonaws.com,1433";
            //dbConString.AttachDBFilename = "BettingPerding";
            dbConString.ConnectionString = "Data Source=betting.ctmdahjqzmcq.us-east-1.rds.amazonaws.com,1433;Initial Catalog=BettingPerding;User ID=vfra87;Password=12345678";
            dbConString.ConnectTimeout=0;
            return dbConString;
            #region oldcode

            //dbConString.Encrypt = true;

            //using (SqlConnection con = new SqlConnection(dbConString.ConnectionString))
            //{
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand("Select * from Team", con);
            //    //var adapter = new SqlDataAdapter(cmd);
            //    //adapter.Fill(ds);
            //    cmd.Dispose();
            //}


            //AWSConfigs.AWSRegion = "us-east-1";
            //var loggingConfig = AWSConfigs.LoggingConfig;
            //loggingConfig.LogMetrics = true;
            //loggingConfig.LogResponses = ResponseLoggingOption.Always;
            //loggingConfig.LogMetricsFormat = LogMetricsFormatOption.JSON;
            //loggingConfig.LogTo = LoggingOptions.SystemDiagnostics;

            ////CognitoCachingCredentialsProvider
            //CognitoAWSCredentials credentials = new CognitoAWSCredentials(
            //    "us-east-1:ec8dc78c-125d-48c7-bc83-1722ed1add43", // Identity Pool ID
            //    RegionEndpoint.USEast1);

            //QueryAsync(credentials, RegionEndpoint.USEast1);




            //var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);

            //DynamoDBContext context = new DynamoDBContext(client, new DynamoDBContextConfig
            //{
            //    Conversion = DynamoDBEntryConversion.V2
            //});


            //Team juve = new Team()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "Juventus",
            //    Status = false,
            //    Bet = "X",
            //    Quantity = new List<double>()
            //    {
            //        2,4,6,8,16,20,25,25,50,35,100,120
            //    }

            //};
            //context.SaveAsync(juve);

            //Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
            //{
            //    {"Id", new AttributeValue {S="" } }
            //};
            //GetItemRequest request = new GetItemRequest
            //{
            //    TableName = "Team",
            //    Key = key,
            //};

            //var result = await client.GetItemAsync(request);
            //System.Diagnostics.Debug.WriteLine("Item:");
            //Dictionary<string, AttributeValue> item = result.Item;
            //foreach (var keyValuePair in item)
            //{
            //    System.Diagnostics.Debug.WriteLine("Name:={0}",item["Name"]);
            //    System.Diagnostics.Debug.WriteLine("Bet:={0}", item["Bet"]);
            //}
            #endregion
        }

        public static Guid InsertRecharge(Recharge r)
        {
            string sqlQuery = "Insert into Recharge(Id,Amount,Date,Bet_Provider) OUTPUT Inserted.Id "
                + "Values (@id, @amount, @date, @betProvider)";

            Guid result = Guid.Empty;

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = r.Id;
                cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = r.Amount;
                cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = r.Date;
                cmd.Parameters.Add("@betProvider", SqlDbType.Int).Value = r.BetProvider;
                //cmd.Connection= new SqlConnection(Initialize().ConnectionString);
                result = Insert(cmd);
            }

            return result;
        }


        public static Guid InsertTeam(Team t)
        {
            //String.Format(, t.Id, t.Name, t.Status, t.Bet,t.Win)
            string sqlQuery = "Insert into Team (Id,Name,Status,Bet,Win,Bet_Provider,Total_Cost) " 
                + "OUTPUT Inserted.Id "
                + "Values (@id, @name, @status, @bet, @win, @betProvider, @totalCost);";
            Guid result = Guid.Empty;

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier ).Value= t.Id;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = t.Name;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = t.Status;
                cmd.Parameters.Add("@bet", SqlDbType.NVarChar).Value = t.Bet;
                //SqlParameter win = new SqlParameter("@win",SqlDbType.Money);
                cmd.Parameters.Add("@win", SqlDbType.Decimal).Value = t.Win;
                cmd.Parameters.Add("@totalCost", SqlDbType.Decimal).Value = t.TotalCost;
                cmd.Parameters.Add("@betProvider", SqlDbType.Int).Value = t.BetProvider;
                //cmd.Connection= new SqlConnection(Initialize().ConnectionString);
                result = Insert(cmd);
            }
            
            return result;
        }


        public static Guid InsertBetDetails(BetDetails details)
        {
            //String.Format(  '{0}', '{1}', '{2}', GETDATE() , details.Id, details.Quantity, details.Team_Id
            string sqlQuery = "Insert into BetDetails (Id,Quantity,Team_Id,Date) OUTPUT Inserted.Id " 
                + "Values (@id, @quantity, @team_id, @date);";

            Guid result = Guid.Empty;

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = details.Id;
                cmd.Parameters.Add("@quantity", SqlDbType.Decimal).Value = details.Quantity;
                cmd.Parameters.Add("@team_id", SqlDbType.UniqueIdentifier).Value = details.Team_Id;
                cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTime.Now;                
                //cmd.Connection= new SqlConnection(Initialize().ConnectionString);
                result = Insert(cmd);
            }
            //Guid result = Insert(sqlQuery);
            return result;
        }

        //string insertQuery
        private static Guid Insert(SqlCommand cmd)
        {
            Guid result;
            using (SqlConnection connection = new SqlConnection(Initialize().ConnectionString))
            {
                connection.Open();

                try
                {
                    cmd.Connection = connection;
                    result = new Guid(cmd.ExecuteScalar().ToString());
                }
                catch (Exception e)
                {
                    result = Guid.Empty;
                }

                //using (SqlCommand command = new SqlCommand(insertQuery, connection))
                //{
                                        
                //}
                connection.Close();
            }
            
            return result;

        }



        public static bool DeleteBetDetail(BetDetails betDetail)
        {
            //OUTPUT Inserted.Id 
            string sqlQuery = "DELETE from BetDetails Where ID= @id AND quantity=@quantity "
                +"AND team_id=@teamId;";
            bool result = false;

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = betDetail.Id;
                cmd.Parameters.Add("@quantity", SqlDbType.Decimal).Value = betDetail.Quantity;
                cmd.Parameters.Add("@teamId", SqlDbType.UniqueIdentifier).Value = betDetail.Team_Id;
                result = Delete(cmd);
            }

            return result;
        }


        public static bool DeleteAllBetTeam(Team t)
        {
            //OUTPUT Inserted.Id 
            string sqlQuery = "DELETE from Team Where ID= @id ;";
            bool result = false;

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = t.Id;
                result = Delete(cmd);
            }

            return result;
        }

        private static bool Delete(SqlCommand cmd)
        {
            bool result=false;
            using (SqlConnection connection = new SqlConnection(Initialize().ConnectionString))
            {
                connection.Open();

                try
                {
                    cmd.Connection = connection;
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        result = true;
                    }
                    
                }
                catch (Exception e)
                {
                    result = false;
                }

                //using (SqlCommand command = new SqlCommand(insertQuery, connection))
                //{

                //}
                connection.Close();
            }

            return result;

        }

        public static Guid UpdateBetTeamDetail(BetDetails betTeamDetail)
        {
            string sqlQuery = "Update BetDetails SET ID = @id, Quantity=@quantity,team_id=@teamId" 
                +",date=@date OUTPUT Inserted.Id "
                + "Where ID=@id ; ";
            Guid result = Guid.Empty;

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = betTeamDetail.Id;
                cmd.Parameters.Add("@quantity", SqlDbType.Decimal).Value = betTeamDetail.Quantity;
                cmd.Parameters.Add("@teamId", SqlDbType.UniqueIdentifier).Value = betTeamDetail.Team_Id;
                cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTime.Now;
                //cmd.Parameters.Add("@bet", SqlDbType.NChar).Value = t.Bet;
                //SqlParameter win = new SqlParameter("@win",SqlDbType.Money);                
                //win.Va


                //cmd.Connection= new SqlConnection(Initialize().ConnectionString);
                result = Insert(cmd);
            }

            return result;

        }


        public static Guid UpdateCloseWin(Team t)
        {
            string sqlQuery = "Update Team SET Status = @status, Win=@win, Total_Cost=@totalCost "
                +"OUTPUT Inserted.Id "
                +"Where ID=@id ; ";
            Guid result = Guid.Empty;

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = t.Status;
                cmd.Parameters.Add("@win", SqlDbType.Decimal).Value = t.Win;
                cmd.Parameters.Add("@totalCost", SqlDbType.Decimal).Value = t.TotalCost;
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = t.Id;                
                
                //cmd.Parameters.Add("@bet", SqlDbType.NChar).Value = t.Bet;
                //SqlParameter win = new SqlParameter("@win",SqlDbType.Money);                
                //win.Va

                
                //cmd.Connection= new SqlConnection(Initialize().ConnectionString);
                result = Insert(cmd);
            }

            return result;

        } 

        //private static Guid Update(SqlCommand cmd)
        //{
        //    Guid result;
        //    using (SqlConnection connection = new SqlConnection(Initialize().ConnectionString))
        //    {
        //        connection.Open();

        //        try
        //        {
        //            cmd.Connection = connection;
        //            result = new Guid(cmd.ExecuteScalar().ToString());
        //        }
        //        catch (Exception e)
        //        {
        //            result = Guid.Empty;
        //        }

        //        //using (SqlCommand command = new SqlCommand(insertQuery, connection))
        //        //{

        //        //}
        //        connection.Close();
        //    }

        //    return result;
        //}

        public static string DoFormat(decimal myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return ((int)myNumber).ToString();
            }
            else
            {
                return s;
            }
        }


        public static List<Recharge> GetRecharges()
        {
            List<Recharge> recharges = new List<Recharge>();
            string sqlQuery = "Select * from recharge where bet_provider=@betProvider";
            int provider = (int)ApplicationState.ActiveProvider;

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Parameters.Add("@betProvider", SqlDbType.Int).Value = provider;
                using (SqlConnection connection = new SqlConnection(Initialize().ConnectionString))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Recharge r = new Recharge();
                                r.Id = new Guid(dataReader["Id"].ToString());
                                r.Amount = Convert.ToDecimal(dataReader["Amount"]);
                                //r.Date = new Guid(dataReader["Date"].ToString());
                                recharges.Add(r);
                            }
                        }
                        dataReader.Close();
                    }
                    connection.Close();
                }
            }
            return recharges;
        }


        public static List<BetDetails> GetBetDetailsByTeamId(Guid teamId)
        {

            List<BetDetails> betDetails = new List<BetDetails>();

            string sqlQuery = "Select * from betdetails where team_id = @teamId " +
                "order by date, quantity";


            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Parameters.Add("@teamId", SqlDbType.UniqueIdentifier).Value = teamId;
                //String.Format("Select * from betdetails where team_id = '{0}' " +
                //"order by date, quantity", teamId.ToString())

                using (SqlConnection connection = new SqlConnection(Initialize().ConnectionString))
                {

                    connection.Open();
                    cmd.Connection = connection;
                    //Select(cmd)
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                BetDetails d = new BetDetails();
                                d.Id = new Guid(dataReader["Id"].ToString());
                                d.Quantity = Convert.ToDecimal(dataReader["Quantity"]);
                                d.Team_Id = new Guid(dataReader["Team_Id"].ToString());
                                betDetails.Add(d);
                            }
                        }
                        dataReader.Close();
                    }
                    connection.Close();
                }                                
            }

            

            return betDetails;
        }



        public static List<Team> GetBetTeam(int status)
        {
            List<Team> teams = new List<Team>();

            string sqlQuery = string.Empty;

            //if (status==0)
            //{

            //    sqlQuery = "Select * from team inner join betdetails on " +
            //    "team.id= betdetails.team_id "+
            //    "where status= @status and bet_provider=@betProvider "+
            //    "order by date desc ";
            //}
            //else if (status==1)
            //{
            //    sqlQuery = "Select * from team " +
            //    "where status= @status and bet_provider=@betProvider ";
            //}

            sqlQuery = "Select * from team " +
                 "where status= @status and bet_provider=@betProvider ";

            int provider = (int)ApplicationState.ActiveProvider;

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Parameters.Add("@status", SqlDbType.Int).Value = status;
                cmd.Parameters.Add("@betProvider", SqlDbType.Int).Value = provider;

                //String.Format("Select * from team where status = {0} and "
                //+"bet_provider={1}", status, provider))

                using (SqlConnection connection = new SqlConnection(Initialize().ConnectionString))
                {

                    connection.Open();
                    cmd.Connection = connection;

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Team t = new Team();
                                t.Id = new Guid(dataReader["Id"].ToString());
                                t.Name = dataReader["Name"].ToString();
                                t.Status = Convert.ToBoolean(dataReader["Status"]);
                                t.Bet = dataReader["Bet"].ToString();
                                t.Win = Convert.ToDecimal(dataReader["Win"]);

                                //if (dataReader["Win"] != null)
                                //{
                                //    object c = dataReader["Win"].ToString();

                                //}                        
                                teams.Add(t);
                            }
                        }
                        dataReader.Close();
                    }

                    connection.Close();
                }

                //Select(cmd)
                
            }            

            
            return teams;
        }



        private static SqlDataReader Select(SqlCommand cmd)
        {
            //string selectQuery

            SqlDataReader result;
            using (SqlConnection connection = new SqlConnection(Initialize().ConnectionString))
            {
                
                connection.Open();
                cmd.Connection = connection;
                result = cmd.ExecuteReader();
                
                connection.Close();
            }

            return result;

        }

        //public static void QueryAsync(CognitoAWSCredentials credentials, 
        //    RegionEndpoint region)
        //{
        //    var client = new AmazonDynamoDBClient(credentials, region);

        //    using (DynamoDBContext context = new DynamoDBContext(client))
        //    {
        //        context.LoadAsync<Team>(1).RunSynchronously();
        //        System.Diagnostics.Debug.WriteLine("finitooooo");

        //    }

        //    //"2a37325d-8a94-4fa6-8c60-772a85f228f0"
        //    //Task<Team> retrievedTeam = context.LoadAsync<Team>(1);
        //    //Team juventus = retrievedTeam.Result;
        //    //retrievedTeam.Wait();

        //    //context.LoadAsync<Team>("2a37325d-8a94-4fa6-8c60-772a85f228f0",
        //    //    );


        //    //var search = context.FromQueryAsync<Team>(new Amazon.DynamoDBv2.DocumentModel.QueryOperationConfig()
        //    //{
        //    //    IndexName = "Name-index",
        //    //    Filter = new Amazon.DynamoDBv2.DocumentModel.QueryFilter("Name", Amazon.DynamoDBv2.DocumentModel.QueryOperator.Equal, "Juventus")
        //    //});

        //   // System.Diagnostics.Debug.WriteLine("Item retrieved " + retrievedTeam.Name+" "+retrievedTeam.Bet);


        //    //var searchResponse = await search.GetRemainingAsync();
        //    //searchResponse.ForEach((t) =>
        //    //{
        //    //    Console.WriteLine(t.Name);
        //    //});

        //}


    }
}