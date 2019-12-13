using System.IO;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CryptChat.Server
{
    public static class FileIO
    {
        public static Config GetConfigFromFile(string path = "config.yaml")
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"{path} does not exist. Please check path to config");
            using var input = new StringReader(File.ReadAllText(path));
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var config = deserializer.Deserialize<Config>(input);
            return config;
        }
    }
}
