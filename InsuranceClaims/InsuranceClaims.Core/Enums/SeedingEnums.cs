using System.ComponentModel;

namespace InsuranceClaims.Enums
{
    public enum ApplicationRolesEnum
    {
        [Description("Super Admin")]
        SuperAdmin = 1,
        [Description("Local Admin")]
        LocalAdmin = 2,
        Manager = 3,
        Standard = 4
    }
    public enum ClaimStatusesEnum
    {
        Pending = 1,
        [Description("Pending With Query")]
        PendingWithQuery = 2,
        Cancelled = 3,
        Completed = 4
    }
    public enum CoverageTypesEnum
    {
        [Description("Employee Only")]
        EmployeeOnly = 1,
        [Description("Employee and Child")]
        EmployeeAndChild = 2,
        [Description("Employee and Souse")]
        EmployeeAndSouse = 3,
        [Description("Employee and Family")]
        EmployeeAndFamily = 4
    }
    public enum PolicyTypesEnum
    {
        GroupMedical = 1,
        GroupLife = 2,
        HealthcareHeroes = 3,
        FrontlineHeroes = 4,
        IndividualLife = 5,
        Pensions = 6,
        TermInsurance = 7,
        PeaceAssured = 8,
        PrivateMotor = 9,
        CommercialMotor = 10,
        Homeowners = 11,
        PublicLiability = 12,
        SpecialEventPublicLiability = 13,
        AllRiskInsurance = 14,
        Burglary = 15,
        Machinery = 16,
        Travel = 17,
        CommercialFire = 18,
    }
}
