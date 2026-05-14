using Jso.Annotationary.Domain.Response;

namespace Jso.Annotationary.Domain.Errors;

public static class DomainErrors
{
    // All user error define here
    public static class User
    {
        public static readonly Error EmailInUse = new(
            "User.EmailInUse", 
            "Email has been used by another user.");

        public static readonly Error NotFound = new(
            "User.NotFound", 
            "Cannot find this user in system.");
    }

    // All project error define here
    public static class Project
    {
        public static readonly Error NotFound = new(
            "Project.NotFound", 
            "Dự án không tồn tại.");
    }
}
