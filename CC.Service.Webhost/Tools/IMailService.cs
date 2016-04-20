namespace CC.Service.Webhost.Services
{
    public interface IMailService
    {
        void SendRegistrationMail(string emailAddress, string firstName);

        void SendPasswordChangeMail(string emailAddress);

        void SendPasswordResetMail(string emailAddress, string temporaryPassword);

        void SendTaskRegistrationMail(Data.Task task, string emailAddress);

        void SendTaskRevokeMail(Data.Task task, string emailAddress);

    }
}