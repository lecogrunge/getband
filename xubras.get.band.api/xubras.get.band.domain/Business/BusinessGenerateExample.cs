namespace bs2.boletos.Domain.Business
{
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;
    using xubras.get.band.domain.Util;

    public class BusinessGenerateExample
    {
        #region [ Attributes ]

        private readonly Configuration _configuration;

        #endregion

        #region [ Constructor ]

        public BusinessGenerateExample(IOptions<Configuration> configuration)
        {
            _configuration = configuration.Value;
        }

        #endregion

        #region [ Public Methods ]

        public async Task Generate(string boleto)
        {

        }

        #endregion

        #region [ Private Methods ]


        #endregion
    }
}
