using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Mappings
{
    public interface ICustomMapperInterface<TFrom, TTo> 
        where TFrom : class 
        where TTo : class
    {
        TTo Map(TFrom source);

        TFrom ReverseMap(TTo source);
    }
}
