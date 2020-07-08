namespace TGenWebApp.ResponseModels.Core {
    public class UserBasicResponseModel {
        public string institutionId { get; set; }
        public string name { get; set; }
        public string emailId { get; set; }
        public string photo { get; set; }
        public long phoneNumber { get; set; }
        public string address { get; set; }
        public Address addressLocation { get; set; } = new Address();
        public int pincode { get; set; }
        public bool initialSetup { get; set; }
    }

    public class Address {
        public string name { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
    }
}