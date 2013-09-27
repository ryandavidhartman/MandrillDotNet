namespace MandrillWrapper.Model.Data
{
    public class SenderData
    {
        public string address { get; set; }
        
        public string created_at { get; set; }
        
        public string sent { get; set; }
        
        public bool is_enabled { get; set; }
        
        public int hard_bounces { get; set; }
        
        public int soft_bounces { get; set; }
        
        public int rejects { get; set; }
        
        public int complaints { get; set; }
        
        public int unsubs { get; set; }
        
        public int opens { get; set; }
        
        public int clicks { get; set; }
    }
}
