using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptChat.Server.Models
{
    public interface IMongoDatabaseSettings
    {
        string Hostname { get; set; }
        string DatabaseName { get; set; }
        string MongoUser { get; set; }
        string MongoPass { get; set; }
        int Port { get; set; }
        string ConnectionString { get; set; }
    }

    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string Hostname { get; set; }
        public string DatabaseName { get; set; }
        public string MongoUser { get; set; }
        public string MongoPass { get; set; }
        public int Port { get; set; }
        public string ConnectionString { get; set; }
    }
}
