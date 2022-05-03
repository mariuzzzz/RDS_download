using Newtonsoft.Json;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;



namespace rds_ataskaitos

{
    public class radio
    {
        DBaccess dBaccess = new DBaccess();

        public void RDS_Load()
        {
            string apiKey = "X2vHCFDUxfu4yPHrddpsGDjkrm68zH7perATLVDN";
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("x-api-key", apiKey);
                string response = webClient.DownloadString("http://c0983.w.dedikuoti.lt/api/live");
                List<RDS_model> rds_model = JsonConvert.DeserializeObject<List<RDS_model>>(response);
                string timeStamp = string.Empty;
                string title = string.Empty;
                string senderId = string.Empty;
                string senderName = string.Empty;
                string artist = string.Empty;
                string channelId = string.Empty;
                string showTitle = string.Empty;
                for (int i = 0; i < rds_model.Count; i++)
                {
                    title = rds_model[i].title;
                    timeStamp = rds_model[i].rdsTimestampCreated;
                    senderId = rds_model[i].senderId;
                    senderName = rds_model[i].senderName;
                    artist = rds_model[i].artist;
                    channelId = rds_model[i].channel_id;
                    showTitle = rds_model[i].showTitle;
                   // Console.WriteLine();
                    string querry = "select * from rds_details where rdsTimeStampCreated != '" + timeStamp + "'";
                    string q = "select * from rds_details where sender_id = '" + senderId + "'";
                    string deleteQ = "DELETE T FROM (SELECT *, DupRank = ROW_NUMBER() OVER(PARTITION BY sender_id ORDER BY(SELECT NULL)) FROM rds_details) AS T WHERE DupRank > 1 ";
                    try
                    {
                        if (dBaccess.readDatathroughReader(q).HasRows)
                        {
                            dBaccess.closeConn();

                            if (dBaccess.readDatathroughReader(querry).HasRows)
                            {
                                dBaccess.closeConn();

                                SqlCommand updCommand = new SqlCommand("update rds_details set sender_id = @senderId, sender_name = @senderName, title = @title, artist = @artist, rdsTimeStampCreated = @timeStamp, channel_id = @channelId, show_title = @showTitle where sender_id = '" + senderId + "'");
                                if (string.IsNullOrEmpty(title))
                                    updCommand.Parameters.Add("@title", DBNull.Value);
                                else
                                    updCommand.Parameters.Add("@title", @title);
                                if (string.IsNullOrEmpty(artist))
                                    updCommand.Parameters.Add("@artist", DBNull.Value);
                                else
                                    updCommand.Parameters.Add("@artist", @artist);
                                updCommand.Parameters.AddWithValue("@senderId", @senderId);
                                updCommand.Parameters.AddWithValue("@senderName", @senderName);
                                updCommand.Parameters.AddWithValue("@timeStamp", @timeStamp);
                                updCommand.Parameters.AddWithValue("@channelId", @channelId);
                                if (string.IsNullOrEmpty(showTitle))
                                    updCommand.Parameters.Add("@showTitle", DBNull.Value);
                                else
                                    updCommand.Parameters.Add("@showTitle", @showTitle);
                                dBaccess.executeQuery(updCommand);
                            }
                        }
                        else
                        {
                            dBaccess.closeConn();
                            SqlCommand insertCommand = new SqlCommand("insert into rds_details(sender_id, sender_name, title, artist, rdsTimeStampCreated, channel_id) values(@senderId, @senderName, @title, @artist, @timeStamp, @channelId)");
                            if (string.IsNullOrEmpty(title))
                                insertCommand.Parameters.Add("@title", DBNull.Value);
                            else
                                insertCommand.Parameters.Add("@title", @title);
                            if (string.IsNullOrEmpty(artist))
                                insertCommand.Parameters.Add("@artist", DBNull.Value);
                            else
                                insertCommand.Parameters.Add("@artist", @artist);
                            insertCommand.Parameters.AddWithValue("@senderId", @senderId);
                            insertCommand.Parameters.AddWithValue("@senderName", @senderName);
                            insertCommand.Parameters.AddWithValue("@timeStamp", @timeStamp);
                            insertCommand.Parameters.AddWithValue("@channelId", @channelId);
                            if (string.IsNullOrEmpty(showTitle))
                                insertCommand.Parameters.Add("@showTitle", DBNull.Value);
                            else
                                insertCommand.Parameters.Add("@showTitle", @showTitle);
                            dBaccess.executeQuery(insertCommand);
                        }
                        if (dBaccess.readDatathroughReader(deleteQ).HasRows)
                        {
                            dBaccess.closeConn();
                            SqlCommand deleteCommand = new SqlCommand(deleteQ);
                            dBaccess.executeQuery(deleteCommand);
                        }
                    }
                    finally
                    {
                        dBaccess.closeConn();
                    }
                  


                }
                Console.WriteLine("The application updated at {0:HH:mm:ss.fff}", DateTime.Now);
                Console.ReadLine();


            }


            
        }
    }
}
