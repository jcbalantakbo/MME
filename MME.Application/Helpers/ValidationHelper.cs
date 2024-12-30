namespace MME.Application.Helpers
{
    public static class ValidationHelper
    {
        public static bool BeAtLeast18YearsOld(DateTime dateOfBirth)
        {
            var dob = DateOnly.FromDateTime(dateOfBirth); // Convert DateTime to DateOnly
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - dob.Year;
            if (dob > today.AddYears(-age)) age--;
            return age >= 18;
        }

        public static bool IsBlacklistedDomain(string email, HashSet<string> blacklistedDomains)
        {
            var domain = email.Split('@').LastOrDefault();
            return blacklistedDomains.Contains(domain);
        }
    }
}
