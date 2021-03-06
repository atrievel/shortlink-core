﻿using System;
using Microsoft.Azure.Cosmos.Table;

namespace HexMaster.ShortLink.Core.Entities
{
    public class HitsAggregateHourlyEntity : TableEntity
    {
        public long TotalHits { get; set; }

        public string DateString { get; set; }
        public string HourString { get; set; }
        public DateTimeOffset AggregateRangeStart { get; set; }
        public DateTimeOffset AggregateRangeEnd { get; set; }
        public string ShortCode { get; set; }
    }
}