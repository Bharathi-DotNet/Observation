namespace Enquiry.Web.Enum
{
    public enum WorkStatus
    {
        Assigned = 1,
        Progress,
        Hold,
        Publication,
        Revision,
        Accepted,
        Rejected,
        Completed
    }

    public enum PaymentMode
    {
        Cash = 1,
        Other,
        Bank,
        Link,
    }

    public enum JournalStatus
    {
        Submitted = 1,
        Rejected,
        Major,
        Minor,
        Accepted,
        Published,
        MailAccount,
        ToApply
    }
}
