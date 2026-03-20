using System;
using System.Collections.Generic;
using System.Text;

namespace Yokinsoft.Salesforce.MCE
{
    public class Address : APIClientBase
    {
        public enum Validator
        {
            SyntaxValidator,
            MXValidator,
            ListDetectiveValidator
        }
        public class Validation
        {
            public string Email { get; set; }
            public bool Valid { get; set; }
            public string FailedValidation { get; set; }
        }
        public Address( AccessToken accessToken) : base( accessToken )
        {
        }
        public Validation ValidateEmail(string email, params Validator[] validators )
            => Post<Validation>("/address/v1/validateEmail", new { email, validators } );
    }
}
