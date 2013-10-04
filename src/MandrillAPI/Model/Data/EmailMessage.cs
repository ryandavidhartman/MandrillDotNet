using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using RestSharp;

namespace MandrillAPI.Model.Data
{
    [DataContract(Name = "email_message")]
    public class EmailMessage
    {
        [DataMember(Name = "html")]
        public string Html { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "from_email")]
        public string FromEmail { get; set; }

        [DataMember(Name = "from_name")]
        public string FromName { get; set; }

        [DataMember(Name = "to")]
        public IEnumerable<EmailAddress> To { get; set; }

        [DataMember(Name = "headers")]
        public JsonObject Headers { get; private set; }

        [DataMember(Name = "track_opens")]
        public bool TrackOpens { get; set; }

        [DataMember(Name = "track_clicks")]
        public bool TrackClicks { get; set; }

        [DataMember(Name = "auto_text")]
        public bool AutoText { get; set; }

        [DataMember(Name = "url_strip_qs")]
        public bool UrlStripQs { get; set; }

        [DataMember(Name = "preserve_recipients")]
        public bool PreserveRecipients { get; set; }

        [DataMember(Name = "bcc_address")]
        public string BccAddress { get; set; }

        [DataMember(Name = "merge")]
        public bool Merge { get; set; }

        [DataMember(Name = "important")]
        public bool Important { get; set; }

        [DataMember(Name = "global_merge_vars")]
        public List<MergeVariable> GlobalMergeVars { get; private set; }

        [DataMember(Name = "merge_vars")]
        public List<RecipientMergeVariables> MergeVars { get; private set; }

        [DataMember(Name = "tags")]
        public IEnumerable<string> Tags { get; set; }

        [DataMember(Name = "google_analytics_domains")]
        public IEnumerable<string> GoogleAnalyticsDomains { get; set; }

        [DataMember(Name = "google_analytics_campaign")]
        public string GoogleAnalyticsCampaign { get; set; }

        [DataMember(Name = "metadata")]
        public JsonObject Metadata { get; private set; }

        [DataMember(Name = "recipient_metadata")]
        public IEnumerable<RecipientMetadata> RecipientMetadata { get; set; }

        [DataMember(Name = "attachments")]
        public IEnumerable<Attachment> Attachments { get; set; }

        [DataMember(Name = "images")]
        public IEnumerable<Image> Images { get; set; }

         [DataMember(Name = "raw_message")]
        public string RawMessage { get; set; }

        public void AddGlobalVariable(string name, string content)
        {
            if (GlobalMergeVars == null)
            {
                GlobalMergeVars = new List<MergeVariable>();
            }

            var mv = new MergeVariable
                {
                Name = name,
                Content = content
            };
            GlobalMergeVars.Add(mv);
        }

        public void AddHeader(string name, string value)
        {
            if (Headers == null)
            {
                Headers = new JsonObject();
            }

            Headers[name] = value;
        }

        public void AddRecipientVariable(string recipient, string name, string content)
        {
            if (MergeVars == null)
            {
                MergeVars = new List<RecipientMergeVariables>();
            }

            var entry = MergeVars.FirstOrDefault(e => e.Recipient == recipient);
            if (entry == null)
            {
                entry = new RecipientMergeVariables{Recipient = recipient};
                MergeVars.Add(entry);
            }

            var mv = new MergeVariable
                {
                Name = name,
                Content = content
            };

            entry.MergeVariables.Add(mv);
        }

        public void AddMetadata(string key, string value)
        {
            if (Metadata == null)
            {
                Metadata = new JsonObject();
            }
            Metadata[key] = value;
        }
    }
}
