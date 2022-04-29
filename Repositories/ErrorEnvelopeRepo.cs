using HlidacStatu.Entities;
using HlidacStatu.Repositories.ES;

using Nest;

using System;
using System.Threading.Tasks;

namespace HlidacStatu.Repositories
{
    public static class ErrorEnvelopeRepo
    {
        public static async Task SaveAsync(ErrorEnvelope errorEnvelope, ElasticClient client = null)
        {
            errorEnvelope.LastUpdate = DateTime.Now;
            var es = client ?? Manager.GetESClient_VerejneZakazkyNaProfiluConverted();
            await es.IndexDocumentAsync<ErrorEnvelope>(errorEnvelope);
        }
    }
}