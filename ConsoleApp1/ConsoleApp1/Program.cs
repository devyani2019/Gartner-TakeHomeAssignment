// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;

public class Program
{

    public static JArray root = null;
    private static void Main(string[] args)
    {
    string fileName = @"C:\Users\dchaturvedi\Downloads\Take Home Assignment\software engineer\coding\feed-products\capterra.yaml";
    string fileName1 = @"C:\Users\dchaturvedi\Downloads\Take Home Assignment\software engineer\coding\feed-products\softwareadvice.json";

        //Take the input command from the user
        Console.WriteLine("Enter the import file name");
        string input = Console.ReadLine();

        if(input.Contains(".yaml"))
        {
            // Iterate for YAML file
            FileStream stream = File.OpenRead(fileName);
            var deserializer = new DeserializerBuilder().Build();
            var yamlObject = deserializer.Deserialize(new StreamReader(stream));
            var serializer = new SerializerBuilder()
                    .JsonCompatible()
                    .Build();
            string jsonString = serializer.Serialize(yamlObject);
            root = JArray.Parse(jsonString);
            List<YamlModel>? yamlList = root.ToObject<List<YamlModel>>();

            //iterate all values in array
            foreach (var item in yamlList)
            {
                Console.WriteLine("importing: Name:" + item.name + "  " + "Categories:" + item.tags + ";" + "Twitter:@" + item.twitter);

            }
        }

        else if (input.Contains(".json"))
        {
            //Iterate for Json file
            using (StreamReader r = new StreamReader(fileName1))
            {
                string jsonstring = r.ReadToEnd();
                JObject obj = JObject.Parse(jsonstring);
                var jsonArray = JArray.Parse(obj["products"].ToString());
                List<JsonModel>? jsonList = jsonArray.ToObject<List<JsonModel>>();

                //iterate all values in list
                foreach (var jToken in jsonList)
                {
                    foreach (var number in jToken.categories)
                    {
                        Console.WriteLine("importing: Name:" + jToken.title + "  " + "Categories:" + number + ";" + "Twitter:" + jToken.twitter);
                    }

                }
            }
        }

        else
        {
            Console.WriteLine("The file is not found. Please enter the correct format");
        }       

        Console.WriteLine("------------------------------");

        
        
    }
}