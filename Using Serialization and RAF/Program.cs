/*
 * Purpose: Using Serialization and Random Access Files
 * Author: Elliot Jost
 * When: March 13th 2024
 */
using System;
using System.IO;
using System.Text.Json;

//Event Class for future Objects
public class Event
{
    public int EventNumber { get; set; }
    public string Location { get; set; }
    public string EventName { get; set; }
}

//Main Program
class Program
{
    static void Main()
    {

        //Input Variables
        Console.WriteLine("Event Number:");
        string input = Console.ReadLine();
        int eventNumber = Convert.ToInt32(input);
        Console.WriteLine("Location:");
        string location = Console.ReadLine();
        Console.WriteLine("Event Name:");
        string eventName = Console.ReadLine();
        Console.WriteLine("In Word: ");
        string word = Console.ReadLine();
        
        //NewEvent
        Event Event1 = new Event { EventNumber = eventNumber, Location = location, EventName = eventName };
        //Sterilize
        SerializeEvent(Event1);
        //Desterilize
        Event deserializedEvent = DeserializeEvent();

        //Outputs
        Console.WriteLine("\n\n");
        Console.WriteLine(deserializedEvent.EventNumber);
        Console.WriteLine(deserializedEvent.Location);
        Console.WriteLine(deserializedEvent.EventName);

        ReadFromFile(word);
    }

    //Sterilization
    static void SerializeEvent(Event ev)
    {
        string json = JsonSerializer.Serialize(ev);
        File.WriteAllText("event.json", json);
    }

    //Desterilization
    static Event DeserializeEvent()
    {
        string json = File.ReadAllText("event.json");
        return JsonSerializer.Deserialize<Event>(json);
    }

    //ReadingFrom File
    static void ReadFromFile(string word)
    {
        string filePath = "data.txt";
        File.WriteAllText(filePath, word);

        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs))
            {
                string content = sr.ReadToEnd();
                int length = content.Length;

                Console.WriteLine("In Word: " + content);
                Console.WriteLine("The First Character is: \"" + content[0] + "\"");
                Console.WriteLine("The Middle Character is: \"" + content[length / 2] + "\"");
                Console.WriteLine("The Last Character is: \"" + content[length - 1] + "\"");
            }
        }
    }
}
