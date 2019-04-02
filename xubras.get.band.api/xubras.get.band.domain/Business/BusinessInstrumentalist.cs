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

    public class BusinessInstrumentalist
    {
        #region [ Attributes ]

        private readonly IInstrumentalistListRepository _instrumentalistListRepository;
        private readonly Configuration _configuration;

        #endregion

        #region [ Constructor ]

        public BusinessInstrumentalist(IInstrumentalistListRepository instrumentalistListRepository, IOptions<Configuration> configuration)
        {
            _instrumentalistListRepository = instrumentalistListRepository;
            _configuration = configuration.Value;
        }

        #endregion


        #region [ Public Methods ]

        public async Task<object> Get(int skip = 1, int take = 10)
        {
            var listInstrumentalist = await _instrumentalistListRepository.GetMany(f => f.InstrumentalistID == 1, GetIncludes());
            var countPages = (listInstrumentalist.Select(b => b).Count() - 1) / take;

            return listInstrumentalist.Select(b => b).OrderBy(d => d.InstrumentalistID).Skip(skip * take).Take(take).ToList();
        }

        public async Task<object> Get(int id)
        {
            return await _instrumentalistListRepository.GetById(new object[] { id });
        }

        public async Task<object> Get(string name, int skip = 1, int take = 10)
        {
            var objects = _instrumentalistListRepository.GetByName(name).OrderBy(d => d.InstrumentalistID).Skip(skip * take).Take(take).ToList();
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
