using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using LogReg.Models;
using Microsoft.Extensions.Options;

namespace LogReg.Factory{
    public class UserFactory : IFactory<User>{

        private readonly IOptions<MySqlOptions> mysqlConfig;

        public UserFactory(IOptions<MySqlOptions> conf) {
            mysqlConfig = conf;
        }

        internal IDbConnection Connection {
            get {
                return new MySqlConnection(mysqlConfig.Value.ConnectionString);
            }
        }
        
    



        // ---------- METHODS ----------- //

        // FIND ALL USERS
        public IEnumerable<User> FindAll(){
            using (IDbConnection dbConnection = Connection){
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM users");
            }
        }


        // ADD USER
        public void AddUser(User user){
            using(IDbConnection dbConnection = Connection){
                string Query = "INSERT into users (FirstName, LastName, Email, Password, CreatedAt, UpdatedAt) VALUES (@FirstName, @LastName, @Email, @Password, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(Query, user);
            }
        }

        // GET USER
        public User GetUser(string email){
            using(IDbConnection dbConnection = Connection){
                dbConnection.Open();
                return dbConnection.QuerySingleOrDefault<User>($"SELECT * FROM users WHERE Email = '{email}'");
            }
        }
    }
}
