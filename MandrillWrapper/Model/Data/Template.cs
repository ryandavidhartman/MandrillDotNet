using System.Runtime.Serialization;

namespace MandrillWrapper.Model.Data
{

    [DataContract(Name = "template")]
    public class Template
    {
        [DataMember(Name = "slug")]
        public string Slug { get; set; }

        [DataMember(Name = "name")]
        public string TemplateName { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "from_email")]
        public string FromEmail { get; set; }

        [DataMember(Name = "from_name")]
        public string FromName { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "publish_name")]
        public string PublishName { get; set; }

        [DataMember(Name = "publish_code")]
        public string PublishCode { get; set; }

        [DataMember(Name = "publish_subject")]
        public string PublishSubject { get; set; }

        [DataMember(Name = "publish_from_email")]
        public string PublishFromEmail { get; set; }

        [DataMember(Name = "publish_from_name")]
        public string PublishFromName { get; set; }

        [DataMember(Name = "publish_text")]
        public string PublishText { get; set; }

        [DataMember(Name = "published_at")]
        public string PublishedAt { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "updated_at")]
        public string UpdatedAt { get; set; }
    }
}
