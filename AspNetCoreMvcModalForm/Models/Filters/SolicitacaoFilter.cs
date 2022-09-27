using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace AspNetCoreMvcModalForm.Models.Filters
{
    public class SolicitacaoFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        [QueryOperator(Operator = WhereOperator.Contains, UseOr = true)]
        public string? Descricao { get; set; }

        //[QueryOperator(Operator = WhereOperator.Equals, UseOr = true)]
        //public DateTime? Data { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals, UseOr = true)]
        public int? Quantidade { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals, UseOr = true)]
        public decimal? Valor { get; set; }

        [QueryOperator(Max = 100)]
        public int? Limit { get; set; } = 10;
        public int? Offset { get; set; } = 0;
        public string Sort { get; set; }
    }
}
