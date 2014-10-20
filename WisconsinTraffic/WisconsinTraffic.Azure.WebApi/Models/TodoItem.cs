using Microsoft.WindowsAzure.Mobile.Service;

namespace WisconsinTraffic.Azure.WebApi.Models
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}