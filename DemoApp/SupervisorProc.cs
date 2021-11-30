using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace DemoApp
{
    public class SupervisorRec {
        public String ID { get; set; }
        public String Phone { get; set; }
        public String jurisdiction { get; set; }
        public String identificationNumber { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }

    }

   
    public class SupervisorProc
    {
        public static String ProcessGet()
        {
            string responseFromServer = "";
            Int32 TestInt = 0;
            StreamReader reader;
            WebRequest request = WebRequest.Create("https://o3m5qixdng.execute-api.us-east-1.amazonaws.com/api/managers");
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {

                reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);

            }


            var SupervisorJson = JsonConvert.DeserializeObject<List<SupervisorRec>>(responseFromServer);

            var OutputList = from item in SupervisorJson
                             where (!Int32.TryParse(item.jurisdiction, out TestInt))
                             orderby item.jurisdiction, item.lastName, item.firstName
                             select new
                             { item.jurisdiction, item.lastName, item.firstName };

            String outputString = JsonConvert.SerializeObject(OutputList).ToString();

            return outputString;
        }

        public static JObject ProcessUpdate(JObject InputData)
        {
            String FirstName = InputData["firstname"].ToString();
            String LastName = InputData["lastname"].ToString();
            String email = InputData["email"].ToString();
            String phoneNumber = InputData["phoneNumber"].ToString();
            String supervisor = InputData["supervisor"].ToString();

            Console.WriteLine(FirstName);
            Console.WriteLine(LastName);
            Console.WriteLine(email);
            Console.WriteLine(phoneNumber);
            Console.WriteLine(supervisor);

            if (String.IsNullOrWhiteSpace(FirstName))            
                return FormatResponce("1", "Invalid First Name");

            if (String.IsNullOrWhiteSpace(LastName))
                return FormatResponce("1", "Invalid Last Name");

            if (String.IsNullOrWhiteSpace(supervisor))
                return FormatResponce("1", "Invalid Supervisor");

            return FormatResponce("0", "Update Processed");
        }

        private static JObject FormatResponce(String Status, String Msg)
        { 
            JObject Responce = new JObject();
            Responce.Add("Status", Status);
            Responce.Add("Message", Msg);

            return Responce;
        }
    }
}
