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
        public bool InsertEmail(Volunteer volunteer) // singIn page
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
                bool emailExist = ReadEmail_V(volunteer.Volunteer_email);
                if (emailExist == false)
                {
                    SqlCommand command = CreateInsert_VEmailVPassword(volunteer, con);
                    command.ExecuteNonQuery();
                    return emailExist;
                }
                else return emailExist;

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
            string insertStr = "INSERT INTO Volunteer_2022 ( [email], [password]) VALUES('"+ volunteer.Volunteer_email + "', '" + volunteer.Volunteer_password+"')";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandTimeout = 5;
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }

        public bool ReadEmail_V(string email)
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
                // C - Create Command

                SqlCommand selectCommand = createSelectCommand_VEmail(con, email);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);


                if (dr.Read())
                {
                    return true;
                }
                return false;

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

        public bool ReadPassword_V(string email, string password)
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
                // C - Create Command

                SqlCommand selectCommand = createSelectCommand_Password_v(con,email);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                string DBpassword = "";
                while (dr.Read())
                {
                    DBpassword = (string)dr["password"];
                }

                if(DBpassword==password)
                {
                    return true;
                }
                return false;

            }

            catch (Exception ex)
            {

                throw new Exception("faild in reading password", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public string ReadEmail_RpasswordV(string email)
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
                // C - Create Command

                SqlCommand selectCommand = createSelectCommand_Password_v(con, email);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                string DBpassword = "";
                while (dr.Read())
                {
                    DBpassword = (string)dr["password"];
                }

                return DBpassword;

            }

            catch (Exception ex)
            {

                throw new Exception("faild in reading password", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        public bool ReadDetails_V(string email)
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
                // C - Create Command

                SqlCommand selectCommand = createSelectCommand_Details(con, email);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);


                string DBdetails = ""; 
                while (dr.Read())
                {
                    if(dr["first_name"] != DBNull.Value)
                    DBdetails = (string)dr["first_name"]; // all the fields are mandatory so it is enough to check only one field to know if all of them are null
                }
                if (DBdetails == "")
                {
                    return true;
                }
                return false;

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

        SqlCommand createSelectCommand_Details(SqlConnection con, string email)
        {
            string commandStr = "SELECT first_name FROM Volunteer_2022 WHERE EMAIL = @email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@email", SqlDbType.VarChar);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }

        SqlCommand createSelectCommand_Password_v(SqlConnection con, string email)
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
                SqlCommand selectCommand = createSelectCommand_UpdateDetails(con,volunteer.Volunteer_email ,volunteer.First_name, volunteer.Last_name,volunteer.Date_of_birth,volunteer.Volunteer_type,volunteer.Gender,volunteer.Phone_number,volunteer.Start_date, volunteer.Language, volunteer.Volunteer_password);
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

        private SqlCommand createSelectCommand_UpdateDetails(SqlConnection con,string email ,string First_name, string Last_name,string Date_of_birth,string Volunteer_type,string Gender,int Phone_number,string Start_date, string Language, string password)
        {
            string commandStr = "UPDATE Volunteer_2022 SET first_name = @First_name" +
               ",last_name = @Last_name" +
                ",date_of_birth =@Date_of_birth" +
                ",volunteer_type =@Volunteer_type" +
                ",gender =@Gender "+
                ",phone_number =@Phone_number" +
                ",start_date =@Start_date " +
                ",language =@Language " +
                ",password = @password " +
                " WHERE email = @email";
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
            cmd.Parameters.Add("@Language", SqlDbType.NVarChar);
            cmd.Parameters["@Language"].Value = Language;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar);
            cmd.Parameters["@password"].Value = password;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }

        /////////////------------- Relates to the manager -------------/////////////

        public bool ReadEmail_M(string email)
        {

            SqlConnection con = null;


            try
            {
                // Connect
                con = Connect("webOsDB");


                // Create the insert command
                SqlCommand selectCommand = createSelectCommand_MEmail(con, email);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);


                if (dr.Read())
                {
                    return true;
                }
                return false;
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

        public bool ReadPassword_M(string email, string password)
        {
            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
                // C - Create Command

                SqlCommand selectCommand = createUpdateCommand_getPassword_m(con, email);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                string DBpassword = "";
                while (dr.Read())
                {
                    DBpassword = (string)dr["password"];
                }

                if (DBpassword == password)
                {
                    return true;
                }
                return false;

            }

            catch (Exception ex)
            {

                throw new Exception("faild in reading password", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public string ReadEmail_RpasswordM(string email)
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
                // C - Create Command

                SqlCommand selectCommand = createUpdateCommand_getPassword_m(con, email);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                string DBpassword = "";
                while (dr.Read())
                {
                    DBpassword = (string)dr["password"];
                }

                return DBpassword;

            }

            catch (Exception ex)
            {

                throw new Exception("faild in reading password", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createUpdateCommand_getPassword_m(SqlConnection con, string email)
        {
            string commandStr = "select password from Manager_2022 where email = @email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@email", SqlDbType.NVarChar);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }

        SqlCommand createSelectCommand_MEmail(SqlConnection con, string email)
        {
            string commandStr = "SELECT email FROM Manager_2022 WHERE EMAIL = @email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@email", SqlDbType.VarChar);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }

        /////////////------------- Relates to the Guiding Program -------------/////////////
        public void InsertLevel(GuidingProgram GP) // insert level into a guiding program 
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
              
                    SqlCommand command = CreateInsert_Level(GP, con);
                    command.ExecuteNonQuery();
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

        SqlCommand CreateInsert_Level(GuidingProgram GP, SqlConnection con)
        {
            string insertStr = "INSERT INTO Guiding_program_2022 ( [date], [program_name],[email],[content_level],[question_content_1], [question_content_2],[question_content_3],[question_content_4], [answers_1],[answers_2],[answers_3], [answers_4]) VALUES('" + GP.Date + "', '" + GP.Program_name + "', '" + GP.Manager_email + "', '" + GP.Content_level + "', '" + GP.Question_content_1 + "', '" + GP.Question_content_2 + "', '" + GP.Question_content_3 + "', '" + GP.Question_content_4+"', '" + GP.Answers_1 + "', '" + GP.Answers_2+ "', '" + GP.Answers_3+ "', '" + GP.Answers_4+ "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandTimeout = 5;
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }

        public GuidingProgram Read_Level(int level_num) // Reading of the values 
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
                // C - Create Command

                SqlCommand selectCommand = createSelectCommand_LevelDetails(con, level_num);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                GuidingProgram gp = new GuidingProgram();
                while (dr.Read())
                {
                    gp.Date = (string)dr["date"];
                    gp.Program_name = (string)dr["program_name"];
                    gp.Manager_email = (string)dr["email"];
                    gp.Level_serial_num = Convert.ToInt16(dr["level_serial_num"]);
                    gp.Content_level = (string)dr["content_level"];
                    gp.Question_content_1 = (string)dr["question_content_1"];
                    gp.Question_content_2 = (string)dr["question_content_2"];
                    gp.Question_content_3 = (string)dr["question_content_3"];
                    gp.Question_content_4 = (string)dr["question_content_4"];
                    gp.Answers_1 = (string)dr["answers_1"];
                    gp.Answers_2 = (string)dr["answers_2"];
                    gp.Answers_3 = (string)dr["answers_3"];
                    gp.Answers_4 = (string)dr["answers_4"];
                }

                return gp;

            }

            catch (Exception ex)
            {

                throw new Exception("faild in reading level", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        SqlCommand createSelectCommand_LevelDetails(SqlConnection con, int level_num)
        {
            string commandStr = "SELECT * FROM Guiding_program_2022 WHERE level_serial_num = @level_num";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@level_num", SqlDbType.SmallInt);
            cmd.Parameters["@level_num"].Value = level_num;
            return cmd;
        }

        public void UpdateLevelDetails(GuidingProgram gp) // Update the values 
        {

            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommand_UpdateLevelDetails(con, gp.Manager_email, gp.Date, gp.Content_level, gp.Level_serial_num, gp.Question_content_1, gp.Question_content_2, gp.Question_content_3, gp.Question_content_4, gp.Answers_1, gp.Answers_2, gp.Answers_3, gp.Answers_4);
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

        private SqlCommand createSelectCommand_UpdateLevelDetails(SqlConnection con, string email, string date, string content_level, int level_serial_num, string question_content_1, string question_content_2, string question_content_3, string question_content_4, string answers_1, string answers_2, string answers_3, string answers_4)
        {
            string commandStr = "UPDATE Guiding_program_2022 SET manager_email = @email" +
               ",date = @date" +
                ",content_level =@content_level" +
                ",question_content_1 =@question_content_1" +
                ",question_content_2 =@question_content_2 " +
                ",question_content_3 =@question_content_3" +
                ",question_content_4 =@question_content_4 " +
                ",answers_1 =@answers_1 " +
                ",answers_2 = @answers_2 " +
                ",answers_3 = @answers_3 " +
                ",answers_4 = @answers_4 " +
                " WHERE level_serial_num = @level_serial_num";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@level_serial_num", SqlDbType.SmallInt);
            cmd.Parameters["@level_serial_num"].Value = level_serial_num;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar);
            cmd.Parameters["@email"].Value = email;
            cmd.Parameters.Add("@date", SqlDbType.NVarChar);
            cmd.Parameters["@date"].Value = date;
            cmd.Parameters.Add("@content_level", SqlDbType.NVarChar);
            cmd.Parameters["@content_level"].Value = content_level;
            cmd.Parameters.Add("@question_content_1", SqlDbType.NVarChar);
            cmd.Parameters["@question_content_1"].Value = question_content_1;
            cmd.Parameters.Add("@question_content_2", SqlDbType.NVarChar);
            cmd.Parameters["@question_content_2"].Value = question_content_2;
            cmd.Parameters.Add("@question_content_3", SqlDbType.NVarChar);
            cmd.Parameters["@question_content_3"].Value = question_content_3;
            cmd.Parameters.Add("@question_content_4", SqlDbType.NVarChar);
            cmd.Parameters["@question_content_4"].Value = question_content_4;
            cmd.Parameters.Add("@answers_1", SqlDbType.NVarChar);
            cmd.Parameters["@answers_1"].Value = answers_1;
            cmd.Parameters.Add("@answers_2", SqlDbType.NVarChar);
            cmd.Parameters["@answers_2"].Value = answers_2;
            cmd.Parameters.Add("@answers_3", SqlDbType.NVarChar);
            cmd.Parameters["@answers_3"].Value = answers_3;
            cmd.Parameters.Add("@answers_4", SqlDbType.NVarChar);
            cmd.Parameters["@answers_4"].Value = answers_4;
            return cmd;
        }

        /////////////------------- Relates to the Guiding -------------/////////////

        public List<GuidingProgram> Read_GP()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommand_GuidingProgram(con);
                List<GuidingProgram> GP_List = new List<GuidingProgram>();
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    GuidingProgram gp = new GuidingProgram();

                    gp.Date = (string)dr["date"];
                    gp.Program_name = (string)dr["program_name"];
                    gp.Manager_email = (string)dr["email"];
                    gp.Level_serial_num = Convert.ToInt16(dr["level_serial_num"]);
                    gp.Content_level = (string)dr["content_level"];
                    gp.Question_content_1 = (string)dr["question_content_1"];
                    gp.Question_content_2 = (string)dr["question_content_2"];
                    gp.Question_content_3 = (string)dr["question_content_3"];
                    gp.Question_content_4 = (string)dr["question_content_4"];
                    gp.Answers_1 = (string)dr["answers_1"];
                    gp.Answers_2 = (string)dr["answers_2"];
                    gp.Answers_3 = (string)dr["answers_3"];
                    gp.Answers_4 = (string)dr["answers_4"];
                    GP_List.Add(gp);
                }
                return GP_List;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of GuidingProgram", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommand_GuidingProgram(SqlConnection con)
        {
            string commandStr = "SELECT * FROM Guiding_program_2022";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }
    }
}
