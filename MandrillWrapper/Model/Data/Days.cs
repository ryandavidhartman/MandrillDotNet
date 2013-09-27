
namespace MandrillWrapper.Model.Data
{
    public class Days
    {

        public SendingStats today { get; set; }

        public SendingStats last_7_days { get; set; }

        public SendingStats last_30_days { get; set; }

        public SendingStats last_60_days { get; set; }

        public SendingStats last_90_days { get; set; }

        public SendingStats all_time { get; set; }

    }
}
