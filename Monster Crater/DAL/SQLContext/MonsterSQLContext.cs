using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;
using System.Windows.Forms;
using Monster_Crater.DAL.Interface;
using Monster_Crater.Models;

namespace Monster_Crater.DAL.SQLContext
{
    public class MonsterSQLContext : IMonster
    {
        public void UpdateMonster(MonsterInstantie monsterInstantie)
        {

            MonsterInstantie monster = null;
            try
            {

                string query = @"EXEC UpdateMonster @MonsterInstantieId,@Name,@Growthstat,@xp,@Level,@CurrentHealth";

                using (Database.command = new SqlCommand(query, Database.OpenConnection()))
                {
                    
                    Database.command.Parameters.Add("@MonsterInstantieId", SqlDbType.Int).Value = monsterInstantie.Id;
                    Database.command.Parameters.Add("@Name", SqlDbType.VarChar).Value = monsterInstantie.Naam;
                    Database.command.Parameters.Add("@Growthstat", SqlDbType.Int).Value = monsterInstantie.GrowthStat;
                    Database.command.Parameters.Add("@xp", SqlDbType.Int).Value = monsterInstantie.Xp;
                    Database.command.Parameters.Add("@Level", SqlDbType.Int).Value = monsterInstantie.Level;
                    Database.command.Parameters.Add("@CurrentHealth", SqlDbType.Int).Value = monsterInstantie.Health;
                    Database.command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
                    Database.command.Parameters.Add("@MonsterId", SqlDbType.Int).Value = r.Next(1,150);
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
                                (soort)Enum.Parse(typeof(soort), reader["Soort"] as string),
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

        public void MonsterSeen(MonsterInstantie monsterInstantie, Player player)
        {
            try
            {

                string query = @"EXEC MonsterSeen @MonsterId, @PlayerId";

                using (Database.command = new SqlCommand(query, Database.OpenConnection()))
                {
                    Database.command.Parameters.Add("@MonsterId", SqlDbType.Int).Value = monsterInstantie.MonsterId;
                    Database.command.Parameters.Add("@PlayerId", SqlDbType.Int).Value = player.Id;
                    Database.command.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public List<string> MonsterFilter(List<string> filterList)
        {
            List<string> statList = new List<string>();
            string query =  @"Exec MonsterFilter @soort,@attack,@specialAttack,@defence,@specialdefence,@maxhealth, @basicmonster";

            using (Database.command = new SqlCommand(query, Database.OpenConnection()))
            {
                Database.command.Parameters.Add("@soort", SqlDbType.NVarChar).Value = filterList[0];
                Database.command.Parameters.Add("@attack", SqlDbType.NVarChar).Value = filterList[1];
                Database.command.Parameters.Add("@specialattack", SqlDbType.NVarChar).Value = filterList[2];
                Database.command.Parameters.Add("@defence", SqlDbType.NVarChar).Value = filterList[3];
                Database.command.Parameters.Add("@specialdefence", SqlDbType.NVarChar).Value = filterList[4];
                Database.command.Parameters.Add("@maxhealth", SqlDbType.NVarChar).Value = filterList[5];
                Database.command.Parameters.Add("@basicmonster", SqlDbType.NVarChar).Value = filterList[6];
                using (var reader = Database.command.ExecuteReader()) // MySqlDataReader
                {
                    while (reader.Read()) // Read advances to the next row.
                    {
                        statList.Add(reader["Aantal"].ToString());
                        statList.Add(reader["Attack"].ToString());
                        statList.Add(reader["SpecialAttack"].ToString());
                        statList.Add(reader["Defence"].ToString());
                        statList.Add(reader["SpecialDefence"].ToString());
                        statList.Add(reader["MaxHealth"].ToString());
                    }
                }
            }

            return statList;
        }

        public List<List<string>> getfilterlist(List<string> filterList)
        {
            string query = @"Exec getMonsterFilter @soort,@attack,@specialAttack,@defence,@specialdefence,@maxhealth, @basicmonster";
            List<string[]> newfilterlist = new List<string[]>();
            List<List<string>> returnList = new List<List<string>>();
            using (Database.command = new SqlCommand(query, Database.OpenConnection()))
            {
                Database.command.Parameters.Add("@soort", SqlDbType.NVarChar).Value = filterList[0];
                Database.command.Parameters.Add("@attack", SqlDbType.NVarChar).Value = filterList[1];
                Database.command.Parameters.Add("@specialattack", SqlDbType.NVarChar).Value = filterList[2];
                Database.command.Parameters.Add("@defence", SqlDbType.NVarChar).Value = filterList[3];
                Database.command.Parameters.Add("@specialdefence", SqlDbType.NVarChar).Value = filterList[4];
                Database.command.Parameters.Add("@maxhealth", SqlDbType.NVarChar).Value = filterList[5];
                Database.command.Parameters.Add("@basicmonster", SqlDbType.NVarChar).Value = filterList[6];
                using (var reader = Database.command.ExecuteReader()) // MySqlDataReader
                {
                    while (reader.Read()) // Read advances to the next row.
                    {
                        string[] array = new string[6];
                        array[0] = reader["Soort"].ToString();
                        array[1] = reader["attack"].ToString();
                        array[2] = reader["SpecialAttack"].ToString();
                        array[3] = reader["defence"].ToString();
                        array[4] = reader["SpecialDefence"].ToString();
                        array[5] = reader["MaxHealth"].ToString();
                        newfilterlist.Add(array);
                    }
                }
            }
            for (int i = 0; i < 6; i++)
            {
                returnList.Add(new List<string>());
            }
            foreach (string[] item in newfilterlist)
            {
                if (!returnList[0].Contains(item[0]))
                {
                    returnList[0].Add(item[0]);
                }
                for (int i = 1; i < 6; i++)
                {
                    if (!returnList[i].Contains(item[i]))
                    {
                        returnList[i].Add(item[i]);
                    }
                }
            }
            return returnList;
        }
    }
}