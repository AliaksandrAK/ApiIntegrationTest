using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiIntegrationTest
{
    class DecisionEngineCutoff
    {
        public int proposal_id { get; set; }

        public int subscriber_id { get; set; }

        public string workspace_id { get; set; }

        public decimal recommendation { get; set; }

        public decimal? cutoff_approve1 { get; set; }

        public decimal? cutoff_approve2 { get; set; }

        public decimal? cutoff_reject1 { get; set; }

        public decimal? cutoff_reject2 { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
