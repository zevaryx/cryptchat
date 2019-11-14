using System;
using System.Linq;

using YamlDotNet.Serialization;

namespace CryptChatServer
{
    class Config
    {
        public ServerConfig Server {get; set;}
        [YamlMember(Alias = "mongodb", ApplyNamingConventions = false)]
        public MongoDBConfig MongoDB { get; set; }
    }

    class ServerConfig
    {
        private string _bindIP = "0.0.0.0";
        [YamlMember(Alias = "bind_ip", ApplyNamingConventions = false)]
        public string BindIP
        {
            get => _bindIP;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new FormatException("IP address is empty!");
                string[] spl = value.Split('.');
                if (spl.Length != 4) throw new FormatException($"{value} does not have 4 octals!");
                byte tmp;
                if (!spl.All(r => byte.TryParse(r, out tmp))) throw new FormatException($"{value} has octets > 255!");
                _bindIP = value;
            }
        }

        private ushort _port = 55132;
        public ushort Port
        {
            get => _port;
            set
            {
                if (value < 1024) throw new FormatException($"{value} is set to a reserved port. Refusing to set!");
                _port = value;
            }
        }
    }
    class MongoDBConfig
    {
        private string _host = "127.0.0.1";
        public string Host
        {
            get => _host;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new FormatException("IP address is empty!");
                string[] spl = value.Split('.');
                if (spl.Length != 4) throw new FormatException($"{value} does not have 4 octals!");
                byte tmp;
                if (!spl.All(r => byte.TryParse(r, out tmp))) throw new FormatException($"{value} has octets > 255!");
                _host = value;
            }
        }

        private ushort _port = 55132;
        public ushort Port
        {
            get => _port;
            set
            {
                if (value < 1024) throw new FormatException($"{value} is set to a reserved port. Refusing to set!");
                _port = value;
            }
        }

        private string _user;
        public string User
        {
            get => _user;
            set => _user = value;
        }

        private string _passwd;
        public string Passwd
        {
            get => _passwd;
            set => _passwd = value;
        }

        private string _database;
        public string Database
        {
            get => _database;
            set => _database = value;
        }
    }
}
