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

        //--------------------------------------------------------------------------//
        //________________________Relates to the Volunteer _________________________//
        //--------------------------------------------------------------------------//

        public bool InsertEmail(Volunteer volunteer) // SingIn page
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

                throw new Exception("failed to add new user", ex);
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
            string insertStr = "INSERT INTO Volunteer_2022 ( [email], [password]) VALUES('" + volunteer.Volunteer_email + "', '" + volunteer.Volunteer_password + "')";
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

                throw new Exception("failed to read volunteer by email", ex);
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

                SqlCommand selectCommand = createSelectCommand_Password_v(con, email);

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

                throw new Exception("failed to read the password", ex);
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

                throw new Exception("failed to read the password", ex);
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
                    if (dr["first_name"] != DBNull.Value)
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

                throw new Exception("failed to check if there is values in the volunteer details", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        SqlCommand createSelectCommand_Details(SqlConnection con, string email)
        {
            string commandStr = "SELECT first_name,last_name,email FROM Volunteer_2022 WHERE EMAIL = @email";
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
                SqlCommand selectCommand = createSelectCommand_UpdateDetails(con, volunteer.Volunteer_email, volunteer.First_name, volunteer.Last_name, volunteer.Date_of_birth, volunteer.Volunteer_type, volunteer.Gender, volunteer.Phone_number, volunteer.Start_date, volunteer.Language, volunteer.Volunteer_password);
                selectCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("failed to update the user details", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommand_UpdateDetails(SqlConnection con, string email, string First_name, string Last_name, string Date_of_birth, string Volunteer_type, string Gender, int Phone_number, string Start_date, string Language, string password)
        {
            string commandStr = "UPDATE Volunteer_2022 SET first_name = @First_name" +
               ",last_name = @Last_name" +
                ",date_of_birth =@Date_of_birth" +
                ",volunteer_type =@Volunteer_type" +
                ",gender =@Gender " +
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

        public Volunteer ReadVolunteer_V(string email) //for localStorage in VolunteerHome
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


                Volunteer v = new Volunteer();
                while (dr.Read())
                {
                    v.First_name = (string)dr["first_name"];
                    v.Last_name = (string)dr["last_name"];
                    v.Volunteer_email = (string)dr["email"];
                }

                return v;
            }

            catch (Exception ex)
            {

                throw new Exception("failed to read the volunteer details", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }

        }

        //--------------------------------------------------------------------------//
        //__________________________Relates to the manager__________________________//
        //--------------------------------------------------------------------------//

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
                throw new Exception("failed to read manager by email", ex);
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

                throw new Exception("failed to read the password", ex);
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

                throw new Exception("failed to read the password", ex);
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

        public Manager ReadManager_M(string email) //for localStorage in ManagerHome
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
                // C - Create Command

                SqlCommand selectCommand = createSelectCommand_DetailsManager(con, email);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);


                Manager m = new Manager();
                while (dr.Read())
                {
                    m.First_name = (string)dr["first_name"];
                    m.Last_name = (string)dr["last_name"];
                }

                return m;
            }

            catch (Exception ex)
            {

                throw new Exception("failed to read the manager details", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }

        }

        SqlCommand createSelectCommand_DetailsManager(SqlConnection con, string email)
        {
            string commandStr = "SELECT first_name,last_name FROM Manager_2022 WHERE EMAIL = @email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@email", SqlDbType.VarChar);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }


        //--------------------------------------------------------------------------//
        //_____________________Relates to the Guiding Program_______________________//
        //--------------------------------------------------------------------------//

        public int Read_Max_Program() // Reading of the values 
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
                // C - Create Command

                SqlCommand selectCommand = createSelectCommand_MaxProgram(con);

                // Execute the command
                //
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                int maxProgram = 0;

                while (dr.Read())
                {
                    if (dr["guiding_serial_num"] != DBNull.Value)
                        maxProgram = Convert.ToInt16(dr["guiding_serial_num"]);
                }

                return maxProgram;

            }

            catch (Exception ex)
            {

                throw new Exception("failed to read level", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        SqlCommand createSelectCommand_MaxProgram(SqlConnection con)
        {
            string commandStr = "SELECT max(guiding_serial_num) as guiding_serial_num FROM Guiding_program_2022";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }

        public void InsertProgram(GuidingProgram GP) // insert level into a guiding program 
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");

                SqlCommand command = Create_InsertProgram(GP, con);
                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

                throw new Exception("failed to add level into a guiding program", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        SqlCommand Create_InsertProgram(GuidingProgram GP, SqlConnection con)
        {
            string insertStr = "INSERT INTO Guiding_program_2022 ( [guiding_serial_num],[file_path], [program_name],[content_level]) VALUES('" + GP.Guiding_serial_num + "', '" + GP.File_path + "', '" + GP.Program_name + "', '" + GP.Content_level + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandTimeout = 5;
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }


        public void InsertQandA(List<QandA> qa) // insert level into a guiding program 
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");
                foreach (var q in qa)
                {
                    SqlCommand command = Create_InsertProgram(q, con);
                    command.ExecuteNonQuery();
                }

            }

            catch (Exception ex)
            {

                throw new Exception("failed to add level into a guiding program", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        SqlCommand Create_InsertProgram(QandA qa, SqlConnection con)
        {
            string insertStr = "INSERT INTO Q_and_A_2022 ([question_serial_num],[question_content],[answers],[guiding_serial_num]) VALUES('" + qa.Question_serial_num + "', '" + qa.Question_content + "', '" + qa.Answers + "', '" + qa.Guiding_serial_num + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandTimeout = 5;
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }

       

        public void UpdateGuidingDetails(GuidingProgram gp) // Update the values 
        {

            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommand_UpdateGuidingDetails(con, gp.Guiding_serial_num, gp.File_path, gp.Content_level, gp.Program_name);
                selectCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("failed to update the Guiding details", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommand_UpdateGuidingDetails(SqlConnection con, int guiding_serial_num, string file_path, string content_level,  string program_name)
        {
            string commandStr = "UPDATE Guiding_program_2022 SET"+
               " file_path = @file_path" +
                ",content_level = @content_level" +
                ",program_name = @program_name" +
                " WHERE guiding_serial_num = @guiding_serial_num";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@guiding_serial_num", SqlDbType.NVarChar);
            cmd.Parameters["@guiding_serial_num"].Value = guiding_serial_num;
            cmd.Parameters.Add("@content_level", SqlDbType.NVarChar);
            cmd.Parameters["@content_level"].Value = content_level;
            cmd.Parameters.Add("@file_path", SqlDbType.NVarChar);
            cmd.Parameters["@file_path"].Value = file_path;
            cmd.Parameters.Add("@program_name", SqlDbType.NVarChar);
            cmd.Parameters["@program_name"].Value = program_name;
            return cmd;
        }

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

                    gp.File_path = (string)dr["file_path"];
                    gp.Program_name = (string)dr["program_name"];
                    gp.Guiding_serial_num = Convert.ToInt16(dr["guiding_serial_num"]);
                    gp.Content_level = (string)dr["content_level"];
                    GP_List.Add(gp);
                }
                return GP_List;
            }
            catch (Exception ex)
            {

                throw new Exception("failed to read the GuidingProgram", ex);
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


        public int Read_maxLevel()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommand_maxLevel(con);
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                int maxLevel = 0;

                while (dr.Read())
                {
                    maxLevel = Convert.ToInt16(dr["level_serial_num"]);
                }
                return maxLevel;
            }
            catch (Exception ex)
            {

                throw new Exception("failed to read the maxLevel", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommand_maxLevel(SqlConnection con)
        {
            string commandStr = "SELECT max(level_serial_num) as 'level_serial_num'  FROM Guiding_program_2022";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }

        public void DeleteProgram(int numProgram)
        {

            SqlConnection con = null;


            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommand_DeleteProgram(con, numProgram);
                selectCommand.ExecuteNonQuery();


            }
            catch (Exception ex)
            {

                throw new Exception("failed to delete the program", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommand_DeleteProgram(SqlConnection con, int numProgram)
        {
            string commandStr = "DELETE FROM Guiding_program_2022 WHERE guiding_serial_num = @numProgram";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@numProgram", SqlDbType.NVarChar);
            cmd.Parameters["@numProgram"].Value = numProgram;
            return cmd;
        }

        //--------------------------------------------------------------------------//
        //_____________________Relates to the Performs Program_______________________//
        //--------------------------------------------------------------------------//

        public void InsertPerforms(PerformsProgram p)
        {

            SqlConnection con = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");

                SqlCommand command = Create_InsertPerforms(p, con);
                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

                throw new Exception("failed to add the Performs Program", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        SqlCommand Create_InsertPerforms(PerformsProgram p, SqlConnection con)
        {
            string insertStr = "INSERT INTO Performs_program_2022 ( [guiding_serial_num],[score],[email]) VALUES('" + p.Guiding_serial_num + "', '" + p.Score + "', '" + p.Volunteer_email + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandTimeout = 5;
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }

        public int Read_maxProPerforms(string email)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommand_Read_maxProPerforms(con, email);
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                int maxP = 0;

                while (dr.Read())
                {
                    if (dr["maxPro"] != DBNull.Value)
                        maxP = Convert.ToInt16(dr["maxPro"]);
                }
                return maxP;
            }
            catch (Exception ex)
            {

                throw new Exception("failed to read the maxlevel", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommand_Read_maxProPerforms(SqlConnection con, string email)
        {
            string commandStr = "SELECT max(guiding_serial_num) as 'maxPro' FROM Performs_program_2022 where email = @email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@email", SqlDbType.NVarChar);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }



        //////QandA

        public List<QandA> Read_QandA(int numProgram)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommand_QandA(con, numProgram);
                List<QandA> Q_List = new List<QandA>();
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    QandA q = new QandA();

                    q.Question_serial_num = Convert.ToInt16(dr["question_serial_num"]);
                    q.Question_content = (string)dr["question_content"];
                    q.Guiding_serial_num = Convert.ToInt16(dr["guiding_serial_num"]);
                    q.Answers = (string)dr["answers"];
                    Q_List.Add(q);
                }
                return Q_List;
            }
            catch (Exception ex)
            {

                throw new Exception("failed to read the GuidingProgram", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommand_QandA(SqlConnection con, int numProgram)
        {
            string commandStr = "SELECT * FROM Q_and_A_2022 where guiding_serial_num =@numProgram ";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@numProgram", SqlDbType.NVarChar);
            cmd.Parameters["@numProgram"].Value = numProgram;
            return cmd;
        }

        public void UpdateQandADetails(List<QandA> qa) // Update the values 
        {

            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                List<QandA> Q_List = new List<QandA>();
                Q_List = Read_QandA(qa[0].Guiding_serial_num);
                foreach (var q in qa)
                {
                    if (q.Question_serial_num > Q_List.Count)
                    {
                        SqlCommand command = Create_InsertProgram(q, con);
                        command.ExecuteNonQuery();
                    }
                    else 
                    {
                        SqlCommand selectCommand = createSelectCommand_UpdateQandADetails(con, q.Question_serial_num, q.Question_content, q.Answers, q.Guiding_serial_num);
                        selectCommand.ExecuteNonQuery();
                    }
                   
                }
                if (Q_List.Count > qa.Count)
                {
                    for (int i = 0; i < (Q_List.Count - qa.Count); i++)
                    {
                        SqlCommand selectCommand = createSelectCommand_DeleteQandA(con, Q_List[qa.Count+i].Question_serial_num, Q_List[qa.Count+i].Guiding_serial_num);
                        selectCommand.ExecuteNonQuery();
                    }
                   
                }

            }
            catch (Exception ex)
            {

                throw new Exception("failed to update the Guiding details", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommand_UpdateQandADetails(SqlConnection con,int question_serial_num,string question_content, string answers, int guiding_serial_num)
        {
            string commandStr = "UPDATE Q_and_A_2022 SET" +
                " question_content = @question_content" +
                ",answers = @answers" +
                " WHERE guiding_serial_num = @guiding_serial_num and question_serial_num = @question_serial_num";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@question_serial_num", SqlDbType.SmallInt);
            cmd.Parameters["@question_serial_num"].Value = question_serial_num;
            cmd.Parameters.Add("@question_content", SqlDbType.NVarChar);
            cmd.Parameters["@question_content"].Value = question_content;
            cmd.Parameters.Add("@answers", SqlDbType.NVarChar);
            cmd.Parameters["@answers"].Value = answers;
            cmd.Parameters.Add("@guiding_serial_num", SqlDbType.NVarChar);
            cmd.Parameters["@guiding_serial_num"].Value = guiding_serial_num;

            return cmd;
        }

        private SqlCommand createSelectCommand_DeleteQandA(SqlConnection con, int question_serial_num, int guiding_serial_num)
        {
            string commandStr = "DELETE FROM Q_and_A_2022 WHERE guiding_serial_num = @guiding_serial_num and question_serial_num = @question_serial_num";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@guiding_serial_num", SqlDbType.NVarChar);
            cmd.Parameters["@guiding_serial_num"].Value = guiding_serial_num;
            cmd.Parameters.Add("@question_serial_num", SqlDbType.SmallInt);
            cmd.Parameters["@question_serial_num"].Value = question_serial_num;
            return cmd;
        }

        public void DeleteQandA(int numProgram)
        {

            SqlConnection con = null;


            try
            {
                con = Connect("webOsDB");
                List<QandA> Q_List = new List<QandA>();
                Q_List = Read_QandA(numProgram);
                for (int i = 1; i < (Q_List.Count + 1); i++)
                {
                    SqlCommand selectCommand = createSelectCommand_DeleteQandA(con, i, numProgram);
                    selectCommand.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

                throw new Exception("failed to delete the program", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        ////////////////Twitter///////////////////
        
        public void InsertTweet (Twitter twitter)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommand_InsertTweet(con, twitter);
                selectCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("failed to save the tweet", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        
        private SqlCommand createSelectCommand_InsertTweet(SqlConnection con, Twitter t)
        {
            string insertStr = "INSERT INTO Tweets_2022 ( [email_v],[tweet_id], [content_text],[created_at],[country],[link_url],[network],[hashtag],[author_name],[status],[lang] ) VALUES('" + t.Volunteer_email + "', '" + t.Tweet_id + "', '" + t.ContentText.Replace("'", "") + "', '" + t.Created_at + "', '" + t.Country + "', '" + t.LinkUrl + "', '" + t.Network + "', '" + t.Hashtag + "', '" + t.Author_name + "', '" + t.Status + "', '" + t.Lang + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeouti
            command.CommandTimeout = 5;
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }

        public List<Twitter> getTweets()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                List<Twitter> tweets_List = new List<Twitter>();
               
                SqlCommand selectCommand = createSelectCommand_notRemoved_tw(con);
                SqlDataReader dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Twitter t = new Twitter();
                    t.LinkUrl = (string)dr["link_url"];
                    t.Volunteer_email = (string)dr["email_v"];
                    t.Country = (string)dr["country"];
                    t.Created_at = Convert.ToDateTime(dr["created_at"]);
                    t.Network = (string)dr["network"];
                    t.Lang = (string)dr["lang"];
                    t.Status = (string)dr["status"];
                    tweets_List.Add(t);
                }
                return tweets_List;

            }
            catch (Exception ex)
            {

                throw new Exception("failed to read the tweet", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand createSelectCommand_notRemoved_tw(SqlConnection con)
        {
            string insertStr = "select link_url,email_v,country,status,created_at,network,lang from Tweets_2022 where lower(status) = 'not removed'";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeouti
            command.CommandTimeout = 5;
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }

        public void UpdateStatus(List<Twitter> notRe_tweets)
        {

            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                if (notRe_tweets.Count > 0)
                {
                    foreach (var t in notRe_tweets)
                    {
                        SqlCommand selectCommand = createSelectCommand_update_status(con, t.Tweet_id, t.Status);
                        selectCommand.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception("failed to update tweets", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommand_update_status(SqlConnection con, long id, string status)
        {
            string commandStr = "UPDATE Tweets_2022 SET" +
                ",status = @status" +
                " WHERE id = @id" ;
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@status", SqlDbType.NVarChar);
            cmd.Parameters["@status"].Value = status;
            cmd.Parameters.Add("@id", SqlDbType.NVarChar);
            cmd.Parameters["@id"].Value = id;
            return cmd;
        }

    }
}