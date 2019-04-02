//namespace bs2.boletos.Domain.Business
//{
//    using Microsoft.Extensions.Options;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Threading.Tasks;
//    using xubras.get.band.domain.Util;

//    public class BusinessListExample
//    {
//        #region [ Attributes ]

//        //private readonly IExampleRepository _exampleRepository;
//        private readonly Configuration _configuration;

//        #endregion

//        #region [ Constructor ]

//        //public BusinessListExample(IExampleRepository exampleRepository, IOptions<Configuration> configuration)
//        //{
//        //    _exampleRepository = exampleRepository;
//        //    _configuration = configuration.Value;
//        //}

//        #endregion

//        #region [ Public Methods ]

//        public async Task<object> Get(int skip = 1, int take = 10)
//        {
//            var exemple = await _exampleRepository.GetMany(e => e.PrimaryKey == 1, GetIncludes());

//            var countPages = (exemple.Select(b => b).Count() - 1) / take;
//            return exemple.Select(b => b).OrderBy(d => d.PrimaryKey).Skip(skip * take).Take(take).ToList();
//            //return new BoletoViewModel
//            //{
//            //    PageCount = countPages,
//            //    Boletos = boletos,
//            //    NextPage = Paging(identificacaoBeneficiario, codigoCarteira, skip + 1, take),
//            //    PreviousPage = (skip - 1) == 0 ? string.Empty : Paging(identificacaoBeneficiario, codigoCarteira, skip, take)
//            //};

//        }

//        public async Task<object> Get(int id)
//        {
//            return await _exampleRepository.GetById(new object[] { id });
//        }

//        public async Task<object> Get(string name, int skip = 1, int take = 10)
//        {
//            var objects = _exampleRepository.GetByName(name).OrderBy(d => d.PrimaryKey).Skip(skip * take).Take(take).ToList();
//            var countPages = (objects.Count - 1) / take;

//            return objects;
//        }

//        public async Task GetFirst()
//        {

//        }

//        #endregion

//        #region [ Private Methods ]

//        private string Paging(int skip = 1, int take = 10)
//        {
//            return $"{_configuration.BaseUrl}?page={skip}&numberOfRecords={take}";
//        }

//        private static List<string> GetIncludes()
//        {
//            return new List<string> { "" };
//        }

//        #endregion
//    }
//}
