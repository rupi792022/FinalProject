using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;


namespace finalproject.Models.DAL
{
    public class DataServices
    {
        SqlCommand createCommand(SqlConnection con, string CommandSTR)
        {

            SqlCommand cmd = new SqlCommand();  // create the command object
            cmd.Connection = con;               // assign the connection to the command object
            cmd.CommandText = CommandSTR;       // can be Select, Insert, Update, Delete
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandTimeout = 5; // seconds
            return cmd;
        }

        SqlConnection Connect(string connectionStringName)
        {

            string connectionString = WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();

            return con;

        }
        public int InsertEmail(Volunteer volunteer) // singIn page
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
                int affected = 0;
                // C - Create Command

                SqlCommand selectCommand = createSelectCommand_VEmail(con, volunteer.Volunteer_email);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);


                if (dr.Read())
                { 
                    int EmailisAllredayExist = -1;
                    return EmailisAllredayExist;
                }

                dr.Close();

                con.Open();

                SqlCommand command = CreateInsert_VEmailVPassword(volunteer, con);
                affected = command.ExecuteNonQuery();
                return affected;
            }

            catch (Exception ex)
            {

                throw new Exception("faild in adding new user", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
         }
        SqlCommand createSelectCommand_VEmail(SqlConnection con, string v_email)
        {
            string commandStr = "SELECT email FROM Volunteer_2022 WHERE email = @v_email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@v_email", SqlDbType.VarChar);
            cmd.Parameters["@v_email"].Value = v_email;
            return cmd;
        }

        SqlCommand CreateInsert_VEmailVPassword(Volunteer volunteer, SqlConnection con)
        {
            string insertStr = "INSERT INTO Volunteer_2022 ( [email], [password],[first_name]) VALUES('"+ volunteer.Volunteer_email + "', '" + volunteer.Volunteer_password+ "', '" + volunteer.First_name + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandTimeout = 5;
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }

        public int ReadEmailPassword(string email,string password)
        {

            SqlConnection con = null;
            try
            {
                // Connect
                con = Connect("webOsDB");

                // Create the insert command
                SqlCommand selectCommand =createSelectCommand_VEmail(con, email);
                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                string DBpassword = "";

                if (dr.Read())
                {
                    selectCommand = createSelectCommand_Password(con, email);
                    dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        DBpassword = (string)dr["password"];
                    }
                    if (DBpassword == password)
                    {
                        selectCommand = createSelectCommand_Details(con, email);
                        dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.Read())
                        {
                            // the password and the email are correct and relates to the volunteer and there is no need to update the user details
                            return 0;
                        }
                        //  the password and the email are correct and relates to the volunteer and there is need to update the user details
                        else return 1;
                    }
                    // the email is correct and relates to the volunteer but the password isn't
                    else return -1;
                }
                else {
                    selectCommand = createSelectCommand_MEmailPassword(con, email);
                    dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.Read())
                    {
                        DBpassword = (string)dr["password"];

                        if (DBpassword == password)
                        {
                            // the email is correct and relates to the manager and the password is correct
                            return 2;
                        }
                        // the email is correct and relates to the manager and the password isn't correct
                        else return 3;
                    }
                    // the email isn't correct
                    else return 4;
                    
                       
                }

            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading volunteer by email", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }

        SqlCommand createSelectCommand_Details(SqlConnection con, string email)
        {
            string commandStr = "SELECT first_name FROM Volunteer_2022 WHERE EMAIL = @email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@email", SqlDbType.VarChar);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }

        SqlCommand createSelectCommand_Password(SqlConnection con, string email)
        {
            string commandStr = "SELECT password FROM Volunteer_2022 WHERE EMAIL = @email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@email", SqlDbType.VarChar);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }

        public void UpdateVolunteerDetails(Volunteer volunteer)
        {

            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommand_UpdateDetails(con,volunteer.Volunteer_email ,volunteer.First_name, volunteer.Last_name,volunteer.Date_of_birth,volunteer.Volunteer_type,volunteer.Gender,volunteer.Phone_number,volunteer.Start_date/*,volunteer.Team_name*/);
                selectCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("failed in update the user details", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommand_UpdateDetails(SqlConnection con,string email ,string First_name, string Last_name,string Date_of_birth,string Volunteer_type,string Gender,int Phone_number,string Start_date/*,string Team_name*/)
        {
            string commandStr = "UPDATE Volunteer_2022 SET first_name = @First_name" +
               ",last_name = @Last_name" +
                ",date_of_birth =@Date_of_birth" +
                ",volunteer_type =@Volunteer_type" +
                ",gender =@Gender "+
                ",phone_number =@Phone_number" +
                ",start_date =@Start_date " +
                //",team_name =@Team_name "+
                "WHERE email = @email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@Phone_number", SqlDbType.Int);
            cmd.Parameters["@Phone_number"].Value = Phone_number;
            cmd.Parameters.Add("@First_name", SqlDbType.NVarChar);
            cmd.Parameters["@First_name"].Value = First_name;
            cmd.Parameters.Add("@Last_name", SqlDbType.NVarChar);
            cmd.Parameters["@Last_name"].Value = Last_name; 
            cmd.Parameters.Add("@Date_of_birth", SqlDbType.NVarChar);
            cmd.Parameters["@Date_of_birth"].Value = Date_of_birth; 
            cmd.Parameters.Add("@Volunteer_type", SqlDbType.NVarChar);
            cmd.Parameters["@Volunteer_type"].Value = Volunteer_type; 
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar);
            cmd.Parameters["@Gender"].Value = Gender;
            cmd.Parameters.Add("@Start_date", SqlDbType.NVarChar);
            cmd.Parameters["@Start_date"].Value = Start_date;
            //cmd.Parameters.Add("@Team_name", SqlDbType.NVarChar);
            //cmd.Parameters["@Team_name"].Value = Team_name;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }

        public string Readpassword( string email)
        {


            SqlConnection con = null;


            try
            {
                // Connect
                con = Connect("webOsDB");


                // Create the insert command
                SqlCommand selectCommand = createUpdateCommand_getPassword(con, email);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);


                string getpassword = "";

                while (dr.Read())
                {
                    getpassword = (string)dr["password"];
                }

                dr.Close();
                return getpassword;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in update the user password", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createUpdateCommand_getPassword(SqlConnection con,  string email) {
            string commandStr = "select password from Volunteer_2022 where email = @email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@email", SqlDbType.NVarChar);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }

        // Relates to the manager

        public int ReadLogInManager(string email, string password)
        {

            SqlConnection con = null;


            try
            {
                // Connect
                con = Connect("webOsDB");


                // Create the insert command
                SqlCommand selectCommand = createSelectCommand_MEmailPassword(con, email);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);


               string Mpassword = "";

                if (dr.Read())
                {
                    while (dr.Read())
                    {
                        Mpassword = (string)dr["password"];
                    }

                    dr.Close();
                    if (Mpassword == password)
                        return 0; // the Manager's email is exist and the password is correct 
                    else return -1; // the Manager's email is exist but the password isn't correct
                }
                else return 1; // the Manager's email is not exist in the table
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading manager by email", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }

        }

        SqlCommand createSelectCommand_MEmailPassword(SqlConnection con, string email)
        {
            string commandStr = "SELECT password FROM Manager_2022 WHERE EMAIL = @email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@email", SqlDbType.VarChar);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }

        public int UpdateManagerpassword(string password, string email)
        {

            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");

                SqlCommand selectCommand = createSelectCommand_MEmailPassword(con, email);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);


                if (dr.Read())
                {
                    //dr.Close();

                    //con.Open();

                    SqlCommand updateCommand = createUpdateCommand_MUpdatePassword(con, password, email);
                    updateCommand.ExecuteNonQuery();
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in update the user password", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createUpdateCommand_MUpdatePassword(SqlConnection con, string password, string email)
        {
            string commandStr = "UPDATE Manager_2022 SET password = @password" +
                "WHERE email = @email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@password", SqlDbType.NVarChar);
            cmd.Parameters["@password"].Value = password;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }
    }
}
