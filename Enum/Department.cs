using System.ComponentModel;

namespace Enquiry.Web.Enum
{
    public enum Department
    {
        BDA = 1, //Business Development Associate
        Tech, // Technical Expert
        Programming, // Programmer
        Publication
    }

    public enum Roles
    {
        Manager = 1,
        [Description("Team Lead")]
        TeamLead,
        Executive
    }
}
