
using Microsoft.Extensions.Configuration;
using System.IO;



namespace OscaFramework.MicroServices
{
    public class OscaConfig
    {
        public IConfiguration Configuration { get; }
        public IConfigurationSection sessao { get; }
        public string ambiente { get; }


        public OscaConfig(string confAmbiente, IConfiguration IConf )
        {
            ambiente = confAmbiente;
            this.Configuration = IConf;
        }
    }
}
