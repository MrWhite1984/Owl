using ConfHub.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfHub.Core.Application.News.Interfaces
{
    public interface INewsRepository
    {
        Task<Domain.Entities.News?> GetNewsByIdAsync(Guid id);
        Task<IEnumerable<Domain.Entities.News>> GetPartOfNewsByDateTimeAsync(DateTime startDateTime, int partSize);
        Task AddAsync(Domain.Entities.News entity);
        void Update(Domain.Entities.News entity);
        Task DeleteAsync(Guid id);

    }
}
