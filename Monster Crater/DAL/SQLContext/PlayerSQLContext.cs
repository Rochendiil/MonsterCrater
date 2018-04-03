using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Monster_Crater.DAL.Interface;
using Monster_Crater.Models;

namespace Monster_Crater.DAL.SQLContext
{
    public class PlayerSQLContext : IPlayer
    {
        public Player Login(string username, string password)
        {
            Player player = null;

                string query =
                @" SELECT p.PlayerId, p.UserName, p.Name, p.Gender, i.Monsterballen FROM Player p 
                inner join inventory i on p.InventoryId = i.InventoryId and p.username = @username and p.password = @password";
                using (Database.command = new SqlCommand(query, Database.OpenConnection()))
                {
                    Database.command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    Database.command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                    using (var reader = Database.command.ExecuteReader()) // MySqlDataReader
                    {
                        if (!reader.HasRows) return null;
                                // Check for rows, if 0 give error that user did not give correct username and password

                        while (reader.Read()) // Read advances to the next row.
                        {
                            player = new Player(Convert.ToInt32(reader["PlayerId"]),
                                reader["Name"] as string,
                                reader["Gender"] as string,
                                Convert.ToInt32(reader["Monsterballen"]));
                        }
                    }
                }

                MonsterInstantie monster = null;
                query = @" SELECT mi.MonsterInstantieId, mi.MonsterId, mi.Naam, m.Soort, mi.[Growth Stat], mi.xp, mi.CurrentHealth, m.MaxHealth, m.[Max xp], mi.Level, m.[Evolutie level], m.Attack, m.SpecialAttack, m.Defence, m.SpecialDefence from Player p
                inner join Party pa on pa.TrainerId = p.TrainerId and p.PlayerId = @playerId
                inner join MonsterInstantie mi on mi.MonsterInstantieId = pa.MonsterInstantieId
                inner join Monster m on mi.MonsterId = m.MonsterId";
                using (Database.command = new SqlCommand(query, Database.OpenConnection()))
                {
                    Database.command.Parameters.Add("@playerId", SqlDbType.Int).Value = player.Id;
                    
                    using (var reader = Database.command.ExecuteReader()) // MySqlDataReader
                    {
                        int PartyCount = 0;
                        if (!reader.HasRows)
                            throw new Exception("U heeft een onjuist gebruikersnaam of wachtwoorden opgegeven.");
                        // Check for rows, if 0 give error that user did not give correct username and password

                        while (reader.Read()) // Read advances to the next row.
                        {
                            monster = new MonsterInstantie(Convert.ToInt32(reader["MonsterInstantieId"]),
                                Convert.ToInt32(reader["MonsterId"]),
                                reader["Naam"] as string,
                                (soort)Enum.Parse(typeof(soort), reader["Soort"] as string),
                                Convert.ToInt32(reader["Growth Stat"]),
                                Convert.ToInt32(reader["xp"]),
                                Convert.ToInt32(reader["Max xp"]),
                                Convert.ToInt32(reader["level"]),
                                Convert.ToInt32(reader["Evolutie level"]),
                                Convert.ToInt32(reader["Attack"]),
                                Convert.ToInt32(reader["SpecialAttack"]),
                                Convert.ToInt32(reader["Defence"]),
                                Convert.ToInt32(reader["SpecialDefence"]),
                                Convert.ToInt32(reader["CurrentHealth"]),
                                Convert.ToInt32(reader["MaxHealth"])
                                );
                            if (PartyCount == 0)
                            {
                                monster.Active = true;
                            }
                            player.Party[PartyCount] = monster;
                            PartyCount++;
                        }
                    }
                }
                int monstercount = 0;
                foreach(MonsterInstantie monsteritem in player.Party)
                {
                    if (monsteritem != null)
                    {
                        query = @"SELECT m.MoveId,m.Naam,m.Soort,m.Attack,m.MaxPP ,mm.CurrentPP FROM MonsterInstantie mi
                            inner join MonsterMove mm on mi.MonsterInstantieId = mm.MonsterinstantieId and mi.MonsterInstantieId = @MonsterInstantieId
                            inner join Move m on m.MoveId = mm.MoveId";
                        using (Database.command = new SqlCommand(query, Database.OpenConnection()))
                        {
                            Database.command.Parameters.Add("@MonsterInstantieId", SqlDbType.Int).Value = monsteritem.Id;

                            using (var reader = Database.command.ExecuteReader()) // MySqlDataReader
                            {
                                int movecount = 0;
                                if (!reader.HasRows)
                                    throw new Exception("U heeft een onjuist gebruikersnaam of wachtwoorden opgegeven.");
                                while (reader.Read()) // Read advances to the next row.
                                {
                                    monsteritem.Moves[movecount] = new Move(Convert.ToInt32(reader["MoveId"]),
                                        reader["Naam"] as string,
                                        (soort) Enum.Parse(typeof(soort), reader["Soort"] as string),
                                        Convert.ToInt32(reader["Attack"])
                                    );
                                    movecount++;
                                    
                                }
                            }
                        }
                    }
                }
            return player;
        }

        public void Register(string username, string password, string name, string gender)
        {
            const string query =
                @"insert into Player(Username, Password, Name, Gender)
                  values(@username, @password, @name, @gender)";
            try
            {
                using (Database.command = new SqlCommand(query, Database.OpenConnection()))
                {
                    Database.command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                    Database.command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
                    Database.command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    Database.command.Parameters.Add("@gender", SqlDbType.VarChar).Value = gender;
                    Database.command.ExecuteNonQuery();
                }
                
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                Database._CloseConnection();
            }
        }

