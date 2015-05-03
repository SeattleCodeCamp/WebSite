
namespace OCC.Service.Webhost.Services
{
    using System;
    using System.Net.Mail;
    using System.Text;

    public class SmtpMailService : IMailService
    {
        public void SendRegistrationMail(string emailAddress, string firstName)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException("emailAddress", "email address cannot be empty or null");
            }

            string registrationMessage =
                string.Format("{0}, Thank you for registering for Orlando Code Camp!",
                              firstName);

            const string registrationSubject = "Orlando Code Camp Registration";
            string addressee = string.Format("{0}", emailAddress);

            SendMail(registrationMessage, addressee, registrationSubject);
        }

        public void SendPasswordChangeMail(string emailAddress)
        {
            const string passwordChangeSubject = "Orlando Code Camp Password Reset";
            const string mailMessage = "Your password has been changed. If you did not initiate a password reset, please contact us.";
            SendMail(mailMessage, emailAddress, passwordChangeSubject);
        }

        /// <summary>
        /// Sends email using server and credentials found in config file.
        /// </summary>
        internal void SendMail(string mailMessage, string addressee, string subject)
        {
            var message = new MailMessage
                              {
                                  From = new MailAddress("slobo@dotnetda.org", subject), //enter email address here
                                  IsBodyHtml = true,
                                  Subject = subject,
                                  Body = mailMessage,
                              };
            message.To.Add(new MailAddress(addressee));

            using (var client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.Send(message);
            }
        }

        /// <summary>
        /// If the attendess requests a password reset, we ship the temporary password to them.
        /// </summary>
        /// <param name="emailAddress">Person.Email</param>
        /// <param name="temporaryPassword">New password hash that's set prior to the email send.</param>
        public void SendPasswordResetMail(string emailAddress, string temporaryPassword)
        {
            const string passwordResetSubject = "Orlando Code Camp Password Reset";
            var messageBody =
                string.Format("Your password has been reset to the following temporary password: {0}" + "\n\n" +
                              "\nIf you did not initiate a password reset, please contact us.", temporaryPassword);
            SendMail(messageBody, emailAddress, passwordResetSubject);
        }

        /// <summary>
        /// If an attendee registers for a task, we'll get an email.
        /// </summary>
        /// <param name="task">The task that was just assigned to the user</param>
        /// <param name="emailAddress">The assignee</param>
        public void SendTaskRegistrationMail(Data.Task task, string emailAddress)
        {
            string taskAssignmentSubject = string.Format("Orlando Code Camp Volunteer Task Assignment: {0}", task.Description);

            var mailMessage = new StringBuilder();
            mailMessage.Append("Thanks for volunteering for our event!.  ");
            mailMessage.AppendFormat("This email is to confirm you registered for <b>{0}</b>.  ", task.Description);
            mailMessage.AppendFormat("<b>This task begins at {0} </b> ", task.StartTime.ToShortTimeString());
            mailMessage.AppendFormat("<b>and ends at {0} </b>. ", task.EndTime.ToShortTimeString());

            mailMessage.Append("If you did not register for this task, please contact us immediately. </br>");
            mailMessage.Append("Thank you again for supporting Orlando Code Camp!");

            SendMail(mailMessage.ToString(), emailAddress, taskAssignmentSubject);
        }

        /// <summary>
        /// If the assigned unregisters from the task, the board get an email on this event.
        /// </summary>
        /// <param name="task">A task that have been revoked by the user and that needs to be reassigned</param>
        /// <param name="emailAddress">The board's email address</param>
        public void SendTaskRevokeMail(Data.Task task, string emailAddress)
        {
            string taskRevokeSubject = string.Format("Volunteer has unregistered from {0}", task.Description);

            var mailMessage = new StringBuilder();
            mailMessage.Append("This email is to confirm this task is now available and needs to be re-assigned.");

            mailMessage.AppendFormat("<b> Volunteer Task: {0} </b>", task.Description);
            mailMessage.AppendFormat("<b> Start Time: {0} </b>", task.StartTime.ToShortTimeString());
            mailMessage.AppendFormat("<b> End Time: {0}. </b>", task.EndTime.ToShortTimeString());

            SendMail(mailMessage.ToString(), emailAddress, taskRevokeSubject);
        }
    }
}