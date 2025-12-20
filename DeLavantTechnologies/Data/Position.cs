using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;

namespace DeLavantTechnologies.Data
{
    using AspNetCore.Identity.MongoDbCore.Infrastructure;
    using AspNetCore.Identity.MongoDbCore.Models;
    using MongoDbGenericRepository.Attributes;

    [CollectionName("Positions")]
    public class Position : IDocument<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Version { get; set; } = 1;

        public string Name { get; set; } = "";

        public Position SetVersion(int version)
        {
            Version = version;
            return this;
        }

    }

}