        public MonsterInstantie RandomMonster()
        {
            MonsterInstantie monster = null;
            try
            {

                string query = @"insert into MonsterInstantie(MonsterId)
                            values (@MonsterId) ";

                using (Database.command = new SqlCommand(query, Database.OpenConnection()))
                {
                    Random r = new Random();
                    Database.command.Parameters.Add("@MonsterId", SqlDbType.Int).Value = 8;
                    Database.command.ExecuteNonQuery();
                }
                query =
                    @"SELECT mi.MonsterInstantieId, mi.MonsterId, mi.Naam, m.Soort, mi.[Growth Stat], mi.xp, mi.CurrentHealth, m.MaxHealth, m.[Max xp], mi.Level, m.[Evolutie level], m.Attack, m.SpecialAttack, m.Defence, m.SpecialDefence from monsterinstantie mi
                            inner join Monster m
                            on mi.monsterid = m.monsterid
                            and mi.monsterinstantieid = (SELECT max(mi.MonsterInstantieId) from monsterinstantie mi)";

                using (Database.command = new SqlCommand(query, Database.OpenConnection()))
                {
                    using (var reader = Database.command.ExecuteReader()) // MySqlDataReader
                    {
                        while (reader.Read()) // Read advances to the next row.
                        {
                            monster = new MonsterInstantie(Convert.ToInt32(reader["MonsterInstantieId"]),
                                Convert.ToInt32(reader["MonsterId"]),
                                reader["Naam"] as string,
                                (soort) Enum.Parse(typeof(soort), reader["Soort"] as string),
                                Convert.ToInt32(reader["Growth Stat"]),
                                Convert.ToInt32(reader["xp"]),
                                Convert.ToInt32(reader["Max xp"]),
                                Convert.ToInt32(reader["level"]),
                                Convert.ToInt32(reader["Evolutie level"]),
                                Convert.ToInt32(reader["Attack"]),
                                Convert.ToInt32(reader["SpecialAttack"]),
                                Convert.ToInt32(reader["Defence"]),
                                Convert.ToInt32(reader["SpecialDefence"]),
                                Convert.ToInt32(reader["CurrentHealth"]),
                                Convert.ToInt32(reader["MaxHealth"])
                            );

                        }

                    }
                }
                query = @"SELECT m.MoveId,m.Naam,m.Soort,m.Attack,m.MaxPP ,mm.CurrentPP FROM MonsterInstantie mi
                            inner join MonsterMove mm on mi.MonsterInstantieId = mm.MonsterinstantieId and mi.MonsterInstantieId = @MonsterInstantieId
                            inner join Move m on m.MoveId = mm.MoveId";
                using (Database.command = new SqlCommand(query, Database.OpenConnection()))
                {
                    Database.command.Parameters.Add("@MonsterInstantieId", SqlDbType.Int).Value = monster.Id;

                    using (var reader = Database.command.ExecuteReader()) // MySqlDataReader
                    {
                        int movecount = 0;
                        if (!reader.HasRows)
                            throw new Exception("U heeft een onjuist gebruikersnaam of wachtwoorden opgegeven.");
                        while (reader.Read()) // Read advances to the next row.
                        {
                            monster.Moves[movecount] = new Move(Convert.ToInt32(reader["MoveId"]),
                                reader["Naam"] as string,
                                (soort) Enum.Parse(typeof(soort), reader["Soort"] as string),
                                Convert.ToInt32(reader["Attack"])
                            );
                            movecount++;

                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return monster;
        }

        public MonsterManual FillMonstermanual(int playerid)
        {
            List<MonsterEntry> monsterlist = new List<MonsterEntry>();
            try
            {

                string query = @"Select m.*, mmm.* from MonsterManual mm
                            inner join Inventory i on i.InventoryId = mm.InventoryId
                            inner join Player p on p.InventoryId = i.InventoryId
                            and p.PlayerId = @playerid
                            inner join MonsterManualMonster mmm on mmm.ManualId = mm.ManualId
                            inner join Monster m on m.MonsterId = mmm.MonsterId";
                using (Database.command = new SqlCommand(query, Database.OpenConnection()))
                {
                    Database.command.Parameters.Add(@"playerid", SqlDbType.Int).Value = playerid;

                    using (var reader = Database.command.ExecuteReader()) // MySqlDataReader
                    {

                        if (!reader.HasRows)
                            throw new Exception("U heeft een onjuist gebruikersnaam of wachtwoorden opgegeven.");
                        while (reader.Read()) // Read advances to the next row.
                        {
                            bool lmao = Convert.ToBoolean(reader["Monster Seen"]);
                            monsterlist.Add(new MonsterEntry(reader["Naam"] as string,
                                    Convert.ToBoolean(reader["Monster Seen"]),
                                    Convert.ToBoolean(Convert.ToInt32(reader["Monster Catched"])),
                                    Convert.ToInt32(reader["Max xp"]),
                                    Convert.ToInt32(reader["Evolutie level"]),
                                    (soort) Enum.Parse(typeof(soort), reader["Soort"] as string),
                                    Convert.ToInt32(reader["Attack"]),
                                    Convert.ToInt32(reader["SpecialAttack"]),
                                    Convert.ToInt32(reader["Defence"]),
                                    Convert.ToInt32(reader["SpecialDefence"])
                                )
                            
                            );
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return new MonsterManual(monsterlist);
        }

        public bool InUse(string username)
        {

            string query = @"Select p.Username from player p where username = @username";
            using (Database.command = new SqlCommand(query, Database.OpenConnection()))
            {
                Database.command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;

                using (var reader = Database.command.ExecuteReader()) // MySqlDataReader
                {

                    if (!reader.HasRows)
                    {
                        return false;
                    }
                        
                }
            }
            return true;
        }
    }
}
