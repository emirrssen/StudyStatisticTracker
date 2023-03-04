using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class AspectResults
    {
        public static IResult Check(params IResult[] aspectResults)
        {
            foreach (var aspectResult in aspectResults)
            {
                if (!aspectResult.Success)
                {
                    return aspectResult;
                }
            }

            return null;
        }
    }
}
