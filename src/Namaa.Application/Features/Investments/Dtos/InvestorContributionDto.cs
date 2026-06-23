using System.Reflection.PortableExecutable;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investments.Dtos;
public class InvestorContributionDto
{
    public Guid Id {get;set;}
    public Guid InvestmentProjectId {get;set;}
    public decimal Amount {get;set;}
    public decimal? ProfitAmount {get;set;}
    public string Status {get;set;}=string.Empty;

}