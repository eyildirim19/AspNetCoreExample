using Microsoft.AspNetCore.Identity;

namespace AspNetCoreExample.Models
{
    public class MyIdentityDescriber : IdentityErrorDescriber
    {

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = "1",
                Description = $"{userName} başkası tarafından alınmış!!!"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = "Email",
                Description = $"{email} başkası tarafında alınmış!!!"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = "Password",
                Description = $"Şifre minumum 3 karakter olmalı"
            };
        }
    }
}
