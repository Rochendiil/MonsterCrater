using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Monster_Crater.Models;

namespace Monster_Crater.DAL.Interface
{
    public interface IPlayer
    {
        Player Login(string username, string password);
        void Register(string username, string password, string name, string gender);
        MonsterInstantie RandomMonster();
        MonsterManual FillMonstermanual(int playerid);
        bool InUse(string username);
    }
}
