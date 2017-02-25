namespace CC.Service.Webhost.Services
{
    public class ChimpMailService : IMailService
    {
        public ChimpMailService()
        {
            // ConfigureMailService();
        }

//        public string MailChimpApiKey { get; set; }

//        private void ConfigureMailService()
//        {
//            NameValueCollection chimpKeys = (NameValueCollection)ConfigurationManager.GetSection("CodeCamp/mailChimp");

//            if (chimpKeys == null)
//            {
//                throw new ConfigurationErrorsException("The chimp section is missin', yo...");
//            }

//            var chimpKey = chimpKeys["apikey"] as string;
//            if (String.IsNullOrEmpty(chimpKey))
//            {
//                throw new ConfigurationErrorsException("The chimp key is missin', yo...");
//            }

//            this.MailChimpApiKey = chimpKey;
//      }

        public void SendRegistrationMail(string emailAddress, string firstName)
        {
            throw new System.NotImplementedException();
        }

        public void SendPasswordChangeMail(string emailAddress)
        {
            throw new System.NotImplementedException();
        }

        public void SendPasswordResetMail(string emailAddress, string temporaryPassword)
        {
            throw new System.NotImplementedException();
        }

        public void SendTaskRegistrationMail(Data.Task task, string emailAddress)
        {
            throw new System.NotImplementedException();
        }

        public void SendTaskRevokeMail(Data.Task task, string emailAddress)
        {
            throw new System.NotImplementedException();
        }
    }
}