﻿using System.Threading.Tasks;
using HexMaster.ShortLink.Core.Entities;
using HexMaster.ShortLink.Core.Helpers;
using HexMaster.ShortLink.Core.ShortLinks.Contracts;
using Microsoft.Azure.Cosmos.Table;

namespace HexMaster.ShortLink.Core.ShortLinks.Repositories
{
    public class ShortLinksRepository : IShortLinksRepository
    {
        private readonly ICloudTableFactory _tableFactory;

        public ShortLinksRepository(ICloudTableFactory tableFactory)
        {
            _tableFactory = tableFactory;
        }

        public async Task<bool> CheckIfShortCodeIsUniqueAsync(string shortCode)
        {
            var table = await _tableFactory.GetCloudTableReferenceAsync(TableNames.ShortLinks);

            var partitionKeyFilter = TableQuery.GenerateFilterCondition(
                nameof(ShortLinkEntity.PartitionKey),
                QueryComparisons.Equal,
                PartitionKeys.ShortLinks);
            var shortCodeFilter = TableQuery.GenerateFilterCondition(
                nameof(ShortLinkEntity.ShortCode),
                QueryComparisons.Equal,
                shortCode);

            var queryFilter = TableQuery.CombineFilters(partitionKeyFilter, TableOperators.And, shortCodeFilter);
            var query = new TableQuery<ShortLinkEntity>().Where(queryFilter);
            var segment = await table.ExecuteQuerySegmentedAsync(query, null);

            return segment.Results.Count == 0;
        }
    }
}