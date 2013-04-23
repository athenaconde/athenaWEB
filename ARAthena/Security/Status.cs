using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace athena.AR.Security
{
    public enum Status 
    {

        Succeed = 0
        ,Fail = 1
        ,InvalidUserName = 2
        ,InvalidPassword =3
        ,InActiveUser =4
        ,UnknownUser =5
        ,InvalidEmail =6
        ,DuplicateUserName = 7
        ,ValidUser =8
         ,DuplicateEmail = 9
        , InvalidAnswer = 10
        ,InvalidQuestion =11
        ,ProviderError = 12
        ,UserRejected =13

      
    }
}