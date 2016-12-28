using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Amazon.DynamoDBv2.DataModel;

namespace AWSBetting
{
    [DynamoDBTable("HistoryBet")]
    public class HistoryBet
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        public double Quantity { get; set; }
        
        public string TeamId { get; set; }
    }
}