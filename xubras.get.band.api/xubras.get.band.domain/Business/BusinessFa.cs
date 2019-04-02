namespace xubras.get.band.domain.Business
{
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using xubras.get.band.domain.Contract.Repository;
    using xubras.get.band.domain.Util;

    public class BusinessFa
    {
        #region [ Attributes ]

        private readonly IFaListRepository _faListRepository;
        private readonly Configuration _configuration;

        #endregion

        #region [ Constructor ]

        public BusinessFa(IFaListRepository faListRepository, IOptions<Configuration> configuration)
        {
            _faListRepository = faListRepository;
            _configuration = configuration.Value;
        }

        #endregion

        #region [ Public Methods ]

        public async Task<object> Get(int skip = 1, int take = 10)
        {
            var listFa = await _faListRepository.GetMany(f => f.FaId == 1, GetIncludes());
            var countPages = (listFa.Select(b => b).Count() - 1) / take;

            return listFa.Select(b => b).OrderBy(d => d.FaId).Skip(skip * take).Take(take).ToList();
        }

        public async Task<object> Get(int id)
        {
            return await _faListRepository.GetById(new object[] { id });
        }

        public async Task<object> Get(string name, int skip = 1, int take = 10)
        {
            var objects = _faListRepository.GetByName(name).OrderBy(d => d.FaId).Skip(skip * take).Take(take).ToList();
            var countPages = (objects.Count - 1) / take;

            return objects;
        }

        //public async Task GetFrist()
        //{

        //}

        #endregion

        #region [ Private Methods ]

        private string Paging(int skip = 1, int take = 10)
        {
            return $"{_configuration.BaseUrl}?page={skip}&numberOfRecords={take}";
        }

        private static List<string> GetIncludes()
        {
            return new List<string> { "" };
        }
        #endregion

    }
}
