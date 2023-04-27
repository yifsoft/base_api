using Application.Utilities.Results;

namespace Application.Business;

public class BusinessRules
{
    public static IResult Run(params IResult[] logics)
    {
        foreach (var result in logics.Where(result => !result.Success))
            return result;

        return new SuccessResult();
    }
}
