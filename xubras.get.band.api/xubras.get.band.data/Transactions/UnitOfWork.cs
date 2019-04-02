using xubras.get.band.data.Persistence.EF;

namespace xubras.get.band.data.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GetBandContext _context;

        public UnitOfWork(GetBandContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
            _context.Dispose();
        }
    }
}